using Microsoft.Extensions.Configuration;
using org.ohdsi.cdm.framework.common.DataReaders.v5;
using org.ohdsi.cdm.framework.common.DataReaders.v5.v54;
using org.ohdsi.cdm.framework.common.Definitions;
using org.ohdsi.cdm.framework.common.Omop;
using org.ohdsi.cdm.framework.desktop.DbLayer;
using org.ohdsi.cdm.framework.desktop.Helpers;
using org.ohdsi.cdm.presentation.builderwebapi.Database;
using org.ohdsi.cdm.presentation.builderwebapi.Enums;
using org.ohdsi.cdm.presentation.builderwebapi.ETL;
using org.ohdsi.cdm.presentation.builderwebapi.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace org.ohdsi.cdm.presentation.builderwebapi.Controllers
{
    public class ConversionController
    {
        private Settings _settings;

        private int _conversionId;
        private string _connectionString;
        private string _fileManagerUrl;
        private string _userName;
        private string _secureKey;

        public ConversionController(int conversionId)
        {
            _conversionId = conversionId;
        }

        public void Init(IConfiguration conf)
        {
            _connectionString = $"Server={conf["SharedDbHost"]};Port={conf["SharedDbPort"]};Database={conf["SharedDbName"]};User Id={conf["SharedDbBuilderUser"]};Password={conf["SharedDbBuilderPass"]};";
            _fileManagerUrl = conf["FilesManagerUrl"] + "/api";
            _secureKey = conf["BuilderSecretKey"];

            ConversionSettings settings = ConversionSettings.SetProperties(DBBuilder.GetParameters(_connectionString, _secureKey, _conversionId));

            Dictionary<string, string> connectionStringTemplates = new Dictionary<string, string>();
            connectionStringTemplates.TryAdd(settings.SourceEngine, conf[settings.SourceEngine]);
            connectionStringTemplates.TryAdd(settings.DestinationEngine, conf[settings.DestinationEngine]);
            connectionStringTemplates.TryAdd(settings.VocabularyEngine, conf[settings.VocabularyEngine]);

            _settings = new Settings(settings, _fileManagerUrl, connectionStringTemplates);
            _settings.Load();

            _userName = DBBuilder.GetUsername(_connectionString, _conversionId);
        }

        public void Start()
        {
            if (CreateDestination())
            {
                CreateLookup();
                StoreVocabularyToFileManager();
                CreateChunks();
            }
        }

        private void CreateChunks()
        {
            if (DBBuilder.IsConversionAborted(_connectionString, _conversionId))
            {
                DBBuilder.CancelStep(_connectionString, _conversionId, Steps.CreateChunks);
                return;
            }

            DBBuilder.StartStep(_connectionString, _conversionId, Steps.CreateChunks);
            try
            {
                var chunkController = new ChunkController(_settings);
                var chunksCount = chunkController.CreateChunks(_connectionString, _conversionId);
            }
            catch (Exception e)
            {
                DBBuilder.FailStep(_connectionString, _conversionId, Steps.CreateChunks);
                WriteLog(LogType.Error, e.Message, 0);
            }

            DBBuilder.CompleteStep(_connectionString, _conversionId, Steps.CreateChunks);
            DBBuilder.StartStep(_connectionString, _conversionId, Steps.ConvertChunks);
        }

        private void WriteLog(LogType status, string message, Double progress)
        {
            Logger.Write(_connectionString, new LogMessage { ConversionId = _conversionId, Type = status, Text = message });
        }

        private bool CreateDestination()
        {
            WriteLog(LogType.Info, "Creating CDM database...", 0);
            DBBuilder.StartStep(_connectionString, _conversionId, Steps.CreateDestination);

            var dbDestination = new DbDestination(_settings.DestinationConnectionString,
                   _settings.ConversionSettings.DestinationSchema);

            try
            {
                dbDestination.CreateDatabase(_settings.CreateCdmDatabaseScript);
                WriteLog(LogType.Info, "CDM database created", 0);
            }
            catch (Exception e)
            {
                WriteLog(LogType.Warning, "Warning: CDM database exists", 0);
            }

            try
            {
                dbDestination.CreateSchema();
                WriteLog(LogType.Info, "Schema created", 0);
            }
            catch (Exception e)
            {
                WriteLog(LogType.Warning, "Warning: CDM schema exists", 0);
            }

            bool successful;
            try
            {
                dbDestination.ExecuteQuery(_settings.CreateCdmTablesScript);
                WriteLog(LogType.Info, "CDM tables created", 0);
                DBBuilder.CompleteStep(_connectionString, _conversionId, Steps.CreateDestination);

                successful = true;
            }
            catch (Exception e)
            {
                WriteLog(LogType.Warning, "Warning: CDM tables exists" , 0);
                try
                {
                    dbDestination.ExecuteQuery(_settings.TruncateTablesScript);
                    WriteLog(LogType.Info, "CDM tables truncated", 0);
                    successful = true;
                }
                catch (Exception e2)
                {
                    WriteLog(LogType.Error, e2.Message, 0);
                    DBBuilder.FailStep(_connectionString, _conversionId, Steps.CreateDestination);
                    throw e2;
                }
            }

            DBBuilder.CompleteStep(_connectionString, _conversionId, Steps.CreateDestination);

            return successful;
        }

        private void CreateLookup()
        {
            if (DBBuilder.IsConversionAborted(_connectionString, _conversionId))
            {
                DBBuilder.CancelStep(_connectionString, _conversionId, Steps.ConvertHealthSystemData);
                return;
            }

            DBBuilder.StartStep(_connectionString, _conversionId, Steps.ConvertHealthSystemData);

            var timer = new Stopwatch();
            timer.Start();
            var vocabulary = new Vocabulary(_settings, _connectionString, _conversionId);
            vocabulary.Fill(true, false);

            var locationConcepts = new List<Location>();
            var careSiteConcepts = new List<CareSite>();
            var providerConcepts = new List<Provider>();
            var cdmSourceConcepts = new List<CdmSource>();
            var metadataConcepts = new List<MetadataOMOP>();
            var locationHistory = new List<LocationHistory>();
            var cohortConcepts = new List<Cohort>();
            var cohortDefinitionConcepts = new List<framework.common.Omop.CohortDefinition>();

            LoadLocation(locationConcepts);
            LoadCareSite(careSiteConcepts);
            LoadProvider(providerConcepts);
            LoadCdmSource(cdmSourceConcepts);
            LoadMetadata(metadataConcepts);
            LoadLocationHistory(locationHistory);
            LoadCohort(cohortConcepts);
            LoadCohortDefinition(cohortDefinitionConcepts);

            SaveLookup(timer, 
                locationConcepts, 
                careSiteConcepts, 
                providerConcepts, 
                cdmSourceConcepts,
                metadataConcepts,
                locationHistory,
                cohortConcepts,
                cohortDefinitionConcepts);

            locationConcepts.Clear();
            careSiteConcepts.Clear();
            providerConcepts.Clear();
            cdmSourceConcepts.Clear();
            metadataConcepts.Clear();
            locationHistory.Clear();
            cohortConcepts.Clear();
            cohortDefinitionConcepts.Clear();
            locationConcepts = null;
            careSiteConcepts = null;
            providerConcepts = null;
            cdmSourceConcepts = null;
            vocabulary = null;
            metadataConcepts = null;
            locationHistory = null;
            cohortConcepts = null;
            cohortDefinitionConcepts = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            DBBuilder.CompleteStep(_connectionString, _conversionId, Steps.ConvertHealthSystemData);
        }

        private void StoreVocabularyToFileManager()
        {
            var vocabulary = new Vocabulary(_settings, _connectionString, _conversionId);
            vocabulary.Fill(false, false);
            vocabulary.StoreToFileManager(_userName, _fileManagerUrl, _secureKey);
        }

        private void SaveLookup(Stopwatch timer, 
            List<Location> locationConcepts, 
            List<CareSite> careSiteConcepts, 
            List<Provider> providerConcepts, 
            List<CdmSource> cdmSource,
            List<MetadataOMOP> metadata,
            List<LocationHistory> locationHistory,
            List<Cohort> cohortConcepts,
            List<framework.common.Omop.CohortDefinition> cohortDefinitionConcepts)
        {
            if (DBBuilder.IsConversionAborted(_connectionString, _conversionId)) 
                return;

            Console.WriteLine("Saving lookups...");
            WriteLog(LogType.Info, "Saving lookups...", 0);

            var saver = _settings.DestinationEngine.GetSaver();
            using (saver.Create(_settings.DestinationConnectionString,
                _settings.Cdm,
                _settings.ConversionSettings.SourceSchema,
                _settings.ConversionSettings.DestinationSchema))
            {
                saver.SaveEntityLookup(_conversionId,
                    _settings.Cdm, 
                    locationConcepts, 
                    careSiteConcepts, 
                    providerConcepts,
                    cohortDefinitionConcepts,
                    cdmSource,
                    metadata,
                    locationHistory,
                    cohortConcepts);
            }

            if (cdmSource.Count == 0)
            {
                using (saver.Create(_settings.DestinationConnectionString,
                    _settings.Cdm,
                    _settings.ConversionSettings.SourceSchema,
                    _settings.ConversionSettings.DestinationSchema))
                {
                    if (_settings.Cdm == framework.common.Enums.CdmVersions.V53)
                    {
                        var reader = new CdmSourceDataReader(new CdmSource
                        {
                            CdmSourceName = _settings.ConversionSettings.SourceDatabase,
                            CdmSourceAbbreviation = _settings.ConversionSettings.SourceDatabase,
                            SourceDescription = _settings.ConversionSettings.SourceDatabase,
                            CdmEtlReference = "unknown",
                            SourceDocumentationReference = "None",
                            CdmReleaseDate = DateTime.Now,
                            SourceReleaseDate = DateTime.Now,
                            CdmVersion = _settings.GetCdmScriptsFolder,
                            VocabularyVersion = "5.3",
                            CdmHolder = "unknown"
                        });
                        saver.Write(null, null, null, reader, "CDM_SOURCE");
                        saver.Commit();
                    }
                    else if (_settings.Cdm == framework.common.Enums.CdmVersions.V54)
                    {
                        var reader = new CdmSourceDataReader54(new CdmSource
                        {
                            CdmSourceName = _settings.ConversionSettings.SourceDatabase,
                            CdmSourceAbbreviation = _settings.ConversionSettings.SourceDatabase,
                            SourceDescription = _settings.ConversionSettings.SourceDatabase,
                            CdmEtlReference = "unknown",
                            SourceDocumentationReference = "None",
                            CdmReleaseDate = DateTime.Now,
                            SourceReleaseDate = DateTime.Now,
                            CdmVersion = _settings.GetCdmScriptsFolder,
                            VocabularyVersion = "5.4",
                            CdmHolder = "unknown"
                        });
                        saver.Write(null, null, null, reader, "CDM_SOURCE");
                        saver.Commit();
                    }
                    else
                    {
                        var reader = new CdmSourceDataReader(new CdmSource
                        {
                            CdmSourceName = _settings.ConversionSettings.SourceDatabase,
                            CdmSourceAbbreviation = _settings.ConversionSettings.SourceDatabase,
                            SourceDescription = _settings.ConversionSettings.SourceDatabase,
                            CdmEtlReference = "unknown",
                            SourceDocumentationReference = "None",
                            CdmReleaseDate = DateTime.Now,
                            SourceReleaseDate = DateTime.Now,
                            CdmVersion = _settings.GetCdmScriptsFolder,
                            VocabularyVersion = "6.0",
                            CdmHolder = "unknown"
                        });
                        saver.Write(null, null, null, reader, "CDM_SOURCE");
                        saver.Commit();
                    }
                }
            }

            Console.WriteLine("Lookups was saved ");
            WriteLog(LogType.Info, "Lookups was saved", 0);
            timer.Stop();

            //WriteLog(LogType.Info, string.Format("{0}| {1}", DateTime.Now, $"Care site, Location and Provider tables were saved to CDM database - {timer.ElapsedMilliseconds} ms"), 0);
        }

        private void LoadProvider(List<Provider> providerConcepts)
        {
            if (DBBuilder.IsConversionAborted(_connectionString, _conversionId)) 
                return;

            Console.WriteLine("Loading providers...");
            WriteLog(LogType.Info, "Loading providers...", 0);
            var provider = _settings.SourceQueryDefinitions.FirstOrDefault(qd => qd.Providers != null);
            if (provider != null)
            {
                FillList<Provider>(providerConcepts, provider, provider.Providers[0]);
            }
            Console.WriteLine("Providers was loaded");
            WriteLog(LogType.Info, "Providers was loaded", 0);
        }

        private void LoadCdmSource(List<CdmSource> cdmSourceConcepts)
        {
            if (DBBuilder.IsConversionAborted(_connectionString, _conversionId))
                return;

            Console.WriteLine("Loading cdmSource...");
            WriteLog(LogType.Info, "Loading cdmSource...", 0);
            var cdmSource = _settings.SourceQueryDefinitions.FirstOrDefault(qd => qd.CdmSource != null);
            if (cdmSource != null)
            {
                FillList<CdmSource>(cdmSourceConcepts, cdmSource, cdmSource.CdmSource[0]);
            }
            Console.WriteLine("CdmSource was loaded");
            WriteLog(LogType.Info, "CdmSource was loaded", 0);
        }

        private void LoadMetadata(List<MetadataOMOP> metadataConcepts)
        {
            if (DBBuilder.IsConversionAborted(_connectionString, _conversionId))
                return;

            Console.WriteLine("Loading Metadata...");
            WriteLog(LogType.Info, "Loading Metadata...", 0);
            var metadata = _settings.SourceQueryDefinitions.FirstOrDefault(qd => qd.Metadata != null);
            if (metadata != null)
            {
                FillList<MetadataOMOP>(metadataConcepts, metadata, metadata.Metadata[0]);
            }
            Console.WriteLine("Metadata was loaded");
            WriteLog(LogType.Info, "Metadata was loaded", 0);
        }

        private void LoadLocationHistory(List<LocationHistory> locationHistoryConcepts)
        {
            if (DBBuilder.IsConversionAborted(_connectionString, _conversionId))
                return;

            Console.WriteLine("Loading LocationHistory...");
            WriteLog(LogType.Info, "Loading LocationHistory...", 0);
            var locationHistory = _settings.SourceQueryDefinitions.FirstOrDefault(qd => qd.LocationHistory != null);
            if (locationHistory != null)
            {
                FillList<LocationHistory>(locationHistoryConcepts, locationHistory, locationHistory.LocationHistory[0]);
            }
            Console.WriteLine("LocationHistory was loaded");
            WriteLog(LogType.Info, "LocationHistory was loaded", 0);
        }

        private void LoadCohort(List<Cohort> cohortConcepts)
        {
            if (DBBuilder.IsConversionAborted(_connectionString, _conversionId))
                return;

            Console.WriteLine("Loading Cohort...");
            WriteLog(LogType.Info, "Loading Cohort...", 0);
            var cohort = _settings.SourceQueryDefinitions.FirstOrDefault(qd => qd.Cohort != null);
            if (cohort != null)
            {
                FillList<Cohort>(cohortConcepts, cohort, cohort.Cohort[0]);
            }
            Console.WriteLine("Cohort was loaded");
            WriteLog(LogType.Info, "Cohort was loaded", 0);
        }

        private void LoadCohortDefinition(List<framework.common.Omop.CohortDefinition> cohortDefinitionConcepts)
        {
            if (DBBuilder.IsConversionAborted(_connectionString, _conversionId))
                return;

            Console.WriteLine("Loading CohortDefinition...");
            WriteLog(LogType.Info, "Loading CohortDefinition...", 0);
            var cohortDefinition = _settings.SourceQueryDefinitions.FirstOrDefault(qd => qd.CohortDefinition != null);
            if (cohortDefinition != null)
            {
                FillList<framework.common.Omop.CohortDefinition>(cohortDefinitionConcepts, cohortDefinition, cohortDefinition.CohortDefinition[0]);
            }
            Console.WriteLine("CohortDefinition was loaded");
            WriteLog(LogType.Info, "CohortDefinition was loaded", 0);
        }

        private void LoadCareSite(List<CareSite> careSiteConcepts)
        {
            if (DBBuilder.IsConversionAborted(_connectionString, _conversionId)) 
                return;

            Console.WriteLine("Loading care sites...");
            WriteLog(LogType.Info, "Loading care sites...", 0);

            var careSite = _settings.SourceQueryDefinitions.FirstOrDefault(qd => qd.CareSites != null);
            if (careSite != null)
            {
                FillList<CareSite>(careSiteConcepts, careSite, careSite.CareSites[0]);
            }

            if (careSiteConcepts.Count == 0)
                careSiteConcepts.Add(new CareSite { Id = 0, LocationId = 0, OrganizationId = 0, PlaceOfSvcSourceValue = null });
            Console.WriteLine("Care sites was loaded");
            WriteLog(LogType.Info, "Care sites was loaded", 0);
        }

        private void LoadLocation(List<Location> locationConcepts)
        {
            if (DBBuilder.IsConversionAborted(_connectionString, _conversionId)) 
                return;

            Console.WriteLine("Loading locations...");
            WriteLog(LogType.Info, "Loading locations...", 0);
            var location = _settings.SourceQueryDefinitions.FirstOrDefault(qd => qd.Locations != null);
            if (location != null)
            {
                FillList<Location>(locationConcepts, location, location.Locations[0]);
            }

            if (locationConcepts.Count == 0)
                locationConcepts.Add(new Location { Id = Entity.GetId(null) });
            Console.WriteLine("Locations was loaded");
            WriteLog(LogType.Info, "Locations was loaded", 0);
        }

        private void FillList<T>(ICollection<T> list, QueryDefinition qd, EntityDefinition ed) where T : IEntity
        {
            var sql = GetSqlHelper.GetSql(_settings.SourceEngine.Database,
                qd.GetSql("", _settings.ConversionSettings.SourceSchema), _settings.ConversionSettings.SourceSchema);

            sql = sql.Replace("{0}", "0"); // TODO: remove chunk join from CdmSource, Metadata, LocationHistory
            if (string.IsNullOrEmpty(sql)) 
                return;

            var keys = new Dictionary<string, bool>();
            using var connection = _settings.SourceEngine.GetConnection(_settings.SourceConnectionString);
            {
                using var c = _settings.SourceEngine.GetCommand(sql, connection);
                c.CommandTimeout = 30000;
                using var reader = c.ExecuteReader();
                while (reader.Read())
                {
                    Concept conceptDef = null;
                    if (ed.Concepts != null && ed.Concepts.Any())
                        conceptDef = ed.Concepts[0];

                    var concept = (T)ed.GetConcepts(conceptDef, reader, null).ToList()[0];

                    var key = concept.GetKey();
                    if (key == null) continue;

                    if (keys.ContainsKey(key))
                        continue;

                    keys.Add(key, false);

                    list.Add(concept);
                }
            }
        }
       
    }
}
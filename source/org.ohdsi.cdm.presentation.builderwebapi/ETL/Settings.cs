﻿using Microsoft.Extensions.Configuration;
using org.ohdsi.cdm.framework.common.Definitions;
using org.ohdsi.cdm.framework.common.Enums;
using org.ohdsi.cdm.framework.common.Extensions;
using org.ohdsi.cdm.framework.desktop.Databases;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;

namespace org.ohdsi.cdm.presentation.builderwebapi.ETL
{
    public class Settings
    {
        //private readonly IConfiguration _configuration;
        private readonly string _filesManagerUrl;
        private readonly Dictionary<string, string> _connectionStringTemplates;

        public CdmVersions Cdm
        {
            get
            {
                if (ConversionSettings.CdmVersion.ToLower().Contains("6.0"))
                    return CdmVersions.V6;

                if (ConversionSettings.CdmVersion.ToLower().Contains("5.4"))
                    return CdmVersions.V54;

                return CdmVersions.V53;
            }
        }

        public string GetCdmScriptsFolder
        {
            get
            {
                if (ConversionSettings.CdmVersion.ToLower().Contains("6.0"))
                    return "v6.0";

                if (ConversionSettings.CdmVersion.ToLower().Contains("5.4"))
                    return "v5.4";

                return "v5.3";
            }
        }

        public IDatabaseEngine SourceEngine
        {
            get
            {
                if (ConversionSettings.SourceEngine.ToLower() == "postgre" ||
                    ConversionSettings.SourceEngine.ToLower() == "postgresql" ||
                    ConversionSettings.SourceEngine.ToLower().Contains("postgre"))
                    return new PostgreDatabaseEngine();

                if (ConversionSettings.SourceEngine.ToLower() == "mysql")
                    return new MySqlDatabaseEngine();

                if (ConversionSettings.SourceEngine.ToLower().Contains("databricks"))
                    return new AzureDatabricksDatabaseEngine();

                return new MssqlDatabaseEngine();
            }
        }

        public IDatabaseEngine DestinationEngine
        {
            get
            {
                if (ConversionSettings.DestinationEngine.ToLower() == "postgre" ||
                    ConversionSettings.DestinationEngine.ToLower() == "postgresql" ||
                    ConversionSettings.DestinationEngine.ToLower().Contains("postgre"))
                    return new PostgreDatabaseEngine();

                if (ConversionSettings.DestinationEngine.ToLower() == "mysql")
                    return new MySqlDatabaseEngine();

                if (ConversionSettings.DestinationEngine.ToLower().Contains("databricks"))
                    return new AzureDatabricksDatabaseEngine();

                return new MssqlDatabaseEngine();
            }
        }

        public IDatabaseEngine VocabularyEngine
        {
            get
            {
                if (ConversionSettings.VocabularyEngine.ToLower() == "postgre" || 
                    ConversionSettings.VocabularyEngine.ToLower() == "postgresql" || 
                    ConversionSettings.VocabularyEngine.ToLower().Contains("postgre"))
                    return new PostgreDatabaseEngine();

                if (ConversionSettings.VocabularyEngine.ToLower() == "mysql")
                    return new MySqlDatabaseEngine();

                if (ConversionSettings.VocabularyEngine.ToLower().Contains("databricks"))
                    return new AzureDatabricksDatabaseEngine();

                return new MssqlDatabaseEngine();
            }
        }

        public string SourceConnectionString
        {
            get
            {
                return GetConnectionString(ConversionSettings.SourceEngine,
                                           ConversionSettings.SourceServer,
                                           ConversionSettings.SourceDatabase,
                                           ConversionSettings.SourceUser,
                                           ConversionSettings.SourcePassword,
                                           ConversionSettings.SourcePort.ToString(),
                                           ConversionSettings.SourceHttppath);
            }
        }

        public string DestinationConnectionString
        {
            get
            {
                return GetConnectionString(ConversionSettings.DestinationEngine,
                                           ConversionSettings.DestinationServer,
                                           ConversionSettings.DestinationDatabase,
                                           ConversionSettings.DestinationUser,
                                           ConversionSettings.DestinationPassword,
                                           ConversionSettings.DestinationPort.ToString(),
                                           ConversionSettings.DestinationHttppath);
            }
        }
        public string VocabularyConnectionString
        {
            get
            {
                return GetConnectionString(ConversionSettings.VocabularyEngine,
                                           ConversionSettings.VocabularyServer,
                                           ConversionSettings.VocabularyDatabase,
                                           ConversionSettings.VocabularyUser,
                                           ConversionSettings.VocabularyPassword,
                                           ConversionSettings.VocabularyPort.ToString(),
                                           "");
            }
        }

        public ConversionSettings ConversionSettings { get; private set; }
        public string Folder { get; private set; }
        public string BatchScript { get; set; }
        public Dictionary<string, string> Lookups;

        public List<QueryDefinition> SourceQueryDefinitions { get; private set; }

        public Settings(ConversionSettings settings, string filesManagerUrl, Dictionary<string, string> connectionStringTemplates)
        {
            Folder = AppContext.BaseDirectory;

            ConversionSettings = settings;
            Lookups = new Dictionary<string, string>();
            SourceQueryDefinitions = new List<QueryDefinition>();
            _filesManagerUrl = filesManagerUrl;

            _connectionStringTemplates = connectionStringTemplates;
        }

        public void Load()
        {
            var mappingsDir = Path.Combine(Folder, "mappings");
            var mappingsFile = Path.Combine(mappingsDir, ConversionSettings.MappingsName + ".zip");

            var actionUrl = _filesManagerUrl;
            actionUrl += $"/{ConversionSettings.ContentKey}";
            using var client = new HttpClient();
            var data = client.GetByteArrayAsync(actionUrl);
            data.Wait();
            using MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(data.Result, 0, data.Result.Length);

            //using ZipArchive archive = ZipFile.OpenRead(mappingsFile);
            using ZipArchive archive = new ZipArchive(memoryStream);
            foreach (var item in archive.Entries)
            {
                using var stream = item.Open();
                using StreamReader reader = new StreamReader(stream);

                var content = reader.ReadToEnd();
                if (item.FullName.StartsWith("Definitions", StringComparison.OrdinalIgnoreCase))
                {
                    // TMP
                    content = content.Replace("<Person>", "<Persons>");
                    content = content.Replace("</Person>", "</Persons>");

                    if (item.Name.Replace(".xml", "").ToLower() == "l_location")
                    {
                        content = content.Replace("<Location>", "<Locations>");
                        content = content.Replace("</Location>", "</Locations>");

                        content = content.Replace("<LocationId>", "<Id>");
                        content = content.Replace("</LocationId>", "</Id>");
                    }

                    if (item.Name.Replace(".xml", "").ToLower() == "l_care_site")
                    {
                        content = content.Replace("<CareSite>", "<CareSites>");
                        content = content.Replace("</CareSite>", "</CareSites>");

                        content = content.Replace("<CareSiteId>", "<Id>");
                        content = content.Replace("</CareSiteId>", "</Id>");
                    }

                    if (item.Name.Replace(".xml", "").ToLower() == "l_provider")
                    {
                        content = content.Replace("<Provider>", "<Providers>");
                        content = content.Replace("</Provider>", "</Providers>");

                        content = content.Replace("<ProviderId>", "<Id>");
                        content = content.Replace("</ProviderId>", "</Id>");
                    }

                    content = content.Replace("<VisitStartDate>", "<StartDate>");
                    content = content.Replace("</VisitStartDate>", "</StartDate>");

                    content = content.Replace("<VisitEndDate>", "<EndDate>");
                    content = content.Replace("</VisitEndDate>", "</EndDate>");


                    content = content.Replace("<ObservationPeriodStartDate>", "<StartDate>");
                    content = content.Replace("</ObservationPeriodStartDate>", "</StartDate>");

                    content = content.Replace("<ObservationPeriodEndDate>", "<EndDate>");
                    content = content.Replace("</ObservationPeriodEndDate>", "</EndDate>");

                    content = content.Replace("<ConceptIdMapper>", "<ConceptIdMappers>");
                    content = content.Replace("</ConceptIdMapper>", "</ConceptIdMappers>");

                    content = content.Replace("<Refills>", "<Refill>");
                    content = content.Replace("</Refills>", "</Refill>");

                    var qd = new QueryDefinition().DeserializeFromXml(content);

                    if (qd.Persons != null)
                    {
                        if (qd.Persons[0].Concepts != null && qd.Persons[0].Concepts.Length > 0)
                        {
                            var gender = qd.Persons[0].Concepts.FirstOrDefault(c => c.Name == "GenderConceptId");
                            if (gender != null)
                                qd.Persons[0].Gender = gender.Fields[0].SourceKey;

                            var race = qd.Persons[0].Concepts.FirstOrDefault(c => c.Name == "RaceConceptId");
                            if (race != null)
                            {
                                qd.Persons[0].Race = race.Fields[0].SourceKey;
                                qd.Persons[0].RaceConceptId = race.Fields[0].ConceptId;
                            }

                            var ethnicity = qd.Persons[0].Concepts.FirstOrDefault(c => c.Name == "EthnicityConceptId");
                            if (ethnicity != null)
                            {
                                qd.Persons[0].Ethnicity = ethnicity.Fields[0].SourceKey;
                                qd.Persons[0].EthnicityConceptId = ethnicity.Fields[0].ConceptId;
                            }
                        }

                        if (string.IsNullOrEmpty(qd.Persons[0].PersonSourceValue))
                            qd.Persons[0].PersonSourceValue = qd.Persons[0].PersonId;
                    }

                    if (qd.VisitOccurrence != null)
                    {
                        foreach (var vo in qd.VisitOccurrence)
                        {
                            if (!string.IsNullOrEmpty(vo.VisitOccurrenceId) && string.IsNullOrEmpty(vo.Id))
                                vo.Id = vo.VisitOccurrenceId;
                        }
                    }

                    qd.FileName = item.Name.Replace(".xml", "");
                    SourceQueryDefinitions.Add(qd);
                }
                else if (item.FullName.StartsWith("Lookups", StringComparison.OrdinalIgnoreCase))
                {
                    Lookups.Add(item.Name.Replace(".sql", ""), content);
                }
                else if (item.FullName.StartsWith("Batch.sql", StringComparison.OrdinalIgnoreCase))
                {
                    BatchScript = content;
                }
            }
        }

        public string DropVocabularyTablesScript => File.ReadAllText(
            Path.Combine(Folder, "ETL", "Common", "Scripts", ConversionSettings.DestinationEngine, GetCdmScriptsFolder, "DropVocabularyTables.sql"));

        public string TruncateWithoutLookupTablesScript => File.ReadAllText(
            Path.Combine(Folder, "ETL", "Common", "Scripts", ConversionSettings.DestinationEngine, GetCdmScriptsFolder, "TruncateWithoutLookupTables.sql"));

        public string TruncateTablesScript => File.ReadAllText(
            Path.Combine(Folder, "ETL", "Common", "Scripts", ConversionSettings.DestinationEngine, GetCdmScriptsFolder, "TruncateTables.sql"));

        public string DropTablesScript => File.ReadAllText(
            Path.Combine(Folder, "ETL", "Common", "Scripts", ConversionSettings.DestinationEngine, GetCdmScriptsFolder, "DropTables.sql"));

        public string TruncateLookupScript => File.ReadAllText(
            Path.Combine(Folder, "ETL", "Common", "Scripts", ConversionSettings.DestinationEngine, GetCdmScriptsFolder, "TruncateLookup.sql"));

        public string CreateCdmTablesScript => File.ReadAllText(
            Path.Combine(Folder, "ETL", "Common", "Scripts", ConversionSettings.DestinationEngine,
                         GetCdmScriptsFolder, "CreateTables.sql"));

        public string CreateCdmDatabaseScript => File.ReadAllText(
            Path.Combine(new[] {
                Folder,
                "ETL",
                "Common",
                "Scripts",
                ConversionSettings.DestinationEngine,
                "CreateDestination.sql"
            }));

        private string GetConnectionString(string dbType, string server, string db, string user, string pswd, string port, string httppath)
        {
            return _connectionStringTemplates[dbType].Replace("{server}", server).Replace("{database}", db).Replace("{username}", user)
                .Replace("{password}", pswd).Replace("{port}", port).Replace("{httppath}", httppath);
        }
    }
}
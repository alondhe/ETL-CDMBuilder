using Newtonsoft.Json;
using org.ohdsi.cdm.framework.common.Base;
using org.ohdsi.cdm.framework.common.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace org.ohdsi.cdm.presentation.builderwebapi.ETL
{
    public class ConversionSettings
    {
        public object ConversionId { get; set; }
        public string ContentKey { get; set; }
        public string SourceEngine { get; set; }
        public string DestinationEngine { get; set; }
        public string VocabularyEngine { get; set; }

        public string SourceServer { get; set; }
        public string DestinationServer { get; set; }
        public string VocabularyServer { get; set; }

        public string SourceSchema { get; set; }
        public string DestinationSchema { get; set; }
        public string VocabularySchema { get; set; }

        public string SourceDatabase { get; set; }
        public string DestinationDatabase { get; set; }
        public string VocabularyDatabase { get; set; }

        public string SourceUser { get; set; }
        public string DestinationUser { get; set; }
        public string VocabularyUser { get; set; }

        public string SourcePassword { get; set; }
        public string DestinationPassword { get; set; }
        public string VocabularyPassword { get; set; }

        public object SourcePort { get; set; }
        public object DestinationPort { get; set; }
        public object VocabularyPort { get; set; }

        public string SourceHttppath { get; set; }
        public string DestinationHttppath { get; set; }

        public string MappingsName { get; set; }
        public string CdmVersion { get; set; } = "v5.3";

        public BuildSettings BuildSettings { get; set; } = new BuildSettings();

        public List<TableConfig> TableSettings { get; set; }

        public void PopulateBuildSettings()
        {
            if (TableSettings == null)
                return;
            BuildSettings.Eras = new List<EraSetting>();
            BuildSettings.Tables = new List<TableSetting>();
            foreach (var tc in TableSettings)
            {
                switch (tc.TableName)
                {
                    case "person":
                        BuildSettings.AllowGenderChanges = tc.Settings.AllowGenderChanges ?? false;
                        BuildSettings.AllowMultipleYearsOfBirth = tc.Settings.AllowMultipleYearsOfBirth ?? false;
                        BuildSettings.AllowUnknownGender = tc.Settings.AllowUnknownGender ?? false;
                        BuildSettings.AllowUnknownYearOfBirth = tc.Settings.AllowUnknownYearOfBirth ?? false;
                        BuildSettings.AllowInvalidObservationTime = tc.Settings.AllowUnknownYearOfBirth ?? false;

                        if(tc.Settings.ImplausibleYearOfBirth != null)
                        {
                            BuildSettings.ImplausibleYearOfBirthAfter = tc.Settings.ImplausibleYearOfBirth.AfterYear;
                            BuildSettings.ImplausibleYearOfBirthBefore = tc.Settings.ImplausibleYearOfBirth.BeforeYear;
                        }
                        break;
                    case "observation_period":
                        BuildSettings.Eras.Add(new EraSetting
                        {
                            Table = framework.common.Enums.EntityType.ObservationPeriod,
                            GapWindow = tc.Settings.GapWindow
                        });
                        break;
                    case "condition_occurrence":
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.ConditionOccurrence,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "visit_occurrence":
                        BuildSettings.UseVisitRollupLogic = tc.Settings.UseVisitConceptRollupLogic ?? false;
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.VisitOccurrence,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "device_exposure":
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.DeviceExposure,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "dose_era":
                        break;
                    case "drug_era":
                        BuildSettings.Eras.Add(new EraSetting
                        {
                            Table = framework.common.Enums.EntityType.DrugEra,
                            GapWindow = tc.Settings.GapWindow,
                            ConceptId = tc.Settings.ConceptId
                        });
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.DrugEra,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "condition_era":
                        BuildSettings.Eras.Add(new EraSetting
                        {
                            Table = framework.common.Enums.EntityType.ConditionEra,
                            GapWindow = tc.Settings.GapWindow,
                            ConceptId = tc.Settings.ConceptId
                        });
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.ConditionEra,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "drug_exposure":
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.DrugExposure,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "measurement":
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.Measurement,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "note":
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.Note,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "note_nlp":
                        break;
                    case "observation":
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.Observation,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "payer_plan_period":
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.PayerPlanPeriod,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "procedure_occurrence":
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.ProcedureOccurrence,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "specimen":
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.Specimen,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "survey_conduct":
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.SurveyConduct,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                    case "visit_detail":
                        BuildSettings.Tables.Add(new TableSetting
                        {
                            Table = framework.common.Enums.EntityType.VisitDetail,
                            WithinTheObservationPeriod = tc.Settings.WithinTheObservationPeriod
                        });
                        break;
                }
            }
        }

        public static IEnumerable<Tuple<string, string>> GetProperties(object atype, string parentName)
        {
            if (atype == null)
                return new List<Tuple<string, string>>();

            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            var result = new List<Tuple<string, string>>();
            foreach (PropertyInfo prp in props)
            {
                if (prp.Name == "TableSettings")
                    continue;

                var currentName = parentName != null ? parentName + "." + prp.Name : prp.Name;
                if (!prp.PropertyType.IsEnum && prp.PropertyType.Namespace != "System")
                {
                    object bs = prp.GetValue(atype, new object[] { });
                    if (prp.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                    {
                        var index = 0;
                        foreach (var item in (IEnumerable)bs)
                        {
                            result.Add(new Tuple<string, string>(
                                parentName + "." + prp.Name + index,
                                string.Join(';', GetProperties(item, null).Select(p => p.Item1 + ":" + p.Item2)))
                                );
                            index++;
                        }
                        continue;
                    }

                    
                    foreach (var p in GetProperties(bs, prp.Name))
                    {
                        result.Add(new Tuple<string, string>(p.Item1, p.Item2));
                    }
                    continue;
                }

                object value = prp.GetValue(atype, new object[] { });
                if (value == null)
                    continue;

                result.Add(new Tuple<string, string>(currentName, value.ToString()));
            }
            return result;
        }

        public static ConversionSettings SetProperties(IEnumerable<Tuple<string, string>> properties)
        {
            var baseSettings = new Dictionary<string, string>();
            var buildSettings = new Dictionary<string, string>();
            var erasSettings = new List<string>();
            var tablesSettings = new List<string>();

            foreach (var p in properties)
            {
                if (p.Item1.StartsWith("BuildSettings.Tables"))
                {
                    tablesSettings.Add(p.Item2);
                }
                else if (p.Item1.StartsWith("BuildSettings.Eras"))
                {
                    erasSettings.Add(p.Item2);
                }
                else if (p.Item1.StartsWith("BuildSettings"))
                {
                    buildSettings.Add(p.Item1.Replace("BuildSettings.", ""), p.Item2);
                }
                else
                {
                    baseSettings.Add(p.Item1, p.Item2);
                }
            }

            var cs = new ConversionSettings();
            FillProperties(baseSettings, cs);

            var bs = new BuildSettings();
            FillProperties(buildSettings, bs);
            cs.BuildSettings = bs;
            cs.BuildSettings.Eras = new List<EraSetting>();
            cs.BuildSettings.Tables = new List<TableSetting>();
            foreach (var ts in erasSettings)
            {
                var e = new EraSetting();
                foreach (var value in ts.Split(';'))
                {
                    if(value.StartsWith("Table:"))
                    {
                        e.Table = (EntityType)Enum.Parse(typeof(EntityType), value.Replace("Table:", ""));
                    } 
                    else if (value.StartsWith("GapWindow:"))
                    {
                        e.GapWindow = int.Parse(value.Replace("GapWindow:", ""));
                    }
                    else if (value.StartsWith("ConceptId:"))
                    {
                        e.ConceptId = int.Parse(value.Replace("ConceptId:", ""));
                    }
                }
                cs.BuildSettings.Eras.Add(e);
            }

            foreach (var ts in tablesSettings)
            {
                var t = new TableSetting();
                foreach (var value in ts.Split(';'))
                {
                    if (value.StartsWith("Table:"))
                    {
                        t.Table = (EntityType)Enum.Parse(typeof(EntityType), value.Replace("Table:", ""));
                    }
                    else if (value.StartsWith("WithinTheObservationPeriod:"))
                    {
                        t.WithinTheObservationPeriod = bool.Parse(value.Replace("WithinTheObservationPeriod:", ""));
                    }
                }
                cs.BuildSettings.Tables.Add(t);
            }

            return cs;
        }

        private static void FillProperties(Dictionary<string, string> settings, object result)
        {
            Type t = result.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo prp in props)
            {
                if (prp.PropertyType.Namespace != "System")
                    continue;

                if (settings.ContainsKey(prp.Name))
                {
                    if (prp.PropertyType.Name == "Boolean")
                        prp.SetValue(result, bool.Parse(settings[prp.Name]));
                    else if (prp.PropertyType.Name == "Int32")
                        prp.SetValue(result, int.Parse(settings[prp.Name]));
                    else
                        prp.SetValue(result, settings[prp.Name]);
                }
            }
        }
    }

    public class TableConfig
    {
        //[JsonProperty("tableName")]
        public string TableName { get; set; }

        //[JsonProperty("settings")]
        public TransformationConfig Settings { get; set; }
    }

    public class TransformationConfig
    {
        //[JsonProperty("conceptId")]
        public int ConceptId { get; set; }

        //[JsonProperty("gapWindow")]
        public int GapWindow { get; set; }

        //[JsonProperty("withinTheObservationPeriod")]
        public bool WithinTheObservationPeriod { get; set; }

        //[JsonProperty("allowGenderChanges")]
        public bool? AllowGenderChanges { get; set; }

        //[JsonProperty("allowInvalidObservationTime")]
        public bool? AllowInvalidObservationTime { get; set; }

        //[JsonProperty("allowMultipleYearsOfBirth")]
        public bool? AllowMultipleYearsOfBirth { get; set; }

        //[JsonProperty("allowUnknownGender")]
        public bool? AllowUnknownGender { get; set; }

        //[JsonProperty("allowUnknownYearOfBirth")]
        public bool? AllowUnknownYearOfBirth { get; set; }

        //[JsonProperty("implausibleYearOfBirth")]
        public ImplausibleYearOfBirth ImplausibleYearOfBirth { get; set; }

        //[JsonProperty("useVisitConceptRollupLogic")]
        public bool? UseVisitConceptRollupLogic { get; set; }
    }

    public class ImplausibleYearOfBirth
    {
        //[JsonProperty("afterYear")]
        public int AfterYear { get; set; }

        //[JsonProperty("beforeYear")]
        public int BeforeYear { get; set; }
    }
}

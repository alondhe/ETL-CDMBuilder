using org.ohdsi.cdm.framework.common.Builder;
using org.ohdsi.cdm.framework.common.Extensions;
using org.ohdsi.cdm.framework.common.Omop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace org.ohdsi.cdm.framework.common.Definitions
{
    public class SurveyConductDefinition : EntityDefinition
    {
        public string SurveySourceIdentifier { get; set; }
        public string SurveyVersionNumber { get; set; }
        public string ResponseVisitOccurrenceId { get; set; }

        public override IEnumerable<IEntity> GetConcepts(Concept concept, IDataRecord reader,
            KeyMasterOffsetManager offset)
        {
            var concepts = GetConcepts(reader);

            foreach (var entity in base.GetConcepts(concept, reader, offset))
            {
                yield return new SurveyConduct(entity)
                {
                    Id = offset.GetKeyOffset(entity.PersonId).SurveyConductId,
                    SurveySourceIdentifier = reader.GetString(SurveySourceIdentifier),
                    SurveyVersionNumber = reader.GetString(SurveyVersionNumber),
                    ResponseVisitOccurrenceId = reader.GetLong(ResponseVisitOccurrenceId),

                    AssistedConceptId = concepts["AssistedConceptId"].ConceptId,
                    AssistedSourceValue = concepts["AssistedConceptId"].SourceValue,

                    RespondentTypeConceptId = concepts["RespondentConceptId"].TypeConceptId,
                    RespondentTypeSourceValue = concepts["Respondent_typeConceptId"].SourceValue,

                    TimingConceptId = concepts["TimingConceptId"].ConceptId,
                    TimingSourceValue = concepts["TimingConceptId"].SourceValue,

                    CollectionMethodConceptId = concepts["Collection_methodConceptId"].ConceptId,
                    CollectionMethodSourceValue = concepts["Collection_methodConceptId"].SourceValue,

                    ValidatedSurveyConceptId = concepts["Validated_surveyConceptId"].ConceptId,
                    ValidatedSurveySourceValue = concepts["Validated_surveyConceptId"].SourceValue
                };
            }
        }

        private Dictionary<string, IEntity> GetConcepts(IDataRecord reader)
        {
            var result = new Dictionary<string, IEntity>();
            var names = new[] { "AssistedConceptId", "RespondentConceptId", "TimingConceptId", "Collection_methodConceptId", "Respondent_typeConceptId", "Validated_surveyConceptId" };
            foreach (var name in names)
            {
                result.Add(name, new Entity());
            }

            if (Concepts != null)
            {
                foreach (var name in names)
                {
                    var concept = Concepts.FirstOrDefault(c => c.Name == name);
                    if (concept != null)
                    {
                        var concepts = base.GetConcepts(concept, reader, null).ToList();
                        if (concepts.Count > 0)
                        {
                            result[name] = concepts[0];
                        }
                    }
                }
            }

            return result;
        }
    }
}
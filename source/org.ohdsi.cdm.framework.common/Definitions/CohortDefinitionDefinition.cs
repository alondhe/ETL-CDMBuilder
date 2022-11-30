using org.ohdsi.cdm.framework.common.Builder;
using org.ohdsi.cdm.framework.common.Extensions;
using org.ohdsi.cdm.framework.common.Omop;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace org.ohdsi.cdm.framework.common.Definitions
{
    public class CohortDefinitionDefinition : EntityDefinition
    {
        public string CohortDefinitionName { get; set; }
        public string CohortDefinitionDescription { get; set; }
        public string CohortDefinitionSyntax { get; set; }
        public string CohortInitiationDate { get; set; }

        public override IEnumerable<IEntity> GetConcepts(Concept concept, IDataRecord reader, KeyMasterOffsetManager offset)
        {
            var startDate = reader.GetDateTime(CohortInitiationDate);

            Entity e;
            if (concept != null)
            {
                var definitionConcept = Concepts.FirstOrDefault(c => c.Name == "DefinitionConceptId");
                int definitionTypeConceptId = 0;
                int subjectConceptId = 0;
                if (definitionConcept != null)
                {
                    var definitionConcepts = GetConcepts(definitionConcept, reader).ToList();
                    if (definitionConcepts.Count > 0 && definitionConcepts[0].TypeConceptId.HasValue)
                    {
                        definitionTypeConceptId = definitionConcepts[0].TypeConceptId.Value;
                    }
                }

                var subjectConcept = Concepts.FirstOrDefault(c => c.Name == "SubjectConceptId");
                if (subjectConcept != null)
                {
                    var subjectConcepts = GetConcepts(subjectConcept, reader).ToList();
                    if (subjectConcepts.Count > 0)
                    {
                        subjectConceptId = subjectConcepts[0].ConceptId;
                    }
                }
                e = new Entity
                {
                    IsUnique = false,
                    SourceValue = " ",
                    ConceptId = subjectConceptId,
                    TypeConceptId = definitionTypeConceptId,
                    StartDate = startDate
                };
            }
            else
            {
                e = new Entity
                {
                    IsUnique = false,
                    StartDate = startDate
                };
            }

            var cohort = new Omop.CohortDefinition(e)
            {
                Syntax = reader.GetString(CohortDefinitionSyntax),
                Name = reader.GetString(CohortDefinitionName),
                Description = reader.GetString(CohortDefinitionDescription)
            };

            cohort.Id = string.IsNullOrEmpty(Id) ? Entity.GetId(cohort.GetKey()) : reader.GetLong(Id).Value;

            yield return cohort;
        }

        private IEnumerable<IEntity> GetConcepts(Concept concept, IDataRecord reader)
        {
            if (concept != null && concept.Fields != null)
            {
                foreach (var field in concept.Fields)
                {
                    var sourceValue = field.DefaultSource;
                    if (string.IsNullOrEmpty(sourceValue))
                        sourceValue = reader.GetString(field.Key);

                    if (!string.IsNullOrEmpty(field.SourceKey))
                        sourceValue = reader.GetString(field.SourceKey);

                    foreach (var lookupValue in concept.GetConceptIdValues(Vocabulary, field, reader))
                    {
                        var cId = lookupValue.ConceptId;
                        if (!cId.HasValue && field.DefaultConceptId.HasValue)
                            cId = field.DefaultConceptId;

                        if (!concept.IdRequired || cId.HasValue)
                        {

                            yield return new Entity
                            {
                                SourceValue = sourceValue,
                                ConceptId = cId ?? 0,
                                TypeConceptId = concept.GetTypeId(field, reader) ?? 0
                            };
                        }
                    }
                }
            }
        }
    }
}
using org.ohdsi.cdm.framework.common.Builder;
using org.ohdsi.cdm.framework.common.Extensions;
using org.ohdsi.cdm.framework.common.Omop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace org.ohdsi.cdm.framework.common.Definitions
{
    public class SpecimenDefinition : EntityDefinition
    {
        public string Quantity { get; set; }
        public string SpecimenSourceId { get; set; }

        public KeyValuePair<int?, string> GetUnitConcept(IDataRecord reader, string conceptName)
        {
            var sourceValue = string.Empty;

            var concept = Concepts.FirstOrDefault(c => c.Name == conceptName);
            if (concept != null)
            {
                var concepts = base.GetConcepts(concept, reader, null).Where(c => c.ConceptId != 0).ToList();
                sourceValue = reader.GetString(concept.Fields[0].Key);

                if (concepts.Count > 0)
                {
                    foreach (var uc in concepts)
                    {
                        if (!string.IsNullOrEmpty(sourceValue) && !string.IsNullOrEmpty(uc.VocabularySourceValue) &&
                            sourceValue.Equals(uc.VocabularySourceValue, StringComparison.Ordinal))
                            return new KeyValuePair<int?, string>(uc.ConceptId, sourceValue);
                    }

                    return new KeyValuePair<int?, string>(concepts[0].ConceptId, concepts[0].SourceValue);

                }
            }

            return new KeyValuePair<int?, string>(null, sourceValue);
        }

        public override IEnumerable<IEntity> GetConcepts(Concept concept, IDataRecord reader, KeyMasterOffsetManager offset)
        {
            KeyValuePair<int?, string> unitConcept = new KeyValuePair<int?, string>(null, null);
            KeyValuePair<int?, string> anatomicConcept = new KeyValuePair<int?, string>(null, null);
            KeyValuePair<int?, string> diseaseConcept = new KeyValuePair<int?, string>(null, null);
            if (Concepts != null)
            {
                unitConcept = GetUnitConcept(reader, "UnitConceptId");
                anatomicConcept = GetUnitConcept(reader, "Anatomic_siteConceptId");
                diseaseConcept = GetUnitConcept(reader, "Disease_statusConceptId");
            }

            var quantity = reader.GetDecimal(Quantity);
            int? q = null;

            if (quantity.HasValue)
                q = System.Decimal.ToInt32(quantity.Value);

            foreach (var e in base.GetConcepts(concept, reader, offset))
            {
                yield return
                   new Specimen(e)
                   {
                       Id = offset.GetKeyOffset(e.PersonId).SpecimenId,
                       Quantity = q,
                       UnitConceptId = unitConcept.Key ?? 0,
                       UnitSourceValue = string.IsNullOrWhiteSpace(unitConcept.Value) ? null : unitConcept.Value,
                       AnatomicSiteConceptId = anatomicConcept.Key ?? 0,
                       AnatomicSiteSourceValue = string.IsNullOrWhiteSpace(anatomicConcept.Value) ? null : anatomicConcept.Value,
                       DiseaseConceptId = diseaseConcept.Key ?? 0,
                       DiseaseSourceValue = string.IsNullOrWhiteSpace(diseaseConcept.Value) ? null : diseaseConcept.Value,
                       SpecimenSourceId = reader.GetString(SpecimenSourceId)
                   };
            }
        }
    }
}

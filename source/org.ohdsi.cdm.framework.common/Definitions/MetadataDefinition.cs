using org.ohdsi.cdm.framework.common.Builder;
using org.ohdsi.cdm.framework.common.Extensions;
using org.ohdsi.cdm.framework.common.Omop;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace org.ohdsi.cdm.framework.common.Definitions
{
    public class MetadataDefinition : EntityDefinition
    {
        public string Name { get; set; }
        public string ValueAsString { get; set; }
        public string MetadataDatetime { get; set; }
        public string MetadataDate { get; set; }
        public string ValueAsNumber { get; set; }

        public override IEnumerable<IEntity> GetConcepts(Concept concept, IDataRecord reader,
           KeyMasterOffsetManager keyOffset)
        {
            int metadataConceptId = 0;
            int metadataTypeConceptId = 0;
            if (Concepts != null)
            {
                var metadataConcept = Concepts.FirstOrDefault(c => c.Name == "MetadataConceptId");
                if (metadataConcept != null)
                {
                    var metadataConcepts = base.GetConcepts(metadataConcept, reader, null).ToList();
                    if (metadataConcepts.Count > 0)
                    {
                        metadataConceptId = metadataConcepts[0].ConceptId;
                        metadataTypeConceptId = metadataConcepts[0].TypeConceptId ?? 0;
                    }
                }
            }

            // todo value_as_concept_id
            var id = string.IsNullOrEmpty(Id) ? 0 : reader.GetLong(Id);
            yield return new MetadataOMOP()
            {
                Id = id.Value,
                Name = reader.GetString(Name),
                ValueAsString = reader.GetString(ValueAsString),
                MetadataDatetime = reader.GetString(MetadataDatetime),
                MetadataDate = reader.GetDateTime(MetadataDate),
                MetadataConceptId = metadataConceptId,
                MetadataTypeConceptId = metadataTypeConceptId,
                ValueAsNumber = reader.GetDecimal(ValueAsNumber) ?? 0
            };

        }
    }
}
using org.ohdsi.cdm.framework.common.Builder;
using org.ohdsi.cdm.framework.common.Extensions;
using org.ohdsi.cdm.framework.common.Omop;
using System.Collections.Generic;
using System.Data;

namespace org.ohdsi.cdm.framework.common.Definitions
{
    public class FactRelationshipDefinition : EntityDefinition
    {
        public string DomainConceptId1 { get; set; }
        public string DomainConceptId2 { get; set; }
        public string FactId1 { get; set; }
        public string FactId2 { get; set; }

        public override IEnumerable<IEntity> GetConcepts(Concept concept, IDataRecord reader, KeyMasterOffsetManager offset)
        {
            var fr = new FactRelationship
            {
                DomainConceptId1 = reader.GetLong(DomainConceptId1).Value,
                DomainConceptId2 = reader.GetLong(DomainConceptId2).Value,
                FactId1 = reader.GetLong(FactId1).Value,
                FactId2 = reader.GetLong(FactId2).Value
            };

            if (concept != null && concept.Fields != null && concept.Fields.Length > 0)
            {
                fr.RelationshipConceptId = concept.Fields[0].DefaultConceptId.Value;
            }

            yield return fr;
        }
    }
}

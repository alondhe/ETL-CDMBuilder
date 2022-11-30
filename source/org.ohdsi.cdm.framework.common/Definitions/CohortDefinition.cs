using org.ohdsi.cdm.framework.common.Builder;
using org.ohdsi.cdm.framework.common.Extensions;
using org.ohdsi.cdm.framework.common.Omop;
using System.Collections.Generic;
using System.Data;

namespace org.ohdsi.cdm.framework.common.Definitions
{
    public class CohortDefinition : EntityDefinition
    {
        public string SubjectId { get; set; }
        public string CohortStartDate { get; set; }
        public string CohortEndDate { get; set; }

        public override IEnumerable<IEntity> GetConcepts(Concept concept, IDataRecord reader, KeyMasterOffsetManager offset)
        {
            var subjectId = reader.GetLong(SubjectId);
            var startDate = reader.GetDateTime(CohortStartDate);
            var endDate = reader.GetDateTime(CohortEndDate);
      
            var cohort = new Cohort(new Entity())
            {
                PersonId = subjectId.Value,
                StartDate = startDate,
                EndDate = endDate
            };

            cohort.Id = string.IsNullOrEmpty(Id) ? Entity.GetId(cohort.GetKey()) : reader.GetLong(Id).Value;

            yield return cohort;
        }
    }
}

using org.ohdsi.cdm.framework.common.Enums;
using System;

namespace org.ohdsi.cdm.framework.common.Omop
{
    public class CohortDefinition : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Syntax { get; set; }
        public int? SubjectConceptId { get; set; }

        public CohortDefinition(Entity ent)
        {
            Init(ent);

            var cohort = ent as CohortDefinition;
            if (cohort != null)
            {
                Name = cohort.Name;
                Description = cohort.Description;
                Syntax = cohort.Syntax;
                SubjectConceptId = cohort.SubjectConceptId;
            }
        }

        public override string GetKey()
        {
            return $"{Name};{StartDate};{SubjectConceptId}";
        }
    } 
}

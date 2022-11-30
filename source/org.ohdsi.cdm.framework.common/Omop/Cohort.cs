using org.ohdsi.cdm.framework.common.Enums;
using System;

namespace org.ohdsi.cdm.framework.common.Omop
{
    public class Cohort : Entity
    {

        public Cohort(Entity ent)
        {
            Init(ent);
        }

        public override string GetKey()
        {
            return $"{PersonId};{StartDate};{EndDate}";
        }
    }
}

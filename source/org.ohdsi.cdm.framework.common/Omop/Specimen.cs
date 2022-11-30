using org.ohdsi.cdm.framework.common.Enums;
using System;
using System.Collections.Generic;

namespace org.ohdsi.cdm.framework.common.Omop
{
    public class Specimen : Entity, IEquatable<Specimen>
    {
        public decimal? Quantity { get; set; }
        public int UnitConceptId { get; set; }
        public string UnitSourceValue { get; set; }
        public int AnatomicSiteConceptId { get; set; }
        public string AnatomicSiteSourceValue { get; set; }

        public int DiseaseConceptId { get; set; }
        public string DiseaseSourceValue { get; set; }

        public string SpecimenSourceId { get; set; }

        public Specimen(IEntity ent)
        {
            Init(ent);

            var specimen = ent as Specimen;
            if (specimen != null)
            {
                Quantity = specimen.Quantity;
                UnitConceptId = specimen.UnitConceptId;
                UnitSourceValue = specimen.UnitSourceValue;
                AnatomicSiteConceptId = specimen.AnatomicSiteConceptId;
                AnatomicSiteSourceValue = specimen.AnatomicSiteSourceValue;
                DiseaseConceptId = specimen.DiseaseConceptId;
                DiseaseSourceValue = specimen.DiseaseSourceValue;
                SpecimenSourceId = specimen.SpecimenSourceId;
            }
        }

        public bool Equals(Specimen other)
        {
            return this.PersonId.Equals(other.PersonId) &&
                   this.ConceptId == other.ConceptId &&
                   this.StartDate == other.StartDate &&
                   this.TypeConceptId == other.TypeConceptId &&
                   this.Quantity == other.Quantity &&
                   this.UnitConceptId == other.UnitConceptId &&
                   this.UnitSourceValue == other.UnitSourceValue &&
                   this.AnatomicSiteConceptId == other.AnatomicSiteConceptId &&
                   this.AnatomicSiteSourceValue == other.AnatomicSiteSourceValue &&
                   this.DiseaseConceptId == other.DiseaseConceptId &&
                   this.DiseaseSourceValue == other.DiseaseSourceValue &&
                   this.SpecimenSourceId == other.SpecimenSourceId &&
                   this.SourceValue == other.SourceValue;
        }

        public override int GetHashCode()
        {
            return PersonId.GetHashCode() ^
                   ConceptId.GetHashCode() ^
                   (StartDate.GetHashCode()) ^
                   (Quantity.GetHashCode()) ^
                   (UnitConceptId.GetHashCode()) ^
                   (UnitSourceValue.GetHashCode()) ^
                   (AnatomicSiteConceptId.GetHashCode()) ^
                   (AnatomicSiteSourceValue.GetHashCode()) ^
                   (DiseaseConceptId.GetHashCode()) ^
                   (DiseaseSourceValue.GetHashCode()) ^
                   (SpecimenSourceId.GetHashCode()) ^
                   TypeConceptId.GetHashCode();
        }

        public override EntityType GeEntityType()
        {
            return EntityType.Specimen;
        }
    }
}
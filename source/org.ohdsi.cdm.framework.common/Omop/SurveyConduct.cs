using org.ohdsi.cdm.framework.common.Enums;
using System;

namespace org.ohdsi.cdm.framework.common.Omop
{
    public class SurveyConduct : Entity, IEquatable<SurveyConduct>
    {
        public long? AssistedConceptId { get; set; }
        public string AssistedSourceValue { get; set; }

        public long? RespondentTypeConceptId { get; set; }
        public string RespondentTypeSourceValue { get; set; }

        public long? TimingConceptId { get; set; }
        public string TimingSourceValue { get; set; }

        public long? CollectionMethodConceptId { get; set; }
        public string CollectionMethodSourceValue { get; set; }

        public long? ValidatedSurveyConceptId { get; set; }
        public string ValidatedSurveySourceValue { get; set; }

        public string SurveySourceIdentifier { get; set; }
        public string SurveyVersionNumber { get; set; }
        public long? ResponseVisitOccurrenceId { get; set; }

        public SurveyConduct(IEntity ent)
        {
            Init(ent);

            var surveyConduct = ent as SurveyConduct;
            if (surveyConduct != null)
            {
                AssistedConceptId = surveyConduct.AssistedConceptId;
                RespondentTypeConceptId = surveyConduct.RespondentTypeConceptId;
                TimingConceptId = surveyConduct.TimingConceptId;
                CollectionMethodConceptId = surveyConduct.CollectionMethodConceptId;
                AssistedSourceValue = surveyConduct.AssistedSourceValue;
                RespondentTypeSourceValue = surveyConduct.RespondentTypeSourceValue;
                TimingSourceValue = surveyConduct.TimingSourceValue;
                CollectionMethodSourceValue = surveyConduct.CollectionMethodSourceValue;
                SurveySourceIdentifier = surveyConduct.SurveySourceIdentifier;
                ValidatedSurveyConceptId = surveyConduct.ValidatedSurveyConceptId;
                ValidatedSurveySourceValue = surveyConduct.ValidatedSurveySourceValue;
                SurveyVersionNumber = surveyConduct.SurveyVersionNumber;
                ResponseVisitOccurrenceId = surveyConduct.ResponseVisitOccurrenceId;
            }
        }

        public bool Equals(SurveyConduct other)
        {
            return this.PersonId.Equals(other.PersonId) &&
                   this.ConceptId == other.ConceptId &&
                   this.StartDate == other.StartDate &&
                   this.EndDate == other.EndDate &&
                   this.VisitOccurrenceId == other.VisitOccurrenceId &&
                   this.TypeConceptId == other.TypeConceptId &&
                   this.ProviderId == other.ProviderId &&
                   this.SourceConceptId == other.SourceConceptId &&
                   this.VisitOccurrenceId == other.VisitOccurrenceId &&
                   this.AssistedConceptId == other.AssistedConceptId &&
                   this.RespondentTypeConceptId == other.RespondentTypeConceptId &&
                   this.TimingConceptId == other.TimingConceptId &&
                   this.CollectionMethodConceptId == other.CollectionMethodConceptId &&
                   this.AssistedSourceValue == other.AssistedSourceValue &&
                   this.RespondentTypeSourceValue == other.RespondentTypeSourceValue &&
                   this.TimingSourceValue == other.TimingSourceValue &&
                   this.CollectionMethodSourceValue == other.CollectionMethodSourceValue &&
                   this.SurveySourceIdentifier == other.SurveySourceIdentifier &&
                   this.ValidatedSurveyConceptId == other.ValidatedSurveyConceptId &&
                   this.ValidatedSurveySourceValue == other.ValidatedSurveySourceValue &&
                   this.SurveyVersionNumber == other.SurveyVersionNumber &&
                   this.ResponseVisitOccurrenceId == other.ResponseVisitOccurrenceId &&
                   this.SourceValue == other.SourceValue;
        }

        public override int GetHashCode()
        {
            return PersonId.GetHashCode() ^
                   ConceptId.GetHashCode() ^
                   (StartDate.GetHashCode()) ^
                   (EndDate.GetHashCode()) ^
                   TypeConceptId.GetHashCode() ^
                   VisitOccurrenceId.GetHashCode() ^
                   ProviderId.GetHashCode() ^
                   AssistedConceptId.GetHashCode() ^
                   RespondentTypeConceptId.GetHashCode() ^
                   TimingConceptId.GetHashCode() ^
                   CollectionMethodConceptId.GetHashCode() ^
                   AssistedSourceValue.GetHashCode() ^
                   RespondentTypeSourceValue.GetHashCode() ^
                   TimingSourceValue.GetHashCode() ^
                   CollectionMethodSourceValue.GetHashCode() ^
                   SurveySourceIdentifier.GetHashCode() ^
                   ValidatedSurveyConceptId.GetHashCode() ^
                   ValidatedSurveySourceValue.GetHashCode() ^
                   SurveyVersionNumber.GetHashCode() ^
                   ResponseVisitOccurrenceId.GetHashCode() ^
                   SourceConceptId.GetHashCode();
        }

        public override EntityType GeEntityType()
        {
            return EntityType.SurveyConduct;
        }
    }
}
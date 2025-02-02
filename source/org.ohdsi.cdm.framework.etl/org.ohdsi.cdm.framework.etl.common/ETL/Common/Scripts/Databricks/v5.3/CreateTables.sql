CREATE TABLE {sc}.METADATA_TMP
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS STRING) AS name
	WHERE 1 = 0;
CREATE TABLE {sc}.COHORT_DEFINITION 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS cohort_definition_id,
	CAST(NULL AS STRING) AS cohort_definition_name,
	CAST(NULL AS STRING) AS cohort_definition_description,
	CAST(NULL AS integer) AS definition_type_concept_id,
	CAST(NULL AS STRING) AS cohort_definition_syntax,
	CAST(NULL AS integer) AS subject_concept_id,
	CAST(NULL AS date) AS cohort_initiation_date
	WHERE 1 = 0;
CREATE TABLE {sc}.COHORT 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS cohort_definition_id,
	CAST(NULL AS bigint) AS subject_id,
	CAST(NULL AS date) AS cohort_start_date,
	CAST(NULL AS date) AS cohort_end_date
	WHERE 1 = 0;

CREATE TABLE {sc}.PERSON 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS gender_concept_id,
	CAST(NULL AS integer) AS year_of_birth,
	CAST(NULL AS integer) AS month_of_birth,
	CAST(NULL AS integer) AS day_of_birth,
	CAST(NULL AS TIMESTAMP) AS birth_datetime,
	CAST(NULL AS integer) AS race_concept_id,
	CAST(NULL AS integer) AS ethnicity_concept_id,
	CAST(NULL AS bigint) AS location_id,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS bigint) AS care_site_id,
	CAST(NULL AS STRING) AS person_source_value,
	CAST(NULL AS STRING) AS gender_source_value,
	CAST(NULL AS integer) AS gender_source_concept_id,
	CAST(NULL AS STRING) AS race_source_value,
	CAST(NULL AS integer) AS race_source_concept_id,
	CAST(NULL AS STRING) AS ethnicity_source_value,
	CAST(NULL AS integer) AS ethnicity_source_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.OBSERVATION_PERIOD 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS observation_period_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS date) AS observation_period_start_date,
	CAST(NULL AS date) AS observation_period_end_date,
	CAST(NULL AS integer) AS period_type_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.VISIT_OCCURRENCE 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS visit_occurrence_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS visit_concept_id,
	CAST(NULL AS date) AS visit_start_date,
	CAST(NULL AS TIMESTAMP) AS visit_start_datetime,
	CAST(NULL AS date) AS visit_end_date,
	CAST(NULL AS TIMESTAMP) AS visit_end_datetime,
	CAST(NULL AS integer) AS visit_type_concept_id,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS bigint) AS care_site_id,
	CAST(NULL AS STRING) AS visit_source_value,
	CAST(NULL AS integer) AS visit_source_concept_id,
	CAST(NULL AS integer) AS admitting_source_concept_id,
	CAST(NULL AS STRING) AS admitting_source_value,
	CAST(NULL AS integer) AS discharge_to_concept_id,
	CAST(NULL AS STRING) AS discharge_to_source_value,
	CAST(NULL AS bigint) AS preceding_visit_occurrence_id WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.VISIT_DETAIL 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS visit_detail_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS visit_detail_concept_id,
	CAST(NULL AS date) AS visit_detail_start_date,
	CAST(NULL AS TIMESTAMP) AS visit_detail_start_datetime,
	CAST(NULL AS date) AS visit_detail_end_date,
	CAST(NULL AS TIMESTAMP) AS visit_detail_end_datetime,
	CAST(NULL AS integer) AS visit_detail_type_concept_id,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS bigint) AS care_site_id,
	CAST(NULL AS STRING) AS visit_detail_source_value,
	CAST(NULL AS integer) AS visit_detail_source_concept_id,
	CAST(NULL AS STRING) AS admitting_source_value,
	CAST(NULL AS integer) AS admitting_source_concept_id,
	CAST(NULL AS STRING) AS discharge_to_source_value,
	CAST(NULL AS integer) AS discharge_to_concept_id,
	CAST(NULL AS bigint) AS preceding_visit_detail_id,
	CAST(NULL AS bigint) AS visit_detail_parent_id,
	CAST(NULL AS bigint) AS visit_occurrence_id WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.CONDITION_OCCURRENCE 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS condition_occurrence_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS condition_concept_id,
	CAST(NULL AS date) AS condition_start_date,
	CAST(NULL AS TIMESTAMP) AS condition_start_datetime,
	CAST(NULL AS date) AS condition_end_date,
	CAST(NULL AS TIMESTAMP) AS condition_end_datetime,
	CAST(NULL AS integer) AS condition_type_concept_id,
	CAST(NULL AS integer) AS condition_status_concept_id,
	CAST(NULL AS STRING) AS stop_reason,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS bigint) AS visit_occurrence_id,
	CAST(NULL AS bigint) AS visit_detail_id,
	CAST(NULL AS STRING) AS condition_source_value,
	CAST(NULL AS integer) AS condition_source_concept_id,
	CAST(NULL AS STRING) AS condition_status_source_value WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.DRUG_EXPOSURE 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS drug_exposure_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS drug_concept_id,
	CAST(NULL AS date) AS drug_exposure_start_date,
	CAST(NULL AS TIMESTAMP) AS drug_exposure_start_datetime,
	CAST(NULL AS date) AS drug_exposure_end_date,
	CAST(NULL AS TIMESTAMP) AS drug_exposure_end_datetime,
	CAST(NULL AS date) AS verbatim_end_date,
	CAST(NULL AS integer) AS drug_type_concept_id,
	CAST(NULL AS STRING) AS stop_reason,
	CAST(NULL AS integer) AS refills,
	CAST(NULL AS float) AS quantity,
	CAST(NULL AS integer) AS days_supply,
	CAST(NULL AS STRING) AS sig,
	CAST(NULL AS integer) AS route_concept_id,
	CAST(NULL AS STRING) AS lot_number,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS bigint) AS visit_occurrence_id,
	CAST(NULL AS bigint) AS visit_detail_id,
	CAST(NULL AS STRING) AS drug_source_value,
	CAST(NULL AS integer) AS drug_source_concept_id,
	CAST(NULL AS STRING) AS route_source_value,
	CAST(NULL AS STRING) AS dose_unit_source_value WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.PROCEDURE_OCCURRENCE 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS procedure_occurrence_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS procedure_concept_id,
	CAST(NULL AS date) AS procedure_date,
	CAST(NULL AS TIMESTAMP) AS procedure_datetime,
	CAST(NULL AS integer) AS procedure_type_concept_id,
	CAST(NULL AS integer) AS modifier_concept_id,
	CAST(NULL AS integer) AS quantity,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS bigint) AS visit_occurrence_id,
	CAST(NULL AS bigint) AS visit_detail_id,
	CAST(NULL AS STRING) AS procedure_source_value,
	CAST(NULL AS integer) AS procedure_source_concept_id,
	CAST(NULL AS STRING) AS modifier_source_value WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.DEVICE_EXPOSURE 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS device_exposure_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS device_concept_id,
	CAST(NULL AS date) AS device_exposure_start_date,
	CAST(NULL AS TIMESTAMP) AS device_exposure_start_datetime,
	CAST(NULL AS date) AS device_exposure_end_date,
	CAST(NULL AS TIMESTAMP) AS device_exposure_end_datetime,
	CAST(NULL AS integer) AS device_type_concept_id,
	CAST(NULL AS STRING) AS unique_device_id,
	CAST(NULL AS integer) AS quantity,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS bigint) AS visit_occurrence_id,
	CAST(NULL AS bigint) AS visit_detail_id,
	CAST(NULL AS STRING) AS device_source_value,
	CAST(NULL AS integer) AS device_source_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.MEASUREMENT 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS measurement_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS measurement_concept_id,
	CAST(NULL AS date) AS measurement_date,
	CAST(NULL AS TIMESTAMP) AS measurement_datetime,
	CAST(NULL AS STRING) AS measurement_time,
	CAST(NULL AS integer) AS measurement_type_concept_id,
	CAST(NULL AS integer) AS operator_concept_id,
	CAST(NULL AS float) AS value_as_number,
	CAST(NULL AS integer) AS value_as_concept_id,
	CAST(NULL AS integer) AS unit_concept_id,
	CAST(NULL AS float) AS range_low,
	CAST(NULL AS float) AS range_high,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS bigint) AS visit_occurrence_id,
	CAST(NULL AS bigint) AS visit_detail_id,
	CAST(NULL AS STRING) AS measurement_source_value,
	CAST(NULL AS integer) AS measurement_source_concept_id,
	CAST(NULL AS STRING) AS unit_source_value,
	CAST(NULL AS STRING) AS value_source_value WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.OBSERVATION 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS observation_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS observation_concept_id,
	CAST(NULL AS date) AS observation_date,
	CAST(NULL AS TIMESTAMP) AS observation_datetime,
	CAST(NULL AS integer) AS observation_type_concept_id,
	CAST(NULL AS float) AS value_as_number,
	CAST(NULL AS STRING) AS value_as_string,
	CAST(NULL AS integer) AS value_as_concept_id,
	CAST(NULL AS integer) AS qualifier_concept_id,
	CAST(NULL AS integer) AS unit_concept_id,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS bigint) AS visit_occurrence_id,
	CAST(NULL AS bigint) AS visit_detail_id,
	CAST(NULL AS STRING) AS observation_source_value,
	CAST(NULL AS integer) AS observation_source_concept_id,
	CAST(NULL AS STRING) AS unit_source_value,
	CAST(NULL AS STRING) AS qualifier_source_value WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.DEATH 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS date) AS death_date,
	CAST(NULL AS TIMESTAMP) AS death_datetime,
	CAST(NULL AS integer) AS death_type_concept_id,
	CAST(NULL AS integer) AS cause_concept_id,
	CAST(NULL AS STRING) AS cause_source_value,
	CAST(NULL AS integer) AS cause_source_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.NOTE 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS note_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS date) AS note_date,
	CAST(NULL AS TIMESTAMP) AS note_datetime,
	CAST(NULL AS integer) AS note_type_concept_id,
	CAST(NULL AS integer) AS note_class_concept_id,
	CAST(NULL AS STRING) AS note_title,
	CAST(NULL AS STRING) AS note_text,
	CAST(NULL AS integer) AS encoding_concept_id,
	CAST(NULL AS integer) AS language_concept_id,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS bigint) AS visit_occurrence_id,
	CAST(NULL AS bigint) AS visit_detail_id,
	CAST(NULL AS STRING) AS note_source_value WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.NOTE_NLP 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS note_nlp_id,
	CAST(NULL AS bigint) AS note_id,
	CAST(NULL AS integer) AS section_concept_id,
	CAST(NULL AS STRING) AS snippet,
	CAST(NULL AS STRING) AS offset,
	CAST(NULL AS STRING) AS lexical_variant,
	CAST(NULL AS integer) AS note_nlp_concept_id,
	CAST(NULL AS integer) AS note_nlp_source_concept_id,
	CAST(NULL AS STRING) AS nlp_system,
	CAST(NULL AS date) AS nlp_date,
	CAST(NULL AS TIMESTAMP) AS nlp_datetime,
	CAST(NULL AS STRING) AS term_exists,
	CAST(NULL AS STRING) AS term_temporal,
	CAST(NULL AS STRING) AS term_modifiers WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.SPECIMEN 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS specimen_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS specimen_concept_id,
	CAST(NULL AS integer) AS specimen_type_concept_id,
	CAST(NULL AS date) AS specimen_date,
	CAST(NULL AS TIMESTAMP) AS specimen_datetime,
	CAST(NULL AS float) AS quantity,
	CAST(NULL AS integer) AS unit_concept_id,
	CAST(NULL AS integer) AS anatomic_site_concept_id,
	CAST(NULL AS integer) AS disease_status_concept_id,
	CAST(NULL AS STRING) AS specimen_source_id,
	CAST(NULL AS STRING) AS specimen_source_value,
	CAST(NULL AS STRING) AS unit_source_value,
	CAST(NULL AS STRING) AS anatomic_site_source_value,
	CAST(NULL AS STRING) AS disease_status_source_value WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.FACT_RELATIONSHIP 
USING DELTA
AS
SELECT 	CAST(NULL AS integer) AS domain_concept_id_1,
	CAST(NULL AS bigint) AS fact_id_1,
	CAST(NULL AS integer) AS domain_concept_id_2,
	CAST(NULL AS bigint) AS fact_id_2,
	CAST(NULL AS integer) AS relationship_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.LOCATION 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS location_id,
	CAST(NULL AS STRING) AS address_1,
	CAST(NULL AS STRING) AS address_2,
	CAST(NULL AS STRING) AS city,
	CAST(NULL AS STRING) AS state,
	CAST(NULL AS STRING) AS zip,
	CAST(NULL AS STRING) AS county,
	CAST(NULL AS STRING) AS location_source_value WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.CARE_SITE 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS care_site_id,
	CAST(NULL AS STRING) AS care_site_name,
	CAST(NULL AS integer) AS place_of_service_concept_id,
	CAST(NULL AS bigint) AS location_id,
	CAST(NULL AS STRING) AS care_site_source_value,
	CAST(NULL AS STRING) AS place_of_service_source_value WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.PROVIDER 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS STRING) AS provider_name,
	CAST(NULL AS STRING) AS npi,
	CAST(NULL AS STRING) AS dea,
	CAST(NULL AS integer) AS specialty_concept_id,
	CAST(NULL AS bigint) AS care_site_id,
	CAST(NULL AS integer) AS year_of_birth,
	CAST(NULL AS integer) AS gender_concept_id,
	CAST(NULL AS STRING) AS provider_source_value,
	CAST(NULL AS STRING) AS specialty_source_value,
	CAST(NULL AS integer) AS specialty_source_concept_id,
	CAST(NULL AS STRING) AS gender_source_value,
	CAST(NULL AS integer) AS gender_source_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.PAYER_PLAN_PERIOD 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS payer_plan_period_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS date) AS payer_plan_period_start_date,
	CAST(NULL AS date) AS payer_plan_period_end_date,
	CAST(NULL AS integer) AS payer_concept_id,
	CAST(NULL AS STRING) AS payer_source_value,
	CAST(NULL AS integer) AS payer_source_concept_id,
	CAST(NULL AS integer) AS plan_concept_id,
	CAST(NULL AS STRING) AS plan_source_value,
	CAST(NULL AS integer) AS plan_source_concept_id,
	CAST(NULL AS integer) AS sponsor_concept_id,
	CAST(NULL AS STRING) AS sponsor_source_value,
	CAST(NULL AS integer) AS sponsor_source_concept_id,
	CAST(NULL AS STRING) AS family_source_value,
	CAST(NULL AS integer) AS stop_reason_concept_id,
	CAST(NULL AS STRING) AS stop_reason_source_value,
	CAST(NULL AS integer) AS stop_reason_source_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.COST 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS cost_id,
	CAST(NULL AS bigint) AS cost_event_id,
	CAST(NULL AS bigint) AS cost_domain_id,
	CAST(NULL AS integer) AS cost_type_concept_id,
	CAST(NULL AS integer) AS currency_concept_id,
	CAST(NULL AS float) AS total_charge,
	CAST(NULL AS float) AS total_cost,
	CAST(NULL AS float) AS total_paid,
	CAST(NULL AS float) AS paid_by_payer,
	CAST(NULL AS float) AS paid_by_patient,
	CAST(NULL AS float) AS paid_patient_copay,
	CAST(NULL AS float) AS paid_patient_coinsurance,
	CAST(NULL AS float) AS paid_patient_deductible,
	CAST(NULL AS float) AS paid_by_primary,
	CAST(NULL AS float) AS paid_ingredient_cost,
	CAST(NULL AS float) AS paid_dispensing_fee,
	CAST(NULL AS bigint) AS payer_plan_period_id,
	CAST(NULL AS float) AS amount_allowed,
	CAST(NULL AS integer) AS revenue_code_concept_id,
	CAST(NULL AS STRING) AS revenue_code_source_value,
	CAST(NULL AS integer) AS drg_concept_id,
	CAST(NULL AS STRING) AS drg_source_value WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.DRUG_ERA 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS drug_era_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS drug_concept_id,
	CAST(NULL AS date) AS drug_era_start_date,
	CAST(NULL AS date) AS drug_era_end_date,
	CAST(NULL AS integer) AS drug_exposure_count,
	CAST(NULL AS integer) AS gap_days WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.DOSE_ERA 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS dose_era_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS drug_concept_id,
	CAST(NULL AS integer) AS unit_concept_id,
	CAST(NULL AS float) AS dose_value,
	CAST(NULL AS date) AS dose_era_start_date,
	CAST(NULL AS date) AS dose_era_end_date WHERE 1 = 0;
--HINT DISTRIBUTE ON KEY (person_id)
CREATE TABLE {sc}.CONDITION_ERA 
USING DELTA
AS
SELECT 	CAST(NULL AS bigint) AS condition_era_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS condition_concept_id,
	CAST(NULL AS date) AS condition_era_start_date,
	CAST(NULL AS date) AS condition_era_end_date,
	CAST(NULL AS integer) AS condition_occurrence_count WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.METADATA 
USING DELTA
AS
SELECT 	CAST(NULL AS integer) AS metadata_concept_id,
	CAST(NULL AS integer) AS metadata_type_concept_id,
	CAST(NULL AS STRING) AS name,
	CAST(NULL AS STRING) AS value_as_string,
	CAST(NULL AS integer) AS value_as_concept_id,
	CAST(NULL AS date) AS metadata_date,
	CAST(NULL AS TIMESTAMP) AS metadata_datetime WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.CDM_SOURCE 
USING DELTA
AS
SELECT 	CAST(NULL AS STRING) AS cdm_source_name,
	CAST(NULL AS STRING) AS cdm_source_abbreviation,
	CAST(NULL AS STRING) AS cdm_holder,
	CAST(NULL AS STRING) AS source_description,
	CAST(NULL AS STRING) AS source_documentation_reference,
	CAST(NULL AS STRING) AS cdm_etl_reference,
	CAST(NULL AS date) AS source_release_date,
	CAST(NULL AS date) AS cdm_release_date,
	CAST(NULL AS STRING) AS cdm_version,
	CAST(NULL AS STRING) AS vocabulary_version WHERE 1 = 0;
CREATE TABLE {sc}.person 
USING DELTA AS	
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS gender_concept_id,
	CAST(NULL AS integer) AS year_of_birth,
	CAST(NULL AS integer) AS month_of_birth,
	CAST(NULL AS integer) AS day_of_birth,
	CAST(NULL AS TIMESTAMP) AS birth_datetime,
	CAST(NULL AS TIMESTAMP) AS death_datetime,
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

CREATE TABLE {sc}.observation_period 
USING DELTA AS	
	CAST(NULL AS bigint) AS observation_period_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS date) AS observation_period_start_date,
	CAST(NULL AS date) AS observation_period_end_date,
	CAST(NULL AS integer) AS period_type_concept_id WHERE 1 = 0;

CREATE TABLE {sc}.specimen 
USING DELTA AS	
	CAST(NULL AS bigint) AS specimen_id,
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

CREATE TABLE {sc}.visit_occurrence 
USING DELTA AS	
	CAST(NULL AS bigint) AS visit_occurrence_id,
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
	CAST(NULL AS integer) AS admitted_from_concept_id,
	CAST(NULL AS STRING) AS admitted_from_source_value,
	CAST(NULL AS STRING) AS discharge_to_source_value,
	CAST(NULL AS integer) AS discharge_to_concept_id,
	CAST(NULL AS bigint) AS preceding_visit_occurrence_id WHERE 1 = 0;

CREATE TABLE {sc}.visit_detail 
USING DELTA AS	
	CAST(NULL AS bigint) AS visit_detail_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS visit_detail_concept_id,
	CAST(NULL AS date) AS visit_detail_start_date,
	CAST(NULL AS TIMESTAMP) AS visit_detail_start_datetime,
	CAST(NULL AS date) AS visit_detail_end_date,
	CAST(NULL AS TIMESTAMP) AS visit_detail_end_datetime,
	CAST(NULL AS integer) AS visit_detail_type_concept_id,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS bigint) AS care_site_id,
	CAST(NULL AS integer) AS discharge_to_concept_id,
	CAST(NULL AS integer) AS admitted_from_concept_id,
	CAST(NULL AS STRING) AS admitted_from_source_value,
	CAST(NULL AS STRING) AS visit_detail_source_value,
	CAST(NULL AS integer) AS visit_detail_source_concept_id,
	CAST(NULL AS STRING) AS discharge_to_source_value,
	CAST(NULL AS bigint) AS preceding_visit_detail_id,
	CAST(NULL AS bigint) AS visit_detail_parent_id,
	CAST(NULL AS bigint) AS visit_occurrence_id WHERE 1 = 0;

CREATE TABLE {sc}.procedure_occurrence 
USING DELTA AS	CAST(NULL AS bigint) AS procedure_occurrence_id,
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

CREATE TABLE {sc}.drug_exposure USING DELTA AS	CAST(NULL AS bigint) AS drug_exposure_id,
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

CREATE TABLE {sc}.device_exposure 
USING DELTA AS	
	CAST(NULL AS bigint) AS device_exposure_id,
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

CREATE TABLE {sc}.condition_occurrence 
USING DELTA AS	
	CAST(NULL AS bigint) AS condition_occurrence_id,
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

CREATE TABLE {sc}.measurement 
USING DELTA AS	
	CAST(NULL AS bigint) AS measurement_id,
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

CREATE TABLE {sc}.note 
USING DELTA AS	
	CAST(NULL AS bigint) AS note_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS bigint) AS note_event_id,
	CAST(NULL AS integer) AS note_event_field_concept_id,
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

CREATE TABLE {sc}.note_nlp 
USING DELTA AS	
	CAST(NULL AS bigint) AS note_nlp_id,
	CAST(NULL AS bigint) AS note_id,
	CAST(NULL AS integer) AS section_concept_id,
	CAST(NULL AS STRING) AS snippet,
	CAST(NULL AS STRING) AS offset,
	CAST(NULL AS STRING) AS lexical_variant,
	CAST(NULL AS integer) AS note_nlp_concept_id,
	CAST(NULL AS STRING) AS nlp_system,
	CAST(NULL AS date) AS nlp_date,
	CAST(NULL AS TIMESTAMP) AS nlp_datetime,
	CAST(NULL AS STRING) AS term_exists,
	CAST(NULL AS STRING) AS term_temporal,
	CAST(NULL AS STRING) AS term_modifiers,
	CAST(NULL AS integer) AS note_nlp_source_concept_id WHERE 1 = 0;

CREATE TABLE {sc}.observation 
USING DELTA AS	
	CAST(NULL AS bigint) AS observation_id,
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
	CAST(NULL AS STRING) AS qualifier_source_value,
	CAST(NULL AS bigint) AS observation_event_id,
	CAST(NULL AS integer) AS obs_event_field_concept_id,
	CAST(NULL AS TIMESTAMP) AS value_as_datetime WHERE 1 = 0;

CREATE TABLE {sc}.survey_conduct 
USING DELTA AS	
	CAST(NULL AS bigint) AS survey_conduct_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS survey_concept_id,
	CAST(NULL AS date) AS survey_start_date,
	CAST(NULL AS TIMESTAMP) AS survey_start_datetime,
	CAST(NULL AS date) AS survey_end_date,
	CAST(NULL AS TIMESTAMP) AS survey_end_datetime,
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS integer) AS assisted_concept_id,
	CAST(NULL AS integer) AS respondent_type_concept_id,
	CAST(NULL AS integer) AS timing_concept_id,
	CAST(NULL AS integer) AS collection_method_concept_id,
	CAST(NULL AS STRING) AS assisted_source_value,
	CAST(NULL AS STRING) AS respondent_type_source_value,
	CAST(NULL AS STRING) AS timing_source_value,
	CAST(NULL AS STRING) AS collection_method_source_value,
	CAST(NULL AS STRING) AS survey_source_value,
	CAST(NULL AS integer) AS survey_source_concept_id,
	CAST(NULL AS STRING) AS survey_source_identifier,
	CAST(NULL AS integer) AS validated_survey_concept_id,
	CAST(NULL AS STRING) AS validated_survey_source_value,
	CAST(NULL AS STRING) AS survey_version_number,
	CAST(NULL AS bigint) AS visit_occurrence_id,
	CAST(NULL AS bigint) AS visit_detail_id,
	CAST(NULL AS bigint) AS response_visit_occurrence_id WHERE 1 = 0;

CREATE TABLE {sc}.fact_relationship 
USING DELTA AS	
	CAST(NULL AS integer) AS domain_concept_id_1,
	CAST(NULL AS bigint) AS fact_id_1,
	CAST(NULL AS integer) AS domain_concept_id_2,
	CAST(NULL AS bigint) AS fact_id_2,
	CAST(NULL AS integer) AS relationship_concept_id WHERE 1 = 0;

CREATE TABLE {sc}.location 
USING DELTA AS	
	CAST(NULL AS bigint) AS location_id,
	CAST(NULL AS STRING) AS address_1,
	CAST(NULL AS STRING) AS address_2,
	CAST(NULL AS STRING) AS city,
	CAST(NULL AS STRING) AS state,
	CAST(NULL AS STRING) AS zip,
	CAST(NULL AS STRING) AS county,
	CAST(NULL AS STRING) AS country,
	CAST(NULL AS STRING) AS location_source_value,
	CAST(NULL AS float) AS latitude,
	CAST(NULL AS float) AS longitude WHERE 1 = 0;

CREATE TABLE {sc}.location_history 
USING DELTA AS	
	CAST(NULL AS bigint) AS location_history_id,
	CAST(NULL AS bigint) AS location_id,
	CAST(NULL AS integer) AS relationship_type_concept_id,
	CAST(NULL AS STRING) AS domain_id,
	CAST(NULL AS bigint) AS entity_id,
	CAST(NULL AS date) AS start_date,
	CAST(NULL AS date) AS end_date WHERE 1 = 0;

CREATE TABLE {sc}.care_site 
USING DELTA AS	
	CAST(NULL AS bigint) AS care_site_id,
	CAST(NULL AS STRING) AS care_site_name,
	CAST(NULL AS integer) AS place_of_service_concept_id,
	CAST(NULL AS bigint) AS location_id,
	CAST(NULL AS STRING) AS care_site_source_value,
	CAST(NULL AS STRING) AS place_of_service_source_value WHERE 1 = 0;

CREATE TABLE {sc}.provider 
USING DELTA AS	
	CAST(NULL AS bigint) AS provider_id,
	CAST(NULL AS STRING) AS provider_name,
	CAST(NULL AS STRING) AS NPI,
	CAST(NULL AS STRING) AS DEA,
	CAST(NULL AS integer) AS specialty_concept_id,
	CAST(NULL AS bigint) AS care_site_id,
	CAST(NULL AS integer) AS year_of_birth,
	CAST(NULL AS integer) AS gender_concept_id,
	CAST(NULL AS STRING) AS provider_source_value,
	CAST(NULL AS STRING) AS specialty_source_value,
	CAST(NULL AS integer) AS specialty_source_concept_id,
	CAST(NULL AS STRING) AS gender_source_value,
	CAST(NULL AS integer) AS gender_source_concept_id WHERE 1 = 0;

CREATE TABLE {sc}.payer_plan_period 
USING DELTA AS	
	CAST(NULL AS bigint) AS payer_plan_period_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS bigint) AS contract_person_id,
	CAST(NULL AS date) AS payer_plan_period_start_date,
	CAST(NULL AS date) AS payer_plan_period_end_date,
	CAST(NULL AS integer) AS payer_concept_id,
	CAST(NULL AS integer) AS plan_concept_id,
	CAST(NULL AS integer) AS contract_concept_id,
	CAST(NULL AS integer) AS sponsor_concept_id,
	CAST(NULL AS integer) AS stop_reason_concept_id,
	CAST(NULL AS STRING) AS payer_source_value,
	CAST(NULL AS integer) AS payer_source_concept_id,
	CAST(NULL AS STRING) AS plan_source_value,
	CAST(NULL AS integer) AS plan_source_concept_id,
	CAST(NULL AS STRING) AS contract_source_value,
	CAST(NULL AS integer) AS contract_source_concept_id,
	CAST(NULL AS STRING) AS sponsor_source_value,
	CAST(NULL AS integer) AS sponsor_source_concept_id,
	CAST(NULL AS STRING) AS family_source_value,
	CAST(NULL AS STRING) AS stop_reason_source_value,
	CAST(NULL AS integer) AS stop_reason_source_concept_id WHERE 1 = 0;

CREATE TABLE {sc}.cost 
USING DELTA AS	
	CAST(NULL AS bigint) AS cost_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS bigint) AS cost_event_id,
	CAST(NULL AS integer) AS cost_event_field_concept_id,
	CAST(NULL AS integer) AS cost_concept_id,
	CAST(NULL AS integer) AS cost_type_concept_id,
	CAST(NULL AS integer) AS currency_concept_id,
	CAST(NULL AS float) AS cost,
	CAST(NULL AS date) AS incurred_date,
	CAST(NULL AS date) AS billed_date,
	CAST(NULL AS date) AS paid_date,
	CAST(NULL AS integer) AS revenue_code_concept_id,
	CAST(NULL AS integer) AS drg_concept_id,
	CAST(NULL AS STRING) AS cost_source_value,
	CAST(NULL AS integer) AS cost_source_concept_id,
	CAST(NULL AS STRING) AS revenue_code_source_value,
	CAST(NULL AS STRING) AS drg_source_value,
	CAST(NULL AS bigint) AS payer_plan_period_id WHERE 1 = 0;

CREATE TABLE {sc}.drug_era 
USING DELTA AS	
	CAST(NULL AS bigint) AS drug_era_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS drug_concept_id,
	CAST(NULL AS TIMESTAMP) AS drug_era_start_datetime,
	CAST(NULL AS TIMESTAMP) AS drug_era_end_datetime,
	CAST(NULL AS integer) AS drug_exposure_count,
	CAST(NULL AS integer) AS gap_days WHERE 1 = 0;

CREATE TABLE {sc}.dose_era 
USING DELTA AS	
	CAST(NULL AS bigint) AS dose_era_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS drug_concept_id,
	CAST(NULL AS integer) AS unit_concept_id,
	CAST(NULL AS float) AS dose_value,
	CAST(NULL AS TIMESTAMP) AS dose_era_start_datetime,
	CAST(NULL AS TIMESTAMP) AS dose_era_end_datetime WHERE 1 = 0;

CREATE TABLE {sc}.condition_era 
USING DELTA AS	
	CAST(NULL AS bigint) AS condition_era_id,
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS integer) AS condition_concept_id,
	CAST(NULL AS TIMESTAMP) AS condition_era_start_datetime,
	CAST(NULL AS TIMESTAMP) AS condition_era_end_datetime,
	CAST(NULL AS integer) AS condition_occurrence_count WHERE 1 = 0;

CREATE TABLE {sc}.cdm_source 
USING DELTA AS	
	CAST(NULL AS STRING) AS cdm_source_name,
	CAST(NULL AS STRING) AS cdm_source_abbreviation,
	CAST(NULL AS STRING) AS cdm_holder,
	CAST(NULL AS STRING) AS source_description,
	CAST(NULL AS STRING) AS source_documentation_reference,
	CAST(NULL AS STRING) AS cdm_etl_reference,
	CAST(NULL AS date) AS source_release_date,
	CAST(NULL AS date) AS cdm_release_date,
	CAST(NULL AS STRING) AS cdm_version,
	CAST(NULL AS STRING) AS vocabulary_version WHERE 1 = 0;

CREATE TABLE {sc}.cohort 
USING DELTA AS	
	CAST(NULL AS bigint) AS cohort_definition_id,
	CAST(NULL AS bigint) AS subject_id,
	CAST(NULL AS date) AS cohort_start_date,
	CAST(NULL AS date) AS cohort_end_date WHERE 1 = 0;

CREATE TABLE {sc}.cohort_definition 
USING DELTA AS	
	CAST(NULL AS bigint) AS cohort_definition_id,
	CAST(NULL AS STRING) AS cohort_definition_name,
	CAST(NULL AS STRING) AS cohort_definition_description,
	CAST(NULL AS bigint) AS definition_type_concept_id,
	CAST(NULL AS STRING) AS cohort_definition_syntax,
	CAST(NULL AS bigint) AS subject_concept_id,
	CAST(NULL AS date) AS cohort_initiation_date WHERE 1 = 0;

CREATE TABLE {sc}.cdm_domain_meta 
USING DELTA AS	
	CAST(NULL AS STRING) AS domain_id,
	CAST(NULL AS STRING) AS description WHERE 1 = 0;

CREATE TABLE {sc}.metadata 
USING DELTA AS	
	CAST(NULL AS integer) AS metadata_concept_id,
	CAST(NULL AS STRING) AS name,
	CAST(NULL AS STRING) AS value_as_string,
	CAST(NULL AS integer) AS value_as_concept_id,
	CAST(NULL AS date) AS metadata_date,
	CAST(NULL AS STRING) AS metadata_datetime WHERE 1 = 0;

CREATE TABLE {sc}.metadata_tmp 
USING DELTA AS	
	CAST(NULL AS bigint) AS person_id,
	CAST(NULL AS STRING) AS name  WHERE 1 = 0;
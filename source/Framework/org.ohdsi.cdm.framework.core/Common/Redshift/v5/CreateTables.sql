CREATE TABLE {sc}.PERSON 
    (
     PERSON_ID						BIGINT		NOT NULL DISTKEY, 
     GENDER_CONCEPT_ID				BIGINT		NOT NULL , 
     YEAR_OF_BIRTH					INTEGER		NOT NULL , 
     MONTH_OF_BIRTH					INTEGER		NULL, 
     DAY_OF_BIRTH					INTEGER		NULL, 
	 TIME_OF_BIRTH					VARCHAR(10)	NULL,
     RACE_CONCEPT_ID				BIGINT		NOT NULL, 
     ETHNICITY_CONCEPT_ID			BIGINT		NOT NULL, 
     LOCATION_ID					BIGINT		NULL, 
     PROVIDER_ID					BIGINT		NULL, 
     CARE_SITE_ID					BIGINT		NULL, 
     PERSON_SOURCE_VALUE			VARCHAR(250) NULL, 
     GENDER_SOURCE_VALUE			VARCHAR(250) NULL,
	 GENDER_SOURCE_CONCEPT_ID		BIGINT		NULL, 
     RACE_SOURCE_VALUE				VARCHAR(250) NULL, 
	 RACE_SOURCE_CONCEPT_ID			BIGINT		NULL, 
     ETHNICITY_SOURCE_VALUE			VARCHAR(250) NULL,
	 ETHNICITY_SOURCE_CONCEPT_ID	BIGINT		NULL
) SORTKEY
(
	PERSON_ID
);




CREATE TABLE {sc}.OBSERVATION_PERIOD 
    ( 
     OBSERVATION_PERIOD_ID				BIGINT		 NOT NULL,
     PERSON_ID							BIGINT		NOT NULL DISTKEY, 
     OBSERVATION_PERIOD_START_DATE		DATE		NOT NULL , 
     OBSERVATION_PERIOD_END_DATE		DATE		NOT NULL ,
	 PERIOD_TYPE_CONCEPT_ID				BIGINT		NOT NULL
) SORTKEY
(
	PERSON_ID
);




CREATE TABLE {sc}.SPECIMEN
    ( 
     SPECIMEN_ID						BIGINT			NOT NULL ,
	 PERSON_ID							BIGINT			NOT NULL DISTKEY,
	 SPECIMEN_CONCEPT_ID				BIGINT			NOT NULL ,
	 SPECIMEN_TYPE_CONCEPT_ID			BIGINT			NOT NULL ,
	 SPECIMEN_DATE						DATE			NOT NULL ,
	 SPECIMEN_TIME						VARCHAR(10)		NULL ,
	 QUANTITY							FLOAT			NULL ,
	 UNIT_CONCEPT_ID					BIGINT			NULL ,
	 ANATOMIC_SITE_CONCEPT_ID			BIGINT			NULL ,
	 DISEASE_STATUS_CONCEPT_ID			BIGINT			NULL ,
	 SPECIMEN_SOURCE_ID					VARCHAR(250)		NULL ,
	 SPECIMEN_SOURCE_VALUE				VARCHAR(250)		NULL ,
	 UNIT_SOURCE_VALUE					VARCHAR(250)		NULL ,
	 ANATOMIC_SITE_SOURCE_VALUE			VARCHAR(250)		NULL ,
	 DISEASE_STATUS_SOURCE_VALUE		VARCHAR(250)		NULL
) SORTKEY
(
	PERSON_ID
);



CREATE TABLE {sc}.DEATH 
    ( 
     PERSON_ID							BIGINT			NOT NULL DISTKEY, 
     DEATH_DATE							DATE			NOT NULL , 
     DEATH_TYPE_CONCEPT_ID				BIGINT			NOT NULL , 
     CAUSE_CONCEPT_ID					BIGINT			NULL , 
     CAUSE_SOURCE_VALUE					VARCHAR(250)		NULL,
	 CAUSE_SOURCE_CONCEPT_ID			BIGINT			NULL
) SORTKEY
(
	PERSON_ID
);




CREATE TABLE {sc}.VISIT_OCCURRENCE 
    ( 
     VISIT_OCCURRENCE_ID			BIGINT			NOT NULL , 
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     VISIT_CONCEPT_ID				BIGINT			NOT NULL , 
	 VISIT_START_DATE				DATE			NOT NULL , 
	 VISIT_START_TIME				VARCHAR(10)		NULL ,
     VISIT_END_DATE					DATE			NOT NULL ,
	 VISIT_END_TIME					VARCHAR(10)		NULL , 
	 VISIT_TYPE_CONCEPT_ID			BIGINT			NOT NULL ,
	 PROVIDER_ID					BIGINT			NULL,
     CARE_SITE_ID					BIGINT			NULL, 
     VISIT_SOURCE_VALUE				VARCHAR(250)		NULL,
	 VISIT_SOURCE_CONCEPT_ID		BIGINT			NULL
) SORTKEY
(
	PERSON_ID
);



CREATE TABLE {sc}.PROCEDURE_OCCURRENCE 
    ( 
     PROCEDURE_OCCURRENCE_ID		BIGINT			NOT NULL , 
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     PROCEDURE_CONCEPT_ID			BIGINT			NOT NULL , 
     PROCEDURE_DATE					DATE			NOT NULL , 
     PROCEDURE_TYPE_CONCEPT_ID		BIGINT			NOT NULL ,
	 MODIFIER_CONCEPT_ID			BIGINT			NULL ,
	 QUANTITY						INTEGER			NULL , 
     PROVIDER_ID					BIGINT			NULL , 
     VISIT_OCCURRENCE_ID			BIGINT			NULL , 
     PROCEDURE_SOURCE_VALUE			VARCHAR(250)	NULL ,
	 PROCEDURE_SOURCE_CONCEPT_ID	BIGINT			NULL ,
	 QUALIFIER_SOURCE_VALUE			VARCHAR(250)		NULL
) SORTKEY
(
	PERSON_ID
);




CREATE TABLE {sc}.DRUG_EXPOSURE 
    ( 
     DRUG_EXPOSURE_ID				BIGINT			NOT NULL , 
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     DRUG_CONCEPT_ID				BIGINT			NOT NULL , 
     DRUG_EXPOSURE_START_DATE		DATE			NOT NULL , 
     DRUG_EXPOSURE_END_DATE			DATE			NULL , 
     DRUG_TYPE_CONCEPT_ID			BIGINT			NOT NULL , 
     STOP_REASON					VARCHAR(20)		NULL , 
     REFILLS						INTEGER			NULL , 
     QUANTITY						FLOAT			NULL , 
     DAYS_SUPPLY					INTEGER			NULL , 
     SIG							VARCHAR(8000)	NULL , 
	 ROUTE_CONCEPT_ID				BIGINT			NULL ,
	 EFFECTIVE_DRUG_DOSE			FLOAT			NULL ,
	 DOSE_UNIT_CONCEPT_ID			BIGINT			NULL ,
	 LOT_NUMBER						VARCHAR(250)		NULL ,
     PROVIDER_ID					BIGINT			NULL , 
     VISIT_OCCURRENCE_ID			BIGINT			NULL , 
     DRUG_SOURCE_VALUE				VARCHAR(250)	NULL ,
	 DRUG_SOURCE_CONCEPT_ID			BIGINT			NULL ,
	 ROUTE_SOURCE_VALUE				VARCHAR(250)		NULL ,
	 DOSE_UNIT_SOURCE_VALUE			VARCHAR(250)		NULL
) SORTKEY
(
	PERSON_ID
);



CREATE TABLE {sc}.DEVICE_EXPOSURE 
    ( 
     DEVICE_EXPOSURE_ID				BIGINT			NOT NULL , 
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     DEVICE_CONCEPT_ID				BIGINT			NOT NULL , 
     DEVICE_EXPOSURE_START_DATE		DATE			NOT NULL , 
     DEVICE_EXPOSURE_END_DATE		DATE			NULL , 
     DEVICE_TYPE_CONCEPT_ID			BIGINT			NOT NULL , 
	 UNIQUE_DEVICE_ID				VARCHAR(250)		NULL ,
	 QUANTITY						INTEGER			NULL ,
     PROVIDER_ID					BIGINT			NULL , 
     VISIT_OCCURRENCE_ID			BIGINT			NULL , 
     DEVICE_SOURCE_VALUE			VARCHAR(100)	NULL ,
	 DEVICE_SOURCE_CONCEPT_ID		BIGINT			NULL
) SORTKEY
(
	PERSON_ID
);



CREATE TABLE {sc}.CONDITION_OCCURRENCE 
    ( 
     CONDITION_OCCURRENCE_ID		BIGINT			 NOT NULL,
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     CONDITION_CONCEPT_ID			BIGINT			NOT NULL , 
     CONDITION_START_DATE			DATE			NOT NULL , 
     CONDITION_END_DATE				DATE			NULL , 
     CONDITION_TYPE_CONCEPT_ID		BIGINT			NOT NULL , 
     STOP_REASON					VARCHAR(20)		NULL , 
     PROVIDER_ID					BIGINT			NULL , 
     VISIT_OCCURRENCE_ID			BIGINT			NULL , 
     CONDITION_SOURCE_VALUE			VARCHAR(250)		NULL ,
	 CONDITION_SOURCE_CONCEPT_ID	BIGINT			NULL
) SORTKEY
(
	PERSON_ID
);




CREATE TABLE {sc}.MEASUREMENT 
    ( 
     MEASUREMENT_ID					BIGINT			 NOT NULL , 
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     MEASUREMENT_CONCEPT_ID			BIGINT			NOT NULL , 
     MEASUREMENT_DATE				DATE			NOT NULL , 
     MEASUREMENT_TIME				VARCHAR(10)		NULL ,
	 MEASUREMENT_TYPE_CONCEPT_ID	BIGINT			NOT NULL ,
	 OPERATOR_CONCEPT_ID			BIGINT			NULL , 
     VALUE_AS_NUMBER				FLOAT			NULL , 
     VALUE_AS_CONCEPT_ID			BIGINT			NULL , 
     UNIT_CONCEPT_ID				BIGINT			NULL , 
     RANGE_LOW						FLOAT			NULL , 
     RANGE_HIGH						FLOAT			NULL , 
     PROVIDER_ID					BIGINT			NULL , 
     VISIT_OCCURRENCE_ID			BIGINT			NULL ,  
     MEASUREMENT_SOURCE_VALUE		VARCHAR(250)	NULL , 
	 MEASUREMENT_SOURCE_CONCEPT_ID	BIGINT			NULL ,
     UNIT_SOURCE_VALUE				VARCHAR(250)		NULL ,
	 VALUE_SOURCE_VALUE				VARCHAR(500)	NULL
)SORTKEY
(
	PERSON_ID
);



CREATE TABLE {sc}.NOTE 
    ( 
     NOTE_ID						BIGINT			NOT NULL , 
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     NOTE_DATE						DATE			NOT NULL ,
	 NOTE_TIME						VARCHAR(10)		NULL ,
	 NOTE_TYPE_CONCEPT_ID			BIGINT			NOT NULL ,
	 NOTE_TEXT						VARCHAR(8000)	NOT NULL ,
     PROVIDER_ID					BIGINT			NULL ,
	 VISIT_OCCURRENCE_ID			BIGINT			NULL ,
	 NOTE_SOURCE_VALUE				VARCHAR(250)		NULL
) SORTKEY
(
	PERSON_ID
);


CREATE TABLE {sc}.OBSERVATION 
    ( 
     OBSERVATION_ID                 BIGINT			 NOT NULL,
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     OBSERVATION_CONCEPT_ID			BIGINT			NOT NULL , 
     OBSERVATION_DATE				DATE			NOT NULL , 
     OBSERVATION_TIME				VARCHAR(10)		NULL , 
     OBSERVATION_TYPE_CONCEPT_ID	BIGINT			NOT NULL , 
	 VALUE_AS_NUMBER				FLOAT			NULL , 
     VALUE_AS_STRING				VARCHAR(500)	NULL , 
     VALUE_AS_CONCEPT_ID			BIGINT			NULL , 
	 QUALIFIER_CONCEPT_ID			BIGINT			NULL ,
     UNIT_CONCEPT_ID				BIGINT			NULL , 
     PROVIDER_ID					BIGINT			NULL , 
     VISIT_OCCURRENCE_ID			BIGINT			NULL , 
     OBSERVATION_SOURCE_VALUE		VARCHAR(250)	NULL ,
	 OBSERVATION_SOURCE_CONCEPT_ID	BIGINT			NULL , 
     UNIT_SOURCE_VALUE				VARCHAR(250)		NULL ,
	 QUALIFIER_SOURCE_VALUE			VARCHAR(250)		NULL
) SORTKEY
(
	PERSON_ID
);


CREATE TABLE {sc}.FACT_RELATIONSHIP 
    ( 
     DOMAIN_CONCEPT_ID_1			BIGINT			NOT NULL , 
	 FACT_ID_1						BIGINT			NOT NULL ,
	 DOMAIN_CONCEPT_ID_2			BIGINT			NOT NULL ,
	 FACT_ID_2						BIGINT			NOT NULL ,
	 RELATIONSHIP_CONCEPT_ID		BIGINT			NOT NULL
) DISTSTYLE ALL;


CREATE TABLE {sc}.LOCATION 
    ( 
     LOCATION_ID					BIGINT			NOT NULL , 
     ADDRESS_1						VARCHAR(250)		NULL , 
     ADDRESS_2						VARCHAR(250)		NULL , 
     CITY							VARCHAR(250)		NULL , 
     STATE							VARCHAR(2)		NULL , 
     ZIP							VARCHAR(9)		NULL , 
     COUNTY							VARCHAR(20)		NULL , 
     LOCATION_SOURCE_VALUE			VARCHAR(250)		NULL
) DISTSTYLE ALL;


CREATE TABLE {sc}.CARE_SITE 
    ( 
     CARE_SITE_ID						BIGINT			NOT NULL , 
	 CARE_SITE_NAME						VARCHAR(255)	NULL ,
     PLACE_OF_SERVICE_CONCEPT_ID		BIGINT			NULL ,
     LOCATION_ID						BIGINT			NULL , 
     CARE_SITE_SOURCE_VALUE				VARCHAR(250)		NULL , 
     PLACE_OF_SERVICE_SOURCE_VALUE		VARCHAR(250)		NULL
) DISTSTYLE ALL;

	
CREATE TABLE {sc}.PROVIDER 
    ( 
     PROVIDER_ID					BIGINT			NOT NULL ,
	 PROVIDER_NAME					VARCHAR(255)	NULL , 
     NPI							VARCHAR(20)		NULL , 
     DEA							VARCHAR(20)		NULL , 
     SPECIALTY_CONCEPT_ID			BIGINT			NULL , 
     CARE_SITE_ID					BIGINT			NULL , 
	 YEAR_OF_BIRTH					INTEGER			NULL ,
	 GENDER_CONCEPT_ID				BIGINT			NULL ,
     PROVIDER_SOURCE_VALUE			VARCHAR(250)		NULL , 
     SPECIALTY_SOURCE_VALUE			VARCHAR(250)		NULL ,
	 SPECIALTY_SOURCE_CONCEPT_ID	BIGINT			NULL , 
	 GENDER_SOURCE_VALUE			VARCHAR(250)		NULL ,
	 GENDER_SOURCE_CONCEPT_ID		BIGINT			NULL
) DISTSTYLE ALL;


CREATE TABLE {sc}.PAYER_PLAN_PERIOD 
    ( 
     PAYER_PLAN_PERIOD_ID			BIGINT			NOT NULL , 
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     PAYER_PLAN_PERIOD_START_DATE	DATE			NOT NULL , 
     PAYER_PLAN_PERIOD_END_DATE		DATE			NOT NULL , 
     PAYER_SOURCE_VALUE				VARCHAR (50)	NULL , 
     PLAN_SOURCE_VALUE				VARCHAR (50)	NULL , 
     FAMILY_SOURCE_VALUE			VARCHAR (50)	NULL 
) SORTKEY
(
	PERSON_ID
);



CREATE TABLE {sc}.VISIT_COST 
    ( 
     VISIT_COST_ID					BIGINT			 NOT NULL, 
     VISIT_OCCURRENCE_ID			BIGINT			NOT NULL DISTKEY, 
	 CURRENCY_CONCEPT_ID			BIGINT			NULL ,
     PAID_COPAY						FLOAT			NULL , 
     PAID_COINSURANCE				FLOAT			NULL , 
     PAID_TOWARD_DEDUCTIBLE			FLOAT			NULL , 
     PAID_BY_PAYER					FLOAT			NULL , 
     PAID_BY_COORDINATION_BENEFITS	FLOAT			NULL , 
     TOTAL_OUT_OF_POCKET			FLOAT			NULL , 
     TOTAL_PAID						FLOAT			NULL ,  
     PAYER_PLAN_PERIOD_ID			BIGINT			NULL
) SORTKEY
(
	VISIT_OCCURRENCE_ID
);


CREATE TABLE {sc}.PROCEDURE_COST 
    ( 
     PROCEDURE_COST_ID				BIGINT			 NOT NULL,
     PROCEDURE_OCCURRENCE_ID		BIGINT			NOT NULL DISTKEY, 
     CURRENCY_CONCEPT_ID			BIGINT			NULL ,
     PAID_COPAY						FLOAT			NULL , 
     PAID_COINSURANCE				FLOAT			NULL , 
     PAID_TOWARD_DEDUCTIBLE			FLOAT			NULL , 
     PAID_BY_PAYER					FLOAT			NULL , 
     PAID_BY_COORDINATION_BENEFITS	FLOAT			NULL , 
     TOTAL_OUT_OF_POCKET			FLOAT			NULL , 
     TOTAL_PAID						FLOAT			NULL ,
	 REVENUE_CODE_CONCEPT_ID		BIGINT			NULL ,  
     PAYER_PLAN_PERIOD_ID			BIGINT			NULL ,
	 REVENUE_CODE_SOURCE_VALUE		VARCHAR(250)		NULL
) SORTKEY
(
	PROCEDURE_OCCURRENCE_ID
);



CREATE TABLE {sc}.DRUG_COST 
    (
     DRUG_COST_ID					BIGINT			NOT NULL , 
     DRUG_EXPOSURE_ID				BIGINT			NOT NULL DISTKEY, 
     CURRENCY_CONCEPT_ID			BIGINT			NULL ,
     PAID_COPAY						FLOAT			NULL , 
     PAID_COINSURANCE				FLOAT			NULL , 
     PAID_TOWARD_DEDUCTIBLE			FLOAT			NULL , 
     PAID_BY_PAYER					FLOAT			NULL , 
     PAID_BY_COORDINATION_BENEFITS	FLOAT			NULL , 
     TOTAL_OUT_OF_POCKET			FLOAT			NULL , 
     TOTAL_PAID						FLOAT			NULL , 
     INGREDIENT_COST				FLOAT			NULL , 
     DISPENSING_FEE					FLOAT			NULL , 
     AVERAGE_WHOLESALE_PRICE		FLOAT			NULL , 
     PAYER_PLAN_PERIOD_ID			BIGINT			NULL
) SORTKEY
(
	DRUG_EXPOSURE_ID
);





CREATE TABLE {sc}.DEVICE_COST 
    (
     DEVICE_COST_ID					BIGINT			 NOT NULL , 
     DEVICE_EXPOSURE_ID				BIGINT			NOT NULL DISTKEY, 
     CURRENCY_CONCEPT_ID			BIGINT			NULL ,
     PAID_COPAY						FLOAT			NULL , 
     PAID_COINSURANCE				FLOAT			NULL , 
     PAID_TOWARD_DEDUCTIBLE			FLOAT			NULL , 
     PAID_BY_PAYER					FLOAT			NULL , 
     PAID_BY_COORDINATION_BENEFITS	FLOAT			NULL , 
     TOTAL_OUT_OF_POCKET			FLOAT			NULL , 
     TOTAL_PAID						FLOAT			NULL , 
     PAYER_PLAN_PERIOD_ID			BIGINT			NULL
) SORTKEY
(
	DEVICE_EXPOSURE_ID
);



CREATE TABLE {sc}.COHORT 
    ( 
	 COHORT_DEFINITION_ID			BIGINT			NOT NULL, 
     SUBJECT_ID						BIGINT			NOT NULL DISTKEY,
	 COHORT_START_DATE				DATE			NOT NULL , 
     COHORT_END_DATE				DATE			NOT NULL
) SORTKEY
(
	SUBJECT_ID
);


CREATE TABLE {sc}.COHORT_ATTRIBUTE 
    ( 
	 COHORT_DEFINITION_ID			BIGINT			NOT NULL , 
     COHORT_START_DATE				DATE			NOT NULL , 
     COHORT_END_DATE				DATE			NOT NULL , 
     SUBJECT_ID						BIGINT			NOT NULL DISTKEY, 
     ATTRIBUTE_DEFINITION_ID		BIGINT			NOT NULL ,
	 VALUE_AS_NUMBER				FLOAT			NULL ,
	 VALUE_AS_CONCEPT_ID			BIGINT			NULL
) SORTKEY
(
	SUBJECT_ID
);




CREATE TABLE {sc}.DRUG_ERA 
    ( 
     DRUG_ERA_ID					BIGINT			 NOT NULL,
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     DRUG_CONCEPT_ID				BIGINT			NOT NULL , 
     DRUG_ERA_START_DATE			DATE			NOT NULL , 
     DRUG_ERA_END_DATE				DATE			NOT NULL , 
     DRUG_EXPOSURE_COUNT			INTEGER			NULL ,
	 GAP_DAYS						INTEGER			NULL
) SORTKEY
(
	PERSON_ID
);


CREATE TABLE {sc}.DOSE_ERA 
    (
     DOSE_ERA_ID					BIGINT			NOT NULL , 
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     DRUG_CONCEPT_ID				BIGINT			NOT NULL , 
	 UNIT_CONCEPT_ID				BIGINT			NOT NULL ,
	 DOSE_VALUE						FLOAT			NOT NULL ,
     DOSE_ERA_START_DATE			DATE			NOT NULL , 
     DOSE_ERA_END_DATE				DATE			NOT NULL 
) SORTKEY
(
	PERSON_ID
);




CREATE TABLE {sc}.CONDITION_ERA 
    ( 
     CONDITION_ERA_ID				BIGINT			 NOT NULL,
     PERSON_ID						BIGINT			NOT NULL DISTKEY, 
     CONDITION_CONCEPT_ID			BIGINT			NOT NULL , 
     CONDITION_ERA_START_DATE		DATE			NOT NULL , 
     CONDITION_ERA_END_DATE			DATE			NOT NULL , 
     CONDITION_OCCURRENCE_COUNT		INTEGER			NULL
) SORTKEY
(
	PERSON_ID
);

CREATE TABLE {sc}.CDM_SOURCE(
	CDM_SOURCE_NAME varchar(250) NOT NULL,
	CDM_SOURCE_ABBREVIATION VARCHAR(250) NOT NULL,
	CDM_HOLDER VARCHAR(250) NOT NULL,
	SOURCE_DESCRIPTION varchar(max) NOT NULL,
	SOURCE_DOCUMENTATION_REFERENCE varchar(250) NOT NULL,
	CDM_ETL_REFERENCE varchar(250) NOT NULL,
	SOURCE_RELEASE_DATE varchar(10) NOT NULL,
	CDM_RELEASE_DATE varchar(10) NOT NULL,
	CDM_VERSION VARCHAR(250) NOT NULL,
	VOCABULARY_VERSION VARCHAR(250) NOT NULL
) DISTSTYLE ALL;

CREATE TABLE {sc}.COHORT_DEFINITION(
	COHORT_DEFINITION_ID bigint NOT NULL,
	COHORT_DEFINITION_NAME varchar(250) NOT NULL,
	COHORT_DEFINITION_DESCRIPTION varchar(max) NOT NULL,
	DEFINITION_TYPE_CONCEPT_ID bigint NULL,
	COHORT_DEFINITION_SYNTAX varchar(250) NULL,
	SUBJECT_CONCEPT_ID bigint NULL,
	COHORT_INSTANTIATION_DATE date NOT NULL
) DISTSTYLE ALL;


CREATE TABLE {sc}.CDM_DOMAIN_META (
DOMAIN_ID varchar(20),
DESCRIPTION varchar(4000)
) DISTSTYLE ALL;

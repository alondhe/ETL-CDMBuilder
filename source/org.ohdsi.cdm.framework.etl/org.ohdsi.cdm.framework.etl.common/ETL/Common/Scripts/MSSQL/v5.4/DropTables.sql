﻿IF OBJECT_ID (N'{sc}.CDM_SOURCE', N'U') IS NOT NULL drop table {sc}.CDM_SOURCE; 
IF OBJECT_ID (N'{sc}.CARE_SITE', N'U') IS NOT NULL drop table {sc}.CARE_SITE; 
IF OBJECT_ID (N'{sc}.COHORT', N'U') IS NOT NULL drop table {sc}.COHORT; 
IF OBJECT_ID (N'{sc}.COHORT_ATTRIBUTE', N'U') IS NOT NULL drop table {sc}.COHORT_ATTRIBUTE; 
IF OBJECT_ID (N'{sc}.COHORT_DEFINITION', N'U') IS NOT NULL drop table {sc}.COHORT_DEFINITION;
IF OBJECT_ID (N'{sc}.CONDITION_ERA', N'U') IS NOT NULL drop table {sc}.CONDITION_ERA; 
IF OBJECT_ID (N'{sc}.CONDITION_OCCURRENCE', N'U') IS NOT NULL drop table {sc}.CONDITION_OCCURRENCE; 
IF OBJECT_ID (N'{sc}.DEATH', N'U') IS NOT NULL drop table {sc}.DEATH; 
IF OBJECT_ID (N'{sc}.DEVICE_EXPOSURE', N'U') IS NOT NULL drop table {sc}.DEVICE_EXPOSURE; 
IF OBJECT_ID (N'{sc}.DOSE_ERA', N'U') IS NOT NULL drop table {sc}.DOSE_ERA; 
IF OBJECT_ID (N'{sc}.DRUG_ERA', N'U') IS NOT NULL drop table {sc}.DRUG_ERA; 
IF OBJECT_ID (N'{sc}.DRUG_EXPOSURE', N'U') IS NOT NULL drop table {sc}.DRUG_EXPOSURE; 
IF OBJECT_ID (N'{sc}.FACT_RELATIONSHIP', N'U') IS NOT NULL drop table {sc}.FACT_RELATIONSHIP; 
IF OBJECT_ID (N'{sc}.LOCATION', N'U') IS NOT NULL drop table {sc}.LOCATION; 
IF OBJECT_ID (N'{sc}.MEASUREMENT', N'U') IS NOT NULL drop table {sc}.MEASUREMENT; 
IF OBJECT_ID (N'{sc}.NOTE', N'U') IS NOT NULL drop table {sc}.NOTE; 
IF OBJECT_ID (N'{sc}.NOTE_NLP', N'U') IS NOT NULL drop table {sc}.NOTE_NLP; 
IF OBJECT_ID (N'{sc}.OBSERVATION', N'U') IS NOT NULL drop table {sc}.OBSERVATION; 
IF OBJECT_ID (N'{sc}.OBSERVATION_PERIOD', N'U') IS NOT NULL drop table {sc}.OBSERVATION_PERIOD; 
IF OBJECT_ID (N'{sc}.ORGANIZATION', N'U') IS NOT NULL drop table {sc}.ORGANIZATION; 
IF OBJECT_ID (N'{sc}.PAYER_PLAN_PERIOD', N'U') IS NOT NULL drop table {sc}.PAYER_PLAN_PERIOD; 
IF OBJECT_ID (N'{sc}.PERSON', N'U') IS NOT NULL drop table {sc}.PERSON; 
IF OBJECT_ID (N'{sc}.PROCEDURE_OCCURRENCE', N'U') IS NOT NULL drop table {sc}.PROCEDURE_OCCURRENCE; 
IF OBJECT_ID (N'{sc}.PROVIDER', N'U') IS NOT NULL drop table {sc}.PROVIDER; 
IF OBJECT_ID (N'{sc}.SPECIMEN', N'U') IS NOT NULL drop table {sc}.SPECIMEN; 
IF OBJECT_ID (N'{sc}.VISIT_OCCURRENCE', N'U') IS NOT NULL drop table {sc}.VISIT_OCCURRENCE;
IF OBJECT_ID (N'{sc}.VISIT_DETAIL', N'U') IS NOT NULL drop table {sc}.VISIT_DETAIL;
IF OBJECT_ID (N'{sc}.COST', N'U') IS NOT NULL drop table {sc}.COST;
IF OBJECT_ID (N'{sc}.CDM_DOMAIN_META', N'U') IS NOT NULL drop table {sc}.CDM_DOMAIN_META;
IF OBJECT_ID (N'{sc}.EPISODE', N'U') IS NOT NULL drop table {sc}.EPISODE;
IF OBJECT_ID (N'{sc}.EPISODE_EVENT', N'U') IS NOT NULL drop table {sc}.EPISODE_EVENT;
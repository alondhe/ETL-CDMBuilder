DROP TABLE IF EXISTS {sc}.concept;
DROP TABLE IF EXISTS {sc}.concept_ancestor;
DROP TABLE IF EXISTS {sc}.concept_class;
DROP TABLE IF EXISTS {sc}.concept_relationship;
DROP TABLE IF EXISTS {sc}.concept_synonym;
DROP TABLE IF EXISTS {sc}.domain;
DROP TABLE IF EXISTS {sc}.drug_strength;
DROP TABLE IF EXISTS {sc}.relationship;
DROP TABLE IF EXISTS {sc}.source_to_concept_map;
DROP TABLE IF EXISTS {sc}.vocabulary;


--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.CONCEPT 
USING DELTA
AS
SELECT 	CAST(NULL AS integer) AS concept_id,
	CAST(NULL AS STRING) AS concept_name,
	CAST(NULL AS STRING) AS domain_id,
	CAST(NULL AS STRING) AS vocabulary_id,
	CAST(NULL AS STRING) AS concept_class_id,
	CAST(NULL AS STRING) AS standard_concept,
	CAST(NULL AS STRING) AS concept_code,
	CAST(NULL AS date) AS valid_start_date,
	CAST(NULL AS date) AS valid_end_date,
	CAST(NULL AS STRING) AS invalid_reason WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.VOCABULARY 
USING DELTA
AS
SELECT 	CAST(NULL AS STRING) AS vocabulary_id,
	CAST(NULL AS STRING) AS vocabulary_name,
	CAST(NULL AS STRING) AS vocabulary_reference,
	CAST(NULL AS STRING) AS vocabulary_version,
	CAST(NULL AS integer) AS vocabulary_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.DOMAIN 
USING DELTA
AS
SELECT 	CAST(NULL AS STRING) AS domain_id,
	CAST(NULL AS STRING) AS domain_name,
	CAST(NULL AS integer) AS domain_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.CONCEPT_CLASS 
USING DELTA
AS
SELECT 	CAST(NULL AS STRING) AS concept_class_id,
	CAST(NULL AS STRING) AS concept_class_name,
	CAST(NULL AS integer) AS concept_class_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.CONCEPT_RELATIONSHIP 
USING DELTA
AS
SELECT 	CAST(NULL AS integer) AS concept_id_1,
	CAST(NULL AS integer) AS concept_id_2,
	CAST(NULL AS STRING) AS relationship_id,
	CAST(NULL AS date) AS valid_start_date,
	CAST(NULL AS date) AS valid_end_date,
	CAST(NULL AS STRING) AS invalid_reason WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.RELATIONSHIP 
USING DELTA
AS
SELECT 	CAST(NULL AS STRING) AS relationship_id,
	CAST(NULL AS STRING) AS relationship_name,
	CAST(NULL AS STRING) AS is_hierarchical,
	CAST(NULL AS STRING) AS defines_ancestry,
	CAST(NULL AS STRING) AS reverse_relationship_id,
	CAST(NULL AS integer) AS relationship_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.CONCEPT_SYNONYM 
USING DELTA
AS
SELECT 	CAST(NULL AS integer) AS concept_id,
	CAST(NULL AS STRING) AS concept_synonym_name,
	CAST(NULL AS integer) AS language_concept_id WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.CONCEPT_ANCESTOR 
USING DELTA
AS
SELECT 	CAST(NULL AS integer) AS ancestor_concept_id,
	CAST(NULL AS integer) AS descendant_concept_id,
	CAST(NULL AS integer) AS min_levels_of_separation,
	CAST(NULL AS integer) AS max_levels_of_separation WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.SOURCE_TO_CONCEPT_MAP 
USING DELTA
AS
SELECT 	CAST(NULL AS STRING) AS source_code,
	CAST(NULL AS integer) AS source_concept_id,
	CAST(NULL AS STRING) AS source_vocabulary_id,
	CAST(NULL AS STRING) AS source_code_description,
	CAST(NULL AS integer) AS target_concept_id,
	CAST(NULL AS STRING) AS target_vocabulary_id,
	CAST(NULL AS date) AS valid_start_date,
	CAST(NULL AS date) AS valid_end_date,
	CAST(NULL AS STRING) AS invalid_reason WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.DRUG_STRENGTH 
USING DELTA
AS
SELECT 	CAST(NULL AS integer) AS drug_concept_id,
	CAST(NULL AS integer) AS ingredient_concept_id,
	CAST(NULL AS float) AS amount_value,
	CAST(NULL AS integer) AS amount_unit_concept_id,
	CAST(NULL AS float) AS numerator_value,
	CAST(NULL AS integer) AS numerator_unit_concept_id,
	CAST(NULL AS float) AS denominator_value,
	CAST(NULL AS integer) AS denominator_unit_concept_id,
	CAST(NULL AS integer) AS box_size,
	CAST(NULL AS date) AS valid_start_date,
	CAST(NULL AS date) AS valid_end_date,
	CAST(NULL AS STRING) AS invalid_reason WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.COHORT_DEFINITION 
USING DELTA
AS
SELECT 	CAST(NULL AS integer) AS cohort_definition_id,
	CAST(NULL AS STRING) AS cohort_definition_name,
	CAST(NULL AS STRING) AS cohort_definition_description,
	CAST(NULL AS integer) AS definition_type_concept_id,
	CAST(NULL AS STRING) AS cohort_definition_syntax,
	CAST(NULL AS integer) AS subject_concept_id,
	CAST(NULL AS date) AS cohort_initiation_date WHERE 1 = 0;
--HINT DISTRIBUTE ON RANDOM
CREATE TABLE {sc}.ATTRIBUTE_DEFINITION 
USING DELTA
AS
SELECT 	CAST(NULL AS integer) AS attribute_definition_id,
	CAST(NULL AS STRING) AS attribute_name,
	CAST(NULL AS STRING) AS attribute_description,
	CAST(NULL AS integer) AS attribute_type_concept_id,
	CAST(NULL AS STRING) AS attribute_syntax WHERE 1 = 0;
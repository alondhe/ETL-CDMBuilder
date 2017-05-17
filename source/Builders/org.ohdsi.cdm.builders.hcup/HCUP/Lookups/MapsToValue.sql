SELECT distinct REPLACE(icd9.concept_code, '.', '') AS source_code, standard.concept_id AS target_concept_id
FROM concept icd9
INNER JOIN concept_relationship
	ON icd9.concept_id = concept_id_1
INNER JOIN concept standard
	ON standard.concept_id = concept_id_2
WHERE icd9.vocabulary_id = 'ICD9CM'
	AND standard.standard_concept = 'S'
	AND relationship_id = 'Maps to value'
	AND (concept_relationship.invalid_reason IS NULL OR concept_relationship.invalid_reason = '')
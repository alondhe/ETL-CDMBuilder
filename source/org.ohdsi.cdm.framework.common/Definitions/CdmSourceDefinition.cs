using org.ohdsi.cdm.framework.common.Builder;
using org.ohdsi.cdm.framework.common.Extensions;
using org.ohdsi.cdm.framework.common.Omop;
using System.Collections.Generic;
using System.Data;

namespace org.ohdsi.cdm.framework.common.Definitions
{
    public class CdmSourceDefinition : EntityDefinition
    {
        public string CdmSourceName { get; set; }
        public string CdmSourceAbbreviation { get; set; }
        public string CdmHolder { get; set; }
        public string SourceDescription { get; set; }
        public string SourceDocumentationReference { get; set; }
        public string CdmEtlReference { get; set; }

        public string SourceReleaseDate { get; set; }
        public string CdmReleaseDate { get; set; }
        public string CdmVersion { get; set; }
        public string VocabularyVersion { get; set; }

        public override IEnumerable<IEntity> GetConcepts(Concept concept, IDataRecord reader,
            KeyMasterOffsetManager keyOffset)
        {
            var cdmSource = new CdmSource
            {
                CdmSourceName = reader.GetString(CdmSourceName),
                CdmSourceAbbreviation = reader.GetString(CdmSourceAbbreviation),
                SourceDescription = reader.GetString(SourceDescription),
                CdmHolder = reader.GetString(CdmHolder),
                SourceDocumentationReference = reader.GetString(SourceDocumentationReference),
                CdmEtlReference = reader.GetString(CdmEtlReference),
                SourceReleaseDate = reader.GetDateTime(SourceReleaseDate),
                CdmReleaseDate = reader.GetDateTime(CdmReleaseDate),
                CdmVersion = reader.GetString(CdmVersion),
                VocabularyVersion = reader.GetString(VocabularyVersion)
            };

            yield return cdmSource;
        }
    }
}

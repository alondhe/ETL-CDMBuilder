using org.ohdsi.cdm.framework.common.Base;
using org.ohdsi.cdm.framework.common.Definitions;
using org.ohdsi.cdm.framework.desktop.Base;
using org.ohdsi.cdm.framework.desktop.Databases;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace org.ohdsi.cdm.presentation.builderwebapi.ETL
{
    public class DatabaseChunkBuilder
    {
        #region Variables

        private readonly int _conversionId;
        private readonly int _chunkId;

        #endregion

        #region Constructors

        public DatabaseChunkBuilder(int conversionId, int chunkId)
        {
            _conversionId = conversionId;
            _chunkId = chunkId;
        }
        #endregion

        #region Methods
        public DatabaseChunkPart Process(IDatabaseEngine sourceEngine, string sourceSchemaName, List<QueryDefinition> sourceQueryDefinitions, string sourceConnectionString, BuildSettings settings)
        {
            try
            {
                Console.WriteLine("DatabaseChunkBuilder");

                var part = new DatabaseChunkPart(_conversionId, _chunkId, () => new PersonBuilder(settings), "0", 0);

                var timer = new Stopwatch();
                timer.Start();


                var result = part.Load(sourceEngine, sourceSchemaName, sourceQueryDefinitions, sourceConnectionString, "");

                if (result.Value != null)
                {
                    throw result.Value;
                }

                part.Build();

                return part;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion
    }
}

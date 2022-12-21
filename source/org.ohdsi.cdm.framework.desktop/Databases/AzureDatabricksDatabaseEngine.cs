using org.ohdsi.cdm.framework.desktop.Enums;
using org.ohdsi.cdm.framework.desktop.Savers;
using System.Data;
using System.Data.Odbc;

namespace org.ohdsi.cdm.framework.desktop.Databases
{
    public class AzureDatabricksDatabaseEngine : DatabaseEngine
    {
        public AzureDatabricksDatabaseEngine()
        {
            Database = Database.AzureDatabricks;
        }

        public override IDbCommand GetCommand(string cmdText, IDbConnection connection)
        {
            var c = new OdbcCommand(cmdText, (OdbcConnection)connection);
            return c;
        }

        public override ISaver GetSaver()
        {
            return new AzureDatabricksSaver();
        }
    }
}
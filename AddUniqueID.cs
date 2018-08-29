using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Migration
{
    public class AddUniqueID
    {
        private bool ReplaceUserWithUniqueID(string fileName, string connString)
        {
            string sqlCommand = "UPDATE " + fileName + " SET User = Uniqueid";
            SQLExecutor sqlExe = new SQLExecutor();
            return sqlExe.CommandExecutor(sqlCommand, connString);
        }
    }
}

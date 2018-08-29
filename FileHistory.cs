using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Migration
{
    public class FileHistory
    {
        public int version;
        public string user_name;
        public DateTime revDate;
        public string file_name;
        public string action;
        public string comment;
    }
}

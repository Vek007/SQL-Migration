using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Migration
{
    public static class Data
    {
        private static tsEntities db = new tsEntities();

        public static tsEntities ST_DB { get { return db; } }

    }
}

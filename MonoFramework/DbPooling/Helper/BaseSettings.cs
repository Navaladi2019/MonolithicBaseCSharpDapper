using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoFrame.DataBase
{
    public class BaseSettings
    {
        public string Secret { get; set; }
        public string ConnectionString { get; set; }

        public string ErrorLogPath { get; set; }

        public int PoolSize { get; set; }


        public int MaxIdleTime { get; set; }

        public string DataBaseType { get; set; }
    }
}

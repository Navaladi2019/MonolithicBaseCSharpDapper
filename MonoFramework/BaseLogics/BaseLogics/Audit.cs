using MonoFrame.BaseObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFrame.BaseLogics
{   
   public class Audit : DoBase
    {
        public long AuditId { get; set; }

        public string Action { get; set; }

        public string OldRecord { get; set; }

        public string NewRecord { get; set; }
    }
}

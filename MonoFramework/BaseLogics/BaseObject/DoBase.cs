using MonoFrame.BaseObject;
using MonoFrame.QueryGenerator;

using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFrame.BaseObject
{
  public abstract  class DoBase
    {
        public DoBase()
        {
            State = ObjState.None;
            
        }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
        public long UpdateSeq { get; set; }
      

        [Ignore]

        public Dictionary<string,object> OldValues { get; set; }


        [Ignore]
        public ObjState State { get; set; }

        [Ignore]

        public InfoMessage InfoMsg{ get; set; }

        [Ignore]
        public List<InfoMessage> ErrMsg { get; set; }

    }
}

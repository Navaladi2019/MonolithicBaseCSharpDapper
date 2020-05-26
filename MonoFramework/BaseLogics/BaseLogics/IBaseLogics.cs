using MonoFrame.BaseObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFrame.BaseLogics
{
   public interface IBaseLogics
    {
        public DoBase idoBase { get; set; }


        public DoBase Save();


        public DoBase Delete();


        public  DoBase Open();


         
    }
}

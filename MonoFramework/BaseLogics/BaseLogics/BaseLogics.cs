using MonoFrame.DataBase;
using MonoFrame.BaseObject;
using MonoFrame.QueryGenerator;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MonoFrame.BaseLogics
{
    public class BaseLogics : IBaseLogics
    {


        public DoBase idoBase { get; set ; }

        public DoBase Delete()
        {
            SqlResult sqlResult = BlQuery.GetDeleteQuery(idoBase);
            int DeletedRow = DataAccess.Execute(sqlResult);
            if(DeletedRow > 0)
            {
                idoBase.State = ObjState.Deleted;
                idoBase.InfoMsg = new InfoMessage { Message = "Data Deleted Successfully." };
            }
            return idoBase;
        }

        public DoBase Open()
        {
            SqlResult sqlResult = BlQuery.GetOpenQuery(idoBase);
            idoBase = DataAccess.QueryFirstOrDefault(sqlResult);
            if (idoBase != null)
            {
                idoBase.State = ObjState.Selected;
                idoBase.InfoMsg = new InfoMessage { Message = "Data selected & Opened Successfully." };
            }
            return idoBase;
        }


        private DoBase Insert()
        {

            SqlResult sqlResult = BlQuery.GetInsertQuery(idoBase);
            object Id = DataAccess.ExecuteScalar(sqlResult);
            idoBase.SetPrimaryValue(Id);
            idoBase.State = ObjState.Inserted;
            idoBase.OldValues = ConstructOldValues(idoBase);
            idoBase.InfoMsg.Message = "New Data Saved Successfully.";
            return idoBase;
        }


        private void SaveAudit(string OldRecord, string NewRecord, string Action, string UserId)
        {
            Audit lAudit = new Audit();
            lAudit.Action = Action;
            lAudit.OldRecord = OldRecord;
            lAudit.NewRecord = NewRecord;
            lAudit.CreatedBy = UserId;
            lAudit.ModifiedBy = UserId;
            lAudit.CreatedDate = DateTime.Now;
            lAudit.ModifiedDate = DateTime.Now;
            SqlResult sqlResult = BlQuery.GetInsertQuery(lAudit);
            object Id = DataAccess.ExecuteScalar(sqlResult);

        }



        private DoBase Update()
        {
            object ChangedValues = null;
            bool HasChangedRecord = false;
            ChangedValues = GetChangedValues(idoBase, ref HasChangedRecord);
            if (HasChangedRecord == true)
            {
                SqlResult sqlResult = BlQuery.GetUpdateQuery(idoBase, ChangedValues);
                int updatedRow = DataAccess.Execute(sqlResult);
                if(updatedRow > 0)
                {
                    idoBase.State = ObjState.Updated;
                    idoBase.OldValues = ConstructOldValues(idoBase);
                    idoBase.InfoMsg.Message = "Data Updated Successfully.";

                }
            }
            else
            {
                idoBase.State = ObjState.NoChanges;
                idoBase.InfoMsg.Message = "No Changes Made To Data.";
            }

            return null;
        }

        public DoBase Save()
        {
               
            if (idoBase.State == ObjState.None && idoBase.GetPrimaryKeyValue() == 0)
            {
             return   Insert();
            }
            else if(idoBase.GetPrimaryKeyValue() > 0)
            {
                return Update();
            }

            idoBase.State = ObjState.NoChanges;
            return idoBase;


        }


        private Dictionary<string,object> ConstructOldValues(DoBase aDoBase)
        {

            Dictionary<string, object> OldValues = new Dictionary<string, object>();
            PropertyInfo[] propertyInfos = aDoBase.GetType().GetProperties();

            foreach(PropertyInfo property in propertyInfos)
            {
                if(! property.GetCustomAttributesData().Where(x=>x.AttributeType.Name == "IgnoreAttribute").Any())
                {
                    OldValues.Add(property.Name, property.GetValue(aDoBase));
                }
            }

            return OldValues;

        }

        private object GetChangedValues(DoBase aDoBase,ref bool HasChangedRecord)
        {
            HasChangedRecord = false;
            ExpandoObject expando = new ExpandoObject();
            if (aDoBase.OldValues != null && aDoBase.OldValues.Count > 0)
            {
                foreach(var old in aDoBase.OldValues)
                {
                    if (aDoBase.GetType().GetProperty(old.Key).GetValue(aDoBase) != old.Value && !(aDoBase.GetType().GetProperty(old.Key).GetCustomAttributesData().Where(x => x.AttributeType.Name == "IgnoreAttribute").Any()))
                    {
                        HasChangedRecord = true;
                        expando.AddProperty(old.Key, aDoBase.GetType().GetProperty(old.Key).GetValue(aDoBase));
                    }
                }
               
            }

            return expando;
        }

      
    }
}

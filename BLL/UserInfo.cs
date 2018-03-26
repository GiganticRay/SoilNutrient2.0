using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using SoilNutrientSoft.Model;
using System.Data.SqlClient;
namespace SoilNutrientSoft.BLL
{
    /// <summary>
    /// UserInfo
    /// </summary>
    public partial class UserInfo
    {
        private readonly SoilNutrientSoft.DAL.UserInfo dal = new SoilNutrientSoft.DAL.UserInfo();
        public UserInfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该用户ID、密码记录
        /// </summary>
        public bool Exists(string UserId, string UserPassword)
        {
            return dal.Exists(UserId, UserPassword);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SoilNutrientSoft.Model.UserInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SoilNutrientSoft.Model.UserInfo model)
        {
            return dal.Update(model);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SoilNutrientSoft.Model.UserInfo GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public SoilNutrientSoft.Model.UserInfo GetModelByCache(int id)
        {

            string CacheKey = "UserInfoModel-" + id;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(id);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (SoilNutrientSoft.Model.UserInfo)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SoilNutrientSoft.Model.UserInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SoilNutrientSoft.Model.UserInfo> DataTableToList(DataTable dt)
        {
            List<SoilNutrientSoft.Model.UserInfo> modelList = new List<SoilNutrientSoft.Model.UserInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SoilNutrientSoft.Model.UserInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 是否存在此GUID记录
        /// </summary>
        public bool Exists(String StrGuid)
        {
            return dal.Exists(StrGuid);
        }
        /// <summary>
        /// 删除用掉的GUID
        /// </summary>
        public bool Delete(string Guid_Code)
        {

            return dal.Delete(Guid_Code);
        }
        #endregion  ExtensionMethod
    }
}

﻿using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using SoilNutrientSoft.Model;
using System.Data.SqlClient;
namespace SoilNutrientSoft.BLL
{
	/// <summary>
	/// CropsMeg
	/// </summary>
	public partial class CropsMeg
	{
		private readonly SoilNutrientSoft.DAL.CropsMeg dal=new SoilNutrientSoft.DAL.CropsMeg();
		public CropsMeg()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
        /// 增加一条数据
		/// </summary>
		public int  Add(SoilNutrientSoft.Model.CropsMeg model)
		{
			return dal.Add(model);
		}
        

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(SoilNutrientSoft.Model.CropsMeg model, Dictionary<SqlConnection, SqlTransaction> MyDict)
		{
            try
            {
                return dal.Update(model, MyDict);
            }
            catch (Exception e)
            {
                throw e;
            }
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public bool Delete(int Id, Dictionary<SqlConnection, SqlTransaction> MyDict)
		{
            try
            {
                return dal.Delete(Id, MyDict);
            }
            catch(Exception e)
            {
                throw e;
            }
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			return dal.DeleteList(Idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SoilNutrientSoft.Model.CropsMeg GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public SoilNutrientSoft.Model.CropsMeg GetModelByCache(int Id)
		{
			
			string CacheKey = "CropsMegModel-" + Id;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (SoilNutrientSoft.Model.CropsMeg)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SoilNutrientSoft.Model.CropsMeg> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SoilNutrientSoft.Model.CropsMeg> DataTableToList(DataTable dt)
		{
			List<SoilNutrientSoft.Model.CropsMeg> modelList = new List<SoilNutrientSoft.Model.CropsMeg>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SoilNutrientSoft.Model.CropsMeg model;
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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
        /// 自己的增加一条数据 Dictionary<SqlConnection, SqlTransaction> MyDict
        /// </summary>
        public int Add(SoilNutrientSoft.Model.CropsMeg model, Dictionary<SqlConnection, SqlTransaction> MyDict)
        {
            try
            {
                return dal.Add(model, MyDict);
            }
            catch
            {
                throw;
            }
        }
		#endregion  ExtensionMethod
	}
}


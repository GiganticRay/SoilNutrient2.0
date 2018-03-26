﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace SoilNutrientSoft.DAL
{
	/// <summary>
	/// 数据访问类:SoilNutrientMeg
	/// </summary>
	public partial class SoilNutrientMeg
	{
		public SoilNutrientMeg()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "SoilNutrientMeg"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SoilNutrientMeg");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SoilNutrientSoft.Model.SoilNutrientMeg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SoilNutrientMeg(");
            strSql.Append("N,P,K,HydrolyticN,QuickP,QUicK,OrganicMatter,PH,All_id)");
            strSql.Append(" values (");
            strSql.Append("@N,@P,@K,@HydrolyticN,@QuickP,@QUicK,@OrganicMatter,@PH,@All_id)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@N", SqlDbType.Float,8),
					new SqlParameter("@P", SqlDbType.Float,8),
					new SqlParameter("@K", SqlDbType.Float,8),
					new SqlParameter("@HydrolyticN", SqlDbType.Float,8),
					new SqlParameter("@QuickP", SqlDbType.Float,8),
					new SqlParameter("@QUicK", SqlDbType.Float,8),
					new SqlParameter("@OrganicMatter", SqlDbType.Float,8),
					new SqlParameter("@PH", SqlDbType.Float,8),
					new SqlParameter("@All_id", SqlDbType.Int,4)};
            parameters[0].Value = model.N;
            parameters[1].Value = model.P;
            parameters[2].Value = model.K;
            parameters[3].Value = model.HydrolyticN;
            parameters[4].Value = model.QuickP;
            parameters[5].Value = model.QUicK;
            parameters[6].Value = model.OrganicMatter;
            parameters[7].Value = model.PH;
            parameters[8].Value = model.All_id;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(SoilNutrientSoft.Model.SoilNutrientMeg model, Dictionary<SqlConnection, SqlTransaction> MyDict)
		{
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update SoilNutrientMeg set ");
                strSql.Append("N=@N,");
                strSql.Append("P=@P,");
                strSql.Append("K=@K,");
                strSql.Append("HydrolyticN=@HydrolyticN,");
                strSql.Append("QuickP=@QuickP,");
                strSql.Append("QUicK=@QUicK,");
                strSql.Append("OrganicMatter=@OrganicMatter,");
                strSql.Append("PH=@PH,");
                strSql.Append("All_id=@All_id");
                strSql.Append(" where Id=@Id");
                SqlParameter[] parameters = {
					new SqlParameter("@N", SqlDbType.Float,8),
					new SqlParameter("@P", SqlDbType.Float,8),
					new SqlParameter("@K", SqlDbType.Float,8),
					new SqlParameter("@HydrolyticN", SqlDbType.Float,8),
					new SqlParameter("@QuickP", SqlDbType.Float,8),
					new SqlParameter("@QUicK", SqlDbType.Float,8),
					new SqlParameter("@OrganicMatter", SqlDbType.Float,8),
					new SqlParameter("@PH", SqlDbType.Float,8),
					new SqlParameter("@All_id", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
                parameters[0].Value = model.N;
                parameters[1].Value = model.P;
                parameters[2].Value = model.K;
                parameters[3].Value = model.HydrolyticN;
                parameters[4].Value = model.QuickP;
                parameters[5].Value = model.QUicK;
                parameters[6].Value = model.OrganicMatter;
                parameters[7].Value = model.PH;
                parameters[8].Value = model.All_id;
                parameters[9].Value = model.Id;

                int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), MyDict, parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
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
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from SoilNutrientMeg ");
                strSql.Append(" where Id=@Id");
                SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			    };
                parameters[0].Value = Id;

                int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), MyDict, parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SoilNutrientMeg ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SoilNutrientSoft.Model.SoilNutrientMeg GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,N,P,K,HydrolyticN,QuickP,QUicK,OrganicMatter,PH,All_id from SoilNutrientMeg ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			SoilNutrientSoft.Model.SoilNutrientMeg model=new SoilNutrientSoft.Model.SoilNutrientMeg();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SoilNutrientSoft.Model.SoilNutrientMeg DataRowToModel(DataRow row)
		{
			SoilNutrientSoft.Model.SoilNutrientMeg model=new SoilNutrientSoft.Model.SoilNutrientMeg();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["N"]!=null && row["N"].ToString()!="")
				{
					model.N=decimal.Parse(row["N"].ToString());
				}
				if(row["P"]!=null && row["P"].ToString()!="")
				{
					model.P=decimal.Parse(row["P"].ToString());
				}
				if(row["K"]!=null && row["K"].ToString()!="")
				{
					model.K=decimal.Parse(row["K"].ToString());
				}
				if(row["HydrolyticN"]!=null && row["HydrolyticN"].ToString()!="")
				{
					model.HydrolyticN=decimal.Parse(row["HydrolyticN"].ToString());
				}
				if(row["QuickP"]!=null && row["QuickP"].ToString()!="")
				{
					model.QuickP=decimal.Parse(row["QuickP"].ToString());
				}
				if(row["QUicK"]!=null && row["QUicK"].ToString()!="")
				{
					model.QUicK=decimal.Parse(row["QUicK"].ToString());
				}
				if(row["OrganicMatter"]!=null && row["OrganicMatter"].ToString()!="")
				{
					model.OrganicMatter=decimal.Parse(row["OrganicMatter"].ToString());
				}
				if(row["PH"]!=null && row["PH"].ToString()!="")
				{
					model.PH=decimal.Parse(row["PH"].ToString());
				}
				if(row["All_id"]!=null && row["All_id"].ToString()!="")
				{
					model.All_id=int.Parse(row["All_id"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,N,P,K,HydrolyticN,QuickP,QUicK,OrganicMatter,PH,All_id ");
			strSql.Append(" FROM SoilNutrientMeg ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,N,P,K,HydrolyticN,QuickP,QUicK,OrganicMatter,PH,All_id ");
			strSql.Append(" FROM SoilNutrientMeg ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM SoilNutrientMeg ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from SoilNutrientMeg T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "SoilNutrientMeg";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 自己的增加一条数据
        /// </summary>
        public int Add(SoilNutrientSoft.Model.SoilNutrientMeg model, Dictionary<SqlConnection, SqlTransaction> MyDict)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SoilNutrientMeg(");
            strSql.Append("N,P,K,HydrolyticN,QuickP,QUicK,OrganicMatter,PH,All_id)");
            strSql.Append(" values (");
            strSql.Append("@N,@P,@K,@HydrolyticN,@QuickP,@QUicK,@OrganicMatter,@PH,@All_id)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@N", SqlDbType.Float,8),
					new SqlParameter("@P", SqlDbType.Float,8),
					new SqlParameter("@K", SqlDbType.Float,8),
					new SqlParameter("@HydrolyticN", SqlDbType.Float,8),
					new SqlParameter("@QuickP", SqlDbType.Float,8),
					new SqlParameter("@QUicK", SqlDbType.Float,8),
					new SqlParameter("@OrganicMatter", SqlDbType.Float,8),
					new SqlParameter("@PH", SqlDbType.Float,8),
					new SqlParameter("@All_id", SqlDbType.Int,4)};
            parameters[0].Value = model.N;
            parameters[1].Value = model.P;
            parameters[2].Value = model.K;
            parameters[3].Value = model.HydrolyticN;
            parameters[4].Value = model.QuickP;
            parameters[5].Value = model.QUicK;
            parameters[6].Value = model.OrganicMatter;
            parameters[7].Value = model.PH;
            parameters[8].Value = model.All_id;

            try
            {
                object obj = DbHelperSQL.GetSingle(strSql.ToString(), MyDict, parameters);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
            catch
            {
                throw;
            }
        }
		#endregion  ExtensionMethod
	}
}


using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace SoilNutrientSoft.DAL
{
	/// <summary>
	/// 数据访问类:CropsMeg
	/// </summary>
	public partial class CropsMeg
	{
		public CropsMeg()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "CropsMeg"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CropsMeg");
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
		public int Add(SoilNutrientSoft.Model.CropsMeg model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CropsMeg(");
			strSql.Append("CropType,Varieties,Yield,urea,An,K,Organic_manure,Others,Irrigation_times,All_id)");
			strSql.Append(" values (");
			strSql.Append("@CropType,@Varieties,@Yield,@urea,@An,@K,@Organic_manure,@Others,@Irrigation_times,@All_id)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CropType", SqlDbType.NVarChar,50),
					new SqlParameter("@Varieties", SqlDbType.NVarChar,50),
					new SqlParameter("@Yield", SqlDbType.Float,8),
					new SqlParameter("@urea", SqlDbType.Float,8),
					new SqlParameter("@An", SqlDbType.Float,8),
					new SqlParameter("@K", SqlDbType.Float,8),
					new SqlParameter("@Organic_manure", SqlDbType.Float,8),
					new SqlParameter("@Others", SqlDbType.Float,8),
					new SqlParameter("@Irrigation_times", SqlDbType.Int,4),
					new SqlParameter("@All_id", SqlDbType.Int,4)};
			parameters[0].Value = model.CropType;
			parameters[1].Value = model.Varieties;
			parameters[2].Value = model.Yield;
			parameters[3].Value = model.urea;
			parameters[4].Value = model.An;
			parameters[5].Value = model.K;
			parameters[6].Value = model.Organic_manure;
			parameters[7].Value = model.Others;
			parameters[8].Value = model.Irrigation_times;
			parameters[9].Value = model.All_id;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
        public bool Update(SoilNutrientSoft.Model.CropsMeg model, Dictionary<SqlConnection, SqlTransaction> MyDict)
		{
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update CropsMeg set ");
                strSql.Append("CropType=@CropType,");
                strSql.Append("Varieties=@Varieties,");
                strSql.Append("Yield=@Yield,");
                strSql.Append("urea=@urea,");
                strSql.Append("An=@An,");
                strSql.Append("K=@K,");
                strSql.Append("Organic_manure=@Organic_manure,");
                strSql.Append("Others=@Others,");
                strSql.Append("Irrigation_times=@Irrigation_times,");
                strSql.Append("All_id=@All_id");
                strSql.Append(" where Id=@Id");
                SqlParameter[] parameters = {
					new SqlParameter("@CropType", SqlDbType.NVarChar,50),
					new SqlParameter("@Varieties", SqlDbType.NVarChar,50),
					new SqlParameter("@Yield", SqlDbType.Float,8),
					new SqlParameter("@urea", SqlDbType.Float,8),
					new SqlParameter("@An", SqlDbType.Float,8),
					new SqlParameter("@K", SqlDbType.Float,8),
					new SqlParameter("@Organic_manure", SqlDbType.Float,8),
					new SqlParameter("@Others", SqlDbType.Float,8),
					new SqlParameter("@Irrigation_times", SqlDbType.Int,4),
					new SqlParameter("@All_id", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
                parameters[0].Value = model.CropType;
                parameters[1].Value = model.Varieties;
                parameters[2].Value = model.Yield;
                parameters[3].Value = model.urea;
                parameters[4].Value = model.An;
                parameters[5].Value = model.K;
                parameters[6].Value = model.Organic_manure;
                parameters[7].Value = model.Others;
                parameters[8].Value = model.Irrigation_times;
                parameters[9].Value = model.All_id;
                parameters[10].Value = model.Id;

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
		/// 删除一条数据
		/// </summary>
        public bool Delete(int Id, Dictionary<SqlConnection, SqlTransaction> MyDict)
		{
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from CropsMeg ");
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
            catch(Exception e)
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
			strSql.Append("delete from CropsMeg ");
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
		public SoilNutrientSoft.Model.CropsMeg GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,CropType,Varieties,Yield,urea,An,K,Organic_manure,Others,Irrigation_times,All_id from CropsMeg ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			SoilNutrientSoft.Model.CropsMeg model=new SoilNutrientSoft.Model.CropsMeg();
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
		public SoilNutrientSoft.Model.CropsMeg DataRowToModel(DataRow row)
		{
			SoilNutrientSoft.Model.CropsMeg model=new SoilNutrientSoft.Model.CropsMeg();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["CropType"]!=null)
				{
					model.CropType=row["CropType"].ToString();
				}
				if(row["Varieties"]!=null)
				{
					model.Varieties=row["Varieties"].ToString();
				}
				if(row["Yield"]!=null && row["Yield"].ToString()!="")
				{
					model.Yield=decimal.Parse(row["Yield"].ToString());
				}
				if(row["urea"]!=null && row["urea"].ToString()!="")
				{
					model.urea=decimal.Parse(row["urea"].ToString());
				}
				if(row["An"]!=null && row["An"].ToString()!="")
				{
					model.An=decimal.Parse(row["An"].ToString());
				}
				if(row["K"]!=null && row["K"].ToString()!="")
				{
					model.K=decimal.Parse(row["K"].ToString());
				}
				if(row["Organic_manure"]!=null && row["Organic_manure"].ToString()!="")
				{
					model.Organic_manure=decimal.Parse(row["Organic_manure"].ToString());
				}
				if(row["Others"]!=null && row["Others"].ToString()!="")
				{
					model.Others=decimal.Parse(row["Others"].ToString());
				}
				if(row["Irrigation_times"]!=null && row["Irrigation_times"].ToString()!="")
				{
					model.Irrigation_times=int.Parse(row["Irrigation_times"].ToString());
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
			strSql.Append("select Id,CropType,Varieties,Yield,urea,An,K,Organic_manure,Others,Irrigation_times,All_id ");
			strSql.Append(" FROM CropsMeg ");
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
			strSql.Append(" Id,CropType,Varieties,Yield,urea,An,K,Organic_manure,Others,Irrigation_times,All_id ");
			strSql.Append(" FROM CropsMeg ");
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
			strSql.Append("select count(1) FROM CropsMeg ");
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
			strSql.Append(")AS Row, T.*  from CropsMeg T ");
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
			parameters[0].Value = "CropsMeg";
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
        public int Add(SoilNutrientSoft.Model.CropsMeg model, Dictionary<SqlConnection, SqlTransaction> MyDict)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CropsMeg(");
            strSql.Append("CropType,Varieties,Yield,urea,An,K,Organic_manure,Others,Irrigation_times,All_id)");
            strSql.Append(" values (");
            strSql.Append("@CropType,@Varieties,@Yield,@urea,@An,@K,@Organic_manure,@Others,@Irrigation_times,@All_id)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CropType", SqlDbType.NVarChar,50),
					new SqlParameter("@Varieties", SqlDbType.NVarChar,50),
					new SqlParameter("@Yield", SqlDbType.Float,8),
					new SqlParameter("@urea", SqlDbType.Float,8),
					new SqlParameter("@An", SqlDbType.Float,8),
					new SqlParameter("@K", SqlDbType.Float,8),
					new SqlParameter("@Organic_manure", SqlDbType.Float,8),
					new SqlParameter("@Others", SqlDbType.Float,8),
					new SqlParameter("@Irrigation_times", SqlDbType.Int,4),
					new SqlParameter("@All_id", SqlDbType.Int,4)};
            parameters[0].Value = model.CropType;
            parameters[1].Value = model.Varieties;
            parameters[2].Value = model.Yield;
            parameters[3].Value = model.urea;
            parameters[4].Value = model.An;
            parameters[5].Value = model.K;
            parameters[6].Value = model.Organic_manure;
            parameters[7].Value = model.Others;
            parameters[8].Value = model.Irrigation_times;
            parameters[9].Value = model.All_id;

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


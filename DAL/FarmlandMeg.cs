using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace SoilNutrientSoft.DAL
{
	/// <summary>
	/// 数据访问类:FarmlandMeg
	/// </summary>
	public partial class FarmlandMeg
	{
		public FarmlandMeg()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "FarmlandMeg"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from FarmlandMeg");
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
		public int Add(SoilNutrientSoft.Model.FarmlandMeg model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into FarmlandMeg(");
			strSql.Append("City,County,Town,Village,Sample_name,Lon,Lat,Name_of_householder,Phone_number,Irrigation_Conditions,Acreage,Fertility,Weeds)");
			strSql.Append(" values (");
			strSql.Append("@City,@County,@Town,@Village,@Sample_name,@Lon,@Lat,@Name_of_householder,@Phone_number,@Irrigation_Conditions,@Acreage,@Fertility,@Weeds)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@City", SqlDbType.NVarChar,50),
					new SqlParameter("@County", SqlDbType.NVarChar,50),
					new SqlParameter("@Town", SqlDbType.NVarChar,50),
					new SqlParameter("@Village", SqlDbType.NVarChar,50),
					new SqlParameter("@Sample_name", SqlDbType.NVarChar,50),
					new SqlParameter("@Lon", SqlDbType.Float,8),
					new SqlParameter("@Lat", SqlDbType.Float,8),
					new SqlParameter("@Name_of_householder", SqlDbType.NVarChar,50),
					new SqlParameter("@Phone_number", SqlDbType.NVarChar,11),
					new SqlParameter("@Irrigation_Conditions", SqlDbType.Int,4),
					new SqlParameter("@Acreage", SqlDbType.Float,8),
					new SqlParameter("@Fertility", SqlDbType.Int,4),
					new SqlParameter("@Weeds", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.City;
			parameters[1].Value = model.County;
			parameters[2].Value = model.Town;
			parameters[3].Value = model.Village;
			parameters[4].Value = model.Sample_name;
			parameters[5].Value = model.Lon;
			parameters[6].Value = model.Lat;
			parameters[7].Value = model.Name_of_householder;
			parameters[8].Value = model.Phone_number;
			parameters[9].Value = model.Irrigation_Conditions;
			parameters[10].Value = model.Acreage;
			parameters[11].Value = model.Fertility;
			parameters[12].Value = model.Weeds;

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
        public bool Update(SoilNutrientSoft.Model.FarmlandMeg model, Dictionary<SqlConnection, SqlTransaction> MyDict)
		{
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FarmlandMeg set ");
                strSql.Append("City=@City,");
                strSql.Append("County=@County,");
                strSql.Append("Town=@Town,");
                strSql.Append("Village=@Village,");
                strSql.Append("Sample_name=@Sample_name,");
                strSql.Append("Lon=@Lon,");
                strSql.Append("Lat=@Lat,");
                strSql.Append("Name_of_householder=@Name_of_householder,");
                strSql.Append("Phone_number=@Phone_number,");
                strSql.Append("Irrigation_Conditions=@Irrigation_Conditions,");
                strSql.Append("Acreage=@Acreage,");
                strSql.Append("Fertility=@Fertility,");
                strSql.Append("Weeds=@Weeds");
                strSql.Append(" where Id=@Id");
                SqlParameter[] parameters = {
					new SqlParameter("@City", SqlDbType.NVarChar,50),
					new SqlParameter("@County", SqlDbType.NVarChar,50),
					new SqlParameter("@Town", SqlDbType.NVarChar,50),
					new SqlParameter("@Village", SqlDbType.NVarChar,50),
					new SqlParameter("@Sample_name", SqlDbType.NVarChar,50),
					new SqlParameter("@Lon", SqlDbType.Float,8),
					new SqlParameter("@Lat", SqlDbType.Float,8),
					new SqlParameter("@Name_of_householder", SqlDbType.NVarChar,50),
					new SqlParameter("@Phone_number", SqlDbType.NVarChar,11),
					new SqlParameter("@Irrigation_Conditions", SqlDbType.Int,4),
					new SqlParameter("@Acreage", SqlDbType.Float,8),
					new SqlParameter("@Fertility", SqlDbType.Int,4),
					new SqlParameter("@Weeds", SqlDbType.NVarChar,100),
					new SqlParameter("@Id", SqlDbType.Int,4)};
                parameters[0].Value = model.City;
                parameters[1].Value = model.County;
                parameters[2].Value = model.Town;
                parameters[3].Value = model.Village;
                parameters[4].Value = model.Sample_name;
                parameters[5].Value = model.Lon;
                parameters[6].Value = model.Lat;
                parameters[7].Value = model.Name_of_householder;
                parameters[8].Value = model.Phone_number;
                parameters[9].Value = model.Irrigation_Conditions;
                parameters[10].Value = model.Acreage;
                parameters[11].Value = model.Fertility;
                parameters[12].Value = model.Weeds;
                parameters[13].Value = model.Id;

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
                strSql.Append("delete from FarmlandMeg ");
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
			strSql.Append("delete from FarmlandMeg ");
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
		public SoilNutrientSoft.Model.FarmlandMeg GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,City,County,Town,Village,Sample_name,Lon,Lat,Name_of_householder,Phone_number,Irrigation_Conditions,Acreage,Fertility,Weeds from FarmlandMeg ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			SoilNutrientSoft.Model.FarmlandMeg model=new SoilNutrientSoft.Model.FarmlandMeg();
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
		public SoilNutrientSoft.Model.FarmlandMeg DataRowToModel(DataRow row)
		{
			SoilNutrientSoft.Model.FarmlandMeg model=new SoilNutrientSoft.Model.FarmlandMeg();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["City"]!=null)
				{
					model.City=row["City"].ToString();
				}
				if(row["County"]!=null)
				{
					model.County=row["County"].ToString();
				}
				if(row["Town"]!=null)
				{
					model.Town=row["Town"].ToString();
				}
				if(row["Village"]!=null)
				{
					model.Village=row["Village"].ToString();
				}
				if(row["Sample_name"]!=null)
				{
					model.Sample_name=row["Sample_name"].ToString();
				}
				if(row["Lon"]!=null && row["Lon"].ToString()!="")
				{
					model.Lon=decimal.Parse(row["Lon"].ToString());
				}
				if(row["Lat"]!=null && row["Lat"].ToString()!="")
				{
					model.Lat=decimal.Parse(row["Lat"].ToString());
				}
				if(row["Name_of_householder"]!=null)
				{
					model.Name_of_householder=row["Name_of_householder"].ToString();
				}
				if(row["Phone_number"]!=null)
				{
					model.Phone_number=row["Phone_number"].ToString();
				}
				if(row["Irrigation_Conditions"]!=null && row["Irrigation_Conditions"].ToString()!="")
				{
					model.Irrigation_Conditions=int.Parse(row["Irrigation_Conditions"].ToString());
				}
				if(row["Acreage"]!=null && row["Acreage"].ToString()!="")
				{
					model.Acreage=decimal.Parse(row["Acreage"].ToString());
				}
				if(row["Fertility"]!=null && row["Fertility"].ToString()!="")
				{
					model.Fertility=int.Parse(row["Fertility"].ToString());
				}
				if(row["Weeds"]!=null)
				{
					model.Weeds=row["Weeds"].ToString();
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
			strSql.Append("select Id,City,County,Town,Village,Sample_name,Lon,Lat,Name_of_householder,Phone_number,Irrigation_Conditions,Acreage,Fertility,Weeds ");
			strSql.Append(" FROM FarmlandMeg ");
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
			strSql.Append(" Id,City,County,Town,Village,Sample_name,Lon,Lat,Name_of_householder,Phone_number,Irrigation_Conditions,Acreage,Fertility,Weeds ");
			strSql.Append(" FROM FarmlandMeg ");
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
			strSql.Append("select count(1) FROM FarmlandMeg ");
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
			strSql.Append(")AS Row, T.*  from FarmlandMeg T ");
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
			parameters[0].Value = "FarmlandMeg";
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
        /// 自己的 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Myconn"></param>
        /// <returns></returns>
        public int Add(SoilNutrientSoft.Model.FarmlandMeg model, Dictionary<SqlConnection, SqlTransaction> MyDict)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FarmlandMeg(");
            strSql.Append("City,County,Town,Village,Sample_name,Lon,Lat,Name_of_householder,Phone_number,Irrigation_Conditions,Acreage,Fertility,Weeds)");
            strSql.Append(" values (");
            strSql.Append("@City,@County,@Town,@Village,@Sample_name,@Lon,@Lat,@Name_of_householder,@Phone_number,@Irrigation_Conditions,@Acreage,@Fertility,@Weeds)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@City", SqlDbType.NVarChar,50),
					new SqlParameter("@County", SqlDbType.NVarChar,50),
					new SqlParameter("@Town", SqlDbType.NVarChar,50),
					new SqlParameter("@Village", SqlDbType.NVarChar,50),
					new SqlParameter("@Sample_name", SqlDbType.NVarChar,50),
					new SqlParameter("@Lon", SqlDbType.Float,8),
					new SqlParameter("@Lat", SqlDbType.Float,8),
					new SqlParameter("@Name_of_householder", SqlDbType.NVarChar,50),
					new SqlParameter("@Phone_number", SqlDbType.NVarChar,11),
					new SqlParameter("@Irrigation_Conditions", SqlDbType.Int,4),
					new SqlParameter("@Acreage", SqlDbType.Float,8),
					new SqlParameter("@Fertility", SqlDbType.Int,4),
					new SqlParameter("@Weeds", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.City;
            parameters[1].Value = model.County;
            parameters[2].Value = model.Town;
            parameters[3].Value = model.Village;
            parameters[4].Value = model.Sample_name;
            parameters[5].Value = model.Lon;
            parameters[6].Value = model.Lat;
            parameters[7].Value = model.Name_of_householder;
            parameters[8].Value = model.Phone_number;
            parameters[9].Value = model.Irrigation_Conditions;
            parameters[10].Value = model.Acreage;
            parameters[11].Value = model.Fertility;
            parameters[12].Value = model.Weeds;

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


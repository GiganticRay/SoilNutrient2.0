using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace SoilNutrientSoft.DAL
{
	/// <summary>
	/// 数据访问类:FarmlandMSug
	/// </summary>
	public partial class FarmlandMSug
	{
		public FarmlandMSug()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "FarmlandMSug"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from FarmlandMSug");
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
        public int Add(SoilNutrientSoft.Model.FarmlandMSug model, params SqlParameter[] cmdParms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FarmlandMSug(");
            strSql.Append("CropType,Varieties,TargetYield,urea,An,K,OrganicManure,Others,IrrigationTimes,SowingAmount,SowingMethod,WeedControl,PestControl,FieldManagement,Remarks,All_id)");
            strSql.Append(" values (");
            strSql.Append("@CropType,@Varieties,@TargetYield,@urea,@An,@K,@OrganicManure,@Others,@IrrigationTimes,@SowingAmount,@SowingMethod,@WeedControl,@PestControl,@FieldManagement,@Remarks,@All_id)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CropType", SqlDbType.NVarChar,50),
					new SqlParameter("@Varieties", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetYield", SqlDbType.Float,8),
					new SqlParameter("@urea", SqlDbType.Float,8),
					new SqlParameter("@An", SqlDbType.Float,8),
					new SqlParameter("@K", SqlDbType.Float,8),
					new SqlParameter("@OrganicManure", SqlDbType.Float,8),
					new SqlParameter("@Others", SqlDbType.Float,8),
					new SqlParameter("@IrrigationTimes", SqlDbType.Int,4),
					new SqlParameter("@SowingAmount", SqlDbType.Float,8),
					new SqlParameter("@SowingMethod", SqlDbType.NVarChar,200),
					new SqlParameter("@WeedControl", SqlDbType.NVarChar,200),
					new SqlParameter("@PestControl", SqlDbType.NVarChar,200),
					new SqlParameter("@FieldManagement", SqlDbType.NVarChar,200),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,200),
					new SqlParameter("@All_id", SqlDbType.Int,4)};
            parameters[0].Value = model.CropType;
            parameters[1].Value = model.Varieties;
            parameters[2].Value = model.TargetYield;
            parameters[3].Value = model.urea;
            parameters[4].Value = model.An;
            parameters[5].Value = model.K;
            parameters[6].Value = model.OrganicManure;
            parameters[7].Value = model.Others;
            parameters[8].Value = model.IrrigationTimes;
            parameters[9].Value = model.SowingAmount;
            parameters[10].Value = model.SowingMethod;
            parameters[11].Value = model.WeedControl;
            parameters[12].Value = model.PestControl;
            parameters[13].Value = model.FieldManagement;
            parameters[14].Value = model.Remarks;
            parameters[15].Value = model.All_id;

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
        public bool Update(SoilNutrientSoft.Model.FarmlandMSug model, Dictionary<SqlConnection, SqlTransaction> MyDict)
		{
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FarmlandMSug set ");
                strSql.Append("CropType=@CropType,");
                strSql.Append("Varieties=@Varieties,");
                strSql.Append("TargetYield=@TargetYield,");
                strSql.Append("urea=@urea,");
                strSql.Append("An=@An,");
                strSql.Append("K=@K,");
                strSql.Append("OrganicManure=@OrganicManure,");
                strSql.Append("Others=@Others,");
                strSql.Append("IrrigationTimes=@IrrigationTimes,");
                strSql.Append("SowingAmount=@SowingAmount,");
                strSql.Append("SowingMethod=@SowingMethod,");
                strSql.Append("WeedControl=@WeedControl,");
                strSql.Append("PestControl=@PestControl,");
                strSql.Append("FieldManagement=@FieldManagement,");
                strSql.Append("Remarks=@Remarks,");
                strSql.Append("All_id=@All_id");
                strSql.Append(" where Id=@Id");
                SqlParameter[] parameters = {
					new SqlParameter("@CropType", SqlDbType.NVarChar,50),
					new SqlParameter("@Varieties", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetYield", SqlDbType.Float,8),
					new SqlParameter("@urea", SqlDbType.Float,8),
					new SqlParameter("@An", SqlDbType.Float,8),
					new SqlParameter("@K", SqlDbType.Float,8),
					new SqlParameter("@OrganicManure", SqlDbType.Float,8),
					new SqlParameter("@Others", SqlDbType.Float,8),
					new SqlParameter("@IrrigationTimes", SqlDbType.Int,4),
					new SqlParameter("@SowingAmount", SqlDbType.Float,8),
					new SqlParameter("@SowingMethod", SqlDbType.NVarChar,200),
					new SqlParameter("@WeedControl", SqlDbType.NVarChar,200),
					new SqlParameter("@PestControl", SqlDbType.NVarChar,200),
					new SqlParameter("@FieldManagement", SqlDbType.NVarChar,200),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,200),
					new SqlParameter("@All_id", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
                parameters[0].Value = model.CropType;
                parameters[1].Value = model.Varieties;
                parameters[2].Value = model.TargetYield;
                parameters[3].Value = model.urea;
                parameters[4].Value = model.An;
                parameters[5].Value = model.K;
                parameters[6].Value = model.OrganicManure;
                parameters[7].Value = model.Others;
                parameters[8].Value = model.IrrigationTimes;
                parameters[9].Value = model.SowingAmount;
                parameters[10].Value = model.SowingMethod;
                parameters[11].Value = model.WeedControl;
                parameters[12].Value = model.PestControl;
                parameters[13].Value = model.FieldManagement;
                parameters[14].Value = model.Remarks;
                parameters[15].Value = model.All_id;
                parameters[16].Value = model.Id;

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
                strSql.Append("delete from FarmlandMSug ");
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
			strSql.Append("delete from FarmlandMSug ");
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
		public SoilNutrientSoft.Model.FarmlandMSug GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,CropType,Varieties,TargetYield,urea,An,K,OrganicManure,Others,IrrigationTimes,SowingAmount,SowingMethod,WeedControl,PestControl,FieldManagement,Remarks,All_id from FarmlandMSug ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			SoilNutrientSoft.Model.FarmlandMSug model=new SoilNutrientSoft.Model.FarmlandMSug();
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
		public SoilNutrientSoft.Model.FarmlandMSug DataRowToModel(DataRow row)
		{
			SoilNutrientSoft.Model.FarmlandMSug model=new SoilNutrientSoft.Model.FarmlandMSug();
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
				if(row["TargetYield"]!=null && row["TargetYield"].ToString()!="")
				{
					model.TargetYield=decimal.Parse(row["TargetYield"].ToString());
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
				if(row["OrganicManure"]!=null && row["OrganicManure"].ToString()!="")
				{
					model.OrganicManure=decimal.Parse(row["OrganicManure"].ToString());
				}
				if(row["Others"]!=null && row["Others"].ToString()!="")
				{
					model.Others=decimal.Parse(row["Others"].ToString());
				}
				if(row["IrrigationTimes"]!=null && row["IrrigationTimes"].ToString()!="")
				{
					model.IrrigationTimes=int.Parse(row["IrrigationTimes"].ToString());
				}
				if(row["SowingAmount"]!=null && row["SowingAmount"].ToString()!="")
				{
					model.SowingAmount=decimal.Parse(row["SowingAmount"].ToString());
				}
				if(row["SowingMethod"]!=null)
				{
					model.SowingMethod=row["SowingMethod"].ToString();
				}
				if(row["WeedControl"]!=null)
				{
					model.WeedControl=row["WeedControl"].ToString();
				}
				if(row["PestControl"]!=null)
				{
					model.PestControl=row["PestControl"].ToString();
				}
				if(row["FieldManagement"]!=null)
				{
					model.FieldManagement=row["FieldManagement"].ToString();
				}
				if(row["Remarks"]!=null)
				{
					model.Remarks=row["Remarks"].ToString();
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
			strSql.Append("select Id,CropType,Varieties,TargetYield,urea,An,K,OrganicManure,Others,IrrigationTimes,SowingAmount,SowingMethod,WeedControl,PestControl,FieldManagement,Remarks,All_id ");
			strSql.Append(" FROM FarmlandMSug ");
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
			strSql.Append(" Id,CropType,Varieties,TargetYield,urea,An,K,OrganicManure,Others,IrrigationTimes,SowingAmount,SowingMethod,WeedControl,PestControl,FieldManagement,Remarks,All_id ");
			strSql.Append(" FROM FarmlandMSug ");
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
			strSql.Append("select count(1) FROM FarmlandMSug ");
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
			strSql.Append(")AS Row, T.*  from FarmlandMSug T ");
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
			parameters[0].Value = "FarmlandMSug";
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
        public int Add(SoilNutrientSoft.Model.FarmlandMSug model, Dictionary<SqlConnection, SqlTransaction> MyDict, params SqlParameter[] cmdParms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FarmlandMSug(");
            strSql.Append("CropType,Varieties,TargetYield,urea,An,K,OrganicManure,Others,IrrigationTimes,SowingAmount,SowingMethod,WeedControl,PestControl,FieldManagement,Remarks,All_id)");
            strSql.Append(" values (");
            strSql.Append("@CropType,@Varieties,@TargetYield,@urea,@An,@K,@OrganicManure,@Others,@IrrigationTimes,@SowingAmount,@SowingMethod,@WeedControl,@PestControl,@FieldManagement,@Remarks,@All_id)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CropType", SqlDbType.NVarChar,50),
					new SqlParameter("@Varieties", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetYield", SqlDbType.Float,8),
					new SqlParameter("@urea", SqlDbType.Float,8),
					new SqlParameter("@An", SqlDbType.Float,8),
					new SqlParameter("@K", SqlDbType.Float,8),
					new SqlParameter("@OrganicManure", SqlDbType.Float,8),
					new SqlParameter("@Others", SqlDbType.Float,8),
					new SqlParameter("@IrrigationTimes", SqlDbType.Int,4),
					new SqlParameter("@SowingAmount", SqlDbType.Float,8),
					new SqlParameter("@SowingMethod", SqlDbType.NVarChar,200),
					new SqlParameter("@WeedControl", SqlDbType.NVarChar,200),
					new SqlParameter("@PestControl", SqlDbType.NVarChar,200),
					new SqlParameter("@FieldManagement", SqlDbType.NVarChar,200),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,200),
					new SqlParameter("@All_id", SqlDbType.Int,4)};
            parameters[0].Value = model.CropType;
            parameters[1].Value = model.Varieties;
            parameters[2].Value = model.TargetYield;
            parameters[3].Value = model.urea;
            parameters[4].Value = model.An;
            parameters[5].Value = model.K;
            parameters[6].Value = model.OrganicManure;
            parameters[7].Value = model.Others;
            parameters[8].Value = model.IrrigationTimes;
            parameters[9].Value = model.SowingAmount;
            parameters[10].Value = model.SowingMethod;
            parameters[11].Value = model.WeedControl;
            parameters[12].Value = model.PestControl;
            parameters[13].Value = model.FieldManagement;
            parameters[14].Value = model.Remarks;
            parameters[15].Value = model.All_id;

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


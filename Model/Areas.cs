using System;
namespace SoilNutrientSoft.Model
{
	/// <summary>
	/// Areas:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Areas
	{
		public Areas()
		{}
		#region Model
		private int _id;
		private int _areaid;
		private string _areaname;
		private int _areapid;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int AreaID
		{
			set{ _areaid=value;}
			get{return _areaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaName
		{
			set{ _areaname=value;}
			get{return _areaname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int AreaPID
		{
			set{ _areapid=value;}
			get{return _areapid;}
		}
		#endregion Model

	}
}


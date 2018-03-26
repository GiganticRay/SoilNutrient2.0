using System;
namespace SoilNutrientSoft.Model
{
	/// <summary>
	/// FarmlandMeg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FarmlandMeg
	{
		public FarmlandMeg()
		{}
		#region Model
		private int _id;
		private string _city;
		private string _county;
		private string _town;
		private string _village;
		private string _sample_name;
		private decimal _lon;
		private decimal _lat;
		private string _name_of_householder;
		private string _phone_number;
		private int? _irrigation_conditions;
		private decimal? _acreage;
		private int? _fertility;
		private string _weeds;
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
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string County
		{
			set{ _county=value;}
			get{return _county;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Town
		{
			set{ _town=value;}
			get{return _town;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Village
		{
			set{ _village=value;}
			get{return _village;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sample_name
		{
			set{ _sample_name=value;}
			get{return _sample_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Lon
		{
			set{ _lon=value;}
			get{return _lon;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Lat
		{
			set{ _lat=value;}
			get{return _lat;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name_of_householder
		{
			set{ _name_of_householder=value;}
			get{return _name_of_householder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone_number
		{
			set{ _phone_number=value;}
			get{return _phone_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Irrigation_Conditions
		{
			set{ _irrigation_conditions=value;}
			get{return _irrigation_conditions;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Acreage
		{
			set{ _acreage=value;}
			get{return _acreage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Fertility
		{
			set{ _fertility=value;}
			get{return _fertility;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Weeds
		{
			set{ _weeds=value;}
			get{return _weeds;}
		}
		#endregion Model

	}
}


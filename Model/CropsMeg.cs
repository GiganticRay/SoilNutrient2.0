using System;
namespace SoilNutrientSoft.Model
{
	/// <summary>
	/// CropsMeg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CropsMeg
	{
		public CropsMeg()
		{}
		#region Model
		private int _id;
		private string _croptype;
		private string _varieties;
		private decimal? _yield;
		private decimal? _urea;
		private decimal? _an;
		private decimal? _k;
		private decimal? _organic_manure;
		private decimal? _others;
		private int? _irrigation_times;
		private int _all_id;
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
		public string CropType
		{
			set{ _croptype=value;}
			get{return _croptype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Varieties
		{
			set{ _varieties=value;}
			get{return _varieties;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Yield
		{
			set{ _yield=value;}
			get{return _yield;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? urea
		{
			set{ _urea=value;}
			get{return _urea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? An
		{
			set{ _an=value;}
			get{return _an;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? K
		{
			set{ _k=value;}
			get{return _k;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Organic_manure
		{
			set{ _organic_manure=value;}
			get{return _organic_manure;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Others
		{
			set{ _others=value;}
			get{return _others;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Irrigation_times
		{
			set{ _irrigation_times=value;}
			get{return _irrigation_times;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int All_id
		{
			set{ _all_id=value;}
			get{return _all_id;}
		}
		#endregion Model

	}
}


using System;
namespace SoilNutrientSoft.Model
{
	/// <summary>
	/// SoilNutrientMeg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SoilNutrientMeg
	{
		public SoilNutrientMeg()
		{}
		#region Model
		private int _id;
		private decimal? _n;
		private decimal? _p;
		private decimal? _k;
		private decimal? _hydrolyticn;
		private decimal? _quickp;
		private decimal? _quick;
		private decimal? _organicmatter;
		private decimal? _ph;
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
		public decimal? N
		{
			set{ _n=value;}
			get{return _n;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? P
		{
			set{ _p=value;}
			get{return _p;}
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
		public decimal? HydrolyticN
		{
			set{ _hydrolyticn=value;}
			get{return _hydrolyticn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? QuickP
		{
			set{ _quickp=value;}
			get{return _quickp;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? QUicK
		{
			set{ _quick=value;}
			get{return _quick;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OrganicMatter
		{
			set{ _organicmatter=value;}
			get{return _organicmatter;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PH
		{
			set{ _ph=value;}
			get{return _ph;}
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


using System;
namespace SoilNutrientSoft.Model
{
	/// <summary>
	/// FarmlandMSug:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FarmlandMSug
	{
		public FarmlandMSug()
		{}
		#region Model
		private int _id;
		private string _croptype;
		private string _varieties;
		private decimal? _targetyield;
		private decimal? _urea;
		private decimal? _an;
		private decimal? _k;
		private decimal? _organicmanure;
		private decimal? _others;
		private int? _irrigationtimes;
		private decimal? _sowingamount;
		private string _sowingmethod;
		private string _weedcontrol;
		private string _pestcontrol;
		private string _fieldmanagement;
		private string _remarks;
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
		public decimal? TargetYield
		{
			set{ _targetyield=value;}
			get{return _targetyield;}
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
		public decimal? OrganicManure
		{
			set{ _organicmanure=value;}
			get{return _organicmanure;}
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
		public int? IrrigationTimes
		{
			set{ _irrigationtimes=value;}
			get{return _irrigationtimes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SowingAmount
		{
			set{ _sowingamount=value;}
			get{return _sowingamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SowingMethod
		{
			set{ _sowingmethod=value;}
			get{return _sowingmethod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WeedControl
		{
			set{ _weedcontrol=value;}
			get{return _weedcontrol;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PestControl
		{
			set{ _pestcontrol=value;}
			get{return _pestcontrol;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FieldManagement
		{
			set{ _fieldmanagement=value;}
			get{return _fieldmanagement;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
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


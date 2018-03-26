using System;
namespace SoilNutrientSoft.Model
{
	/// <summary>
	/// Picture:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Picture
	{
		public Picture()
		{}
		#region Model
		private int _id;
		private string _picturepath;
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
		public string picturePath
		{
			set{ _picturepath=value;}
			get{return _picturepath;}
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


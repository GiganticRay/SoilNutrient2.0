using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoilNutrientSoft.Model
{
    /// <summary>
    /// UserInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class UserInfo
    {
        public UserInfo()
        { }
        #region Model
        private int _id;
        private string _userid;
        private string _userpassword;
        private string _guidlicense;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserPassword
        {
            set { _userpassword = value; }
            get { return _userpassword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GuidLicense
        {
            set { _guidlicense = value; }
            get { return _guidlicense; }
        }
        #endregion Model

    }
}

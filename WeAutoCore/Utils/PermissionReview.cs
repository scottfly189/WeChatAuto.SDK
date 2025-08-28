using System.ComponentModel;
using WxAutoCore.Assets;
using WxAutoCore.Enums;

namespace WxAutoCore.Utils
{
    internal static class PermissionReview
    {
        private static UserType _userType = UserType.None;
        private static void InitData(string apiKey)
        {
            if (_userType == UserType.None)
            {
                _userType = Review(apiKey);
            }
        }
        /// <summary>
        /// 是否是未付费用户
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static bool IsNoPay(string apiKey)
        {
            InitData(apiKey);
            return _userType == UserType.NoPay;
        }
        /// <summary>
        /// 是否是付费用户
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static bool IsPro(string apiKey)
        {
            InitData(apiKey);
            return _userType == UserType.Pro;
        }
        /// <summary>
        /// 是否是企业用户
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static bool IsEnt(string apiKey)
        {
            InitData(apiKey);
            return _userType == UserType.Ent;
        }
        /// <summary>
        /// 是否是测试用户
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static bool IsTest(string apiKey)
        {
            InitData(apiKey);
            return _userType == UserType.Test;
        }
        private static UserType Review(string apiKey)
        {
            _userType = UserType.NoPay;
            if (Source.ProSourceList.Contains(apiKey))
            {
                _userType = UserType.Pro;
            }
            else if (Source.ExtSourceList.Contains(apiKey))
            {
                _userType = UserType.Ent;
            }
            else if (Source.TestSourceList.Contains(apiKey))
            {
                _userType = UserType.Test;
            }
            return _userType;
        }
    }
}
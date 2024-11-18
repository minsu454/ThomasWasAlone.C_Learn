using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.EnumExtensions
{
    public static class EnumExtensions
    {
        private static readonly Dictionary<Enum, string> enumToStringDic = new Dictionary<Enum, string>();      //enum타입 string 변환 저장해 놓는 Dictionary

        /// <summary>
        /// enum을 string으로 변환 함수
        /// </summary>
        public static string EnumToString<T>(this T type) where T : Enum
        {
            if (!enumToStringDic.TryGetValue(type, out string value))
            {
                value = type.ToString();
                enumToStringDic[type] = value;
            }

            return value;
        }
    }
}

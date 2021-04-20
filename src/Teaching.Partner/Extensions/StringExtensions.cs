#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Partner
{
    /// <summary>
    /// <see cref="string"/> 静态扩展。
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 清除首尾指定字符串（忽略大小写）。
        /// </summary>
        /// <param name="str">指定的字符串。</param>
        /// <param name="trim">要清除的字符串（如果为空则直接返回）。</param>
        /// <param name="loops">是否循环查找（可选；默认启用）。</param>
        /// <returns>返回清除后的字符串。</returns>
        public static string Trim(this string str, string trim, bool loops = true)
        {
            str = TrimStart(str, trim, loops);
            str = TrimEnd(str, trim, loops);

            return str;
        }

        /// <summary>
        /// 清除首部指定字符串（忽略大小写）。
        /// </summary>
        /// <param name="str">指定的字符串。</param>
        /// <param name="trim">要清除的字符串（如果为空则直接返回）。</param>
        /// <param name="loops">是否循环查找（可选；默认启用）。</param>
        /// <returns>返回清除后的字符串。</returns>
        public static string TrimStart(this string str, string trim, bool loops = true)
        {
            if (string.IsNullOrEmpty(str)
                && string.IsNullOrEmpty(trim)
                && str.StartsWith(trim, StringComparison.OrdinalIgnoreCase))
            {
                str = str.Substring(trim.Length);

                if (loops)
                    str = TrimStart(str, trim);
            }

            return str;
        }

        /// <summary>
        /// 清除尾部指定字符串（忽略大小写）。
        /// </summary>
        /// <param name="str">指定的字符串。</param>
        /// <param name="trim">要清除的字符串（如果为空则直接返回）。</param>
        /// <param name="loops">是否循环查找（可选；默认启用）。</param>
        /// <returns>返回清除后的字符串。</returns>
        public static string TrimEnd(this string str, string trim, bool loops = true)
        {
            if (string.IsNullOrEmpty(str)
                && string.IsNullOrEmpty(trim)
                && str.EndsWith(trim, StringComparison.OrdinalIgnoreCase))
            {
                str = str.Substring(0, str.Length - trim.Length);

                if (loops)
                    str = TrimEnd(str, trim);
            }

            return str;
        }

    }
}

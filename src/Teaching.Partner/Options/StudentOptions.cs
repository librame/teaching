#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Text;

namespace Teaching.Partner
{
    /// <summary>
    /// 学生选项。
    /// </summary>
    public class StudentOptions
    {
        /// <summary>
        /// 编号。
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 头像。
        /// </summary>
        public string? Portrait { get; set; }

        ///// <summary>
        ///// 性别。
        ///// </summary>
        //public bool Gender { get; set; }

        ///// <summary>
        ///// 电话。
        ///// </summary>
        //public string? Phone { get; set; }

        ///// <summary>
        ///// 邮箱。
        ///// </summary>
        //public string? Email { get; set; }

        ///// <summary>
        ///// 身份证。
        ///// </summary>
        //public string? IdCard { get; set; }

        ///// <summary>
        ///// 地址。
        ///// </summary>
        //public string? Address { get; set; }


        /// <summary>
        /// 从字符串还原。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回 <see cref="StudentOptions"/>。</returns>
        public static StudentOptions? FromString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            var pair = str.Split(' ');
            return new StudentOptions
            {
                Id = pair[0],
                Name = pair[1]
            };
        }


        /// <summary>
        /// 转换为字符串（默认显示掩码）。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => ToString(isShowMask: false);

        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <param name="isShowMask">是否显示掩码。</param>
        /// <returns>返回字符串。</returns>
        public virtual string ToString(bool isShowMask)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Id))
            {
                if (isShowMask)
                    sb.AppendFormat("{0}**", Id.Substring(0, Id.Length - 2));
                else
                    sb.Append(Id);

                sb.Append(' ');
            }

            if (!string.IsNullOrEmpty(Name))
            {
                if (isShowMask)
                    sb.AppendFormat("{0}**", Name.Substring(0, 1));
                else
                    sb.Append(Name);
            }

            return sb.ToString();
        }

    }
}

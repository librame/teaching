#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

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

        /// <summary>
        /// 性别。
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// 电话。
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱。
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 身份证。
        /// </summary>
        public string? IdCard { get; set; }

        /// <summary>
        /// 地址。
        /// </summary>
        public string? Address { get; set; }
    }
}

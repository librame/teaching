#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace Teaching.Partner
{
    /// <summary>
    /// 班级选项。
    /// </summary>
    public class ClassOptions
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
        /// 年级。
        /// </summary>
        public GradeOptions? Grade { get; set; }

        /// <summary>
        /// 学生列表。
        /// </summary>
        public List<StudentOptions>? Students { get; set; }
    }
}

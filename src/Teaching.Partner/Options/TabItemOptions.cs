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
    /// 标签项选项。
    /// </summary>
    public class TabItemOptions
    {
        /// <summary>
        /// 标题外距。
        /// </summary>
        public double HeaderMargin { get; set; }

        /// <summary>
        /// 数据网格内距。
        /// </summary>
        public string? DataGridPadding { get; set; }

        /// <summary>
        /// 序号列可见性。
        /// </summary>
        public bool SerialColumnVisibility { get; set; }

        /// <summary>
        /// 显示的学生列集合。
        /// </summary>
        public List<StudentColumnOptions>? StudentColumns { get; set; } 
    }
}

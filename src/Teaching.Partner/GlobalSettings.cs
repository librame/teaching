#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.IO;
using System.Text;

namespace Teaching.Partner
{
    /// <summary>
    /// 全局首选项。
    /// </summary>
    public static class GlobalSettings
    {
        /// <summary>
        /// 当前字符编码（默认为 UTF-8）。
        /// </summary>
        public static readonly Encoding CurrentEncoding
            = Encoding.UTF8;

        /// <summary>
        /// 当前目录。
        /// </summary>
        public static readonly string CurrentDirectory
            = Directory.GetCurrentDirectory();

        /// <summary>
        /// 配置目录。
        /// </summary>
        public static readonly string ConfigDirectory
            = Path.Combine(CurrentDirectory, "config");


        /// <summary>
        /// 班级配置路径。
        /// </summary>
        public static readonly string ClassesConfigPath
            = Path.Combine(CurrentDirectory, "config/classes.json");

        /// <summary>
        /// 样式配置路径。
        /// </summary>
        public static readonly string StyleConfigPath
            = Path.Combine(CurrentDirectory, "config/style.json");

    }
}

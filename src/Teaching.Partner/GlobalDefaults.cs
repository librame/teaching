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
using System.IO;
using System.Text;

namespace Teaching.Partner
{
    /// <summary>
    /// 全局默认值。
    /// </summary>
    public static class GlobalDefaults
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
        /// 规则配置目录。
        /// </summary>
        public static readonly string RulesConfigDirectory
            = Path.Combine(ConfigDirectory, "rules");

        /// <summary>
        /// 点名配置目录。
        /// </summary>
        public static readonly string CalledNamesConfigDirectory
            = Path.Combine(ConfigDirectory, "called_names");


        /// <summary>
        /// 班级配置路径。
        /// </summary>
        public static readonly string ClassesConfigPath
            = Path.Combine(ConfigDirectory, "classes.json");

        /// <summary>
        /// 设定配置路径。
        /// </summary>
        public static readonly string SettingsConfigPath
            = Path.Combine(ConfigDirectory, "settings.json");


        /// <summary>
        /// 获取点名配置路径。
        /// </summary>
        /// <param name="options">给定的 <see cref="ClassOptions"/>。</param>
        /// <returns>返回字符串。</returns>
        public static string GetCalledNamesConfigPath(ClassOptions options)
        {
            return $"{options?.Grade?.Id}{options?.Id}.dat"
                .SetBasePath(CalledNamesConfigDirectory);
        }

    }
}

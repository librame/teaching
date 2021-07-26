#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using RulesEngine.Interfaces;
using System.Linq;
using System.Text.Json.Serialization;

namespace Teaching.Partner
{
    /// <summary>
    /// 规则选项。
    /// </summary>
    public class RuleOptions
    {
        /// <summary>
        /// 规则名称。
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 文件名称。
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// 支持的文件扩展名集合。
        /// </summary>
        public string? SupportedFileExtensions { get; set; }

        /// <summary>
        /// 规则引擎对象。
        /// </summary>
        [JsonIgnore]
        public IRulesEngine? Engine { get; set; }


        /// <summary>
        /// 是否为支持的文件扩展名。
        /// </summary>
        /// <param name="extension">给定的文件扩展名。</param>
        /// <returns>返回布尔值。</returns>
        public bool IsSupportedFileExtension(string extension)
#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
            => (bool)SupportedFileExtensions?.Split(',').Contains(extension);
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。

    }
}

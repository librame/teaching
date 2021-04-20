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

namespace Teaching.Partner
{
    public class RulesOptions
    {
        public string? Name { get; set; }

        public string? ConfigFile { get; set; }

        public string? SupportedFileExtensions { get; set; }

        public IRulesEngine? Rule { get; set; }


        public bool IsSupportedFileExtension(string extension)
#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
            => (bool)SupportedFileExtensions?.Split(',').Contains(extension);
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。

    }
}

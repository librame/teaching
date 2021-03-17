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

namespace Teaching.Partner
{
    /// <summary>
    /// 全局实用工具。
    /// </summary>
    public static class GlobalUtility
    {
        public static Uri ToImageUri(string? imagePath)
            => new Uri(StandardizeSource(imagePath));


        public static string StandardizeSource(string? source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));

            var baseDirectory = GlobalSettings.CurrentDirectory;

            // 支持相对路径（要求以“/”路径分隔符为前导符）
            if (source.StartsWith("./") || !source.StartsWith(baseDirectory))
                source = Path.Combine(baseDirectory, source);

            if (Path.IsPathFullyQualified(source))
                source = $"file:///{source.Replace("\\", "/")}";

            return source;
        }

    }
}

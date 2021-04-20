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
    /// <see cref="Path"/> 静态扩展。
    /// </summary>
    public static class PathExtensions
    {
        /// <summary>
        /// 获取文件信息集合。
        /// </summary>
        /// <param name="directory">给定的目录。</param>
        /// <param name="searchPattern">给定查找文件的规则（可选；默认获取所有文件）。</param>
        /// <returns>返回 <see cref="FileInfo"/> 数组。</returns>
        public static FileInfo[] GetFileInfos(this string? directory, string searchPattern = "*.*")
        {
#pragma warning disable CS8604 // 可能的 null 引用参数。
            var dir = new DirectoryInfo(directory);
#pragma warning restore CS8604 // 可能的 null 引用参数。

            return dir.GetFiles(searchPattern, SearchOption.AllDirectories);
        }


        /// <summary>
        /// 设置基础路径。
        /// </summary>
        /// <param name="relativePath">给定的相对路径。</param>
        /// <param name="basePath">给定的基础路径（可选；默认使用当前目录）。</param>
        /// <returns>返回路径字符串。</returns>
        public static string SetBasePath(this string? relativePath, string? basePath = null)
        {
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentNullException(nameof(relativePath));

            if (string.IsNullOrEmpty(basePath))
                basePath = GlobalSettings.CurrentDirectory;

            if (relativePath.StartsWith("./") || !relativePath.StartsWith(basePath))
                return Path.Combine(basePath, relativePath);

            return relativePath;
        }


        /// <summary>
        /// 转换为 URI。
        /// </summary>
        /// <param name="relativePath">给定的相对路径。</param>
        /// <returns>返回 <see cref="Uri"/>。</returns>
        public static Uri ToUri(this string? relativePath)
            => new Uri(relativePath.ToUncPath());

        /// <summary>
        /// 转换为 UNC 路径。
        /// </summary>
        /// <param name="relativePath">给定的相对路径。</param>
        /// <param name="basePath">给定的基础路径（可选；默认使用当前目录）。</param>
        /// <returns>返回 UNC 路径字符串。</returns>
        public static string ToUncPath(this string? relativePath, string? basePath = null)
        {
            relativePath = relativePath.SetBasePath(basePath);

            if (Path.IsPathFullyQualified(relativePath))
            {
                relativePath = relativePath.Replace("\\", "/");
                relativePath = $"file:///{relativePath}";
            }

            return relativePath;
        }


        /// <summary>
        /// 尝试创建指定文件路径的目录。
        /// </summary>
        /// <param name="filePath">给定的文件路径。</param>
        /// <returns>返回是否成功创建目录的布尔值。</returns>
        public static bool TryCreateDirectory(this string? filePath)
            => filePath.TryCreateDirectory(out _);

        /// <summary>
        /// 尝试创建指定文件路径的目录。
        /// </summary>
        /// <param name="filePath">给定的文件路径。</param>
        /// <param name="directoryInfo">输出创建的 <see cref="DirectoryInfo"/>。</param>
        /// <returns>返回是否成功创建目录的布尔值。</returns>
        public static bool TryCreateDirectory(this string? filePath, out DirectoryInfo? directoryInfo)
        {
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                directoryInfo = Directory.CreateDirectory(directory);
                return true;
            }

            directoryInfo = null;
            return false;
        }

    }
}

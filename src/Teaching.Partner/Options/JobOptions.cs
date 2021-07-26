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
using System.Linq;

namespace Teaching.Partner
{
    /// <summary>
    /// 作业选项。
    /// </summary>
    public class JobOptions
    {
        /// <summary>
        /// 检查有效性。
        /// </summary>
        public string? CheckValidity { get; set; }

        /// <summary>
        /// 压缩文件。
        /// </summary>
        public string? CompressedFile { get; set; }

        /// <summary>
        /// 压缩文件扩展名集合。
        /// </summary>
        public string? CompressedFileExtensions { get; set; }


        /// <summary>
        /// 是否为支持的压缩文件。
        /// </summary>
        /// <param name="extension">给定的文件扩展名。</param>
        /// <returns>返回布尔值。</returns>
        public bool IsCompressedFileExtension(string extension)
#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
            => (bool)CompressedFileExtensions?.Split(',').Contains(extension);
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。


        /// <summary>
        /// 解压文件。
        /// </summary>
        /// <param name="compressFile">给定的压缩文件。</param>
        /// <returns>返回解压的文件数组。</returns>
        public FileInfo[]? UncompressFiles(FileInfo? compressFile)
        {
            if (compressFile is null || !compressFile.Exists || string.IsNullOrEmpty(CompressedFile))
                return null;

            var uncompressFilePath = compressFile.FullName.TrimEnd(compressFile.Extension);
            // 解压文件
            if (!uncompressFilePath.UncompressProcess(compressFile.FullName, CompressedFile))
                return null;

            return uncompressFilePath.GetFileInfos();
        }

    }
}

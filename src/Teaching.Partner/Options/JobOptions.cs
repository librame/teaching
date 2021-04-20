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
    public class JobOptions
    {
        public string? CheckValidity { get; set; }

        public string? CompressedFile { get; set; }

        public string? CompressedFileExtensions { get; set; }


        public bool IsCompressedFileExtension(string extension)
#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
            => (bool)CompressedFileExtensions?.Split(',').Contains(extension);
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。


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

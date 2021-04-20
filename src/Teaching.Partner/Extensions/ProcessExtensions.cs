#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Diagnostics;

namespace Teaching.Partner
{
    /// <summary>
    /// <see cref="Process"/> 静态扩展。
    /// </summary>
    public static class ProcessExtensions
    {
        /// <summary>
        /// 压缩文件。
        /// </summary>
        /// <param name="targetFilePath">给定要压缩的文件或文件夹路径。</param>
        /// <param name="compressFilePath">给定要生成的压缩文件路径。</param>
        /// <param name="exeFileName">给定的解压程序文件名。</param>
        /// <param name="password">给定的压缩密码（可选；默认为空）。</param>
        /// <param name="isCover">是否覆盖已存在的文件（可选；默认覆盖）。</param>
        /// <param name="autoCreateDirectory">自动创建解压目录（可选；默认启用）。</param>
        /// <returns>返回是否已成功压缩的布尔值。</returns>
        public static bool CompressProcess(this string targetFilePath, string compressFilePath,
            string exeFileName, string? password = null, bool isCover = true, bool autoCreateDirectory = true)
        {
            if (autoCreateDirectory)
                compressFilePath.TryCreateDirectory();

            string command;

            if (!string.IsNullOrEmpty(password))
            {
                // 解压加密文件
                if (isCover)
                {
                    // 覆盖已存在文件
                    command = string.Format(" a -ep1 -p{0} -o+ {1} {2} -r", password, compressFilePath, targetFilePath);
                }
                else
                {
                    // 不覆盖已存在文件
                    command = string.Format(" a -ep1 -p{0} -o- {1} {2} -r", password, compressFilePath, targetFilePath);
                }
            }
            else if (isCover)
            {
                command = string.Format(" a -ep1 -o+ {0} {1} -r", compressFilePath, targetFilePath);
            }
            else
            {
                command = string.Format(" a -ep1 -o- {0} {1} -r", compressFilePath, targetFilePath);
            }

            return exeFileName.ProcessByNoWindow(command);
        }

        /// <summary>
        /// 解压文件。
        /// </summary>
        /// <param name="uncompressFilePath">给定的解压文件路径。</param>
        /// <param name="compressFilePath">给定的压缩文件路径。</param>
        /// <param name="exeFileName">给定的解压程序文件名。</param>
        /// <param name="password">给定的压缩密码（可选；默认为空）。</param>
        /// <param name="isCover">是否覆盖已存在的文件（可选；默认覆盖）。</param>
        /// <param name="autoCreateDirectory">自动创建解压目录（可选；默认启用）。</param>
        /// <returns>返回是否已成功解压的布尔值。</returns>
        public static bool UncompressProcess(this string uncompressFilePath, string compressFilePath,
            string exeFileName, string? password = null, bool isCover = true, bool autoCreateDirectory = true)
        {
            if (autoCreateDirectory)
                uncompressFilePath.TryCreateDirectory();

            string command;

            if (!string.IsNullOrEmpty(password))
            {
                // 解压加密文件
                if (isCover)
                {
                    // 覆盖已存在文件
                    command = string.Format(" x -p{0} -o+ {1} {2} -y", password, compressFilePath, uncompressFilePath);
                }
                else
                {
                    // 不覆盖已存在文件
                    command = string.Format(" x -p{0} -o- {1} {2} -y", password, compressFilePath, uncompressFilePath);
                }
            }
            else if (isCover)
            {
                command = string.Format(" x -o+ {0} {1} -y", compressFilePath, uncompressFilePath);
            }
            else
            {
                command = string.Format(" x -o- {0} {1} -y", compressFilePath, uncompressFilePath);
            }

            return exeFileName.ProcessByNoWindow(command);
        }


        /// <summary>
        /// 使用后台进程调用程序处理命令。
        /// </summary>
        /// <param name="fileName">给定的程序文件名。</param>
        /// <param name="command">给定要处理的命令。</param>
        /// <returns>返回是否已成功处理的布尔值。</returns>
        public static bool ProcessByNoWindow(this string fileName, string command)
        {
            var p = new Process();

            p.StartInfo.FileName = fileName;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Arguments = command;

            p.Start();
            p.WaitForExit();

            if (p.ExitCode == 0)
            {
                // 正常执行
                p.Close();
                return true;
            }
            else
            {
                // 不正常执行
                p.Close();
                return false;
            }
        }

    }
}

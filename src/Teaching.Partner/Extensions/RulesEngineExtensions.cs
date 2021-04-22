#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using RulesEngine.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using static RulesEngine.Extensions.ListofRuleResultTreeExtension;

namespace Teaching.Partner
{
    /// <summary>
    /// <see cref="RulesEngine"/> 静态扩展。
    /// </summary>
    public static class RulesEngineExtensions
    {
        /// <summary>
        /// 阅卷计分。
        /// </summary>
        /// <param name="rule">给定的 <see cref="RuleOptions"/>。</param>
        /// <param name="job">给定的 <see cref="JobOptions"/>。</param>
        /// <param name="file">给定用于阅卷的 <see cref="FileInfo"/>。</param>
        /// <param name="onSuccessFunc">给定的成功方法（可选）。</param>
        /// <param name="onFailureFunc">给定的失败方法（可选）。</param>
        /// <returns>返回分数。</returns>
        public static int Score(this RuleOptions rule, JobOptions? job, FileInfo? file,
            OnSuccessFunc? onSuccessFunc = null, OnFailureFunc? onFailureFunc = null)
        {
            if (rule is null || job is null || file is null)
                return -1;

            var inputs = new List<string>();

            // 如果是压缩文件
            if (job.IsCompressedFileExtension(file.Extension))
            {
                // 得到解压文件集合
                var files = job.UncompressFiles(file);
                if (files is null || files.Length < 1)
                    throw new ArgumentException("压缩包文件列表为空");

                foreach (var item in files)
                {
                    if (rule.IsSupportedFileExtension(item.Extension))
                        inputs.Add(File.ReadAllText(item.FullName));
                }
            }
            else
            {
                if (rule.IsSupportedFileExtension(file.Extension))
                    inputs.Add(File.ReadAllText(file.FullName));
            }

            //执行规则
            var resultList = rule.Engine?.ExecuteAllRulesAsync(rule.Name, inputs).Result;
            if (resultList is null || resultList.Count < 1)
                return -1;

            //处理结果
            if (onSuccessFunc is not null)
                resultList.OnSuccess(onSuccessFunc);

            if (onFailureFunc is not null)
                resultList.OnFail(onFailureFunc);

            return 0; //resultList.Average(result => result.SuccessEvent);
        }

    }
}

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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static RulesEngine.Extensions.ListofRuleResultTreeExtension;

namespace Teaching.Partner
{
    /// <summary>
    /// <see cref="RulesEngine"/> 静态扩展。
    /// </summary>
    public static class RulesEngineExtensions
    {
        public static int ScoreRules(this FileInfo? file, List<RulesOptions>? rules,
            OnSuccessFunc? onSuccessFunc = null, OnFailureFunc? onFailureFunc = null)
        {
            if (file is null || rules is null || rules.Count < 1)
                return -1;

            var score = 0;

            foreach (var rule in rules)
            {
                if (!rule.IsSupportedFileExtension(file.Extension))
                    continue;

                var inputs = new object[0];

                //执行规则
                var resultList = rule?.Rule?.ExecuteAllRulesAsync("Discount", inputs).Result;
                if (resultList is null || resultList.Count < 1)
                    continue;

                //处理结果
                if (onSuccessFunc is not null)
                    resultList.OnSuccess(onSuccessFunc);

                if (onFailureFunc is not null)
                    resultList.OnFail(onFailureFunc);

                score += resultList.Average(result => result.SuccessEvent);
            }

            return score;
        }

    }
}

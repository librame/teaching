#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using RulesEngine.Models;
using System.Collections.Generic;

namespace Teaching.Partner
{
    /// <summary>
    /// APP 选项。
    /// </summary>
    public class AppOptions
    {
        public WindowOptions? Window { get; set; }

        public TabOptions? Tab { get; set; }

        public JobOptions? Job { get; set; }

        public List<RulesOptions>? Rules { get; set; }


        public void InitializeRules()
        {
            if (Rules is null || Rules.Count < 1)
                return;

            foreach (var rule in Rules)
            {
                var configFile = rule?.ConfigFile.SetBasePath(GlobalSettings.ConfigDirectory);

#pragma warning disable CS8604 // 可能的 null 引用参数。
                var workflowRules = configFile.ReadJson<List<WorkflowRules>>();
#pragma warning restore CS8604 // 可能的 null 引用参数。

                if (workflowRules is null || workflowRules.Count < 1)
                    continue;

                //初始化规则引擎
#pragma warning disable CS8602 // 解引用可能出现空引用。
                rule.Rule = new RulesEngine.RulesEngine(workflowRules.ToArray(), null);
#pragma warning restore CS8602 // 解引用可能出现空引用。
            }
        }

    }
}

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
    /// 设定选项。
    /// </summary>
    public class SettingsOptions
    {
        /// <summary>
        /// 窗口选项。
        /// </summary>
        public WindowOptions? Window { get; set; }

        /// <summary>
        /// 标签选项。
        /// </summary>
        public TabOptions? Tab { get; set; }

        /// <summary>
        /// 作业选项。
        /// </summary>
        public JobOptions? Job { get; set; }

        /// <summary>
        /// 初始化规则。
        /// </summary>
        public bool InitializationRules { get; set; }

        /// <summary>
        /// 规则选项列表。
        /// </summary>
        public List<RuleOptions>? Rules { get; set; }


        /// <summary>
        /// 初始化规则。
        /// </summary>
        public void InitializeRules()
        {
            if (Rules is null || Rules.Count < 1)
                return;

            foreach (var rule in Rules)
            {
                var file = rule?.FileName.SetBasePath(GlobalDefaults.RulesConfigDirectory);

#pragma warning disable CS8604 // 可能的 null 引用参数。
                var workflowRules = file.ReadJson<List<WorkflowRules>>();
#pragma warning restore CS8604 // 可能的 null 引用参数。

                if (workflowRules is null || workflowRules.Count < 1)
                    continue;

                // 初始化规则引擎（单个配置文件对应一个规则引擎实例）
#pragma warning disable CS8602 // 解引用可能出现空引用。
                rule.Engine = new RulesEngine.RulesEngine(workflowRules.ToArray(), null);
#pragma warning restore CS8602 // 解引用可能出现空引用。
            }
        }

    }
}

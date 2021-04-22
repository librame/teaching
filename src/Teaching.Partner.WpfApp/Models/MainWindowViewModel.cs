#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using MaterialDesignThemes.Wpf;
using System.Collections.Generic;

namespace Teaching.Partner.WpfApp
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ISnackbarMessageQueue? snackbarMessageQueue)
        {
            Classes = GlobalDefaults.ClassesConfigPath.ReadJson<List<ClassOptions>>();
            Settings = GlobalDefaults.SettingsConfigPath.ReadJson<SettingsOptions>();

#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
            if ((bool)Settings?.InitializationRules)
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。
            {
                Settings.InitializeRules();
            }
        }


        public List<ClassOptions>? Classes { get; }

        public List<CheckJobInfo>? JobInfos { get; set; }

        public SettingsOptions? Settings { get; }

        public string? CheckJobFolder { get; set; }

        public List<string> CalledNames { get; set; }
            = new List<string>();
    }
}

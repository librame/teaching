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
            Classes = GlobalSettings.ClassesConfigPath.ReadJson<List<ClassOptions>>();
            App = GlobalSettings.StyleConfigPath.ReadJson<AppOptions>();
            App?.InitializeRules();
        }


        public List<ClassOptions>? Classes { get; }

        public List<CheckJobInfo>? JobInfos { get; set; }

        public AppOptions? App { get; }

        public string? CheckJobFolder { get; set; }
    }
}

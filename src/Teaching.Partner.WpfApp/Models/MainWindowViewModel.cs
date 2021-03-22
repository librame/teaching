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
using System;
using System.Collections.Generic;

namespace Teaching.Partner.WpfApp
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ISnackbarMessageQueue? snackbarMessageQueue)
        {
            Classes = GlobalSettings.ClassesConfigPath.ReadJson<List<ClassOptions>>();
            Style = GlobalSettings.StyleConfigPath.ReadJson<StyleOptions>();
        }


        public List<ClassOptions>? Classes { get; }

        public StyleOptions? Style { get; }

    }
}

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
using System.IO;
using System.Linq;

namespace Teaching.Partner.WpfApp
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ISnackbarMessageQueue? snackbarMessageQueue)
        {
            Directory.CreateDirectory(GlobalDefaults.CalledNamesConfigDirectory);

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

        public List<StudentOptions>? CalledNames { get; set; }

        public SettingsOptions? Settings { get; }

        public string? CheckJobFolder { get; set; }


        public void SavedCalledNames(ClassOptions options)
        {
            if (CalledNames is null || CalledNames.Count < 1)
                return;

            var contents = CalledNames.Select(s => s.ToString(isShowMask: false));

            var filePath = GlobalDefaults.GetCalledNamesConfigPath(options);
            filePath.WriteAllLines(contents);
        }

        public List<StudentOptions> GetCalledNames(ClassOptions options)
        {
            var list = new List<StudentOptions>();

            var filePath = GlobalDefaults.GetCalledNamesConfigPath(options);
            var lines = filePath.ReadAllLines();
            if (lines.Length > 0)
            {
                foreach (var line in lines)
                {
                    var student = StudentOptions.FromString(line);
                    if (student is not null)
                        list.Add(student);
                }
            }

            return list;
        }

    }
}

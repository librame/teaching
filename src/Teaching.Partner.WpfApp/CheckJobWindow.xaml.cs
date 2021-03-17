using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Teaching.Partner.WpfApp
{
    /// <summary>
    /// CheckJobWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CheckJobWindow : Window
    {
        private readonly string _folder;
        private readonly ClassOptions? _options;
        private readonly StyleOptions? _style;


        public CheckJobWindow(string folder, ClassOptions? options, StyleOptions? style)
        {
            _folder = folder;
            _options = options;
            _style = style;

            InitializeComponent();

            InitializeWindow();
        }


        private void InitializeWindow()
        {
            tbkTitle.Text = $"当前正在检查“{_options?.Grade?.Name + _options?.Name}”作业：{_folder}";

            var dir = new DirectoryInfo(_folder);
            var files = dir.GetFiles("*.*", SearchOption.AllDirectories);

            var jobs = new List<CheckJobInfo>();

#pragma warning disable CS8602 // 解引用可能出现空引用。
            foreach (var student in _options?.Students)
#pragma warning restore CS8602 // 解引用可能出现空引用。
            {
                var job = new CheckJobInfo
                {
                    Id = student.Id,
                    Name = student.Name
                };

                foreach (var file in files)
                {
#pragma warning disable CS8604 // 可能的 null 引用参数。
                    job.IdValidity = file.Name.Contains(student.Id);
                    job.NameValidity = file.Name.Contains(student.Name);
#pragma warning restore CS8604 // 可能的 null 引用参数。

                    if (job.IsValid(_style?.Job))
                        break;
                }

                jobs.Add(job);
            }

            dgdResults.ItemsSource = jobs;
        }

    }
}

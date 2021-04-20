using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Teaching.Partner.WpfApp
{
    /// <summary>
    /// CheckJobWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CheckJobWindow : Window
    {
        private readonly ClassOptions? _options;
        private readonly MainWindowViewModel _viewModel;


        public CheckJobWindow(ClassOptions? options, MainWindowViewModel viewModel)
        {
            _options = options;
            _viewModel = viewModel;

            InitializeComponent();

            InitializeWindow();

#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
            FontSize = (double)viewModel.App?.Window?.FontSize;
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。
        }


        /// <summary>
        /// 检查作业信息列表。
        /// </summary>
        public List<CheckJobInfo>? JobInfos { get; private set; }


        private void InitializeWindow()
        {
            tbkTitle.Text = $"当前正在检查“{_options?.Grade?.Name + _options?.Name}”作业：{_viewModel.CheckJobFolder}";

            var files = _viewModel.CheckJobFolder.GetFileInfos();

            JobInfos = new List<CheckJobInfo>();

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

                    if (job.IsValid(_viewModel.App?.Job))
                    {
                        job.File = file;
                        break;
                    }
                }

                JobInfos.Add(job);
            }

            dgdResults.ItemsSource = JobInfos;
        }


        #region Grid Wrapper

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState != MouseButtonState.Pressed)
                return;

            DragMove();
        }

        #endregion


        #region Window

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

    }
}

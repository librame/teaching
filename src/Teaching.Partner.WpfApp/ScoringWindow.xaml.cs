using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Teaching.Partner.WpfApp
{
    /// <summary>
    /// ScoringWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScoringWindow : Window
    {
        private readonly ClassOptions? _options;
        private readonly MainWindowViewModel _viewModel;


        public ScoringWindow(ClassOptions? options, MainWindowViewModel viewModel)
        {
            _options = options;
            _viewModel = viewModel;

            InitializeComponent();

            InitializeWindow();

#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
            FontSize = (double)viewModel.App?.Window?.FontSize;
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。
        }


        private void InitializeWindow()
        {
            tbkTitle.Text = $"当前正在批阅“{_options?.Grade?.Name + _options?.Name}”试卷：{_viewModel.CheckJobFolder}";

            var scorings = new List<ScoringInfo>();

#pragma warning disable CS8602 // 解引用可能出现空引用。
            foreach (var jobInfo in _viewModel.JobInfos)
#pragma warning restore CS8602 // 解引用可能出现空引用。
            {
                if (jobInfo.File is null)
                    continue;

                int score = 0;

                // 如果是压缩文件
#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
                if ((bool)_viewModel.App?.Job?.IsCompressedFileExtension(jobInfo.File.Extension))
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。
                {
                    // 得到解压文件集合
                    var files = _viewModel.App.Job.UncompressFiles(jobInfo.File);
                    if (files is null || files.Length < 1)
                        continue;

                    foreach (var file in files)
                    {
                        score += file.ScoreRules(_viewModel.App.Rules);
                    }
                }
                else
                {
                    score = jobInfo.File.ScoreRules(_viewModel.App.Rules);
                }

                var scoring = new ScoringInfo
                {
                    Id = jobInfo.Id,
                    Name = jobInfo.Name,
                    Score = score
                };

                scorings.Add(scoring);
            }

            dgdResults.ItemsSource = scorings;
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

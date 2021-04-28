using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Teaching.Partner.WpfApp
{
    /// <summary>
    /// ScoringWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScoringWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly ClassOptions? _options;


        public ScoringWindow(MainWindowViewModel viewModel, ClassOptions? options)
        {
            _options = options;
            _viewModel = viewModel;

            InitializeComponent();

            InitializeWindow();

#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
            FontSize = (double)viewModel.Settings?.Window?.FontSize;
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。
        }


        private void InitializeWindow()
        {
            tbkTitle.Text = $"当前正在批阅“{_options?.Grade?.Name + _options?.Name}”试卷：{_viewModel.CheckJobFolder}";

            cbxRules.DataContext = _viewModel.Settings?.Rules?.Select(r => r.FileName);
        }

        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            var ruleName = cbxRules.SelectedItem as string;
            if (string.IsNullOrEmpty(ruleName))
                return;

            var rule = _viewModel.Settings?.Rules?.FirstOrDefault(r => r.FileName == ruleName);
            if (rule is null)
                return;

            var scorings = new List<ScoringInfo>();

#pragma warning disable CS8602 // 解引用可能出现空引用。
            foreach (var jobInfo in _viewModel.JobInfos)
#pragma warning restore CS8602 // 解引用可能出现空引用。
            {
                if (jobInfo.File is null)
                    continue;

                var score = rule.Score(_viewModel.Settings?.Job, jobInfo.File);

                scorings.Add(new ScoringInfo
                {
                    Id = jobInfo.Id,
                    Name = jobInfo.Name,
                    Score = score
                });
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

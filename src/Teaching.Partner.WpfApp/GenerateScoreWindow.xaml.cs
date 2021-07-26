using WpfDataGridTextColumn = MaterialDesignThemes.Wpf.DataGridTextColumn;
using DataBinding = System.Windows.Data.Binding;
using DataRelativeSource = System.Windows.Data.RelativeSource;
using DataRelativeSourceMode = System.Windows.Data.RelativeSourceMode;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace Teaching.Partner.WpfApp
{
    /// <summary>
    /// ScoringWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GenerateScoreWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly ClassOptions? _options;

        private List<SubjectScoreOptions>? _currentScores;


        public GenerateScoreWindow(MainWindowViewModel viewModel, ClassOptions? options)
        {
            _options = options;
            _viewModel = viewModel;

            InitializeComponent();

            var window = _viewModel.Settings?.Window!;
            Width = (double)window.Width;
            Height = (double)window.Height;
            FontSize = (double)window.FontSize;
        }


        private void AddDataGridColumns()
        {
            dgdResults.Columns.Add(new WpfDataGridTextColumn()
            {
                Header = "序号",
                Binding = new DataBinding("Header")
                {
                    RelativeSource = new DataRelativeSource(DataRelativeSourceMode.FindAncestor, typeof(DataGridRow), 1)
                }
            });

            dgdResults.Columns.Add(new WpfDataGridTextColumn()
            {
                Header = "学号",
                Binding = new DataBinding("Id")
            });

            dgdResults.Columns.Add(new WpfDataGridTextColumn()
            {
                Header = "姓名",
                Binding = new DataBinding("Name")
            });

            dgdResults.Columns.Add(new WpfDataGridTextColumn()
            {
                Header = "分级",
                Binding = new DataBinding("Level")
            });

            dgdResults.Columns.Add(new WpfDataGridTextColumn()
            {
                Header = "平时成绩",
                Binding = new DataBinding("NormalScore")
            });

            dgdResults.Columns.Add(new WpfDataGridTextColumn()
            {
                Header = "期末成绩",
                Binding = new DataBinding("FinalScore")
            });
        }


        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            // 获取难度系数
            double difficulty = 100;

            if (!string.IsNullOrEmpty(cbxDifficulty.Text))
                difficulty = double.Parse(cbxDifficulty.Text);

            _currentScores = _options?.Students?.GenerateScores(difficulty);

            dgdResults.ItemsSource = _currentScores;
            dgdResults.IsReadOnly = true;
            dgdResults.AutoGenerateColumns = false;

            dgdResults.LoadingRow += DgdResults_LoadingRow;

            if (dgdResults.Columns.Count <= 0)
                AddDataGridColumns();

            btnGenerate.IsEnabled = false;
            btnRefresh.IsEnabled = true;
            btnExport.IsEnabled = true;
        }

        private void DgdResults_LoadingRow(object? sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // 获取难度系数
            double difficulty = 100;

            if (!string.IsNullOrEmpty(cbxDifficulty.Text))
                difficulty = double.Parse(cbxDifficulty.Text);

            if (_currentScores == null)
                _currentScores = _options?.Students?.GenerateScores(difficulty);
            else
                _currentScores = _currentScores.RefreshScores(difficulty);

            dgdResults.ItemsSource = null;
            dgdResults.ItemsSource = _currentScores;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //var className = _options?.Grade?.Id + _options?.Id;
            var classTitle = _options?.Grade?.Name + _options?.Name;

            var sfd = new SaveFileDialog();
            sfd.Title = "请选择要导出的 Excel 文件保存路径";
            sfd.DefaultExt = ".csv";
            sfd.FileName = classTitle;
            sfd.Filter = $"Microsoft Excel 工作表|*{sfd.DefaultExt}";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                NPOIHelper.ExportCSV(_currentScores!, sfd.FileName);
            }
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

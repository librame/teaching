using MaterialDesignThemes.Wpf;
using WpfDataGridTextColumn = MaterialDesignThemes.Wpf.DataGridTextColumn;
using DataBinding = System.Windows.Data.Binding;
using DataRelativeSource = System.Windows.Data.RelativeSource;
using DataRelativeSourceMode = System.Windows.Data.RelativeSourceMode;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace Teaching.Partner.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Snackbar? Snackbar;

        private readonly double _defaultWidth;
        private readonly double _defaultHeight;

        private double _lastLeft;
        private double _lastTop;

        private MainWindowViewModel _viewModel;


        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainWindowViewModel(MainSnackbar.MessageQueue);

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2500);
            })
            .ContinueWith(t =>
            {
                MainSnackbar.MessageQueue?.Enqueue("欢迎使用教学助手");
            },
            TaskScheduler.FromCurrentSynchronizationContext());

            BindClasses();

            DataContext = _viewModel;
            Snackbar = MainSnackbar;

            var window = _viewModel.Settings?.Window!;
            _defaultWidth = Width = (double)window.Width;
            _defaultHeight = Height = (double)window.Height;
            FontSize = (double)window.FontSize;
        }


        private void BindClasses()
        {
            if (_viewModel.Classes is null)
                return;

            foreach (var c in _viewModel.Classes)
            {
                var item = new TabItem();
                
                item.Header = BindHeader(c);
                item.Content = BindContent(c);
                item.Tag = c;

                tabClasses.Items.Add(item);
            }

            StackPanel BindHeader(ClassOptions options)
            {
                var pin = new PackIcon();
                pin.Kind = PackIconKind.Account;

                var tbk = new TextBlock();
                tbk.Text = options.Grade?.Id + options.Id;

                var spl = new StackPanel();

                spl.Margin = new Thickness((double)_viewModel.Settings?.Tab?.Item?.HeaderMargin!);

                spl.Children.Add(pin);
                spl.Children.Add(tbk);

                return spl;
            }

            DataGrid BindContent(ClassOptions options)
            {
                var dgd = new DataGrid();

                dgd.ItemsSource = options.Students;
                dgd.IsReadOnly = true;
                dgd.AutoGenerateColumns = false;

                var padding = _viewModel.Settings?.Tab?.Item?.DataGridPadding!.Split(',')!;
                dgd.Padding = new Thickness(double.Parse(padding[0]), double.Parse(padding[1]),
                    double.Parse(padding[2]), double.Parse(padding[3]));

                dgd.LoadingRow += Dgd_LoadingRow;
                dgd.MouseDoubleClick += Dgd_MouseDoubleClick;

                if ((bool)_viewModel.Settings?.Tab?.Item?.SerialColumnVisibility!)
                {
                    dgd.Columns.Add(new WpfDataGridTextColumn()
                    {
                        Header = "序号",
                        Binding = new DataBinding("Header")
                        {
                            RelativeSource = new DataRelativeSource(DataRelativeSourceMode.FindAncestor, typeof(DataGridRow), 1)
                        }
                    });
                }

                foreach (var column in _viewModel.Settings?.Tab?.Item?.StudentColumns!)
                {
                    dgd.Columns.Add(new WpfDataGridTextColumn()
                    {
                        Header = column.Descr ?? column.Name,
                        Binding = new DataBinding(column.Name)
                    });
                }
                
                return dgd;
            }
        }

        private void Dgd_LoadingRow(object? sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void Dgd_MouseDoubleClick(object? sender, MouseButtonEventArgs e)
        {
            var item = tabClasses.SelectedItem as TabItem;
            var options = item?.Tag as ClassOptions;

            var dgd = sender as DataGrid;
#pragma warning disable CS8602 // 解引用可能出现空引用。
            var student = options?.Students?[dgd.SelectedIndex];
#pragma warning restore CS8602 // 解引用可能出现空引用。

            if (!string.IsNullOrEmpty(student?.Portrait))
            {
                var window = new PreviewPortraitWindow(student);
                window.ShowDialog();
            }
        }


        private void RandomCall_Click(object sender, RoutedEventArgs e)
        {
            var item = tabClasses.SelectedItem as TabItem;
            var options = item?.Tag as ClassOptions;

            if (options?.Students is null || options.Students.Count < 1)
                return;

            var window = new CalledNameWindow(_viewModel, options);
            window.ShowDialog();
        }

        private void JobFolderOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.UseDescriptionForTitle = true;
            dialog.Description = "请选择包含作业的目录：";
            dialog.RootFolder = Environment.SpecialFolder.MyDocuments;

            var result = dialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.MessageBox.Show("操作取消", "选择目录");
                return;
            }

            _viewModel.CheckJobFolder = dialog.SelectedPath;

            var item = tabClasses.SelectedItem as TabItem;
            var options = item?.Tag as ClassOptions;

            var window = new CheckJobWindow(options, _viewModel);
            window.ShowDialog();
            _viewModel.JobInfos = window.JobInfos;
        }

        private void ScoringFolderOpen_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.JobInfos is null)
            {
                System.Windows.MessageBox.Show("请先查收作业", "查收作业");
                return;
            }

            var item = tabClasses.SelectedItem as TabItem;
            var options = item?.Tag as ClassOptions;

            var window = new ScoringWindow(_viewModel, options);
            window.ShowDialog();
        }

        private void GenerateScore_Click(object sender, RoutedEventArgs e)
        {
            var item = tabClasses.SelectedItem as TabItem;
            var options = item?.Tag as ClassOptions;

            var window = new GenerateScoreWindow(_viewModel, options);
            window.ShowDialog();
        }


        #region Grid Wrapper

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState != MouseButtonState.Pressed)
                return;

            DragMove();

            _lastLeft = Left;
            _lastTop = Top;
        }

        #endregion


        #region Window

        private Action? WindowMaximizeAction { get; set; }

        private Action? WindowRestoreAction { get; set; }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            var icon = btnMaximize.Content as PackIcon;

            if (WindowState == WindowState.Maximized)
            {
                Left = _lastLeft;
                Top = _lastTop;
                //ResizeMode = ResizeMode.CanResizeWithGrip;

                Width = _defaultWidth;
                Height = _defaultHeight;

                WindowState = WindowState.Normal;
                Topmost = false;

                if (icon is not null)
                {
                    icon.Kind = PackIconKind.WindowMaximize;
                    btnMaximize.ToolTip = "最大化";
                }

                WindowRestoreAction?.Invoke();
                return;
            }

            if (WindowState == WindowState.Normal)
            {
                MaxWidth = SystemParameters.PrimaryScreenWidth;
                MaxHeight = SystemParameters.PrimaryScreenHeight;

                WindowState = WindowState.Maximized;
                Topmost = true;

                if (icon is not null)
                {
                    icon.Kind = PackIconKind.WindowRestore;
                    btnMaximize.ToolTip = "还原";
                }

                WindowMaximizeAction?.Invoke();

                //Activated += new EventHandler(window_Activated);
                //Deactivated += new EventHandler(window_Deactivated);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

    }
}

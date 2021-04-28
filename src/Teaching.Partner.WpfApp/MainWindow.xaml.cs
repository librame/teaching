using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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

#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
            _defaultWidth = Width = (double)_viewModel.Settings?.Window?.Width;
            _defaultHeight = Height = (double)_viewModel.Settings?.Window?.Height;
            FontSize = (double)_viewModel.Settings?.Window?.FontSize;
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。
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

#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
                spl.Margin = new Thickness((double)_viewModel.Settings?.Tab?.Item?.HeaderMargin);
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。

                spl.Children.Add(pin);
                spl.Children.Add(tbk);

                return spl;
            }

            DataGrid BindContent(ClassOptions options)
            {
                var dgd = new DataGrid();

                dgd.MouseDoubleClick += Dgd_MouseDoubleClick;
                dgd.ItemsSource = options.Students;
                dgd.IsReadOnly = true;

                return dgd;
            }
        }

        private void Dgd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

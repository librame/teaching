using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace Teaching.Partner.WpfApp
{
    /// <summary>
    /// CalledNameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CalledNameWindow : Window
    {
        private readonly DispatcherTimer _timer;
        private readonly Random _random;

        private readonly MainWindowViewModel _viewModel;
        private readonly ClassOptions _options;


        public CalledNameWindow(MainWindowViewModel viewModel, ClassOptions options)
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _random = new Random(unchecked((int)DateTime.Now.Ticks));

#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
            tbxName.FontSize = (double)viewModel?.Settings?.Window?.StrikingFontSize;
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。

            viewModel.CalledNames = viewModel.GetCalledNames(options);

            _viewModel = viewModel;
            _options = options;

            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Start();
        }


        private void ShowStudent(StudentOptions student, bool isShowMask = true)
        {
            tbxName.Text = student.ToString(isShowMask);
        }


        private void _timer_Tick(object? sender, EventArgs e)
        {
#pragma warning disable CS8602 // 解引用可能出现空引用。
            var index = _random.Next(_options.Students.Count);
#pragma warning restore CS8602 // 解引用可能出现空引用。
            ShowStudent(_options.Students[index]);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                btnStop.Content = "启动";

                ShowStudent(GetStudent(), isShowMask: false);
                _viewModel.SavedCalledNames(_options);
            }
            else
            {
                _timer.Start();
                btnStop.Content = "停止";
            }

            StudentOptions GetStudent()
            {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                var index = _random.Next(_options.Students.Count);
#pragma warning restore CS8602 // 解引用可能出现空引用。
                var student = _options.Students[index];

#pragma warning disable CS8604 // 可能的 null 引用参数。
                if (_viewModel.CalledNames.Any(s => s.Id == student.Id && s.Name == student.Name))
#pragma warning restore CS8604 // 可能的 null 引用参数。
                    return GetStudent();

                if (_viewModel.CalledNames.Count == _options.Students.Count)
                    _viewModel.CalledNames.Clear();

                _viewModel.CalledNames.Add(student);
                return student;
            }
        }

    }
}

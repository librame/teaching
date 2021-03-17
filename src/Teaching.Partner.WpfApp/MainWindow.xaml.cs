using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
        private readonly List<ClassOptions>? _classes;
        private readonly StyleOptions? _style;


        public MainWindow()
        {
            InitializeComponent();

            _classes = GlobalSettings.ClassesConfigPath.ReadJson<List<ClassOptions>>();
            _style = GlobalSettings.StyleConfigPath.ReadJson<StyleOptions>();

            BindClasses();
        }


        private void BindClasses()
        {
            if (_classes is null)
                return;

            foreach (var c in _classes)
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
                spl.Margin = new Thickness((_style?.Tab?.Item?.HeaderMargin).Value);
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

            var item = tabClasses.SelectedItem as TabItem;
            var options = item?.Tag as ClassOptions;

            var window = new CheckJobWindow(dialog.SelectedPath, options, _style);
            window.ShowDialog();
        }

    }
}

using System.Windows;
using System.Windows.Media.Imaging;

namespace Teaching.Partner.WpfApp
{
    /// <summary>
    /// PreviewPortraitWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PreviewPortraitWindow : Window
    {
        public PreviewPortraitWindow(StudentOptions? options)
        {
            InitializeComponent();

            tbkTitle.Text = options?.Id + options?.Name;
            imgPortrait.Source = new BitmapImage(GlobalUtility.ToImageUri(options?.Portrait));
        }

    }
}

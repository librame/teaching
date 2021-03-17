using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Media;

namespace LibramePlayer.WpfApp
{
    static class MaterialDesignHelper
    {
        private static readonly Lazy<BundledTheme> _bundledTheme
            = new Lazy<BundledTheme>(InitializeBundledTheme);

        private static BundledTheme InitializeBundledTheme()
        {
            foreach (var dictionary in Application.Current.Resources.MergedDictionaries)
            {
                if (dictionary is BundledTheme bundledTheme)
                    return bundledTheme;
            }

            throw new NotImplementedException($"The current resource dictionary collection does not contain {nameof(BundledTheme)} resource.");
        }


        /// <summary>
        /// 绑定的主题。
        /// </summary>
        public static BundledTheme BundledTheme
            => _bundledTheme.Value;


        public static ITheme Theme
            => BundledTheme.GetTheme();


        public static SolidColorBrush? PrimaryHueLightBrush
            => BundledTheme[nameof(PrimaryHueLightBrush)] as SolidColorBrush;

        public static SolidColorBrush? PrimaryHueLightForegroundBrush
            => BundledTheme[nameof(PrimaryHueLightForegroundBrush)] as SolidColorBrush;

        public static SolidColorBrush? PrimaryHueMidBrush
            => BundledTheme[nameof(PrimaryHueMidBrush)] as SolidColorBrush;

        public static SolidColorBrush? PrimaryHueMidForegroundBrush
            => BundledTheme[nameof(PrimaryHueMidForegroundBrush)] as SolidColorBrush;

        public static SolidColorBrush? PrimaryHueDarkBrush
            => BundledTheme[nameof(PrimaryHueDarkBrush)] as SolidColorBrush;

        public static SolidColorBrush? PrimaryHueDarkForegroundBrush
            => BundledTheme[nameof(PrimaryHueDarkForegroundBrush)] as SolidColorBrush;


        public static SolidColorBrush? SecondaryHueLightBrush
            => BundledTheme[nameof(SecondaryHueLightBrush)] as SolidColorBrush;

        public static SolidColorBrush? SecondaryHueLightForegroundBrush
            => BundledTheme[nameof(SecondaryHueLightForegroundBrush)] as SolidColorBrush;

        public static SolidColorBrush? SecondaryHueMidBrush
            => BundledTheme[nameof(SecondaryHueMidBrush)] as SolidColorBrush;

        public static SolidColorBrush? SecondaryHueMidForegroundBrush
            => BundledTheme[nameof(SecondaryHueMidForegroundBrush)] as SolidColorBrush;

        public static SolidColorBrush? SecondaryHueDarkBrush
            => BundledTheme[nameof(SecondaryHueDarkBrush)] as SolidColorBrush;

        public static SolidColorBrush? SecondaryHueDarkForegroundBrush
            => BundledTheme[nameof(SecondaryHueDarkForegroundBrush)] as SolidColorBrush;


        public static SolidColorBrush? SecondaryAccentBrush
            => BundledTheme[nameof(SecondaryAccentBrush)] as SolidColorBrush;

        public static SolidColorBrush? SecondaryAccentForegroundBrush
            => BundledTheme[nameof(SecondaryAccentForegroundBrush)] as SolidColorBrush;


        public static SolidColorBrush? ValidationErrorBrush
            => BundledTheme[nameof(ValidationErrorBrush)] as SolidColorBrush;


        public static SolidColorBrush? MaterialDesignBackground
            => BundledTheme[nameof(MaterialDesignBackground)] as SolidColorBrush;
    }
}

namespace NeoSmart.App
{
    using Helpers;
    using ViewModels;
    using Views;

    public partial class App : Application
    {

        #region Properties
        public static NavigationPage? Navigator { get; internal set; }
        public static FlyoutPage? Master { get; internal set; }
        public static string? Data { get; set; }
        #endregion

        #region Constructors
        public App()
        {
            InitializeComponent();

            if (Settings.IsLogin)
            {
                MainPage = new MasterPage();

            }
            else
            {
                MainPage = new MasterLoginPage();
            }
        }
        #endregion
    }
}

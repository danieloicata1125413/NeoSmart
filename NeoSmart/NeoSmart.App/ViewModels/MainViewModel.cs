namespace NeoSmart.App.ViewModels
{
    using NeoSmart.App.Helpers;
    public class MainViewModel : BaseViewModel
    {
        #region Atributes
        private string empresa = "NeoSmart";
        private string currentVersion;
        #endregion

        #region Properties
        public string Empresa
        {
            get { return this.empresa; }
            set { SetValue(ref this.empresa, value); }
        }
        public string CurrentVersion
        {
            get { return this.currentVersion; }
            set { SetValue(ref this.currentVersion, value); }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            CurrentVersion = VersionTracking.CurrentVersion;
            if (Settings.IsLogin)
            {
                //Dashboards = new MasterDashboardViewModel();

            }
            else
            {
                MasterLogin = new MasterLoginViewModel();
            }
        }
        #endregion

        #region Atributes
        private MasterLoginViewModel? masterLogin;
        #endregion

        #region Properties
        public MasterLoginViewModel? MasterLogin
        {
            get { return masterLogin; }
            set { SetValue(ref masterLogin, value); }
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion
    }
}

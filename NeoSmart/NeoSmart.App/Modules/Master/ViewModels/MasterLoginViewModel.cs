namespace NeoSmart.App.ViewModels
{
    using Helpers;
    using System.Windows.Input;
    using Views;

    public class MasterLoginViewModel : BaseViewModel
    {
        #region Events

        #endregion

        #region Services
        //private ApiService apiService;
        #endregion

        #region Attributes
        private string email;
        private string password;
        private string currentVersion;
        private bool isPassword;
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public string CurrentVersion
        {
            get { return this.currentVersion; }
            set { SetValue(ref this.currentVersion, value); }
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        public bool IsPassword
        {
            get { return this.isPassword; }
            set { SetValue(ref this.isPassword, value); }
        }
        #endregion

        #region Constructors
        public MasterLoginViewModel()
        { 
            this.IsPassword = true;
            this.IsRemembered = true;
            this.IsEnabled = true;
            //this.apiService = new ApiService();
            CurrentVersion = MainViewModel.GetInstance().CurrentVersion;
            LoadUsers();
            //if (!string.IsNullOrEmpty(Settings.Email))
            //{
            //    this.Email = Settings.Email;
            //    this.Password = Settings.Password;
            //}
            //_ = Application.Current!.MainPage!.DisplayAlert(
            //    "Error",
            //    "El dispositivo y el sistema operativo No admite.",
            //    "Aceptar");

        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new Command(() => Login());
            }
        }
        public ICommand PqrsCommand
        {
            get
            {
                return new Command(() => LoadOpenWebPqrs());
            }
        }
        public ICommand PFCommand
        {
            get
            {
                return new Command(() => LoadOpenPF());
            }
        }
        public ICommand LockCommand
        {
            get
            {
                return new Command(() => LoadLockCommand());
            }
        }
        #endregion

        #region Methods
        private async void LoadUsers()
        {
            //_ = await MainViewModel.GetInstance().SyncPersonas();
        }
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "Debe ingresar el nombre",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "Debe ingresar el Password",
                    "Aceptar");
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;

            ////Verificar conexion
            //Response connection = await this.apiService.CheckConnection();
            //if (!connection.IsSuccess)
            //{
            //    await Application.Current!.MainPage!.DisplayAlert(
            //        "Error",
            //        connection.Message,
            //        "Aceptar");
            //    this.IsRunning = false;
            //    this.IsEnabled = true;
            //    return;
            //}

            //this.Email = this.Email.Replace(" ", "");
            //Response response = new Response();
            //if (connection.IsSuccess)
            //{
            //    response = await this.apiService.Get<User>(
            //    Settings.UrlEBSA + "/api/Users/" + this.Email + "/" + this.Password + "/" + Settings.Token + "/" + Settings.TokenType + "/" + CurrentVersion);
            //    if (!response.IsSuccess)
            //    {
            //        response = await this.apiService.Get<User>(
            //        Settings.UrlEBSA2 + "/api/Users/" + this.Email + "/" + this.Password + "/" + Settings.Token + "/" + Settings.TokenType + "/" + CurrentVersion);
            //    }
            //}
            //if (!response.IsSuccess)
            //{
            //    if (response.Message.Equals("NotFound"))
            //    {
            //        await Application.Current!.MainPage!.DisplayAlert(
            //        "Error de acceso",
            //        "Los datos de acceso son incorrectos.",
            //        "Aceptar");
            //    }
            //    else
            //    {
            //        await Application.Current!.MainPage!.DisplayAlert(
            //        "Error",
            //        response.Message,
            //        "Aceptar");
            //    }
            //    this.IsRunning = false;
            //    this.IsEnabled = true;
            //    return;
            //}

            //User _User = (User)response.Result;

            //if (_User == null)
            //{
            //    await Application.Current!.MainPage!.DisplayAlert(
            //    "Error de acceso",
            //    "Los datos de acceso son incorrectos.",
            //    "Aceptar");
            //    this.IsRunning = false;
            //    this.IsEnabled = true;
            //    //this.Password = string.Empty;
            //    return;
            //}

          

            ////pesistencia
            //Settings.UserID = _User.id;
            //if (IsRemembered)
            //{
            //    Settings.Email = this.Email;
            //    Settings.Password = this.Password;
            //    Settings.IsLogin = true;
            //}
            //else
            //{
            //    Settings.Email = string.Empty;
            //    Settings.Password = string.Empty;
            //    Settings.IsLogin = false;
            //}
            //this.Email = string.Empty;
            //this.Password = string.Empty;

            //MainViewModel.GetInstance().Dashboards = new MasterDashboardViewModel();
            //await MainViewModel.GetInstance().LoadPersonaReload();
            //this.IsRunning = false;
            //this.IsEnabled = true;
            App.Current!.MainPage = new MasterPage();
        }
        private void LoadLockCommand()
        {
            if (IsPassword == true)
            {
                IsPassword = false;
            }
            else
            {
                IsPassword = true;
            }
        }
        private async void LoadOpenWebPqrs()
        {
            this.IsRunning = true;
            bool respuesta = false;
            respuesta = await Application.Current!.MainPage!.DisplayAlert("¿Desea continuar?", "Se abrirá un link para enviar una solicitud PQRS", "Si", "No");
            if (respuesta)
            {
                string link = "https://ebsa.com.co/sitio/pagina/Registro-de-PQR";
                //MainViewModel.GetInstance().MasterWeb = new MasterWebViewModel(link);
                //MasterWebPage PQRS = new MasterWebPage();
                //Application.Current!.MainPage! = PQRS;
            }
            this.IsRunning = false;
        }
        private async void LoadOpenPF()
        {
            this.IsRunning = true;
            bool respuesta = false;
            respuesta = await App.Current!.MainPage!.DisplayAlert("¿Desea continuar?", "Se abrirá un link de preguntas frecuentes", "Si", "No");
            if (respuesta)
            {
                string link = "https://ebsa.com.co/entidad/preguntas_frecuentes";
                //MainViewModel.GetInstance().MasterWeb = new MasterWebViewModel(link);
                //MasterWebPage HV = new MasterWebPage();
                //Application.Current!.MainPage! = HV;
            }
            this.IsRunning = false;
        }
        #endregion
    }
}

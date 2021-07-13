using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculadorMediaMovelObservable
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new Screens.InitialPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

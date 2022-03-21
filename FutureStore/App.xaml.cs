using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutureStore
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new DesignPage();
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

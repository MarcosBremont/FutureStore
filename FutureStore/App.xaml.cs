using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutureStore
{
    public partial class App : Application
    {
        public static int Codigo { get; set; }
        public static int CantidadParaVender { get; set; }
        public static int CantidadProductosEnLaCompra { get; set; }
        public static int PrecioDelEnvioEnLaCompra { get; set; }
        public static Double GananciaParaVender { get; set; }
        public static String NombreParaVender { get; set; }
        public static String PrecioParaVender { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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

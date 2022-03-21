using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.Serialization;


namespace FutureStore
{
    public class Configuracion
    {

        //Clases Necesarias
        public static Page LoginPage = null;
        public static TabbedPage MenuPage = null;
        public Menu MenuForThread = null;
        public static Page RecargarPage = null;
        public static Page MetodosDePago = null;
        public static Page PageParaCancelarConfSMS;
        public static bool LoggedIn = false;
        public static bool LoggingOut = false;

        //Conexion de la Api
        //public static string Server = "http://10.0.0.100:7011/";//ApiOfitec Servidor
        //public static string Server = "http://190.6.141.29:7003/";//ApiOfitec Servidor global
        //public static string Server = "http://localhost:7108/Api/";//Conexion Vacia
        public static string Server = "http://10.0.0.136:7108/Api/";//Conexion Vacia
        //private static string Server = "http://api.oficable.com/"; Plesk
        private static string ApiName = "Api/";
        public static string ServerApi { get { return Server + ApiName; } }
        public static int limitLinkLenght = 295;
        internal static int timeOutLimit = 50;
        public static DateTime dtConfirmSMS;

        //Empresa
        public static string Empresa = null;
        public static string correoEmpresa = null;
        public static string telefonoEmpresa = null;
        public static string moneda = "$";

        //No Photo Found Picture
        public static string NoImageFound = "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";
        public static string CarDefaultImage = "iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAYAAADDPmHLAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6QkYzMzRGNzBGNDIyMTFFOTlCM0ZEOTlCOUE1MkYyNEIiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6QkYzMzRGNzFGNDIyMTFFOTlCM0ZEOTlCOUE1MkYyNEIiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDpCRjMzNEY2RUY0MjIxMUU5OUIzRkQ5OUI5QTUyRjI0QiIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDpCRjMzNEY2RkY0MjIxMUU5OUIzRkQ5OUI5QTUyRjI0QiIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/Pke3eugAABrbSURBVHja7F0JWFRX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";
        public static string automaticPass = "";

        //Effects Stuff
        public static double ButtonPressOpacity = 0.5;

        //User Stuff
        public static int id_cliente { get; set; }
        public static string cedula { get; set; }
        public static string nombre { get; set; }
        public static string direccion { get; set; }
        public static string telefono { get; set; }
        public static string celular { get; set; }
        public static string correo { get; set; }
        public static DateTime fechanaci { get; set; }
        public static int id_sexo { get; set; }
        public static string comentario { get; set; }
        public static string imagen { get; set; }
        public static string id_pais { get; set; }
        public static string id_provincia { get; set; }
        public static string id_municipio { get; set; }
        public static string istiemporeal { get; set; }
        public static string tipo_ncf { get; set; }
        public static string key { get; set; }
        public static string version_ofitec { get; set; }

        //User LocalDataStuff
        public static Thread RealtimeSimulationMenuThread;
        public static Thread RealtimeSimulationLoginThread;

        //Estado de Cuenta y Balances
        public static double balance { get; set; } = 0;
        public static double ultimaRecarga { get; set; } = 0;
        public static double ultimoParqueo { get; set; } = 0;
        public static App AppClass { get; internal set; }

        public Configuracion()
        {
        }

        public async static void ReiniciarDatosDeUsuario()
        {
            try
            {
                ResetGeneral();

                try
                {

                    //Cerrar hasta el menú...             
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);

                }
                catch (Exception ex)
                {
                    if (!ex.Message.ToLower().Contains("index"))
                        await MenuPage.DisplayAlert("", ex.Message, "OK");
                }
                finally
                {
                    await LoginPage.DisplayAlert("", "Se agotó el tiempo de su sesión...", "Entendido");
                    LoggedIn = false;
                    await Task.Delay(1000);
                    LoggingOut = false;
                }
            }
            catch { }

        }

        public async static void ReiniciarDatosDeUsuarioConCerrarSesion()
        {
            try
            {

                try
                {
                    await new Herramientas().EjecutarSentenciaEnApiLibre($"CerrarSesion/{id_cliente},S,{key}");

                    ResetGeneral();

                    //Cerrar hasta el login...
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);
                    await Configuracion.MenuPage.Navigation.PopModalAsync(false);

                }
                catch (Exception ex)
                {
                    if (!ex.Message.ToLower().Contains("index"))
                        await LoginPage.DisplayAlert("", ex.Message, "OK");
                }

            }
            catch { }
            finally
            {
                LoggedIn = false;
            }

        }

        public static void ResetGeneral()
        {
            //Reiniciar Datos Generales
            key = "ninguno";
            id_cliente = 0;
            cedula = "0";
            nombre = "0";
            direccion = "0";
            fechanaci = new DateTime();
            correo = "";
            id_sexo = 0;
            telefono = "0";
            celular = "0";
            istiemporeal = "Null";
            tipo_ncf = "0";
            comentario = "...";

            //Reiniciar Balances
            balance = 0;
            ultimaRecarga = 0;
            ultimoParqueo = 0;

        }

    }
}

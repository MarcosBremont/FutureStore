using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Reflection;
using Acr.UserDialogs;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using FutureStore.Models;
using Plugin.Connectivity;

namespace FutureStore
{
    public class Herramientas
    {
        //Public Objects, clases and Variables...
        public string fileSelectedPath = null;
        public Page defaultParentPage = null;
        public Color errColor = Color.Red;
        public bool busy = false;
        public bool BuenaConexionAInternet = false;
        public string msgConexionAInternet = "";

        //Server
        HttpClient usuarioFoto = new HttpClient();

     

        public bool ComprobarConexionURL(string URL)
        {
            try
            {
                URL = URL.ToLower().Replace("http://", "").Replace("https://", "");
                var ip_regularizado = URL.Split('/');
                URL = ip_regularizado[0];


                Ping p1 = new Ping();
                PingReply PR = p1.Send(URL);
                // check when the ping is not success
                while (!PR.Status.ToString().Equals("Success"))
                {
                    return false;
                }
                // check after the ping is n success
                while (PR.Status.ToString().Equals("Success"))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                //toastConfig.MostrarNotificacion("¡Error al conectar!");

                Console.WriteLine(ex.Message);
                return false;
            }
        }


        internal string FormatoMontoPreAzul(double monto)
        {
            try
            {
                string montoFormateado = monto >= 1 ? monto.ToString("###.00") : "0" + monto.ToString("###.00");
                return montoFormateado.Replace(",", ".");
            }
            catch
            {
                return "Error";
            }
        }

       

        internal void Toast(string texto)
        {
            UserDialogs.Instance.Toast(texto);
        }

       

        

        internal string FromURLTOBase64(string fileName)
        {
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] response = wc.DownloadData(fileName);
                return ByteArrayToBase64(response);
            }
            catch (Exception)
            {
                return "Error";
            }
        }

       

        public object GetInstance(string strFullyQualifiedName)
        {
            try
            {
                Type t = Type.GetType(strFullyQualifiedName);
                return Activator.CreateInstance(t);
            }
            catch (Exception e)
            {
                //Hubo un error al tratar de crear la isntancia de la clase strFullyQualifiedName
                return null;
            }
        }

        public string NewTrimingForMask(string Mask, string TextFor)
        {

            //999-9999999-9
            //___-_______-_
            //_59-___2__1-_

            List<string> AllDiferentChars = SacarCadaCaracter(Mask);
            bool finishFromleft = false;
            bool finishFromright = false;

            for (int i = 0; i < AllDiferentChars.Count; i++)
            {
                //Remove from left
                for (int j = 0; finishFromleft; j++)
                {
                    if (TextFor.Substring(j, 1) == AllDiferentChars[i])
                    {
                        TextFor = TextFor.Remove(j, 1);
                    }
                    else
                    {
                        finishFromleft = true;
                    }
                }

                //Remove from right
                for (int j = 0; finishFromright; j++)
                {
                    if (TextFor.Substring(j, 1) == AllDiferentChars[i])
                    {
                        TextFor = TextFor.Remove(j, 1);
                    }
                    else
                    {
                        finishFromright = true;
                    }
                }

            }


            return TextFor;
        }

        public List<string> SacarCadaCaracter(string TextoATrabajar)
        {
            List<string> MyCaracters = new List<string>();

            for (int i = 0; i < TextoATrabajar.Length; i++)
            {
                if (!MyCaracters.Contains(TextoATrabajar.Substring(i, 1)) && TextoATrabajar.Substring(i, 1) != "_")
                {
                    MyCaracters.Add(TextoATrabajar.Substring(i, 1));
                }
            }
            return MyCaracters;
        }

     

       

        public string FormatoDecimalFix(string valor)
        {
            try
            {
                try
                {
                    string s = valor.Replace("$", "");
                    s = Convert.ToDouble(valor).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

                    if (s.Contains("-"))
                    {
                        s = s.Replace("-", "");
                        if (s == ".00")
                        {
                            s = "0.00";
                        }
                        s = "-" + s;
                    }
                    else
                    {
                        if (s == ".00")
                        {
                            s = "0.00";
                        }
                    }

                    return s;
                }
                catch
                {
                    return "Error";
                }
            }
            catch
            {
                return valor;
            }
        }

        public string FormatoDecimal(string valor)
        {
            try
            {
                string SignoDeMenos = valor.Substring(0, 1) == "-" ? "-" : "";
                try
                {
                    valor = valor.Replace(",", "");
                    string s = Convert.ToDouble(valor).ToString("###,###.00");
                    if (s == ".00")
                    {
                        s = "0.00";
                    }

                    if (s.Substring(0, 1) == ".")
                        s = "0" + s;

                    return Convert.ToDouble(s) == 0 ? SignoDeMenos + s : s;
                }
                catch
                {
                    string s = Convert.ToDouble(valor.Replace("-", "")).ToString("###,###.00");
                    if (s == ".00")
                    {
                        s = "0.00";
                    }

                    if (s.Substring(0, 1) == ".")
                        s = "0" + s;

                    return Convert.ToDouble(s) == 0 ? SignoDeMenos + s : s;
                }
            }
            catch
            {
                return valor;
            }
        }

        public bool IsDecimalValido(string valor)
        {
            try
            {
                valor = valor.Replace("$", "");

                double s = Convert.ToDouble(valor);

                return true;
            }
            catch
            {
                if (string.IsNullOrEmpty(valor))
                    return true;
                else
                    return false;
            }
        }

        public string BorrarConBackSpaceEnMascara(string Mask, string texto, int caretposition)
        {
            List<string> AllDiferentChars = SacarCadaCaracter(Mask);
            texto = texto.Remove(caretposition, 1) + Mask.Substring(Mask.Length - 1, 1);
            //texto = FixMovedCharacters(Mask, texto);
            string SubTexto = texto.Substring(caretposition, texto.Length - caretposition);
            string SubTextoW = SubTexto;
            string SubMask = Mask.Substring(caretposition, texto.Length - caretposition);
            bool Pertenece = false;
            int fullCaret = 0;
            string CharacterSame = "";
            if (caretposition < texto.Length - 1)
            {
                for (int i = 0; i < SubTexto.Length - 1; i++)
                {

                    Pertenece = false;
                    if (AllDiferentChars.Contains(SubTexto.Substring(i, 1)))
                        Pertenece = true;

                    if (Pertenece)
                    {
                        Pertenece = false;
                        for (int NewCaret = 0; !Pertenece; NewCaret++)
                        {
                            if ((i + NewCaret + 1) >= (SubTexto.Length - 1))
                            {
                                SubTexto = FixMovedCharacters(SubMask, SubTexto);
                                Pertenece = false;
                                break;
                            }
                            else
                            {
                                CharacterSame = SubTexto.Substring(i + NewCaret + 1, 1);
                                fullCaret = NewCaret + i + 1;
                                if (!AllDiferentChars.Contains(CharacterSame))
                                {
                                    Pertenece = true;
                                }
                            }
                        }
                        if (!Pertenece) { break; } else { }//End of the line...

                        if (i == 0)
                        {
                            try
                            {
                                SubTexto = SubTextoW.Substring(fullCaret, 1);
                                SubTexto += SubTextoW.Substring(i, fullCaret - i);
                                SubTexto += SubTextoW.Substring(fullCaret + 1, SubTextoW.Length - (fullCaret + 1));
                            }
                            catch
                            {
                            }
                        }
                        else if (i > 0)
                        {
                            try
                            {
                                SubTexto = SubTextoW.Substring(0, i);
                                SubTexto += SubTextoW.Substring(fullCaret, 1);
                                SubTexto += SubTextoW.Substring(i, fullCaret - i);
                                SubTexto += SubTextoW.Substring(fullCaret + 1, SubTextoW.Length - (fullCaret + 1));
                            }
                            catch
                            {
                            }
                        }
                        i = fullCaret;
                    }
                    SubTextoW = SubTexto;

                }
            }

            texto = texto.Substring(0, caretposition) + SubTexto;

            return texto;
        }

        public bool Validate(string textoActual, string mask)
        {
            bool Validado = true;

            if (textoActual.Length > mask.Length + 1 || textoActual.Length < mask.Length - 1)
            {
                Validado = false;
            }

            textoActual = RepararMascara(mask, textoActual);

            return Validado;
        }

        public string RepararMascara(string mask, string texto)
        {
            List<string> AllMyChars = SacarCadaCaracter(mask);

            for (int i = 0; i < texto.Length; i++)
            {
                if (!AllMyChars.Contains(texto.Substring(i, 1)) || texto.Substring(i, 1) == "_")
                {
                    //Mask sigue normal...
                }
                else
                {
                    if (texto.Substring(i, 1) != " ")
                    {
                        //Es un dígito o valor, significa que no está vacío...
                        return texto;
                    }
                }
            }

            return mask;
        }

        //public void TextChangeTextBoxCustom(EventArgs e, Control EsteTextBox)
        //{

        //    int SelectionPosition = (EsteTextBox as TextBoxCustom).SelectionStart;
        //    if ((EsteTextBox as TextBoxCustom).TipoValidacion == Validacion.Cedula)
        //    {
        //        if (Validate((EsteTextBox as TextBoxCustom).Text, EsteTextBox))
        //        {
        //            lastValidValue = (EsteTextBox as TextBoxCustom).Text;
        //        }
        //        else
        //        {
        //            (EsteTextBox as TextBoxCustom).Text = lastValidValue;
        //        }

        //        string Formato = (EsteTextBox as TextBoxCustom).DefaultValue.Replace("9", "_").Replace("0", "_");
        //        if ((EsteTextBox as TextBoxCustom).SelectionStart > 0 && (EsteTextBox as TextBoxCustom).Text.Length > Formato.Length)
        //        {

        //            if (((EsteTextBox as TextBoxCustom).Text.Substring((EsteTextBox as TextBoxCustom).SelectionStart, 1) == "_" || char.IsDigit(Convert.ToChar((EsteTextBox as TextBoxCustom).Text.Substring((EsteTextBox as TextBoxCustom).SelectionStart, 1)))) && (EsteTextBox as TextBoxCustom).SelectionStart != (EsteTextBox as TextBoxCustom).Text.Length)
        //            {
        //                if (!ChanguingText)
        //                {
        //                    ChanguingText = true;

        //                    (EsteTextBox as TextBoxCustom).Text = (EsteTextBox as TextBoxCustom).Text.Remove((EsteTextBox as TextBoxCustom).SelectionStart, 1);

        //                }
        //                ChanguingText = false;
        //            }
        //            else if ((EsteTextBox as TextBoxCustom).Text.Substring((EsteTextBox as TextBoxCustom).SelectionStart, 1) != "_")
        //            {
        //                List<string> MyCharacters = SacarCadaCaracter(Formato);
        //                bool EsCaracterDeLaMascara = false;
        //                int PositionToErase = 0;
        //                for (int i = 0; !EsCaracterDeLaMascara; i++)
        //                {
        //                    EsCaracterDeLaMascara = true;
        //                    for (int j = 0; j < MyCharacters.Count; j++)
        //                    {
        //                        if ((EsteTextBox as TextBoxCustom).Text.Substring((EsteTextBox as TextBoxCustom).SelectionStart + PositionToErase, 1) == MyCharacters[j])
        //                        {
        //                            PositionToErase++;
        //                            EsCaracterDeLaMascara = false;
        //                        }
        //                    }
        //                }
        //                (EsteTextBox as TextBoxCustom).Text = (EsteTextBox as TextBoxCustom).Text.Substring(0, (EsteTextBox as TextBoxCustom).SelectionStart - 1) + (EsteTextBox as TextBoxCustom).Text.Substring((EsteTextBox as TextBoxCustom).SelectionStart, PositionToErase) + (EsteTextBox as TextBoxCustom).Text.Substring((EsteTextBox as TextBoxCustom).SelectionStart - 1, 1) + (EsteTextBox as TextBoxCustom).Text.Substring((EsteTextBox as TextBoxCustom).SelectionStart + (PositionToErase + 1), ((EsteTextBox as TextBoxCustom).Text.Length - ((EsteTextBox as TextBoxCustom).SelectionStart + (PositionToErase + 1))));
        //                SelectionPosition = SelectionPosition + PositionToErase;

        //            }

        //            (EsteTextBox as TextBoxCustom).SelectionStart = SelectionPosition;
        //        }
        //        else
        //        {
        //            if ((EsteTextBox as TextBoxCustom).Text.Length < Formato.Length)//addd a "_" because it erased...
        //            {
        //                if (!ChanguingText)
        //                {
        //                    ChanguingText = true;

        //                    if (Formato.Substring((EsteTextBox as TextBoxCustom).SelectionStart, 1) != "_")
        //                    {
        //                        (EsteTextBox as TextBoxCustom).Text = (EsteTextBox as TextBoxCustom).Text.Remove((EsteTextBox as TextBoxCustom).SelectionStart, 1).Insert((EsteTextBox as TextBoxCustom).SelectionStart, Formato.Substring((EsteTextBox as TextBoxCustom).SelectionStart, 1) + Formato.Substring((EsteTextBox as TextBoxCustom).SelectionStart + 1, 1));
        //                    }
        //                    else
        //                    {
        //                        (EsteTextBox as TextBoxCustom).Text = (EsteTextBox as TextBoxCustom).Text.Insert((EsteTextBox as TextBoxCustom).SelectionStart, "_");
        //                    }
        //                    (EsteTextBox as TextBoxCustom).SelectionStart = SelectionPosition;
        //                }
        //                ChanguingText = false;
        //            }
        //        }
        //    }

        //}

        public string FixMovedCharacters(string mask, string texto)
        {
            List<string> AllMyChars = SacarCadaCaracter(mask);
            for (int i = 0; i < mask.Length; i++)
            {
                if (texto.Substring(i, 1) != "_")
                {
                    if (AllMyChars.Contains(texto.Substring(i, 1)))
                    {
                        texto = texto.Remove(i, 1);
                        texto = texto.Insert(i, mask.Substring(i, 1));
                    }
                }
            }
            return texto;
        }

        public string ColocarPrimerCaracterEnMayuscula(string OldString)
        {
            OldString = OldString.Substring(0, 1).ToUpper() + OldString.Substring(1, OldString.Length - 1);
            return OldString;
        }

        public bool ElDatasourceContieneElItemTal(DataTable myDataSource, string nombreColumna, string ItemTal)
        {
            bool SeEncuentraElItem = false;

            for (int i = 0; i < myDataSource.Rows.Count; i++)
            {
                if (myDataSource.Rows[i][nombreColumna].ToString() == ItemTal)
                    SeEncuentraElItem = true;
            }

            return SeEncuentraElItem;
        }

        public bool IsValorNumerico(string texto)
        {
            try
            {
                texto = texto.Replace("$", "");
                double isNumeric = Convert.ToDouble(texto);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValorEntero(string texto)
        {
            try
            {
                texto = texto.Replace("$", "");
                long isNumeric = Convert.ToInt64(texto);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValorNumericoNegativo(string texto)
        {
            try
            {
                texto = texto.Replace("$", "");
                double isNumeric = Convert.ToDouble(texto);
                if (isNumeric < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public int StrToInt(string numero)
        {
            int numero_int;
            int.TryParse(numero, out numero_int);
            return numero_int;
        }

        public double StrToDouble(string numero)
        {
            double numero_double;
            double.TryParse(numero, out numero_double);
            return numero_double;
        }

        public double FixStrToDouble(string numero)
        {
            if (numero is null)
                return 0;

            string numeroReparado = "";
            for (int caracter = 0; caracter < numero.Length; caracter++)
            {
                if (Char.IsDigit(Convert.ToChar(numero.Substring(caracter, 1))) || Convert.ToChar(numero.Substring(caracter, 1)) == '.')
                {
                    numeroReparado += numero.Substring(caracter, 1);
                }
                else if (caracter == 0 & Convert.ToChar(numero.Substring(caracter, 1)) == '-')
                {
                    numeroReparado += numero.Substring(caracter, 1);
                }
            }

            numero = numeroReparado;
            double numero_double;
            double.TryParse(numero, out numero_double);
            return numero_double;
        }

        public string StrToNumbers(string cadena)
        {
            if (cadena is null)
                return "";

            string numeroReparado = "";
            for (int caracter = 0; caracter < cadena.Length; caracter++)
            {
                if (Char.IsDigit(Convert.ToChar(cadena.Substring(caracter, 1))))
                {
                    numeroReparado += cadena.Substring(caracter, 1);
                }
            }
            return numeroReparado;
        }

        public decimal StrToDecimal(string numero)
        {
            decimal numero_double;
            decimal.TryParse(numero, out numero_double);
            return numero_double;
        }

        public Int64 StrToInt64(string numero)
        {
            Int64 numero_int;
            Int64.TryParse(numero, out numero_int);
            return numero_int;
        }

        public DateTime StrToDateTime(string fecha)
        {
            DateTime fechaParsed;
            DateTime.TryParse(fecha, out fechaParsed);
            return fechaParsed;
        }

        public DateTime FechaHora()
        {
            TimeZoneInfo zona = TimeZoneInfo.FindSystemTimeZoneById("Venezuela Standard Time");
            return TimeZoneInfo.ConvertTime(DateTime.Now, zona);
        }


        public async Task<bool> RevisarConexionAInternet(bool mostrarMensajes = false)
        {
            try
            {
                var cnx = CrossConnectivity.Current;
                msgConexionAInternet = "";

                if (cnx.IsConnected)
                {

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        BuenaConexionAInternet = true;
                        return BuenaConexionAInternet;
                    }

                    var tipoConexion = Plugin.Connectivity.Abstractions.ConnectionType.Cellular;
                    string movilOWifi = cnx.ConnectionTypes.Contains(tipoConexion) ? "Móvil" : "Wifi";

                    var capacidadMinima = 500000;
                    var anchoDeBanda = cnx.Bandwidths;

                    if (anchoDeBanda.Count() == 0)
                    {
                        BuenaConexionAInternet = false;
                        msgConexionAInternet = $"No hay acceso a internet en su red {movilOWifi}...";
                    }
                    else
                    {
                        if (anchoDeBanda.Max() >= (ulong)capacidadMinima)
                        {
                            BuenaConexionAInternet = true;
                        }
                        else
                        {
                            BuenaConexionAInternet = false;
                            msgConexionAInternet = $"Su conexión a Internet es deficiente, compruebe su conexión a internet e inténtelo de nuevo...";
                        }
                    }
                }
                else
                {
                    BuenaConexionAInternet = false;
                    msgConexionAInternet = $"Usted no está conectado a Internet...";
                }
            }
            catch (Exception ex)
            {
                BuenaConexionAInternet = false;
                msgConexionAInternet = $"Error al comprobar su conexión a internet...";
            }

            if (!BuenaConexionAInternet && mostrarMensajes)
                MsgNormal(msgConexionAInternet);

            return BuenaConexionAInternet;
        }


        public ImageSource Base64ToString(string base64)
        {
            try
            {
                byte[] Base64Stream = Convert.FromBase64String(base64);
                return ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            }
            catch (Exception)
            {
                byte[] Base64Stream = Convert.FromBase64String(Configuracion.NoImageFound);
                return ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            }

        }

        public byte[] ImageToByteArray(string imgSourcePath)
        {
            //file to base64 string
            return System.IO.File.ReadAllBytes(imgSourcePath);
        }

        public string ByteArrayToBase64(byte[] ByteArray)
        {
            return Convert.ToBase64String(ByteArray);
        }

        public string PathToBase64(string PathFileFrom)
        {
            if (PathFileFrom is null)
                return "Image not found...";
            return ByteArrayToBase64(ImageToByteArray(PathFileFrom));
        }

        #region Mensajes y Alertas

        public async void MsgNormal(Page pageSender, string msg, string buttonOkName = "OK", string title = "")
        {

            if (!Configuracion.LoggedIn && msg.ToLower().Contains("tiempo de espera agotado"))
                return;

            await pageSender.DisplayAlert(title, msg, buttonOkName);
        }

        public async void MsgNormal(string msg, string buttonOkName = "OK", string title = "")
        {
            if (!Configuracion.LoggedIn && msg.ToLower().Contains("tiempo de espera agotado"))
                return;

            await defaultParentPage.DisplayAlert(title, msg, buttonOkName);
        }

        public async Task<bool> MsgNormalAsync(string msg, string buttonOkName = "OK", string title = "")
        {
            if (!Configuracion.LoggedIn && msg.ToLower().Contains("tiempo de espera agotado"))
                return false;

            await defaultParentPage.DisplayAlert(title, msg, buttonOkName);
            return true;
        }

        public async Task<bool> MsgPregunta(Page pageSender, string msg, string buttonOkName = "ACEPTAR", string buttonCancelName = "CANCELAR", string title = "")
        {
            bool myResult = false;

            //Make the question...
            var result = await pageSender.DisplayAlert(title, msg, buttonOkName, buttonCancelName);
            if (result)
                myResult = true;
            else
                myResult = false;

            //Retrieve the answer...
            return myResult;
        }

        public async void MsgError(Page pageSender, string msg = "Error inesperado...", string buttonOkName = "OK", string title = "")
        {
            if (!msg.ToLower().Contains("tiempo de espera agotado"))
                await pageSender.DisplayAlert(title, msg, buttonOkName);
        }

        #endregion

        public async Task<bool> RevisarTiempoDeSesion(string msg)
        {
            if (!Configuracion.LoggedIn)
                return true;

            if (msg.ToLower().Contains("tiempo de espera agotado"))
            {
                Configuracion.LoggingOut = true;
                Configuracion.ReiniciarDatosDeUsuario();
                await Task.Delay(500);

                return false;
            }
            else return true;
        }



        public async Task<string> EjecutarSentenciaEnApiLibre(string urlPart, bool check = false)
        {
            string request = "";
            try
            {
                string url = Configuracion.Server + urlPart;

                HttpClient clientHTTP = new HttpClient();
                var response = await clientHTTP.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    request = jsonString.ToString();
                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast("¡No se pudo establecer conexión con el servidor!. ");
            }
            return request;
        }

        public async Task<string> EjecutarMetodoPost(string parametros, string body_data)
        {
            try
            {
                string url = Configuracion.ServerApi + parametros;
                string bvody_data_formated = "\"" + body_data.Replace("\"", "\\" + "\"") + "\"";

                HttpClient client = new HttpClient();
                var content = new StringContent(bvody_data_formated, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                var jsonString = await response.Content.ReadAsStringAsync();
                return jsonString.ToString();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast("¡No se pudo establecer conexión con el servidor!. ");
                throw ex;
            }
        }

        public async Task<string> EjecutarSentenciaEnApiParaSubirFoto(string Method)
        {
            string request = "Nothing from sever...";

            string sentencia = Configuracion.ServerApi + Method;

            var response = await usuarioFoto.GetAsync(sentencia);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                request = jsonString.ToString();
            }

            return request;
        }

        public List<object> GetObjectListPorFiltro(List<object> completeList, List<object> emptyList, string parameterName, string parameterFilter)
        {
            //Obtener el vehiculo por el id recibido...
            foreach (object item in completeList)
            {
                var propertiesOfList = item.GetType().GetProperties();
                foreach (var prop in propertiesOfList)
                {
                    if (prop.Name == parameterName)
                    {
                        if (prop.GetValue(item).ToString() == parameterFilter)
                        {
                            emptyList.Add(item);
                            break;
                        }
                    }
                }
            }

            return emptyList;
        }

        public List<object> LlenarLista(DataTable dt, object clase)
        {
            List<object> usuarios = new List<object>();
            Type thisType = clase.GetType();
            MethodInfo theMethod = thisType.GetMethod("Clone");
            var newReset = new object();
            var properties = clase.GetType().GetProperties();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                newReset = theMethod.Invoke(clase, null);
                foreach (var Prop in properties)
                {
                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        if (Prop.Name == dt.Columns[x].ColumnName)
                        {
                            Prop.SetValue(newReset, dt.Rows[i][x].ToString());
                        }
                    }
                }
                usuarios.Add(newReset);

            }
            return usuarios;
        }

        public string[] SplitByMaximunPortionSize(string texto, int portionMaximunSize, bool replaceDiagonal = false)
        {
            int parts = DivideRoundingUp(texto.Length, portionMaximunSize);
            if (replaceDiagonal)
                texto = texto.Replace("/", "*");

            string[] result = new string[parts];
            int position = 0;

            while (texto.Length > 0)
            {
                while (texto.Length >= portionMaximunSize)
                {
                    result[position] = texto.Substring(0, portionMaximunSize);
                    texto = texto.Remove(0, portionMaximunSize);
                    position++;
                }

                //Last Portion
                result[position] = texto.Substring(0, texto.Length);
                texto = "";

            }

            return result;
        }

        public static int DivideRoundingUp(int x, int y)
        {
            // TODO: Define behaviour for negative numbers
            int remainder;
            int quotient = Math.DivRem(x, y, out remainder);
            return remainder == 0 ? quotient : quotient + 1;
        }

        public string GenerarClave(int longitudPassword)
        {

            Random rnd = new Random();
            string Password = "";

            for (int i = 0; i < longitudPassword; i++)
            {
                Password = Password + (rnd.Next(1, 10).ToString());
            }

            return Password;

        }

        public string GetFileNameFromImageControl(Image imgWorking)
        {
            var source = imgWorking.Source as FileImageSource;

            if (source != null)
                return source.File;
            else
                return null;
        }

        public bool IsNumeroMayorQueCero(string textoToWork)
        {
            try
            {
                return Convert.ToInt64(textoToWork) > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

       
        #region METODOS PARA ENCRIPTAR
        /// Encripta una cadena
        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
        #endregion

        public async void Appear(Layout control, int speed = 100, double increaseAmmount = 0.05)
        {
            //regulate Speed...
            if (speed > 100)
                speed = 100;
            if (speed < 0)
                speed = 0;

            //Delay calculation
            int delay = 106 - speed;

            while (control.Opacity < 1)
            {
                control.Opacity += increaseAmmount;
                await Task.Delay(delay);
            }
        }

        public static string EncriptarConContraseña(string key, string texto_plano)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(texto_plano);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DesencriptarConContraseña(string key, string texto_plano)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(texto_plano);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }

    public enum Permiso
    {
        Camara,
        Galeria,
        Telefono
    }

    public enum ContextoDeFoto
    {
        cliente,
        vehiculo
    }

    public class RequestResult
    {
        public bool ResultStatus { get; set; }
        public string String_Result { get; set; }
    }


}


using Newtonsoft.Json;
using FutureStore.Entidad;
using FutureStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FutureStore.Models
{
    public class Metodos
    {
        Herramientas herramientas = new Herramientas();
        //ToastConfigClass toastConfigClass = new ToastConfigClass();
        public Metodos()
        {
            // constructor
        }

        public async Task<List<EProductos>> GetListadoProductosPorID(int cod)
        {

            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Productos/ConsultarListaDeProductos/{cod}");
            var listado_de_productos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EProductos>>(result);

            return listado_de_productos;
        } // Fin del método ObtenerMenu

        public async Task<List<EProductos>> GetListadoProductos()
        {

            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Productos/ConsultarListaDeProductos/");
            var listado_de_productos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EProductos>>(result);

            return listado_de_productos;
        } // Fin del método ObtenerMenu

        public async Task<Result> SentenciaProductos(string Nombre, int Precio, int Cantidad)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Productos/SentenciaProductos/{Nombre}/{Precio}/{Cantidad}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }

        public async Task<Result> UProducto(int cantidad, int Cod)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Productos/UProducto/{cantidad}/{Cod}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }


        public async Task<Result> DProducto(int Cod)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Productos/DProducto/{Cod}/");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }







        //public async Task<Result> ActualizarOrden(int tiposervicio, int numero, string progreso_orden, string reportetecnico, string servicio)
        //{

        //    reportetecnico = reportetecnico.Replace("/", " ");
        //    string url = $"ofitec/ActualizarOrden?" +
        //     $"tiposervicio={tiposervicio}" +
        //     $"&numero={numero}" +
        //     $"&progreso_orden={progreso_orden}" +
        //     $"&reportetecnico={reportetecnico}" +
        //     $"&servicio={servicio}";
        //    var result = await herramientas.EjecutarSentenciaEnApiLibre(url);

        //    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
        //    return response;
        //}
        //public async Task<Result> SetHistorialProgresoOrden(int numero, int numero_ordencable, int numero_ordeninternet, string progreso_orden, string fecha, string lat, string lng)
        //{

        //    string url = $"ofitec/SetHistorialProgresoOrden?" +
        //     $"numero={numero}" +
        //     $"&numero_ordencable={numero_ordencable}" +
        //     $"&numero_ordeninternet={numero_ordeninternet}" +
        //     $"&progreso_orden={progreso_orden}" +
        //     $"&fecha={fecha}" +
        //     $"&lat={lat}" +
        //     $"&lng={lng}";
        //    var result = await herramientas.EjecutarSentenciaEnApiLibre(url);

        //    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
        //    return response;
        //}

        //public async Task<List<CantidadOrdenProgreso>> ConsultarCantidadOrdenesProgreso(int id_tecnico)
        //{
        //    var result = await herramientas.EjecutarSentenciaEnApiLibre($"ofitec/ConsultarCantidadOrdenesProgreso/{id_tecnico}");
        //    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CantidadOrdenProgreso>>(result);
        //    return response;
        //}

        //public async Task<List<OrdenServi>> GetListadoDeOrdenes(int tiposervicio, int id_tecnico, string progreso_orden, DateTime desde, DateTime hasta)
        //{
        //    string fechas = desde.ToString("yyyy-MM-dd") + "/" + hasta.ToString("yyyy-MM-dd");
        //    var result = await herramientas.EjecutarSentenciaEnApiLibre($"ofitec/ConsultarListadoDeOrdenesPorFechas/{tiposervicio}/{id_tecnico}/{progreso_orden}/{fechas}");

        //    var listado_de_ordenes_servicio = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrdenServi>>(result);

        //    return listado_de_ordenes_servicio;
        //}

        //public async Task<Tecnico> CambiarContrasena(int id_tecnico, string clave)
        //{
        //    var result = await herramientas.EjecutarSentenciaEnApiLibre($"/ofitec/cambiarclave/{id_tecnico}/{clave}");
        //    var tecnico = Newtonsoft.Json.JsonConvert.DeserializeObject<Tecnico>(result);

        //    return tecnico;
        //}

        //public async Task<Result> SetFacturaTemporal(List<Producto> listadodeproductosinventario, int IDFacturaTemporal)
        //{
        //    var body_data = JsonConvert.SerializeObject(listadodeproductosinventario);
        //    var result = await new Herramientas().EjecutarMetodoPost(
        //       $"ofitec/GuardarDetalleFacturaTemporal?id_factura_temporal={IDFacturaTemporal}", body_data);
        //    return new Result { Respuesta = "OK", Mensaje = result };
        //}



        //public async Task<Tuple<string, List<Producto>>> GetListadoDeProductosPorOrden(int numero_orden_general)
        //{
        //    try
        //    {
        //        var result = await herramientas.EjecutarSentenciaEnApiLibre($"ofitec/ListadoDeProductosPorOrdenes/{numero_orden_general}");
        //        var listado_de_productos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Producto>>(result);

        //        List<Producto> lista = new List<Producto>();
        //        return new Tuple<string, List<Producto>>("True", listado_de_productos);
        //    }
        //    catch (Exception ex)
        //    {
        //        List<Producto> lista = new List<Producto>();
        //        return new Tuple<string, List<Producto>>("Error. " + ex.Message, lista);
        //    }
        //} // Fin del método 

        //public async Task<Tuple<string, List<Producto>>> ConsultarListadoDeProductosPorSector(int id_sector, string estado, string serviciotecnico)
        //{
        //    try
        //    {
        //        var result = await herramientas.EjecutarSentenciaEnApiLibre($"ofitec/ConsultarListadoDeProductosPorSector/{id_sector}/{"A"}{"S"}");
        //        var listado_de_productos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Producto>>(result);

        //        List<Producto> lista = new List<Producto>();
        //        return new Tuple<string, List<Producto>>("True", listado_de_productos);
        //    }
        //    catch (Exception ex)
        //    {
        //        List<Producto> lista = new List<Producto>();
        //        return new Tuple<string, List<Producto>>("Error. " + ex.Message, lista);
        //    }
        //} // Fin del método 

        //public async Task<Tuple<string, List<Sectores>>> GetListadoDeSectores()
        //{
        //    try
        //    {
        //        var result = await herramientas.EjecutarSentenciaEnApiLibre($"ofitec/ConsultarSectores");
        //        var listado_de_sectores = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sectores>>(result);

        //        return new Tuple<string, List<Sectores>>("True", listado_de_sectores);
        //    }
        //    catch (Exception ex)
        //    {
        //        List<Sectores> lista = new List<Sectores>();

        //        return new Tuple<string, List<Sectores>>("Error. " + ex.Message, lista);
        //    }
        //} // Fin del método 

        //public async Task<Result> ActualizarLocalizacion(int codigo_cli, string lat, string lng)
        //{
        //    Result response = new Result();

        //    try
        //    {
        //        string url = $"ofitec/ActualizarLocalizacion/{codigo_cli}/{lat}/{lng}";
        //        var result = await herramientas.EjecutarSentenciaEnApiLibre(url);

        //        response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        toastConfigClass.MostrarNotificacion("No es posible obtener la lozalización en este momento");
        //    }

        //    return response;


        //}

        //public async Task<ResultValidarImei> IimeiOfitec(string imei, DateTime fecha_hora, string aplicacion)
        //{

        //    ResultValidarImei resultValidarImei = new ResultValidarImei();
        //    try
        //    {
        //        string fechaactual = DateTime.Now.ToString("yyyy-MM-dd");
        //        var result = await herramientas.EjecutarSentenciaEnApiLibre($"ofitec/IimeiOfitec/{imei}/{fechaactual}/{aplicacion}");
        //        resultValidarImei = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultValidarImei>(result);
        //        resultValidarImei.HayError = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (resultValidarImei is null)
        //            resultValidarImei = new ResultValidarImei();

        //        resultValidarImei.HayError = true;
        //        resultValidarImei.Mensaje = ex.Message;
        //    }
        //    return resultValidarImei;
        //} // Fin del método 

        //public async Task<Result> ActualizarIdentificadorTecnico(int cod_tecnico, string imei)
        //{
        //    var result = await herramientas.EjecutarSentenciaEnApiLibre($"ofitec/ActualizarIdentificadorTecnico/{cod_tecnico}/{imei}");
        //    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
        //    return response;
        //}


        //public async Task<Result> IsLicenciaValida(DateTime fecha_hora)
        //{
        //    Result result1 = new Result();
        //    try
        //    {
        //        string fechaactual = DateTime.Now.ToString("yyyy-MM-dd");

        //        var response = await herramientas.EjecutarSentenciaEnApiLibre($"ofitec/IsLicenciaValida/{fechaactual}");
        //        result1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(response);
        //        if (result1 is null)
        //        {
        //            result1 = new Result();
        //            result1.Respuesta = "ERROR";
        //            result1.Mensaje = "Por favor configure la conexión de la api en los ajustes";

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        if (result1 is null)
        //            result1 = new Result();

        //        result1.Respuesta = "ERROR";
        //        result1.Mensaje = "No hay conexión con el servidor";
        //    }
        //    return result1;
        //}

        //public async Task<Tuple<string, List<Empresa>>> GetListadoEmpresa(int codigo)
        //{
        //    try
        //    {
        //        var result = await herramientas.EjecutarSentenciaEnApiLibre($"ofitec/SEmpresaConfiguracion/{codigo}");
        //        var listado_empresa = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Empresa>>(result);

        //        return new Tuple<string, List<Empresa>>("True", listado_empresa);
        //    }
        //    catch (Exception ex)
        //    {
        //        List<Empresa> lista_empresa = new List<Empresa>();

        //        return new Tuple<string, List<Empresa>>("Error. " + ex.Message, lista_empresa);
        //    }
        //} // Fin del método 

        //public async Task<List<ClientesEnPrueba>> ConsultarClientesEnPrueba()
        //{
        //    var result = await herramientas.EjecutarSentenciaEnApiLibre($"ofitec/ConsultarListadoDeClientesEnPrueba");

        //    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClientesEnPrueba>>(result);
        //    return response;
        //}
    }
} // Fin clase


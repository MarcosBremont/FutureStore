using System;
using System.Collections.Generic;
using System.Text;

namespace FutureStore.Entidad
{
    public class EProductos
    {
        public int Cod { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int PrecioSinGanancias { get; set; }
        public int Ganancia { get; set; }
        public int CantidadProductosEnLaCompra { get; set; }
        public int PrecioDelEnvioEnLaCompra { get; set; }
        public string Foto { get; set; } = "";
    }
}

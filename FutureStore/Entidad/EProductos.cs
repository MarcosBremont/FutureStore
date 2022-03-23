using System;
using System.Collections.Generic;
using System.Text;

namespace FutureStore.Entidad
{
    public class EProductos
    {
        public int Cod { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; } = "";
        public string Cantidad { get; set; } = "";
        public string Foto { get; set; } = "";
    }
}

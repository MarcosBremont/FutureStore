using FutureStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutureStore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DesignPage : ContentPage
    {
        Metodos metodos = new Metodos();
        public DesignPage()
        {
            InitializeComponent();
        }

        private async void btnAgregarProducto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var response = await metodos.SentenciaProductos(TxtNombre.Text, Convert.ToInt32(TxtPrecio.Text), Convert.ToInt32(TxtCantidad.Text));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
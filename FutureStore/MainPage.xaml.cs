using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutureStore
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnCalcular_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtValorDelProductoEnPesos.Text))
            {
                await DisplayAlert("Aviso", "Por favor rellene el campo antes de continuar", "OK");
            }
            else if (string.IsNullOrEmpty(TxtPrecioEnvio.Text ))
            {
                await DisplayAlert("Aviso", "Por favor rellene el campo antes de continuar", "OK");
            }
            else if (string.IsNullOrEmpty(TxtCantidadProductosQueLlegaron.Text))
            {
                await DisplayAlert("Aviso", "Por favor rellene el campo antes de continuar", "OK");

            }
            else
            {
                int CantidadProductosQueLlegaron = Convert.ToInt32(TxtCantidadProductosQueLlegaron.Text);
                int PrecioEnvio = Convert.ToInt32(TxtPrecioEnvio.Text);
                int CuantoSeLeCobraranDeLaLibra = PrecioEnvio / CantidadProductosQueLlegaron;

                int PrecioDelPaqueteEnPesos = Convert.ToInt32(TxtValorDelProductoEnPesos.Text);


                Double PrecioDelPaqueteYLibra = PrecioDelPaqueteEnPesos + CuantoSeLeCobraranDeLaLibra;
                Double PrecioTotalMasLasGanancias = PrecioDelPaqueteYLibra * 0.30;


                TxtValorTotal.Text = (PrecioDelPaqueteYLibra + PrecioTotalMasLasGanancias).ToString();
            }


        
    }
}
}

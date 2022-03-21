using FutureStore.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutureStore
{
    public partial class MainPage : ContentPage
    {
        readonly Metodos metodos = new Metodos();
        public MainPage()
        {
            InitializeComponent();
            btnImgCalculator.Source = "calculatorRojo.png";

            gridCalculator.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    LayoutProductos.IsVisible = false;
                    btnImgCalculator.Source = "calculatorRojo.png";
                    StackLayourCalculator.IsVisible = true;
                    StackLayoutProducts.IsVisible = false;
                    btnProducts.Source = "products.png";
                    btnAddProduct.Source = "add.png";
                    lsv_productos.IsVisible = false;
                }),
                NumberOfTapsRequired = 1
            });

            gridProducts.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    LayoutProductos.IsVisible = true;
                    btnProducts.Source = "productsRojo.png";
                    StackLayourCalculator.IsVisible = false;
                    StackLayoutProducts.IsVisible = true;
                    btnImgCalculator.Source = "calculator.png";
                    btnAddProduct.Source = "add.png";
                    lsv_productos.IsVisible = true;
                }),
                NumberOfTapsRequired = 1
            });

            gridaddproduct.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    LayoutProductos.IsVisible = true;
                    btnAddProduct.Source = "add_Rojo.png";
                    StackLayourCalculator.IsVisible = false;
                    StackLayoutProducts.IsVisible = true;
                    btnImgCalculator.Source = "calculator.png";
                    btnProducts.Source = "products.png";
                    lsv_productos.IsVisible = true;
                }),
                NumberOfTapsRequired = 1
            });
        }

        protected async override void OnAppearing()
        {
            try
            {
                LayoutProductos.IsVisible = false;
                lsv_productos.IsVisible = false;
                base.OnAppearing();
                this.IsBusy = false;
                var apiResult = await metodos.GetListadoProductos();
                lsv_productos.ItemsSource = apiResult;
                

                //Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {

            }

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

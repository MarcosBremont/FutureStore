using FutureStore.Entidad;
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

            lsv_productos.ItemSelected += Lsv_productos_ItemSelected;

            gridCalculator.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    LayoutProductos.IsVisible = false;
                    StackLayoutAddProducts.IsVisible = false;

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

                    StackLayoutAddProducts.IsVisible = false;

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
                    StackLayoutProducts.IsVisible = false;

                    StackLayoutAddProducts.IsVisible = true;
                    LayoutProductos.IsVisible = true;
                    btnAddProduct.Source = "add_Rojo.png";
                    StackLayourCalculator.IsVisible = false;
                    btnImgCalculator.Source = "calculator.png";
                    btnProducts.Source = "products.png";
                }),
                NumberOfTapsRequired = 1
            });
        }

        private async void Lsv_productos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var element = lsv_productos.SelectedItem as EProductos;
                if (await DisplayAlert("Atención", "¿Desea eliminar este producto?", "SI", "NO"))
                {
                    var result = await new Metodos().DProducto(Convert.ToInt32(element.Cod));

                }
                else
                {

                }

                var apiResult = await metodos.GetListadoProductos();
                lsv_productos.ItemsSource = apiResult;
            }
        }

        protected async override void OnAppearing()
        {
            try
            {
                LayoutProductos.IsVisible = false;
                StackLayoutAddProducts.IsVisible = false;
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
            else if (string.IsNullOrEmpty(TxtPrecioEnvio.Text))
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

        private async void btnAgregarProducto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var response = await metodos.SentenciaProductos(TxtNombre.Text, Convert.ToInt32(TxtPrecio.Text), Convert.ToInt32(TxtCantidad.Text));
                var apiResult = await metodos.GetListadoProductos();
                lsv_productos.ItemsSource = apiResult;
            }
            catch (Exception ex)
            {

            }
        }
    }
}

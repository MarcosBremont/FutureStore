using FutureStore.Entidad;
using FutureStore.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Rg.Plugins.Popup.Services;
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
        ModalCantidad modalCantidad = new ModalCantidad();

        public MainPage()
        {
            InitializeComponent();
            btnImgCalculator.Source = "calculatorAmarillo.png";

            lsv_productos.ItemSelected += Lsv_productos_ItemSelected;

            gridCalculator.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    LayoutProductos.IsVisible = false;
                    StackLayoutAddProducts.IsVisible = false;

                    btnImgCalculator.Source = "calculatorAmarillo.png";
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
                    btnProducts.Source = "productsAmarillo.png";
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
                    btnAddProduct.Source = "addAmarillo.png";
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
                App.Codigo = element.Cod;
                Acr.UserDialogs.UserDialogs.Instance.Toast("¡Seleccione que desea hacer, eliminar o modificar!");
                TxtCantidadProductos.IsVisible = true;
                TxtCantidadProductos.Text = element.Cantidad;
                BtnModificar.IsVisible = true;
                FrameCantidadProductos.IsVisible = true;

                BtnEliminar.IsVisible = true;

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
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("¡No se pudo conectar, intente mas tarde!");

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
                if (string.IsNullOrEmpty(TxtNombre.Text))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("¡Por favor llenar el campo del nombre!");
                }
                else if (string.IsNullOrEmpty(TxtPrecio.Text))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("¡Por favor llenar el campo del precio!");
                }
                else if (string.IsNullOrEmpty(TxtCantidad.Text))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("¡Por favor llenar el campo de cantidad!");
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                    var response = await metodos.SentenciaProductos(TxtNombre.Text, Convert.ToInt32(TxtPrecio.Text), Convert.ToInt32(TxtCantidad.Text));
                    var apiResult = await metodos.GetListadoProductos();
                    lsv_productos.ItemsSource = apiResult;
                    TxtNombre.Text = "";
                    TxtPrecio.Text = "";
                    TxtCantidad.Text = "";
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();

                    Acr.UserDialogs.UserDialogs.Instance.Toast("¡Producto Agregado Con Exito!");
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("¡No se pudo agregar el producto, intente mas tarde!");
            }
        }

        void BtnLimpiar_Clicked(System.Object sender, System.EventArgs e)
        {
            TxtCantidadProductosQueLlegaron.Text = "";
            TxtValorDelProductoEnPesos.Text = "";
            TxtPrecioEnvio.Text = "";
            TxtValorTotal.Text = "";
            Acr.UserDialogs.UserDialogs.Instance.Toast("¡Se han limpiado los campos!");

        }

        private async void BtnModificar_Clicked(object sender, EventArgs e)
        {
            var result = await new Metodos().UProducto(Convert.ToInt32(TxtCantidadProductos.Text), App.Codigo);
            Acr.UserDialogs.UserDialogs.Instance.Toast("¡Cantidad Modificada con Exito!");
            BtnModificar.IsVisible = false;
            BtnEliminar.IsVisible = false;
            TxtCantidadProductos.IsVisible = false;
            FrameCantidadProductos.IsVisible = false;

            var apiResult = await metodos.GetListadoProductos();
            lsv_productos.ItemsSource = apiResult;
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var result = await new Metodos().DProducto(Convert.ToInt32(App.Codigo));
            Acr.UserDialogs.UserDialogs.Instance.Toast("¡Producto Eliminado con Exito!");
            BtnEliminar.IsVisible = false;
            BtnModificar.IsVisible = false;
            FrameCantidadProductos.IsVisible = false;
            TxtCantidadProductos.IsVisible = false;
            var apiResult = await metodos.GetListadoProductos();
            lsv_productos.ItemsSource = apiResult;
        }
    }
}

using Acr.UserDialogs;
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
        ToastConfigClass toastConfig = new ToastConfigClass();
        readonly Metodos metodos = new Metodos();

        public MainPage()
        {
            InitializeComponent();
            TxtPrecioEnvio.Text = "210";
            btnImgHome.Source = "homeAmarillo.png";

            lsv_productos.ItemSelected += Lsv_productos_ItemSelected;

            gridCalculator.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    LayoutProductos.IsVisible = false;
                    StackLayoutAddProducts.IsVisible = false;
                    StackLayoutDashboard.IsVisible = false;
                    btnImgHome.Source = "home.png";
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
                    StackLayoutDashboard.IsVisible = false;
                    btnImgHome.Source = "home.png";
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
                    StackLayoutDashboard.IsVisible = false;
                    btnImgHome.Source = "home.png";
                    StackLayoutAddProducts.IsVisible = true;
                    LayoutProductos.IsVisible = true;
                    btnAddProduct.Source = "addAmarillo.png";
                    StackLayourCalculator.IsVisible = false;
                    btnImgCalculator.Source = "calculator.png";
                    btnProducts.Source = "products.png";
                }),
                NumberOfTapsRequired = 1
            });

            gridHome.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutProducts.IsVisible = false;
                    StackLayoutDashboard.IsVisible = false;
                    StackLayoutAddProducts.IsVisible = false;
                    btnImgHome.Source = "homeAmarillo.png";
                    StackLayoutDashboard.IsVisible = true;
                    LayoutProductos.IsVisible = false;
                    btnAddProduct.Source = "add.png";
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
                App.NombreParaVender = element.Nombre;
                App.PrecioParaVender = element.Precio.ToString(); ;
                App.GananciaParaVender =  element.Precio;
                App.CantidadProductosEnLaCompra =  element.CantidadProductosEnLaCompra;
                App.PrecioDelEnvioEnLaCompra =  element.PrecioDelEnvioEnLaCompra;
                Acr.UserDialogs.UserDialogs.Instance.Toast("");
                toastConfig.MostrarNotificacion($"¡Seleccione que desea hacer, eliminar o modificar!", ToastPosition.Top, 3, "#51C560");
                TxtCantidadProductos.IsVisible = true;
                TxtCantidadProductos.Text = element.Cantidad.ToString();
                BtnModificar.IsVisible = true;
                FrameCantidadProductos.IsVisible = true;
                BtnCancelar.IsVisible = true;
                BtnEliminar.IsVisible = true;
                BtnVender.IsVisible = true;

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
                toastConfig.MostrarNotificacion($"¡No se pudo conectar, intente mas tarde!", ToastPosition.Top, 3, "#B02828");
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
                    toastConfig.MostrarNotificacion($"¡Por favor llenar el campo del nombre!", ToastPosition.Top, 3, "#B02828");
                }
                else if (string.IsNullOrEmpty(TxtPrecio.Text))
                {
                    toastConfig.MostrarNotificacion($"¡Por favor llenar el campo del precio!", ToastPosition.Top, 3, "#B02828");
                }
                else if (string.IsNullOrEmpty(TxtCantidad.Text))
                {
                    toastConfig.MostrarNotificacion($"¡Por favor llenar el campo de cantidad!", ToastPosition.Top, 3, "#B02828");
                }
                else if (string.IsNullOrEmpty(TxtCantidadProductosEnLaCompra.Text))
                {
                    toastConfig.MostrarNotificacion($"¡Por favor llenar el campo de los productos!", ToastPosition.Top, 3, "#B02828");
                }
                else if (string.IsNullOrEmpty(TxtPrecioDelEnvioEnLaCompra.Text))
                {
                    toastConfig.MostrarNotificacion($"¡Por favor llenar el campo del precio en la compra!", ToastPosition.Top, 3, "#B02828");
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                    var response = await metodos.SentenciaProductos(TxtNombre.Text.ToUpper(), Convert.ToInt32(TxtPrecio.Text), Convert.ToInt32(TxtCantidad.Text), Convert.ToInt32(TxtCantidadProductosEnLaCompra.Text), Convert.ToInt32(TxtPrecioDelEnvioEnLaCompra.Text));
                    var apiResult = await metodos.GetListadoProductos();
                    lsv_productos.ItemsSource = apiResult;
                    TxtNombre.Text = "";
                    TxtPrecio.Text = "";
                    TxtCantidad.Text = "";
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    toastConfig.MostrarNotificacion($"¡Producto Agregado Con Exito!", ToastPosition.Top, 3, "#51C560");
                }
            }
            catch (Exception ex)
            {
                toastConfig.MostrarNotificacion($"¡No se pudo agregar el producto, intente mas tarde!", ToastPosition.Top, 3, "#B02828");
            }
        }

        void BtnLimpiar_Clicked(System.Object sender, System.EventArgs e)
        {
            TxtCantidadProductosQueLlegaron.Text = "";
            TxtValorDelProductoEnPesos.Text = "";
            TxtPrecioEnvio.Text = "";
            TxtValorTotal.Text = "";
            toastConfig.MostrarNotificacion($"¡Se han limpiado los campos!", ToastPosition.Top, 3, "#51C560");

        }

        private async void BtnModificar_Clicked(object sender, EventArgs e)
        {
            var result = await new Metodos().UProducto(Convert.ToInt32(TxtCantidadProductos.Text), App.Codigo);
            toastConfig.MostrarNotificacion($"¡Cantidad Modificada con Exito!", ToastPosition.Top, 3, "#51C560");

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
            toastConfig.MostrarNotificacion($"¡Producto Eliminado con Exito!", ToastPosition.Top, 3, "#51C560");
            BtnEliminar.IsVisible = false;
            BtnModificar.IsVisible = false;
            FrameCantidadProductos.IsVisible = false;
            TxtCantidadProductos.IsVisible = false;
            var apiResult = await metodos.GetListadoProductos();
            lsv_productos.ItemsSource = apiResult;
        }

        private void btnCalculadora_Clicked(object sender, EventArgs e)
        {
            LayoutProductos.IsVisible = false;
            StackLayoutAddProducts.IsVisible = false;
            StackLayoutDashboard.IsVisible = false;
            btnImgHome.Source = "home.png";
            btnImgCalculator.Source = "calculatorAmarillo.png";
            StackLayourCalculator.IsVisible = true;
            StackLayoutProducts.IsVisible = false;
            btnProducts.Source = "products.png";
            btnAddProduct.Source = "add.png";
            lsv_productos.IsVisible = false;
        }

        private void btnInventario_Clicked(object sender, EventArgs e)
        {
            StackLayoutAddProducts.IsVisible = false;

            LayoutProductos.IsVisible = true;
            btnProducts.Source = "productsAmarillo.png";
            StackLayoutDashboard.IsVisible = false;
            btnImgHome.Source = "home.png";
            StackLayourCalculator.IsVisible = false;
            StackLayoutProducts.IsVisible = true;
            btnImgCalculator.Source = "calculator.png";
            btnAddProduct.Source = "add.png";
            lsv_productos.IsVisible = true;
        }

        private void btnAgregarProductos_Clicked(object sender, EventArgs e)
        {
            StackLayoutProducts.IsVisible = false;
            StackLayoutDashboard.IsVisible = false;
            btnImgHome.Source = "home.png";
            StackLayoutAddProducts.IsVisible = true;
            LayoutProductos.IsVisible = true;
            btnAddProduct.Source = "addAmarillo.png";
            StackLayourCalculator.IsVisible = false;
            btnImgCalculator.Source = "calculator.png";
            btnProducts.Source = "products.png";
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            BtnModificar.IsVisible = false;
            BtnEliminar.IsVisible = false;
            BtnCancelar.IsVisible = false;
            FrameCantidadProductos.IsVisible = false;
        }

        private async void BtnVender_Clicked(object sender, EventArgs e)
        {
            double PrecioTotalMasLasGanancias = App.GananciaParaVender / App.CantidadProductosEnLaCompra * 0.30;
            var result = await new Metodos().UProducto(Convert.ToInt32(TxtCantidadProductos.Text), App.Codigo);
            var result2 = await new Metodos().IProductoVendido(App.NombreParaVender, Convert.ToInt32(App.PrecioParaVender), Convert.ToInt32(TxtCantidadProductos.Text), Convert.ToInt32(PrecioTotalMasLasGanancias), Convert.ToInt32(App.CantidadProductosEnLaCompra), Convert.ToInt32(FrPrecioDelEnvioEnLaCompra));
            toastConfig.MostrarNotificacion($"¡Producto Vendido con Exito!", ToastPosition.Top, 3, "#51C560");

        }
    }
}

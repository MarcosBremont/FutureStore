﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureStore.Models;
using Rg.Plugins.Popup.Services;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutureStore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalCantidad : Rg.Plugins.Popup.Pages.PopupPage
    {
        public event EventHandler OnLLamarOtraPantalla;

        public ModalCantidad()
        {
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
            }
            catch (Exception ex)
            {

            }

        }

        async void BtnModificar_Clicked(object sender, EventArgs e)
        {
        }


    }
}
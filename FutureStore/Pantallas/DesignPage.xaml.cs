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

       
    }
}
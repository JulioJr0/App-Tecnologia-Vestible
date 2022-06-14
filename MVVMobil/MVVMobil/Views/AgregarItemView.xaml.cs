using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarItemView : ContentPage
    {
        public AgregarItemView()
        {
            InitializeComponent();
        }

        private void Regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();

        }

        private void Agregar_Clicked(object sender, EventArgs e)
        {

        }
    }
}
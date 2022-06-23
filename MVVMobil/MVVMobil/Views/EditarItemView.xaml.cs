using MVVMobil.ViewModels;
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
    public partial class EditarItemView : ContentPage
    {
        public EditarItemView()
        {
            InitializeComponent();
        }

        private void ButtonToSave_Clicked(object sender, EventArgs e)
        {
            _ = new bool[2];
            _ = new bool[2];
            _ = new bool[4];

            bool[] UNO = (bool[])sta2.BindingContext;
            bool[] DOS = (bool[])sta3.BindingContext;
            bool[] TRES = (bool[])sta.BindingContext;

            var x = (ItemsViewModel)BindingContext;

            x.ComprobarDevolucionesGratisCommand.Execute(DOS); //Verifica dos booleanos
            x.ComprobarEnvioGratisCommand.Execute(UNO); //Verifica dos booleanos
            x.BeforeToSave(TRES); //Válida y Guarda
        }
    }
}
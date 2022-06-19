﻿using MVVMobil.ViewModels;
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
            //x.CambiarVistaCommand.Execute()
        }

        private void Regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();

        }

        private void Agregar_Clicked(object sender, EventArgs e)
        {

        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            bool[] UNO = new bool[2];
            bool[] DOS = new bool[2];
            bool[] TRES = new bool[4];

            UNO = (bool[])(sta2.BindingContext);
            DOS = (bool[])(sta3.BindingContext);
            TRES = (bool[])(sta.BindingContext);

            var x = (ItemsViewModel)BindingContext;
            x.CambiarVistaCommand.Execute("Agregado1");
            x.ComprobarEnvioGratisCommand.Execute(UNO);
            x.ComprobarDevolucionesGratisCommand.Execute(DOS);
            x.AgregarCommand.Execute(TRES);
        }
    }
}
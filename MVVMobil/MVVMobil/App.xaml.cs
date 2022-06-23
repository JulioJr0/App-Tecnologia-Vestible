using MVVMobil.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: ExportFont("BebasNeue-Regular.ttf", Alias = "BebasNeue-Regular")]
[assembly: ExportFont("BIZUDGotic-Regular.ttf", Alias = "BIZUDGotic-Regular")]
[assembly: ExportFont("Mada-ExtraLight.ttf", Alias = "Mada-ExtraLight")]
[assembly: ExportFont("SawarabiGothic-Regular.ttf", Alias = "SawarabiGothic-Regular")]
[assembly: ExportFont("Rubik-VariableFont_wght.ttf", Alias = "Rubik-VariableFont_wght")]


namespace MVVMobil
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListaItemsView());
        }
        // DetallesItemView
        // ListaItemsView
        // AgregarItemView
        //EditarItemView
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

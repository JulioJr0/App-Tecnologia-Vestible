using MVVMobil.Models;
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
    public partial class ListaItemsView : ContentPage
    {

        //public string TituloCorto;

        public ListaItemsView()
        {
            InitializeComponent();
            // StackLayout_SubCategorias

            //foreach (var child in StackLayout_SubCategorias.Children)
            //{
            //    (child as StackLayout).GestureRecognizers.Add(new TapGestureRecognizer(){
            //        Command = new Command(OnStackLayoutTap),
            //        CommandParameter = child
                    
            //    });
            //}
            //var x = (ItemsViewModel)BindingContext;

        }
        //private void OnStackLayoutTap(object obj)
        //{
        //    //(obj as StackLayout).BackgroundColor = Color.Black;
        //    //Task.Run(async () =>
        //    //{
        //    //    await (obj as StackLayout).RotateTo((obj as StackLayout).Rotation + 360, 1500);
        //    //     //(obj as StackLayout).BackgroundColor = Color.Black;
        //    //});
        //    //Task.Run(async () =>
        //    //{
        //    //    await (obj as StackLayout).ScaleTo(0.5, 750);
        //    //    await (obj as StackLayout).ScaleTo(1, 750);
        //    //});
        //}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarItemView());
        }

        //private void ImageButton_Clicked(object sender, EventArgs e)
        //{
        //     Navigation.PushAsync(new AgregarItemView());
        //}

        private void SwipeItem_Delete_Clicked(object sender, EventArgs e)
        {
            //var sw = (SwipeItem)sender; //unboxing
            //if (await DisplayAlert("Por favor confirme", $"¿Está seguro de eliminar a {((Alumno)sw.CommandParameter).Nombre} ?", "Si", "No") == true)
            //{
            //    avm.EliminarCommand.Execute(sw.CommandParameter);
            //}
        }

        private async void SwipeItem_Delete_Clicked_1(object sender, EventArgs e)
        {
            var sw = (SwipeItem)sender; // unboxing

            if ( await DisplayAlert("Por favor confirme", $"¿Estás seguro de eliminar el siguiente producto: {((Item)sw.CommandParameter).Nombre_Item}?", "Sí, quiero eliminarlo", "No") == true)
            {
                
                avm.EliminarCommand.Execute(sw.CommandParameter);
            }
        }
    }
}
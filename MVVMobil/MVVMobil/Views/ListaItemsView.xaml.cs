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
        public ListaItemsView()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarItemView());
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new AgregarItemView());
        }

        private void SwipeItem_Delete_Clicked(object sender, EventArgs e)
        {
            //var sw = (SwipeItem)sender; //unboxing
            //if (await DisplayAlert("Por favor confirme", $"¿Está seguro de eliminar a {((Alumno)sw.CommandParameter).Nombre} ?", "Si", "No") == true)
            //{
            //    avm.EliminarCommand.Execute(sw.CommandParameter);
            //}
        }
    }
}
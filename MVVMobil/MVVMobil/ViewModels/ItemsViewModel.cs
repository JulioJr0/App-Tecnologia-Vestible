using MVVMobil.Models;
using MVVMobil.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMobil.ViewModels
{
    public class ItemsViewModel : INotifyPropertyChanged
    {
        //Agregar
        public ObservableCollection<Item> Lista { get; set; } = new ObservableCollection<Item>();

        public Item Item { get; set; } //agregar, editar, eliminar
        public string Error { get; set; } = "";
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand ComprobarMedidaCommand { get; set; }


        public ItemsViewModel()
        {
            Deserializar();
            CambiarVistaCommand = new Command<string>(CambiarVista);
            AgregarCommand = new Command(Agregar);
            ComprobarMedidaCommand = new Command(ComprobarMedida);
        }

        private void ComprobarMedida(object obj)
        {
            
        }

        //PAGES
        AgregarItemView vistaItem;



        private void Agregar(object obj)
        {
            if (Item != null)
            {
                Error = "";
                if (string.IsNullOrWhiteSpace(Item.Nombre_Item))
                {
                    Error = "Escribe el NOMBRE del ITEM";
                }
                if (string.IsNullOrWhiteSpace(Item.Precio.ToString()))
                {
                    Error = "Escribe el PRECIO del ITEM";
                }
                if (string.IsNullOrWhiteSpace(Item.Nombre_Empresa_Vende))
                {
                    Error = "Escribe el NOMBRE de la empresa en la que se encuentra el ITEM";
                }
                if (string.IsNullOrWhiteSpace(Error)) //Agregar
                {
                    Lista.Add(Item);
                    CambiarVista("Ver");
                    Serializar();
                }
                Change();
            }
        }

        private void Change()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        private void CambiarVista(string vista)
        {
            if (vista == "Agregar")
            {
                Item = new Item(); //Aquí se guardarán los datos capturados al agregar
                vistaItem = new AgregarItemView() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(vistaItem);
            }
            else if(vista == "Ver")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        void Serializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "lista.json";
            File.WriteAllText(file, JsonConvert.SerializeObject(Lista));
        }
        void Deserializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "lista.json";
            if (File.Exists(file))
            {
                Lista = JsonConvert.DeserializeObject <ObservableCollection< Item >> (File.ReadAllText(file));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

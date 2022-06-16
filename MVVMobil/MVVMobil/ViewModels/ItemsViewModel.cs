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
    public class ItemsViewModel : INotifyPropertyChanged, IMultiValueConverter
    {
        //Agregar
        public ObservableCollection<Item> Lista { get; set; } = new ObservableCollection<Item>();
        public ushort Medida { get; set; }
        public bool EstadoVista { get; set; } = false;
        public Item Item { get; set; } //agregar, editar, eliminar
        public string Error { get; set; } = "";
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand ComprobarEnvioGratisCommand { get; set; }
        public ICommand ComprobarDevolucionGratisCommand { get; set; }

        public string[] combinacion = new string[5];
        public ItemsViewModel()
        {
            Deserializar();
            CambiarVistaCommand = new Command<string>(CambiarVista);
            AgregarCommand = new Command<object>(Agregar);
            ComprobarEnvioGratisCommand = new Command<bool>(ComprobarEnvioGratis);
            ComprobarDevolucionGratisCommand = new Command<bool>(ComprobarDevolucionGratis);
        }

        private void ComprobarDevolucionGratis(bool obj)
        {
            
        }

        private void ComprobarEnvioGratis(bool obj)
        {

        }

        //PAGES
        AgregarItemView vistaItem;



        private void Agregar(object obj)
        {
            EstadoVista = true;
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
                    Item.Tamaño = Medida;
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
            EstadoVista = false;

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
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool x0 = (bool)values[0];
            bool x1 = (bool)values[1];
            bool x2 = (bool)values[2];
            bool x3 = (bool)values[3];
            bool x4 = (bool)values[4];
            if (!(x0 == false && x1 == false && x2 == false && x3 == false && x4 == false))
            {
                try
                {
                    if (x0 == true)
                    {
                        //string name = values[0].ToString();
                        //return new Item { Numero_De_Reviews = ushort.Parse(age)};
                        return Medida = 0;

                    }
                    else if (x1 == true)
                    {
                        return Medida = 1;

                    }
                    else if (x2 == true)
                    {
                        return Medida = 2;
                    }
                    else if (x3 == true)
                    {
                        return Medida = 3;
                    }
                    else if (x4 == true)
                    {
                        return Medida = 4;

                    }
                }
                catch (NullReferenceException e)
                {
                    Error = e.Message; ;
                    //Code to do something with e
                }
                
            }

            return Medida = 0;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

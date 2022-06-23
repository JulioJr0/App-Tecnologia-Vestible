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
using System.Linq;
namespace MVVMobil.ViewModels
{
    public class ItemsViewModel : INotifyPropertyChanged/*, IMultiValueConverter*/
    {
        public IEnumerable<Item> ListaOrdenada => Lista.OrderBy(x => x.Nombre_Item);
        //Agregar
        public ObservableCollection<Item> Lista { get; set; } = new ObservableCollection<Item>();

        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand ComprobarEnvioGratisCommand { get; set; }
        public ICommand ComprobarDevolucionesGratisCommand { get; set; }
        public ICommand TapShowDetailsCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ToSaveCommand { get; set; }
        //public ICommand TapCommand { get; set; }
        //public ushort Medida { get; set; }
        public string EstadoVista { get; set; } = "Lista";
        public Item ItemPrincipal { get; set; } //agregar, editar, eliminar
        public string Error { get; set; } = "";
        public string FaltanteURL { get; set; } = "";
        public string Visible
        {
            get
            {
                if (!(Lista.Count != 0))
                {
                    return "Verdadero";
                }
                else
                {
                    return "Falso";
                }
            }
            set => Visible = value;
        }
        //public string[] combinacion = new string[5];
        //public bool[] EnvioYDevoluciones = new bool[4]; 
        //public bool[] Envio_Y_Devolucion_Gratis = new bool[4];
        //public string TapMessage { get; set; } = "";
        public ItemsViewModel()
        {
            Deserializar();
            CambiarVistaCommand = new Command<string>(CambiarVista);
            AgregarCommand = new Command<bool[]>(Agregar);
            EliminarCommand = new Command<Item>(Eliminar);
            ComprobarEnvioGratisCommand = new Command<bool[]>(ComprobarEnvioGratis);
            ComprobarDevolucionesGratisCommand = new Command<bool[]>(ComprobarDevolucionesGratis);
            TapShowDetailsCommand = new Command<Item>(TapShowDetails);
            EditCommand = new Command<Item>(Edit);
            ToSaveCommand = new Command<int>(ToSave);
            
            //TapCommand = new Command<string>(Tap);
        }
        int indice;
        public void BeforeToSave(bool[] obj)
        {
            if (ItemPrincipal != null)
            {
                string xx = ItemPrincipal.FaltanteURL1 + ItemPrincipal.FaltanteURL2;
                if (Uri.IsWellFormedUriString(xx, UriKind.Absolute))
                {
                    ItemPrincipal.URL_Completa = xx;
                }
                else
                {
                    Error = "Ingresa una URL válida para la imagen del ITEM";
                }
                if (Error == "Selecciona si el ITEM incluye ENVÍO GRATIS o no")
                {
                    Error = "Selecciona si el ITEM incluye ENVÍO GRATIS o no";
                }
                else if (Error == "Selecciona si el ITEM incluye GASTOS DE DEVOLUCIÓN GRATUITOS")
                {
                    Error = "Selecciona si el ITEM incluye GASTOS DE DEVOLUCIÓN GRATUITOS";
                }
                else if (string.IsNullOrWhiteSpace(ItemPrincipal.Nombre_Item))
                {
                    Error = "Escribe el NOMBRE del ITEM";
                }
                else if (string.IsNullOrWhiteSpace(ItemPrincipal.Precio))
                {
                    Error = "Escribe el PRECIO del ITEM";
                }
                else if (string.IsNullOrWhiteSpace(ItemPrincipal.Nombre_Empresa_Vende))
                {
                    Error = "Escribe el NOMBRE de la empresa en la que se encuentra el ITEM";
                }
                else if (string.IsNullOrWhiteSpace(ItemPrincipal.Descripción))
                {
                    Error = "Escribe una breve DESCRIPCIÓN del ITEM";
                }
                else if (string.IsNullOrWhiteSpace(ItemPrincipal.Numero_De_Reviews))
                {
                    Error = "Escribe la cantidad de REVIEWS que tiene el ITEM";
                }
                else if (!(string.IsNullOrWhiteSpace(ItemPrincipal.Numero_De_Reviews)))
                {
                    //string x = ItemPrincipal.Numero_De_Reviews;
                    //ItemPrincipal.Numero_De_Reviews = x + " " + "Reviews";
                    //string y = ItemPrincipal.Precio;
                    //ItemPrincipal.Precio = "$" + y;
                    if (obj[0] == false && obj[1] == false && obj[2] == false && obj[3] == false && obj[4] == false)
                    {
                        Error = "Selecciona una MEDIDA para el ITEM, puedes elegir 'UX' si no aplica ninguna medida";
                    }
                    if (string.IsNullOrWhiteSpace(Error)) //Agregar
                    {
                        if (obj[0])
                        {
                            ItemPrincipal.Tamaño = 0;
                        }
                        else if (obj[1])
                        {
                            ItemPrincipal.Tamaño = 1;
                        }
                        else if (obj[2])
                        {
                            ItemPrincipal.Tamaño = 2;
                        }
                        else if (obj[3])
                        {
                            ItemPrincipal.Tamaño = 3;
                        }
                        else if (obj[4])
                        {
                            ItemPrincipal.Tamaño = 4;
                        }
                        ToSave(0);
                    }
                }
            }
            Change();
            Error = "";
        }
        private void Edit(Item obj)
        {
            //Clonar
            indice = Lista.IndexOf(obj);
            ItemPrincipal = new Item()
            {
                Nombre_Item = obj.Nombre_Item,
                Nombre_Empresa_Vende = obj.Nombre_Empresa_Vende,
                Numero_De_Reviews = obj.Numero_De_Reviews,
                Devolucion_Gratis_SioNo = obj.Devolucion_Gratis_SioNo,
                Envio_Gratis_SioNo = obj.Envio_Gratis_SioNo,
                Descripción = obj.Descripción,
                Precio = obj.Precio,
                Tamaño = obj.Tamaño,
                FaltanteURL1 = obj.FaltanteURL1,
                FaltanteURL2 = obj.FaltanteURL2,
                URL_Completa = obj.URL_Completa
            };

            vistaEditar = new EditarItemView()
            {
                BindingContext = this
            };
            Application.Current.MainPage.Navigation.PushAsync(vistaEditar);
        }

        private void ToSave(int obj)
        {
            Lista[indice] = ItemPrincipal; //Remplaza el original por el clon
            Serializar();
            App.Current.MainPage.Navigation.PopToRootAsync();
        }

        private void TapShowDetails(Item obj)
        {
            vistaDetalles = new DetallesItemView()
            {
                BindingContext = obj
            };
            App.Current.MainPage.Navigation.PushAsync(vistaDetalles);
        }

        private void Eliminar(Item obj)
        {
            if (obj != null)
            {
                Lista.Remove(obj);
                Serializar();
            }
        }

        //private void Tap(string obj)
        //{
        //    if (obj == "TapSelect")
        //    {
        //        TapMessage = "TapSelect";
        //    }
        //    Change();
        //}

        private void ComprobarDevolucionesGratis(bool[] obj)
        {
            if (!(obj[0] == false && obj[1] == false))
            {
                if (obj[0])
                {
                    ItemPrincipal.Devolucion_Gratis_SioNo = true;
                }
                else
                {
                    ItemPrincipal.Devolucion_Gratis_SioNo = false;
                }
            }
            else
            {
                Error = "Selecciona si el ITEM incluye GASTOS DE DEVOLUCIÓN GRATUITOS";
            }
            Change();
        }

        private void ComprobarEnvioGratis(bool[] obj)
        {
            if (!(obj[0] == false && obj[1] == false))
            {
                if (obj[0])
                {
                    ItemPrincipal.Envio_Gratis_SioNo = true;
                }
                else
                {
                    ItemPrincipal.Envio_Gratis_SioNo = false;
                }
            }
            else
            {
                Error = "Selecciona si el ITEM incluye ENVÍO GRATIS o no";
            }
            Change();
        }
        
        //PAGES
        AgregarItemView vistaItem;
        DetallesItemView vistaDetalles;
        EditarItemView vistaEditar;

        private void Agregar(bool[] obj)
        {
            if (ItemPrincipal != null)
            {
                string xx = ItemPrincipal.FaltanteURL1 + ItemPrincipal.FaltanteURL2;
                if (Uri.IsWellFormedUriString(xx, UriKind.Absolute))
                {
                    ItemPrincipal.URL_Completa = xx;
                }
                else
                {
                    Error = "Ingresa una URL válida para la imagen del ITEM";
                }
                
                if (Error == "Selecciona si el ITEM incluye ENVÍO GRATIS o no")
                {
                    Error = "Selecciona si el ITEM incluye ENVÍO GRATIS o no";
                }
                else if (Error == "Selecciona si el ITEM incluye GASTOS DE DEVOLUCIÓN GRATUITOS")
                {
                    Error = "Selecciona si el ITEM incluye GASTOS DE DEVOLUCIÓN GRATUITOS";
                }
                else if (string.IsNullOrWhiteSpace(ItemPrincipal.Nombre_Item))
                {
                    Error = "Escribe el NOMBRE del ITEM";
                }
                else if ( string.IsNullOrWhiteSpace(ItemPrincipal.Precio))
                {
                    Error = "Escribe el PRECIO del ITEM";
                }
                else if (string.IsNullOrWhiteSpace(ItemPrincipal.Nombre_Empresa_Vende))
                {
                    Error = "Escribe el NOMBRE de la empresa en la que se encuentra el ITEM";
                }
                else if (string.IsNullOrWhiteSpace(ItemPrincipal.Descripción))
                {
                    Error = "Escribe una breve DESCRIPCIÓN del ITEM";
                }
                else if (string.IsNullOrWhiteSpace(ItemPrincipal.Numero_De_Reviews))
                {
                    Error = "Escribe la cantidad de REVIEWS que tiene el ITEM";
                }
                else if (!(string.IsNullOrWhiteSpace(ItemPrincipal.Numero_De_Reviews)))
                {
                    //string x = ItemPrincipal.Numero_De_Reviews;
                    //ItemPrincipal.Numero_De_Reviews = x + " " + "Reviews";
                    //string y = ItemPrincipal.Precio;
                    //ItemPrincipal.Precio = "$" + y;
                    if (obj[0] == false && obj[1] == false && obj[2] == false && obj[3] == false && obj[4] == false)
                    {
                        Error = "Selecciona una MEDIDA para el ITEM, puedes elegir 'UX' si no aplica ninguna medida";
                    }
                    if (string.IsNullOrWhiteSpace(Error)) //Agregar
                    {
                        if (obj[0])
                        {
                            ItemPrincipal.Tamaño = 0;
                        }
                        else if (obj[1])
                        {
                            ItemPrincipal.Tamaño = 1;
                        }
                        else if (obj[2])
                        {
                            ItemPrincipal.Tamaño = 2;
                        }
                        else if (obj[3])
                        {
                            ItemPrincipal.Tamaño = 3;
                        }
                        else if (obj[4])
                        {
                            ItemPrincipal.Tamaño = 4;
                        }
                        Lista.Add(ItemPrincipal);
                        CambiarVista("Ver");
                        Serializar();
                        EstadoVista = "Lista";
                    }
                }
                Change();
                Error = "";
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
                ItemPrincipal = new Item(); //Aquí se guardarán los datos capturados al agregar
                vistaItem = new AgregarItemView() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(vistaItem);
            }
            else if(vista == "Ver")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else if (vista == "Agregado1")
            {
                EstadoVista = "Agregado1";
            }
            else if (vista == "Editar")
            {
                
            }
            else if (vista == "Agregado3")
       
            Change();
        }

        void Serializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "lista.json";
            File.WriteAllText(file, JsonConvert.SerializeObject(Lista));
            EstadoVista = "";

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
        //public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    bool x0 = (bool)values[0];
        //    bool x1 = (bool)values[1];
        //    bool x2 = (bool)values[2];
        //    bool x3 = (bool)values[3];
        //    bool x4 = (bool)values[4];
        //    if (!(x0 == false && x1 == false && x2 == false && x3 == false && x4 == false))
        //    {
        //        try
        //        {
        //            if (x0 == true)
        //            {
        //                //string name = values[0].ToString();
        //                //return new Item { Numero_De_Reviews = ushort.Parse(age)};
        //                return Medida = 0;

        //            }
        //            else if (x1 == true)
        //            {
        //                return Medida = 1;

        //            }
        //            else if (x2 == true)
        //            {
        //                return Medida = 2;
        //            }
        //            else if (x3 == true)
        //            {
        //                return Medida = 3;
        //            }
        //            else if (x4 == true)
        //            {
        //                return Medida = 4;

        //            }
        //        }
        //        catch (NullReferenceException e)
        //        {
        //            Error = e.Message; ;
        //            //Code to do something with e
        //        }
                
        //    }

        //    return Medida = 0;
        //}
        //public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

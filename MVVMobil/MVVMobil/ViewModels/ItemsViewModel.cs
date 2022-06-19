﻿using MVVMobil.Models;
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
    public class ItemsViewModel : INotifyPropertyChanged/*, IMultiValueConverter*/
    {
        //Agregar
        public ObservableCollection<Item> Lista { get; set; } = new ObservableCollection<Item>();

        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand ComprobarEnvioGratisCommand { get; set; }
        public ICommand ComprobarDevolucionesGratisCommand { get; set; }
        //public ushort Medida { get; set; }
        public string EstadoVista { get; set; } = "Lista";
        public Item ItemPrincipal { get; set; } //agregar, editar, eliminar
        public string Error { get; set; } = "";
        public string[] combinacion = new string[5];
        public bool[] EnvioYDevoluciones = new bool[4]; 
        //public bool[] Envio_Y_Devolucion_Gratis = new bool[4];
        public ItemsViewModel()
        {
            Deserializar();
            CambiarVistaCommand = new Command<string>(CambiarVista);
            AgregarCommand = new Command<bool[]>(Agregar);
            ComprobarEnvioGratisCommand = new Command<bool[]>(ComprobarEnvioGratis);
            ComprobarDevolucionesGratisCommand = new Command<bool[]>(ComprobarDevolucionesGratis);
            //ComprobarMedidaItemCommand = new Command<string[]>(ComprobarMedidaItem);
        }

        private void ComprobarDevolucionesGratis(bool[] obj)
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

        private void ComprobarEnvioGratis(bool[] obj)
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

        //PAGES
        AgregarItemView vistaItem;
        //public void ComprobarEnvio()
        //{
        //    if (EnvioYDevoluciones[0] == true)
        //    {
        //        ItemPrincipal.Envio_Gratis_SioNo = true;
        //    }
        //    else
        //    {
        //        ItemPrincipal.Envio_Gratis_SioNo = false;
        //    }

        //    if (EnvioYDevoluciones[2] == true)
        //    {
        //        ItemPrincipal.Devolucion_Gratis_SioNo = true;
        //    }
        //    else
        //    {
        //        ItemPrincipal.Devolucion_Gratis_SioNo = false;
        //    }
        //}
        private void Agregar(bool[] obj)
        {
            if (ItemPrincipal != null)
            {
                Error = "";
                if (string.IsNullOrWhiteSpace(ItemPrincipal.Nombre_Item))
                {
                    Error = "Escribe el NOMBRE del ITEM";
                }
                if (string.IsNullOrWhiteSpace(ItemPrincipal.Precio.ToString()))
                {
                    Error = "Escribe el PRECIO del ITEM";
                }
                if (string.IsNullOrWhiteSpace(ItemPrincipal.Nombre_Empresa_Vende))
                {
                    Error = "Escribe el NOMBRE de la empresa en la que se encuentra el ITEM";
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
                    //ComprobarEnvio();
                    
                    Lista.Add(ItemPrincipal);
                    CambiarVista("Ver");
                    Serializar();
                    EstadoVista = "Lista";
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
            else if (vista == "Agregado2")
            {
                EstadoVista = "Agregado2";
            }
            else if (vista == "Agregado3")
            {
                EstadoVista = "Agregado3";
            }
            else if (vista == "Agregado4")
            {
                EstadoVista = "Agregado4";
            }
            else if (vista == "Agregado5")
            {
                EstadoVista = "Agregad5";
            }
            else if (vista == "Agregado6")
            {
                EstadoVista = "Agregado6";
            }
            else if (vista == "Agregado7")
            {
                EstadoVista = "Agregado7";
            }
            else if (vista == "Agregado8")
            {
                EstadoVista = "Agregado8";
            }
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

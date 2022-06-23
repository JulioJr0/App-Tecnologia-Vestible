using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMobil.Models
{
    public class Item
    {
        //String
        public string Nombre_Item { get; set; } = "";
        public string Nombre_Empresa_Vende { get; set; } = "";
        public string Descripción { get; set; } = "";
        public string Numero_De_Reviews { get; set; } = "";
        public string Precio { get; set; } = "";
        public string FaltanteURL1 { get; set; } = "";
        public string FaltanteURL2 { get; set; } = "";
        public string URL_Completa { get; set; } = "";
        //Bool
        public bool Envio_Gratis_SioNo { get; set; }
        public bool Devolucion_Gratis_SioNo { get; set; }
        //Ushort
        public ushort Tamaño { get; set; }
    }

}

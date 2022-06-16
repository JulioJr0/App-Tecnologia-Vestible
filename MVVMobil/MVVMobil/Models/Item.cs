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
        public bool Envio_Gratis_SioNo { get; set; }
        public bool Devolucion_Gratis_SioNo { get; set; }
        //Ushort
        public ushort Tamaño { get; set; }
        //Int
        public int Numero_De_Reviews { get; set; }
        //Decimal
        public decimal Precio { get; set; }
    }

}

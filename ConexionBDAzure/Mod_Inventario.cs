using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBDAzure
{
    public class Mod_Inventario
    {
        //Atributos
        public int IdInventario { get; set; }
        public string NombreCorto { get; set; }
        public string Descripcion { get; set; }
        public string Serie { get; set; }
        public string Color { get; set; }
        public string FechaAdquision { get; set; }
        public string TipoAdquision { get; set; }
        public string observaciones { get; set; }
        public int Area_id { get; set; }
        public string NombreArea { get; set; }
        

        //Constructor

        public Mod_Inventario(int IdInventario, string NombreCorto, string Descripcion, string Serie,
            string Color, string FechaAdquision, string TipoAdquision, string observaciones, int area)
        {
            this.IdInventario = IdInventario;
            this.NombreCorto = NombreCorto;
            this.Descripcion = Descripcion;
            this.Serie = Serie;
            this.Color = Color;
            this.FechaAdquision = FechaAdquision;
            this.TipoAdquision = TipoAdquision;
            this.observaciones = observaciones;
            this.Area_id = area;
        }

        public Mod_Inventario(int IdInventario, string NombreCorto, string Descripcion, string Serie,
            string Color, string FechaAdquision, string TipoAdquision, string observaciones, string NombreArea)
        {
            this.IdInventario = IdInventario;
            this.NombreCorto = NombreCorto;
            this.Descripcion = Descripcion;
            this.Serie = Serie;
            this.Color = Color;
            this.FechaAdquision = FechaAdquision;
            this.TipoAdquision = TipoAdquision;
            this.observaciones = observaciones;
            this.NombreArea = NombreArea;
        }
    }
}

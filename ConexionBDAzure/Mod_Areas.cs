using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBDAzure
{
    public class Mod_Areas
    {
        //Atributos
        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }


        //Constructor
        public Mod_Areas(int IdArea, string Nombre, string Ubicacion)
        {
            this.IdArea = IdArea;
            this.Nombre = Nombre;
            this.Ubicacion = Ubicacion;
        }
    }
    
}

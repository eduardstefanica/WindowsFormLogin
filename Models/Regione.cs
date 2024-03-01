using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsLogin.Models
{
    public class Regione
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool isAutonoma { get; set; }
        public int NumAbitanti { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsLogin.Models
{
    public class Provincia
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int IdRegione { get; set; }
        public bool isCapoluogo { get; set; }
        public int NumAbitanti { get; set; }
    }
}

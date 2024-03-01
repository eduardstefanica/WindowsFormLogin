using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsLogin
{
    public class Utente
    {
        public string nome { get; set; }
        public string cognome { get; set; }
        public string sesso { get; set; }
        public DateTime data_di_nascita { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Boolean isValid { get; set; }
    }
}

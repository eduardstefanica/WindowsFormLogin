using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsLogin.Models;

namespace WindowsFormsLogin.DataLayer
{
    public interface IListeDati
    {
        List<Utente> LoadUtente();
        List<Regione> LoadRegione();
        List<Provincia> LoadProvincia();
        List<Comune> LoadComune();
    }
}

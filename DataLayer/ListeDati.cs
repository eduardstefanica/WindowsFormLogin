using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsLogin.Models;

namespace WindowsFormsLogin.DataLayer
{
    public class ListeDati : IListeDati
    {
        // SERILOG
        private readonly ILogger _logger;
        public ListeDati(ILogger logger) {
            _logger = logger;
        }

        public List<Utente> LoadUtente()
        {
            string sMsg = string.Empty;
            List<Utente> listUtenti = null;
            Utente oUt = null;

            try
            {
                listUtenti = new List<Utente>();

                oUt = new Utente();
                oUt.nome = "Patrizio";
                oUt.cognome = "Tardiolo Bonifazi";
                oUt.sesso = "M";
                oUt.email = "p.tardiolobonifazi@vivasoft.it";
                oUt.password = "password";
                oUt.isValid = true;
                oUt.data_di_nascita = DateTime.Now;

                listUtenti.Add(oUt);

                oUt = new Utente();
                oUt.nome = "Eduard";
                oUt.cognome = "Stefanica";
                oUt.sesso = "M";
                oUt.email = "eduardstefanicacard@gmail.com";
                oUt.password = "password";
                oUt.isValid = true;
                oUt.data_di_nascita = DateTime.Now;

                listUtenti.Add(oUt);

                oUt = new Utente();
                oUt.nome = "Mario";
                oUt.cognome = "Rossi";
                oUt.sesso = "M";
                oUt.email = "m.rossi@vivasoft.it";
                oUt.password = "password";
                oUt.isValid = true;
                oUt.data_di_nascita = DateTime.Now;

                listUtenti.Add(oUt);

                oUt = new Utente();
                oUt.nome = "Giuseppe";
                oUt.cognome = "Verdi";
                oUt.sesso = "M";
                oUt.email = "g.verdi@vivasoft.it";
                oUt.password = "password";
                oUt.isValid = true;
                oUt.data_di_nascita = DateTime.Now;

                listUtenti.Add(oUt);

            }
            catch (Exception ex)
            {
                sMsg = string.Format("Source:{0}{3}Message:{1}{3}StackTrace:{2}{3}", ex.Source, ex.Message, ex.StackTrace, Environment.NewLine);
                _logger.Error(sMsg);
            }

            return listUtenti;
        }

        public List<Regione> LoadRegione()
        { 
            string sMsg = string.Empty;
            List<Regione> listRegioni = null;
            Regione oReg = null;

            try
            {
                listRegioni = new List<Regione>();
                oReg = new Regione();
                oReg.ID = 1;
                oReg.Nome = "Umbria";
                oReg.isAutonoma = false;
                oReg.NumAbitanti = 500000;
                listRegioni.Add(oReg);

                oReg = new Regione();
                oReg.ID = 2;
                oReg.Nome = "Lazio";
                oReg.isAutonoma = false;
                oReg.NumAbitanti = 6500000;

                listRegioni.Add(oReg);

                oReg = new Regione();
                oReg.ID = 3;
                oReg.Nome = "Friuli Venezia Giulia";
                oReg.isAutonoma = true;
                oReg.NumAbitanti = 1150000;

                listRegioni.Add(oReg);

            }
            catch(Exception ex) { 
                sMsg = string.Format("Source:{0}{3}Message:{1}{3}StackTrace:{2}{3}", ex.Source, ex.Message, ex.StackTrace, Environment.NewLine);
                _logger.Error(sMsg);
            }

            return listRegioni;
        }

        public List<Provincia> LoadProvincia()
        {
            string sMsg = string.Empty;
            List<Provincia> listProvince = null;
            Provincia oProv = null;
            try
            {
                listProvince = new List<Provincia>();
                oProv = new Provincia();
                oProv.ID = 1;
                oProv.Nome = "Perugia";
                oProv.IdRegione = 1;
                oProv.isCapoluogo = true;
                oProv.NumAbitanti = 150000;
                listProvince.Add(oProv);

                oProv = new Provincia();
                oProv.ID = 2;
                oProv.Nome = "Terni";
                oProv.IdRegione = 1;
                oProv.isCapoluogo = false;
                oProv.NumAbitanti = 100000;
                listProvince.Add(oProv);

                oProv = new Provincia();
                oProv.ID = 3;
                oProv.Nome = "Roma";
                oProv.IdRegione = 2;
                oProv.isCapoluogo = true;
                oProv.NumAbitanti = 5000000;
                listProvince.Add(oProv);
            }
            catch (Exception ex)
            {
                sMsg = string.Format("Source:{0}{3}Message:{1}{3}StackTrace:{2}{3}", ex.Source, ex.Message, ex.StackTrace, Environment.NewLine);
                _logger.Error(sMsg);
            }
            return listProvince;
        }

        public List<Comune> LoadComune()
        {
            string sMsg = string.Empty;
            List<Comune> listComuni = null;
            Comune oCom = null;
            try
            {
                listComuni = new List<Comune>();
                oCom = new Comune();
                oCom.ID = 1;
                oCom.Nome = "Gubbio";
                oCom.IdProvincia = 1;
                oCom.NumAbitanti = 30000;
                listComuni.Add(oCom);

                oCom = new Comune();
                oCom.ID = 2;
                oCom.Nome = "Narni";
                oCom.IdProvincia = 2;
                oCom.NumAbitanti = 28000;
                listComuni.Add(oCom);

                oCom = new Comune();
                oCom.ID = 3;
                oCom.Nome = "Roma";
                oCom.IdProvincia = 3;
                oCom.NumAbitanti = 5000000;
                listComuni.Add(oCom);

                oCom = new Comune();
                oCom.ID = 4;
                oCom.Nome = "Tivoli";
                oCom.IdProvincia = 3;
                oCom.NumAbitanti = 15000;
                listComuni.Add(oCom);
            }
            catch (Exception ex)
            {
                sMsg = string.Format("Source:{0}{3}Message:{1}{3}StackTrace:{2}{3}", ex.Source, ex.Message, ex.StackTrace, Environment.NewLine);
                _logger.Error(sMsg);
            }
            return listComuni;
        }
    }
}

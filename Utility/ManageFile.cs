using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsLogin.Utility
{
    public class ManageFile
    {

        public void LoadJson()
        {
            using (StreamReader r = new StreamReader("file.json"))
            {
                string json = r.ReadToEnd();
                // List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hw_2_c_
{
    public class tabella
    {
        public void modif(DataGridView d,List<dati> lista,string cosa)                 // funziona , adesso quando viene chiamata prima ripulisci
        {

            d.ColumnCount = 4;
            d.Columns[0].Name = cosa;            // si vede !!!!!!!!!!!!!!!!!!
            d.Columns[1].Name = "absolute frequency";            
            d.Columns[2].Name = "absolute frequency";            
            d.Columns[3].Name = "percentage frequency";   
            foreach( dati info in lista)
            {
                d.Rows.Add(info.nome,info.assoluto, info.relativo, info.percentuale);
            }

        }

        public void con_dic(DataGridView d, Dictionary<string, int> fin)                 // cerca di sistemarlo come fatto sopra
        {

            d.ColumnCount = fin.Count();
            List<string> nomi = fin.Keys.ToList();
            List<int> v = fin.Values.ToList();

            d.Columns[0].Name = "intervallo x";           
            d.Columns[1].Name = "absolute frequency";

            for (int i = 0; i < fin.Count; i++)
            {
                d.Rows.Add(nomi[i],v[i],"","");
            }



        }

        public void clr(DataGridView d)
        {
            d.DataSource = null;
            d.Rows.Clear();
            d.Columns.Clear();  
            d.Refresh();
        }



    }
}

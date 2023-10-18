using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Diagnostics.Eventing.Reader;
using System.Collections;

namespace hw_2_c_
{


    public partial class Form1 : Form
    {


        //public static string s;

        //--------------------------------- ricava valori per 2 (dovresti aggiustare con una unica)
        static public List<string> parse_file_joint(int cosa,int cosa1, string percorso)      // FUNZIONA 
        {

            Console.WriteLine(percorso);  // ok
            //string[] res = new string[60];        // all the value in the survey

            List<string> res = new List<string>(); // all values 2 column
           




            string filePath = percorso;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int dove = 0;
                    int skip = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (skip != 0)
                        {
                            // Split the line by commas
                            string[] fields = line.Split(';');


                            // Extract values


                            
                            string value = fields[cosa].Trim().ToLower();        // considero value1 come non multivalore
                            //Console.WriteLine($"questo è il value primo campo {value}");


                            string value1 = fields[cosa1].Trim().ToLower();
                            //Console.WriteLine($"questo è il value secondo campo {value1}");
                            string[] t = value1.Split(',');        // potrebbe esserci doppio multivalore happiness,time - c# python ecc


                            if (value1 != null && value != "" && value!=null)
                            {
                                if (t.Length > 1)  // 
                                {
                                    //Console.Write("multivalore");
                                    foreach (string elem in t)
                                    {
                                        //Console.WriteLine("Value from the x field: " + elem);
                                        //Console.WriteLine($"unisco elem {elem} a {value}");
                                        string temp = elem.Trim();
                                        //Console.WriteLine("cosa sto vedendo ==");
                                        //Console.WriteLine(elem);
                                        //Console.WriteLine(value);
                                        string s = value + "-" + temp;
                                        //Console.WriteLine(s);
                                        res.Add(s);

                                    }
                                }

                                else
                                {
                                    //Console.WriteLine("non multivalore");
                                    //Console.WriteLine("lunghezza == ");
                                    //Console.WriteLine(t.Length);
                                    //Console.WriteLine("Value from the x field: " + value);
                                    string s1 = value + "-" + value1;
                                    //Console.WriteLine(s1);
                                    res.Add(s1);

                                }

                            }




                        }
                        skip = 1;

                    }
                    //Console.WriteLine("finito ??");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }
            return res;
        }

        // ---------------------------------        funzione ricava valori
        static public List<string> parse_file(int cosa,string percorso)
        {
           
            Console.WriteLine(percorso);  // ok
            //string[] res = new string[60];        // all the value in the survey

            List<string> res = new List<string>(); // all values of a column




            string filePath = percorso;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int dove = 0;
                    int skip = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (skip != 0)
                        {
                            // Split the line by commas
                            string[] fields = line.Split(';');


                            // Extract values


                            string value = fields[cosa].Trim().ToLower();
                            string[] t = value.Split(',');


                            if (value != null)
                            {
                                if (t.Length > 1)  // il fatto che ognuno ha >1 valore rende la lunghezza non prevedibile??
                                {
                                    Console.Write("multivalore");
                                    foreach (string elem in t)
                                    {
                                        //Console.WriteLine("Value from the x field: " + elem);
                                        res.Add(elem.Trim());

                                    }
                                }

                                else
                                {
                                    //Console.WriteLine("lunghezza == ");
                                    //Console.WriteLine(t.Length);
                                    //Console.WriteLine("Value from the x field: " + value);
                                    res.Add(value);

                                }

                            }
                         



                        }
                        skip = 1;

                    }
                    //Console.WriteLine("finito ??");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }
            return res;
        }


        //---------------------------------     FUNZIONE DIZIONARI


        static public Dictionary<string, int> crea_dictionary(List<string> valori)  // devi gestire quelli con multivalore ( lo fa nonso come)
        {
            Dictionary<string, int> res = new Dictionary<string, int>();
            foreach (string elem in valori)
            {
                if (res.ContainsKey(elem))
                    {
                    
                    res[elem] = res[elem]+1;
                }
                else
                {
                    res.Add(elem, 1);
                }
            }




            return res;
        } 

        //------------------------------------    FUNZIONE PERCENTUALI (qui popola lista db+spawna la tabella)

        static public List<dati> percentuali(Dictionary<string,int> info,string cosa)
        {
            

            List<string> d_keys = new List<string>();
            List<int> d_values = new List<int>();

            List<dati> result = new List<dati>();
            //DataTable d = new DataTable();

            //DataGridView datagridview2 = new DataGridView();
            //datagridview2.Controls.Add(datagridview2);




            int tot = 0;
            float verifica = 0;
            int l = 0;

            foreach (string key in info.Keys)
            {
                d_keys.Add(key);
                l = l + 1;
            }
            foreach (int value in info.Values)
            {
                d_values.Add(value);
                tot = tot + value;
            }

            // calcola totale elementi 

            Console.Write("somma == ");
            Console.WriteLine(tot);


            Console.WriteLine(cosa);

          

            /*d.Columns.Add(cosa, typeof(string));
            d.Columns.Add("Absolute frequency", typeof(string));
            d.Columns.Add("relative frequency", typeof(string));
            d.Columns.Add("percentage frequency", typeof(string));*/

            for (var i = 0; i < l; i++)
            {
                string k = d_keys[i];
                int v = d_values[i];
                float relative = (float)v / (float)tot;
                verifica = verifica + relative;
                //crea classe (sono 54 valori) ognuno con la sua statistica
                Console.WriteLine($"Absolute frequency for {k} == {v}");
                Console.WriteLine($"Percentage frequency for {k} == {relative * 100} %");
                Console.WriteLine($"Relative frequency for {k} ({v}) == {relative}");

                dati stat = new dati();
                stat.nome = k;
                stat.assoluto = v;
                stat.relativo = relative;
                stat.percentuale = relative*100;

                result.Add(stat);


                //d.Rows.Add(k, v,relative,relative*100);


            }

            



            //datagridview2.DataSource = d;


            Console.WriteLine(verifica);     // controllo percentuali
            return result;
        }


   

        //------------------------           FUNZIONE wrapup


        static public List<dati> wrap_up(int q,string cosa)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            List<string> values = new List<string>();


            // prende valori singola colonna

            values = parse_file(q, Form1.selectedFilePath);  //i
            foreach (string val in values)
            {

                {
                    //Console.WriteLine(val);
                }
            }

            //        crea dizionario associato         


            map = crea_dictionary(values);
            foreach (var pair in map)
            {
                string key = pair.Key;                            // stampa di circostanza
                int value = pair.Value;
                Console.WriteLine(key + ": " + value);
            }

            ///////////////// calcolo percentuali  GIUSTO
           


            List<dati> r = percentuali(map,cosa);
            Console.WriteLine("da qui");
            foreach (dati val in r)
            {            
                   Console.WriteLine(val.nome);
                
            }




            return r;
            
        }

   

        





        public static string selectedFilePath = "";
        public static DataTable d = new DataTable();
        public static tabella t = new tabella();






            public Form1()                             ///////////////////--------------             principale
        {
            InitializeComponent();

            // chiede file csv

            //string selectedFilePath = "";


            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select a File";
                openFileDialog.Filter = "All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    //MessageBox.Show("Selected file: " + selectedFilePath, "File Selected");
                }
            }

    
           









        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("test2");    // funge andando in progetto->proprietà-> tipo di output-> console
            int quale = comboBox1.SelectedIndex;


            
           

            Console.WriteLine($"item selezionato == {quale}");
            Console.WriteLine(comboBox1.GetItemText(this.comboBox1.SelectedItem));

            
            List<dati> r = wrap_up(quale,comboBox1.GetItemText(comboBox1.SelectedItem));                                                      // qui viene chiamata
            


            tabella t = new tabella();
            t.clr(dataGridView2);
            t.modif(dataGridView2,r,comboBox1.GetItemText(comboBox1.SelectedItem));         // FUNZIONA TUTTO 










        }

        private void button2_Click(object sender, EventArgs e)
        {
            int quale = comboBox2.SelectedIndex;
            int quale1 = comboBox3.SelectedIndex;

            string val = comboBox2.GetItemText(this.comboBox2.SelectedItem);
            string val1 = comboBox3.GetItemText(this.comboBox3.SelectedItem);

            string s = val + val1;

            List<string> values = new List<string>();
            Dictionary<string, int> map = new Dictionary<string, int>();
            values = parse_file_joint(quale, quale1, selectedFilePath);
            /*foreach (string elem in values)
            {
                Console.WriteLine(elem);
            }*/
            map=crea_dictionary(values);
            foreach (var pair in map)
            {
                string key = pair.Key;                            // stampa di circostanza
                int value = pair.Value;
                Console.WriteLine(key + ": " + value);
            }
            // il dizionario è corretto, adesso alle percentuali
            List<dati>  r = percentuali(map, s);

            Console.WriteLine("da qui (due variabili)");
            foreach (dati v in r)
            {
                Console.WriteLine(v.nome);

            }


            tabella t = new tabella();
            t.clr(dataGridView2);
            t.modif(dataGridView2, r, s);



            





        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

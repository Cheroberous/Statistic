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
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq;

namespace hw_2_c_
{


    public partial class Form1 : Form
    {


        //public static string s;

        //--------------------------------- // dovrei in automatico fare un dizionario con le associazioni per ogni colonna
        static public List<string> parse_file_joint(int cosa, int cosa1, string percorso)      // FUNZIONA , le combo non riportate non esistono ovviamente (estenti a k var)
        {

            Console.WriteLine(percorso);  // ok
            //string[] res = new string[60];        // all the value in the survey

            List<string> res = new List<string>(); // all values 2 column

            //C: \Users\desil\Desktop\Statistics\H2\hw_2_c#\new_d.csv



            string filePath = percorso;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int skip = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (skip != 0)
                        {
                            // Split the line by commas
                            string[] fields = line.Split(';');


                            // Extract values                // se una delle due è vuota come faccio la combinazione?? (gestito nelll'if la scarta)



                            string value = fields[cosa].Trim().ToLower();        // considero value1 come non multivalore
                            //Console.WriteLine($"questo è il value primo campo {value}");


                            string value1 = fields[cosa1].Trim().ToLower();
                            //Console.WriteLine($"questo è il value secondo campo {value1}");
                            string[] t = value1.Split(',');        // potrebbe esserci doppio multivalore happiness,time - c# python ecc


                            if (value1 != null && value != "" && value != null) // anche per le k, se una var nella riga non esiste (o vuota) butto tuto
                            {
                                if (t.Length > 1)  // 
                                {
                                    //Console.Write("multivalore");
                                    foreach (string elem in t)                                          // per ogni elemento nel multivalore aggiunge player-happi, player-time
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
                                    string s1 = value + "-" + value1;                              // se entrambi singoli aggiunge player-value
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

        // var interesse  
        static public List<string> parse_file(int cosa, string percorso)
        {

            Console.WriteLine(percorso);


            List<string> res = new List<string>(); // all values of a column




            string filePath = percorso;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int skip = 0; // skip first line
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (skip != 0)
                        {
                            // Split the line by commas
                            string[] fields = line.Split(';');


                            // Extract values


                            string value = fields[cosa].Trim().ToLower();    // valore se singolo (es 23 anni)

                            string[] t = value.Split(',');                  // se multivalore 


                            if (value != null && value != "")
                            {
                                if (t.Length > 1)  // il fatto che ognuno ha >1 valore rende la lunghezza non prevedibile??
                                {
                                    Console.Write("multivalore");
                                    foreach (string elem in t)
                                    {
                                        //Console.WriteLine("Value from the x field: " + elem);      // per ogni subvalore aggiungo
                                        res.Add(elem.Trim());

                                    }
                                }

                                else
                                {
                                    //Console.WriteLine("lunghezza == ");
                                    //Console.WriteLine(t.Length);
                                    //Console.WriteLine("Value from the x field: " + value);
                                    res.Add(value);                                                 // aggiungo valore singolo

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

                    res[elem] = res[elem] + 1;
                }
                else
                {
                    res.Add(elem, 1);
                }
            }




            return res;
        }

        //------------------------------------    FUNZIONE PERCENTUALI (qui popola lista db+spawna la tabella)

        static public List<dati> percentuali(Dictionary<string, int> info, string cosa)
        {


            List<string> d_keys = new List<string>();
            List<int> d_values = new List<int>();

            List<dati> result = new List<dati>();               // restituisce lista con una colonna

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
                stat.percentuale = relative * 100;

                result.Add(stat);


                //d.Rows.Add(k, v,relative,relative*100);


            }





            //datagridview2.DataSource = d;


            Console.WriteLine(verifica);     // controllo percentuali
            return result;
        }




        //------------------------           FUNZIONE wrapup


        static public List<dati> wrap_up(int q, string cosa)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            List<string> values = new List<string>();


            // prende valori singola colonna

            values = parse_file(q, Form1.selectedFilePath);  //                           lista di una colonna di valori
            foreach (string val in values)
            {

                {
                    //Console.WriteLine(val);
                }
            }

            //        crea dizionario associato         


            map = crea_dictionary(values);                    // crea valore-occorrenze dizionario {23 anni: 10 volte, 26 anni: 3 volte}
            foreach (var pair in map)
            {
                string key = pair.Key;                            // stampa di circostanza
                int value = pair.Value;
                Console.WriteLine(key + ": " + value);
            }

            ///////////////// calcolo percentuali  GIUSTO



            List<dati> r = percentuali(map, cosa);           // dato dizionario estrae percentuali e le mette in grafica
            Console.WriteLine("da qui");
            foreach (dati val in r)
            {
                Console.WriteLine(val.nome);

            }




            return r;

        }


        static public void conta_righe_el_lista()                            // in teoria solo per le continue disc (fatto per tutti e funziona) 
        {

            string[] fields;
            string[] attr;
            string[] dash;

            int quante = 0;
            try
            {
                using (StreamReader reader = new StreamReader(selectedFilePath))
                {
                    string line = reader.ReadLine();
                    fields = line.Split(';');

                    cols = fields.Count();                                          //
                    Console.WriteLine($"quanti campi == {fields.Count()}");

                    for (int i = 0; i < fields.Count(); i++)
                    {
                        Diz_tot.Add(fields[i], new List<string> { });
                        // chiavi = intestazioni

                    }

                    line = reader.ReadLine();             // test

                    while (line != null)
                    {

                        quante++;
                        attr = line.Split(';');




                        //Console.WriteLine($"quanti attributi trova == {attr.Count()}");

                        for (int i = 0; i < attr.Count(); i++)
                        {
                            //Console.WriteLine($" trovato == {attr[i]}");

                        }


                        for (int i = 0; i < attr.Count(); i++)
                        {
                            if (attr[i] == null || attr[i] == "")
                            {
                                attr[i] = "0";
                            }

                            dash = attr[i].Split('-');                                                // dizionario     age: lista valori, altezza:lista valori
                            if (dash != null)
                            {
                                for (int j = 0; j < dash.Count(); j++)
                                {
                                    Diz_tot[fields[i]].Add(dash[j].Trim().ToLower());

                                }
                            }
                            else
                            {
                                Diz_tot[fields[i]].Add(attr[i].Trim().ToLower());           //
                                                                                            //Diz_tot[fields[i]].Add(int.Parse(attr[i].Trim()));           //
                                                                                            //Console.WriteLine($"aggiunto a {fields[i]} la stringa {attr[i]}");
                            }


                        }
                        line = reader.ReadLine();



                    }
                    Console.WriteLine($"quante =={quante}");
                    rows = quante;                                                //
                }
            }
            catch (Exception ex)
            {

            }


        }


        static public void stampa(List<List<string>> l)
        {
            foreach (List<string> lista in l)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    Console.WriteLine($"elemento == {lista[i]}");
                }
            }
        }
        static public void stampa_d(Dictionary<string, List<string>> d)
        {
            foreach (var coppia in d)
            {
                Console.WriteLine("Chiave: " + coppia.Key);
                Console.WriteLine("Valori:");
                foreach (var valore in coppia.Value)
                {
                    Console.WriteLine(valore);
                }
                Console.WriteLine();
            }
        }



        /// --------------------------------------------------------------------------------------

        //  QUI 


        public static List<List<(float, float)>> totale_var_int = new List<List<(float, float)>>(); // lista di liste di tuple (intervalli)


        static public Dictionary<string,int> joint_k(string intervals, List<int> indici_var,List<string> nomi_var)             // 3,4    x,y   age,weight
        {


            foreach (List<(float,float)> innerList in totale_var_int)
            {
                innerList.Clear();
            }

            totale_var_int.Clear();


            string[] interv = intervals.Split(',');      // 3,4

            for (int i = 0; i < indici_var.Count; i++)
            {
                List<(float, float)> k = new List<(float, float)>();
                k=dividi_classi(indici_var[i], nomi_var[i], int.Parse(interv[i]));                                               // per ogni var creo lista tuple intervalli THIS
                foreach((float,float) elem in k)
                {
                    //Console.WriteLine($"intervallo {elem.Item1} {elem.Item2}");           // giusto

                }
                totale_var_int.Add(k);
            }

            // prendi una "riga"  guarda gli elementi di interesse, controlla 

            // 18,19   (age,weight) (2)


            return una_volta(nomi_var);
            


        }

       

        public static Dictionary<string,int> una_volta(List<string> attributi)       // scritta da cani? si, funziona? si
        {


            // sono 3
            //riga 0

        Dictionary<string,int> intervalli_quanti = new Dictionary<string, int>();                         // per mappatura nomi indici continue



        string nome_key_joint ="";
            for (int k = 0; k < rows; k++)
            {


                for (int i = 0; i < attributi.Count; i++)
                {
                    string interval_name = check_interval(attributi[i], i, Diz_tot[attributi[i]][k]);                 // quale intervallo " age[0] "
                    if (interval_name == "valore nullo")
                    {
                        nome_key_joint = "no";
                        break;

                    }
                    else
                    {
                        nome_key_joint = nome_key_joint + interval_name + "-";
                    }

                }
                // agginugi chiave al dizionario
                string tmp = nome_key_joint.Substring(0, nome_key_joint.Length - 1);

                if (nome_key_joint != "no")
                {
                    if (intervalli_quanti.ContainsKey(tmp))
                    {
                        intervalli_quanti[tmp]++;
                    }
                    else
                    {
                        intervalli_quanti.Add(tmp, 1);
                    }
                }

                Console.WriteLine(nome_key_joint);
                nome_key_joint = "";
            }

            // stampa diz
            Console.WriteLine("INIZIO STAMPA DIZIONARIO SPERO ULTIMO");
            foreach (var kvp in intervalli_quanti)
            {
                string key = kvp.Key;
                int value = kvp.Value;
                Console.WriteLine($"Key: {key}, Value: {value}");
            }


            return intervalli_quanti;



        }

        public static string check_interval(string nome,int index,string valore)  // ritorna nome intervallo in cui cade valore
        {
            // il valore è un numero ma va convertito
            float valore1 = float.Parse(valore);
            Console.WriteLine($"valore analizzato == {valore1}");
            string init="";
            string fin="";
            string s = "";

            if (valore1 == 0)
            {
                return "valore nullo";
            }

            foreach (var interval in totale_var_int[index])         // come valore ha lista di tuple
            {
                int intervallo = totale_var_int[index].Count-1;
                if (valore1 == totale_var_int[index][intervallo].Item2)
                {
                    init = totale_var_int[index][intervallo].Item1.ToString();
                    fin = totale_var_int[index][intervallo].Item2.ToString();

                    s = "("+init + ";" + fin + ")";
                    return s;

                }

                if (valore1 >= interval.Item1 && valore1 < interval.Item2)
                {
                     s = "("+interval.Item1.ToString() + ";" + interval.Item2.ToString()+")";

                    init = interval.Item1.ToString();
                    fin = interval.Item2.ToString();
                    return s;
                }
                
            }
            
            s = init + "," + fin;

            return s;
        }


        // crea dizionario intervallo valori : quanti                               // il tutto per una sola variabile
        // crea dizionario intervallo valori1 : quanti
        static public List<(float, float)> dividi_classi(int index, string nome, int intervalli)        // numero intervalli , quando ho hard worker e simili gli intervalli vengono decimali

        {

            Dictionary<(float, float), int> intervals_quanti = new Dictionary<(float, float), int>(); // questo  è per una variabile
            List<(float, float)> tuple_intervalli = new List<(float, float)>();

            foreach (string elem in Diz_tot[nome])
            {
                //Console.WriteLine($"elemento prima == {elem}");
            }

            List<float> l = new List<float>();



            l = Diz_tot[nome].Select(float.Parse).ToList();                        // converto la lista in float

            float max = l.Max();
            
            float min = l.Min();

            float smallest = l.Max();
            float secondSmallest=l.Max();

            if (nome == "height" || nome == "weight")
            {

                foreach (float number in l)
                {
                    if (number < smallest)
                    {
                        secondSmallest = smallest;
                        smallest = number;
                    }
                    else if (number < secondSmallest && number != smallest)
                    {
                        secondSmallest = number;
                    }
                }
                min = secondSmallest;
            }

            /*Console.WriteLine($"max == {max}");
            Console.WriteLine($"min == {min}");
            Console.WriteLine($"intervalli richiesti == {intervalli}");*/



            float dim = (max - min) / (float)intervalli;                          // grandezza ogni intervallo

            //Console.WriteLine($"larghezza intervalli == {dim}");


     

            for (int i = 0; i < intervalli; i++) // 3
            {
                float s = min + i * dim;
                float s1 = s + dim;
                intervals_quanti.Add((s, s1), 0);

                /*if (i == intervalli - 1)
                {
                    //s1 = s1 + 0.1f;                                                                                // per ora funge (per ultimo valore)
                    tuple_intervalli.Add((s, s1));          // aggiunge intervallo a lista intervalli

                }*/
                
                tuple_intervalli.Add((s, s1));          // aggiunge intervallo a lista intervalli

                

            }


            //  INTERVALS_QUANTI NON USATA (posso togliere)
            
            foreach (var kvp in tuple_intervalli)
            {
                Console.WriteLine($"Key: ({kvp.Item1}, {kvp.Item2})");     // stampa le tuple in cui poso mettre i valori
            }


            foreach (float elem in l)
            {

                //Console.WriteLine(elem);

                if (elem == max)
                {
                    int ultimo = tuple_intervalli.Count-1;
                    var ultim_int = tuple_intervalli[ultimo];
                    Console.WriteLine($"ultimo intervallo == {ultim_int.Item1} {ultim_int.Item2}");

                    intervals_quanti[(ultim_int.Item1, ultim_int.Item2)]++; // uso il nome dell'intervallo per ritrovare la chiave nel dizionario

                }
                else
                {
                    foreach (var interval in tuple_intervalli)
                    {
                        if (elem == 0) break;



                        if (elem >= interval.Item1 && elem < interval.Item2)
                        {
                            float v_2 = (float)Math.Round(interval.Item2, 2);
                            //Console.WriteLine($"inserisco elemento == {elem} in intervallo {interval.Item1} {v_2}");

                            //try
                            //{
                            intervals_quanti[(interval.Item1, interval.Item2)]++; // uso il nome dell'intervallo per ritrovare la chiave nel dizionario
                                                                                  // break;
                                                                                  //}
                                                                                  //catch
                                                                                  //{
                                                                                  //    intervals_quanti[(interval.Item1, interval.Item2 - 0.1f)]++;           // chettata
                                                                                  //}
                        }
                    }
                }
            }





            foreach (var kvp in intervals_quanti)
            {
                Console.WriteLine($"Key: ({kvp.Key.Item1}, {kvp.Key.Item2}), Value: {kvp.Value}");     // stampa dizionario tuple-quanti
            }


            return tuple_intervalli;


        }





        public static int rows = 0;
        public static int cols = 0;
        public static string selectedFilePath = "";
        public static DataTable d = new DataTable();
        public static tabella t = new tabella();

        public static List<List<string>> l_l_a = new List<List<string>>();
        public static Dictionary<string, List<string>> Diz_tot = new Dictionary<string, List<string>>();   // questo ha tutti i valori in fila (usalo per controllare righe (sempre 62 valori)

        public static List<List<(float, float)>> l_l_intervalli = new List<List<(float, float)>>();        // var 0 (int1),(int2),(int3) ; var 1 (int1),(int2)


        // mappatura

        //public static Dictionary<string, Tuple<(int, int)>> map1 = new Dictionary<string, Tuple<(int, int)>>();

        public static Dictionary<string,(int, int)> map1 = new Dictionary<string,(int,int)>();                         // per mappatura nomi indici continue


      








        /* Hard Worker(0-5)
         Team leader or Team player?
         Enterpreneurial attitude(0-5)
         Preferred Workload
         Age
         weight
         height*/







        public Form1()                             ///////////////////--------------             principale
        {
            InitializeComponent();
            selectedFilePath = "..\\..\\new_d.csv";
            conta_righe_el_lista();                             // crea dizionario con chiave nome e valore valori di nome (per tutti)

            //stampa_d(Diz_tot);
            map1.Add("Hard Worker (0-5)", (0, 4));
            map1.Add("Ambitious (0-5)", (0, 5));
            map1.Add(" Team leader or Team player?", (1, 8));
            map1.Add("Enterpreneurial attitude (0-5)", (2, 11));
            map1.Add("Preferred Workload", (3, 9));
            map1.Add("Age", (4, 16));
            map1.Add("weight", (5, 17));
            map1.Add("height", (6, 18));
          


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("test2");    // funge andando in progetto->proprietà-> tipo di output-> console
            int quale = comboBox4.SelectedIndex;


            Console.WriteLine($"item selezionato == {quale}");
            Console.WriteLine("nome == ");
            Console.WriteLine(comboBox4.GetItemText(comboBox4.SelectedItem));

            
                                   // var scelta      // indice
            List<dati> r = wrap_up(quale,comboBox4.GetItemText(comboBox4.SelectedItem));                                                      // qui viene chiamata
            


            tabella t = new tabella();
            t.clr(dataGridView2);
            t.modif(dataGridView2,r,comboBox4.GetItemText(comboBox4.SelectedItem));         // FUNZIONA TUTTO 


        }

        private void button3_Click(object sender, EventArgs e)
        {


            List<int> quali=new List<int>();
            List<string> nomi=new List<string>();                          
            string intervalli = textBox1.Text;



            foreach (string itemChecked in checkedListBox1.CheckedItems)
            {
                //quali.Add(itemChecked);
                nomi.Add(itemChecked);
                quali.Add(map1[itemChecked].Item2);
                //Console.WriteLine($"this == {itemChecked}");
            }


            /*for(int i = 0; i < nomi.Count(); i++)
            {
                Console.WriteLine($"this == {nomi[i]} indice == {quali[i]}");

            }*/
          


            Dictionary<string,int> ved=joint_k(intervalli, quali, nomi);                        // this
            tabella t = new tabella();
            t.clr(dataGridView2);
            t.con_dic(dataGridView2,ved);



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

        private void label1_Click(object sender, EventArgs e)
        {

        }

   
    }
}

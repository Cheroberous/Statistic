using Microsoft.VisualBasic.FileIO;
using System;
using System.Drawing;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace App_SamplingDist_C    // max e min del relative trovato, dividi hai le classi

{
    public partial class Form1 : Form
    {



        EditableRectangle r1;

        Bitmap b;
        Graphics g;

        float lunghezza_tratto;
        float altezza_tratto;
        float h_r;




        List<Pen> pens = new List<Pen>();
        List<Pen> pens_g = new List<Pen>();

        List<List<int>> listOfLists = new List<List<int>>();
        List<int> som_lol = new List<int>();






        int linee = 10;
        int sample = 100;



        int n_linee = 10;
        int n_sample = 100;


        double lambda = 0;
        double prob_lambda = 0;

        int max_y = 0;
        int min_y = 0;



        Random rand = new Random();


        //SortedDictionary<Interval, int> dist_mean;
        //SortedDictionary<Interval, int> dist_variance;

        public Form1()
        {


            InitializeComponent();
            initialize_graphics(); // OK
        }

        public void create_pens()
        {

            pens.Clear();
            pens_g.Clear();
            for (int i = 0; i < linee; i++)
            {
                int red = rand.Next(256); // 0-255
                int green = rand.Next(256); // 0-255
                int blue = rand.Next(256); // 0-255


                Color randomColor = Color.FromArgb(red, green, blue);
                Pen peng = new Pen(randomColor, 1);

                pens.Add(peng);
                //Console.WriteLine("aggiunta penna");
            }
            for (int i = 0; i < linee; i++)
            {
                int red = rand.Next(256); // 0-255
                int green = rand.Next(256); // 0-255
                int blue = rand.Next(256); // 0-255


                Color randomColor = Color.FromArgb(red, green, blue);
                Pen peng = new Pen(randomColor, 1);

                pens_g.Add(peng);
                //Console.WriteLine("aggiunta penna");
            }

        }

        public void initialize_stat()
        {
            max_y = 0;
            foreach(List<int> l in listOfLists)
            {
                l.Clear();
            }
            listOfLists.Clear();
            som_lol.Clear();

            for (int i = 0; i < linee; i++)
            {
                som_lol.Add(0);
                List<int> a = new List<int>();
                int local_max = 0;
                for (int j = 0; j < sample; j++)
                {

                    double randomDouble = (double)rand.NextDouble();
                    //Console.WriteLine($"valore usci vs prob == {randomFloat} {prob_lambda}");
                    if (randomDouble < prob_lambda)
                    {
                        a.Add(+1);
                        local_max++;
                        som_lol[i]++;
                        //Console.WriteLine($"attacco di successo linea {i}");
                        //Console.WriteLine($"valore usci vs prob == {randomDouble} {prob_lambda}");

                    }
                    else
                    {
                        a.Add(0);


                    }
                }
                if (local_max > max_y) max_y = local_max;
                local_max = 0;
                listOfLists.Add(a);
            }
         

            //Console.WriteLine($"massimo == {max_y}");
            /*for (int i = 0; i < som_lol.Count; i++)
            {
                Console.WriteLine($"valore == {som_lol[i]}");
            }*/
            








        }

        private void initialize_graphics()
        {
            //timer1.Start();
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);

            // coordinate iniziali                                              // this passa il form
            r1 = new EditableRectangle(20, 20, pictureBox1.Width / 2 +40, pictureBox1.Height / 2 +40, pictureBox1, this);

        }




        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            n_linee = trackBar1.Value;
            string labeltext = "Sample size -" + n_linee.ToString();
            label1.Text = labeltext;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            n_sample = trackBar2.Value;
            string labeltext = "Number of samples -" + n_sample.ToString();
            label2.Text = labeltext;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            timer1.Stop();



            sample = n_sample;
            linee = n_linee;
            lambda = (double)(numericUpDown3.Value);

            Console.WriteLine($"lambda == {lambda}");
            // controlla lambda<sample 


            prob_lambda = lambda / sample;

            initialize_stat();                          // modificato
            create_pens();                              // devo ricalcolarlo perchè cambia il numero di penne



            timer1.Start();



        }








        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(pictureBox1.BackColor);
            draw_rectangles();
            linechart();
            pictureBox1.Image = b;
        }


        // ----------------------------------------------- QUI  ---------------------------------------------------



        private void linechart()
        {
            //Console.WriteLine("quante linee quanti sample 2");
            //Console.WriteLine(linee);
            //Console.WriteLine(sample);


            Point centro_sempre = new Point(r1.r.Left, ((r1.r.Bottom - r1.r.Top) / 2) + r1.r.Top);
            Point fine = new Point(r1.r.Right, ((r1.r.Bottom - r1.r.Top) / 2) + r1.r.Top);

            Pen pen = new Pen(Color.Black, 3);
            g.DrawLine(Pens.Black, centro_sempre, fine);


            // guarda max_y
            altezza_tratto = ((r1.r.Bottom - r1.r.Top) / (max_y))/2;   // non mi piace

            // gestisci caso max_y=0
            //quasi ma con alti lambda va fuori
                                                                   

            lunghezza_tratto = (r1.r.Right - r1.r.Left) / (sample * 1.5f);    // onesto

            List<int> somme = new List<int>();
            List<int> somme_med = new List<int>(); // per instogramma a metà (magari migliora)
            //Console.WriteLine($"massimo brech == {max_y}");


            for (int j = 0; j < listOfLists.Count; j++){

               

                somme.Add(0);
                somme_med.Add(0);


                PointF p0 = new PointF(r1.r.Left, ((r1.r.Bottom - r1.r.Top) / 2) + r1.r.Top);   // centro

                Pen new_p = pens[j];
                PointF next;

                for (int i = 0; i < listOfLists[j].Count; i++)                  //for (int i = 0; i < stat.Count; i++)
                {

                    float p1x = (float)p0.X + lunghezza_tratto;
                    float p1y = (float)p0.Y - altezza_tratto;
                    float p1y_m = (float)p0.Y;




                    if (listOfLists[j][i] > 0)
                    {

                        next = new PointF(p1x, p1y);

                        g.DrawLine(new_p, p0, next);
                        p0 = next;


                    }
                    else
                    {
                        next = new PointF(p1x, p1y_m);
                        g.DrawLine(new_p, p0, next);
                        p0 = next;



                    }
                    //somme[j] = somme[j] + listOfLists[j][i];              // calcolalo in fase di init_stat

                    /*if (i < (sample / 2))
                    {
                        somme_med[j] = somme_med[j] + listOfLists[j][i];
                    }*/


                }



            }


            int delta = 2;
            int m = som_lol.Max();
            int min = som_lol.Min();
            int q_n = linee / delta;
            float q = ((float)(m - min) / (float)q_n);      // spaziatura
            //Console.WriteLine($"il massimo è {m} il minimo è {min}");
            //Console.WriteLine($"la spaziatura è da {q}");
            int intervalli = som_lol.Count / delta;

            /*foreach (int number in som_lol)
            {
                Console.WriteLine(number);
            }*/

            compute_instogram(som_lol, intervalli, q, 0);

            // per rettangoli istogramma






        }


        public void compute_instogram(List<int> val, int inter, float spaziatura, int first)       // l'index mi dice dove sulla inea
        {


          


            Dictionary<(float, float),int> intervalli_quanti_val = new Dictionary<(float, float), int>();
            List<(float, float)> tuple_intervalli = new List<(float, float)>();
            Rectangle r_insto;
            int x_s;


            x_s = (int)(r1.r.Left + (sample) * lunghezza_tratto);     //  la x è questa
            
            int y_s = r1.r.Bottom;
            int y_f = r1.r.Top;
            int spazio_rimasto_1 = r1.r.Right - x_s;


            //occupo max la metà della larghezza
            int larghezza_tutti =(y_s - y_f)/2;
            int base_rett =larghezza_tutti/inter;                                                     // buone le basi


            PointF p0 = new PointF(x_s, y_s);
            PointF p1 = new PointF(x_s, y_f);
            g.DrawLine(Pens.Magenta, p0, p1);  // linea verticale magenta


            //Console.WriteLine($"date linee {linee} faro {inter} rettangoli");


            float inizio_int = val.Min();

            for (int k = 0; k < inter; k++)       //dizionario intervalli -quanti
            {
                float fin = inizio_int + spaziatura;
                intervalli_quanti_val.Add((inizio_int,fin),0);
                tuple_intervalli.Add((inizio_int, fin));

                inizio_int = fin;

                
            }




            
            // popola diz

            for (int j = 0; j < inter; j++)       // dovrei fare lista tuple con intervalli
            {
                var t = tuple_intervalli[j];

                for (int i = 0; i < val.Count; i++)
                {
            
                    if (val[i] >= t.Item1 && val[i] < t.Item2)
                    {
                        //Console.WriteLine($"valore == {val[i]} in intervallo {t.Item1} a {t.Item2}");

                        intervalli_quanti_val[t]++;     // quanti di successo
                    }

                }
            }



            /*foreach (var kvp in tuple_intervalli)
            {
                Console.WriteLine($"intervallo: {kvp.Item1}; {kvp.Item2}");
            }*/


            //TEST
            /*foreach (var kvp in intervalli_quanti_val)
            {
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            }*/





            int h_max= (intervalli_quanti_val.Values).Max();
            //Console.WriteLine($"hmax == {h_max}");

            

           // Console.WriteLine($"spazio rimasto == {spazio_rimasto_1}");



            // THIS
            int a = 0;
            foreach (var kvp in intervalli_quanti_val)
            {
                //Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");



                int l = (spazio_rimasto_1 * kvp.Value) / h_max;                                         
                                                                                         
                //Console.WriteLine($"lunghezza rett == {l}");


                r_insto = new Rectangle(x_s, y_s - (base_rett) * (a + 1), l, base_rett);

                g.DrawRectangle(Pens.Black, r_insto);
                g.FillRectangle(Brushes.Red, r_insto);

                a++;
            }



















        }







        //qui disegno rettangoli
        private void draw_rectangles()
        {

            g.FillRectangle(Brushes.White, r1.r);
            g.DrawRectangle(Pens.Red, r1.r);


        }





    }
}


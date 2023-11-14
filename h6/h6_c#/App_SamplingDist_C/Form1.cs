using Microsoft.VisualBasic.FileIO;
using System;
using System.Drawing;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;



/* A system is discarded as "unsecure" if it reaches a penetration score of P before showing instead a security score of S.
   Simulate the distribution, respect to P = k*10 (k=2,...,10), of the probability of a system being discarded, conditional
   on the 3 cases for S: S = 20, S = 60, S = 100 (or any other value of S of your choice that you find useful to explore).

*/

namespace App_SamplingDist_C    // se va bene va giù

{
    public partial class Form1 : Form
    {



        EditableRectangle r1;
        EditableRectangle r2;
        EditableRectangle r3;

        Bitmap b;
        Graphics g;

        float lunghezza_tratto;
        float altezza_tratto;
        float h_r;





        List<Pen> pens = new List<Pen>();
        List<Pen> pens_g = new List<Pen>();

        List<List<int>> listOfLists = new List<List<int>>();
        List<int> som_lol = new List<int>();

        List<sistema> lista_sistemi = new List<sistema>();

        List<(int, int)> inter_p = new List<(int, int)>()
        {

            (20, 30),
            (30, 40),
            (40, 50),
            (50, 60),
            (60, 70),
            (70, 80),
            (80, 90),
            (90, 100)

        };

        List<int> fin = new List<int>();
        List<int> fin2 = new List<int>();
        List<int> fin3 = new List<int>();



        int linee = 10;
        int sample = 100;


        int n_linee = 10;
        int n_sample = 100;

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

        public void initialize_stat() // qui calcola già chi ha raggiunto la soglia (metti verso l'alto le penetration)
        {

            foreach (List<int> l in listOfLists)
            {
                l.Clear();
            }
            listOfLists.Clear();
            som_lol.Clear();
            lista_sistemi.Clear();
            fin.Clear();
            fin2.Clear();
            fin3.Clear();



            max_y = 0;
            min_y = 0;
            for (int i = 0; i < linee; i++)
            {
                som_lol.Add(0);                     // lista somma dei -1/+1 del sistema

                sistema sistema = new sistema();
                sistema.num_linea = i;


                List<int> a = new List<int>();       // lista con -1 e +1 del sistema


                for (int j = 0; j < sample; j++)
                {

                    double randomDouble = (double)rand.NextDouble();
                    //Console.WriteLine($"valore usci vs prob == {randomFloat} {prob_lambda}");
                    if (randomDouble < 0.5)
                    {
                        a.Add(+1);


                        som_lol[i]++;




                    }
                    else
                    {
                        som_lol[i]--;

                        a.Add(0);

                    }
                    // controllo dove è arrivata la linea del singolo grafico
                    // controllo primo grafico

                    // se tocca una di penetration e poi security non devo settare ad 1 la soglia di s (invece si, se ritorna ad una di penetartion non vale)

                    //Console.WriteLine($"la linea è a {som_lol[i]}");

                    // controlla non le soglie ma il p raggiunto
                  

                    if (som_lol[i] <= -20 && sistema.soglia1 != 1)
                    {
                        sistema.soglia1 = 1;
                        //Console.WriteLine("RAGGIUNTA SOGLIA 1");
                    }
                    if (sistema.soglia1 != 1)
                    {
                        for (int k = 0; k < inter_p.Count; k++)
                        {
                            var t = inter_p[k];
                            if (som_lol[i] > t.Item1 && som_lol[i] < t.Item2)
                            {
                                if (sistema.p_score1 < k + 1)
                                {
                                    //Console.WriteLine($"ragiunta soglia {k} sistema {sistema.num_linea}");
                                    sistema.p_score1 = k + 1;
                                }
                            }
                        }
                    }

                    if (som_lol[i] <= -60 && sistema.soglia2 != 1)
                    {
                        sistema.soglia2 = 1;
                        //Console.WriteLine("RAGGIUNTA SOGLIA 2");

                    }
                    if (sistema.soglia2 != 1)
                    {
                        for (int k = 0; k < inter_p.Count; k++)
                        {
                            var t = inter_p[k];
                            if (som_lol[i] > t.Item1 && som_lol[i] < t.Item2)
                            {
                                if (sistema.p_score2 < k + 1)
                                {
                                    sistema.p_score2 = k + 1;                        // non credo sia corretto, se scende sotto
                                }                                      // una soglia che aveva superato cambia ilp_score
                            }
                        }
                    }

                    if (som_lol[i] <= -100 && sistema.soglia3 != 1)
                    {
                        sistema.soglia3 = 1;
                        //Console.WriteLine("RAGGIUNTA SOGLIA 3");

                    }
                    if (sistema.soglia3 != 1)
                    {
                        for (int k = 0; k < inter_p.Count; k++)
                        {
                            var t = inter_p[k];
                            if (som_lol[i] > t.Item1 && som_lol[i] < t.Item2)
                            {
                                if (sistema.p_score3 < k + 1)
                                {
                                    sistema.p_score3 = k + 1;
                                }
                            }
                        }
                    }




                }
                Console.WriteLine("FINE LINEA");
                listOfLists.Add(a);

                // aggiungo lista sistema con 0/+1
                lista_sistemi.Add(sistema);

            }

            Console.WriteLine("-----   FINE SISTEMA -----");


            for (int i = 0; i < som_lol.Count; i++)                              // calcolo massimo in altezza  // dio santo non commentare (grazie)
            {
                //Console.WriteLine($"valore == {som_lol[i]}");      // usato ??
                int num = Math.Abs(som_lol[i]);
                if (num > max_y)
                {
                    max_y = num;
                }
             
            }




            //----------------    conta quanti in ogni intervallo intervalli                      // problema numero intervalli e come inserisce quante linee

            //Console.WriteLine($"quanti sistemi == {lista_sistemi.Count()}");

            for (int i = 0; i < inter_p.Count(); i++)
            {
                fin.Add(0);                           // tanti quanti gli intervalli
                fin2.Add(0);
                fin3.Add(0);

            }


            foreach (sistema s in lista_sistemi)
            {
                //Console.WriteLine($"sistema con p_score = {s.p_score1}");
                for (int i = s.p_score1; i > 0; i--)
                {
                    //Console.WriteLine($"Contatore == {i}");
                    fin[i - 1]++;
                }
                for (int i = s.p_score2; i > 0; i--)
                {
                    //Console.WriteLine($"Contatore == {i}");
                    fin2[i - 1]++;
                }
                for (int i = s.p_score3; i > 0; i--)
                {
                    //Console.WriteLine($"Contatore == {i}");
                    fin3[i - 1]++;
                }

            }

            int v = 0;
            Console.WriteLine("INIZIO --------------- 1");

            /*foreach (int elem in fin)
            {
                Console.WriteLine($"intervallo {v} quanti {elem}");      // interessante
                v++;
            }*/


            v = 0;
            /*
            Console.WriteLine("INIZIO --------------- 2");

            foreach (int elem in fin2)
            {
                Console.WriteLine($"intervallo {v} quanti {elem}");
                v++;

            }
            v = 0;

            Console.WriteLine("INIZIO --------------- 3");

            foreach (int elem in fin3)
            {
                Console.WriteLine($"intervallo {v} quanti {elem}");
                v++;
            }

            Console.WriteLine("FINE COSTRUZIONE");*/





        }

        private void initialize_graphics()
        {
            //timer1.Start();
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);

            // coordinate iniziali                                              // this passa il form
            r1 = new EditableRectangle(5, 5, (int)(pictureBox1.Width / 3.5) + 40, pictureBox1.Height / 3 + 40, pictureBox1, this);
            r3 = new EditableRectangle(5, 400, (int)(pictureBox1.Width / 3.5) + 40, pictureBox1.Height / 3 + 40, pictureBox1, this);
            r2 = new EditableRectangle(600, 5, (int)(pictureBox1.Width / 3.5) + 40, pictureBox1.Height / 3 + 40, pictureBox1, this);
            //blue
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


            //Console.WriteLine($"lambda == {lambda}");
            // controlla lambda<sample 




            initialize_stat();                          // modificato
            create_pens();                              // devo ricalcolarlo perchè cambia il numero di penne



            timer1.Start();



        }








        private void timer1_Tick(object sender, EventArgs e)
        {



            g.Clear(pictureBox1.BackColor);
            draw_rectangles();

       



            linechart(r1, fin);                                                                          // primo rettangolo
            linechart(r2, fin2);
            linechart(r3, fin3);

            int fine_1 = ((r1.r.Bottom - r1.r.Top) / 2) + r1.r.Top + 20 * (int)altezza_tratto;   // altezza linea
            Point prima = new Point(r1.r.Left + 5, fine_1);
            Point fine_P = new Point(r1.r.Left + (int)(lunghezza_tratto * sample) - 20, fine_1);
            int fine_2 = ((r2.r.Bottom - r2.r.Top) / 2) + r2.r.Top + 60 * (int)altezza_tratto;   // altezza linea
            Point prima2 = new Point(r2.r.Left + 5, fine_2);
            Point fine_P2 = new Point(r2.r.Left + (int)(lunghezza_tratto * sample) - 20, fine_2);
            int fine_3 = ((r3.r.Bottom - r3.r.Top) / 2) + r3.r.Top + 100 * (int)altezza_tratto;   // altezza linea
            Point prima3 = new Point(r3.r.Left + 5, fine_3);
            Point fine_P3 = new Point(r3.r.Left + (int)(lunghezza_tratto * sample) - 20, fine_3);

            Pen p = new Pen(Color.Black, 3);
            g.DrawLine(p, prima, fine_P);
            g.DrawLine(p, prima2, fine_P2);
            g.DrawLine(p, prima3, fine_P3);




            // replica per gli altri due chart

            pictureBox1.Image = b;
        }


        // ----------------------------------------------- QUI  ---------------------------------------------------



        public void draw_lines(EditableRectangle quale)
        {
            // disegno security score e P
            int i1 = ((quale.r.Bottom - quale.r.Top) / 2) + quale.r.Top - 20 * (int)altezza_tratto;
            int i2 = ((quale.r.Bottom - quale.r.Top) / 2) + quale.r.Top - 30 * (int)altezza_tratto;
            int i3 = ((quale.r.Bottom - quale.r.Top) / 2) + quale.r.Top - 40 * (int)altezza_tratto;
            int i4 = ((quale.r.Bottom - quale.r.Top) / 2) + quale.r.Top - 50 * (int)altezza_tratto;
            int i5 = ((quale.r.Bottom - quale.r.Top) / 2) + quale.r.Top - 60 * (int)altezza_tratto;
            int i6 = ((quale.r.Bottom - quale.r.Top) / 2) + quale.r.Top - 70 * (int)altezza_tratto;
            int i7 = ((quale.r.Bottom - quale.r.Top) / 2) + quale.r.Top - 80 * (int)altezza_tratto;
            int i8 = ((quale.r.Bottom - quale.r.Top) / 2) + quale.r.Top - 90 * (int)altezza_tratto;
            int i9 = ((quale.r.Bottom - quale.r.Top) / 2) + quale.r.Top - 100 * (int)altezza_tratto;

            Point p1i = new Point(quale.r.Left, i1);
            Point p1f = new Point(quale.r.Right, i1);

            Point p2i = new Point(quale.r.Left, i2);
            Point p2f = new Point(quale.r.Right, i2);

            Point p3i = new Point(quale.r.Left, i3);
            Point p3f = new Point(quale.r.Right, i3);

            Point p4i = new Point(quale.r.Left, i4);
            Point p4f = new Point(quale.r.Right, i4);

            Point p5i = new Point(quale.r.Left, i5);
            Point p5f = new Point(quale.r.Right, i5);

            Point p6i = new Point(quale.r.Left, i6);
            Point p6f = new Point(quale.r.Right, i6);

            Point p7i = new Point(quale.r.Left, i7);
            Point p7f = new Point(quale.r.Right, i7);

            Point p8i = new Point(quale.r.Left, i8);
            Point p8f = new Point(quale.r.Right, i8);

            Point p9i = new Point(quale.r.Left, i9);
            Point p9f = new Point(quale.r.Right, i9);

            Pen p = new Pen(Color.Orange, 3);

            g.DrawLine(p, p1i, p1f);
            g.DrawLine(p, p2i, p2f);
            g.DrawLine(p, p3i, p3f);
            g.DrawLine(p, p4i, p4f);
            g.DrawLine(p, p5i, p5f);
            g.DrawLine(p, p6i, p6f);
            g.DrawLine(p, p7i, p7f);
            g.DrawLine(p, p8i, p8f);
            g.DrawLine(p, p9i, p9f);
        }

        private void linechart(EditableRectangle quale_r, List<int> lista)
        {

            //Pen p = new Pen(Color.Black, 3);
            //g.DrawLine(p, li, fi);

            //Console.WriteLine("quante linee quanti sample 2");
            //Console.WriteLine(linee);
            //Console.WriteLine(sample);


            //altezza_tratto = ((float)(quale_r.r.Bottom - quale_r.r.Top) / 2 / (float)(max_y)) * 0.4f;        // funziona ma non mi piace // max = massimo ragginugibile nel grafico

            altezza_tratto = (float)(((float)(quale_r.r.Bottom - quale_r.r.Top) / 2) / (float)(max_y));       




            //lunghezza_tratto = (quale_r.r.Right - quale_r.r.Left) / (sample * 1.5f);

            lunghezza_tratto = (float)((quale_r.r.Right - quale_r.r.Left)*80/100) / sample;
            draw_lines(quale_r);


            // linea dimezzeria

            Point centro_sempre = new Point(quale_r.r.Left, ((quale_r.r.Bottom - quale_r.r.Top) / 2) + quale_r.r.Top);
            Point fine = new Point(quale_r.r.Right, ((quale_r.r.Bottom - quale_r.r.Top) / 2) + quale_r.r.Top);

            Pen pen = new Pen(Color.Black, 3);
            g.DrawLine(Pens.Black, centro_sempre, fine);

            //------




            //---------- colore non esatto non controlla altre soglie per gli altri sistemi


     
    
            //Console.WriteLine($"massimo brech == {max_y}");


            for (int j = 0; j < listOfLists.Count; j++)
            {

                //Console.WriteLine($"sistema {j} soglia1 == {lista_sistemi[j].soglia1}");
                Pen c=Pens.Red;
                if (quale_r == r1)
                {
                    if (lista_sistemi[j].soglia1 == 1)
                    {
                        c = Pens.Green;
                    }
                }
                if (quale_r == r2)
                {
                    if (lista_sistemi[j].soglia2 == 1)
                    {
                        c = Pens.Green;
                    }
                }
                if(quale_r == r3)
                {
                    if (lista_sistemi[j].soglia3 == 1)
                    {
                        c = Pens.Green;
                    }
                }
              

         
            


                PointF p0 = new PointF(quale_r.r.Left, ((quale_r.r.Bottom - quale_r.r.Top) / 2) + quale_r.r.Top);   // centro

                //Pen new_p = pens[j];
                PointF next;

                for (int i = 0; i < listOfLists[j].Count; i++)                  //for (int i = 0; i < stat.Count; i++)
                {

                    /*float p1x = (float)p0.X + lunghezza_tratto;
                    float p1y = (float)p0.Y - altezza_tratto;
                    float p1y_m = (float)p0.Y + altezza_tratto;*/

                    float p1x = (float)p0.X + lunghezza_tratto;
                    float p1y = (float)p0.Y - altezza_tratto;
                    float p1y_m = (float)p0.Y + altezza_tratto;




                    if (listOfLists[j][i] > 0)
                    {

                        next = new PointF(p1x, p1y);

                        g.DrawLine(c, p0, next);
                        p0 = next;


                    }
                    else
                    {
                        next = new PointF(p1x, p1y_m);
                        g.DrawLine(c, p0, next);
                        p0 = next;



                    }



                }



            }






            compute_instogram(lista, quale_r);





        }





        // ------------------------------------------------------------------------------------- -                                -------------------------------------------//




        public void compute_instogram(List<int> da_graficare, EditableRectangle quale_rettangolo) // da graicare ha intervalli e quanti nell'intervallo
        {


            RectangleF r_insto;
            int x_s;


            x_s = (int)(quale_rettangolo.r.Left + (sample) * lunghezza_tratto);     //  la x è questa

            int y_s = quale_rettangolo.r.Bottom - ((quale_rettangolo.r.Bottom - quale_rettangolo.r.Top) / 2);
            int y_f = quale_rettangolo.r.Top;
            int spazio_rimasto_1 = (quale_rettangolo.r.Right - x_s) / 2;       // meglio con /2 (dx)


            //occupo max la metà della larghezza

            int larghezza_tutti = (y_s - y_f) / 2;
            int base_rett = larghezza_tutti / 8;                                                     // buone le basi


            PointF p0 = new PointF(x_s, y_s);
            PointF p1 = new PointF(x_s, y_f);
            g.DrawLine(Pens.Magenta, p0, p1);  // linea verticale magenta




            int h_max = da_graficare.Max();
            //Console.WriteLine($"hmax == {h_max}");



            // Console.WriteLine($"spazio rimasto == {spazio_rimasto_1}");



            // THIS
            int a = 0;
            int bonus = 20;
            foreach (int kvp in da_graficare)
            {
                //Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
                if (kvp != 0)
                {

                    //Console.WriteLine($"percentuale occupata == {(float)(kvp/(float)h_max)}");

                    //Console.WriteLine($"altezza computata == {l}");

                    //int l = (int)((float)(spazio_rimasto_1 * kvp) / (float)h_max);

                    float l = (int)((float)(spazio_rimasto_1) * (kvp / (float)h_max));

                    //Console.WriteLine($"lunghezza rett == {l}");


                    r_insto = new RectangleF(x_s, y_s - (base_rett) * (a + 1) - bonus, (float)l, base_rett);

                    g.FillRectangle(Brushes.Red, r_insto);
                    g.DrawRectangle(Pens.Black, r_insto);
                }

                a++;
            }





        }







        //qui disegno rettangoli
        private void draw_rectangles()
        {

            g.FillRectangle(Brushes.White, r1.r);
            g.DrawRectangle(Pens.Red, r1.r);
            g.FillRectangle(Brushes.White, r2.r);
            g.DrawRectangle(Pens.Yellow, r2.r);
            g.FillRectangle(Brushes.White, r3.r);
            g.DrawRectangle(Pens.Blue, r3.r);


        }


    }
}


namespace App_SamplingDist_C
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            trackBar1 = new TrackBar();
            label1 = new Label();
            trackBar2 = new TrackBar();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(12, 38);
            trackBar1.Maximum = 500;
            trackBar1.Minimum = 1;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(104, 45);
            trackBar1.TabIndex = 0;
            trackBar1.Value = 10;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 1;
            label1.Text = "Sample size - 10";
            // 
            // trackBar2
            // 
            trackBar2.Location = new Point(163, 38);
            trackBar2.Maximum = 10000;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(104, 45);
            trackBar2.TabIndex = 2;
            trackBar2.Value = 100;
            trackBar2.Scroll += trackBar2_Scroll;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(163, 9);
            label2.Name = "label2";
            label2.Size = new Size(140, 15);
            label2.TabIndex = 3;
            label2.Text = "Number of samples - 100";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 89);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1875, 892);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(353, 12);
            button1.Name = "button1";
            button1.Size = new Size(101, 47);
            button1.TabIndex = 5;
            button1.Text = "Compute new distributions";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // timer1
            // 
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1370, 749);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(trackBar2);
            Controls.Add(label1);
            Controls.Add(trackBar1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar trackBar1;
        private Label label1;
        private TrackBar trackBar2;
        private Label label2;
        private PictureBox pictureBox1;
        private Button button1;
        private System.Windows.Forms.Timer timer1;
    }
}
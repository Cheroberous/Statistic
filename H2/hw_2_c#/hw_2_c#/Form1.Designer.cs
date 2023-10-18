namespace hw_2_c_
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(918, 413);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 85);
            this.button1.TabIndex = 0;
            this.button1.Text = "Click me for frequency distribution";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Stat Unit ID",
            "Background (degree)",
            "Expected work sectors",
            "Programming Languages",
            "Hard Worker (0-5)",
            "Ambitious (0-5)",
            "Team leader or Team player ?",
            "Enterpreneurial attitude (0-5)",
            "Preferred Workload",
            "Scalability",
            "Most recent working position",
            "Sex",
            "Age",
            "weight",
            "height",
            "Main Interests",
            "Main hobbies",
            "Play some instruments? Which ones?",
            "Would you like to maintain a YT channel ?",
            "Sports",
            "Dream Works",
            "Priorities in Life",
            "Country",
            "Primary language(s)",
            "Secondary language(s)",
            "Can this be a \"representative\" sample of larger populations ? If yes, which ones?" +
                "",
            "Do you think that looking at other answers is influencing your responses? Why?"});
            this.comboBox1.Location = new System.Drawing.Point(944, 357);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(167, 28);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(939, 297);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Choose a variable !";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(901, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Choose two variables !";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Stat Unit ID",
            "Background (degree)",
            "Expected work sectors",
            "Programming Languages",
            "Hard Worker (0-5)",
            "Ambitious (0-5)",
            "Team leader or Team player ?",
            "Enterpreneurial attitude (0-5)",
            "Preferred Workload",
            "Scalability",
            "Most recent working position",
            "Sex",
            "Age",
            "weight",
            "height",
            "Main Interests",
            "Main hobbies",
            "Play some instruments? Which ones?",
            "Would you like to maintain a YT channel ?",
            "Sports",
            "Dream Works",
            "Priorities in Life",
            "Country",
            "Primary language(s)",
            "Secondary language(s)",
            "Can this be a \"representative\" sample of larger populations ? If yes, which ones?" +
                "",
            "Do you think that looking at other answers is influencing your responses? Why?"});
            this.comboBox2.Location = new System.Drawing.Point(836, 102);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(167, 28);
            this.comboBox2.TabIndex = 7;
            // 
            // comboBox3
            // 
            this.comboBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Stat Unit ID",
            "Background (degree)",
            "Expected work sectors",
            "Programming Languages",
            "Hard Worker (0-5)",
            "Ambitious (0-5)",
            "Team leader or Team player ?",
            "Enterpreneurial attitude (0-5)",
            "Preferred Workload",
            "Scalability",
            "Most recent working position",
            "Sex",
            "Age",
            "weight",
            "height",
            "Main Interests",
            "Main hobbies",
            "Play some instruments? Which ones?",
            "Would you like to maintain a YT channel ?",
            "Sports",
            "Dream Works",
            "Priorities in Life",
            "Country",
            "Primary language(s)",
            "Secondary language(s)",
            "Can this be a \"representative\" sample of larger populations ? If yes, which ones?" +
                "",
            "Do you think that looking at other answers is influencing your responses? Why?"});
            this.comboBox3.Location = new System.Drawing.Point(1043, 102);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(167, 28);
            this.comboBox3.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(921, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(219, 85);
            this.button2.TabIndex = 9;
            this.button2.Text = "Click me for frequency distribution";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(2, 12);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(808, 490);
            this.dataGridView2.TabIndex = 10;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 555);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.DataGridView dataGridView2;
    }
}


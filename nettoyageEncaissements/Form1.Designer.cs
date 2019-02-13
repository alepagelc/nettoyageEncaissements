namespace nettoyageEncaissements
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAdrServeur = new System.Windows.Forms.TextBox();
            this.textBoxPortServeur = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxBaseServeur = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAdrClient = new System.Windows.Forms.TextBox();
            this.textBoxPortClient = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxBaseClient = new System.Windows.Forms.ComboBox();
            this.buttonDemarrage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Base serveur";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(421, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Base cliente";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::nettoyageEncaissements.Properties.Resources.poste_serveur;
            this.pictureBox2.Location = new System.Drawing.Point(80, 53);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(137, 129);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::nettoyageEncaissements.Properties.Resources.poste_esclave;
            this.pictureBox1.Location = new System.Drawing.Point(400, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(137, 129);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(479, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Sélectionner les bases pour lesquelles un nettoyage des encaissements est à réali" +
    "ser :";
            // 
            // textBoxAdrServeur
            // 
            this.textBoxAdrServeur.Location = new System.Drawing.Point(12, 212);
            this.textBoxAdrServeur.Name = "textBoxAdrServeur";
            this.textBoxAdrServeur.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdrServeur.TabIndex = 2;
            this.textBoxAdrServeur.Text = "127.0.0.1";
            // 
            // textBoxPortServeur
            // 
            this.textBoxPortServeur.Location = new System.Drawing.Point(118, 212);
            this.textBoxPortServeur.Name = "textBoxPortServeur";
            this.textBoxPortServeur.Size = new System.Drawing.Size(70, 20);
            this.textBoxPortServeur.TabIndex = 2;
            this.textBoxPortServeur.Text = "5432";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Adresse et port :";
            // 
            // comboBoxBaseServeur
            // 
            this.comboBoxBaseServeur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaseServeur.DropDownWidth = 270;
            this.comboBoxBaseServeur.FormattingEnabled = true;
            this.comboBoxBaseServeur.Location = new System.Drawing.Point(12, 262);
            this.comboBoxBaseServeur.Name = "comboBoxBaseServeur";
            this.comboBoxBaseServeur.Size = new System.Drawing.Size(274, 21);
            this.comboBoxBaseServeur.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Sélection de la base :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(325, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Adresse et port :";
            // 
            // textBoxAdrClient
            // 
            this.textBoxAdrClient.Location = new System.Drawing.Point(328, 212);
            this.textBoxAdrClient.Name = "textBoxAdrClient";
            this.textBoxAdrClient.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdrClient.TabIndex = 2;
            this.textBoxAdrClient.Text = "127.0.0.1";
            // 
            // textBoxPortClient
            // 
            this.textBoxPortClient.Location = new System.Drawing.Point(437, 212);
            this.textBoxPortClient.Name = "textBoxPortClient";
            this.textBoxPortClient.Size = new System.Drawing.Size(70, 20);
            this.textBoxPortClient.TabIndex = 2;
            this.textBoxPortClient.Text = "5432";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(325, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Sélection de la base :";
            // 
            // comboBoxBaseClient
            // 
            this.comboBoxBaseClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaseClient.DropDownWidth = 270;
            this.comboBoxBaseClient.FormattingEnabled = true;
            this.comboBoxBaseClient.Location = new System.Drawing.Point(328, 262);
            this.comboBoxBaseClient.Name = "comboBoxBaseClient";
            this.comboBoxBaseClient.Size = new System.Drawing.Size(274, 21);
            this.comboBoxBaseClient.TabIndex = 3;
            // 
            // buttonDemarrage
            // 
            this.buttonDemarrage.Location = new System.Drawing.Point(210, 312);
            this.buttonDemarrage.Name = "buttonDemarrage";
            this.buttonDemarrage.Size = new System.Drawing.Size(169, 23);
            this.buttonDemarrage.TabIndex = 4;
            this.buttonDemarrage.Text = "Lancer la vérification";
            this.buttonDemarrage.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 350);
            this.Controls.Add(this.buttonDemarrage);
            this.Controls.Add(this.comboBoxBaseClient);
            this.Controls.Add(this.comboBoxBaseServeur);
            this.Controls.Add(this.textBoxPortClient);
            this.Controls.Add(this.textBoxPortServeur);
            this.Controls.Add(this.textBoxAdrClient);
            this.Controls.Add(this.textBoxAdrServeur);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Nettoyage des encaissements";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAdrServeur;
        private System.Windows.Forms.TextBox textBoxPortServeur;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxBaseServeur;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAdrClient;
        private System.Windows.Forms.TextBox textBoxPortClient;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxBaseClient;
        private System.Windows.Forms.Button buttonDemarrage;
    }
}


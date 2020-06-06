namespace Progim_Gest
{
    partial class Anteprima
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
            this.labelusername = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.checkInserimento = new System.Windows.Forms.CheckBox();
            this.checkRiempMensile = new System.Windows.Forms.CheckBox();
            this.checkRiempAnnuale = new System.Windows.Forms.CheckBox();
            this.btnAvvio = new System.Windows.Forms.Button();
            this.dateTimeGiorno = new System.Windows.Forms.DateTimePicker();
            this.dateTimeMese = new System.Windows.Forms.DateTimePicker();
            this.dateTimeAnno = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // labelusername
            // 
            this.labelusername.AutoSize = true;
            this.labelusername.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelusername.Location = new System.Drawing.Point(151, 18);
            this.labelusername.Name = "labelusername";
            this.labelusername.Size = new System.Drawing.Size(92, 31);
            this.labelusername.TabIndex = 0;
            this.labelusername.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Utente :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(46, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Data :";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker.Location = new System.Drawing.Point(157, 77);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(162, 26);
            this.dateTimePicker.TabIndex = 4;
            // 
            // checkInserimento
            // 
            this.checkInserimento.AutoSize = true;
            this.checkInserimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkInserimento.Location = new System.Drawing.Point(50, 165);
            this.checkInserimento.Name = "checkInserimento";
            this.checkInserimento.Size = new System.Drawing.Size(352, 33);
            this.checkInserimento.TabIndex = 5;
            this.checkInserimento.Text = "Inserimento attività giornaliera";
            this.checkInserimento.UseVisualStyleBackColor = true;
            // 
            // checkRiempMensile
            // 
            this.checkRiempMensile.AutoSize = true;
            this.checkRiempMensile.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkRiempMensile.Location = new System.Drawing.Point(50, 222);
            this.checkRiempMensile.Name = "checkRiempMensile";
            this.checkRiempMensile.Size = new System.Drawing.Size(228, 33);
            this.checkRiempMensile.TabIndex = 6;
            this.checkRiempMensile.Text = "Riepilogo mensile";
            this.checkRiempMensile.UseVisualStyleBackColor = true;
            // 
            // checkRiempAnnuale
            // 
            this.checkRiempAnnuale.AutoSize = true;
            this.checkRiempAnnuale.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkRiempAnnuale.Location = new System.Drawing.Point(50, 289);
            this.checkRiempAnnuale.Name = "checkRiempAnnuale";
            this.checkRiempAnnuale.Size = new System.Drawing.Size(228, 33);
            this.checkRiempAnnuale.TabIndex = 8;
            this.checkRiempAnnuale.Text = "Riepilogo annuale";
            this.checkRiempAnnuale.UseVisualStyleBackColor = true;
            // 
            // btnAvvio
            // 
            this.btnAvvio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvvio.Location = new System.Drawing.Point(253, 371);
            this.btnAvvio.Name = "btnAvvio";
            this.btnAvvio.Size = new System.Drawing.Size(113, 39);
            this.btnAvvio.TabIndex = 9;
            this.btnAvvio.Text = "Avvio";
            this.btnAvvio.UseVisualStyleBackColor = true;
            this.btnAvvio.Click += new System.EventHandler(this.btnAvvio_Click);
            // 
            // dateTimeGiorno
            // 
            this.dateTimeGiorno.CustomFormat = "yyyy-MM-dd";
            this.dateTimeGiorno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeGiorno.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeGiorno.Location = new System.Drawing.Point(467, 166);
            this.dateTimeGiorno.Name = "dateTimeGiorno";
            this.dateTimeGiorno.Size = new System.Drawing.Size(123, 29);
            this.dateTimeGiorno.TabIndex = 10;
            // 
            // dateTimeMese
            // 
            this.dateTimeMese.CustomFormat = "MM/yyyy";
            this.dateTimeMese.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeMese.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeMese.Location = new System.Drawing.Point(467, 223);
            this.dateTimeMese.Name = "dateTimeMese";
            this.dateTimeMese.Size = new System.Drawing.Size(123, 29);
            this.dateTimeMese.TabIndex = 11;
            // 
            // dateTimeAnno
            // 
            this.dateTimeAnno.CustomFormat = "yyyy";
            this.dateTimeAnno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeAnno.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeAnno.Location = new System.Drawing.Point(467, 289);
            this.dateTimeAnno.Name = "dateTimeAnno";
            this.dateTimeAnno.Size = new System.Drawing.Size(122, 29);
            this.dateTimeAnno.TabIndex = 12;
            // 
            // Anteprima
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 436);
            this.Controls.Add(this.dateTimeAnno);
            this.Controls.Add(this.dateTimeMese);
            this.Controls.Add(this.dateTimeGiorno);
            this.Controls.Add(this.btnAvvio);
            this.Controls.Add(this.checkRiempAnnuale);
            this.Controls.Add(this.checkRiempMensile);
            this.Controls.Add(this.checkInserimento);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelusername);
            this.Name = "Anteprima";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelusername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.CheckBox checkInserimento;
        private System.Windows.Forms.CheckBox checkRiempMensile;
        private System.Windows.Forms.CheckBox checkRiempAnnuale;
        private System.Windows.Forms.Button btnAvvio;
        private System.Windows.Forms.DateTimePicker dateTimeGiorno;
        private System.Windows.Forms.DateTimePicker dateTimeMese;
        private System.Windows.Forms.DateTimePicker dateTimeAnno;
    }
}


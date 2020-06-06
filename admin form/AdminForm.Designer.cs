namespace Progim_Gest
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboCommesse = new System.Windows.Forms.ComboBox();
            this.dateTimeAnno = new System.Windows.Forms.DateTimePicker();
            this.dateTimeMese = new System.Windows.Forms.DateTimePicker();
            this.dateTimeGiorno = new System.Windows.Forms.DateTimePicker();
            this.btnAvvio = new System.Windows.Forms.Button();
            this.checkRiempAnnuale = new System.Windows.Forms.CheckBox();
            this.checkRiempAtt = new System.Windows.Forms.CheckBox();
            this.checkRiempMensile = new System.Windows.Forms.CheckBox();
            this.checkInserimento = new System.Windows.Forms.CheckBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelusername = new System.Windows.Forms.Label();
            this.checkedListDisegnatore = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimeRiepilogo = new System.Windows.Forms.DateTimePicker();
            this.comboRiepilogoCommessa = new System.Windows.Forms.ComboBox();
            this.checkRiempielogoCommessa = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboCommesse
            // 
            this.comboCommesse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCommesse.FormattingEnabled = true;
            this.comboCommesse.Location = new System.Drawing.Point(482, 319);
            this.comboCommesse.Name = "comboCommesse";
            this.comboCommesse.Size = new System.Drawing.Size(307, 24);
            this.comboCommesse.TabIndex = 26;
            // 
            // dateTimeAnno
            // 
            this.dateTimeAnno.CustomFormat = "yyyy";
            this.dateTimeAnno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeAnno.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeAnno.Location = new System.Drawing.Point(424, 243);
            this.dateTimeAnno.Name = "dateTimeAnno";
            this.dateTimeAnno.Size = new System.Drawing.Size(122, 29);
            this.dateTimeAnno.TabIndex = 25;
            // 
            // dateTimeMese
            // 
            this.dateTimeMese.CustomFormat = "MM/yyyy";
            this.dateTimeMese.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeMese.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeMese.Location = new System.Drawing.Point(424, 177);
            this.dateTimeMese.Name = "dateTimeMese";
            this.dateTimeMese.Size = new System.Drawing.Size(123, 29);
            this.dateTimeMese.TabIndex = 24;
            // 
            // dateTimeGiorno
            // 
            this.dateTimeGiorno.CustomFormat = "yyyy-MM-dd";
            this.dateTimeGiorno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeGiorno.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeGiorno.Location = new System.Drawing.Point(424, 120);
            this.dateTimeGiorno.Name = "dateTimeGiorno";
            this.dateTimeGiorno.Size = new System.Drawing.Size(123, 29);
            this.dateTimeGiorno.TabIndex = 23;
            // 
            // btnAvvio
            // 
            this.btnAvvio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvvio.Location = new System.Drawing.Point(353, 436);
            this.btnAvvio.Name = "btnAvvio";
            this.btnAvvio.Size = new System.Drawing.Size(113, 39);
            this.btnAvvio.TabIndex = 22;
            this.btnAvvio.Text = "Avvio";
            this.btnAvvio.UseVisualStyleBackColor = true;
            this.btnAvvio.Click += new System.EventHandler(this.btnAvvio_Click);
            // 
            // checkRiempAnnuale
            // 
            this.checkRiempAnnuale.AutoSize = true;
            this.checkRiempAnnuale.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkRiempAnnuale.Location = new System.Drawing.Point(22, 243);
            this.checkRiempAnnuale.Name = "checkRiempAnnuale";
            this.checkRiempAnnuale.Size = new System.Drawing.Size(228, 33);
            this.checkRiempAnnuale.TabIndex = 21;
            this.checkRiempAnnuale.Text = "Riepilogo annuale";
            this.checkRiempAnnuale.UseVisualStyleBackColor = true;
            // 
            // checkRiempAtt
            // 
            this.checkRiempAtt.AutoSize = true;
            this.checkRiempAtt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkRiempAtt.Location = new System.Drawing.Point(22, 316);
            this.checkRiempAtt.Name = "checkRiempAtt";
            this.checkRiempAtt.Size = new System.Drawing.Size(366, 29);
            this.checkRiempAtt.TabIndex = 20;
            this.checkRiempAtt.Text = "Riepilogo per commessa nel mese:";
            this.checkRiempAtt.UseVisualStyleBackColor = true;
            // 
            // checkRiempMensile
            // 
            this.checkRiempMensile.AutoSize = true;
            this.checkRiempMensile.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkRiempMensile.Location = new System.Drawing.Point(22, 176);
            this.checkRiempMensile.Name = "checkRiempMensile";
            this.checkRiempMensile.Size = new System.Drawing.Size(228, 33);
            this.checkRiempMensile.TabIndex = 19;
            this.checkRiempMensile.Text = "Riepilogo mensile";
            this.checkRiempMensile.UseVisualStyleBackColor = true;
            // 
            // checkInserimento
            // 
            this.checkInserimento.AutoSize = true;
            this.checkInserimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkInserimento.Location = new System.Drawing.Point(22, 119);
            this.checkInserimento.Name = "checkInserimento";
            this.checkInserimento.Size = new System.Drawing.Size(352, 33);
            this.checkInserimento.TabIndex = 18;
            this.checkInserimento.Text = "Inserimento attività giornaliera";
            this.checkInserimento.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker.Location = new System.Drawing.Point(129, 55);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(162, 26);
            this.dateTimePicker.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 24);
            this.label3.TabIndex = 16;
            this.label3.Text = "Data :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 25);
            this.label2.TabIndex = 15;
            this.label2.Text = "Utente :";
            // 
            // labelusername
            // 
            this.labelusername.AutoSize = true;
            this.labelusername.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelusername.Location = new System.Drawing.Point(123, 2);
            this.labelusername.Name = "labelusername";
            this.labelusername.Size = new System.Drawing.Size(92, 31);
            this.labelusername.TabIndex = 14;
            this.labelusername.Text = "label1";
            // 
            // checkedListDisegnatore
            // 
            this.checkedListDisegnatore.FormattingEnabled = true;
            this.checkedListDisegnatore.Location = new System.Drawing.Point(608, 118);
            this.checkedListDisegnatore.Name = "checkedListDisegnatore";
            this.checkedListDisegnatore.Size = new System.Drawing.Size(120, 154);
            this.checkedListDisegnatore.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(663, 447);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 61);
            this.button1.TabIndex = 28;
            this.button1.Text = "Gestione Commesse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimeRiepilogo
            // 
            this.dateTimeRiepilogo.CustomFormat = "MM-yyyy";
            this.dateTimeRiepilogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeRiepilogo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeRiepilogo.Location = new System.Drawing.Point(394, 319);
            this.dateTimeRiepilogo.Name = "dateTimeRiepilogo";
            this.dateTimeRiepilogo.Size = new System.Drawing.Size(82, 26);
            this.dateTimeRiepilogo.TabIndex = 29;
            // 
            // comboRiepilogoCommessa
            // 
            this.comboRiepilogoCommessa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboRiepilogoCommessa.FormattingEnabled = true;
            this.comboRiepilogoCommessa.Location = new System.Drawing.Point(317, 382);
            this.comboRiepilogoCommessa.Name = "comboRiepilogoCommessa";
            this.comboRiepilogoCommessa.Size = new System.Drawing.Size(472, 24);
            this.comboRiepilogoCommessa.TabIndex = 31;
            // 
            // checkRiempielogoCommessa
            // 
            this.checkRiempielogoCommessa.AutoSize = true;
            this.checkRiempielogoCommessa.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkRiempielogoCommessa.Location = new System.Drawing.Point(22, 379);
            this.checkRiempielogoCommessa.Name = "checkRiempielogoCommessa";
            this.checkRiempielogoCommessa.Size = new System.Drawing.Size(279, 29);
            this.checkRiempielogoCommessa.TabIndex = 30;
            this.checkRiempielogoCommessa.Text = "Riepilogo per commessa :";
            this.checkRiempielogoCommessa.UseVisualStyleBackColor = true;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 520);
            this.Controls.Add(this.comboRiepilogoCommessa);
            this.Controls.Add(this.checkRiempielogoCommessa);
            this.Controls.Add(this.dateTimeRiepilogo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListDisegnatore);
            this.Controls.Add(this.comboCommesse);
            this.Controls.Add(this.dateTimeAnno);
            this.Controls.Add(this.dateTimeMese);
            this.Controls.Add(this.dateTimeGiorno);
            this.Controls.Add(this.btnAvvio);
            this.Controls.Add(this.checkRiempAnnuale);
            this.Controls.Add(this.checkRiempAtt);
            this.Controls.Add(this.checkRiempMensile);
            this.Controls.Add(this.checkInserimento);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelusername);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboCommesse;
        private System.Windows.Forms.DateTimePicker dateTimeAnno;
        private System.Windows.Forms.DateTimePicker dateTimeMese;
        private System.Windows.Forms.DateTimePicker dateTimeGiorno;
        private System.Windows.Forms.Button btnAvvio;
        private System.Windows.Forms.CheckBox checkRiempAnnuale;
        private System.Windows.Forms.CheckBox checkRiempAtt;
        private System.Windows.Forms.CheckBox checkRiempMensile;
        private System.Windows.Forms.CheckBox checkInserimento;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelusername;
        private System.Windows.Forms.CheckedListBox checkedListDisegnatore;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimeRiepilogo;
        private System.Windows.Forms.ComboBox comboRiepilogoCommessa;
        private System.Windows.Forms.CheckBox checkRiempielogoCommessa;
    }
}
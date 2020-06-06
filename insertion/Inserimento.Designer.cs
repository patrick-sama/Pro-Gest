namespace Progim_Gest
{
    partial class Inserimento
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelNome = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Attività = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Ore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.LabelTotale = new System.Windows.Forms.Label();
            this.BtnAddRow = new System.Windows.Forms.Button();
            this.BtnElimina = new System.Windows.Forms.Button();
            this.btnCambiaData = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(129, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Utente : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(129, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Giorno : ";
            // 
            // labelNome
            // 
            this.labelNome.AutoSize = true;
            this.labelNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNome.Location = new System.Drawing.Point(245, 23);
            this.labelNome.Name = "labelNome";
            this.labelNome.Size = new System.Drawing.Size(78, 29);
            this.labelNome.TabIndex = 2;
            this.labelNome.Text = "nome";
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Attività,
            this.Ore,
            this.Note,
            this.Id});
            this.dgv.Location = new System.Drawing.Point(59, 127);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(668, 245);
            this.dgv.TabIndex = 4;
            this.dgv.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // Attività
            // 
            this.Attività.HeaderText = "NomeCommessa";
            this.Attività.Name = "Attività";
            this.Attività.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Attività.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Attività.Width = 300;
            // 
            // Ore
            // 
            this.Ore.HeaderText = "Ore";
            this.Ore.Name = "Ore";
            this.Ore.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Ore.Width = 40;
            // 
            // Note
            // 
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            this.Note.Width = 250;
            // 
            // Id
            // 
            this.Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(244, 388);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 31);
            this.label3.TabIndex = 5;
            this.label3.Text = "Totale :";
            // 
            // LabelTotale
            // 
            this.LabelTotale.AutoSize = true;
            this.LabelTotale.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTotale.Location = new System.Drawing.Point(355, 394);
            this.LabelTotale.Name = "LabelTotale";
            this.LabelTotale.Size = new System.Drawing.Size(24, 25);
            this.LabelTotale.TabIndex = 6;
            this.LabelTotale.Text = "0";
            // 
            // BtnAddRow
            // 
            this.BtnAddRow.Location = new System.Drawing.Point(531, 388);
            this.BtnAddRow.Name = "BtnAddRow";
            this.BtnAddRow.Size = new System.Drawing.Size(120, 31);
            this.BtnAddRow.TabIndex = 7;
            this.BtnAddRow.Text = "Salva";
            this.BtnAddRow.UseVisualStyleBackColor = true;
            this.BtnAddRow.Click += new System.EventHandler(this.BtnAddRow_Click);
            // 
            // BtnElimina
            // 
            this.BtnElimina.Location = new System.Drawing.Point(757, 198);
            this.BtnElimina.Name = "BtnElimina";
            this.BtnElimina.Size = new System.Drawing.Size(120, 31);
            this.BtnElimina.TabIndex = 8;
            this.BtnElimina.Text = "Elimina";
            this.BtnElimina.UseVisualStyleBackColor = true;
            this.BtnElimina.Click += new System.EventHandler(this.BtnElimina_Click);
            // 
            // btnCambiaData
            // 
            this.btnCambiaData.Location = new System.Drawing.Point(491, 75);
            this.btnCambiaData.Name = "btnCambiaData";
            this.btnCambiaData.Size = new System.Drawing.Size(124, 23);
            this.btnCambiaData.TabIndex = 9;
            this.btnCambiaData.Text = "Cambia Data";
            this.btnCambiaData.UseVisualStyleBackColor = true;
            this.btnCambiaData.Click += new System.EventHandler(this.btnCambiaData_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(250, 78);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // Inserimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 450);
            this.Controls.Add(this.btnCambiaData);
            this.Controls.Add(this.BtnElimina);
            this.Controls.Add(this.BtnAddRow);
            this.Controls.Add(this.LabelTotale);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.labelNome);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Inserimento";
            this.Text = "Inserimento";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelNome;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LabelTotale;
        private System.Windows.Forms.DataGridViewComboBoxColumn Attività;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ore;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.Button BtnAddRow;
        private System.Windows.Forms.Button BtnElimina;
        private System.Windows.Forms.Button btnCambiaData;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}
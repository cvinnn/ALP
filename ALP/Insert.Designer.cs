namespace ALP
{
    partial class Insert
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
            this.btnPasien = new System.Windows.Forms.Button();
            this.btnDokter = new System.Windows.Forms.Button();
            this.btnPerawat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPasien
            // 
            this.btnPasien.Location = new System.Drawing.Point(65, 259);
            this.btnPasien.Name = "btnPasien";
            this.btnPasien.Size = new System.Drawing.Size(207, 97);
            this.btnPasien.TabIndex = 0;
            this.btnPasien.Text = "Pasien";
            this.btnPasien.UseVisualStyleBackColor = true;
            this.btnPasien.Click += new System.EventHandler(this.btnPasien_Click);
            // 
            // btnDokter
            // 
            this.btnDokter.Location = new System.Drawing.Point(501, 259);
            this.btnDokter.Name = "btnDokter";
            this.btnDokter.Size = new System.Drawing.Size(207, 97);
            this.btnDokter.TabIndex = 1;
            this.btnDokter.Text = "Dokter";
            this.btnDokter.UseVisualStyleBackColor = true;
            this.btnDokter.Click += new System.EventHandler(this.btnDokter_Click);
            // 
            // btnPerawat
            // 
            this.btnPerawat.Location = new System.Drawing.Point(931, 259);
            this.btnPerawat.Name = "btnPerawat";
            this.btnPerawat.Size = new System.Drawing.Size(207, 97);
            this.btnPerawat.TabIndex = 2;
            this.btnPerawat.Text = "Perawat";
            this.btnPerawat.UseVisualStyleBackColor = true;
            this.btnPerawat.Click += new System.EventHandler(this.btnPerawat_Click);
            // 
            // Insert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.ControlBox = false;
            this.Controls.Add(this.btnPerawat);
            this.Controls.Add(this.btnDokter);
            this.Controls.Add(this.btnPasien);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Insert";
            this.Text = "  ";
            this.Load += new System.EventHandler(this.Insert_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPasien;
        private System.Windows.Forms.Button btnDokter;
        private System.Windows.Forms.Button btnPerawat;
    }
}


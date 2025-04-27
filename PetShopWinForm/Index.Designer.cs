
namespace PetShopWinForm
{
    partial class Index
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Index));
            this.FaixaMenu = new System.Windows.Forms.Panel();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.BtnHome = new System.Windows.Forms.Button();
            this.BtnAgendamento = new System.Windows.Forms.Button();
            this.FaixaMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // FaixaMenu
            // 
            this.FaixaMenu.BackColor = System.Drawing.Color.PaleGreen;
            this.FaixaMenu.Controls.Add(this.BtnAgendamento);
            this.FaixaMenu.Controls.Add(this.BtnHome);
            this.FaixaMenu.Controls.Add(this.Logo);
            this.FaixaMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.FaixaMenu.Location = new System.Drawing.Point(0, 0);
            this.FaixaMenu.Name = "FaixaMenu";
            this.FaixaMenu.Size = new System.Drawing.Size(1074, 95);
            this.FaixaMenu.TabIndex = 0;
            // 
            // Logo
            // 
            this.Logo.Image = ((System.Drawing.Image)(resources.GetObject("Logo.Image")));
            this.Logo.InitialImage = null;
            this.Logo.Location = new System.Drawing.Point(0, 3);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(100, 92);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Logo.TabIndex = 0;
            this.Logo.TabStop = false;
            // 
            // BtnHome
            // 
            this.BtnHome.BackColor = System.Drawing.Color.PaleGreen;
            this.BtnHome.FlatAppearance.BorderSize = 0;
            this.BtnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnHome.Font = new System.Drawing.Font("Dubai", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnHome.ForeColor = System.Drawing.Color.Black;
            this.BtnHome.Location = new System.Drawing.Point(147, 12);
            this.BtnHome.Name = "BtnHome";
            this.BtnHome.Size = new System.Drawing.Size(157, 66);
            this.BtnHome.TabIndex = 1;
            this.BtnHome.Text = "Home";
            this.BtnHome.UseVisualStyleBackColor = false;
            // 
            // BtnAgendamento
            // 
            this.BtnAgendamento.BackColor = System.Drawing.Color.PaleGreen;
            this.BtnAgendamento.FlatAppearance.BorderSize = 0;
            this.BtnAgendamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAgendamento.Font = new System.Drawing.Font("Dubai", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgendamento.ForeColor = System.Drawing.Color.Black;
            this.BtnAgendamento.Location = new System.Drawing.Point(294, 12);
            this.BtnAgendamento.Name = "BtnAgendamento";
            this.BtnAgendamento.Size = new System.Drawing.Size(232, 66);
            this.BtnAgendamento.TabIndex = 2;
            this.BtnAgendamento.Text = "Agendamento";
            this.BtnAgendamento.UseVisualStyleBackColor = false;
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1074, 555);
            this.Controls.Add(this.FaixaMenu);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Index";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Index_Load);
            this.FaixaMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel FaixaMenu;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Button BtnHome;
        private System.Windows.Forms.Button BtnAgendamento;
    }
}


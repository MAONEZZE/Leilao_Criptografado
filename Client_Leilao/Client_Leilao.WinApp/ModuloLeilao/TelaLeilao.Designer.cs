namespace Client_Leilao.WinApp.ModuloLeilao
{
    partial class TelaLeilao
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox2 = new GroupBox();
            lbl_ultimoLance = new Label();
            groupBox1 = new GroupBox();
            lbl_tempo = new Label();
            btn_confirmar = new Button();
            lbx_infoProduto = new ListBox();
            txb_valor = new MaskedTextBox();
            lbx_chat = new ListBox();
            btn_sair = new Button();
            pcbox_produto = new PictureBox();
            groupBox3 = new GroupBox();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pcbox_produto).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lbl_ultimoLance);
            groupBox2.Location = new Point(404, 98);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(80, 72);
            groupBox2.TabIndex = 18;
            groupBox2.TabStop = false;
            groupBox2.Text = "Seu ultimo lance";
            // 
            // lbl_ultimoLance
            // 
            lbl_ultimoLance.AutoSize = true;
            lbl_ultimoLance.Enabled = false;
            lbl_ultimoLance.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_ultimoLance.Location = new Point(6, 37);
            lbl_ultimoLance.Name = "lbl_ultimoLance";
            lbl_ultimoLance.Size = new Size(0, 21);
            lbl_ultimoLance.TabIndex = 8;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lbl_tempo);
            groupBox1.Location = new Point(404, 21);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(80, 58);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tempo";
            // 
            // lbl_tempo
            // 
            lbl_tempo.Anchor = AnchorStyles.None;
            lbl_tempo.AutoSize = true;
            lbl_tempo.Enabled = false;
            lbl_tempo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_tempo.Location = new Point(8, 28);
            lbl_tempo.Name = "lbl_tempo";
            lbl_tempo.Size = new Size(34, 15);
            lbl_tempo.TabIndex = 8;
            lbl_tempo.Text = "12:00";
            // 
            // btn_confirmar
            // 
            btn_confirmar.ImageAlign = ContentAlignment.MiddleLeft;
            btn_confirmar.Location = new Point(287, 331);
            btn_confirmar.Name = "btn_confirmar";
            btn_confirmar.Size = new Size(96, 25);
            btn_confirmar.TabIndex = 15;
            btn_confirmar.Text = "CONFIRMAR";
            btn_confirmar.UseVisualStyleBackColor = true;
            btn_confirmar.Click += btn_confirmar_Click;
            // 
            // lbx_infoProduto
            // 
            lbx_infoProduto.FormattingEnabled = true;
            lbx_infoProduto.ItemHeight = 15;
            lbx_infoProduto.Location = new Point(6, 190);
            lbx_infoProduto.Name = "lbx_infoProduto";
            lbx_infoProduto.Size = new Size(169, 154);
            lbx_infoProduto.TabIndex = 12;
            // 
            // txb_valor
            // 
            txb_valor.Location = new Point(217, 332);
            txb_valor.Mask = "0000000";
            txb_valor.Name = "txb_valor";
            txb_valor.Size = new Size(64, 23);
            txb_valor.TabIndex = 13;
            txb_valor.TextAlign = HorizontalAlignment.Center;
            txb_valor.ValidatingType = typeof(int);
            // 
            // lbx_chat
            // 
            lbx_chat.FormattingEnabled = true;
            lbx_chat.ItemHeight = 15;
            lbx_chat.Location = new Point(217, 21);
            lbx_chat.Name = "lbx_chat";
            lbx_chat.Size = new Size(166, 304);
            lbx_chat.TabIndex = 16;
            // 
            // btn_sair
            // 
            btn_sair.Location = new Point(420, 327);
            btn_sair.Name = "btn_sair";
            btn_sair.Size = new Size(64, 29);
            btn_sair.TabIndex = 14;
            btn_sair.Text = "Sair";
            btn_sair.UseVisualStyleBackColor = true;
            btn_sair.Click += btn_sair_Click;
            // 
            // pcbox_produto
            // 
            pcbox_produto.Location = new Point(6, 22);
            pcbox_produto.Name = "pcbox_produto";
            pcbox_produto.Size = new Size(169, 163);
            pcbox_produto.TabIndex = 11;
            pcbox_produto.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(pcbox_produto);
            groupBox3.Controls.Add(lbx_infoProduto);
            groupBox3.Location = new Point(12, 11);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(182, 352);
            groupBox3.TabIndex = 19;
            groupBox3.TabStop = false;
            groupBox3.Text = "Produto";
            // 
            // TelaLeilao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btn_confirmar);
            Controls.Add(txb_valor);
            Controls.Add(lbx_chat);
            Controls.Add(btn_sair);
            Name = "TelaLeilao";
            Size = new Size(500, 375);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pcbox_produto).EndInit();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox2;
        private Label lbl_ultimoLance;
        private GroupBox groupBox1;
        private Label lbl_tempo;
        private Button btn_confirmar;
        private ListBox lbx_infoProduto;
        private MaskedTextBox txb_valor;
        private ListBox lbx_chat;
        private Button btn_sair;
        private PictureBox pcbox_produto;
        private GroupBox groupBox3;
    }
}

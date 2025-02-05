namespace Server_Leilao.WinApp.ModuloEnvioLeilao
{
    partial class ComunicacaoLeila
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
            groupBox1 = new GroupBox();
            label1 = new Label();
            txb_caminhoImg = new TextBox();
            btn_selecionar = new Button();
            groupBox2 = new GroupBox();
            txb_tempo = new MaskedTextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            txb_descricao = new TextBox();
            txb_valor = new MaskedTextBox();
            groupBox3 = new GroupBox();
            pcbox_produto = new PictureBox();
            btn_enviar = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pcbox_produto).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txb_caminhoImg);
            groupBox1.Controls.Add(btn_selecionar);
            groupBox1.Location = new Point(12, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(229, 122);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Selecionar Imagem";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 37);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 2;
            label1.Text = "Caminho da Imagem";
            // 
            // txb_caminhoImg
            // 
            txb_caminhoImg.Location = new Point(15, 55);
            txb_caminhoImg.Name = "txb_caminhoImg";
            txb_caminhoImg.ReadOnly = true;
            txb_caminhoImg.Size = new Size(197, 23);
            txb_caminhoImg.TabIndex = 1;
            // 
            // btn_selecionar
            // 
            btn_selecionar.Location = new Point(15, 84);
            btn_selecionar.Name = "btn_selecionar";
            btn_selecionar.Size = new Size(75, 23);
            btn_selecionar.TabIndex = 0;
            btn_selecionar.Text = "Selecionar";
            btn_selecionar.UseVisualStyleBackColor = true;
            btn_selecionar.Click += btn_selecionar_Click_1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txb_tempo);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(txb_descricao);
            groupBox2.Controls.Add(txb_valor);
            groupBox2.Location = new Point(12, 142);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(229, 199);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Informações do Produto";
            // 
            // txb_tempo
            // 
            txb_tempo.Location = new Point(139, 170);
            txb_tempo.Mask = "00000";
            txb_tempo.Name = "txb_tempo";
            txb_tempo.Size = new Size(41, 23);
            txb_tempo.TabIndex = 6;
            txb_tempo.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(31, 176);
            label4.Name = "label4";
            label4.Size = new Size(102, 15);
            label4.TabIndex = 5;
            label4.Text = "Tempo (minutos):";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 125);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 4;
            label3.Text = "Valor:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 33);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 3;
            label2.Text = "Descrição:";
            // 
            // txb_descricao
            // 
            txb_descricao.Location = new Point(70, 33);
            txb_descricao.Multiline = true;
            txb_descricao.Name = "txb_descricao";
            txb_descricao.Size = new Size(142, 78);
            txb_descricao.TabIndex = 2;
            // 
            // txb_valor
            // 
            txb_valor.Location = new Point(73, 117);
            txb_valor.Mask = "00000";
            txb_valor.Name = "txb_valor";
            txb_valor.Size = new Size(41, 23);
            txb_valor.TabIndex = 0;
            txb_valor.TextAlign = HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(pcbox_produto);
            groupBox3.Location = new Point(247, 14);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(229, 268);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Imagem";
            // 
            // pcbox_produto
            // 
            pcbox_produto.Location = new Point(6, 22);
            pcbox_produto.Name = "pcbox_produto";
            pcbox_produto.Size = new Size(217, 237);
            pcbox_produto.TabIndex = 0;
            pcbox_produto.TabStop = false;
            // 
            // btn_enviar
            // 
            btn_enviar.Location = new Point(401, 318);
            btn_enviar.Name = "btn_enviar";
            btn_enviar.Size = new Size(75, 23);
            btn_enviar.TabIndex = 1;
            btn_enviar.Text = "Enviar";
            btn_enviar.UseVisualStyleBackColor = true;
            btn_enviar.Click += btn_enviar_Click_1;
            // 
            // ComunicacaoLeila
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btn_enviar);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "ComunicacaoLeila";
            Size = new Size(500, 375);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pcbox_produto).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private TextBox txb_caminhoImg;
        private Button btn_selecionar;
        private GroupBox groupBox2;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox txb_descricao;
        private MaskedTextBox txb_valor;
        private GroupBox groupBox3;
        private PictureBox pcbox_produto;
        private Button btn_enviar;
        private MaskedTextBox txb_tempo;
    }
}

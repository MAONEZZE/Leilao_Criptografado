namespace Client_Leilao.WinApp
{
    partial class Tela_Principal
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
            statusStrip1 = new StatusStrip();
            label_status = new ToolStripStatusLabel();
            panel = new Panel();
            txb_cpf = new MaskedTextBox();
            btn_entrar = new Button();
            label4 = new Label();
            txb_nome = new TextBox();
            label3 = new Label();
            groupBox1 = new GroupBox();
            txb_port = new TextBox();
            txb_ip = new TextBox();
            label1 = new Label();
            label2 = new Label();
            statusStrip1.SuspendLayout();
            panel.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { label_status });
            statusStrip1.Location = new Point(0, 375);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(500, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // label_status
            // 
            label_status.Name = "label_status";
            label_status.Size = new Size(38, 17);
            label_status.Text = "status";
            // 
            // panel
            // 
            panel.Controls.Add(txb_cpf);
            panel.Controls.Add(btn_entrar);
            panel.Controls.Add(label4);
            panel.Controls.Add(txb_nome);
            panel.Controls.Add(label3);
            panel.Controls.Add(groupBox1);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 0);
            panel.Name = "panel";
            panel.Size = new Size(500, 375);
            panel.TabIndex = 2;
            // 
            // txb_cpf
            // 
            txb_cpf.Location = new Point(176, 122);
            txb_cpf.Mask = "000000000-00";
            txb_cpf.Name = "txb_cpf";
            txb_cpf.Size = new Size(190, 23);
            txb_cpf.TabIndex = 17;
            txb_cpf.Text = "46336237885";
            // 
            // btn_entrar
            // 
            btn_entrar.FlatStyle = FlatStyle.Popup;
            btn_entrar.Location = new Point(291, 287);
            btn_entrar.Name = "btn_entrar";
            btn_entrar.Size = new Size(75, 23);
            btn_entrar.TabIndex = 16;
            btn_entrar.Text = "Entrar";
            btn_entrar.UseVisualStyleBackColor = true;
            btn_entrar.Click += btn_entrar_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(146, 125);
            label4.Name = "label4";
            label4.Size = new Size(29, 15);
            label4.TabIndex = 14;
            label4.Text = "Cpf:";
            // 
            // txb_nome
            // 
            txb_nome.Location = new Point(176, 80);
            txb_nome.Name = "txb_nome";
            txb_nome.Size = new Size(190, 23);
            txb_nome.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(132, 82);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 13;
            label3.Text = "Nome:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txb_port);
            groupBox1.Controls.Add(txb_ip);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(133, 167);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(250, 100);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Server Config";
            // 
            // txb_port
            // 
            txb_port.Location = new Point(48, 64);
            txb_port.Name = "txb_port";
            txb_port.Size = new Size(185, 23);
            txb_port.TabIndex = 6;
            txb_port.Text = "51000";
            // 
            // txb_ip
            // 
            txb_ip.Location = new Point(48, 26);
            txb_ip.Name = "txb_ip";
            txb_ip.Size = new Size(185, 23);
            txb_ip.TabIndex = 5;
            txb_ip.Text = "10.151.57.122";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 28);
            label1.Name = "label1";
            label1.Size = new Size(20, 15);
            label1.TabIndex = 3;
            label1.Text = "IP:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 66);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 4;
            label2.Text = "Port:";
            // 
            // Tela_Principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 397);
            Controls.Add(panel);
            Controls.Add(statusStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Tela_Principal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Leilão Client";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel.ResumeLayout(false);
            panel.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel label_status;
        private Panel panel;
        private Button btn_entrar;
        private Label label4;
        private TextBox txb_nome;
        private Label label3;
        private GroupBox groupBox1;
        private TextBox txb_port;
        private TextBox txb_ip;
        private Label label1;
        private Label label2;
        private MaskedTextBox txb_cpf;
    }
}

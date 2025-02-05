namespace Server_Leilao.WinApp
{
    partial class TelaPrincipal
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
            btn_entrar = new Button();
            groupBox = new GroupBox();
            txb_porta = new TextBox();
            txb_ip = new TextBox();
            label1 = new Label();
            labl = new Label();
            statusStrip1.SuspendLayout();
            panel.SuspendLayout();
            groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { label_status });
            statusStrip1.Location = new Point(0, 375);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(500, 22);
            statusStrip1.TabIndex = 0;
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
            panel.Controls.Add(btn_entrar);
            panel.Controls.Add(groupBox);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 0);
            panel.Name = "panel";
            panel.Size = new Size(500, 375);
            panel.TabIndex = 1;
            // 
            // btn_entrar
            // 
            btn_entrar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_entrar.Location = new Point(290, 312);
            btn_entrar.Name = "btn_entrar";
            btn_entrar.Size = new Size(75, 36);
            btn_entrar.TabIndex = 1;
            btn_entrar.Text = "Entrar";
            btn_entrar.UseVisualStyleBackColor = true;
            btn_entrar.Click += btn_entrar_Click;
            // 
            // groupBox
            // 
            groupBox.Controls.Add(txb_porta);
            groupBox.Controls.Add(txb_ip);
            groupBox.Controls.Add(label1);
            groupBox.Controls.Add(labl);
            groupBox.Location = new Point(131, 77);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(235, 219);
            groupBox.TabIndex = 0;
            groupBox.TabStop = false;
            groupBox.Text = "Endereço do Leilão";
            // 
            // txb_porta
            // 
            txb_porta.Location = new Point(30, 157);
            txb_porta.Name = "txb_porta";
            txb_porta.Size = new Size(180, 23);
            txb_porta.TabIndex = 3;
            txb_porta.Text = "52000";
            // 
            // txb_ip
            // 
            txb_ip.Location = new Point(30, 74);
            txb_ip.Name = "txb_ip";
            txb_ip.Size = new Size(180, 23);
            txb_ip.TabIndex = 2;
            txb_ip.Text = "127.0.0.1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F);
            label1.Location = new Point(25, 122);
            label1.Name = "label1";
            label1.Size = new Size(82, 30);
            label1.TabIndex = 1;
            label1.Text = "PORTA:";
            // 
            // labl
            // 
            labl.AutoSize = true;
            labl.Font = new Font("Segoe UI", 15.75F);
            labl.Location = new Point(25, 39);
            labl.Name = "labl";
            labl.Size = new Size(36, 30);
            labl.TabIndex = 0;
            labl.Text = "IP:";
            // 
            // TelaPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 397);
            Controls.Add(panel);
            Controls.Add(statusStrip1);
            Name = "TelaPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += TelaPrincipal_FormClosing;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel.ResumeLayout(false);
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private Panel panel;
        private GroupBox groupBox;
        private Label labl;
        private TextBox txb_porta;
        private TextBox txb_ip;
        private Label label1;
        private Button btn_entrar;
        private ToolStripStatusLabel label_status;
    }
}

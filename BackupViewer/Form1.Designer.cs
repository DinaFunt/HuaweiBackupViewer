namespace BackupViewer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.pathIn = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.pathOut = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.pathIn);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(731, 82);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Path to backup";
            // 
            // button2
            // 
            this.button2.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(666, 34);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 35);
            this.button2.TabIndex = 0;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.pathIn_Click);
            // 
            // pathIn
            // 
            this.pathIn.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.pathIn.Location = new System.Drawing.Point(6, 42);
            this.pathIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pathIn.Name = "pathIn";
            this.pathIn.Size = new System.Drawing.Size(581, 27);
            this.pathIn.TabIndex = 0;
            // 
            // password
            // 
            this.password.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.password.Location = new System.Drawing.Point(6, 39);
            this.password.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(581, 27);
            this.password.TabIndex = 2;
            // 
            // pathOut
            // 
            this.pathOut.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.pathOut.Location = new System.Drawing.Point(6, 38);
            this.pathOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pathOut.Name = "pathOut";
            this.pathOut.Size = new System.Drawing.Size(581, 27);
            this.pathOut.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(451, 344);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(236, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.StartDecrypting);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.pathOut);
            this.groupBox2.Location = new System.Drawing.Point(12, 125);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(731, 82);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Path to save decrypt backup";
            // 
            // button3
            // 
            this.button3.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(666, 33);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(36, 32);
            this.button3.TabIndex = 1;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.pathOut_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.password);
            this.groupBox3.Location = new System.Drawing.Point(12, 230);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(731, 82);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Password";
            // 
            // button4
            // 
            this.button4.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(666, 34);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(36, 32);
            this.button4.TabIndex = 2;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button4_MouseDown);
            this.button4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button4_MouseUp);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 400);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Backup Data Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox pathOut;
        private System.Windows.Forms.TextBox pathIn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button4;
    }
}
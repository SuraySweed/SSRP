namespace TorChatClient
{
    partial class ConnectToTorChat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectToTorChat));
            this.exitButton = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.fstMsg = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.ForeColor = System.Drawing.Color.Indigo;
            this.exitButton.Location = new System.Drawing.Point(31, 291);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(120, 45);
            this.exitButton.TabIndex = 0;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // nameBox
            // 
            this.nameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameBox.Location = new System.Drawing.Point(131, 170);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(209, 24);
            this.nameBox.TabIndex = 1;
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            // 
            // fstMsg
            // 
            this.fstMsg.AutoSize = true;
            this.fstMsg.BackColor = System.Drawing.Color.Transparent;
            this.fstMsg.Font = new System.Drawing.Font("Miriam Fixed", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fstMsg.ForeColor = System.Drawing.Color.Indigo;
            this.fstMsg.Location = new System.Drawing.Point(62, 51);
            this.fstMsg.Name = "fstMsg";
            this.fstMsg.Size = new System.Drawing.Size(357, 32);
            this.fstMsg.TabIndex = 2;
            this.fstMsg.Text = "What\'s Your  Name?";
            this.fstMsg.Click += new System.EventHandler(this.label1_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Enabled = false;
            this.ConnectButton.ForeColor = System.Drawing.Color.Indigo;
            this.ConnectButton.Location = new System.Drawing.Point(342, 290);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(99, 46);
            this.ConnectButton.TabIndex = 3;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ConnectToTorChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(496, 359);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.fstMsg);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.exitButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectToTorChat";
            this.Text = "TorChater";
            this.Load += new System.EventHandler(this.TorChater_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label fstMsg;
        private System.Windows.Forms.Button ConnectButton;
    }
}


namespace TorChatClient
{
    partial class TorChater
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
            this.exitButton = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.fstMsg = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.MsgBox = new System.Windows.Forms.TextBox();
            this.scndTxt = new System.Windows.Forms.Label();
            this.recepientName = new System.Windows.Forms.TextBox();
            this.thrdTxt = new System.Windows.Forms.Label();
            this.notes = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // exitButton
            // 
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
            this.fstMsg.Font = new System.Drawing.Font("Miriam Fixed", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.ConnectButton.Location = new System.Drawing.Point(342, 290);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(99, 46);
            this.ConnectButton.TabIndex = 3;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(315, 286);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(126, 50);
            this.SendButton.TabIndex = 4;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Visible = false;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // MsgBox
            // 
            this.MsgBox.Location = new System.Drawing.Point(68, 95);
            this.MsgBox.Multiline = true;
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.Size = new System.Drawing.Size(256, 69);
            this.MsgBox.TabIndex = 5;
            this.MsgBox.Visible = false;
            // 
            // scndTxt
            // 
            this.scndTxt.AutoSize = true;
            this.scndTxt.Font = new System.Drawing.Font("Miriam Fixed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scndTxt.Location = new System.Drawing.Point(64, 60);
            this.scndTxt.Name = "scndTxt";
            this.scndTxt.Size = new System.Drawing.Size(217, 20);
            this.scndTxt.TabIndex = 6;
            this.scndTxt.Text = "Message To Send:";
            this.scndTxt.Visible = false;
            // 
            // recepientName
            // 
            this.recepientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recepientName.Location = new System.Drawing.Point(68, 213);
            this.recepientName.Name = "recepientName";
            this.recepientName.Size = new System.Drawing.Size(209, 29);
            this.recepientName.TabIndex = 7;
            this.recepientName.Visible = false;
            // 
            // thrdTxt
            // 
            this.thrdTxt.AutoSize = true;
            this.thrdTxt.Font = new System.Drawing.Font("Miriam Fixed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thrdTxt.Location = new System.Drawing.Point(64, 174);
            this.thrdTxt.Name = "thrdTxt";
            this.thrdTxt.Size = new System.Drawing.Size(113, 20);
            this.thrdTxt.TabIndex = 8;
            this.thrdTxt.Text = "Send To:";
            this.thrdTxt.Visible = false;
            // 
            // notes
            // 
            this.notes.AutoSize = true;
            this.notes.Location = new System.Drawing.Point(383, 111);
            this.notes.Name = "notes";
            this.notes.Size = new System.Drawing.Size(0, 13);
            this.notes.TabIndex = 9;
            // 
            // TorChater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 359);
            this.Controls.Add(this.notes);
            this.Controls.Add(this.thrdTxt);
            this.Controls.Add(this.recepientName);
            this.Controls.Add(this.scndTxt);
            this.Controls.Add(this.MsgBox);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.fstMsg);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.exitButton);
            this.Name = "TorChater";
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
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.TextBox MsgBox;
        private System.Windows.Forms.Label scndTxt;
        private System.Windows.Forms.TextBox recepientName;
        private System.Windows.Forms.Label thrdTxt;
        private System.Windows.Forms.LinkLabel notes;
    }
}


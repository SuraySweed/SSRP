namespace TorChatClient
{
    partial class ChatGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatGUI));
            this.ChatText = new System.Windows.Forms.RichTextBox();
            this.messageToSendBox = new System.Windows.Forms.TextBox();
            this.HelppingText = new System.Windows.Forms.Label();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.exitBut = new System.Windows.Forms.Button();
            this.NameOfOther = new System.Windows.Forms.TextBox();
            this.moreHelp = new System.Windows.Forms.Label();
            this.getNameButton = new System.Windows.Forms.Button();
            this.otherName = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ChatText
            // 
            this.ChatText.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ChatText.Font = new System.Drawing.Font("Miriam Fixed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatText.ForeColor = System.Drawing.Color.RoyalBlue;
            this.ChatText.Location = new System.Drawing.Point(24, 21);
            this.ChatText.Name = "ChatText";
            this.ChatText.ReadOnly = true;
            this.ChatText.Size = new System.Drawing.Size(410, 256);
            this.ChatText.TabIndex = 0;
            this.ChatText.Text = "";
            this.ChatText.TextChanged += new System.EventHandler(this.ChatText_TextChanged);
            // 
            // messageToSendBox
            // 
            this.messageToSendBox.Location = new System.Drawing.Point(215, 331);
            this.messageToSendBox.Multiline = true;
            this.messageToSendBox.Name = "messageToSendBox";
            this.messageToSendBox.Size = new System.Drawing.Size(349, 82);
            this.messageToSendBox.TabIndex = 1;
            this.messageToSendBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // HelppingText
            // 
            this.HelppingText.AutoSize = true;
            this.HelppingText.Font = new System.Drawing.Font("Miriam Fixed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelppingText.ForeColor = System.Drawing.Color.ForestGreen;
            this.HelppingText.Location = new System.Drawing.Point(20, 296);
            this.HelppingText.Name = "HelppingText";
            this.HelppingText.Size = new System.Drawing.Size(262, 19);
            this.HelppingText.TabIndex = 2;
            this.HelppingText.Text = "Type your message here:";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Enabled = false;
            this.sendMessageButton.Font = new System.Drawing.Font("Miriam Fixed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendMessageButton.ForeColor = System.Drawing.Color.Indigo;
            this.sendMessageButton.Location = new System.Drawing.Point(24, 349);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(167, 40);
            this.sendMessageButton.TabIndex = 3;
            this.sendMessageButton.Text = "Send";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // exitBut
            // 
            this.exitBut.Font = new System.Drawing.Font("Miriam Fixed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitBut.ForeColor = System.Drawing.Color.Indigo;
            this.exitBut.Location = new System.Drawing.Point(454, 220);
            this.exitBut.Name = "exitBut";
            this.exitBut.Size = new System.Drawing.Size(142, 57);
            this.exitBut.TabIndex = 4;
            this.exitBut.Text = "Exit";
            this.exitBut.UseVisualStyleBackColor = true;
            this.exitBut.Click += new System.EventHandler(this.exitBut_Click);
            // 
            // NameOfOther
            // 
            this.NameOfOther.Location = new System.Drawing.Point(454, 75);
            this.NameOfOther.Name = "NameOfOther";
            this.NameOfOther.Size = new System.Drawing.Size(142, 20);
            this.NameOfOther.TabIndex = 5;
            this.NameOfOther.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // moreHelp
            // 
            this.moreHelp.AutoSize = true;
            this.moreHelp.Font = new System.Drawing.Font("Miriam Fixed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moreHelp.ForeColor = System.Drawing.Color.ForestGreen;
            this.moreHelp.Location = new System.Drawing.Point(451, 42);
            this.moreHelp.Name = "moreHelp";
            this.moreHelp.Size = new System.Drawing.Size(98, 16);
            this.moreHelp.TabIndex = 6;
            this.moreHelp.Text = "Where to?";
            // 
            // getNameButton
            // 
            this.getNameButton.Enabled = false;
            this.getNameButton.Font = new System.Drawing.Font("Miriam Fixed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getNameButton.ForeColor = System.Drawing.Color.Indigo;
            this.getNameButton.Location = new System.Drawing.Point(474, 114);
            this.getNameButton.Name = "getNameButton";
            this.getNameButton.Size = new System.Drawing.Size(75, 23);
            this.getNameButton.TabIndex = 7;
            this.getNameButton.Text = "Set";
            this.getNameButton.UseVisualStyleBackColor = true;
            this.getNameButton.Click += new System.EventHandler(this.getNameButton_Click);
            // 
            // otherName
            // 
            this.otherName.AutoSize = true;
            this.otherName.Font = new System.Drawing.Font("Miriam Fixed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.otherName.Location = new System.Drawing.Point(466, 75);
            this.otherName.Name = "otherName";
            this.otherName.Size = new System.Drawing.Size(0, 16);
            this.otherName.TabIndex = 8;
            this.otherName.Visible = false;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Font = new System.Drawing.Font("Miriam Fixed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disconnectButton.ForeColor = System.Drawing.Color.Indigo;
            this.disconnectButton.Location = new System.Drawing.Point(454, 114);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(81, 23);
            this.disconnectButton.TabIndex = 9;
            this.disconnectButton.Text = "Change";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Visible = false;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ChatGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(625, 471);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.otherName);
            this.Controls.Add(this.getNameButton);
            this.Controls.Add(this.moreHelp);
            this.Controls.Add(this.NameOfOther);
            this.Controls.Add(this.exitBut);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.HelppingText);
            this.Controls.Add(this.messageToSendBox);
            this.Controls.Add(this.ChatText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatGUI";
            this.Text = "ChatForm";
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ChatText;
        private System.Windows.Forms.TextBox messageToSendBox;
        private System.Windows.Forms.Label HelppingText;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.Button exitBut;
        private System.Windows.Forms.TextBox NameOfOther;
        private System.Windows.Forms.Label moreHelp;
        private System.Windows.Forms.Button getNameButton;
        private System.Windows.Forms.Label otherName;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Timer timer1;
    }
}
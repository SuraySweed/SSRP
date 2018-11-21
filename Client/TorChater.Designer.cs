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
            this.typeCommand = new System.Windows.Forms.Label();
            this.MessangerBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.recievedMessages = new System.Windows.Forms.RichTextBox();
            this.serverResponse = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(50, 274);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(120, 45);
            this.exitButton.TabIndex = 0;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // typeCommand
            // 
            this.typeCommand.AutoSize = true;
            this.typeCommand.Font = new System.Drawing.Font("Miriam Fixed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeCommand.Location = new System.Drawing.Point(63, 41);
            this.typeCommand.Name = "typeCommand";
            this.typeCommand.Size = new System.Drawing.Size(207, 19);
            this.typeCommand.TabIndex = 1;
            this.typeCommand.Text = "Type Message Here:";
            // 
            // MessangerBox
            // 
            this.MessangerBox.Location = new System.Drawing.Point(263, 41);
            this.MessangerBox.Name = "MessangerBox";
            this.MessangerBox.Size = new System.Drawing.Size(203, 20);
            this.MessangerBox.TabIndex = 2;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(318, 261);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(110, 58);
            this.sendButton.TabIndex = 3;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // recievedMessages
            // 
            this.recievedMessages.Location = new System.Drawing.Point(64, 146);
            this.recievedMessages.Name = "recievedMessages";
            this.recievedMessages.Size = new System.Drawing.Size(228, 51);
            this.recievedMessages.TabIndex = 4;
            this.recievedMessages.Text = "";
            // 
            // serverResponse
            // 
            this.serverResponse.AutoSize = true;
            this.serverResponse.Font = new System.Drawing.Font("Miriam Fixed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverResponse.Location = new System.Drawing.Point(63, 124);
            this.serverResponse.Name = "serverResponse";
            this.serverResponse.Size = new System.Drawing.Size(207, 19);
            this.serverResponse.TabIndex = 5;
            this.serverResponse.Text = "Server\'s Response:";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(263, 84);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(135, 20);
            this.nameBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Miriam Fixed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Name Of Recipient:";
            // 
            // TorChater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 359);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.serverResponse);
            this.Controls.Add(this.recievedMessages);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.MessangerBox);
            this.Controls.Add(this.typeCommand);
            this.Controls.Add(this.exitButton);
            this.Name = "TorChater";
            this.Text = "TorChater";
            this.Load += new System.EventHandler(this.TorChater_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label typeCommand;
        private System.Windows.Forms.TextBox MessangerBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.RichTextBox recievedMessages;
        private System.Windows.Forms.Label serverResponse;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label label1;
    }
}


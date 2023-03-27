namespace AutoEpicSeven
{
    partial class Form1
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
            this.RunBtn = new System.Windows.Forms.Button();
            this.TestBtn = new System.Windows.Forms.Button();
            this.CropBtn = new System.Windows.Forms.Button();
            this.flagInput = new System.Windows.Forms.TextBox();
            this.flagInputLabel = new System.Windows.Forms.Label();
            this.countLabel = new System.Windows.Forms.Label();
            this.adbLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RunBtn
            // 
            this.RunBtn.Location = new System.Drawing.Point(292, 14);
            this.RunBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(115, 46);
            this.RunBtn.TabIndex = 4;
            this.RunBtn.Text = "Run";
            this.RunBtn.UseVisualStyleBackColor = true;
            this.RunBtn.Click += new System.EventHandler(this.RunBtn_Click);
            // 
            // TestBtn
            // 
            this.TestBtn.Location = new System.Drawing.Point(24, 140);
            this.TestBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(115, 46);
            this.TestBtn.TabIndex = 5;
            this.TestBtn.Text = "Test";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // CropBtn
            // 
            this.CropBtn.Location = new System.Drawing.Point(24, 69);
            this.CropBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CropBtn.Name = "CropBtn";
            this.CropBtn.Size = new System.Drawing.Size(115, 46);
            this.CropBtn.TabIndex = 6;
            this.CropBtn.Text = "Crop";
            this.CropBtn.UseVisualStyleBackColor = true;
            this.CropBtn.Click += new System.EventHandler(this.CropBtn_Click);
            // 
            // flagInput
            // 
            this.flagInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagInput.Location = new System.Drawing.Point(235, 20);
            this.flagInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flagInput.Name = "flagInput";
            this.flagInput.Size = new System.Drawing.Size(41, 30);
            this.flagInput.TabIndex = 7;
            // 
            // flagInputLabel
            // 
            this.flagInputLabel.AutoSize = true;
            this.flagInputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagInputLabel.Location = new System.Drawing.Point(28, 27);
            this.flagInputLabel.Name = "flagInputLabel";
            this.flagInputLabel.Size = new System.Drawing.Size(185, 25);
            this.flagInputLabel.TabIndex = 8;
            this.flagInputLabel.Text = "Enter flag\'s number:";
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countLabel.Location = new System.Drawing.Point(288, 62);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(104, 20);
            this.countLabel.TabIndex = 9;
            this.countLabel.Text = "0 time(s) left";
            // 
            // adbLabel
            // 
            this.adbLabel.AutoSize = true;
            this.adbLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adbLabel.Location = new System.Drawing.Point(183, 161);
            this.adbLabel.Name = "adbLabel";
            this.adbLabel.Size = new System.Drawing.Size(64, 25);
            this.adbLabel.TabIndex = 10;
            this.adbLabel.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 214);
            this.Controls.Add(this.adbLabel);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.flagInputLabel);
            this.Controls.Add(this.flagInput);
            this.Controls.Add(this.CropBtn);
            this.Controls.Add(this.TestBtn);
            this.Controls.Add(this.RunBtn);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Auto EpicSeven";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RunBtn;
        private System.Windows.Forms.Button TestBtn;
        private System.Windows.Forms.Button CropBtn;
        private System.Windows.Forms.TextBox flagInput;
        private System.Windows.Forms.Label flagInputLabel;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.Label adbLabel;
    }
}


namespace Read_JSON
{
    partial class fMainForm
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
            this.btnDocFileJSON = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDocFileJSON
            // 
            this.btnDocFileJSON.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDocFileJSON.Location = new System.Drawing.Point(128, 84);
            this.btnDocFileJSON.Name = "btnDocFileJSON";
            this.btnDocFileJSON.Size = new System.Drawing.Size(505, 240);
            this.btnDocFileJSON.TabIndex = 0;
            this.btnDocFileJSON.Text = "Đọc file JSON";
            this.btnDocFileJSON.UseVisualStyleBackColor = true;
            this.btnDocFileJSON.Click += new System.EventHandler(this.btnDocFileJSON_Click);
            // 
            // fMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDocFileJSON);
            this.Name = "fMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDocFileJSON;
    }
}


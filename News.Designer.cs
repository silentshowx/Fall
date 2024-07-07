namespace Fall
{
    partial class News
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.pCube = new System.Windows.Forms.PictureBox();
            this.bCube = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pCube)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(759, 597);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(100, 40);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // pCube
            // 
            this.pCube.Location = new System.Drawing.Point(688, 752);
            this.pCube.Name = "pCube";
            this.pCube.Size = new System.Drawing.Size(107, 67);
            this.pCube.TabIndex = 2;
            this.pCube.TabStop = false;
            this.pCube.Tag = "72";
            // 
            // bCube
            // 
            this.bCube.Location = new System.Drawing.Point(605, 581);
            this.bCube.Name = "bCube";
            this.bCube.Size = new System.Drawing.Size(100, 40);
            this.bCube.TabIndex = 3;
            this.bCube.UseVisualStyleBackColor = true;
            // 
            // News
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 891);
            this.Controls.Add(this.bCube);
            this.Controls.Add(this.pCube);
            this.Controls.Add(this.btnGenerate);
            this.Name = "News";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "News";
            ((System.ComponentModel.ISupportInitialize)(this.pCube)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.PictureBox pCube;
        private System.Windows.Forms.Button bCube;
    }
}
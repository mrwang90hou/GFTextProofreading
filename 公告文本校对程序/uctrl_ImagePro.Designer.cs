namespace ImgeProClass
{
    partial class uctrl_ImagePro
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // uctrl_ImagePro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "uctrl_ImagePro";
            this.Size = new System.Drawing.Size(927, 608);
            this.Load += new System.EventHandler(this.uctrl_ImagePro_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Uctrl_ImagePro_Scroll);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.uctrl_ImagePro_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uctrl_ImagePro_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.uctrl_ImagePro_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.uctrl_ImagePro_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

using System.Windows.Forms;

namespace os_lab2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtDir1;
        private System.Windows.Forms.TextBox txtDir2;
        private System.Windows.Forms.TextBox txtM;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblResult1;
        private System.Windows.Forms.Label lblResult2;
        private System.Windows.Forms.Label lblCompare;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtDir1 = new System.Windows.Forms.TextBox();
            this.txtDir2 = new System.Windows.Forms.TextBox();
            this.txtM = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblResult1 = new System.Windows.Forms.Label();
            this.lblResult2 = new System.Windows.Forms.Label();
            this.lblCompare = new System.Windows.Forms.Label();
            this.SuspendLayout();

            txtDir1.Text = @"C:\Test1";
            txtDir2.Text = @"C:\Test2";
            txtM.Text = "5";

            btnStart.Text = "Старт";
            btnStart.Click += new System.EventHandler(this.btnStart_Click);

            txtDir1.SetBounds(20, 20, 300, 25);
            txtDir2.SetBounds(20, 60, 300, 25);
            txtM.SetBounds(20, 100, 100, 25);
            btnStart.SetBounds(20, 140, 100, 30);

            lblResult1.SetBounds(20, 180, 300, 25);
            lblResult2.SetBounds(20, 210, 300, 25);
            lblCompare.SetBounds(20, 240, 400, 50);

            Controls.AddRange(new Control[]
            {
                txtDir1, txtDir2, txtM, btnStart,
                lblResult1, lblResult2, lblCompare
            });

            this.ClientSize = new System.Drawing.Size(450, 320);
            this.Text = "File Counter";
            this.ResumeLayout(false);
        }
    }
}

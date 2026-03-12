namespace CatchButton
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRunAway = new Button();
            SuspendLayout();
            // 
            // btnRunAway
            // 
            btnRunAway.BackColor = Color.Yellow;
            btnRunAway.Font = new Font("맑은 고딕", 36F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btnRunAway.ForeColor = Color.Red;
            btnRunAway.Location = new Point(248, 199);
            btnRunAway.Name = "btnRunAway";
            btnRunAway.Size = new Size(391, 107);
            btnRunAway.TabIndex = 0;
            btnRunAway.Text = "나를 잡아봐";
            btnRunAway.UseVisualStyleBackColor = false;
            btnRunAway.Click += button1_Click;
            btnRunAway.MouseEnter += btnRunAway_MouseEnter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(986, 578);
            Controls.Add(btnRunAway);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnRunAway;
    }
}

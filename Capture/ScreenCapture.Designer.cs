namespace Capture
{
    partial class ScreenCapture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenCapture));
            btn_Capture = new Button();
            btn_SelectArea = new Button();
            btn_ToWord = new Button();
            btn_Close = new Button();
            SuspendLayout();
            // 
            // btn_Capture
            // 
            btn_Capture.Enabled = false;
            btn_Capture.Location = new Point(12, 59);
            btn_Capture.Name = "btn_Capture";
            btn_Capture.Size = new Size(255, 104);
            btn_Capture.TabIndex = 0;
            btn_Capture.Text = "Capture";
            btn_Capture.UseVisualStyleBackColor = true;
            btn_Capture.Click += btn_Capture_Click;
            // 
            // btn_SelectArea
            // 
            btn_SelectArea.Location = new Point(12, 13);
            btn_SelectArea.Name = "btn_SelectArea";
            btn_SelectArea.Size = new Size(255, 40);
            btn_SelectArea.TabIndex = 1;
            btn_SelectArea.Text = "Select Area";
            btn_SelectArea.UseVisualStyleBackColor = true;
            btn_SelectArea.Click += btn_SelectArea_Click;
            // 
            // btn_ToWord
            // 
            btn_ToWord.Location = new Point(12, 170);
            btn_ToWord.Name = "btn_ToWord";
            btn_ToWord.Size = new Size(94, 29);
            btn_ToWord.TabIndex = 3;
            btn_ToWord.Text = "To Word";
            btn_ToWord.UseVisualStyleBackColor = true;
            btn_ToWord.Click += btn_ToWord_Click;
            // 
            // btn_Close
            // 
            btn_Close.Location = new Point(173, 169);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new Size(94, 29);
            btn_Close.TabIndex = 5;
            btn_Close.Text = "Close";
            btn_Close.UseVisualStyleBackColor = true;
            btn_Close.Click += btn_Close_Click;
            // 
            // ScreenCapture
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(306, 268);
            ControlBox = false;
            Controls.Add(btn_Close);
            Controls.Add(btn_ToWord);
            Controls.Add(btn_SelectArea);
            Controls.Add(btn_Capture);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ScreenCapture";
            Opacity = 0.9D;
            Text = "Sneha's Screen Capture";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Capture;
        private Button btn_SelectArea;
        private Button btn_ToWord;
        private Button btn_Close;
    }
}

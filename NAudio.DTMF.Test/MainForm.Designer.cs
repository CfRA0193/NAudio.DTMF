namespace NAudio.DTMF.Test
{
    partial class MainForm
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
            this._playDTMFButton = new System.Windows.Forms.Button();
            this._dtmfTextBox = new System.Windows.Forms.TextBox();
            this._volumeTrackBar = new System.Windows.Forms.TrackBar();
            this._volumeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._volumeTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // _playDTMFButton
            // 
            this._playDTMFButton.Location = new System.Drawing.Point(12, 38);
            this._playDTMFButton.Name = "_playDTMFButton";
            this._playDTMFButton.Size = new System.Drawing.Size(75, 39);
            this._playDTMFButton.TabIndex = 1;
            this._playDTMFButton.Text = "Play DTMF";
            this._playDTMFButton.UseVisualStyleBackColor = true;
            this._playDTMFButton.Click += new System.EventHandler(this._playDTMFButton_Click);
            // 
            // _dtmfTextBox
            // 
            this._dtmfTextBox.Location = new System.Drawing.Point(12, 12);
            this._dtmfTextBox.Name = "_dtmfTextBox";
            this._dtmfTextBox.Size = new System.Drawing.Size(260, 20);
            this._dtmfTextBox.TabIndex = 0;
            this._dtmfTextBox.Text = "151 262 888 111";
            // 
            // _volumeTrackBar
            // 
            this._volumeTrackBar.Location = new System.Drawing.Point(280, 3);
            this._volumeTrackBar.Maximum = 100;
            this._volumeTrackBar.Name = "_volumeTrackBar";
            this._volumeTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this._volumeTrackBar.Size = new System.Drawing.Size(45, 86);
            this._volumeTrackBar.TabIndex = 2;
            this._volumeTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this._volumeTrackBar.Value = 100;
            this._volumeTrackBar.ValueChanged += new System.EventHandler(this._volumeTrackBar_ValueChanged);
            // 
            // _volumeLabel
            // 
            this._volumeLabel.AutoSize = true;
            this._volumeLabel.Location = new System.Drawing.Point(200, 51);
            this._volumeLabel.Name = "_volumeLabel";
            this._volumeLabel.Size = new System.Drawing.Size(74, 13);
            this._volumeLabel.TabIndex = 3;
            this._volumeLabel.Text = "Volume: 100%";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 86);
            this.Controls.Add(this._volumeLabel);
            this.Controls.Add(this._volumeTrackBar);
            this.Controls.Add(this._dtmfTextBox);
            this.Controls.Add(this._playDTMFButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NAudio.DTMF.Test";
            ((System.ComponentModel.ISupportInitialize)(this._volumeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _playDTMFButton;
        private System.Windows.Forms.TextBox _dtmfTextBox;
        private System.Windows.Forms.TrackBar _volumeTrackBar;
        private System.Windows.Forms.Label _volumeLabel;
    }
}


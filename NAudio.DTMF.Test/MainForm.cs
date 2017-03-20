using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAudio.DTMF.Test
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
              
        private void _volumeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            _volumeLabel.Text = String.Format("Volume: {0}%", _volumeTrackBar.Value);
        }

        private void _playDTMFButton_Click(object sender, EventArgs e)
        {
            var outputAudioDeviceSelectedIndex = 1;
            var dtmf = new DTMFGenerator(ref outputAudioDeviceSelectedIndex, 48000) { Volume = _volumeTrackBar.Value / 100.0F };
            dtmf.Play(_dtmfTextBox.Text);
        }
    }
}

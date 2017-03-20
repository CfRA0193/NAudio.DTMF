using NAudio.Wave;
using System.Collections.Generic;

namespace NAudio.DTMF
{
    public static class AudioUtils
    {
        public static string[] GetWaveInNames()
        {
            List<string> deviceNames = new List<string>();
            for (var i = 0; i < WaveIn.DeviceCount; i++)
            {
                var deviceName = WaveIn.GetCapabilities(i).ProductName;
                deviceNames.Add(deviceName);
            }
            return deviceNames.ToArray();
        }

        public static string[] GetWaveOutNames()
        {
            List<string> deviceNames = new List<string>();
            for (var i = 0; i < WaveOut.DeviceCount; i++)
            {
                var deviceName = WaveOut.GetCapabilities(i).ProductName;
                deviceNames.Add(deviceName);
            }
            return deviceNames.ToArray();
        }
    }
}

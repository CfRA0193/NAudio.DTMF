using System;
using System.Collections.Generic;
using NAudio.Wave;

namespace NAudio.DTMF
{
    public class SineGenerator
    {
        private int _deviceNumber;
        private int _sampleRate;
        private int _bits;
        private float _waveOutVolume = 1.0F;
        private WaveOut _waveOut;
               
        private object _syncRoot = new object();

        public int SampleRate
        {
            get
            {
                return _sampleRate;
            }
        }

        public int Bits
        {
            get
            {
                return _bits;
            }
        }

        public float Volume
        {
            get
            {
                lock (_syncRoot)
                {
                    return _waveOutVolume;
                }
            }
            set
            {
                lock (_syncRoot)
                {
                    if (value < 0F || value > 1F)
                    {
                        throw new Exception("SineGenerator: Volume < 0 Or Volume > 1");
                    }
                    _waveOutVolume = value;
                    if (_waveOut != null)
                    {
                        _waveOut.Volume = _waveOutVolume;
                    }
                }
            }
        }

        public SineGenerator(ref int selectedDeviceNumber, int sampleRate, int bits = 24) //DeviceNumber -> Out variable
        {
            selectedDeviceNumber = (selectedDeviceNumber < 0) ? 0 : selectedDeviceNumber;
            _sampleRate = sampleRate;
            _bits = bits;
            if (bits != 8 && bits != 16 && bits != 24)
            {
                throw new Exception("SineGenerator: wrong bit depth");
            }
            var waveFormat = new WaveFormat(_sampleRate, _bits, 2);
            var waveProvider = new BufferedWaveProvider(waveFormat);
            try
            {
                _waveOut = new WaveOut() { DeviceNumber = selectedDeviceNumber };
                _waveOut.Init(waveProvider);
                _deviceNumber = selectedDeviceNumber;
            }
            catch
            {                
                int waveOutNamesLength = AudioUtils.GetWaveOutNames().Length;
                for (var i = 0; i < waveOutNamesLength; i++)
                {
                    var exc = false;
                    if (i != selectedDeviceNumber)
                    {
                        try
                        {
                            _waveOut = new WaveOut() { DeviceNumber = i };
                            _waveOut.Init(waveProvider);
                            selectedDeviceNumber = i;
                        }
                        catch
                        {
                            _waveOut = null;
                            exc = true;
                        }
                        if (!exc)
                        {
                            break;
                        }
                    }
                }
            }
            _waveOut = null;
        }

        public void Play(Queue<SineTaskBlock> program)
        {
            lock (_syncRoot)
            {
                PlayWith(new ProgrammedSineWaveProvider32(program));
            }
        }

        public void SwitchOn(IEnumerable<float> frequencies)
        {
            lock (_syncRoot)
            {
                PlayWith(new ProgrammedSineWaveProvider32(frequencies));
            }
        }

        public void SwitchOff()
        {
            lock (_syncRoot)
            {
                if (_waveOut != null)
                {
                    _waveOut.Stop();
                    _waveOut.Dispose();
                    _waveOut = null;
                }
            }
        }

        private void PlayWith(ProgrammedSineWaveProvider32 sineWaveProvider)
        {
            lock (_syncRoot)
            {
                if (_waveOut == null)
                {
                    sineWaveProvider.SetWaveFormat(_sampleRate, 2);
                    _waveOut = new WaveOut() { DeviceNumber = _deviceNumber };
                    _waveOut.Init(sineWaveProvider);
                    _waveOut.Volume = _waveOutVolume;
                    _waveOut.Play();
                }
                else
                {
                    SwitchOff();
                    PlayWith(sineWaveProvider);
                }
            }
        }
    }
}

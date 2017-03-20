using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NAudio.DTMF
{
    public class ProgrammedSineWaveProvider32 : WaveProvider32
    {
        private int _sampleIdx;

        public Queue<SineTaskBlock> Program { get; set; }

        public ProgrammedSineWaveProvider32()
        {
        }

        public ProgrammedSineWaveProvider32(Queue<SineTaskBlock> program)
        {
            this.Program = new Queue<SineTaskBlock>(program);
        }

        public ProgrammedSineWaveProvider32(IEnumerable<float> frequencies)
        {
            this.Program = new Queue<SineTaskBlock>(new[] { new SineTaskBlock(frequencies, frequencies.Select((item) => 1.0F).ToArray()) });
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            var sampleRate = base.WaveFormat.SampleRate;
            var nChannels = base.WaveFormat.Channels;
            sampleCount /= nChannels;
            for (var sampleIdx = 0; sampleIdx < sampleCount; sampleIdx++)
            {
                var program = this.Program.FirstOrDefault();
                if (program != null)
                {
                    var ampSum = program.Amplitudes.Sum();
                    float sample = 0F;
                    for (var sineIdx = 0; sineIdx < program.Frequencies.Count; sineIdx++)
                    {
                        sample += Convert.ToSingle((program.Amplitudes[sineIdx] / ampSum) * Math.Sin((2 * Math.PI * _sampleIdx * program.Frequencies[sineIdx]) / sampleRate));
                    }
                    for (var channel = 0; channel < nChannels; channel++)
                    {
                        buffer[((nChannels * sampleIdx) + channel) + offset] = sample;
                    }
                    NextSample(sampleRate);
                    if (!program.NextSampleAllowed())
                    {
                        this.Program.Dequeue();
                    }
                }
                else
                {
                    return 0;
                }
            }
            return sampleCount * nChannels;
        }

        private void NextSample(int sampleRate)
        {
            _sampleIdx += 1;
            if (_sampleIdx >= sampleRate)
            {
                _sampleIdx = 0;
            }
        }
    }
}

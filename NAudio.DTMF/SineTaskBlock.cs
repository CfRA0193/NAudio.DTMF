using System.Collections.Generic;

namespace NAudio.DTMF
{
    public class SineTaskBlock
    {
        private ulong _samplesTotal;
        private ulong _samplesGenerated;
        public List<float> Frequencies { get; set; }
        public List<float> Amplitudes { get; set; }

        public SineTaskBlock(IEnumerable<float> frequencies, IEnumerable<float> amplitudes, ulong samplesTotal = ulong.MaxValue)
        {
            this.Frequencies = new List<float>(frequencies);
            this.Amplitudes = new List<float>(amplitudes);
            _samplesTotal = samplesTotal;
            _samplesGenerated = 0;
        }

        public bool NextSampleAllowed()
        {
            _samplesGenerated += 1;
            return _samplesGenerated < _samplesTotal;
        }
    }
}

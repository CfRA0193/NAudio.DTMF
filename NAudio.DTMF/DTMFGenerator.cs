using System;
using System.Collections.Generic;

namespace NAudio.DTMF
{
    public class DTMFGenerator
    {
        private struct FreqPair
        {
            public readonly float LowFreq;
            public readonly float HighFreq;
            public FreqPair(float lowFreq, float highFreq) : this()
            {
                this.LowFreq = lowFreq;
                this.HighFreq = highFreq;
            }
        }

        private double _baseSymbolTime = 160.0; //160.0
        private double _baseSpaceTime = 40.0; //40.0
        private double _speed = 1.0; //1.0
        private SineGenerator _generator;
        private Dictionary<char, FreqPair> _encodeMatrix = new Dictionary<char, FreqPair>();

        private object _syncRoot = new object();

        public double SymbolTime
        {
            get
            {
                lock (_syncRoot)
                {
                    return _baseSymbolTime / Speed;
                }
            }
        }

        public double SpaceTime
        {
            get
            {
                lock (_syncRoot)
                {
                    return _baseSpaceTime / Speed;
                }
            }
        }

        public double Speed
        {
            get
            {
                lock (_syncRoot)
                {
                    return _speed;
                }
            }
            set
            {
                lock (_syncRoot)
                {
                    _speed = value;
                }
            }
        }

        public float Volume
        {
            get
            {
                lock (_syncRoot)
                {
                    return _generator.Volume;
                }
            }
            set
            {
                lock (_syncRoot)
                {
                    _generator.Volume = value;
                }
            }
        }

        public DTMFGenerator(ref int deviceNumber, int sampleRate)
        {
            _generator = new SineGenerator(ref deviceNumber, sampleRate);

            _encodeMatrix.Add('1', new FreqPair(697F, 1209F)); //697, 1209
            _encodeMatrix.Add('2', new FreqPair(697F, 1336F)); //697, 1336
            _encodeMatrix.Add('3', new FreqPair(697F, 1477F)); //697, 1477
            _encodeMatrix.Add('A', new FreqPair(697F, 1633F)); //697, 1633

            _encodeMatrix.Add('4', new FreqPair(770F, 1209F)); //770, 1209
            _encodeMatrix.Add('5', new FreqPair(770F, 1336F)); //770, 1336
            _encodeMatrix.Add('6', new FreqPair(770F, 1477F)); //770, 1477
            _encodeMatrix.Add('B', new FreqPair(770F, 1633F)); //770, 1633

            _encodeMatrix.Add('7', new FreqPair(852F, 1209F)); //852, 1209
            _encodeMatrix.Add('8', new FreqPair(852F, 1336F)); //852, 1336
            _encodeMatrix.Add('9', new FreqPair(852F, 1477F)); //852, 1477
            _encodeMatrix.Add('C', new FreqPair(852F, 1633F)); //852, 1633

            _encodeMatrix.Add('*', new FreqPair(941F, 1209F)); //941, 1209
            _encodeMatrix.Add('0', new FreqPair(941F, 1336F)); //941, 1336
            _encodeMatrix.Add('#', new FreqPair(941F, 1477F)); //941, 1477
            _encodeMatrix.Add('D', new FreqPair(941F, 1633F)); //941, 1633
        }

        public void Play(string sequence)
        {
            lock (_syncRoot)
            {
                Queue<SineTaskBlock> program = new Queue<SineTaskBlock>();
                foreach (var s in sequence.ToUpper())
                {
                    if (_encodeMatrix.ContainsKey(s))
                    {
                        var freqPair = _encodeMatrix[s];
                        program.Enqueue(new SineTaskBlock(new[] { freqPair.LowFreq, freqPair.HighFreq }, new[] { 1.0F, 1.0F }, (ulong)Math.Round(((this.SymbolTime / 1000.0) * _generator.SampleRate)))); //symbol
                        program.Enqueue(new SineTaskBlock(new[] { 0F, 0F }, new[] { 0F, 0F }, (ulong)Math.Round(((this.SpaceTime / 1000.0) * _generator.SampleRate)))); //space
                    }
                    else
                    {
                        program.Enqueue(new SineTaskBlock(new[] { 0F, 0F }, new[] { 0F, 0F }, (ulong)Math.Round(((this.SymbolTime / 1000.0) * _generator.SampleRate)))); //space symbol
                    }
                }
                _generator.Play(program);
            }
        }
    }
}

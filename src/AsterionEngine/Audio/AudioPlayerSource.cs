/*
==========================================================================
This file is part of Asterion Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
WadPacker is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
WadPacker is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with Asterion Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using OpenTK.Audio.OpenAL;
using System;
using System.IO;

namespace Asterion.Audio
{
    internal struct AudioPlayerSource : IDisposable
    {
        internal readonly int Source;
        internal readonly int Buffer;

        internal readonly bool IsValid;

        internal AudioPlayerSource(byte[] soundFileBytes)
        {
            Buffer = AL.GenBuffer();
            Source = AL.GenSource();
            IsValid = false;

            using (MemoryStream ms = new MemoryStream(soundFileBytes))
            {
                byte[] soundData = LoadWave(ms, out int channels, out int bits_per_sample, out int sample_rate, out IsValid);

                AL.BufferData(Buffer, GetSoundFormat(channels, bits_per_sample), soundData, soundData.Length, sample_rate);
            }
        }

        private byte[] LoadWave(Stream stream, out int channels, out int bits, out int rate, out bool isValid)
        {
            isValid = true; channels = 1; bits = 16; rate = 11025;

            if (stream == null) { isValid = false; return new byte[0]; }

            using (BinaryReader reader = new BinaryReader(stream))
            {
                string signature = new string(reader.ReadChars(4));
                if (signature != "RIFF") { isValid = false; return new byte[0]; }

                int riff_chunck_size = reader.ReadInt32();

                string format = new string(reader.ReadChars(4));
                if (format != "WAVE") { isValid = false; return new byte[0]; }

                string format_signature = new string(reader.ReadChars(4));
                if (format_signature != "fmt ") { isValid = false; return new byte[0]; }

                int format_chunk_size = reader.ReadInt32();
                int audio_format = reader.ReadInt16();
                int num_channels = reader.ReadInt16();
                int sample_rate = reader.ReadInt32();
                int byte_rate = reader.ReadInt32();
                int block_align = reader.ReadInt16();
                int bits_per_sample = reader.ReadInt16();

                string data_signature = new string(reader.ReadChars(4));
                if (data_signature != "data") { isValid = false; return new byte[0]; }

                int data_chunk_size = reader.ReadInt32();

                channels = num_channels;
                bits = bits_per_sample;
                rate = sample_rate;

                return reader.ReadBytes((int)reader.BaseStream.Length);
            }
        }

        private ALFormat GetSoundFormat(int channels, int bits)
        {
            switch (channels)
            {
                case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                default: throw new NotSupportedException("The specified sound format is not supported.");
            }
        }

        public void Dispose()
        {
            AL.SourceStop(Source);
            AL.DeleteSource(Source);
            AL.DeleteBuffer(Buffer);
        }
    }
}

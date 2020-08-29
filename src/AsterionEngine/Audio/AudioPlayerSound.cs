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
    /// <summary>
    /// (Internal) Creates and stores an OpenAL sound buffer from a PCM Wave file.
    /// </summary>
    internal class AudioPlayerSound : IDisposable
    {
        /// <summary>
        /// (Internal) OpenAL buffer handle.
        /// </summary>
        internal int Buffer { get; private set; }

        /// <summary>
        /// (Internal) Does this sound source contains valid data?
        /// </summary>
        internal bool IsValid { get; private set; }

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        /// <param name="waveFileBytes">Bytes for a PCM Wave file to use for this sound</param>
        internal AudioPlayerSound(byte[] waveFileBytes)
        {
            Buffer = AL.GenBuffer();
            IsValid = true;

            using (MemoryStream ms = new MemoryStream(waveFileBytes))
            {
                byte[] soundData = LoadWave(ms, out int channels, out int bitsPerSample, out int sampleRate);
                if (soundData == null) { IsValid = false; Dispose(); return; }

                ALFormat? soundFormat = GetSoundFormat(channels, bitsPerSample);
                if (!soundFormat.HasValue) { IsValid = false; Dispose(); return; }

                AL.BufferData(Buffer, soundFormat.Value, soundData, soundData.Length, sampleRate);
            }
        }

        /// <summary>
        /// (Private) Loads sound data from a PCM Wave file, using the file header to interpret the raw bytes.
        /// </summary>
        /// <param name="stream">Stream containing the raw bytes from a PCM Wave file</param>
        /// <param name="channels">Number of channels</param>
        /// <param name="bits">Number of bits per sample</param>
        /// <param name="rate">Sample rate</param>
        /// <returns>Sound data as a byte array, or null if data was invalid.</returns>
        private byte[] LoadWave(Stream stream, out int channels, out int bits, out int rate)
        {
            channels = 1; bits = 16; rate = 11025;

            if (stream == null) return null;

            using (BinaryReader reader = new BinaryReader(stream))
            {
                string signature = new string(reader.ReadChars(4));
                if (signature != "RIFF") return null;

                int riff_chunck_size = reader.ReadInt32();

                string format = new string(reader.ReadChars(4));
                if (format != "WAVE") return null;

                string format_signature = new string(reader.ReadChars(4));
                if (format_signature != "fmt ") return null;

                int format_chunk_size = reader.ReadInt32();
                int audio_format = reader.ReadInt16();
                int num_channels = reader.ReadInt16();
                int sample_rate = reader.ReadInt32();
                int byte_rate = reader.ReadInt32();
                int block_align = reader.ReadInt16();
                int bits_per_sample = reader.ReadInt16();

                string data_signature = new string(reader.ReadChars(4));
                if (data_signature != "data") return null;

                int data_chunk_size = reader.ReadInt32();

                channels = num_channels;
                bits = bits_per_sample;
                rate = sample_rate;

                return reader.ReadBytes((int)reader.BaseStream.Length);
            }
        }

        /// <summary>
        /// (Private, static) Converts a channels/bits per sample combination into a value from the ALFormat enum.
        /// </summary>
        /// <param name="channels">Number of channels</param>
        /// <param name="bits">Number of bits per sample</param>
        /// <returns>An ALFormat value, or null if the channels/bits combination isn't valid</returns>
        private static ALFormat? GetSoundFormat(int channels, int bits)
        {
            switch (channels)
            {
                case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                default: return null;
            }
        }

        /// <summary>
        /// IDispose implementation. Frees the OpenAL buffer handle.
        /// </summary>
        public void Dispose()
        {
            AL.DeleteBuffer(Buffer);
        }
    }
}

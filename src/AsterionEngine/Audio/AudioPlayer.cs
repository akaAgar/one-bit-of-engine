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

using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.IO;

namespace Asterion.Audio
{
    public sealed class AudioPlayer
    {
        /// <summary>
        /// OpenAL context used by this audio player.
        /// </summary>
        private readonly AudioContext Context = null;
        private readonly Dictionary<string, AudioPlayerSource> SoundCache = new Dictionary<string, AudioPlayerSource>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Constructor.
        /// </summary>
        internal AudioPlayer()
        {
            Context = new AudioContext();
        }

        public bool PlaySound(string internalName, string waveFilePath, float volume = 1.0f, float pitch = 1.0f)
        {
            if (string.IsNullOrEmpty(waveFilePath) || !File.Exists(waveFilePath)) return false;
            return PlaySound(internalName, File.ReadAllBytes(waveFilePath), volume, pitch);
        }

        public bool PlaySound(string internalName, byte[] waveFileBytes, float volume = 1.0f, float pitch = 1.0f)
        {
            if (!SoundCache.ContainsKey(internalName))
            {
                AudioPlayerSource source = new AudioPlayerSource(waveFileBytes);
                if (!source.IsValid) return false;
                SoundCache.Add(internalName, source);
            }

            AudioPlayerSource sound = SoundCache[internalName];

            AL.Source(sound.Source, ALSourcef.Gain, Math.Max(0.0f, Math.Min(10.0f, volume)));
            AL.Source(sound.Source, ALSourcef.Pitch, Math.Max(0.01f, Math.Min(10.0f, pitch)));
            AL.Source(sound.Source, ALSourcei.Buffer, sound.Buffer);
            AL.SourcePlay(sound.Source);

            return true;
        }

        public void ClearCache()
        {
            foreach (string k in SoundCache.Keys)
                SoundCache[k].Dispose();

            SoundCache.Clear();
        }

        internal void Dispose()
        {
            ClearCache();
            Context.Dispose();
        }
    }
}

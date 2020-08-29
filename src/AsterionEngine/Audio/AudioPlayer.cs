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

using Asterion.IO;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;

namespace Asterion.Audio
{
    /// <summary>
    /// An audio player able to load and play PCM Wave files loaded from the engine FileSystem class.
    /// </summary>
    public sealed class AudioPlayer
    {
        /// <summary>
        /// Number of loop sound channels.
        /// </summary>
        public const int LOOP_CHANNELS = 4;

        /// <summary>
        /// (Private) OpenAL context used by this audio player.
        /// </summary>
        private AudioContext Context = null;
        
        /// <summary>
        /// (Private) The Asterion.FileSystem to use.
        /// </summary>
        private readonly FileSystem Files;

        /// <summary>
        /// (Private) Dictionary storing data for the cached sound files.
        /// </summary>
        private readonly Dictionary<string, AudioPlayerSource> SoundCache = new Dictionary<string, AudioPlayerSource>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// (Private) Channels for the looped sounds.
        /// </summary>
        private AudioPlayerSource[] LoopedSoundChannels = new AudioPlayerSource[LOOP_CHANNELS];

        /// <summary>
        /// Is this AudioPlayer enabled? If not, you can enable it by calling the Enable() method.
        /// </summary>
        public bool Enabled { get; private set; } = false;

        /// <summary>
        /// (Internal) Constructor.
        /// </summary>
        /// <param name="fileSystem">The FileSystem from which to load the sound files</param>
        internal AudioPlayer(FileSystem fileSystem)
        {
            Files = fileSystem;

            for (int i = 0; i < 0; i++)
                LoopedSoundChannels[i] = null;
        }

        /// <summary>
        /// Enables this AudioPlayer. Must be called before any sound can be played.
        /// Make sure a copy of OpenAL32.dll is located in your program's directory or this will not work.
        /// </summary>
        public void Enable()
        {
            if (Enabled) return;
            Context = new AudioContext();
            Enabled = true;
        }

        /// <summary>
        /// Loads a sound from the engine's FileSystem and plays it.
        /// If the sound wasn't precached, it will be automatically before it is played.
        /// Make sure the Enabled() method was called or no sound will be played.
        /// </summary>
        /// <param name="file">The name of the file, as found in the FileSystem file source</param>
        /// <param name="volume">Volume at which the sound should be played, 1.0 means normal, 0.5 means half volume, 2.0 means twice as loud</param>
        /// <param name="pitch">Pitch at which the sound should be played. 1.0 means normal, 0.5 means 2x slower, 2.0 means 2x faster.</param>
        /// <param name="randomPitch">Random pitch variation to add or subtract to the "base" pitch. (e.g. with pitch=1.0 and randomPitch=0.1, actual pitch will be between 0.9 and 1.1)</param>
        /// <returns>True if everything went correctly, false otherwise</returns>
        public bool PlaySound(string file, float volume = 1.0f, float pitch = 1.0f, float randomPitch = 0.0f)
        {
            // No need to check Enabled, it is checked in PrecacheSound
            if (!PrecacheSound(file)) return false;

            AudioPlayerSource sound = SoundCache[file];

            float realPitch = pitch + AsterionTools.RandomFloat(-randomPitch, randomPitch);

            AL.Source(sound.Source, ALSourcef.Gain, AsterionTools.Clamp(0f, 10f, volume));
            AL.Source(sound.Source, ALSourcef.Pitch, AsterionTools.Clamp(0.01f, 10f, realPitch));
            AL.Source(sound.Source, ALSourcei.Buffer, sound.Buffer);
            AL.Source(sound.Source, ALSourceb.Looping, false);
            AL.SourcePlay(sound.Source);

            return true;
        }

        public bool PlayLoopedSound(int channel, string file, float volume = 1.0f, float pitch = 1.0f)
        {
            // No need to check Enabled, it is checked in PrecacheSound
            if (!PrecacheSound(file)) return false;
            if ((channel < 0) || (channel >= LOOP_CHANNELS)) return false;

            StopLoopedSound(channel);

            Loop = SoundCache[file];

            AL.Source(sound.Source, ALSourcef.Gain, AsterionTools.Clamp(0f, 10f, volume));
            AL.Source(sound.Source, ALSourcef.Pitch, AsterionTools.Clamp(0.01f, 10f, pitch));
            AL.Source(sound.Source, ALSourcei.Buffer, sound.Buffer);
            AL.Source(sound.Source, ALSourceb.Looping, true);
            AL.SourcePlay(sound.Source);

            return true;
        }

        public bool StopLoopedSound(int channel)
        {
            if (!Enabled) return false;
            if ((channel < 0) || (channel >= LOOP_CHANNELS)) return false;
            if (LoopedSoundChannels[channel] == null) return true; // Channel was already stopped

            LoopedSoundChannels[channel].Dispose();
            LoopedSoundChannels[channel] = null;

            return true;
        }

        /// <summary>
        /// Loads a sound from the engine's FileSystem and stores it in the cache.
        /// </summary>
        /// <param name="file">The name of the file, as found in the FileSystem file source</param>
        /// <returns>True if the sound was precached properly, false otherwise</returns>
        public bool PrecacheSound(string file)
        {
            if (!Enabled) return false;
            if (string.IsNullOrEmpty(file)) return false;
            if (SoundCache.ContainsKey(file)) return true; // File was already precached
            
            byte[] waveFileBytes = Files.GetFile(file);
            if (waveFileBytes == null) return false;
            AudioPlayerSource source = new AudioPlayerSource(waveFileBytes);
            if (!source.IsValid) return false;
            SoundCache.Add(file, source);

            return true;
        }

        /// <summary>
        /// Stop all sounds and clears the cache of all precached sounds.
        /// </summary>
        public void ClearCache()
        {
            if (!Enabled) return;

            for (int i = 0; i < LOOP_CHANNELS; i++)
                StopLoopedSound(i);

            foreach (string k in SoundCache.Keys)
                SoundCache[k].Dispose();

            SoundCache.Clear();
        }

        /// <summary>
        /// (Internal) Stop all sounds, clears the sound cache and destroys the OpenAL context.
        /// </summary>
        internal void Destroy()
        {
            if (!Enabled) return; // This AudioPlayer was not enabled, nothing to clear or destroy

            ClearCache();
            Context.Dispose();
        }
    }
}

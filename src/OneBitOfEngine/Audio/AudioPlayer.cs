/*
==========================================================================
This file is part of One Bit of Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
One Bit of Engine is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
One Bit of Engine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with One Bit of Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using OneBitOfEngine.IO;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;

namespace OneBitOfEngine.Audio
{
    /// <summary>
    /// An audio player able to load and play PCM Wave files loaded from the engine FileSystem class.
    /// </summary>
    public sealed class AudioPlayer
    {
        /// <summary>
        /// Number of channels for "one shot" sounds.
        /// </summary>
        public const int SINGLE_CHANNELS = 8;

        /// <summary>
        /// Number of channels for looping sounds.
        /// </summary>
        public const int LOOP_CHANNELS = 2;

        /// <summary>
        /// Total number of sound channels.
        /// </summary>
        public const int TOTAL_CHANNELS = SINGLE_CHANNELS + LOOP_CHANNELS;

        /// <summary>
        /// (Private) OpenAL context used by this audio player.
        /// </summary>
        private AudioContext Context = null;
        
        /// <summary>
        /// (Private) The OneBitOfEngine.FileSystem to use.
        /// </summary>
        private readonly FileSystem Files;

        /// <summary>
        /// (Private) Dictionary storing data for the cached sound files.
        /// </summary>
        private readonly Dictionary<string, AudioPlayerSound> SoundCache = new Dictionary<string, AudioPlayerSound>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// (Private) Sounds channels.
        /// The first SOUND_CHANNELS channels are for "one shot" sounds, the last LOOP_CHANNELS are reserved for looping sounds.
        /// </summary>
        private readonly int[] SoundChannels = new int[TOTAL_CHANNELS];

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

            for (int i = 0; i < TOTAL_CHANNELS; i++)
                SoundChannels[i] = -1;
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
            if (!Enabled) return false;

            int freeChannel = -1;
            for (int i = 0; i < SINGLE_CHANNELS; i++) // Look for an unused channel or a channel with a sound which is done playing
            {
                if (
                    (SoundChannels[i] == -1) ||
                    (AL.GetSourceState(SoundChannels[i]) != ALSourceState.Playing)
                   )
                {
                    freeChannel = i;
                    break;
                }
            }
            if (freeChannel == -1) return false; // No available channel

            float realPitch = pitch + OneBitOfTools.RandomFloat(-randomPitch, randomPitch);
            PlaySoundInChannel(freeChannel, file, volume, realPitch, false);
            return true;
        }

        /// <summary>
        /// Stops all currently playing sounds.
        /// </summary>
        /// <param name="stopOneShotSounds">Should "one-shot" sounds be stopped?</param>
        /// <param name="stopLoopingSounds">Should looping sounds be stopped?</param>
        public void StopAllSounds(bool stopOneShotSounds = true, bool stopLoopingSounds = true)
        {
            for (int i = 0; i < TOTAL_CHANNELS; i++)
            {
                if (!stopOneShotSounds && (i < SINGLE_CHANNELS)) continue;
                if (!stopLoopingSounds && (i >= SINGLE_CHANNELS)) continue;

                StopSound(i);
            }
        }

        /// <summary>
        /// Loads a sound from the engine's FileSystem and plays it in an endless loop.
        /// If the sound wasn't precached, it will be automatically before it is played.
        /// Make sure the Enabled() method was called or no sound will be played.
        /// </summary>
        /// <param name="file">The sound file, as it appears in the file sourced used by FileSystem</param>
        /// <param name="volume">Volume at which the sound should be played, 1.0 means normal, 0.5 means half volume, 2.0 means twice as loud</param>
        /// <param name="pitch">Pitch at which the sound should be played. 1.0 means normal, 0.5 means 2x slower, 2.0 means 2x faster.</param>
        /// <param name="channel">A looping sound channel between 0 and <see cref="LOOP_CHANNELS"/></param>
        /// <returns></returns>
        public bool PlayLoopingSound(string file, float volume = 1.0f, float pitch = 1.0f, int channel = 0)
        {
            if (!Enabled) return false;
            if ((channel < 0) || (channel >= LOOP_CHANNELS)) return false; // Channel out of bounds

            PlaySoundInChannel(channel, file, volume, pitch, true);
            return true;
        }

        /// <summary>
        /// Stops playing a looping sound.
        /// </summary>
        /// <param name="channel">A looping sound channel between 0 and <see cref="LOOP_CHANNELS"/></param>
        public void StopLoopingSound(int channel = 0)
        {
            StopSound(SINGLE_CHANNELS + channel);
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
            AudioPlayerSound source = new AudioPlayerSound(waveFileBytes);
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

            StopAllSounds();

            foreach (string k in SoundCache.Keys)
                SoundCache[k].Dispose();

            SoundCache.Clear();
        }

        /// <summary>
        /// (Internal) Stops all sounds, clears the sound cache and destroys the OpenAL context.
        /// </summary>
        internal void Destroy()
        {
            if (!Enabled) return; // This AudioPlayer was not enabled, nothing to clear or destroy

            ClearCache();
            Context.Dispose();
        }

        /// <summary>
        /// (Private) Private method handling low-level OpenAL calls required to play a sound, used by PlaySound() and PlayLoopingSound().
        /// </summary>
        /// <param name="channel">The sound channel in which to play the sound</param>
        /// <param name="file">The sound file, as it appears in the file sourced used by FileSystem</param>
        /// <param name="volume">Volume at which the sound should be played, 1.0 means normal, 0.5 means half volume, 2.0 means twice as loud</param>
        /// <param name="pitch">Pitch at which the sound should be played. 1.0 means normal, 0.5 means 2x slower, 2.0 means 2x faster.</param>
        /// <param name="looping">Should the sound be looping?</param>
        /// <returns>True if everything went well, false otherwise</returns>
        private bool PlaySoundInChannel(int channel, string file, float volume, float pitch, bool looping)
        {
            if (!Enabled) return false;
            if ((channel < 0) || (channel >= TOTAL_CHANNELS)) return false; // Channel index is out of bounds
            if (!PrecacheSound(file)) return false; // Failed to precache the sound (wrong data or file do not exist)

            AudioPlayerSound sound = SoundCache[file];

            StopSound(channel);
            SoundChannels[channel] = AL.GenSource();
            AL.Source(SoundChannels[channel], ALSourcef.Gain, OneBitOfTools.Clamp(volume, 0f, 10f));
            AL.Source(SoundChannels[channel], ALSourcef.Pitch, OneBitOfTools.Clamp(pitch, 0.01f, 10f));
            AL.Source(SoundChannels[channel], ALSourcei.Buffer, sound.Buffer);
            AL.Source(SoundChannels[channel], ALSourceb.Looping, looping);
            AL.SourcePlay(SoundChannels[channel]);

            return true;
        }

        /// <summary>
        /// (Private) Private method handling low-level OpenAL calls required to stop a sound.
        /// </summary>
        /// <param name="channel">The sound channel to stop</param>
        private void StopSound(int channel)
        {
            if (!Enabled) return;
            if ((channel < 0) || (channel >= TOTAL_CHANNELS)) return; // Channel index is out of bounds
            if (SoundChannels[channel] == -1) return; // Channel is not in use

            AL.SourceStop(SoundChannels[channel]);
            AL.DeleteSource(SoundChannels[channel]);
            SoundChannels[channel] = -1;
        }
    }
}

using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace Madu
{
    public class Sounds : IDisposable
    {
        private IWavePlayer musicOut;
        private AudioFileReader musicReader;
        private VolumeSampleProvider musicVolumeProvider;

        private IWavePlayer eatOut;
        private AudioFileReader eatReader;
        private VolumeSampleProvider eatVolumeProvider;

        private IWavePlayer dieOut;
        private AudioFileReader dieReader;
        private VolumeSampleProvider dieVolumeProvider;

        private float volume = 1.0f;

        public Sounds(int volumePercent)
        {
            volume = volumePercent / 100f;

            musicReader = new AudioFileReader("resources/music.wav");
            musicVolumeProvider = new VolumeSampleProvider(musicReader.ToSampleProvider()) { Volume = volume };
            musicOut = new WaveOutEvent();
            musicOut.Init(musicVolumeProvider);

            musicOut.PlaybackStopped += (s, e) =>
            {
                musicReader.Position = 0;
                musicOut.Play();
            };

            eatReader = new AudioFileReader("resources/eat.wav");
            eatVolumeProvider = new VolumeSampleProvider(eatReader.ToSampleProvider()) { Volume = volume };
            eatOut = new WaveOutEvent();
            eatOut.Init(eatVolumeProvider);

            dieReader = new AudioFileReader("resources/die.wav");
            dieVolumeProvider = new VolumeSampleProvider(dieReader.ToSampleProvider()) { Volume = volume };
            dieOut = new WaveOutEvent();
            dieOut.Init(dieVolumeProvider);
        }

        public void SetVolume(float newVolume)
        {
            volume = newVolume;

            if (musicVolumeProvider != null)
                musicVolumeProvider.Volume = volume;
            if (eatVolumeProvider != null)
                eatVolumeProvider.Volume = volume;
            if (dieVolumeProvider != null)
                dieVolumeProvider.Volume = volume;
        }

        public void PlayMusicLoop()
        {
            musicReader.Position = 0;
            musicOut.Play();
        }

        public void PlayEat()
        {
            eatReader.Position = 0;
            eatOut.Play();
        }

        public void PlayDie()
        {
            dieReader.Position = 0;
            dieOut.Play();
        }

        public void StopMusic()
        {
            musicOut.Stop();
        }

        public void Dispose()
        {
            musicOut?.Dispose();
            musicReader?.Dispose();

            eatOut?.Dispose();
            eatReader?.Dispose();

            dieOut?.Dispose();
            dieReader?.Dispose();
        }
    }
}

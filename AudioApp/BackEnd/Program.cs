using NAudio.Wave;
using NAudio.Wave.SampleProviders;

var wg = new SignalGenerator
{
    Type = SignalGeneratorType.Sin,
    Frequency = 500,
    Gain = 0.1
};

var adsr = new AdsrSampleProvider(wg.ToMono())
{
    AttackSeconds = 0.3f,
    ReleaseSeconds = 5.0f
};

using (var driverOut = new WasapiOut())
{
    var mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2))
    {
        ReadFully = true
    };

    mixer.AddMixerInput(adsr.ToStereo());

    driverOut.Init(mixer);
    driverOut.Play();

    Thread.Sleep(5000);

    adsr.Stop();

    while (driverOut.PlaybackState == PlaybackState.Playing) { }
}
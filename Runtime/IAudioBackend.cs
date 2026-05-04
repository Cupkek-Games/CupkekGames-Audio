namespace CupkekGames.Audio
{
    /// <summary>
    /// Single kind-driven entry point for audio playback.
    /// Channels are designer-defined string keys (see <see cref="AudioChannels"/> for reserved ones);
    /// new channels are added by registering routing in the adapter and using a new constant,
    /// not by editing this interface.
    /// </summary>
    public interface IAudioBackend
    {
        /// <summary>Play <paramref name="cue"/> on <paramref name="channel"/>. Pass an <see cref="IAudioPlaybackContext"/> for per-call extras (positional source, fade options, etc.). Unknown channels are silently ignored.</summary>
        void Play(string channel, IAudioCue cue, IAudioPlaybackContext context = null);

        /// <summary>Stop <paramref name="cue"/> on <paramref name="channel"/>. Pass an <see cref="IAudioPlaybackContext"/> for per-call extras (e.g. <see cref="AudioPlaybackContext.AllowFadeOut"/>).</summary>
        void Stop(string channel, IAudioCue cue, IAudioPlaybackContext context = null);

        /// <summary>Stop all cues currently playing on <paramref name="channel"/>.</summary>
        void StopAll(string channel, IAudioPlaybackContext context = null);
    }
}

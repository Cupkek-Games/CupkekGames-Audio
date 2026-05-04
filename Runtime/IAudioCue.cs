namespace CupkekGames.Audio
{
    /// <summary>
    /// Marker interface for audio cue assets. Concrete implementations
    /// (e.g. <c>SonityAudioCueSO</c>) carry backend-specific asset references.
    /// Cues are played by passing them to <see cref="IAudioBackend.Play"/> on a chosen channel.
    /// </summary>
    public interface IAudioCue { }
}

namespace CupkekGames.Audio
{
    public interface IAudioBackend
    {
        void PlayMusic(IAudioCue cue, bool stopAllOtherMusic = true, bool allowFadeOut = true);
        void StopAllMusic(bool allowFadeOut = true);
        void PlayUI(IAudioCue cue);
        void StopUI(IAudioCue cue, bool allowFadeOut = true);
    }
}

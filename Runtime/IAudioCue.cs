using UnityEngine;

namespace CupkekGames.Audio
{
    public interface IAudioCue
    {
        void Play(Transform transform);
        void Stop(Transform transform);
        void PlayUI();
        void StopUI();
    }
}

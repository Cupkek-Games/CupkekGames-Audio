using CupkekGames.TimeSystem;
using UnityEngine;

namespace CupkekGames.Audio
{
    public interface ISFXPlayer
    {
        void Play(Transform transform);
        void RegisterTimeScale(TimeBundle timeBundle, Transform owner);
    }
}

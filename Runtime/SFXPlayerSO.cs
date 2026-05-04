using CupkekGames.TimeSystem;
using UnityEngine;

namespace CupkekGames.Audio
{
    public abstract class SFXPlayerSO : ScriptableObject, ISFXPlayer
    {
        public abstract void Play(Transform transform);
        public abstract void RegisterTimeScale(TimeBundle timeBundle, Transform owner);
    }
}

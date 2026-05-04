using UnityEngine;

namespace CupkekGames.Audio
{
    /// <summary>
    /// Standard per-call playback context. All fields are optional —
    /// adapters use whichever ones apply to the requested channel.
    /// </summary>
    public class AudioPlaybackContext : IAudioPlaybackContext
    {
        /// <summary>Source transform for positional 3D play. Null for 2D channels (UI, Music, Ambient).</summary>
        public Transform Source;

        /// <summary>For music/ambient: whether the previous cue on this channel should fade out (vs hard cut).</summary>
        public bool AllowFadeOut = true;

        /// <summary>For music: stop all currently-playing music on this channel before starting the new cue.</summary>
        public bool StopAllOthers = true;
    }
}

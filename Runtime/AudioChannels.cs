namespace CupkekGames.Audio
{
    /// <summary>
    /// Reserved audio channel string constants.
    /// Call sites should use these constants instead of hand-written magic strings,
    /// but the catalog stays open — domain games add new channels (Voice, Cinematic, etc.)
    /// without editing this class.
    /// </summary>
    public static class AudioChannels
    {
        /// <summary>Background music. State-tracked: switching to a new cue stops the previous one.</summary>
        public const string Music = "Music";

        /// <summary>Ambient soundscape. State-tracked like Music but separate stream.</summary>
        public const string Ambient = "Ambient";

        /// <summary>UI button clicks, notifications, menu sounds. Non-positional 2D.</summary>
        public const string UI = "UI";

        /// <summary>3D positional gameplay sound effects. Use <see cref="AudioPlaybackContext.Source"/> to set position.</summary>
        public const string SFX = "SFX";

        /// <summary>2D non-positional sound effects (gameplay, not UI).</summary>
        public const string SFX2D = "SFX2D";
    }
}

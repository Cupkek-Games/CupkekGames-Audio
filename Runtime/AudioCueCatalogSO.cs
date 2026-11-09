using System;
using System.Collections.Generic;
using CupkekGames.Services;
using UnityEngine;

namespace CupkekGames.Audio
{
  /// <summary>
  /// A table of audio cues by kind, played on one channel. Add it to a ServiceRegistrySO; it
  /// registers as <see cref="AudioCueCatalogSO"/> under <see cref="Key"/>, so callers resolve it
  /// with <c>ServiceLocator.Get&lt;AudioCueCatalogSO&gt;(key)</c>. Kinds are plain strings the game
  /// names; <see cref="Has"/> tells whether one is authored, <see cref="Get"/> and
  /// <see cref="Play"/> fail loud on one that is not.
  /// Plays through the registered <see cref="IAudioBackend"/>.
  /// </summary>
  [CreateAssetMenu(fileName = "AudioCueCatalog", menuName = "CupkekGames/Audio/Cue Catalog")]
  public class AudioCueCatalogSO : ServiceProviderSO
  {
    [Serializable]
    public class Entry
    {
      public string Kind;

      [Tooltip("An asset that implements IAudioCue.")]
      public ScriptableObject CueAsset;
    }

    [Tooltip("The key this catalog registers under. One catalog per key.")]
    [SerializeField] private string _key = AudioChannels.UI;

    [Tooltip("The channel every cue in this catalog plays on (see AudioChannels).")]
    [SerializeField] private string _channel = AudioChannels.UI;

    [SerializeField] private List<Entry> _entries = new List<Entry>();

    [NonSerialized] private Dictionary<string, IAudioCue> _map;
    [NonSerialized] private IAudioBackend _backend;

    public string Key => _key;
    public string Channel => _channel;

    public override void RegisterServices()
    {
      if (string.IsNullOrEmpty(_key))
        throw new InvalidOperationException($"[Audio] cue catalog '{name}' has no key.");
      if (string.IsNullOrEmpty(_channel))
        throw new InvalidOperationException($"[Audio] cue catalog '{name}' has no channel.");

      _map = new Dictionary<string, IAudioCue>(_entries.Count);
      foreach (Entry entry in _entries)
      {
        if (string.IsNullOrEmpty(entry.Kind) || entry.CueAsset is not IAudioCue cue)
          throw new InvalidOperationException(
            $"[Audio] cue catalog '{name}' has a malformed entry (kind '{entry.Kind}'): it needs a kind and an IAudioCue asset.");
        if (!_map.TryAdd(entry.Kind, cue))
          throw new InvalidOperationException($"[Audio] cue catalog '{name}' lists kind '{entry.Kind}' twice.");
      }

      // The backend may register after this catalog: it is resolved on the first play.
      _backend = null;
      ServiceLocator.Register(this, typeof(AudioCueCatalogSO), _key);
    }

    public override void UnregisterServices()
    {
      ServiceLocator.RemoveInstance(this, typeof(AudioCueCatalogSO));
      _map = null;
      _backend = null;
    }

    /// <summary>True when a cue is authored for <paramref name="kind"/>.</summary>
    public bool Has(string kind) => !string.IsNullOrEmpty(kind) && Map.ContainsKey(kind);

    /// <summary>The cue authored for <paramref name="kind"/>; throws when there is none.</summary>
    public IAudioCue Get(string kind)
    {
      if (!string.IsNullOrEmpty(kind) && Map.TryGetValue(kind, out IAudioCue cue)) return cue;
      throw new KeyNotFoundException($"[Audio] cue catalog '{name}' has no cue for kind '{kind}'.");
    }

    /// <summary>Plays the cue for <paramref name="kind"/> on this catalog's channel; throws when there is none.</summary>
    public void Play(string kind, IAudioPlaybackContext context = null)
    {
      IAudioCue cue = Get(kind);
      Backend.Play(_channel, cue, context);
    }

    /// <summary>Stops the cue for <paramref name="kind"/> on this catalog's channel; throws when there is none.</summary>
    public void Stop(string kind, IAudioPlaybackContext context = null)
    {
      IAudioCue cue = Get(kind);
      Backend.Stop(_channel, cue, context);
    }

    private IAudioBackend Backend => _backend ??= ServiceLocator.Get<IAudioBackend>();

    private Dictionary<string, IAudioCue> Map => _map ?? throw new InvalidOperationException(
      $"[Audio] cue catalog '{name}' is not registered: add it to a ServiceRegistrySO.");
  }
}

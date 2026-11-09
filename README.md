# CupkekGames Audio

Audio backend + SFX cue interfaces for CupkekGames games.

Extracted from HeroManager (Phase A, 2026-04-30).

## Cue catalog

`AudioCueCatalogSO` is a table of cues by kind (a string the game names) that all play on one channel. Add it to a `ServiceRegistrySO`'s providers; it registers under its key:

```csharp
AudioCueCatalogSO ui = ServiceLocator.Get<AudioCueCatalogSO>(AudioChannels.UI);
ui.Play("ui.open");                  // throws when the kind is not authored
if (ui.Has("mood.rise")) ui.Play("mood.rise"); // a kind that may stay silent
```

A malformed entry or a kind listed twice throws when the catalog registers.

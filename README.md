# bannerlord-1.4.6

Decompiled C# source of **Mount & Blade II: Bannerlord** (Steam, version 1.4.6),
generated with [ilspycmd](https://github.com/icsharpcode/ILSpy) (`-p` project mode).
For reference / modding study only. All code is property of TaleWorlds Entertainment.

## Structure

One folder per assembly at the repository root (90 assemblies), each with its own
`.csproj`; a single solution `ManagedStarter.sln` ties them together.

| Group | Contents |
|-------|----------|
| `TaleWorlds.*` | Core engine + game assemblies (`TaleWorlds.Core`, `TaleWorlds.Engine`, `TaleWorlds.MountAndBlade.*`, `TaleWorlds.CampaignSystem.*`, …) |
| `SandBox*`, `StoryMode*` | SandBox / StoryMode modules and their GauntletUI + View + ViewModelCollection layers |
| `ManagedStarter` | Managed launcher entry point |
| `Newtonsoft.Json`, `StbSharp`, `Steamworks.NET`, `jose-jwt`, `GalaxyCSharp` | Third-party libraries shipped with the game |
| `mscorlib`, `netstandard`, `System.*` | Framework reference assemblies included by the decompiler |

**Total:** 90 assemblies, 11385 `.cs` files.

## Notes

- Only the game's own managed (.NET) assemblies are included — the framework/runtime
  DLLs shipped under `mono/`, `Microsoft.NETCore.App/`, `Microsoft.WindowsDesktop.App/`,
  `DigitalCompanion/` (Unity), and `CrashUploader.Publish/` are excluded (public framework source).

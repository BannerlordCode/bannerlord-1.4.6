using System;
using System.Collections.Generic;
using System.Threading;
using psai.net;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D5 RID: 469
	public class MBMusicManager
	{
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x000608A8 File Offset: 0x0005EAA8
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x000608AF File Offset: 0x0005EAAF
		public static MBMusicManager Current { get; private set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x000608B7 File Offset: 0x0005EAB7
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x000608BF File Offset: 0x0005EABF
		public MusicMode CurrentMode { get; private set; }

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000608C8 File Offset: 0x0005EAC8
		private MBMusicManager()
		{
			if (!NativeConfig.DisableSound)
			{
				List<string> list = new List<string>();
				foreach (MbObjectXmlInformation mbObjectXmlInformation in XmlResource.MbprojXmls)
				{
					if (mbObjectXmlInformation.Id == "soln_soundtrack")
					{
						string moduleName = mbObjectXmlInformation.ModuleName;
						list.Add(moduleName);
					}
				}
				PsaiCore.Instance.LoadSoundtrackFromProjectFile(list);
			}
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0006095C File Offset: 0x0005EB5C
		public static bool IsCreationCompleted()
		{
			return MBMusicManager._creationCompleted;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00060963 File Offset: 0x0005EB63
		private static void ProcessCreation(object callback)
		{
			MBMusicManager.Current = new MBMusicManager();
			MusicParameters.LoadFromXml();
			MBMusicManager._creationCompleted = true;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0006097A File Offset: 0x0005EB7A
		public static void Create()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(MBMusicManager.ProcessCreation));
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00060990 File Offset: 0x0005EB90
		public static void Initialize()
		{
			if (!MBMusicManager._initialized)
			{
				MBMusicManager.Current._battleMode = new MBMusicManager.BattleMusicMode();
				MBMusicManager.Current._campaignMode = new MBMusicManager.CampaignMusicMode();
				MBMusicManager.Current.CurrentMode = MusicMode.Paused;
				MBMusicManager.Current._menuModeActivationTimer = 0.5f;
				MBMusicManager._initialized = true;
				Debug.Print("MusicManager Initialize completed.", 0, Debug.DebugColor.Green, 281474976710656UL);
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000609F7 File Offset: 0x0005EBF7
		public void OnCampaignMusicHandlerInit(IMusicHandler campaignMusicHandler)
		{
			this._campaignMusicHandler = campaignMusicHandler;
			this._activeMusicHandler = this._campaignMusicHandler;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00060A0C File Offset: 0x0005EC0C
		public void OnCampaignMusicHandlerFinalize()
		{
			this._campaignMusicHandler = null;
			this.CheckActiveHandler();
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00060A1B File Offset: 0x0005EC1B
		public void OnBattleMusicHandlerInit(IMusicHandler battleMusicHandler)
		{
			this._battleMusicHandler = battleMusicHandler;
			this._activeMusicHandler = this._battleMusicHandler;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x00060A30 File Offset: 0x0005EC30
		public void OnBattleMusicHandlerFinalize()
		{
			this._battleMusicHandler = null;
			this.CheckActiveHandler();
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00060A3F File Offset: 0x0005EC3F
		public void OnSilencedMusicHandlerInit(IMusicHandler silencedMusicHandler)
		{
			this._silencedMusicHandler = silencedMusicHandler;
			this._activeMusicHandler = this._silencedMusicHandler;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00060A54 File Offset: 0x0005EC54
		public void OnSilencedMusicHandlerFinalize()
		{
			this._silencedMusicHandler = null;
			this.CheckActiveHandler();
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00060A63 File Offset: 0x0005EC63
		private void CheckActiveHandler()
		{
			IMusicHandler musicHandler;
			if ((musicHandler = this._battleMusicHandler) == null)
			{
				musicHandler = this._silencedMusicHandler ?? this._campaignMusicHandler;
			}
			this._activeMusicHandler = musicHandler;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00060A88 File Offset: 0x0005EC88
		private void ActivateMenuMode()
		{
			if (!this._systemPaused)
			{
				this.CurrentMode = MusicMode.Menu;
				MusicTheme musicTheme = (ModuleHelper.IsModuleActive("NavalDLC") ? MusicTheme.NavalMainTheme : MusicTheme.MainTheme);
				PsaiCore.Instance.MenuModeEnter((int)musicTheme, 0.5f);
			}
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00060ACA File Offset: 0x0005ECCA
		private void DeactivateMenuMode()
		{
			PsaiCore.Instance.MenuModeLeave();
			this.CurrentMode = MusicMode.Paused;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00060ADE File Offset: 0x0005ECDE
		public void ActivateBattleMode()
		{
			if (!this._systemPaused)
			{
				this.CurrentMode = MusicMode.Battle;
			}
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00060AEF File Offset: 0x0005ECEF
		public void DeactivateBattleMode()
		{
			PsaiCore.Instance.StopMusic(true, 3f);
			this.CurrentMode = MusicMode.Paused;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00060B09 File Offset: 0x0005ED09
		public void ActivateCampaignMode()
		{
			if (!this._systemPaused)
			{
				this.CurrentMode = MusicMode.Campaign;
			}
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00060B1A File Offset: 0x0005ED1A
		public void DeactivateCampaignMode()
		{
			PsaiCore.Instance.StopMusic(true, 3f);
			this.CurrentMode = MusicMode.Paused;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00060B34 File Offset: 0x0005ED34
		public void DeactivateCurrentMode()
		{
			switch (this.CurrentMode)
			{
			case MusicMode.Menu:
				break;
			case MusicMode.Campaign:
				this.DeactivateCampaignMode();
				return;
			case MusicMode.Battle:
				this.DeactivateBattleMode();
				break;
			default:
				return;
			}
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00060B6A File Offset: 0x0005ED6A
		private bool CheckMenuModeActivationTimer()
		{
			return this._menuModeActivationTimer <= 0f;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00060B7C File Offset: 0x0005ED7C
		public void UnpauseMusicManagerSystem()
		{
			if (this._systemPaused)
			{
				this._systemPaused = false;
				this._menuModeActivationTimer = 1f;
			}
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00060B98 File Offset: 0x0005ED98
		public void PauseMusicManagerSystem()
		{
			if (!this._systemPaused)
			{
				if (this.CurrentMode == MusicMode.Menu)
				{
					this.DeactivateMenuMode();
				}
				this._systemPaused = true;
			}
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00060BB8 File Offset: 0x0005EDB8
		public void StartTheme(MusicTheme theme, float startIntensity, bool queueEndSegment = false)
		{
			PsaiCore.Instance.TriggerMusicTheme((int)theme, startIntensity);
			if (queueEndSegment)
			{
				PsaiCore.Instance.StopMusic(false, 3f);
			}
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00060BDB File Offset: 0x0005EDDB
		public void StartThemeWithConstantIntensity(MusicTheme theme, bool queueEndSegment = false)
		{
			PsaiCore.Instance.HoldCurrentIntensity(true);
			this.StartTheme(theme, 0f, queueEndSegment);
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00060BF6 File Offset: 0x0005EDF6
		public void ForceStopThemeWithFadeOut()
		{
			PsaiCore.Instance.StopMusic(true, 3f);
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x00060C09 File Offset: 0x0005EE09
		public void ChangeCurrentThemeIntensity(float deltaIntensity)
		{
			PsaiCore.Instance.AddToCurrentIntensity(deltaIntensity);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00060C18 File Offset: 0x0005EE18
		public void Update(float dt)
		{
			if (Utilities.EngineFrameNo == this._latestFrameUpdatedNo)
			{
				return;
			}
			this._latestFrameUpdatedNo = Utilities.EngineFrameNo;
			if (this._menuModeActivationTimer > 0f)
			{
				this._menuModeActivationTimer -= dt;
			}
			if (!this._systemPaused)
			{
				if (GameStateManager.Current != null && GameStateManager.Current.ActiveState != null)
				{
					GameState activeState = GameStateManager.Current.ActiveState;
					MusicMode currentMode = this.CurrentMode;
					if (currentMode != MusicMode.Paused)
					{
						if (currentMode == MusicMode.Menu)
						{
							if (!activeState.IsMusicMenuState)
							{
								this.DeactivateMenuMode();
							}
						}
					}
					else if (activeState.IsMusicMenuState && this.CheckMenuModeActivationTimer())
					{
						this.ActivateMenuMode();
					}
				}
				if (this._activeMusicHandler != null)
				{
					this._activeMusicHandler.OnUpdated(dt);
				}
			}
			PsaiCore.Instance.Update();
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00060CD4 File Offset: 0x0005EED4
		public MusicTheme GetSiegeTheme(BasicCultureObject culture)
		{
			return this._battleMode.GetSiegeTheme(culture);
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x00060CE2 File Offset: 0x0005EEE2
		public MusicTheme GetBattleTheme(BasicCultureObject culture, int battleSize, out bool isPaganBattle)
		{
			return this._battleMode.GetBattleTheme(culture, battleSize, out isPaganBattle);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00060CF2 File Offset: 0x0005EEF2
		public MusicTheme GetBattleEndTheme(BasicCultureObject culture, bool isVictory)
		{
			return this._battleMode.GetBattleEndTheme(culture, isVictory);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00060D01 File Offset: 0x0005EF01
		public MusicTheme GetBattleTurnsOneSideTheme(BasicCultureObject culture, bool isPositive, bool isPaganBattle)
		{
			if (isPaganBattle)
			{
				if (!isPositive)
				{
					return MusicTheme.PaganTurnsNegative;
				}
				return MusicTheme.PaganTurnsPositive;
			}
			else
			{
				if (!isPositive)
				{
					return MusicTheme.BattleTurnsNegative;
				}
				return MusicTheme.BattleTurnsPositive;
			}
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00060D18 File Offset: 0x0005EF18
		public MusicTheme GetCampaignMusicTheme(BasicCultureObject culture, bool isDark, bool isWarMode, bool isAtSea)
		{
			MusicTheme musicTheme = MusicTheme.None;
			if (!isDark && isWarMode)
			{
				musicTheme = this._campaignMode.GetCampaignDramaticThemeWithCulture(culture);
			}
			if (isAtSea)
			{
				musicTheme = this._campaignMode.GetSeaCampignMusic(culture);
			}
			if (musicTheme != MusicTheme.None)
			{
				return musicTheme;
			}
			return this._campaignMode.GetCampaignTheme(culture, isDark);
		}

		// Token: 0x04000957 RID: 2391
		private const string CultureEmpire = "empire";

		// Token: 0x04000958 RID: 2392
		private const string CultureSturgia = "sturgia";

		// Token: 0x04000959 RID: 2393
		private const string CultureAserai = "aserai";

		// Token: 0x0400095A RID: 2394
		private const string CultureVlandia = "vlandia";

		// Token: 0x0400095B RID: 2395
		private const string CultureBattania = "battania";

		// Token: 0x0400095C RID: 2396
		private const string CultureKhuzait = "khuzait";

		// Token: 0x0400095D RID: 2397
		private const string CultureNord = "nord";

		// Token: 0x0400095E RID: 2398
		private const float DefaultFadeOutDurationInSeconds = 3f;

		// Token: 0x0400095F RID: 2399
		private const float MenuModeActivationTimerInSeconds = 0.5f;

		// Token: 0x04000962 RID: 2402
		private MBMusicManager.BattleMusicMode _battleMode;

		// Token: 0x04000963 RID: 2403
		private MBMusicManager.CampaignMusicMode _campaignMode;

		// Token: 0x04000964 RID: 2404
		private IMusicHandler _campaignMusicHandler;

		// Token: 0x04000965 RID: 2405
		private IMusicHandler _battleMusicHandler;

		// Token: 0x04000966 RID: 2406
		private IMusicHandler _silencedMusicHandler;

		// Token: 0x04000967 RID: 2407
		private IMusicHandler _activeMusicHandler;

		// Token: 0x04000968 RID: 2408
		private static bool _initialized;

		// Token: 0x04000969 RID: 2409
		private static bool _creationCompleted;

		// Token: 0x0400096A RID: 2410
		private float _menuModeActivationTimer;

		// Token: 0x0400096B RID: 2411
		private bool _systemPaused;

		// Token: 0x0400096C RID: 2412
		private int _latestFrameUpdatedNo = -1;

		// Token: 0x0200050C RID: 1292
		private class CampaignMusicMode
		{
			// Token: 0x06003BB6 RID: 15286 RVA: 0x000EEE6A File Offset: 0x000ED06A
			public CampaignMusicMode()
			{
				this._factionSpecificCampaignThemeSelectionFactor = 0.35f;
				this._factionSpecificCampaignDramaticThemeSelectionFactor = 0.35f;
			}

			// Token: 0x06003BB7 RID: 15287 RVA: 0x000EEE88 File Offset: 0x000ED088
			public MusicTheme GetCampaignTheme(BasicCultureObject culture, bool isDark)
			{
				if (isDark)
				{
					return MusicTheme.CampaignDark;
				}
				MusicTheme campaignThemeWithCulture = this.GetCampaignThemeWithCulture(culture);
				MusicTheme musicTheme;
				if (campaignThemeWithCulture == MusicTheme.None)
				{
					musicTheme = MusicTheme.CampaignStandard;
					this._factionSpecificCampaignThemeSelectionFactor += 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificCampaignThemeSelectionFactor);
				}
				else
				{
					musicTheme = campaignThemeWithCulture;
					this._factionSpecificCampaignThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificCampaignThemeSelectionFactor);
				}
				return musicTheme;
			}

			// Token: 0x06003BB8 RID: 15288 RVA: 0x000EEEE8 File Offset: 0x000ED0E8
			private MusicTheme GetCampaignThemeWithCulture(BasicCultureObject culture)
			{
				if (MBRandom.NondeterministicRandomFloat <= this._factionSpecificCampaignThemeSelectionFactor)
				{
					this._factionSpecificCampaignThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificCampaignThemeSelectionFactor);
					if (culture.StringId == "empire")
					{
						if (MBRandom.NondeterministicRandomFloat >= 0.5f)
						{
							return MusicTheme.EmpireCampaignB;
						}
						return MusicTheme.EmpireCampaignA;
					}
					else
					{
						if (culture.StringId == "sturgia")
						{
							return MusicTheme.SturgiaCampaignA;
						}
						if (culture.StringId == "aserai")
						{
							return MusicTheme.AseraiCampaignA;
						}
						if (culture.StringId == "vlandia")
						{
							return MusicTheme.VlandiaCampaignA;
						}
						if (culture.StringId == "khuzait")
						{
							return MusicTheme.KhuzaitCampaignA;
						}
						if (culture.StringId == "battania")
						{
							return MusicTheme.BattaniaCampaignA;
						}
						if (culture.StringId == "nord")
						{
							return MusicTheme.NordCampaign;
						}
					}
				}
				return MusicTheme.None;
			}

			// Token: 0x06003BB9 RID: 15289 RVA: 0x000EEFC8 File Offset: 0x000ED1C8
			public MusicTheme GetCampaignDramaticThemeWithCulture(BasicCultureObject culture)
			{
				if (MBRandom.NondeterministicRandomFloat <= this._factionSpecificCampaignDramaticThemeSelectionFactor)
				{
					this._factionSpecificCampaignDramaticThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificCampaignDramaticThemeSelectionFactor);
					if (culture.StringId == "empire")
					{
						return MusicTheme.EmpireCampaignDramatic;
					}
					if (culture.StringId == "sturgia")
					{
						return MusicTheme.SturgiaCampaignDramatic;
					}
					if (culture.StringId == "aserai")
					{
						return MusicTheme.AseraiCampaignDramatic;
					}
					if (culture.StringId == "vlandia")
					{
						return MusicTheme.VlandiaCampaignDramatic;
					}
					if (culture.StringId == "khuzait")
					{
						return MusicTheme.KhuzaitCampaignDramatic;
					}
					if (culture.StringId == "battania")
					{
						return MusicTheme.BattaniaCampaignDramatic;
					}
					if (culture.StringId == "nord")
					{
						return MusicTheme.NordCampaign;
					}
				}
				this._factionSpecificCampaignDramaticThemeSelectionFactor += 0.1f;
				MBMath.ClampUnit(ref this._factionSpecificCampaignDramaticThemeSelectionFactor);
				return MusicTheme.None;
			}

			// Token: 0x06003BBA RID: 15290 RVA: 0x000EF0B8 File Offset: 0x000ED2B8
			public MusicTheme GetSeaCampignMusic(BasicCultureObject culture)
			{
				if (culture.StringId == "sturgia" || culture.StringId == "battania" || culture.StringId == "nord")
				{
					return MusicTheme.SeaCampaignNorthern;
				}
				if (culture.StringId == "aserai" || culture.StringId == "vlandia" || culture.StringId == "khuzait" || culture.StringId == "empire")
				{
					return MusicTheme.SeaCampaignSouthern;
				}
				return MusicTheme.None;
			}

			// Token: 0x04001CDF RID: 7391
			private const float DefaultSelectionFactorForFactionSpecificCampaignTheme = 0.35f;

			// Token: 0x04001CE0 RID: 7392
			private const float SelectionFactorDecayAmountForFactionSpecificCampaignTheme = 0.1f;

			// Token: 0x04001CE1 RID: 7393
			private const float SelectionFactorGrowthAmountForFactionSpecificCampaignTheme = 0.1f;

			// Token: 0x04001CE2 RID: 7394
			private float _factionSpecificCampaignThemeSelectionFactor;

			// Token: 0x04001CE3 RID: 7395
			private float _factionSpecificCampaignDramaticThemeSelectionFactor;
		}

		// Token: 0x0200050D RID: 1293
		private class BattleMusicMode
		{
			// Token: 0x06003BBB RID: 15291 RVA: 0x000EF150 File Offset: 0x000ED350
			public BattleMusicMode()
			{
				this._factionSpecificBattleThemeSelectionFactor = 0.35f;
				this._factionSpecificSiegeThemeSelectionFactor = 0.35f;
			}

			// Token: 0x06003BBC RID: 15292 RVA: 0x000EF170 File Offset: 0x000ED370
			private MusicTheme GetBattleThemeWithCulture(BasicCultureObject culture, out bool isPaganBattle)
			{
				isPaganBattle = false;
				MusicTheme musicTheme = MusicTheme.None;
				if (MBRandom.NondeterministicRandomFloat <= this._factionSpecificBattleThemeSelectionFactor)
				{
					this._factionSpecificBattleThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificBattleThemeSelectionFactor);
					if (culture.StringId == "sturgia" || culture.StringId == "aserai" || culture.StringId == "khuzait" || culture.StringId == "battania")
					{
						isPaganBattle = true;
						musicTheme = ((MBRandom.NondeterministicRandomFloat < 0.5f) ? MusicTheme.BattlePaganA : MusicTheme.BattlePaganB);
					}
					else if (culture.StringId == "nord")
					{
						musicTheme = MusicTheme.BattleNord;
					}
					else
					{
						musicTheme = ((MBRandom.NondeterministicRandomFloat < 0.5f) ? MusicTheme.CombatA : MusicTheme.CombatB);
					}
				}
				return musicTheme;
			}

			// Token: 0x06003BBD RID: 15293 RVA: 0x000EF23C File Offset: 0x000ED43C
			private MusicTheme GetSiegeThemeWithCulture(BasicCultureObject culture)
			{
				MusicTheme musicTheme = MusicTheme.None;
				if (MBRandom.NondeterministicRandomFloat <= this._factionSpecificSiegeThemeSelectionFactor)
				{
					this._factionSpecificSiegeThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificSiegeThemeSelectionFactor);
					if (culture.StringId == "sturgia" || culture.StringId == "aserai" || culture.StringId == "khuzait" || culture.StringId == "battania")
					{
						musicTheme = MusicTheme.PaganSiege;
					}
				}
				return musicTheme;
			}

			// Token: 0x06003BBE RID: 15294 RVA: 0x000EF2C4 File Offset: 0x000ED4C4
			private MusicTheme GetVictoryThemeForCulture(BasicCultureObject culture)
			{
				if (MBRandom.NondeterministicRandomFloat <= 0.65f)
				{
					if (culture.StringId == "empire")
					{
						return MusicTheme.EmpireVictory;
					}
					if (culture.StringId == "sturgia" || culture.StringId == "nord")
					{
						return MusicTheme.SturgiaVictory;
					}
					if (culture.StringId == "aserai")
					{
						return MusicTheme.AseraiVictory;
					}
					if (culture.StringId == "vlandia")
					{
						return MusicTheme.VlandiaVictory;
					}
					if (culture.StringId == "khuzait")
					{
						return MusicTheme.KhuzaitVictory;
					}
					if (culture.StringId == "battania")
					{
						return MusicTheme.BattaniaVictory;
					}
				}
				return MusicTheme.None;
			}

			// Token: 0x06003BBF RID: 15295 RVA: 0x000EF370 File Offset: 0x000ED570
			public MusicTheme GetBattleTheme(BasicCultureObject culture, int battleSize, out bool isPaganBattle)
			{
				MusicTheme battleThemeWithCulture = this.GetBattleThemeWithCulture(culture, out isPaganBattle);
				MusicTheme musicTheme;
				if (battleThemeWithCulture == MusicTheme.None)
				{
					musicTheme = (((float)battleSize < (float)MusicParameters.SmallBattleTreshold - (float)MusicParameters.SmallBattleTreshold * 0.2f * MBRandom.NondeterministicRandomFloat) ? MusicTheme.BattleSmall : MusicTheme.BattleMedium);
					this._factionSpecificBattleThemeSelectionFactor += 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificBattleThemeSelectionFactor);
				}
				else
				{
					musicTheme = battleThemeWithCulture;
					this._factionSpecificBattleThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificBattleThemeSelectionFactor);
				}
				return musicTheme;
			}

			// Token: 0x06003BC0 RID: 15296 RVA: 0x000EF3F0 File Offset: 0x000ED5F0
			public MusicTheme GetSiegeTheme(BasicCultureObject culture)
			{
				MusicTheme siegeThemeWithCulture = this.GetSiegeThemeWithCulture(culture);
				MusicTheme musicTheme;
				if (siegeThemeWithCulture == MusicTheme.None)
				{
					musicTheme = MusicTheme.BattleSiege;
					this._factionSpecificSiegeThemeSelectionFactor += 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificSiegeThemeSelectionFactor);
				}
				else
				{
					musicTheme = siegeThemeWithCulture;
					this._factionSpecificSiegeThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificSiegeThemeSelectionFactor);
				}
				return musicTheme;
			}

			// Token: 0x06003BC1 RID: 15297 RVA: 0x000EF44C File Offset: 0x000ED64C
			public MusicTheme GetBattleEndTheme(BasicCultureObject culture, bool isVictorious)
			{
				MusicTheme musicTheme;
				if (isVictorious)
				{
					MusicTheme victoryThemeForCulture = this.GetVictoryThemeForCulture(culture);
					if (victoryThemeForCulture == MusicTheme.None)
					{
						musicTheme = MusicTheme.BattleVictory;
					}
					else
					{
						musicTheme = victoryThemeForCulture;
					}
				}
				else
				{
					musicTheme = MusicTheme.BattleDefeat;
				}
				return musicTheme;
			}

			// Token: 0x04001CE4 RID: 7396
			private const float DefaultSelectionFactorForFactionSpecificBattleTheme = 0.35f;

			// Token: 0x04001CE5 RID: 7397
			private const float SelectionFactorDecayAmountForFactionSpecificBattleTheme = 0.1f;

			// Token: 0x04001CE6 RID: 7398
			private const float SelectionFactorGrowthAmountForFactionSpecificBattleTheme = 0.1f;

			// Token: 0x04001CE7 RID: 7399
			private const float DefaultSelectionFactorForFactionSpecificVictoryTheme = 0.65f;

			// Token: 0x04001CE8 RID: 7400
			private float _factionSpecificBattleThemeSelectionFactor;

			// Token: 0x04001CE9 RID: 7401
			private float _factionSpecificSiegeThemeSelectionFactor;
		}
	}
}

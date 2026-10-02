using System;
using Helpers;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000443 RID: 1091
	public class SiegeAmbushCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004624 RID: 17956 RVA: 0x0015E1A4 File Offset: 0x0015C3A4
		public override void RegisterEvents()
		{
			CampaignEvents.OnAfterSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnAfterSessionLaunched));
			CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
			CampaignEvents.OnSiegeEventEndedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeEventEnded));
			CampaignEvents.HourlyTickEvent.AddNonSerializedListener(this, new Action(this.HourlyTick));
			CampaignEvents.OnMissionEndedEvent.AddNonSerializedListener(this, new Action<IMission>(this.OnMissionEnded));
			CampaignEvents.OnNewGameCreatedPartialFollowUpEndEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreatedPartialFollowUpEnd));
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x0015E23B File Offset: 0x0015C43B
		private void OnNewGameCreatedPartialFollowUpEnd(CampaignGameStarter starter)
		{
			this._lastAmbushTime = CampaignTime.Never;
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x0015E248 File Offset: 0x0015C448
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<CampaignTime>("_lastAmbushTime", ref this._lastAmbushTime);
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x0015E25C File Offset: 0x0015C45C
		private void OnAfterSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.AddGameMenus(campaignGameStarter);
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x0015E265 File Offset: 0x0015C465
		private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
		{
			if (mapEvent.IsSiegeAmbush)
			{
				this._lastAmbushTime = CampaignTime.Now;
			}
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x0015E27A File Offset: 0x0015C47A
		private void OnSiegeEventEnded(SiegeEvent siegeEvent)
		{
			if (siegeEvent == PlayerSiege.PlayerSiegeEvent)
			{
				this._lastAmbushTime = CampaignTime.Never;
			}
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x0015E28F File Offset: 0x0015C48F
		private void HourlyTick()
		{
			if (PlayerSiege.PlayerSiegeEvent != null && this._lastAmbushTime == CampaignTime.Never && PlayerSiege.PlayerSiegeEvent.BesiegerCamp.IsPreparationComplete)
			{
				this._lastAmbushTime = CampaignTime.Now;
			}
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x0015E2C6 File Offset: 0x0015C4C6
		private void OnMissionEnded(IMission mission)
		{
			MapEvent battle = PlayerEncounter.Battle;
			if (battle != null && battle.IsSiegeAmbush)
			{
				PlayerEncounter.Current.FinalizeBattle();
				PlayerEncounter.Current.SetIsSallyOutAmbush(false);
			}
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x0015E2F0 File Offset: 0x0015C4F0
		private bool CanMainHeroAmbush(out TextObject reason)
		{
			if (this._lastAmbushTime.ElapsedHoursUntilNow < 24f)
			{
				reason = new TextObject("{=lCYPxuWN}The enemy is alert, you cannot ambush right now.", null);
				return false;
			}
			if (Hero.MainHero.IsWounded)
			{
				reason = new TextObject("{=pQaQW1As}You cannot ambush right now due to your wounds.", null);
				return false;
			}
			SiegeEvent playerSiegeEvent = PlayerSiege.PlayerSiegeEvent;
			if (playerSiegeEvent.BesiegerCamp.LeaderParty.MapEvent != null && MobileParty.MainParty.MapEvent == null)
			{
				reason = new TextObject("{=GAh1gNYn}Enemies are already engaged in battle.", null);
				return false;
			}
			if (playerSiegeEvent.GetPreparedSiegeEnginesAsDictionary(playerSiegeEvent.GetSiegeEventSide(BattleSideEnum.Attacker)).Count <= 0)
			{
				reason = new TextObject("{=f4g7r0xF}The enemy does not have any vulnerabilities.", null);
				return false;
			}
			if (playerSiegeEvent.BesiegedSettlement.SettlementWallSectionHitPointsRatioList.AnyQ<float>((float r) => r <= 0f))
			{
				reason = new TextObject("{=Nzt8Xkro}You cannot ambush because the settlement walls are breached.", null);
				return false;
			}
			reason = TextObject.GetEmpty();
			return true;
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x0015E3D8 File Offset: 0x0015C5D8
		private void AddGameMenus(CampaignGameStarter campaignGameSystemStarter)
		{
			campaignGameSystemStarter.AddGameMenuOption("menu_siege_strategies", "menu_siege_strategies_ambush", "{=LEKzuGzi}Ambush", new GameMenuOption.OnConditionDelegate(this.menu_siege_strategies_ambush_condition), new GameMenuOption.OnConsequenceDelegate(this.menu_siege_strategies_ambush_on_consequence), false, -1, false, null);
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x0015E418 File Offset: 0x0015C618
		private bool menu_siege_strategies_ambush_condition(MenuCallbackArgs args)
		{
			if (PlayerSiege.PlayerSiegeEvent == null || PlayerSiege.PlayerSide != BattleSideEnum.Defender)
			{
				return false;
			}
			args.optionLeaveType = GameMenuOption.LeaveType.SiegeAmbush;
			TextObject textObject;
			if (!this.CanMainHeroAmbush(out textObject))
			{
				args.IsEnabled = false;
				args.Tooltip = textObject;
			}
			return true;
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x0015E458 File Offset: 0x0015C658
		private void menu_siege_strategies_ambush_on_consequence(MenuCallbackArgs args)
		{
			this._lastAmbushTime = CampaignTime.Now;
			if (PlayerEncounter.EncounterSettlement != null && PlayerEncounter.EncounterSettlement.SiegeEvent != null && !PlayerEncounter.EncounterSettlement.MapFaction.IsAtWarWith(MobileParty.MainParty.MapFaction))
			{
				PlayerEncounter.RestartPlayerEncounter(PartyBase.MainParty, PlayerEncounter.EncounterSettlement.SiegeEvent.BesiegerCamp.LeaderParty.Party, false, false);
			}
			PlayerEncounter.Current.SetIsSallyOutAmbush(true);
			PlayerEncounter.StartBattle();
			MenuHelper.EncounterAttackConsequence(args);
		}

		// Token: 0x040013B1 RID: 5041
		private const int SiegeAmbushCooldownPeriodAsHours = 24;

		// Token: 0x040013B2 RID: 5042
		private CampaignTime _lastAmbushTime;
	}
}

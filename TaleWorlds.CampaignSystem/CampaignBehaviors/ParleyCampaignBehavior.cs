using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000422 RID: 1058
	public class ParleyCampaignBehavior : CampaignBehaviorBase, IParleyCampaignBehavior
	{
		// Token: 0x06004383 RID: 17283 RVA: 0x00147B99 File Offset: 0x00145D99
		public override void RegisterEvents()
		{
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x00147BB2 File Offset: 0x00145DB2
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<PartyBase>("_parleyedParty", ref this._parleyedParty);
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x00147BC6 File Offset: 0x00145DC6
		public void StartParley(PartyBase partyBase)
		{
			if (partyBase.IsSettlement)
			{
				this._parleyedParty = partyBase;
				GameMenu.ActivateGameMenu("request_meeting_parley");
				return;
			}
			Debug.FailedAssert("MobileParty parley not implemented yet!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\ParleyCampaignBehavior.cs", "StartParley", 35);
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x00147BF8 File Offset: 0x00145DF8
		private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.AddMenus(campaignGameStarter);
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x00147C04 File Offset: 0x00145E04
		private void AddMenus(CampaignGameStarter campaignGameStarter)
		{
			campaignGameStarter.AddGameMenu("request_meeting_parley", "{=pBAx7jTM}With whom do you want to meet?", new OnInitDelegate(this.game_menu_town_menu_request_meeting_on_init), GameMenu.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameStarter.AddGameMenuOption("request_meeting_parley", "request_meeting_with", "{=!}{HERO_TO_MEET.LINK}", new GameMenuOption.OnConditionDelegate(this.game_menu_request_meeting_with_on_condition), new GameMenuOption.OnConsequenceDelegate(this.game_menu_request_meeting_with_on_consequence), false, -1, true, null);
			campaignGameStarter.AddGameMenuOption("request_meeting_parley", "meeting_town_leave", "{=3sRdGQou}Leave", new GameMenuOption.OnConditionDelegate(this.game_meeting_town_leave_on_condition), new GameMenuOption.OnConsequenceDelegate(this.game_menu_request_meeting_town_leave_on_consequence), true, -1, false, null);
			campaignGameStarter.AddGameMenuOption("request_meeting_parley", "meeting_castle_leave", "{=3sRdGQou}Leave", new GameMenuOption.OnConditionDelegate(this.game_meeting_castle_leave_on_condition), new GameMenuOption.OnConsequenceDelegate(this.game_menu_request_meeting_castle_leave_on_consequence), true, -1, false, null);
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x00147CC4 File Offset: 0x00145EC4
		private void game_menu_town_menu_request_meeting_on_init(MenuCallbackArgs args)
		{
			List<Hero> heroesToMeetInTown = TownHelpers.GetHeroesToMeetInTown(this._parleyedParty.Settlement);
			args.MenuContext.SetRepeatObjectList(heroesToMeetInTown);
			args.MenuContext.SetBackgroundMeshName(this._parleyedParty.Settlement.SettlementComponent.WaitMeshName);
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x00147D10 File Offset: 0x00145F10
		private bool game_menu_request_meeting_with_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Conversation;
			Hero hero = args.MenuContext.GetCurrentRepeatableObject() as Hero;
			if (this._parleyedParty != null && hero != null)
			{
				StringHelpers.SetCharacterProperties("HERO_TO_MEET", hero.CharacterObject, null, false);
				MenuHelper.SetIssueAndQuestDataForHero(args, hero);
				return true;
			}
			return false;
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x00147D5E File Offset: 0x00145F5E
		private void game_menu_request_meeting_town_leave_on_consequence(MenuCallbackArgs args)
		{
			this.SettlementMenuLeaveConsequenceCommon();
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x00147D66 File Offset: 0x00145F66
		private void game_menu_request_meeting_castle_leave_on_consequence(MenuCallbackArgs args)
		{
			this.SettlementMenuLeaveConsequenceCommon();
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x00147D6E File Offset: 0x00145F6E
		private void SettlementMenuLeaveConsequenceCommon()
		{
			GameMenu.ExitToLast();
			this._parleyedParty = null;
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x00147D7C File Offset: 0x00145F7C
		private void game_menu_request_meeting_with_on_consequence(MenuCallbackArgs args)
		{
			string text;
			string meetingScene = this.GetMeetingScene(out text);
			Hero hero = (Hero)args.MenuContext.GetSelectedObject();
			ConversationCharacterData conversationCharacterData = new ConversationCharacterData(Hero.MainHero.CharacterObject, PartyBase.MainParty, false, false, false, false, false, false);
			CharacterObject characterObject = hero.CharacterObject;
			MobileParty partyBelongedTo = hero.PartyBelongedTo;
			ConversationCharacterData conversationCharacterData2 = new ConversationCharacterData(characterObject, (partyBelongedTo != null) ? partyBelongedTo.Party : null, true, false, false, false, false, false);
			CampaignMission.OpenConversationMission(conversationCharacterData, conversationCharacterData2, meetingScene, text, false);
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x00147DEC File Offset: 0x00145FEC
		private string GetMeetingScene(out string sceneLevel)
		{
			string text = GameSceneDataManager.Instance.MeetingScenes.GetRandomElementWithPredicate<MeetingSceneData>((MeetingSceneData x) => x.Culture == this._parleyedParty.Settlement.Culture).SceneID;
			if (string.IsNullOrEmpty(text))
			{
				text = GameSceneDataManager.Instance.MeetingScenes.GetRandomElement<MeetingSceneData>().SceneID;
			}
			sceneLevel = "";
			if (this._parleyedParty.Settlement.IsFortification)
			{
				sceneLevel = Campaign.Current.Models.LocationModel.GetUpgradeLevelTag(this._parleyedParty.Settlement.Town.GetWallLevel());
			}
			return text;
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x00147E82 File Offset: 0x00146082
		private bool game_meeting_town_leave_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			return this._parleyedParty.Settlement.IsTown;
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x00147E9C File Offset: 0x0014609C
		private bool game_meeting_castle_leave_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			return this._parleyedParty.Settlement.IsCastle;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x00147EB6 File Offset: 0x001460B6
		public PartyBase GetParleyedParty()
		{
			return this._parleyedParty;
		}

		// Token: 0x0400134D RID: 4941
		private PartyBase _parleyedParty;
	}
}

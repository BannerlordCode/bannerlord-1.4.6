using System;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ScreenSystem;

namespace SandBox.View.Map.Navigation.NavigationElements
{
	// Token: 0x0200006F RID: 111
	public class PartyNavigationElement : MapNavigationElementBase
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0002549E File Offset: 0x0002369E
		public override string StringId
		{
			get
			{
				return "party";
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000254A5 File Offset: 0x000236A5
		public override bool IsActive
		{
			get
			{
				return base._game.GameStateManager.ActiveState is PartyState;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000254C0 File Offset: 0x000236C0
		public override bool IsLockingNavigation
		{
			get
			{
				GameStateManager gameStateManager = GameStateManager.Current;
				PartyState partyState;
				return (partyState = ((gameStateManager != null) ? gameStateManager.ActiveState : null) as PartyState) != null && partyState.PartyScreenLogic != null && partyState.PartyScreenMode != PartyScreenHelper.PartyScreenMode.Normal;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000254FA File Offset: 0x000236FA
		public override bool HasAlert
		{
			get
			{
				return this._viewDataTracker.IsPartyNotificationActive;
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00025507 File Offset: 0x00023707
		public PartyNavigationElement(MapNavigationHandler handler)
			: base(handler)
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00025510 File Offset: 0x00023710
		protected override NavigationPermissionItem GetPermission()
		{
			if (!MapNavigationHelper.IsNavigationBarEnabled(this._handler))
			{
				return new NavigationPermissionItem(false, null);
			}
			if (this.IsActive)
			{
				return new NavigationPermissionItem(false, null);
			}
			if (MobileParty.MainParty.IsInRaftState || Hero.MainHero.HeroState == Hero.CharacterStates.Prisoner)
			{
				return new NavigationPermissionItem(false, null);
			}
			if (MobileParty.MainParty.MapEvent != null)
			{
				return new NavigationPermissionItem(false, null);
			}
			Mission mission = Mission.Current;
			if (mission != null && !mission.IsPartyWindowAccessAllowed)
			{
				return new NavigationPermissionItem(false, null);
			}
			return new NavigationPermissionItem(true, null);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0002559C File Offset: 0x0002379C
		protected override TextObject GetTooltip()
		{
			if (!Input.IsGamepadActive && (base.Permission.IsAuthorized || this.IsActive))
			{
				string text = Game.Current.GameTextManager.GetHotKeyGameText("GenericCampaignPanelsGameKeyCategory", 43).ToString();
				TextObject textObject = GameTexts.FindText("str_hotkey_with_hint", null);
				textObject.SetTextVariable("TEXT", GameTexts.FindText("str_party", null).ToString());
				textObject.SetTextVariable("HOTKEY", text);
				return textObject;
			}
			return GameTexts.FindText("str_party", null);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00025624 File Offset: 0x00023824
		protected override TextObject GetAlertTooltip()
		{
			if (this.HasAlert)
			{
				return this._viewDataTracker.GetPartyNotificationText();
			}
			return TextObject.GetEmpty();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00025640 File Offset: 0x00023840
		public override void OpenView()
		{
			if (base.Permission.IsAuthorized)
			{
				IChangeableScreen changeableScreen;
				if ((changeableScreen = ScreenManager.TopScreen as IChangeableScreen) != null && changeableScreen.AnyUnsavedChanges())
				{
					InformationManager.ShowInquiry(changeableScreen.CanChangesBeApplied() ? MapNavigationHelper.GetUnsavedChangedInquiry(new Action(PartyScreenHelper.OpenScreenAsNormal)) : MapNavigationHelper.GetUnapplicableChangedInquiry(), false, false);
					return;
				}
				MapNavigationHelper.SwitchToANewScreen(new Action(PartyScreenHelper.OpenScreenAsNormal));
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000256AC File Offset: 0x000238AC
		public override void OpenView(params object[] parameters)
		{
			Debug.FailedAssert("Party screen shouldn't be opened with parameters from navigation", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\SandBox.View\\Map\\Navigation\\NavigationElements\\PartyNavigationElement.cs", "OpenView", 118);
			this.OpenView();
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000256CA File Offset: 0x000238CA
		public override void GoToLink()
		{
		}
	}
}

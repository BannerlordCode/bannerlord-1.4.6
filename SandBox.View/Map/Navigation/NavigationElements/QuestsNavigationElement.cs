using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ScreenSystem;

namespace SandBox.View.Map.Navigation.NavigationElements
{
	// Token: 0x02000070 RID: 112
	public class QuestsNavigationElement : MapNavigationElementBase
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000256CC File Offset: 0x000238CC
		public override string StringId
		{
			get
			{
				return "quest";
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x000256D3 File Offset: 0x000238D3
		public override bool IsActive
		{
			get
			{
				return base._game.GameStateManager.ActiveState is QuestsState;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x000256ED File Offset: 0x000238ED
		public override bool IsLockingNavigation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000256F0 File Offset: 0x000238F0
		public override bool HasAlert
		{
			get
			{
				return this._viewDataTracker.IsQuestNotificationActive;
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000256FD File Offset: 0x000238FD
		public QuestsNavigationElement(MapNavigationHandler handler)
			: base(handler)
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00025708 File Offset: 0x00023908
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
			Mission mission = Mission.Current;
			if (mission != null && !mission.IsQuestScreenAccessAllowed)
			{
				return new NavigationPermissionItem(false, null);
			}
			return new NavigationPermissionItem(true, null);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00025760 File Offset: 0x00023960
		protected override TextObject GetTooltip()
		{
			if (!Input.IsGamepadActive && (base.Permission.IsAuthorized || this.IsActive))
			{
				string text = Game.Current.GameTextManager.GetHotKeyGameText("GenericCampaignPanelsGameKeyCategory", 42).ToString();
				TextObject textObject = GameTexts.FindText("str_hotkey_with_hint", null);
				textObject.SetTextVariable("TEXT", GameTexts.FindText("str_quest", null).ToString());
				textObject.SetTextVariable("HOTKEY", text);
				return textObject;
			}
			return GameTexts.FindText("str_quest", null);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000257E8 File Offset: 0x000239E8
		protected override TextObject GetAlertTooltip()
		{
			if (this.HasAlert)
			{
				return this._viewDataTracker.GetQuestNotificationText();
			}
			return TextObject.GetEmpty();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00025803 File Offset: 0x00023A03
		public override void OpenView()
		{
			this.PrepareToOpenQuestsScreen(delegate
			{
				this.OpenQuestsAction();
			});
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00025818 File Offset: 0x00023A18
		public override void OpenView(params object[] parameters)
		{
			if (parameters.Length != 0)
			{
				QuestsNavigationElement.<>c__DisplayClass13_0 CS$<>8__locals1 = new QuestsNavigationElement.<>c__DisplayClass13_0();
				CS$<>8__locals1.<>4__this = this;
				object obj = parameters[0];
				if ((CS$<>8__locals1.issue = obj as IssueBase) != null)
				{
					this.PrepareToOpenQuestsScreen(delegate
					{
						CS$<>8__locals1.<>4__this.OpenQuestsAction(CS$<>8__locals1.issue);
					});
					return;
				}
				QuestsNavigationElement.<>c__DisplayClass13_1 CS$<>8__locals2 = new QuestsNavigationElement.<>c__DisplayClass13_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				if ((CS$<>8__locals2.quest = obj as QuestBase) != null)
				{
					this.PrepareToOpenQuestsScreen(delegate
					{
						CS$<>8__locals2.CS$<>8__locals1.<>4__this.OpenQuestsAction(CS$<>8__locals2.quest);
					});
					return;
				}
				JournalLogEntry log;
				if ((log = obj as JournalLogEntry) != null)
				{
					this.PrepareToOpenQuestsScreen(delegate
					{
						CS$<>8__locals2.CS$<>8__locals1.<>4__this.OpenQuestsAction(log);
					});
					return;
				}
				Debug.FailedAssert(string.Format("Invalid parameter type when opening the quest screen from navigation: {0}", obj.GetType()), "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\SandBox.View\\Map\\Navigation\\NavigationElements\\QuestsNavigationElement.cs", "OpenView", 97);
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000258EC File Offset: 0x00023AEC
		public override void GoToLink()
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000258F0 File Offset: 0x00023AF0
		private void PrepareToOpenQuestsScreen(Action openQuestsAction)
		{
			if (base.Permission.IsAuthorized)
			{
				IChangeableScreen changeableScreen;
				if ((changeableScreen = ScreenManager.TopScreen as IChangeableScreen) != null && changeableScreen.AnyUnsavedChanges())
				{
					InformationManager.ShowInquiry(changeableScreen.CanChangesBeApplied() ? MapNavigationHelper.GetUnsavedChangedInquiry(openQuestsAction) : MapNavigationHelper.GetUnapplicableChangedInquiry(), false, false);
					return;
				}
				MapNavigationHelper.SwitchToANewScreen(openQuestsAction);
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00025948 File Offset: 0x00023B48
		private void OpenQuestsAction()
		{
			QuestsState questsState = base._game.GameStateManager.CreateState<QuestsState>();
			base._game.GameStateManager.PushState(questsState, 0);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00025978 File Offset: 0x00023B78
		private void OpenQuestsAction(IssueBase issue)
		{
			QuestsState questsState = base._game.GameStateManager.CreateState<QuestsState>(new object[] { issue });
			base._game.GameStateManager.PushState(questsState, 0);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000259B4 File Offset: 0x00023BB4
		private void OpenQuestsAction(QuestBase quest)
		{
			QuestsState questsState = base._game.GameStateManager.CreateState<QuestsState>(new object[] { quest });
			base._game.GameStateManager.PushState(questsState, 0);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000259F0 File Offset: 0x00023BF0
		private void OpenQuestsAction(JournalLogEntry log)
		{
			QuestsState questsState = base._game.GameStateManager.CreateState<QuestsState>(new object[] { log });
			base._game.GameStateManager.PushState(questsState, 0);
		}
	}
}

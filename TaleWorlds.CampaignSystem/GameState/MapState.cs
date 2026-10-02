using System;
using Helpers;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Incidents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000399 RID: 921
	public class MapState : GameState
	{
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x000D9881 File Offset: 0x000D7A81
		// (set) Token: 0x06003534 RID: 13620 RVA: 0x000D9889 File Offset: 0x000D7A89
		public Incident NextIncident
		{
			get
			{
				return this._nextIncident;
			}
			set
			{
				this._nextIncident = value;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x000D9892 File Offset: 0x000D7A92
		// (set) Token: 0x06003536 RID: 13622 RVA: 0x000D989A File Offset: 0x000D7A9A
		public MenuContext MenuContext
		{
			get
			{
				return this._menuContext;
			}
			private set
			{
				this._menuContext = value;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x000D98A3 File Offset: 0x000D7AA3
		// (set) Token: 0x06003538 RID: 13624 RVA: 0x000D98B4 File Offset: 0x000D7AB4
		public string GameMenuId
		{
			get
			{
				return Campaign.Current.MapStateData.GameMenuId;
			}
			set
			{
				Campaign.Current.MapStateData.GameMenuId = value;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x000D98C6 File Offset: 0x000D7AC6
		public bool AtMenu
		{
			get
			{
				return this.MenuContext != null;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x000D98D1 File Offset: 0x000D7AD1
		public bool MapConversationActive
		{
			get
			{
				return this._mapConversationActive;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600353B RID: 13627 RVA: 0x000D98D9 File Offset: 0x000D7AD9
		// (set) Token: 0x0600353C RID: 13628 RVA: 0x000D98E1 File Offset: 0x000D7AE1
		public IMapStateHandler Handler
		{
			get
			{
				return this._handler;
			}
			set
			{
				this._handler = value;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x000D98EA File Offset: 0x000D7AEA
		public bool IsSimulationActive
		{
			get
			{
				return this._battleSimulation != null;
			}
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x000D98F5 File Offset: 0x000D7AF5
		protected override void OnIdleTick(float dt)
		{
			base.OnIdleTick(dt);
			IMapStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnIdleTick(dt);
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000D990F File Offset: 0x000D7B0F
		private void RefreshHandler()
		{
			IMapStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnRefreshState();
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000D9921 File Offset: 0x000D7B21
		public void OnJoinArmy()
		{
			this.RefreshHandler();
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000D9929 File Offset: 0x000D7B29
		public void OnLeaveArmy()
		{
			this.RefreshHandler();
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000D9931 File Offset: 0x000D7B31
		public void OnDispersePlayerLeadedArmy()
		{
			this.RefreshHandler();
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000D9939 File Offset: 0x000D7B39
		public void OnArmyCreated(MobileParty mobileParty)
		{
			this.RefreshHandler();
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000D9941 File Offset: 0x000D7B41
		public void StartIncident(Incident incident)
		{
			IMapStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnIncidentStarted(incident);
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000D9954 File Offset: 0x000D7B54
		public void OnMainPartyEncounter()
		{
			IMapStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnMainPartyEncounter();
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000D9968 File Offset: 0x000D7B68
		public void ProcessTravel(CampaignVec2 moveTargetPoint)
		{
			MobileParty.MainParty.ForceAiNoPathMode = false;
			NavigationHelper.EmbarkDisembarkData embarkDisembarkData = NavigationHelper.EmbarkDisembarkData.Invalid;
			if (MobileParty.MainParty.HasNavalNavigationCapability)
			{
				Vec2 vec = (moveTargetPoint.ToVec2() - MobileParty.MainParty.Position.ToVec2()).Normalized();
				embarkDisembarkData = NavigationHelper.GetEmbarkAndDisembarkDataForPlayer(MobileParty.MainParty.Position, vec, moveTargetPoint, moveTargetPoint.IsOnLand);
				if (embarkDisembarkData.IsTargetingTheDeadZone)
				{
					moveTargetPoint = (MobileParty.MainParty.IsTransitionInProgress ? embarkDisembarkData.TransitionEndPosition : embarkDisembarkData.TransitionStartPosition);
				}
			}
			MobileParty.NavigationType navigationType;
			if (NavigationHelper.CanPlayerNavigateToPosition(moveTargetPoint, out navigationType))
			{
				MobileParty.MainParty.SetMoveGoToPoint(moveTargetPoint, navigationType);
			}
			if (MobileParty.MainParty.HasNavalNavigationCapability && !embarkDisembarkData.IsTargetingTheDeadZone && navigationType == MobileParty.NavigationType.Naval && MobileParty.MainParty.IsCurrentlyAtSea && MobileParty.MainParty.IsTransitionInProgress)
			{
				MobileParty.MainParty.CancelNavigationTransition();
			}
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000D9A48 File Offset: 0x000D7C48
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (Campaign.Current.SaveHandler.IsSaving)
			{
				Campaign.Current.SaveHandler.SaveTick();
				return;
			}
			if (this._battleSimulation != null)
			{
				this._battleSimulation.Tick(dt);
			}
			else if (this.AtMenu)
			{
				this.OnMenuModeTick(dt);
			}
			this.OnMapModeTick(dt);
			if (!Campaign.Current.SaveHandler.IsSaving)
			{
				Campaign.Current.SaveHandler.CampaignTick();
			}
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000D9AC9 File Offset: 0x000D7CC9
		private void OnMenuModeTick(float dt)
		{
			this.MenuContext.OnTick(dt);
			IMapStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnMenuModeTick(dt);
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000D9AE8 File Offset: 0x000D7CE8
		private void OnMapModeTick(float dt)
		{
			if (this._closeScreenNextFrame)
			{
				Game.Current.GameStateManager.CleanStates(0);
				return;
			}
			if (this.Handler != null)
			{
				this.Handler.BeforeTick(dt);
			}
			if (Campaign.Current != null && base.GameStateManager.ActiveState == this)
			{
				Campaign.Current.RealTick(dt);
				IMapStateHandler handler = this.Handler;
				if (handler != null)
				{
					handler.Tick(dt);
				}
				IMapStateHandler handler2 = this.Handler;
				if (handler2 != null)
				{
					handler2.AfterTick(dt);
				}
				Campaign.Current.Tick();
				IMapStateHandler handler3 = this.Handler;
				if (handler3 == null)
				{
					return;
				}
				handler3.AfterWaitTick(dt);
			}
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000D9B84 File Offset: 0x000D7D84
		public void OnLoadingFinished()
		{
			if (!string.IsNullOrEmpty(this.GameMenuId))
			{
				this.EnterMenuMode();
			}
			this.RefreshHandler();
			if (Campaign.Current.CurrentMenuContext != null && Campaign.Current.CurrentMenuContext.GameMenu != null && Campaign.Current.CurrentMenuContext.GameMenu.IsWaitMenu)
			{
				Campaign.Current.CurrentMenuContext.GameMenu.StartWait();
			}
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnGameLoadFinished();
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000D9C0C File Offset: 0x000D7E0C
		public void OnMapConversationStarts(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData)
		{
			this._mapConversationActive = true;
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnMapConversationStarts(playerCharacterData, conversationPartnerData);
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000D9C28 File Offset: 0x000D7E28
		public void OnMapConversationOver()
		{
			IMapStateHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnMapConversationOver();
			}
			this._mapConversationActive = false;
			if (Game.Current.GameStateManager.ActiveState is MapState)
			{
				MenuContext menuContext = this.MenuContext;
				if (menuContext != null)
				{
					menuContext.Refresh();
				}
			}
			this.RefreshHandler();
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000D9C7A File Offset: 0x000D7E7A
		internal void OnSignalPeriodicEvents()
		{
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnSignalPeriodicEvents();
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000D9C8C File Offset: 0x000D7E8C
		internal void OnHourlyTick()
		{
			IMapStateHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnHourlyTick();
			}
			MenuContext menuContext = this.MenuContext;
			if (menuContext == null)
			{
				return;
			}
			menuContext.OnHourlyTick();
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000D9CAF File Offset: 0x000D7EAF
		protected override void OnActivate()
		{
			base.OnActivate();
			if (!Campaign.Current.ConversationManager.IsConversationFlowActive)
			{
				MenuContext menuContext = this.MenuContext;
				if (menuContext != null)
				{
					menuContext.Refresh();
				}
			}
			this.RefreshHandler();
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000D9CDF File Offset: 0x000D7EDF
		public void EnterMenuMode()
		{
			this.MenuContext = MBObjectManager.Instance.CreateObject<MenuContext>();
			IMapStateHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnEnteringMenuMode(this.MenuContext);
			}
			this.MenuContext.Refresh();
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000D9D13 File Offset: 0x000D7F13
		public void ExitMenuMode()
		{
			IMapStateHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnExitingMenuMode();
			}
			this.MenuContext.Destroy();
			MBObjectManager.Instance.UnregisterObject(this.MenuContext);
			this.MenuContext = null;
			this.GameMenuId = null;
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000D9D4F File Offset: 0x000D7F4F
		public void StartBattleSimulation()
		{
			this._battleSimulation = PlayerEncounter.Current.BattleSimulation;
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnBattleSimulationStarted(this._battleSimulation);
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000D9D77 File Offset: 0x000D7F77
		public void EndBattleSimulation()
		{
			this._battleSimulation = null;
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnBattleSimulationEnded();
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000D9D90 File Offset: 0x000D7F90
		public void OnPlayerSiegeActivated()
		{
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerSiegeActivated();
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000D9DA2 File Offset: 0x000D7FA2
		public void OnPlayerSiegeDeactivated()
		{
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerSiegeDeactivated();
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000D9DB4 File Offset: 0x000D7FB4
		public void OnSiegeEngineClick(MatrixFrame siegeEngineFrame)
		{
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnSiegeEngineClick(siegeEngineFrame);
		}

		// Token: 0x04000F34 RID: 3892
		private Incident _nextIncident;

		// Token: 0x04000F35 RID: 3893
		private MenuContext _menuContext;

		// Token: 0x04000F36 RID: 3894
		private bool _mapConversationActive;

		// Token: 0x04000F37 RID: 3895
		private bool _closeScreenNextFrame;

		// Token: 0x04000F38 RID: 3896
		private IMapStateHandler _handler;

		// Token: 0x04000F39 RID: 3897
		private BattleSimulation _battleSimulation;
	}
}

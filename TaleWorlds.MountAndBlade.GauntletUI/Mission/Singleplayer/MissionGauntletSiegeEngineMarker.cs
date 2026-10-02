using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD.FormationMarker;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x0200003D RID: 61
	[OverrideView(typeof(MissionSiegeEngineMarkerView))]
	public class MissionGauntletSiegeEngineMarker : MissionBattleUIBaseView
	{
		// Token: 0x060002CA RID: 714 RVA: 0x000107E4 File Offset: 0x0000E9E4
		protected override void OnCreateView()
		{
			this._dataSource = new MissionSiegeEngineMarkerVM(base.Mission, base.MissionScreen.CombatCamera);
			this._gauntletLayer = new GauntletLayer("MissionSiegeEngineMarker", this.ViewOrderPriority, false);
			this._gauntletLayer.LoadMovie("SiegeEngineMarker", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
			this._orderHandler = base.Mission.GetMissionBehavior<MissionGauntletSingleplayerOrderUIHandler>();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00010860 File Offset: 0x0000EA60
		public override void OnDeploymentFinished()
		{
			base.OnDeploymentFinished();
			this._siegeEngines = new List<SiegeWeapon>();
			using (List<MissionObject>.Enumerator enumerator = base.Mission.ActiveMissionObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SiegeWeapon siegeWeapon;
					if ((siegeWeapon = enumerator.Current as SiegeWeapon) != null && siegeWeapon.DestructionComponent != null && siegeWeapon.Side != BattleSideEnum.None)
					{
						this._siegeEngines.Add(siegeWeapon);
					}
				}
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000108E8 File Offset: 0x0000EAE8
		protected override void OnDestroyView()
		{
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00010914 File Offset: 0x0000EB14
		protected override void OnSuspendView()
		{
			if (this._gauntletLayer != null)
			{
				ScreenManager.SetSuspendLayer(this._gauntletLayer, true);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0001092A File Offset: 0x0000EB2A
		protected override void OnResumeView()
		{
			if (this._gauntletLayer != null)
			{
				ScreenManager.SetSuspendLayer(this._gauntletLayer, false);
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00010940 File Offset: 0x0000EB40
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (base.IsViewCreated)
			{
				if (!this._dataSource.IsInitialized && this._siegeEngines != null)
				{
					this._dataSource.InitializeWith(this._siegeEngines);
				}
				if (!this._orderHandler.IsDeployment)
				{
					this._dataSource.IsEnabled = base.Input.IsGameKeyDown(5);
				}
				this._dataSource.Tick(dt);
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000109B2 File Offset: 0x0000EBB2
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			if (base.IsViewCreated)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 0f;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000109D7 File Offset: 0x0000EBD7
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			if (base.IsViewCreated)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 1f;
			}
		}

		// Token: 0x04000175 RID: 373
		private List<SiegeWeapon> _siegeEngines;

		// Token: 0x04000176 RID: 374
		private MissionSiegeEngineMarkerVM _dataSource;

		// Token: 0x04000177 RID: 375
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000178 RID: 376
		private MissionGauntletSingleplayerOrderUIHandler _orderHandler;
	}
}

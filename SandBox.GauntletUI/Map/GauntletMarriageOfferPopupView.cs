using System;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MarriageOfferPopup;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000043 RID: 67
	[OverrideView(typeof(MarriageOfferPopupView))]
	public class GauntletMarriageOfferPopupView : MapView
	{
		// Token: 0x06000314 RID: 788 RVA: 0x00012061 File Offset: 0x00010261
		public GauntletMarriageOfferPopupView(Hero suitor, Hero maiden)
		{
			this._suitor = suitor;
			this._maiden = maiden;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00012078 File Offset: 0x00010278
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new MarriageOfferPopupVM(this._suitor, this._maiden, new Action(this.OnPopupClosed));
			this.InitializeKeyVisuals();
			base.Layer = new GauntletLayer("MapMarriageOffer", 203, false);
			this._layerAsGauntletLayer = base.Layer as GauntletLayer;
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
			base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			base.Layer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(base.Layer);
			this._movie = this._layerAsGauntletLayer.LoadMovie("MarriageOfferPopup", this._dataSource);
			base.MapScreen.AddLayer(base.Layer);
			base.MapScreen.SetIsMarriageOfferPopupActive(true);
			this._previousTimeControlMode = Campaign.Current.TimeControlMode;
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
			Campaign.Current.SetTimeControlModeLock(true);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00012197 File Offset: 0x00010397
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			this.HandleInput();
			MarriageOfferPopupVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.Update();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000121B6 File Offset: 0x000103B6
		protected override void OnMenuModeTick(float dt)
		{
			base.OnMenuModeTick(dt);
			this.HandleInput();
			MarriageOfferPopupVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.Update();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000121D5 File Offset: 0x000103D5
		protected override void OnIdleTick(float dt)
		{
			base.OnIdleTick(dt);
			this.HandleInput();
			MarriageOfferPopupVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.Update();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000121F4 File Offset: 0x000103F4
		protected override void OnFinalize()
		{
			this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			base.MapScreen.RemoveLayer(base.Layer);
			this._movie = null;
			this._dataSource = null;
			base.Layer = null;
			this._layerAsGauntletLayer = null;
			base.MapScreen.SetIsMarriageOfferPopupActive(false);
			Campaign.Current.SetTimeControlModeLock(false);
			Campaign.Current.TimeControlMode = this._previousTimeControlMode;
			base.OnFinalize();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001226C File Offset: 0x0001046C
		protected override bool IsEscaped()
		{
			MarriageOfferPopupVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.ExecuteDeclineOffer();
			}
			return true;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00012280 File Offset: 0x00010480
		protected override bool IsOpeningEscapeMenuOnFocusChangeAllowed()
		{
			return false;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00012283 File Offset: 0x00010483
		private void OnPopupClosed()
		{
			base.MapScreen.CloseMarriageOfferPopup();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00012290 File Offset: 0x00010490
		private void HandleInput()
		{
			if (this._dataSource != null)
			{
				if (base.Layer.Input.IsGameKeyPressed(39))
				{
					base.MapScreen.OpenEncyclopedia();
					return;
				}
				if (base.Layer.Input.IsHotKeyReleased("Confirm"))
				{
					UISoundsHelper.PlayUISound("event:/ui/panels/next");
					this._dataSource.ExecuteAcceptOffer();
					return;
				}
				if (base.Layer.Input.IsHotKeyReleased("Exit"))
				{
					UISoundsHelper.PlayUISound("event:/ui/panels/next");
					this._dataSource.ExecuteDeclineOffer();
				}
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001231E File Offset: 0x0001051E
		private void InitializeKeyVisuals()
		{
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
		}

		// Token: 0x0400012B RID: 299
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x0400012C RID: 300
		private MarriageOfferPopupVM _dataSource;

		// Token: 0x0400012D RID: 301
		private GauntletMovieIdentifier _movie;

		// Token: 0x0400012E RID: 302
		private CampaignTimeControlMode _previousTimeControlMode;

		// Token: 0x0400012F RID: 303
		private Hero _suitor;

		// Token: 0x04000130 RID: 304
		private Hero _maiden;
	}
}

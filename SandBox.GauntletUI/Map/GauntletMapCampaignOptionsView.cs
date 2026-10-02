using System;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000030 RID: 48
	[OverrideView(typeof(MapCampaignOptionsView))]
	public class GauntletMapCampaignOptionsView : MapCampaignOptionsView
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000E908 File Offset: 0x0000CB08
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new CampaignOptionsVM(new Action(this.OnClose));
			base.Layer = new GauntletLayer("MapCampaignOptions", 4401, false)
			{
				IsFocusLayer = true
			};
			this._layerAsGauntletLayer = base.Layer as GauntletLayer;
			this._layerAsGauntletLayer.LoadMovie("CampaignOptions", this._dataSource);
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			base.MapScreen.AddLayer(base.Layer);
			base.MapScreen.PauseAmbientSounds();
			ScreenManager.TrySetFocus(base.Layer);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E9CA File Offset: 0x0000CBCA
		private void OnClose()
		{
			MapScreen.Instance.CloseCampaignOptions();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000E9D6 File Offset: 0x0000CBD6
		protected override void OnIdleTick(float dt)
		{
			base.OnFrameTick(dt);
			if (base.Layer.Input.IsHotKeyReleased("Exit"))
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				this._dataSource.ExecuteDone();
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000EA0C File Offset: 0x0000CC0C
		protected override void OnFinalize()
		{
			base.OnFinalize();
			base.Layer.InputRestrictions.ResetInputRestrictions();
			base.MapScreen.RemoveLayer(base.Layer);
			base.MapScreen.RestartAmbientSounds();
			ScreenManager.TryLoseFocus(base.Layer);
			base.Layer = null;
			this._dataSource = null;
			this._layerAsGauntletLayer = null;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000EA6B File Offset: 0x0000CC6B
		protected override void OnMapConversationStart()
		{
			base.OnMapConversationStart();
			if (this._layerAsGauntletLayer != null)
			{
				ScreenManager.SetSuspendLayer(this._layerAsGauntletLayer, true);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000EA87 File Offset: 0x0000CC87
		protected override void OnMapConversationOver()
		{
			base.OnMapConversationOver();
			if (this._layerAsGauntletLayer != null)
			{
				ScreenManager.SetSuspendLayer(this._layerAsGauntletLayer, false);
			}
		}

		// Token: 0x040000D0 RID: 208
		private CampaignOptionsVM _dataSource;

		// Token: 0x040000D1 RID: 209
		private GauntletLayer _layerAsGauntletLayer;
	}
}

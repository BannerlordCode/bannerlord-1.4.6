using System;
using SandBox.View.Map;
using SandBox.ViewModelCollection.SaveLoad;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x0200003F RID: 63
	[OverrideView(typeof(MapSaveView))]
	public class GauntletMapSaveView : MapView
	{
		// Token: 0x060002EE RID: 750 RVA: 0x00011644 File Offset: 0x0000F844
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new MapSaveVM(new Action<bool>(this.OnStateChange));
			this._layerAsGauntletLayer = new GauntletLayer("MapSave", 10000, false);
			this._layerAsGauntletLayer.LoadMovie("MapSave", this._dataSource);
			base.Layer = this._layerAsGauntletLayer;
			base.Layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.MouseButtons | InputUsageMask.Keyboardkeys);
			base.MapScreen.AddLayer(base.Layer);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000116CC File Offset: 0x0000F8CC
		private void OnStateChange(bool isActive)
		{
			if (isActive)
			{
				base.Layer.IsFocusLayer = true;
				ScreenManager.TrySetFocus(base.Layer);
				base.Layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
				return;
			}
			base.Layer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(base.Layer);
			base.Layer.InputRestrictions.ResetInputRestrictions();
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001172D File Offset: 0x0000F92D
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._dataSource.OnFinalize();
			base.MapScreen.RemoveLayer(base.Layer);
			base.Layer = null;
			this._dataSource = null;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001175F File Offset: 0x0000F95F
		protected override void OnMapConversationStart()
		{
			base.OnMapConversationStart();
			if (this._layerAsGauntletLayer != null)
			{
				ScreenManager.SetSuspendLayer(this._layerAsGauntletLayer, true);
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001177B File Offset: 0x0000F97B
		protected override void OnMapConversationOver()
		{
			base.OnMapConversationOver();
			if (this._layerAsGauntletLayer != null)
			{
				ScreenManager.SetSuspendLayer(this._layerAsGauntletLayer, false);
			}
		}

		// Token: 0x04000120 RID: 288
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x04000121 RID: 289
		private MapSaveVM _dataSource;
	}
}

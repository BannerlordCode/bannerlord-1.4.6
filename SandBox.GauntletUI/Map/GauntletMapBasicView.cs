using System;
using SandBox.View.Map;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x0200002E RID: 46
	[OverrideView(typeof(MapBasicView))]
	public class GauntletMapBasicView : MapView
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000E4D7 File Offset: 0x0000C6D7
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000E4DF File Offset: 0x0000C6DF
		public GauntletLayer GauntletLayer { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000E4F0 File Offset: 0x0000C6F0
		public GauntletLayer GauntletNameplateLayer { get; private set; }

		// Token: 0x06000243 RID: 579 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this.GauntletLayer = new GauntletLayer("MapMenuView", 100, false);
			this.GauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
			base.MapScreen.AddLayer(this.GauntletLayer);
			this.GauntletNameplateLayer = new GauntletLayer("MapNameplateLayer", 90, false);
			this.GauntletNameplateLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.MouseButtons | InputUsageMask.Keyboardkeys);
			base.MapScreen.AddLayer(this.GauntletNameplateLayer);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000E57B File Offset: 0x0000C77B
		protected override void OnMapConversationStart()
		{
			base.OnMapConversationStart();
			ScreenManager.SetSuspendLayer(this.GauntletLayer, true);
			ScreenManager.SetSuspendLayer(this.GauntletNameplateLayer, true);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000E59B File Offset: 0x0000C79B
		protected override void OnMapConversationOver()
		{
			base.OnMapConversationOver();
			ScreenManager.SetSuspendLayer(this.GauntletLayer, false);
			ScreenManager.SetSuspendLayer(this.GauntletNameplateLayer, false);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000E5BB File Offset: 0x0000C7BB
		protected override void OnFinalize()
		{
			base.MapScreen.RemoveLayer(this.GauntletLayer);
			this.GauntletLayer = null;
			base.OnFinalize();
		}
	}
}

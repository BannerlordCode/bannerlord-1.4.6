using System;
using SandBox.View.Map;
using SandBox.View.Map.Navigation;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x0200002C RID: 44
	[OverrideView(typeof(MapBarView))]
	public class GauntletMapBarView : MapView
	{
		// Token: 0x06000228 RID: 552 RVA: 0x0000DA10 File Offset: 0x0000BC10
		protected override void OnMapConversationStart()
		{
			base.OnMapConversationStart();
			this._mapBarGlobalLayer.OnMapConversationStarted();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000DA23 File Offset: 0x0000BC23
		protected override void OnMapConversationOver()
		{
			base.OnMapConversationOver();
			this._mapBarGlobalLayer.OnMapConversationOver();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000DA36 File Offset: 0x0000BC36
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._mapBarGlobalLayer = new GauntletMapBarGlobalLayer(base.MapScreen, new MapNavigationHandler(), 8.5f);
			this._mapBarGlobalLayer.Initialize(new MapBarVM());
			ScreenManager.AddGlobalLayer(this._mapBarGlobalLayer, true);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000DA75 File Offset: 0x0000BC75
		protected override void OnFinalize()
		{
			this._mapBarGlobalLayer.OnFinalize();
			ScreenManager.RemoveGlobalLayer(this._mapBarGlobalLayer);
			base.OnFinalize();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000DA93 File Offset: 0x0000BC93
		protected override void OnResume()
		{
			base.OnResume();
			this._mapBarGlobalLayer.Refresh();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000DAA6 File Offset: 0x0000BCA6
		protected override bool IsEscaped()
		{
			return this._mapBarGlobalLayer.IsEscaped();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000DAB3 File Offset: 0x0000BCB3
		protected override TutorialContexts GetTutorialContext()
		{
			if (this._mapBarGlobalLayer.IsInArmyManagement)
			{
				return TutorialContexts.ArmyManagement;
			}
			return base.GetTutorialContext();
		}

		// Token: 0x040000BD RID: 189
		protected GauntletMapBarGlobalLayer _mapBarGlobalLayer;
	}
}

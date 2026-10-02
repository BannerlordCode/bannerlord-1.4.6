using System;
using SandBox.View.Map;
using SandBox.ViewModelCollection.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000038 RID: 56
	[OverrideView(typeof(MapEventVisualsView))]
	public class GauntletMapEventVisualsView : MapView, IGauntletMapEventVisualHandler
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000FED0 File Offset: 0x0000E0D0
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new MapEventVisualsVM(base.MapScreen.MapCameraView.Camera);
			GauntletMapBasicView mapView = base.MapScreen.GetMapView<GauntletMapBasicView>();
			base.Layer = mapView.GauntletNameplateLayer;
			this._layerAsGauntletLayer = base.Layer as GauntletLayer;
			this._movie = this._layerAsGauntletLayer.LoadMovie("MapEventVisuals", this._dataSource);
			GauntletMapEventVisualCreator gauntletMapEventVisualCreator;
			if ((gauntletMapEventVisualCreator = Campaign.Current.VisualCreator.MapEventVisualCreator as GauntletMapEventVisualCreator) != null)
			{
				gauntletMapEventVisualCreator.Handlers.Add(this);
				foreach (GauntletMapEventVisual gauntletMapEventVisual in gauntletMapEventVisualCreator.GetCurrentEvents())
				{
					this._dataSource.OnMapEventStarted(gauntletMapEventVisual.MapEvent);
				}
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		protected override void OnMapScreenUpdate(float dt)
		{
			base.OnMapScreenUpdate(dt);
			this._dataSource.Update(dt);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		protected override void OnFinalize()
		{
			GauntletMapEventVisualCreator gauntletMapEventVisualCreator;
			if ((gauntletMapEventVisualCreator = Campaign.Current.VisualCreator.MapEventVisualCreator as GauntletMapEventVisualCreator) != null)
			{
				gauntletMapEventVisualCreator.Handlers.Remove(this);
			}
			this._dataSource.OnFinalize();
			this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			base.Layer = null;
			this._layerAsGauntletLayer = null;
			this._movie = null;
			this._dataSource = null;
			base.OnFinalize();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0001003C File Offset: 0x0000E23C
		protected override void OnMapConversationStart()
		{
			base.OnMapConversationStart();
			if (this._layerAsGauntletLayer != null)
			{
				ScreenManager.SetSuspendLayer(this._layerAsGauntletLayer, true);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00010058 File Offset: 0x0000E258
		protected override void OnMapConversationOver()
		{
			base.OnMapConversationOver();
			if (this._layerAsGauntletLayer != null)
			{
				ScreenManager.SetSuspendLayer(this._layerAsGauntletLayer, false);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00010074 File Offset: 0x0000E274
		void IGauntletMapEventVisualHandler.OnNewEventStarted(GauntletMapEventVisual newEvent)
		{
			this._dataSource.OnMapEventStarted(newEvent.MapEvent);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00010087 File Offset: 0x0000E287
		void IGauntletMapEventVisualHandler.OnInitialized(GauntletMapEventVisual newEvent)
		{
			this._dataSource.OnMapEventStarted(newEvent.MapEvent);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0001009A File Offset: 0x0000E29A
		void IGauntletMapEventVisualHandler.OnEventEnded(GauntletMapEventVisual newEvent)
		{
			this._dataSource.OnMapEventEnded(newEvent.MapEvent);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000100AD File Offset: 0x0000E2AD
		void IGauntletMapEventVisualHandler.OnEventVisibilityChanged(GauntletMapEventVisual visibilityChangedEvent)
		{
			this._dataSource.OnMapEventVisibilityChanged(visibilityChangedEvent.MapEvent);
		}

		// Token: 0x040000F9 RID: 249
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000FA RID: 250
		private GauntletMovieIdentifier _movie;

		// Token: 0x040000FB RID: 251
		private MapEventVisualsVM _dataSource;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.MapEvents;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000035 RID: 53
	public class GauntletMapEventVisualCreator : IMapEventVisualCreator
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000FA0C File Offset: 0x0000DC0C
		public IMapEventVisual CreateMapEventVisual(MapEvent mapEvent)
		{
			GauntletMapEventVisual newEventVisual = new GauntletMapEventVisual(mapEvent, new Action<GauntletMapEventVisual>(this.OnMapEventInitialized), new Action<GauntletMapEventVisual>(this.OnMapEventVisibilityChanged), new Action<GauntletMapEventVisual>(this.OnMapEventOver));
			List<IGauntletMapEventVisualHandler> handlers = this.Handlers;
			if (handlers != null)
			{
				handlers.ForEach(delegate(IGauntletMapEventVisualHandler h)
				{
					h.OnNewEventStarted(newEventVisual);
				});
			}
			this._listOfEvents.Add(newEventVisual);
			return newEventVisual;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000FA84 File Offset: 0x0000DC84
		private void OnMapEventOver(GauntletMapEventVisual overEvent)
		{
			this._listOfEvents.Remove(overEvent);
			List<IGauntletMapEventVisualHandler> handlers = this.Handlers;
			if (handlers == null)
			{
				return;
			}
			handlers.ForEach(delegate(IGauntletMapEventVisualHandler h)
			{
				h.OnEventEnded(overEvent);
			});
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000FACC File Offset: 0x0000DCCC
		private void OnMapEventInitialized(GauntletMapEventVisual initializedEvent)
		{
			List<IGauntletMapEventVisualHandler> handlers = this.Handlers;
			if (handlers == null)
			{
				return;
			}
			handlers.ForEach(delegate(IGauntletMapEventVisualHandler h)
			{
				h.OnInitialized(initializedEvent);
			});
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000FB04 File Offset: 0x0000DD04
		private void OnMapEventVisibilityChanged(GauntletMapEventVisual visibilityChangedEvent)
		{
			List<IGauntletMapEventVisualHandler> handlers = this.Handlers;
			if (handlers == null)
			{
				return;
			}
			handlers.ForEach(delegate(IGauntletMapEventVisualHandler h)
			{
				h.OnEventVisibilityChanged(visibilityChangedEvent);
			});
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000FB3A File Offset: 0x0000DD3A
		public IEnumerable<GauntletMapEventVisual> GetCurrentEvents()
		{
			return this._listOfEvents.AsEnumerable<GauntletMapEventVisual>();
		}

		// Token: 0x040000EA RID: 234
		public List<IGauntletMapEventVisualHandler> Handlers = new List<IGauntletMapEventVisualHandler>();

		// Token: 0x040000EB RID: 235
		private readonly List<GauntletMapEventVisual> _listOfEvents = new List<GauntletMapEventVisual>();
	}
}

using System;
using TaleWorlds.CampaignSystem.Incidents;

namespace SandBox.View.Map
{
	// Token: 0x02000050 RID: 80
	public class MapIncidentView : MapView
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x00017FC5 File Offset: 0x000161C5
		public MapIncidentView()
		{
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00017FCD File Offset: 0x000161CD
		public MapIncidentView(Incident incident)
		{
			this.Incident = incident;
		}

		// Token: 0x04000173 RID: 371
		public readonly Incident Incident;
	}
}

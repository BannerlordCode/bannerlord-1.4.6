using System;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay
{
	// Token: 0x020000BC RID: 188
	public class MenuOverlay : Attribute
	{
		// Token: 0x06001294 RID: 4756 RVA: 0x0004B59B File Offset: 0x0004979B
		public MenuOverlay(string typeId)
		{
			this.TypeId = typeId;
		}

		// Token: 0x0400087A RID: 2170
		public new string TypeId;
	}
}

using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.GameMenus;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay
{
	// Token: 0x020000B9 RID: 185
	public static class GameMenuOverlayFactory
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x0004A265 File Offset: 0x00048465
		public static void RegisterProvider(IGameMenuOverlayProvider provider)
		{
			GameMenuOverlayFactory._providers.Add(provider);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0004A272 File Offset: 0x00048472
		public static void UnregisterProvider(IGameMenuOverlayProvider provider)
		{
			GameMenuOverlayFactory._providers.Remove(provider);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0004A280 File Offset: 0x00048480
		public static GameMenuOverlay GetOverlay(GameMenu.MenuOverlayType menuOverlayType)
		{
			for (int i = GameMenuOverlayFactory._providers.Count - 1; i >= 0; i--)
			{
				GameMenuOverlay overlay = GameMenuOverlayFactory._providers[i].GetOverlay(menuOverlayType);
				if (overlay != null)
				{
					return overlay;
				}
			}
			return null;
		}

		// Token: 0x0400085A RID: 2138
		private static List<IGameMenuOverlayProvider> _providers = new List<IGameMenuOverlayProvider>();
	}
}

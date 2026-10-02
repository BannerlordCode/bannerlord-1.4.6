using System;
using Helpers;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x0200039B RID: 923
	public class PartyState : PlayerGameState
	{
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x000D9DCF File Offset: 0x000D7FCF
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000D9DD2 File Offset: 0x000D7FD2
		// (set) Token: 0x06003576 RID: 13686 RVA: 0x000D9DDA File Offset: 0x000D7FDA
		public PartyScreenLogic PartyScreenLogic { get; set; }

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x000D9DE3 File Offset: 0x000D7FE3
		// (set) Token: 0x06003578 RID: 13688 RVA: 0x000D9DEB File Offset: 0x000D7FEB
		public PartyScreenHelper.PartyScreenMode PartyScreenMode { get; set; }

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06003579 RID: 13689 RVA: 0x000D9DF4 File Offset: 0x000D7FF4
		// (set) Token: 0x0600357A RID: 13690 RVA: 0x000D9DFC File Offset: 0x000D7FFC
		public bool IsDonating { get; set; }

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x0600357B RID: 13691 RVA: 0x000D9E05 File Offset: 0x000D8005
		// (set) Token: 0x0600357C RID: 13692 RVA: 0x000D9E0D File Offset: 0x000D800D
		public IPartyScreenLogicHandler Handler { get; set; }

		// Token: 0x0600357D RID: 13693 RVA: 0x000D9E16 File Offset: 0x000D8016
		public void RequestUserInput(string text, Action accept, Action cancel)
		{
			if (this.Handler != null)
			{
				this.Handler.RequestUserInput(text, accept, cancel);
			}
		}
	}
}

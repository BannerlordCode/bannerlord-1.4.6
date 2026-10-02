using System;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x0200039C RID: 924
	public interface IPartyScreenLogicHandler
	{
		// Token: 0x0600357F RID: 13695
		void RequestUserInput(string text, Action accept, Action cancel);
	}
}

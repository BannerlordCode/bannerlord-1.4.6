using System;

namespace TaleWorlds.CampaignSystem.CharacterCreationContent
{
	// Token: 0x0200020C RID: 524
	public abstract class CharacterCreationStageBase
	{
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x00090D7D File Offset: 0x0008EF7D
		// (set) Token: 0x0600200D RID: 8205 RVA: 0x00090D85 File Offset: 0x0008EF85
		public ICharacterCreationStageListener Listener { get; set; }

		// Token: 0x0600200E RID: 8206 RVA: 0x00090D8E File Offset: 0x0008EF8E
		protected internal virtual void OnFinalize()
		{
			ICharacterCreationStageListener listener = this.Listener;
			if (listener == null)
			{
				return;
			}
			listener.OnStageFinalize();
		}
	}
}

using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000389 RID: 905
	public class BarberState : GameState
	{
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000D94BB File Offset: 0x000D76BB
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060034CE RID: 13518 RVA: 0x000D94BE File Offset: 0x000D76BE
		// (set) Token: 0x060034CF RID: 13519 RVA: 0x000D94C6 File Offset: 0x000D76C6
		public IFaceGeneratorCustomFilter Filter { get; private set; }

		// Token: 0x060034D0 RID: 13520 RVA: 0x000D94CF File Offset: 0x000D76CF
		public BarberState()
		{
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000D94D7 File Offset: 0x000D76D7
		public BarberState(BasicCharacterObject character, IFaceGeneratorCustomFilter filter)
		{
			this.Character = character;
			this.Filter = filter;
		}

		// Token: 0x04000F19 RID: 3865
		public BasicCharacterObject Character;
	}
}

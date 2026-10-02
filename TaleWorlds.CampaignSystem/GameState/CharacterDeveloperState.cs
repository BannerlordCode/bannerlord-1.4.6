using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x0200038A RID: 906
	public class CharacterDeveloperState : GameState
	{
		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060034D2 RID: 13522 RVA: 0x000D94ED File Offset: 0x000D76ED
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x000D94F0 File Offset: 0x000D76F0
		// (set) Token: 0x060034D4 RID: 13524 RVA: 0x000D94F8 File Offset: 0x000D76F8
		public Hero InitialSelectedHero { get; private set; }

		// Token: 0x060034D5 RID: 13525 RVA: 0x000D9501 File Offset: 0x000D7701
		public CharacterDeveloperState()
		{
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000D9509 File Offset: 0x000D7709
		public CharacterDeveloperState(Hero initialSelectedHero)
		{
			this.InitialSelectedHero = initialSelectedHero;
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000D9518 File Offset: 0x000D7718
		// (set) Token: 0x060034D8 RID: 13528 RVA: 0x000D9520 File Offset: 0x000D7720
		public ICharacterDeveloperStateHandler Handler
		{
			get
			{
				return this._handler;
			}
			set
			{
				this._handler = value;
			}
		}

		// Token: 0x04000F1C RID: 3868
		private ICharacterDeveloperStateHandler _handler;
	}
}

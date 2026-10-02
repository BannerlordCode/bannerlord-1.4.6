using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000390 RID: 912
	public class EducationState : GameState
	{
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x000D963D File Offset: 0x000D783D
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060034F6 RID: 13558 RVA: 0x000D9640 File Offset: 0x000D7840
		// (set) Token: 0x060034F7 RID: 13559 RVA: 0x000D9648 File Offset: 0x000D7848
		public Hero Child { get; private set; }

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x000D9651 File Offset: 0x000D7851
		// (set) Token: 0x060034F9 RID: 13561 RVA: 0x000D9659 File Offset: 0x000D7859
		public IEducationStateHandler Handler
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

		// Token: 0x060034FA RID: 13562 RVA: 0x000D9662 File Offset: 0x000D7862
		public EducationState()
		{
			Debug.FailedAssert("Do not use EducationState with default constructor!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\GameState\\EducationState.cs", ".ctor", 22);
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000D9680 File Offset: 0x000D7880
		public EducationState(Hero child)
		{
			this.Child = child;
		}

		// Token: 0x04000F26 RID: 3878
		private IEducationStateHandler _handler;
	}
}

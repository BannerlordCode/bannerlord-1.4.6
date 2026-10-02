using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003FF RID: 1023
	public abstract class FormationArrangementModel : MBGameModel<FormationArrangementModel>
	{
		// Token: 0x060037B5 RID: 14261
		public abstract List<FormationArrangementModel.ArrangementPosition> GetBannerBearerPositions(Formation formation, int maxCount);

		// Token: 0x020006A2 RID: 1698
		public struct ArrangementPosition
		{
			// Token: 0x17000AFC RID: 2812
			// (get) Token: 0x060041C7 RID: 16839 RVA: 0x000FC4E2 File Offset: 0x000FA6E2
			public bool IsValid
			{
				get
				{
					return this.FileIndex > -1 && this.RankIndex > -1;
				}
			}

			// Token: 0x17000AFD RID: 2813
			// (get) Token: 0x060041C8 RID: 16840 RVA: 0x000FC4F8 File Offset: 0x000FA6F8
			public static FormationArrangementModel.ArrangementPosition Invalid
			{
				get
				{
					return default(FormationArrangementModel.ArrangementPosition);
				}
			}

			// Token: 0x060041C9 RID: 16841 RVA: 0x000FC50E File Offset: 0x000FA70E
			public ArrangementPosition(int fileIndex = -1, int rankIndex = -1)
			{
				this.FileIndex = fileIndex;
				this.RankIndex = rankIndex;
			}

			// Token: 0x040022E7 RID: 8935
			public readonly int FileIndex;

			// Token: 0x040022E8 RID: 8936
			public readonly int RankIndex;
		}
	}
}

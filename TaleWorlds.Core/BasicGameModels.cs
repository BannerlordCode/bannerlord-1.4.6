using System;
using System.Collections.Generic;

namespace TaleWorlds.Core
{
	// Token: 0x0200001C RID: 28
	public class BasicGameModels : GameModelsManager
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000699C File Offset: 0x00004B9C
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000069A4 File Offset: 0x00004BA4
		public RidingModel RidingModel { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000069AD File Offset: 0x00004BAD
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000069B5 File Offset: 0x00004BB5
		public ItemCategorySelector ItemCategorySelector { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000069BE File Offset: 0x00004BBE
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000069C6 File Offset: 0x00004BC6
		public ItemValueModel ItemValueModel { get; private set; }

		// Token: 0x0600018C RID: 396 RVA: 0x000069CF File Offset: 0x00004BCF
		public BasicGameModels(IEnumerable<GameModel> inputComponents)
			: base(inputComponents)
		{
			this.RidingModel = base.GetGameModel<RidingModel>();
			this.ItemCategorySelector = base.GetGameModel<ItemCategorySelector>();
			this.ItemValueModel = base.GetGameModel<ItemValueModel>();
		}
	}
}

using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000AD RID: 173
	public abstract class MBGameModel<T> : GameModel where T : GameModel
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0001E0E6 File Offset: 0x0001C2E6
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0001E0EE File Offset: 0x0001C2EE
		private protected T BaseModel { protected get; private set; }

		// Token: 0x06000927 RID: 2343 RVA: 0x0001E0F7 File Offset: 0x0001C2F7
		public void Initialize(T baseModel)
		{
			this.BaseModel = baseModel;
		}
	}
}

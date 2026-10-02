using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000038 RID: 56
	public class CampaignEntityComponent : IEntityComponent
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0001E7A7 File Offset: 0x0001C9A7
		void IEntityComponent.OnInitialize()
		{
			this.OnInitialize();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001E7AF File Offset: 0x0001C9AF
		void IEntityComponent.OnFinalize()
		{
			this.OnFinalize();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001E7B7 File Offset: 0x0001C9B7
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001E7B9 File Offset: 0x0001C9B9
		protected virtual void OnFinalize()
		{
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001E7BB File Offset: 0x0001C9BB
		public virtual void OnTick(float realDt, float dt)
		{
		}
	}
}

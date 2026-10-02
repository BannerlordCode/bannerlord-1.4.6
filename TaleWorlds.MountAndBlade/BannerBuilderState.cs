using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200023A RID: 570
	public class BannerBuilderState : GameState
	{
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x0007495D File Offset: 0x00072B5D
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x00074960 File Offset: 0x00072B60
		public string DefaultBannerKey { get; }

		// Token: 0x06002117 RID: 8471 RVA: 0x00074968 File Offset: 0x00072B68
		public BannerBuilderState()
		{
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00074970 File Offset: 0x00072B70
		public BannerBuilderState(string defaultBannerKey)
		{
			this.DefaultBannerKey = defaultBannerKey;
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0007497F File Offset: 0x00072B7F
		protected override void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x00074987 File Offset: 0x00072B87
		protected override void OnFinalize()
		{
			base.OnFinalize();
		}
	}
}

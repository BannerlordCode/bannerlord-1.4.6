using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200023F RID: 575
	public class InitialState : GameState
	{
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x00074A45 File Offset: 0x00072C45
		public override bool IsMusicMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600212C RID: 8492 RVA: 0x00074A48 File Offset: 0x00072C48
		// (remove) Token: 0x0600212D RID: 8493 RVA: 0x00074A80 File Offset: 0x00072C80
		public event OnInitialMenuOptionInvokedDelegate OnInitialMenuOptionInvoked;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600212E RID: 8494 RVA: 0x00074AB8 File Offset: 0x00072CB8
		// (remove) Token: 0x0600212F RID: 8495 RVA: 0x00074AF0 File Offset: 0x00072CF0
		public event OnGameContentUpdatedDelegate OnGameContentUpdated;

		// Token: 0x06002130 RID: 8496 RVA: 0x00074B25 File Offset: 0x00072D25
		protected override void OnActivate()
		{
			base.OnActivate();
			MBMusicManager mbmusicManager = MBMusicManager.Current;
			if (mbmusicManager == null)
			{
				return;
			}
			mbmusicManager.UnpauseMusicManagerSystem();
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x00074B3C File Offset: 0x00072D3C
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x00074B45 File Offset: 0x00072D45
		public void OnExecutedInitialStateOption(InitialStateOption target)
		{
			OnInitialMenuOptionInvokedDelegate onInitialMenuOptionInvoked = this.OnInitialMenuOptionInvoked;
			if (onInitialMenuOptionInvoked == null)
			{
				return;
			}
			onInitialMenuOptionInvoked(target);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x00074B58 File Offset: 0x00072D58
		public void RefreshContentState()
		{
			OnGameContentUpdatedDelegate onGameContentUpdated = this.OnGameContentUpdated;
			if (onGameContentUpdated == null)
			{
				return;
			}
			onGameContentUpdated();
		}
	}
}

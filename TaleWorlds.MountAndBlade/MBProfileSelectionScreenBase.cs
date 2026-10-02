using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.PlatformService;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200038A RID: 906
	public class MBProfileSelectionScreenBase : ScreenBase, IGameStateListener
	{
		// Token: 0x060033FE RID: 13310 RVA: 0x000D6422 File Offset: 0x000D4622
		public MBProfileSelectionScreenBase(ProfileSelectionState state)
		{
			this._state = state;
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000D6431 File Offset: 0x000D4631
		protected sealed override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (ScreenManager.TopScreen == this)
			{
				this.OnProfileSelectionTick(dt);
			}
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000D6449 File Offset: 0x000D4649
		protected virtual void OnProfileSelectionTick(float dt)
		{
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000D644B File Offset: 0x000D464B
		protected void OnActivateProfileSelection()
		{
			PlatformServices.Instance.LoginUser();
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000D6457 File Offset: 0x000D4657
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000D6459 File Offset: 0x000D4659
		void IGameStateListener.OnDeactivate()
		{
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000D645B File Offset: 0x000D465B
		void IGameStateListener.OnFinalize()
		{
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000D645D File Offset: 0x000D465D
		void IGameStateListener.OnInitialize()
		{
			Utilities.DisableGlobalLoadingWindow();
			LoadingWindow.DisableGlobalLoadingWindow();
		}

		// Token: 0x040015F0 RID: 5616
		private ProfileSelectionState _state;
	}
}

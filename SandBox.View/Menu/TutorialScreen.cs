using System;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;

namespace SandBox.View.Menu
{
	// Token: 0x0200003C RID: 60
	[GameStateScreen(typeof(TutorialState))]
	public class TutorialScreen : ScreenBase, IGameStateListener
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0001395E File Offset: 0x00011B5E
		public MenuViewContext MenuViewContext { get; }

		// Token: 0x060001F3 RID: 499 RVA: 0x00013966 File Offset: 0x00011B66
		public TutorialScreen(TutorialState tutorialState)
		{
			this.MenuViewContext = new MenuViewContext(this, tutorialState.MenuContext);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00013980 File Offset: 0x00011B80
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			this.MenuViewContext.OnFrameTick(dt);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00013995 File Offset: 0x00011B95
		protected override void OnActivate()
		{
			base.OnActivate();
			this.MenuViewContext.OnActivate();
			LoadingWindow.DisableGlobalLoadingWindow();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000139AD File Offset: 0x00011BAD
		protected override void OnDeactivate()
		{
			this.MenuViewContext.OnDeactivate();
			base.OnDeactivate();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000139C0 File Offset: 0x00011BC0
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.MenuViewContext.OnInitialize();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000139D3 File Offset: 0x00011BD3
		protected override void OnFinalize()
		{
			this.MenuViewContext.OnFinalize();
			base.OnFinalize();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000139E6 File Offset: 0x00011BE6
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000139E8 File Offset: 0x00011BE8
		void IGameStateListener.OnDeactivate()
		{
			this.MenuViewContext.OnGameStateDeactivate();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000139F5 File Offset: 0x00011BF5
		void IGameStateListener.OnInitialize()
		{
			this.MenuViewContext.OnGameStateInitialize();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00013A02 File Offset: 0x00011C02
		void IGameStateListener.OnFinalize()
		{
			this.MenuViewContext.OnGameStateFinalize();
		}
	}
}

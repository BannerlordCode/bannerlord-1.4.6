using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.Options;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000244 RID: 580
	public class ProfileSelectionState : GameState
	{
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x00075560 File Offset: 0x00073760
		// (set) Token: 0x0600216E RID: 8558 RVA: 0x00075568 File Offset: 0x00073768
		public bool IsDirectPlayPossible { get; private set; } = true;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x0600216F RID: 8559 RVA: 0x00075574 File Offset: 0x00073774
		// (remove) Token: 0x06002170 RID: 8560 RVA: 0x000755AC File Offset: 0x000737AC
		public event ProfileSelectionState.OnProfileSelectionEvent OnProfileSelection;

		// Token: 0x06002171 RID: 8561 RVA: 0x000755E1 File Offset: 0x000737E1
		public void OnProfileSelected()
		{
			NativeOptions.ReadRGLConfigFiles();
			BannerlordConfig.Initialize();
			ProfileSelectionState.OnProfileSelectionEvent onProfileSelection = this.OnProfileSelection;
			if (onProfileSelection != null)
			{
				onProfileSelection();
			}
			this.StartGame();
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x00075604 File Offset: 0x00073804
		public void StartGame()
		{
			Module.CurrentModule.SetInitialModuleScreenAsRootScreen();
		}

		// Token: 0x02000538 RID: 1336
		// (Invoke) Token: 0x06003C55 RID: 15445
		public delegate void OnProfileSelectionEvent();
	}
}

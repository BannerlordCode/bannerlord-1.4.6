using System;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E0 RID: 736
	public abstract class MultiplayerGameMode
	{
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x000A4157 File Offset: 0x000A2357
		// (set) Token: 0x06002AA8 RID: 10920 RVA: 0x000A415F File Offset: 0x000A235F
		public string Name { get; private set; }

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000A4168 File Offset: 0x000A2368
		protected MultiplayerGameMode(string name)
		{
			this.Name = name;
		}

		// Token: 0x06002AAA RID: 10922
		public abstract void JoinCustomGame(JoinGameData joinGameData);

		// Token: 0x06002AAB RID: 10923
		public abstract void StartMultiplayerGame(string scene);
	}
}

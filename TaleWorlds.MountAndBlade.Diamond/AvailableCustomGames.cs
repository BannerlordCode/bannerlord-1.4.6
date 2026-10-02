using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000E9 RID: 233
	[Serializable]
	public class AvailableCustomGames
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00005134 File Offset: 0x00003334
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0000513C File Offset: 0x0000333C
		[JsonProperty]
		public List<GameServerEntry> CustomGameServerInfos { get; private set; }

		// Token: 0x06000473 RID: 1139 RVA: 0x00005145 File Offset: 0x00003345
		public AvailableCustomGames()
		{
			this.CustomGameServerInfos = new List<GameServerEntry>();
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00005158 File Offset: 0x00003358
		public AvailableCustomGames GetCustomGamesByPermission(int playerPermission)
		{
			AvailableCustomGames availableCustomGames = new AvailableCustomGames();
			foreach (GameServerEntry gameServerEntry in this.CustomGameServerInfos)
			{
				if (gameServerEntry.Permission <= playerPermission)
				{
					availableCustomGames.CustomGameServerInfos.Add(gameServerEntry);
				}
			}
			return availableCustomGames;
		}
	}
}

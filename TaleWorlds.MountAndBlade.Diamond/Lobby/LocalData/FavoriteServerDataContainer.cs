using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x02000170 RID: 368
	public class FavoriteServerDataContainer : MultiplayerLocalDataContainer<FavoriteServerData>
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x0001086C File Offset: 0x0000EA6C
		protected override string GetSaveDirectoryName()
		{
			return "Data";
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00010873 File Offset: 0x0000EA73
		protected override string GetSaveFileName()
		{
			return "FavoriteServers.json";
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001087C File Offset: 0x0000EA7C
		public bool TryGetServerData(GameServerEntry serverEntry, out FavoriteServerData favoriteServerData)
		{
			favoriteServerData = null;
			MBReadOnlyList<FavoriteServerData> entries = base.GetEntries();
			for (int i = 0; i < entries.Count; i++)
			{
				FavoriteServerData favoriteServerData2 = entries[i];
				if (favoriteServerData2.HasSameContentWith(serverEntry))
				{
					favoriteServerData = favoriteServerData2;
					return true;
				}
			}
			return false;
		}
	}
}

using System;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x0200016F RID: 367
	public class FavoriteServerData : MultiplayerLocalData
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0001072F File Offset: 0x0000E92F
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x00010737 File Offset: 0x0000E937
		public string Address { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00010740 File Offset: 0x0000E940
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x00010748 File Offset: 0x0000E948
		public int Port { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00010751 File Offset: 0x0000E951
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x00010759 File Offset: 0x0000E959
		public string GameType { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00010762 File Offset: 0x0000E962
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x0001076A File Offset: 0x0000E96A
		public string Name { get; set; }

		// Token: 0x06000A3B RID: 2619 RVA: 0x00010773 File Offset: 0x0000E973
		private FavoriteServerData()
		{
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001077B File Offset: 0x0000E97B
		public static FavoriteServerData CreateFrom(GameServerEntry serverEntry)
		{
			if (serverEntry == null)
			{
				return null;
			}
			return new FavoriteServerData
			{
				Address = serverEntry.Address,
				Port = serverEntry.Port,
				GameType = serverEntry.GameType,
				Name = serverEntry.ServerName
			};
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000107B8 File Offset: 0x0000E9B8
		public override bool HasSameContentWith(MultiplayerLocalData other)
		{
			FavoriteServerData favoriteServerData;
			return (favoriteServerData = other as FavoriteServerData) != null && (this.Address == favoriteServerData.Address && this.Port == favoriteServerData.Port && this.GameType == favoriteServerData.GameType) && this.Name == favoriteServerData.Name;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00010818 File Offset: 0x0000EA18
		public bool HasSameContentWith(GameServerEntry serverEntry)
		{
			return this.Address == serverEntry.Address && this.Port == serverEntry.Port && this.GameType == serverEntry.GameType && this.Name == serverEntry.ServerName;
		}
	}
}

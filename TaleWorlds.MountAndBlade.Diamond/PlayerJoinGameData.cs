using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000127 RID: 295
	[Serializable]
	public class PlayerJoinGameData
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0000B74B File Offset: 0x0000994B
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x0000B753 File Offset: 0x00009953
		public PlayerData PlayerData { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0000B75C File Offset: 0x0000995C
		public PlayerId PlayerId
		{
			get
			{
				return this.PlayerData.PlayerId;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0000B769 File Offset: 0x00009969
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x0000B771 File Offset: 0x00009971
		public string Name { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0000B77A File Offset: 0x0000997A
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x0000B782 File Offset: 0x00009982
		public Guid? PartyId { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0000B78B File Offset: 0x0000998B
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x0000B793 File Offset: 0x00009993
		public Dictionary<string, List<string>> UsedCosmetics { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0000B79C File Offset: 0x0000999C
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x0000B7A4 File Offset: 0x000099A4
		[JsonProperty]
		public string IpAddress { get; private set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000B7AD File Offset: 0x000099AD
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0000B7B5 File Offset: 0x000099B5
		[JsonProperty]
		public bool IsAdmin { get; private set; }

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000B7BE File Offset: 0x000099BE
		public PlayerJoinGameData()
		{
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0000B7C6 File Offset: 0x000099C6
		public PlayerJoinGameData(PlayerData playerData, string name, Guid? partyId, Dictionary<string, List<string>> usedCosmetics, string ipAddress, bool isAdmin)
		{
			this.PlayerData = playerData;
			this.Name = name;
			this.PartyId = partyId;
			this.UsedCosmetics = usedCosmetics;
			this.IpAddress = ipAddress;
			this.IsAdmin = isAdmin;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000B7FC File Offset: 0x000099FC
		public override string ToString()
		{
			return string.Format("Player Join Game Data: {0}, name={1}, party={2}, cosmetics={3}, ip={4}, isAdmin={5}", new object[]
			{
				this.PlayerId,
				this.Name,
				this.PartyId,
				this.UsedCosmetics.Count,
				this.IpAddress,
				this.IsAdmin
			});
		}
	}
}

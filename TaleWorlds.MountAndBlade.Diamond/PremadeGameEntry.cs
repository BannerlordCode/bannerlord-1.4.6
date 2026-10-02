using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014F RID: 335
	[Serializable]
	public class PremadeGameEntry
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0000DAC1 File Offset: 0x0000BCC1
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x0000DAC9 File Offset: 0x0000BCC9
		[JsonProperty]
		public Guid Id { get; private set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0000DAD2 File Offset: 0x0000BCD2
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x0000DADA File Offset: 0x0000BCDA
		[JsonProperty]
		public string Name { get; private set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0000DAE3 File Offset: 0x0000BCE3
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x0000DAEB File Offset: 0x0000BCEB
		[JsonProperty]
		public string Region { get; private set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0000DB05 File Offset: 0x0000BD05
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x0000DB0D File Offset: 0x0000BD0D
		[JsonProperty]
		public string MapName { get; private set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0000DB16 File Offset: 0x0000BD16
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x0000DB1E File Offset: 0x0000BD1E
		[JsonProperty]
		public string FactionA { get; private set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0000DB27 File Offset: 0x0000BD27
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x0000DB2F File Offset: 0x0000BD2F
		[JsonProperty]
		public string FactionB { get; private set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0000DB38 File Offset: 0x0000BD38
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x0000DB40 File Offset: 0x0000BD40
		[JsonProperty]
		public bool IsPasswordProtected { get; private set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0000DB49 File Offset: 0x0000BD49
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0000DB51 File Offset: 0x0000BD51
		[JsonProperty]
		public PremadeGameType PremadeGameType { get; private set; }

		// Token: 0x06000969 RID: 2409 RVA: 0x0000DB5A File Offset: 0x0000BD5A
		public PremadeGameEntry()
		{
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0000DB64 File Offset: 0x0000BD64
		public PremadeGameEntry(Guid id, string name, string region, string gameType, string mapName, string factionA, string factionB, bool isPasswordProtected, PremadeGameType premadeGameType)
		{
			this.Id = id;
			this.Name = name;
			this.Region = region;
			this.GameType = gameType;
			this.MapName = mapName;
			this.FactionA = factionA;
			this.FactionB = factionB;
			this.IsPasswordProtected = isPasswordProtected;
			this.PremadeGameType = premadeGameType;
		}
	}
}

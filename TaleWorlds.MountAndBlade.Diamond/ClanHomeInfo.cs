using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000103 RID: 259
	[Serializable]
	public class ClanHomeInfo
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00006F55 File Offset: 0x00005155
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00006F5D File Offset: 0x0000515D
		[JsonProperty]
		public bool IsInClan { get; private set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00006F66 File Offset: 0x00005166
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x00006F6E File Offset: 0x0000516E
		[JsonProperty]
		public bool CanCreateClan { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00006F77 File Offset: 0x00005177
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00006F7F File Offset: 0x0000517F
		[JsonProperty]
		public ClanInfo ClanInfo { get; private set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00006F88 File Offset: 0x00005188
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x00006F90 File Offset: 0x00005190
		[JsonProperty]
		public NotEnoughPlayersInfo NotEnoughPlayersInfo { get; private set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00006F99 File Offset: 0x00005199
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00006FA1 File Offset: 0x000051A1
		[JsonProperty]
		public PlayerNotEligibleInfo[] PlayerNotEligibleInfos { get; private set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00006FAA File Offset: 0x000051AA
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00006FB2 File Offset: 0x000051B2
		[JsonProperty]
		public ClanPlayerInfo[] ClanPlayerInfos { get; private set; }

		// Token: 0x06000580 RID: 1408 RVA: 0x00006FBB File Offset: 0x000051BB
		public ClanHomeInfo(bool isInClan, bool canCreateClan, ClanInfo clanInfo, NotEnoughPlayersInfo notEnoughPlayersInfo, PlayerNotEligibleInfo[] playerNotEligibleInfos, ClanPlayerInfo[] clanPlayerInfos)
		{
			this.IsInClan = isInClan;
			this.CanCreateClan = canCreateClan;
			this.ClanInfo = clanInfo;
			this.NotEnoughPlayersInfo = notEnoughPlayersInfo;
			this.PlayerNotEligibleInfos = playerNotEligibleInfos;
			this.ClanPlayerInfos = clanPlayerInfos;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00006FF0 File Offset: 0x000051F0
		public static ClanHomeInfo CreateInClanInfo(ClanInfo clanInfo, ClanPlayerInfo[] clanPlayerInfos)
		{
			return new ClanHomeInfo(true, false, clanInfo, null, null, clanPlayerInfos);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00006FFD File Offset: 0x000051FD
		public static ClanHomeInfo CreateCanCreateClanInfo()
		{
			return new ClanHomeInfo(false, true, null, null, null, null);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000700A File Offset: 0x0000520A
		public static ClanHomeInfo CreateCantCreateClanInfo(NotEnoughPlayersInfo notEnoughPlayersInfo, PlayerNotEligibleInfo[] playerNotEligibleInfos)
		{
			return new ClanHomeInfo(false, false, null, notEnoughPlayersInfo, playerNotEligibleInfos, null);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00007017 File Offset: 0x00005217
		public static ClanHomeInfo CreateInvalidStateClanInfo()
		{
			return new ClanHomeInfo(false, false, null, null, null, null);
		}
	}
}

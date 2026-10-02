using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000124 RID: 292
	public class PartyPlayerInLobbyClient
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0000B688 File Offset: 0x00009888
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x0000B690 File Offset: 0x00009890
		public PlayerId PlayerId { get; private set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0000B699 File Offset: 0x00009899
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x0000B6A1 File Offset: 0x000098A1
		public string Name { get; private set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0000B6AA File Offset: 0x000098AA
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x0000B6B2 File Offset: 0x000098B2
		public bool WaitingInvitation { get; private set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0000B6BB File Offset: 0x000098BB
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x0000B6C3 File Offset: 0x000098C3
		public bool IsPartyLeader { get; private set; }

		// Token: 0x0600078C RID: 1932 RVA: 0x0000B6CC File Offset: 0x000098CC
		public PartyPlayerInLobbyClient(PlayerId playerId, string name, bool isPartyLeader = false)
		{
			this.PlayerId = playerId;
			this.Name = name;
			this.IsPartyLeader = isPartyLeader;
			this.WaitingInvitation = true;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public void SetAtParty()
		{
			this.WaitingInvitation = false;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000B6F9 File Offset: 0x000098F9
		public void SetLeader()
		{
			this.IsPartyLeader = true;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0000B702 File Offset: 0x00009902
		public void SetMember()
		{
			this.IsPartyLeader = false;
		}
	}
}

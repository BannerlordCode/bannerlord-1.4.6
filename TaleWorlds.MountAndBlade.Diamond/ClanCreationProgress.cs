using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	public class ClanCreationProgress
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00006EA0 File Offset: 0x000050A0
		public Progress Progress
		{
			get
			{
				int num = 0;
				int num2 = 0;
				foreach (ClanCreationPlayerData clanCreationPlayerData2 in this.ClanCreationPlayerData)
				{
					if (clanCreationPlayerData2.ClanCreationAnswer == ClanCreationAnswer.Accepted)
					{
						num++;
					}
					else if (clanCreationPlayerData2.ClanCreationAnswer == ClanCreationAnswer.Declined)
					{
						num2++;
					}
				}
				if (num == this.ClanCreationPlayerData.Length)
				{
					return Progress.Success;
				}
				if (num2 > 0)
				{
					return Progress.Fail;
				}
				return Progress.Undecided;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00006EFD File Offset: 0x000050FD
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00006F05 File Offset: 0x00005105
		public ClanCreationPlayerData[] ClanCreationPlayerData { get; private set; }

		// Token: 0x0600056E RID: 1390 RVA: 0x00006F0E File Offset: 0x0000510E
		public ClanCreationProgress(ClanCreationPlayerData[] clanCreationPlayerData)
		{
			this.ClanCreationPlayerData = clanCreationPlayerData;
		}
	}
}

using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	public class PlayerNotEligibleInfo
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000705C File Offset: 0x0000525C
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00007064 File Offset: 0x00005264
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000706D File Offset: 0x0000526D
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x00007075 File Offset: 0x00005275
		[JsonProperty]
		public PlayerNotEligibleError[] Errors { get; private set; }

		// Token: 0x0600058E RID: 1422 RVA: 0x0000707E File Offset: 0x0000527E
		public PlayerNotEligibleInfo(PlayerId playerId, PlayerNotEligibleError[] errors)
		{
			this.PlayerId = playerId;
			this.Errors = errors;
		}
	}
}

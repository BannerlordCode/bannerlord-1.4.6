using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000FD RID: 253
	[Serializable]
	public class ChatRoomInformationForClient
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00006DC5 File Offset: 0x00004FC5
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00006DCD File Offset: 0x00004FCD
		[JsonProperty]
		public Guid RoomId { get; private set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00006DD6 File Offset: 0x00004FD6
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00006DDE File Offset: 0x00004FDE
		[JsonProperty]
		public string Name { get; private set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00006DE7 File Offset: 0x00004FE7
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00006DEF File Offset: 0x00004FEF
		[JsonProperty]
		public string Endpoint { get; private set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00006DF8 File Offset: 0x00004FF8
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00006E00 File Offset: 0x00005000
		[JsonProperty]
		public string RoomColor { get; private set; }

		// Token: 0x06000560 RID: 1376 RVA: 0x00006E09 File Offset: 0x00005009
		public ChatRoomInformationForClient()
		{
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00006E11 File Offset: 0x00005011
		public ChatRoomInformationForClient(Guid roomId, string name, string endpoint, string color)
		{
			this.RoomId = roomId;
			this.Name = name;
			this.Endpoint = endpoint;
			this.RoomColor = color;
		}
	}
}

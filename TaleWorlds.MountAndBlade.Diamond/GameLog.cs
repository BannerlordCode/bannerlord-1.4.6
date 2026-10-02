using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public class GameLog
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00007E64 File Offset: 0x00006064
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x00007E6C File Offset: 0x0000606C
		public int Id { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00007E75 File Offset: 0x00006075
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x00007E7D File Offset: 0x0000607D
		public GameLogType Type { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00007E86 File Offset: 0x00006086
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x00007E8E File Offset: 0x0000608E
		public PlayerId Player { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00007E97 File Offset: 0x00006097
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00007E9F File Offset: 0x0000609F
		public float GameTime { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00007EA8 File Offset: 0x000060A8
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x00007EB0 File Offset: 0x000060B0
		public Dictionary<string, string> Data { get; set; }

		// Token: 0x06000621 RID: 1569 RVA: 0x00007EB9 File Offset: 0x000060B9
		public GameLog()
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00007EC1 File Offset: 0x000060C1
		public GameLog(GameLogType type, PlayerId player, float gameTime)
		{
			this.Type = type;
			this.Player = player;
			this.GameTime = gameTime;
			this.Data = new Dictionary<string, string>();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00007EEC File Offset: 0x000060EC
		public string GetDataAsString()
		{
			string text = "{}";
			try
			{
				text = JsonConvert.SerializeObject(this.Data, Formatting.None);
			}
			catch (Exception)
			{
			}
			return text;
		}
	}
}

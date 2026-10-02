using System;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x02000175 RID: 373
	public struct TauntIndexData
	{
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00011107 File Offset: 0x0000F307
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0001110F File Offset: 0x0000F30F
		public string TauntId { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00011118 File Offset: 0x0000F318
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x00011120 File Offset: 0x0000F320
		public int TauntIndex { get; set; }

		// Token: 0x06000A82 RID: 2690 RVA: 0x00011129 File Offset: 0x0000F329
		public TauntIndexData(string tauntId, int tauntIndex)
		{
			this.TauntId = tauntId;
			this.TauntIndex = tauntIndex;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001113C File Offset: 0x0000F33C
		public override bool Equals(object obj)
		{
			if (obj is TauntIndexData)
			{
				TauntIndexData tauntIndexData = (TauntIndexData)obj;
				return this.TauntId == tauntIndexData.TauntId && this.TauntIndex == tauntIndexData.TauntIndex;
			}
			return false;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00011184 File Offset: 0x0000F384
		public override int GetHashCode()
		{
			return (this.TauntId.GetHashCode() * 397) ^ this.TauntIndex.GetHashCode();
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000111B1 File Offset: 0x0000F3B1
		public static bool operator ==(TauntIndexData first, TauntIndexData second)
		{
			return first.TauntId == second.TauntId && first.TauntIndex == second.TauntIndex;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000111DA File Offset: 0x0000F3DA
		public static bool operator !=(TauntIndexData first, TauntIndexData second)
		{
			return !(first == second);
		}
	}
}

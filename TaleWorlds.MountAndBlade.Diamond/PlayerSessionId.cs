using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000141 RID: 321
	[Serializable]
	public struct PlayerSessionId
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0000CA9A File Offset: 0x0000AC9A
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0000CAA2 File Offset: 0x0000ACA2
		[JsonProperty]
		public Guid Guid
		{
			get
			{
				return this._guid;
			}
			private set
			{
				this._guid = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0000CAAB File Offset: 0x0000ACAB
		public SessionKey SessionKey
		{
			get
			{
				return new SessionKey(this._guid);
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		public PlayerSessionId(Guid guid)
		{
			this._guid = guid;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0000CAC1 File Offset: 0x0000ACC1
		public PlayerSessionId(SessionKey sessionKey)
		{
			this._guid = sessionKey.Guid;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		public static PlayerSessionId NewGuid()
		{
			return new PlayerSessionId(Guid.NewGuid());
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0000CADC File Offset: 0x0000ACDC
		public override string ToString()
		{
			return this._guid.ToString();
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0000CAEF File Offset: 0x0000ACEF
		public byte[] ToByteArray()
		{
			return this._guid.ToByteArray();
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		public static bool operator ==(PlayerSessionId a, PlayerSessionId b)
		{
			return a._guid == b._guid;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0000CB0F File Offset: 0x0000AD0F
		public static bool operator !=(PlayerSessionId a, PlayerSessionId b)
		{
			return a._guid != b._guid;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public override bool Equals(object o)
		{
			return o != null && o is PlayerSessionId && this._guid.Equals(((PlayerSessionId)o).Guid);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000CB57 File Offset: 0x0000AD57
		public override int GetHashCode()
		{
			return this._guid.GetHashCode();
		}

		// Token: 0x040003A5 RID: 933
		private Guid _guid;
	}
}

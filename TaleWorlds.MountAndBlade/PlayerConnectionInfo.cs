using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000321 RID: 801
	public class PlayerConnectionInfo
	{
		// Token: 0x06002D8D RID: 11661 RVA: 0x000AFF61 File Offset: 0x000AE161
		public PlayerConnectionInfo(PlayerId playerID)
		{
			this.PlayerID = playerID;
			this._parameters = new Dictionary<string, object>();
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000AFF7B File Offset: 0x000AE17B
		public void AddParameter(string name, object parameter)
		{
			if (!this._parameters.ContainsKey(name))
			{
				this._parameters.Add(name, parameter);
			}
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000AFF98 File Offset: 0x000AE198
		public T GetParameter<T>(string name) where T : class
		{
			if (this._parameters.ContainsKey(name))
			{
				return this._parameters[name] as T;
			}
			return default(T);
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000AFFD3 File Offset: 0x000AE1D3
		// (set) Token: 0x06002D91 RID: 11665 RVA: 0x000AFFDB File Offset: 0x000AE1DB
		public int SessionKey { get; set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x000AFFE4 File Offset: 0x000AE1E4
		// (set) Token: 0x06002D93 RID: 11667 RVA: 0x000AFFEC File Offset: 0x000AE1EC
		public string Name { get; set; }

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000AFFF5 File Offset: 0x000AE1F5
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x000AFFFD File Offset: 0x000AE1FD
		public NetworkCommunicator NetworkPeer { get; set; }

		// Token: 0x040011EE RID: 4590
		private Dictionary<string, object> _parameters;

		// Token: 0x040011F2 RID: 4594
		public readonly PlayerId PlayerID;
	}
}

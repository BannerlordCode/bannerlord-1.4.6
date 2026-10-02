using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000AA RID: 170
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class InitializeSession : LoginMessage
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002FA RID: 762 RVA: 0x000040A7 File Offset: 0x000022A7
		// (set) Token: 0x060002FB RID: 763 RVA: 0x000040AF File Offset: 0x000022AF
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000040B8 File Offset: 0x000022B8
		// (set) Token: 0x060002FD RID: 765 RVA: 0x000040C0 File Offset: 0x000022C0
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000040C9 File Offset: 0x000022C9
		// (set) Token: 0x060002FF RID: 767 RVA: 0x000040D1 File Offset: 0x000022D1
		[JsonProperty]
		public ApplicationVersion ApplicationVersion { get; private set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000040DA File Offset: 0x000022DA
		// (set) Token: 0x06000301 RID: 769 RVA: 0x000040E2 File Offset: 0x000022E2
		[JsonProperty]
		public string ConnectionPassword { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000040EB File Offset: 0x000022EB
		// (set) Token: 0x06000303 RID: 771 RVA: 0x000040F3 File Offset: 0x000022F3
		[JsonProperty]
		public ModuleInfoModel[] LoadedModules { get; private set; }

		// Token: 0x06000304 RID: 772 RVA: 0x000040FC File Offset: 0x000022FC
		public InitializeSession()
		{
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00004104 File Offset: 0x00002304
		public InitializeSession(PlayerId playerId, string playerName, AccessObject accessObject, ApplicationVersion applicationVersion, string connectionPassword, ModuleInfoModel[] loadedModules)
			: base(playerId.ConvertToPeerId(), accessObject)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.ApplicationVersion = applicationVersion;
			this.ConnectionPassword = connectionPassword;
			this.LoadedModules = loadedModules;
		}
	}
}

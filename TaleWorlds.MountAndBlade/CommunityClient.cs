using System;
using TaleWorlds.Library.Http;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E4 RID: 740
	public class CommunityClient
	{
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x000A48ED File Offset: 0x000A2AED
		// (set) Token: 0x06002ACB RID: 10955 RVA: 0x000A48F5 File Offset: 0x000A2AF5
		public bool IsInGame { get; private set; }

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x000A48FE File Offset: 0x000A2AFE
		// (set) Token: 0x06002ACD RID: 10957 RVA: 0x000A4906 File Offset: 0x000A2B06
		public ICommunityClientHandler Handler { get; set; }

		// Token: 0x06002ACE RID: 10958 RVA: 0x000A490F File Offset: 0x000A2B0F
		public CommunityClient()
		{
			this._httpDriver = HttpDriverManager.GetDefaultHttpDriver();
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000A4922 File Offset: 0x000A2B22
		public void QuitFromGame()
		{
			if (this.IsInGame)
			{
				this.IsInGame = false;
				ICommunityClientHandler handler = this.Handler;
				if (handler == null)
				{
					return;
				}
				handler.OnQuitFromGame();
			}
		}

		// Token: 0x04001047 RID: 4167
		private IHttpDriver _httpDriver;
	}
}

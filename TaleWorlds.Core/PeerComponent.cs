using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000C3 RID: 195
	public abstract class PeerComponent : IEntityComponent
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00023058 File Offset: 0x00021258
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x00023060 File Offset: 0x00021260
		public VirtualPlayer Peer
		{
			get
			{
				return this._peer;
			}
			set
			{
				this._peer = value;
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00023069 File Offset: 0x00021269
		public virtual void Initialize()
		{
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0002306B File Offset: 0x0002126B
		public string Name
		{
			get
			{
				return this.Peer.UserName;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00023078 File Offset: 0x00021278
		public bool IsMine
		{
			get
			{
				return this.Peer.IsMine;
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00023085 File Offset: 0x00021285
		public T GetComponent<T>() where T : PeerComponent
		{
			return this.Peer.GetComponent<T>();
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00023092 File Offset: 0x00021292
		public virtual void OnInitialize()
		{
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00023094 File Offset: 0x00021294
		public virtual void OnFinalize()
		{
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00023096 File Offset: 0x00021296
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0002309E File Offset: 0x0002129E
		public uint TypeId { get; set; }

		// Token: 0x040005FD RID: 1533
		private VirtualPlayer _peer;
	}
}

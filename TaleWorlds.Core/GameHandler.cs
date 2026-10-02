using System;

namespace TaleWorlds.Core
{
	// Token: 0x0200006A RID: 106
	public abstract class GameHandler : IEntityComponent
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x00019E91 File Offset: 0x00018091
		void IEntityComponent.OnInitialize()
		{
			this.OnInitialize();
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00019E99 File Offset: 0x00018099
		void IEntityComponent.OnFinalize()
		{
			this.OnFinalize();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00019EA1 File Offset: 0x000180A1
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00019EA3 File Offset: 0x000180A3
		protected virtual void OnFinalize()
		{
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00019EA5 File Offset: 0x000180A5
		protected internal virtual void OnTick(float dt)
		{
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00019EA7 File Offset: 0x000180A7
		protected internal virtual void OnGameStart()
		{
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00019EA9 File Offset: 0x000180A9
		protected internal virtual void OnGameEnd()
		{
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00019EAB File Offset: 0x000180AB
		protected internal virtual void OnGameNetworkBegin()
		{
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00019EAD File Offset: 0x000180AD
		protected internal virtual void OnGameNetworkEnd()
		{
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00019EAF File Offset: 0x000180AF
		protected internal virtual void OnEarlyPlayerConnect(VirtualPlayer peer)
		{
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00019EB1 File Offset: 0x000180B1
		protected internal virtual void OnPlayerConnect(VirtualPlayer peer)
		{
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00019EB3 File Offset: 0x000180B3
		protected internal virtual void OnPlayerDisconnect(VirtualPlayer peer)
		{
		}

		// Token: 0x060007A9 RID: 1961
		public abstract void OnBeforeSave();

		// Token: 0x060007AA RID: 1962
		public abstract void OnAfterSave();
	}
}

using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200036A RID: 874
	public abstract class UsableMissionObjectComponent
	{
		// Token: 0x06003233 RID: 12851 RVA: 0x000CD1A4 File Offset: 0x000CB3A4
		protected internal virtual void OnAdded(Scene scene)
		{
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000CD1A6 File Offset: 0x000CB3A6
		protected internal virtual void OnRemoved()
		{
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x000CD1A8 File Offset: 0x000CB3A8
		protected internal virtual void OnFocusGain(Agent userAgent)
		{
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000CD1AA File Offset: 0x000CB3AA
		protected internal virtual void OnFocusLose(Agent userAgent)
		{
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000CD1AC File Offset: 0x000CB3AC
		public virtual bool IsOnTickRequired()
		{
			return false;
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000CD1AF File Offset: 0x000CB3AF
		protected internal virtual void OnTick(float dt)
		{
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000CD1B1 File Offset: 0x000CB3B1
		protected internal virtual void OnEditorTick(float dt)
		{
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000CD1B3 File Offset: 0x000CB3B3
		protected internal virtual void OnEditorValidate()
		{
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000CD1B5 File Offset: 0x000CB3B5
		protected internal virtual void OnUse(Agent userAgent)
		{
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000CD1B7 File Offset: 0x000CB3B7
		protected internal virtual void OnUseStopped(Agent userAgent, bool isSuccessful = true)
		{
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000CD1B9 File Offset: 0x000CB3B9
		protected internal virtual void OnMissionReset()
		{
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000CD1BB File Offset: 0x000CB3BB
		protected internal virtual void OnMissionObjectDisabled()
		{
		}
	}
}

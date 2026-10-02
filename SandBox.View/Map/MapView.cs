using System;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.View.Map
{
	// Token: 0x0200005B RID: 91
	public abstract class MapView : SandboxView
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0001C8C3 File Offset: 0x0001AAC3
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0001C8CB File Offset: 0x0001AACB
		public MapScreen MapScreen { get; internal set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0001C8D4 File Offset: 0x0001AAD4
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0001C8DC File Offset: 0x0001AADC
		public MapState MapState { get; internal set; }

		// Token: 0x06000381 RID: 897 RVA: 0x0001C8E5 File Offset: 0x0001AAE5
		protected internal virtual void CreateLayout()
		{
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001C8E7 File Offset: 0x0001AAE7
		protected internal virtual void OnResume()
		{
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001C8E9 File Offset: 0x0001AAE9
		protected internal virtual void OnHourlyTick()
		{
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001C8EB File Offset: 0x0001AAEB
		protected internal virtual void OnStartWait(string waitMenuId)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001C8ED File Offset: 0x0001AAED
		protected internal virtual void OnMainPartyEncounter()
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001C8EF File Offset: 0x0001AAEF
		protected internal virtual void OnDispersePlayerLeadedArmy()
		{
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001C8F1 File Offset: 0x0001AAF1
		protected internal virtual void OnArmyLeft()
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001C8F3 File Offset: 0x0001AAF3
		protected internal virtual bool IsEscaped()
		{
			return false;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001C8F6 File Offset: 0x0001AAF6
		protected internal virtual bool IsOpeningEscapeMenuOnFocusChangeAllowed()
		{
			return true;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001C8F9 File Offset: 0x0001AAF9
		protected internal virtual void OnOverlayCreated()
		{
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001C8FB File Offset: 0x0001AAFB
		protected internal virtual void OnOverlayClosed()
		{
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001C8FD File Offset: 0x0001AAFD
		protected internal virtual void OnMenuModeTick(float dt)
		{
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001C8FF File Offset: 0x0001AAFF
		protected internal virtual void OnMapScreenUpdate(float dt)
		{
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001C901 File Offset: 0x0001AB01
		protected internal virtual void OnIdleTick(float dt)
		{
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001C903 File Offset: 0x0001AB03
		protected internal virtual void OnMapTerrainClick()
		{
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001C905 File Offset: 0x0001AB05
		protected internal virtual void OnSiegeEngineClick(MatrixFrame siegeEngineFrame)
		{
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001C907 File Offset: 0x0001AB07
		protected internal virtual void OnMapConversationStart()
		{
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001C909 File Offset: 0x0001AB09
		protected internal virtual void OnMapConversationOver()
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001C90B File Offset: 0x0001AB0B
		protected internal virtual TutorialContexts GetTutorialContext()
		{
			return TutorialContexts.MapWindow;
		}

		// Token: 0x040001D5 RID: 469
		protected const float ContextAlphaModifier = 8.5f;
	}
}

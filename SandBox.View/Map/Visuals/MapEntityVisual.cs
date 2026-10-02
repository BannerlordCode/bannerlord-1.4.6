using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace SandBox.View.Map.Visuals
{
	// Token: 0x02000060 RID: 96
	public abstract class MapEntityVisual
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001DF68 File Offset: 0x0001C168
		public MapScreen MapScreen
		{
			get
			{
				return MapScreen.Instance;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060003C5 RID: 965
		public abstract CampaignVec2 InteractionPositionForPlayer { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060003C6 RID: 966
		public abstract MapEntityVisual AttachedTo { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0001DF6F File Offset: 0x0001C16F
		public virtual bool IsMobileEntity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001DF72 File Offset: 0x0001C172
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0001DF7A File Offset: 0x0001C17A
		public virtual MatrixFrame CircleLocalFrame { get; protected set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001DF83 File Offset: 0x0001C183
		public virtual bool IsMainEntity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0001DF86 File Offset: 0x0001C186
		public virtual float BearingRotation { get; }

		// Token: 0x060003CC RID: 972
		public abstract bool OnMapClick(bool followModifierUsed);

		// Token: 0x060003CD RID: 973
		public abstract void OnHover();

		// Token: 0x060003CE RID: 974
		public abstract void OnOpenEncyclopedia();

		// Token: 0x060003CF RID: 975
		public abstract bool IsVisibleOrFadingOut();

		// Token: 0x060003D0 RID: 976
		public abstract Vec3 GetVisualPosition();

		// Token: 0x060003D1 RID: 977 RVA: 0x0001DF8E File Offset: 0x0001C18E
		public virtual void ReleaseResources()
		{
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001DF90 File Offset: 0x0001C190
		public virtual void OnHoverEnd()
		{
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001DF92 File Offset: 0x0001C192
		public virtual void OnTrackAction()
		{
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001DF94 File Offset: 0x0001C194
		public virtual bool IsEnemyOf(IFaction faction)
		{
			return false;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001DF97 File Offset: 0x0001C197
		public virtual bool IsAllyOf(IFaction faction)
		{
			return false;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001DF9A File Offset: 0x0001C19A
		public virtual bool IsInSameFaction(IFaction faction)
		{
			return false;
		}
	}
}

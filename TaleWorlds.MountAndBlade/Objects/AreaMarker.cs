using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Objects
{
	// Token: 0x0200039E RID: 926
	public class AreaMarker : MissionObject, ITrackableBase
	{
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x000D94D1 File Offset: 0x000D76D1
		public virtual string Tag
		{
			get
			{
				return "area_marker_" + this.AreaIndex;
			}
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000D94E8 File Offset: 0x000D76E8
		protected internal override void OnEditorTick(float dt)
		{
			if (this.CheckToggle)
			{
				MBEditor.HelpersEnabled();
			}
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000D94F8 File Offset: 0x000D76F8
		protected internal override void OnEditorInit()
		{
			this.CheckToggle = false;
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000D9504 File Offset: 0x000D7704
		public bool IsPositionInRange(Vec3 position)
		{
			return position.DistanceSquared(base.GameEntity.GlobalPosition) <= this.AreaRadius * this.AreaRadius;
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000D9538 File Offset: 0x000D7738
		public virtual List<UsableMachine> GetUsableMachinesInRange(string excludeTag = null)
		{
			float distanceSquared = this.AreaRadius * this.AreaRadius;
			return (from x in Mission.Current.ActiveMissionObjects.FindAllWithType<UsableMachine>()
				where !x.IsDeactivated && x.GameEntity.GlobalPosition.DistanceSquared(this.GameEntity.GlobalPosition) <= distanceSquared && !x.GameEntity.HasTag(excludeTag)
				select x).ToList<UsableMachine>();
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000D9594 File Offset: 0x000D7794
		public virtual List<UsableMachine> GetUsableMachinesWithTagInRange(string tag)
		{
			float distanceSquared = this.AreaRadius * this.AreaRadius;
			return (from x in Mission.Current.ActiveMissionObjects.FindAllWithType<UsableMachine>()
				where x.GameEntity.GlobalPosition.DistanceSquared(this.GameEntity.GlobalPosition) <= distanceSquared && x.GameEntity.HasTag(tag)
				select x).ToList<UsableMachine>();
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000D95F0 File Offset: 0x000D77F0
		public virtual List<GameEntity> GetGameEntitiesWithTagInRange(string tag)
		{
			float distanceSquared = this.AreaRadius * this.AreaRadius;
			return (from x in Mission.Current.Scene.FindEntitiesWithTag(tag)
				where x.GlobalPosition.DistanceSquared(this.GameEntity.GlobalPosition) <= distanceSquared && x.HasTag(tag)
				select x).ToList<GameEntity>();
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000D9650 File Offset: 0x000D7850
		public virtual TextObject GetName()
		{
			return new TextObject(base.GameEntity.Name, null);
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000D9674 File Offset: 0x000D7874
		public virtual Vec3 GetPosition()
		{
			return base.GameEntity.GlobalPosition;
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000D968F File Offset: 0x000D788F
		TextObject ITrackableBase.GetName()
		{
			return this.GetName();
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000D9697 File Offset: 0x000D7897
		Vec3 ITrackableBase.GetPosition()
		{
			return this.GetPosition();
		}

		// Token: 0x04001668 RID: 5736
		public float AreaRadius = 3f;

		// Token: 0x04001669 RID: 5737
		public int AreaIndex;

		// Token: 0x0400166A RID: 5738
		public bool CheckToggle;
	}
}

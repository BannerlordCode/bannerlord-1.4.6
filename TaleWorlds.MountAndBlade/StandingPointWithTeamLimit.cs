using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200035A RID: 858
	public class StandingPointWithTeamLimit : StandingPoint
	{
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x000C7EB5 File Offset: 0x000C60B5
		// (set) Token: 0x0600313B RID: 12603 RVA: 0x000C7EBD File Offset: 0x000C60BD
		public Team UsableTeam { get; set; }

		// Token: 0x0600313C RID: 12604 RVA: 0x000C7EC6 File Offset: 0x000C60C6
		public override bool IsDisabledForAgent(Agent agent)
		{
			return agent.Team != this.UsableTeam || base.IsDisabledForAgent(agent);
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000C7EDF File Offset: 0x000C60DF
		protected internal override bool IsUsableBySide(BattleSideEnum side)
		{
			return side == this.UsableTeam.Side && base.IsUsableBySide(side);
		}
	}
}

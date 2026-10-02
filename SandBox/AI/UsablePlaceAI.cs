using System;
using TaleWorlds.MountAndBlade;

namespace SandBox.AI
{
	// Token: 0x0200010D RID: 269
	public class UsablePlaceAI : UsableMachineAIBase
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x00061410 File Offset: 0x0005F610
		public UsablePlaceAI(UsableMachine usableMachine)
			: base(usableMachine)
		{
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0006141C File Offset: 0x0005F61C
		protected override Agent.AIScriptedFrameFlags GetScriptedFrameFlags(Agent agent)
		{
			if (!this.UsableMachine.GameEntity.HasTag("quest_wanderer_target"))
			{
				return Agent.AIScriptedFrameFlags.DoNotRun;
			}
			return Agent.AIScriptedFrameFlags.None;
		}
	}
}

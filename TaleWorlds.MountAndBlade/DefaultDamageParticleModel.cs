using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001FB RID: 507
	public class DefaultDamageParticleModel : DamageParticleModel
	{
		// Token: 0x06001DA4 RID: 7588 RVA: 0x000657F0 File Offset: 0x000639F0
		public DefaultDamageParticleModel()
		{
			this._bloodStartHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_blood_sword_enter");
			this._bloodContinueHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_blood_sword_inside");
			this._bloodEndHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_blood_sword_exit");
			this._sweatStartHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_sweat_sword_enter");
			this._sweatContinueHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_sweat_sword_enter");
			this._sweatEndHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_sweat_sword_enter");
			this._missileHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_blood_sword_enter");
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x000658A4 File Offset: 0x00063AA4
		public override void GetMeleeAttackBloodParticles(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData, out HitParticleResultData particleResultData)
		{
			particleResultData.StartHitParticleIndex = this._bloodStartHitParticleIndex;
			particleResultData.ContinueHitParticleIndex = this._bloodContinueHitParticleIndex;
			particleResultData.EndHitParticleIndex = this._bloodEndHitParticleIndex;
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x000658CD File Offset: 0x00063ACD
		public override void GetMeleeAttackSweatParticles(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData, out HitParticleResultData particleResultData)
		{
			particleResultData.StartHitParticleIndex = this._sweatStartHitParticleIndex;
			particleResultData.ContinueHitParticleIndex = this._sweatContinueHitParticleIndex;
			particleResultData.EndHitParticleIndex = this._sweatEndHitParticleIndex;
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x000658F6 File Offset: 0x00063AF6
		public override int GetMissileAttackParticle(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData)
		{
			return this._missileHitParticleIndex;
		}

		// Token: 0x04000A3A RID: 2618
		private int _bloodStartHitParticleIndex = -1;

		// Token: 0x04000A3B RID: 2619
		private int _bloodContinueHitParticleIndex = -1;

		// Token: 0x04000A3C RID: 2620
		private int _bloodEndHitParticleIndex = -1;

		// Token: 0x04000A3D RID: 2621
		private int _sweatStartHitParticleIndex = -1;

		// Token: 0x04000A3E RID: 2622
		private int _sweatContinueHitParticleIndex = -1;

		// Token: 0x04000A3F RID: 2623
		private int _sweatEndHitParticleIndex = -1;

		// Token: 0x04000A40 RID: 2624
		private int _missileHitParticleIndex = -1;
	}
}

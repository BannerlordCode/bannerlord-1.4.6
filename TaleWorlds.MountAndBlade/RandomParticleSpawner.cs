using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000339 RID: 825
	public class RandomParticleSpawner : ScriptComponentBehavior
	{
		// Token: 0x06002E3E RID: 11838 RVA: 0x000B29F6 File Offset: 0x000B0BF6
		private void InitScript()
		{
			this._timeUntilNextParticleSpawn = this.spawnInterval;
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000B2A04 File Offset: 0x000B0C04
		private void CheckSpawnParticle(float dt)
		{
			this._timeUntilNextParticleSpawn -= dt;
			if (this._timeUntilNextParticleSpawn <= 0f)
			{
				int childCount = base.GameEntity.ChildCount;
				if (childCount > 0)
				{
					int num = MBRandom.RandomInt(childCount);
					WeakGameEntity child = base.GameEntity.GetChild(num);
					int componentCount = child.GetComponentCount(TaleWorlds.Engine.GameEntity.ComponentType.ParticleSystemInstanced);
					for (int i = 0; i < componentCount; i++)
					{
						((ParticleSystem)child.GetComponentAtIndex(i, TaleWorlds.Engine.GameEntity.ComponentType.ParticleSystemInstanced)).Restart();
					}
				}
				this._timeUntilNextParticleSpawn += this.spawnInterval;
			}
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000B2A98 File Offset: 0x000B0C98
		protected internal override void OnInit()
		{
			base.OnInit();
			this.InitScript();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000B2AB2 File Offset: 0x000B0CB2
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.OnInit();
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000B2AC0 File Offset: 0x000B0CC0
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000B2ACA File Offset: 0x000B0CCA
		protected internal override void OnTick(float dt)
		{
			this.CheckSpawnParticle(dt);
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000B2AD3 File Offset: 0x000B0CD3
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this.CheckSpawnParticle(dt);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000B2AE3 File Offset: 0x000B0CE3
		protected internal override bool MovesEntity()
		{
			return true;
		}

		// Token: 0x0400125B RID: 4699
		private float _timeUntilNextParticleSpawn;

		// Token: 0x0400125C RID: 4700
		public float spawnInterval = 3f;
	}
}

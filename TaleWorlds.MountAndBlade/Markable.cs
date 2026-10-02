using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200034B RID: 843
	public class Markable : ScriptComponentBehavior
	{
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000BB976 File Offset: 0x000B9B76
		// (set) Token: 0x06002F9A RID: 12186 RVA: 0x000BB97E File Offset: 0x000B9B7E
		private bool MarkerActive
		{
			get
			{
				return this._markerActive;
			}
			set
			{
				if (this._markerActive != value)
				{
					this._markerActive = value;
					base.SetScriptComponentToTick(this.GetTickRequirement());
				}
			}
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000BB99C File Offset: 0x000B9B9C
		protected internal override void OnInit()
		{
			base.OnInit();
			this._marker = TaleWorlds.Engine.GameEntity.Instantiate(Mission.Current.Scene, "highlight_beam", base.GameEntity.GetGlobalFrame(), true);
			this.DeactivateMarker();
			this._destructibleComponent = base.GameEntity.GetFirstScriptOfType<DestructableComponent>();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000BB9FE File Offset: 0x000B9BFE
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (this.MarkerActive)
			{
				return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000BBA18 File Offset: 0x000B9C18
		protected internal override void OnTick(float dt)
		{
			if (this.MarkerActive)
			{
				if (this._destructibleComponent != null && this._destructibleComponent.IsDestroyed)
				{
					if (this._markerVisible)
					{
						this.DisableMarkerActivation();
						return;
					}
				}
				else if (this._markerVisible)
				{
					if (Mission.Current.CurrentTime - this._markerEventBeginningTime > this._markerActiveDuration)
					{
						this.DeactivateMarker();
						return;
					}
				}
				else if (!this._markerVisible && Mission.Current.CurrentTime - this._markerEventBeginningTime > this._markerPassiveDuration)
				{
					this.ActivateMarkerFor(this._markerActiveDuration, this._markerPassiveDuration);
				}
			}
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000BBAAC File Offset: 0x000B9CAC
		public void DisableMarkerActivation()
		{
			this.MarkerActive = false;
			this.DeactivateMarker();
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000BBABC File Offset: 0x000B9CBC
		public void ActivateMarkerFor(float activeSeconds, float passiveSeconds)
		{
			if (this._destructibleComponent == null || !this._destructibleComponent.IsDestroyed)
			{
				this.MarkerActive = true;
				this._markerVisible = true;
				this._markerEventBeginningTime = Mission.Current.CurrentTime;
				this._markerActiveDuration = activeSeconds;
				this._markerPassiveDuration = passiveSeconds;
				this._marker.SetVisibilityExcludeParents(true);
				this._marker.BurstEntityParticle(true);
			}
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000BBB22 File Offset: 0x000B9D22
		private void DeactivateMarker()
		{
			this._markerVisible = false;
			this._marker.SetVisibilityExcludeParents(false);
			this._markerEventBeginningTime = Mission.Current.CurrentTime;
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000BBB47 File Offset: 0x000B9D47
		public void ResetPassiveDurationTimer()
		{
			if (!this._markerVisible && this.MarkerActive)
			{
				this._markerEventBeginningTime = Mission.Current.CurrentTime;
			}
		}

		// Token: 0x04001376 RID: 4982
		public string MarkerPrefabName = "highlight_beam";

		// Token: 0x04001377 RID: 4983
		private GameEntity _marker;

		// Token: 0x04001378 RID: 4984
		private DestructableComponent _destructibleComponent;

		// Token: 0x04001379 RID: 4985
		private bool _markerActive;

		// Token: 0x0400137A RID: 4986
		private bool _markerVisible;

		// Token: 0x0400137B RID: 4987
		private float _markerEventBeginningTime;

		// Token: 0x0400137C RID: 4988
		private float _markerActiveDuration;

		// Token: 0x0400137D RID: 4989
		private float _markerPassiveDuration;
	}
}

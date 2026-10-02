using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002DF RID: 735
	public class MPPerksAgentComponent : AgentComponent
	{
		// Token: 0x06002A9F RID: 10911 RVA: 0x000A3F60 File Offset: 0x000A2160
		public MPPerksAgentComponent(Agent agent)
			: base(agent)
		{
			this.Agent.OnAgentHealthChanged += this.OnHealthChanged;
			if (this.Agent.HasMount)
			{
				this.Agent.MountAgent.OnAgentHealthChanged += this.OnMountHealthChanged;
			}
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000A3FB4 File Offset: 0x000A21B4
		public override void OnMount(Agent mount)
		{
			mount.OnAgentHealthChanged += this.OnMountHealthChanged;
			mount.UpdateAgentProperties();
			MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
			if (perkHandler == null)
			{
				return;
			}
			perkHandler.OnEvent(this.Agent, MPPerkCondition.PerkEventFlags.MountChange);
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000A3FEE File Offset: 0x000A21EE
		public override void OnDismount(Agent mount)
		{
			mount.OnAgentHealthChanged -= this.OnMountHealthChanged;
			mount.UpdateAgentProperties();
			MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
			if (perkHandler == null)
			{
				return;
			}
			perkHandler.OnEvent(this.Agent, MPPerkCondition.PerkEventFlags.MountChange);
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000A4028 File Offset: 0x000A2228
		public override void OnItemPickup(SpawnedItemEntity item)
		{
			if (!item.WeaponCopy.IsEmpty && item.WeaponCopy.Item.ItemType == ItemObject.ItemTypeEnum.Banner)
			{
				MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
				if (perkHandler == null)
				{
					return;
				}
				perkHandler.OnEvent(MPPerkCondition.PerkEventFlags.BannerPickUp);
			}
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000A4073 File Offset: 0x000A2273
		public override void OnWeaponDrop(MissionWeapon droppedWeapon)
		{
			if (!droppedWeapon.IsEmpty && droppedWeapon.Item.ItemType == ItemObject.ItemTypeEnum.Banner)
			{
				MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
				if (perkHandler == null)
				{
					return;
				}
				perkHandler.OnEvent(MPPerkCondition.PerkEventFlags.BannerDrop);
			}
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000A40A8 File Offset: 0x000A22A8
		public override void OnAgentRemoved()
		{
			if (this.Agent.HasMount)
			{
				this.Agent.MountAgent.OnAgentHealthChanged -= this.OnMountHealthChanged;
				this.Agent.MountAgent.UpdateAgentProperties();
			}
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000A40E3 File Offset: 0x000A22E3
		private void OnHealthChanged(Agent agent, float oldHealth, float newHealth)
		{
			MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
			if (perkHandler == null)
			{
				return;
			}
			perkHandler.OnEvent(agent, MPPerkCondition.PerkEventFlags.HealthChange);
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000A40FC File Offset: 0x000A22FC
		private void OnMountHealthChanged(Agent agent, float oldHealth, float newHealth)
		{
			if (!this.Agent.IsActive() || this.Agent.MountAgent != agent)
			{
				agent.OnAgentHealthChanged -= this.OnMountHealthChanged;
				return;
			}
			MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
			if (perkHandler == null)
			{
				return;
			}
			perkHandler.OnEvent(this.Agent, MPPerkCondition.PerkEventFlags.MountHealthChange);
		}
	}
}

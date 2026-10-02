using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.Network.Gameplay.Perks.Conditions
{
	// Token: 0x020003C1 RID: 961
	public class BannerBearerCondition : MPPerkCondition
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060035EC RID: 13804 RVA: 0x000DE25D File Offset: 0x000DC45D
		public override MPPerkCondition.PerkEventFlags EventFlags
		{
			get
			{
				return MPPerkCondition.PerkEventFlags.AliveBotCountChange | MPPerkCondition.PerkEventFlags.BannerPickUp | MPPerkCondition.PerkEventFlags.BannerDrop | MPPerkCondition.PerkEventFlags.SpawnEnd;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x000DE264 File Offset: 0x000DC464
		public override bool IsPeerCondition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000DE267 File Offset: 0x000DC467
		protected BannerBearerCondition()
		{
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000DE26F File Offset: 0x000DC46F
		protected override void Deserialize(XmlNode node)
		{
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000DE274 File Offset: 0x000DC474
		public override bool Check(MissionPeer peer)
		{
			Formation formation = ((peer != null) ? peer.ControlledFormation : null);
			if (formation != null && MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0)
			{
				using (List<IFormationUnit>.Enumerator enumerator = formation.Arrangement.GetAllUnits().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Agent agent;
						if ((agent = enumerator.Current as Agent) != null && agent.IsActive())
						{
							MissionWeapon missionWeapon = agent.Equipment[EquipmentIndex.ExtraWeaponSlot];
							if (!missionWeapon.IsEmpty && missionWeapon.Item.ItemType == ItemObject.ItemTypeEnum.Banner && new Banner(formation.BannerCode, peer.Team.Color, peer.Team.Color2).Serialize() == missionWeapon.Banner.Serialize())
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000DE364 File Offset: 0x000DC564
		public override bool Check(Agent agent)
		{
			agent = ((agent != null && agent.IsMount) ? agent.RiderAgent : agent);
			MissionPeer missionPeer = ((agent != null) ? agent.MissionPeer : null) ?? ((agent != null) ? agent.OwningAgentMissionPeer : null);
			return this.Check(missionPeer);
		}

		// Token: 0x0400170E RID: 5902
		protected static string StringType = "BannerBearer";
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000136 RID: 310
	public class DefaultPartyNavigationModel : PartyNavigationModel
	{
		// Token: 0x06001940 RID: 6464 RVA: 0x0007D6D8 File Offset: 0x0007B8D8
		public override float GetEmbarkDisembarkThresholdDistance()
		{
			return 0f;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0007D6DF File Offset: 0x0007B8DF
		private static bool IsTerrainTypeValidForDefault(TerrainType t)
		{
			return t == TerrainType.Plain || t == TerrainType.Desert || t == TerrainType.Snow || t == TerrainType.Forest || t == TerrainType.Steppe || t == TerrainType.Swamp || t == TerrainType.Dune || t == TerrainType.Bridge || t == TerrainType.Fording || t == TerrainType.Beach;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0007D710 File Offset: 0x0007B910
		public DefaultPartyNavigationModel()
		{
			List<int> list = new List<int>();
			foreach (object obj in Enum.GetValues(typeof(TerrainType)))
			{
				TerrainType terrainType = (TerrainType)obj;
				if (!DefaultPartyNavigationModel.IsTerrainTypeValidForDefault(terrainType))
				{
					list.Add((int)terrainType);
				}
			}
			this._invalidTerrainTypes = list.ToArray();
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0007D794 File Offset: 0x0007B994
		public override int[] GetInvalidTerrainTypesForNavigationType(MobileParty.NavigationType navigationType)
		{
			if (navigationType == MobileParty.NavigationType.Default || navigationType == MobileParty.NavigationType.All)
			{
				return this._invalidTerrainTypes;
			}
			return new int[0];
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0007D7AB File Offset: 0x0007B9AB
		public override bool IsTerrainTypeValidForNavigationType(TerrainType terrainType, MobileParty.NavigationType navigationType)
		{
			return (navigationType == MobileParty.NavigationType.Default || navigationType == MobileParty.NavigationType.All) && DefaultPartyNavigationModel.IsTerrainTypeValidForDefault(terrainType);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0007D7BD File Offset: 0x0007B9BD
		public override bool HasNavalNavigationCapability(MobileParty mobileParty)
		{
			return false;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0007D7C0 File Offset: 0x0007B9C0
		public override bool CanPlayerNavigateToPosition(CampaignVec2 vec2, out MobileParty.NavigationType navigationType)
		{
			navigationType = MobileParty.NavigationType.Default;
			return vec2.Face.IsValid() && MobileParty.MainParty.Position.IsOnLand && vec2.IsOnLand && !Campaign.Current.Models.PartyNavigationModel.GetInvalidTerrainTypesForNavigationType(navigationType).Contains(vec2.Face.FaceGroupIndex);
		}

		// Token: 0x04000843 RID: 2115
		private int[] _invalidTerrainTypes;
	}
}

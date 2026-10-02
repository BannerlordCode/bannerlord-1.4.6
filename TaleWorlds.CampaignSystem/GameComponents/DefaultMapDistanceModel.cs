using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000127 RID: 295
	public class DefaultMapDistanceModel : MapDistanceModel
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x000764C5 File Offset: 0x000746C5
		public override int RegionSwitchCostFromLandToSea
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x000764C8 File Offset: 0x000746C8
		public override int RegionSwitchCostFromSeaToLand
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x000764CB File Offset: 0x000746CB
		public override float MaximumSpawnDistanceForCompanionsAfterDisband
		{
			get
			{
				return 150f;
			}
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x000764DA File Offset: 0x000746DA
		public override void RegisterDistanceCache(MobileParty.NavigationType navigationCapability, MapDistanceModel.INavigationCache cacheToRegister)
		{
			this._navigationCache = cacheToRegister;
			cacheToRegister.FinalizeInitialization();
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000764E9 File Offset: 0x000746E9
		public override float GetMaximumDistanceBetweenTwoConnectedSettlements(MobileParty.NavigationType navigationCapabilities)
		{
			MapDistanceModel.INavigationCache navigationCache = this._navigationCache;
			if (navigationCache == null)
			{
				return 0f;
			}
			return navigationCache.MaximumDistanceBetweenTwoConnectedSettlements;
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x00076500 File Offset: 0x00074700
		public override float GetLandRatioOfPathBetweenSettlements(Settlement fromSettlement, Settlement toSettlement, bool isFromPort, bool isTargetingPort)
		{
			if (this._navigationCache != null)
			{
				float num;
				this._navigationCache.GetSettlementToSettlementDistanceWithLandRatio(fromSettlement, false, toSettlement, false, out num);
				return num;
			}
			return 1f;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00076530 File Offset: 0x00074730
		public override float GetDistance(Settlement fromSettlement, Settlement toSettlement, bool isFromPort = false, bool isTargetingPort = false, MobileParty.NavigationType navigationCapability = MobileParty.NavigationType.Default)
		{
			float num;
			return this.GetDistance(fromSettlement, toSettlement, isFromPort, isTargetingPort, MobileParty.NavigationType.Default, out num);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0007654C File Offset: 0x0007474C
		public override float GetDistance(Settlement fromSettlement, Settlement toSettlement, bool isFromPort, bool isTargetingPort, MobileParty.NavigationType navigationCapability, out float landRatio)
		{
			float num = float.MaxValue;
			landRatio = 1f;
			if (fromSettlement != null && toSettlement != null)
			{
				if (fromSettlement != toSettlement)
				{
					return this._navigationCache.GetSettlementToSettlementDistanceWithLandRatio(fromSettlement, isFromPort, toSettlement, isTargetingPort, out landRatio);
				}
				num = 0f;
			}
			return num;
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00076590 File Offset: 0x00074790
		public override float GetDistance(MobileParty fromMobileParty, Settlement toSettlement, bool isTargetingPort, MobileParty.NavigationType customCapability, out float estimatedLandRatio)
		{
			float num = 100000000f;
			estimatedLandRatio = 1f;
			if (fromMobileParty.CurrentNavigationFace.FaceIndex == toSettlement.GatePosition.Face.FaceIndex)
			{
				if (Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(Campaign.Current.MapSceneWrapper.GetFaceTerrainType(fromMobileParty.Position.Face), MobileParty.NavigationType.Default))
				{
					num = fromMobileParty.Position.Distance(toSettlement.GatePosition);
				}
			}
			else if (fromMobileParty.IsCurrentlyAtSea)
			{
				num = 100000000f;
			}
			else
			{
				Settlement item = Campaign.Current.Models.MapDistanceModel.GetClosestEntranceToFace(fromMobileParty.CurrentNavigationFace, MobileParty.NavigationType.Default).Item1;
				if (item != null)
				{
					num = fromMobileParty.Position.Distance(toSettlement.GatePosition) - item.GatePosition.Distance(toSettlement.GatePosition) + Campaign.Current.Models.MapDistanceModel.GetDistance(item, toSettlement, false, false, MobileParty.NavigationType.Default);
				}
			}
			return MBMath.ClampFloat(num, 0f, float.MaxValue);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x000766A4 File Offset: 0x000748A4
		public override float GetDistance(MobileParty fromMobileParty, MobileParty toMobileParty, MobileParty.NavigationType customCapability, out float landRatio)
		{
			float num;
			Campaign.Current.Models.MapDistanceModel.GetDistance(fromMobileParty, toMobileParty, customCapability, 100000000f, out num, out landRatio);
			return num;
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x000766D4 File Offset: 0x000748D4
		public override bool GetDistance(MobileParty fromMobileParty, MobileParty toMobileParty, MobileParty.NavigationType customCapability, float maxDistance, out float distance, out float landRatio)
		{
			landRatio = 1f;
			distance = float.MaxValue;
			if (fromMobileParty.CurrentNavigationFace.FaceIndex == toMobileParty.CurrentNavigationFace.FaceIndex)
			{
				if (Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(Campaign.Current.MapSceneWrapper.GetFaceTerrainType(fromMobileParty.Position.Face), MobileParty.NavigationType.Default))
				{
					distance = fromMobileParty.Position.Distance(toMobileParty.Position);
				}
			}
			else if (fromMobileParty.IsCurrentlyAtSea || toMobileParty.IsCurrentlyAtSea)
			{
				distance = float.MaxValue;
			}
			else
			{
				distance = fromMobileParty.Position.Distance(toMobileParty.Position);
			}
			distance = MBMath.ClampFloat(distance, 0f, float.MaxValue);
			return distance <= maxDistance;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x000767A8 File Offset: 0x000749A8
		public override float GetDistance(MobileParty fromMobileParty, in CampaignVec2 toPoint, MobileParty.NavigationType customCapability, out float landRatio)
		{
			float num = float.MaxValue;
			landRatio = 1f;
			CampaignVec2 campaignVec = toPoint;
			PathFaceRecord face = campaignVec.Face;
			if (fromMobileParty.CurrentNavigationFace.FaceIndex == face.FaceIndex)
			{
				if (Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(Campaign.Current.MapSceneWrapper.GetFaceTerrainType(fromMobileParty.Position.Face), MobileParty.NavigationType.Default))
				{
					num = fromMobileParty.Position.Distance(toPoint);
				}
			}
			else
			{
				MapDistanceModel mapDistanceModel = Campaign.Current.Models.MapDistanceModel;
				ValueTuple<Settlement, bool> closestEntranceToFace = mapDistanceModel.GetClosestEntranceToFace(fromMobileParty.CurrentNavigationFace, MobileParty.NavigationType.Default);
				ref ValueTuple<Settlement, bool> closestEntranceToFace2 = mapDistanceModel.GetClosestEntranceToFace(face, MobileParty.NavigationType.Default);
				Settlement item = closestEntranceToFace.Item1;
				Settlement item2 = closestEntranceToFace2.Item1;
				if (item != null && item2 != null)
				{
					num = fromMobileParty.Position.Distance(toPoint) - item.GatePosition.Distance(item2.GatePosition) + this.GetDistance(item, item2, false, false, MobileParty.NavigationType.Default);
				}
			}
			return MBMath.ClampFloat(num, 0f, float.MaxValue);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x000768B8 File Offset: 0x00074AB8
		public override float GetDistance(Settlement fromSettlement, in CampaignVec2 toPoint, bool isFromPort, MobileParty.NavigationType customCapability)
		{
			float num = float.MaxValue;
			CampaignVec2 campaignVec = (isFromPort ? fromSettlement.PortPosition : fromSettlement.GatePosition);
			CampaignVec2 campaignVec2 = toPoint;
			PathFaceRecord face = campaignVec2.Face;
			PathFaceRecord face2 = campaignVec.Face;
			if (face2.FaceIndex == face.FaceIndex)
			{
				if (Campaign.Current.Models.PartyNavigationModel.IsTerrainTypeValidForNavigationType(Campaign.Current.MapSceneWrapper.GetFaceTerrainType(face2), MobileParty.NavigationType.Default))
				{
					num = campaignVec.Distance(toPoint);
				}
			}
			else
			{
				MapDistanceModel mapDistanceModel = Campaign.Current.Models.MapDistanceModel;
				Settlement item = mapDistanceModel.GetClosestEntranceToFace(face, MobileParty.NavigationType.Default).Item1;
				if (item != null)
				{
					num = fromSettlement.GatePosition.Distance(toPoint) - fromSettlement.GatePosition.Distance(item.GatePosition) + mapDistanceModel.GetDistance(fromSettlement, item, false, false, MobileParty.NavigationType.Default);
				}
			}
			return MBMath.ClampFloat(num, 0f, 100000000f);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000769AA File Offset: 0x00074BAA
		public override float GetPortToGateDistanceForSettlement(Settlement settlement)
		{
			return 100000000f;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x000769B1 File Offset: 0x00074BB1
		public override bool PathExistBetweenPoints(in CampaignVec2 fromPoint, in CampaignVec2 toPoint, MobileParty.NavigationType navigationType)
		{
			return fromPoint.IsOnLand && toPoint.IsOnLand;
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x000769C4 File Offset: 0x00074BC4
		public override ValueTuple<Settlement, bool> GetClosestEntranceToFace(PathFaceRecord face, MobileParty.NavigationType navigationCapabilities)
		{
			bool flag;
			return new ValueTuple<Settlement, bool>(this._navigationCache.GetClosestSettlementToFaceIndex(face.FaceIndex, out flag), flag);
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000769EA File Offset: 0x00074BEA
		public override MBReadOnlyList<Settlement> GetNeighborsOfFortification(Town town, MobileParty.NavigationType navigationCapabilities)
		{
			return this._navigationCache.GetNeighbors(town.Settlement);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000769FD File Offset: 0x00074BFD
		public override float GetTransitionCostAdjustment(Settlement settlement1, bool isFromPort, Settlement settlement2, bool isTargetingPort, bool fromIsCurrentlyAtSea, bool toIsCurrentlyAtSea)
		{
			return 0f;
		}

		// Token: 0x04000805 RID: 2053
		private MapDistanceModel.INavigationCache _navigationCache;
	}
}

using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200037F RID: 895
	public class VolumeBox : MissionObject
	{
		// Token: 0x060033A0 RID: 13216 RVA: 0x000D3FD5 File Offset: 0x000D21D5
		protected internal override void OnInit()
		{
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000D3FD7 File Offset: 0x000D21D7
		public void AddToCheckList(Agent agent)
		{
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000D3FD9 File Offset: 0x000D21D9
		public void RemoveFromCheckList(Agent agent)
		{
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000D3FDB File Offset: 0x000D21DB
		public void SetIsOccupiedDelegate(VolumeBox.VolumeBoxDelegate volumeBoxDelegate)
		{
			this._volumeBoxIsOccupiedDelegate = volumeBoxDelegate;
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000D3FE4 File Offset: 0x000D21E4
		public bool HasAgentsInAttackerSide()
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(Mission.Current, globalFrame.origin.AsVec2, globalFrame.rotation.GetScaleVector().AsVec2.Length, false);
			while (proximityMapSearchStruct.LastFoundAgent != null)
			{
				Agent lastFoundAgent = proximityMapSearchStruct.LastFoundAgent;
				if (lastFoundAgent.Team != null && lastFoundAgent.Team.Side == BattleSideEnum.Attacker && this.IsPointIn(lastFoundAgent.Position))
				{
					return true;
				}
				AgentProximityMap.FindNext(Mission.Current, ref proximityMapSearchStruct);
			}
			return false;
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000D4080 File Offset: 0x000D2280
		public bool IsPointIn(Vec3 point)
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			Vec3 scaleVector = globalFrame.rotation.GetScaleVector();
			Vec3 vec = new Vec3(1f / scaleVector.x, 1f / scaleVector.y, 1f / scaleVector.z, -1f);
			globalFrame.rotation.ApplyScaleLocal(in vec);
			point = globalFrame.TransformToLocal(in point);
			return MathF.Abs(point.x) <= scaleVector.x / 2f && MathF.Abs(point.y) <= scaleVector.y / 2f && MathF.Abs(point.z) <= scaleVector.z / 2f;
		}

		// Token: 0x040015B8 RID: 5560
		private VolumeBox.VolumeBoxDelegate _volumeBoxIsOccupiedDelegate;

		// Token: 0x02000658 RID: 1624
		// (Invoke) Token: 0x0600405F RID: 16479
		public delegate void VolumeBoxDelegate(VolumeBox volumeBox, List<Agent> agentsInVolume);
	}
}

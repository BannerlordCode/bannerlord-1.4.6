using System;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Objects
{
	// Token: 0x020003A4 RID: 932
	public class StealthBox : ScriptComponentBehavior
	{
		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x060034FB RID: 13563 RVA: 0x000D9CEC File Offset: 0x000D7EEC
		// (remove) Token: 0x060034FC RID: 13564 RVA: 0x000D9D20 File Offset: 0x000D7F20
		public static event Action<StealthBox> OnBoxInitialized;

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x060034FD RID: 13565 RVA: 0x000D9D54 File Offset: 0x000D7F54
		// (remove) Token: 0x060034FE RID: 13566 RVA: 0x000D9D88 File Offset: 0x000D7F88
		public static event Action<StealthBox> OnBoxRemoved;

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060034FF RID: 13567 RVA: 0x000D9DBB File Offset: 0x000D7FBB
		public bool CoversStandingAgents
		{
			get
			{
				return this._coversStandingAgents;
			}
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000D9DC4 File Offset: 0x000D7FC4
		protected internal override void OnInit()
		{
			base.OnInit();
			MetaMesh metaMesh = base.GameEntity.GetMetaMesh(0);
			if (metaMesh != null)
			{
				base.GameEntity.RemoveMultiMesh(metaMesh);
			}
			Action<StealthBox> onBoxInitialized = StealthBox.OnBoxInitialized;
			if (onBoxInitialized == null)
			{
				return;
			}
			onBoxInitialized(this);
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000D9E10 File Offset: 0x000D8010
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			Action<StealthBox> onBoxRemoved = StealthBox.OnBoxRemoved;
			if (onBoxRemoved == null)
			{
				return;
			}
			onBoxRemoved(this);
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000D9E2C File Offset: 0x000D802C
		public bool IsPointInside(Vec3 point)
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			Vec3 scaleVector = globalFrame.rotation.GetScaleVector();
			if (globalFrame.origin.DistanceSquared(point) > scaleVector.LengthSquared)
			{
				return false;
			}
			Vec3 vec = new Vec3(1f / scaleVector.x, 1f / scaleVector.y, 1f / scaleVector.z, -1f);
			globalFrame.rotation.ApplyScaleLocal(in vec);
			point = globalFrame.TransformToLocal(in point);
			return MathF.Abs(point.x) <= scaleVector.x / 2f && MathF.Abs(point.y) <= scaleVector.y / 2f && point.z >= 0f && point.z <= scaleVector.z;
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000D9F07 File Offset: 0x000D8107
		public bool IsAgentInside(Agent agent)
		{
			return this.IsPointInside(agent.Position);
		}

		// Token: 0x04001682 RID: 5762
		[EditableScriptComponentVariable(true, "")]
		private bool _coversStandingAgents;
	}
}

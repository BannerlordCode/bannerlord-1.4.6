using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000365 RID: 869
	public class TacticalPosition : MissionObject
	{
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x000CB5FC File Offset: 0x000C97FC
		// (set) Token: 0x060031DB RID: 12763 RVA: 0x000CB604 File Offset: 0x000C9804
		public WorldPosition Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000CB60D File Offset: 0x000C980D
		// (set) Token: 0x060031DD RID: 12765 RVA: 0x000CB615 File Offset: 0x000C9815
		public Vec2 Direction
		{
			get
			{
				return this._direction;
			}
			set
			{
				this._direction = value;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x000CB61E File Offset: 0x000C981E
		public float Width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x000CB626 File Offset: 0x000C9826
		public float Slope
		{
			get
			{
				return this._slope;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x000CB62E File Offset: 0x000C982E
		public bool IsInsurmountable
		{
			get
			{
				return this._isInsurmountable;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x000CB636 File Offset: 0x000C9836
		public bool IsOuterEdge
		{
			get
			{
				return this._isOuterEdge;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000CB63E File Offset: 0x000C983E
		// (set) Token: 0x060031E3 RID: 12771 RVA: 0x000CB646 File Offset: 0x000C9846
		public List<TacticalPosition> LinkedTacticalPositions
		{
			get
			{
				return this._linkedTacticalPositions;
			}
			set
			{
				this._linkedTacticalPositions = value;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000CB64F File Offset: 0x000C984F
		public TacticalPosition.TacticalPositionTypeEnum TacticalPositionType
		{
			get
			{
				return this._tacticalPositionType;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000CB657 File Offset: 0x000C9857
		public TacticalRegion.TacticalRegionTypeEnum TacticalRegionMembership
		{
			get
			{
				return this._tacticalRegionMembership;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x000CB65F File Offset: 0x000C985F
		public FormationAI.BehaviorSide TacticalPositionSide
		{
			get
			{
				return this._tacticalPositionSide;
			}
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000CB667 File Offset: 0x000C9867
		public TacticalPosition()
		{
			this._width = 1f;
			this._slope = 0f;
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000CB68C File Offset: 0x000C988C
		public TacticalPosition(WorldPosition position, Vec2 direction, float width, float slope = 0f, bool isInsurmountable = false, TacticalPosition.TacticalPositionTypeEnum tacticalPositionType = TacticalPosition.TacticalPositionTypeEnum.Regional, TacticalRegion.TacticalRegionTypeEnum tacticalRegionMembership = TacticalRegion.TacticalRegionTypeEnum.Opening)
		{
			this._position = position;
			this._direction = direction;
			this._width = width;
			this._slope = slope;
			this._isInsurmountable = isInsurmountable;
			this._tacticalPositionType = tacticalPositionType;
			this._tacticalRegionMembership = tacticalRegionMembership;
			this._tacticalPositionSide = FormationAI.BehaviorSide.BehaviorSideNotSet;
			this._isOuterEdge = false;
			this._linkedTacticalPositions = new List<TacticalPosition>();
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000CB6F4 File Offset: 0x000C98F4
		protected internal override void OnInit()
		{
			base.OnInit();
			this._position = new WorldPosition(base.GameEntity.GetScenePointer(), base.GameEntity.GlobalPosition);
			this._direction = base.GameEntity.GetGlobalFrame().rotation.f.AsVec2.Normalized();
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000CB75C File Offset: 0x000C995C
		public override void AfterMissionStart()
		{
			base.AfterMissionStart();
			this._linkedTacticalPositions = (from c in base.GameEntity.GetChildren()
				where c.HasScriptOfType<TacticalPosition>()
				select c.GetFirstScriptOfType<TacticalPosition>()).ToList<TacticalPosition>();
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000CB7D0 File Offset: 0x000C99D0
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.ApplyChangesFromEditor();
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000CB7DE File Offset: 0x000C99DE
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this.ApplyChangesFromEditor();
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000CB7F0 File Offset: 0x000C99F0
		private void ApplyChangesFromEditor()
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			if (this._width > 0f && this._width != this._oldWidth)
			{
				globalFrame.rotation.MakeUnit();
				Vec3 vec = new Vec3(this._width, 1f, 1f, -1f);
				globalFrame.rotation.ApplyScaleLocal(in vec);
				base.GameEntity.SetGlobalFrame(in globalFrame, true);
				base.GameEntity.UpdateTriadFrameForEditorForAllChildren();
				this._oldWidth = this._width;
			}
			this._direction = globalFrame.rotation.f.AsVec2.Normalized();
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000CB8A7 File Offset: 0x000C9AA7
		public void SetWidth(float width)
		{
			this._width = width;
		}

		// Token: 0x0400150F RID: 5391
		private WorldPosition _position;

		// Token: 0x04001510 RID: 5392
		private Vec2 _direction;

		// Token: 0x04001511 RID: 5393
		private float _oldWidth;

		// Token: 0x04001512 RID: 5394
		[EditableScriptComponentVariable(true, "")]
		private float _width;

		// Token: 0x04001513 RID: 5395
		[EditableScriptComponentVariable(true, "")]
		private float _slope;

		// Token: 0x04001514 RID: 5396
		[EditableScriptComponentVariable(true, "")]
		private bool _isInsurmountable;

		// Token: 0x04001515 RID: 5397
		[EditableScriptComponentVariable(true, "")]
		private bool _isOuterEdge;

		// Token: 0x04001516 RID: 5398
		private List<TacticalPosition> _linkedTacticalPositions;

		// Token: 0x04001517 RID: 5399
		[EditableScriptComponentVariable(true, "")]
		private TacticalPosition.TacticalPositionTypeEnum _tacticalPositionType;

		// Token: 0x04001518 RID: 5400
		[EditableScriptComponentVariable(true, "")]
		private TacticalRegion.TacticalRegionTypeEnum _tacticalRegionMembership;

		// Token: 0x04001519 RID: 5401
		[EditableScriptComponentVariable(true, "")]
		private FormationAI.BehaviorSide _tacticalPositionSide = FormationAI.BehaviorSide.BehaviorSideNotSet;

		// Token: 0x02000641 RID: 1601
		public enum TacticalPositionTypeEnum
		{
			// Token: 0x0400211B RID: 8475
			Regional,
			// Token: 0x0400211C RID: 8476
			HighGround,
			// Token: 0x0400211D RID: 8477
			ChokePoint,
			// Token: 0x0400211E RID: 8478
			Cliff,
			// Token: 0x0400211F RID: 8479
			SpecialMissionPosition
		}
	}
}

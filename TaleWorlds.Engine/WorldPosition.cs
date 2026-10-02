using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x020000A0 RID: 160
	[EngineStruct("rglWorld_position::Plain_world_position", false, null)]
	public struct WorldPosition
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x000116B4 File Offset: 0x0000F8B4
		public Vec2 AsVec2
		{
			get
			{
				return this._position.AsVec2;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x000116C1 File Offset: 0x0000F8C1
		public float X
		{
			get
			{
				return this._position.x;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000116CE File Offset: 0x0000F8CE
		public float Y
		{
			get
			{
				return this._position.y;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x000116DC File Offset: 0x0000F8DC
		public bool IsValid
		{
			get
			{
				return this.AsVec2.IsValid && this._scene != UIntPtr.Zero;
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0001170B File Offset: 0x0000F90B
		internal WorldPosition(UIntPtr scenePointer, Vec3 position)
		{
			this = new WorldPosition(scenePointer, UIntPtr.Zero, position, false);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0001171C File Offset: 0x0000F91C
		internal WorldPosition(UIntPtr scenePointer, UIntPtr navMesh, Vec3 position, bool hasValidZ)
		{
			this._scene = scenePointer;
			this._navMesh = navMesh;
			this._nearestNavMesh = this._navMesh;
			this._position = position;
			this.Normal = Vec3.Zero;
			if (hasValidZ)
			{
				this._lastValidZPosition = this._position.AsVec2;
				this.State = ZValidityState.Valid;
				return;
			}
			this._lastValidZPosition = Vec2.Invalid;
			this.State = ZValidityState.Invalid;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00011784 File Offset: 0x0000F984
		public WorldPosition(Scene scene, Vec3 position)
		{
			this = new WorldPosition((scene != null) ? scene.Pointer : UIntPtr.Zero, UIntPtr.Zero, position, false);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000117A9 File Offset: 0x0000F9A9
		public WorldPosition(Scene scene, UIntPtr navMesh, Vec3 position, bool hasValidZ)
		{
			this = new WorldPosition((scene != null) ? scene.Pointer : UIntPtr.Zero, navMesh, position, hasValidZ);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x000117CC File Offset: 0x0000F9CC
		public void SetVec3(UIntPtr navMesh, Vec3 position, bool hasValidZ)
		{
			this._navMesh = navMesh;
			this._nearestNavMesh = this._navMesh;
			this._position = position;
			this.Normal = Vec3.Zero;
			if (hasValidZ)
			{
				this._lastValidZPosition = this._position.AsVec2;
				this.State = ZValidityState.Valid;
				return;
			}
			this._lastValidZPosition = Vec2.Invalid;
			this.State = ZValidityState.Invalid;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0001182C File Offset: 0x0000FA2C
		private void ValidateZ(ZValidityState minimumValidityState)
		{
			if (this.State < minimumValidityState)
			{
				EngineApplicationInterface.IScene.WorldPositionValidateZ(ref this, (int)minimumValidityState);
			}
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00011844 File Offset: 0x0000FA44
		private void ValidateZMT(ZValidityState minimumValidityState)
		{
			if (this.State < minimumValidityState)
			{
				using (new TWSharedMutexReadLock(Scene.PhysicsAndRayCastLock))
				{
					EngineApplicationInterface.IScene.WorldPositionValidateZ(ref this, (int)minimumValidityState);
				}
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00011894 File Offset: 0x0000FA94
		public UIntPtr GetNavMesh()
		{
			this.ValidateZ(ZValidityState.ValidAccordingToNavMesh);
			return this._navMesh;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000118A3 File Offset: 0x0000FAA3
		public UIntPtr GetNavMeshMT()
		{
			this.ValidateZMT(ZValidityState.ValidAccordingToNavMesh);
			return this._navMesh;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x000118B2 File Offset: 0x0000FAB2
		public UIntPtr GetNearestNavMesh()
		{
			EngineApplicationInterface.IScene.WorldPositionComputeNearestNavMesh(ref this);
			return this._nearestNavMesh;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x000118C5 File Offset: 0x0000FAC5
		public float GetNavMeshZ()
		{
			this.ValidateZ(ZValidityState.ValidAccordingToNavMesh);
			if (this.State >= ZValidityState.ValidAccordingToNavMesh)
			{
				return this._position.z;
			}
			return float.NaN;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x000118E8 File Offset: 0x0000FAE8
		public float GetNavMeshZMT()
		{
			this.ValidateZMT(ZValidityState.ValidAccordingToNavMesh);
			if (this.State >= ZValidityState.ValidAccordingToNavMesh)
			{
				return this._position.z;
			}
			return float.NaN;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0001190B File Offset: 0x0000FB0B
		public float GetGroundZ()
		{
			this.ValidateZ(ZValidityState.Valid);
			if (this.State >= ZValidityState.Valid)
			{
				return this._position.z;
			}
			return float.NaN;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0001192E File Offset: 0x0000FB2E
		public float GetGroundZMT()
		{
			this.ValidateZMT(ZValidityState.Valid);
			if (this.State >= ZValidityState.Valid)
			{
				return this._position.z;
			}
			return float.NaN;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00011951 File Offset: 0x0000FB51
		public Vec3 GetNavMeshVec3()
		{
			return new Vec3(this._position.AsVec2, this.GetNavMeshZ(), -1f);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0001196E File Offset: 0x0000FB6E
		public Vec3 GetNavMeshVec3MT()
		{
			return new Vec3(this._position.AsVec2, this.GetNavMeshZMT(), -1f);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0001198B File Offset: 0x0000FB8B
		public Vec3 GetGroundVec3()
		{
			return new Vec3(this._position.AsVec2, this.GetGroundZ(), -1f);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x000119A8 File Offset: 0x0000FBA8
		public Vec3 GetGroundVec3MT()
		{
			return new Vec3(this._position.AsVec2, this.GetGroundZMT(), -1f);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000119C5 File Offset: 0x0000FBC5
		public Vec3 GetVec3WithoutValidity()
		{
			return this._position;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x000119D0 File Offset: 0x0000FBD0
		public void SetVec2MT(Vec2 value)
		{
			if (this._position.AsVec2 != value)
			{
				if (this.State != ZValidityState.Invalid)
				{
					this.State = ZValidityState.Invalid;
				}
				else if (!this._lastValidZPosition.IsValid)
				{
					this.ValidateZMT(ZValidityState.ValidAccordingToNavMesh);
					this.State = ZValidityState.Invalid;
				}
				this._position.x = value.x;
				this._position.y = value.y;
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00011A40 File Offset: 0x0000FC40
		public void SetVec2(Vec2 value)
		{
			if (this._position.AsVec2 != value)
			{
				if (this.State != ZValidityState.Invalid)
				{
					this.State = ZValidityState.Invalid;
				}
				else if (!this._lastValidZPosition.IsValid)
				{
					this.ValidateZ(ZValidityState.ValidAccordingToNavMesh);
					this.State = ZValidityState.Invalid;
				}
				this._position.x = value.x;
				this._position.y = value.y;
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00011AB0 File Offset: 0x0000FCB0
		public float DistanceSquaredWithLimit(in Vec3 targetPoint, float limitSquared)
		{
			Vec2 asVec = this._position.AsVec2;
			Vec3 vec = targetPoint;
			float num = asVec.DistanceSquared(vec.AsVec2);
			if (num <= limitSquared)
			{
				return this.GetGroundVec3().DistanceSquared(targetPoint);
			}
			return num;
		}

		// Token: 0x0400020F RID: 527
		private readonly UIntPtr _scene;

		// Token: 0x04000210 RID: 528
		private UIntPtr _navMesh;

		// Token: 0x04000211 RID: 529
		private UIntPtr _nearestNavMesh;

		// Token: 0x04000212 RID: 530
		private Vec3 _position;

		// Token: 0x04000213 RID: 531
		[CustomEngineStructMemberData("normal_")]
		public Vec3 Normal;

		// Token: 0x04000214 RID: 532
		private Vec2 _lastValidZPosition;

		// Token: 0x04000215 RID: 533
		[CustomEngineStructMemberData("z_validity_state_")]
		public ZValidityState State;

		// Token: 0x04000216 RID: 534
		public static readonly WorldPosition Invalid = new WorldPosition(UIntPtr.Zero, UIntPtr.Zero, Vec3.Invalid, false);

		// Token: 0x020000E1 RID: 225
		public enum WorldPositionEnforcedCache
		{
			// Token: 0x040004DD RID: 1245
			None,
			// Token: 0x040004DE RID: 1246
			NavMeshVec3,
			// Token: 0x040004DF RID: 1247
			GroundVec3
		}
	}
}

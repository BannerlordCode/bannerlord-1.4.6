using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200004F RID: 79
	public class GameEntityWithWorldPosition
	{
		// Token: 0x0600085E RID: 2142 RVA: 0x0000678C File Offset: 0x0000498C
		public GameEntityWithWorldPosition(WeakGameEntity gameEntity)
		{
			this._customLocalFrame = MatrixFrame.Identity;
			this._gameEntity = gameEntity;
			Scene scene = gameEntity.Scene;
			float groundHeightAtPosition = scene.GetGroundHeightAtPosition(gameEntity.GlobalPosition, BodyFlags.CommonCollisionExcludeFlags);
			this._worldPosition = new WorldPosition(scene, UIntPtr.Zero, new Vec3(gameEntity.GlobalPosition.AsVec2, groundHeightAtPosition, -1f), false);
			this._worldPosition.GetGroundVec3();
			this._orthonormalRotation = gameEntity.GetGlobalFrame().rotation;
			this._orthonormalRotation.Orthonormalize();
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00006821 File Offset: 0x00004A21
		public WeakGameEntity GameEntity
		{
			get
			{
				return this._gameEntity;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00006829 File Offset: 0x00004A29
		public WorldPosition WorldPosition
		{
			get
			{
				this.ValidateWorldPosition();
				return this._worldPosition;
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00006838 File Offset: 0x00004A38
		private void ValidateWorldPosition()
		{
			Vec3 vec = (this._customLocalFrame.IsIdentity ? this.GameEntity.GetGlobalFrame().origin : this.GameEntity.GetGlobalFrame().TransformToParent(in this._customLocalFrame).origin);
			if (!this._worldPosition.AsVec2.NearlyEquals(vec.AsVec2, 1E-05f))
			{
				this._worldPosition.SetVec3(UIntPtr.Zero, vec, false);
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000068BC File Offset: 0x00004ABC
		public void InvalidateWorldPosition()
		{
			this._worldPosition.State = ZValidityState.Invalid;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x000068CC File Offset: 0x00004ACC
		public WorldFrame WorldFrame
		{
			get
			{
				Mat3 mat = (this._customLocalFrame.rotation.IsIdentity() ? this.GameEntity.GetGlobalFrame().rotation : this.GameEntity.GetGlobalFrame().rotation.TransformToParent(in this._customLocalFrame.rotation));
				if (!mat.NearlyEquals(in this._orthonormalRotation, 1E-05f))
				{
					this._orthonormalRotation = mat;
					this._orthonormalRotation.Orthonormalize();
				}
				return new WorldFrame(this._orthonormalRotation, this.WorldPosition);
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0000695E File Offset: 0x00004B5E
		public void SetCustomLocalFrame(in MatrixFrame customLocalFrame)
		{
			this._customLocalFrame = customLocalFrame;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0000696C File Offset: 0x00004B6C
		public Vec2 AsVec2
		{
			get
			{
				this.ValidateWorldPosition();
				return this._worldPosition.AsVec2;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000697F File Offset: 0x00004B7F
		public UIntPtr GetNavMesh()
		{
			this.ValidateWorldPosition();
			return this._worldPosition.GetNavMesh();
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00006992 File Offset: 0x00004B92
		public Vec3 GetNavMeshVec3()
		{
			this.ValidateWorldPosition();
			return this._worldPosition.GetNavMeshVec3();
		}

		// Token: 0x040000B2 RID: 178
		private MatrixFrame _customLocalFrame;

		// Token: 0x040000B3 RID: 179
		private readonly WeakGameEntity _gameEntity;

		// Token: 0x040000B4 RID: 180
		private WorldPosition _worldPosition;

		// Token: 0x040000B5 RID: 181
		private Mat3 _orthonormalRotation;
	}
}

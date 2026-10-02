using System;
using System.Diagnostics;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x02000095 RID: 149
	public class MissionEntitySelectionUIHandler : MissionView
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x00027270 File Offset: 0x00025470
		public MissionEntitySelectionUIHandler(Action<WeakGameEntity> onSelect = null, Action<WeakGameEntity> onHover = null)
		{
			this.onSelect = onSelect;
			this.onHover = onHover;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00027288 File Offset: 0x00025488
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			WeakGameEntity value = new Lazy<WeakGameEntity>(new Func<WeakGameEntity>(this.GetCollidedEntity)).Value;
			Action<WeakGameEntity> action = this.onHover;
			if (action != null)
			{
				action(value);
			}
			if (base.Input.IsKeyReleased(InputKey.LeftMouseButton))
			{
				Action<WeakGameEntity> action2 = this.onSelect;
				if (action2 == null)
				{
					return;
				}
				action2(value);
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000272E8 File Offset: 0x000254E8
		private WeakGameEntity GetCollidedEntity()
		{
			Vec2 mousePositionRanged = base.Input.GetMousePositionRanged();
			Vec3 vec;
			Vec3 vec2;
			base.MissionScreen.ScreenPointToWorldRay(mousePositionRanged, out vec, out vec2);
			WeakGameEntity weakGameEntity;
			using (new TWSharedMutexReadLock(Scene.PhysicsAndRayCastLock))
			{
				if (Mission.Current != null)
				{
					float num;
					WeakGameEntity parent;
					Mission.Current.Scene.RayCastForClosestEntityOrTerrain(vec, vec2, out num, out parent, 0.3f, BodyFlags.CommonFocusRayCastExcludeFlags);
					while (parent.IsValid)
					{
						weakGameEntity = parent.Parent;
						if (!weakGameEntity.IsValid)
						{
							break;
						}
						parent = parent.Parent;
					}
					weakGameEntity = parent;
				}
				else
				{
					weakGameEntity = WeakGameEntity.Invalid;
				}
			}
			return weakGameEntity;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0002739C File Offset: 0x0002559C
		public override void OnRemoveBehavior()
		{
			this.onSelect = null;
			this.onHover = null;
			base.OnRemoveBehavior();
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000273B4 File Offset: 0x000255B4
		[Conditional("DEBUG")]
		public void TickDebug()
		{
			WeakGameEntity collidedEntity = this.GetCollidedEntity();
			if (collidedEntity.IsValid)
			{
				string name = collidedEntity.Name;
			}
		}

		// Token: 0x040002FA RID: 762
		private Action<WeakGameEntity> onSelect;

		// Token: 0x040002FB RID: 763
		private Action<WeakGameEntity> onHover;
	}
}

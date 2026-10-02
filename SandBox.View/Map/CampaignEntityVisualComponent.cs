using System;
using SandBox.View.Map.Visuals;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.View.Map
{
	// Token: 0x0200003F RID: 63
	public class CampaignEntityVisualComponent : IEntityComponent
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00013F53 File Offset: 0x00012153
		public virtual void OnVisualTick(MapScreen screen, float realDt, float dt)
		{
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00013F55 File Offset: 0x00012155
		public virtual bool OnMouseClick(MapEntityVisual visualOfSelectedEntity, Vec3 intersectionPoint, PathFaceRecord mouseOverFaceIndex, bool isDoubleClick)
		{
			return false;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00013F58 File Offset: 0x00012158
		public virtual bool OnVisualIntersected(Ray mouseRay, UIntPtr[] intersectedEntityIDs, Intersection[] intersectionInfos, int entityCount, Vec3 worldMouseNear, Vec3 worldMouseFar, Vec3 terrainIntersectionPoint, ref MapEntityVisual hoveredVisual, ref MapEntityVisual selectedVisual)
		{
			return false;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00013F5B File Offset: 0x0001215B
		public virtual void OnFrameTick(float dt)
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00013F5D File Offset: 0x0001215D
		public virtual void OnGameLoadFinished()
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00013F5F File Offset: 0x0001215F
		public virtual void OnTick(float realDt, float dt)
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00013F61 File Offset: 0x00012161
		public virtual void ClearVisualMemory()
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00013F63 File Offset: 0x00012163
		void IEntityComponent.OnInitialize()
		{
			this.OnInitialize();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00013F6B File Offset: 0x0001216B
		void IEntityComponent.OnFinalize()
		{
			this.OnFinalize();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00013F73 File Offset: 0x00012173
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00013F75 File Offset: 0x00012175
		protected virtual void OnFinalize()
		{
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00013F77 File Offset: 0x00012177
		public virtual int Priority
		{
			get
			{
				return 0;
			}
		}
	}
}

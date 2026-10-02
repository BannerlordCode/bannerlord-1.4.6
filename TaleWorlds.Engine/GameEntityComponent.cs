using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200004B RID: 75
	[EngineClass("rglEntity_component")]
	public abstract class GameEntityComponent : NativeObject
	{
		// Token: 0x060007E1 RID: 2017 RVA: 0x00005C6E File Offset: 0x00003E6E
		internal GameEntityComponent(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00005C7D File Offset: 0x00003E7D
		public WeakGameEntity GetEntity()
		{
			return new WeakGameEntity(EngineApplicationInterface.IGameEntityComponent.GetEntityPointer(base.Pointer));
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00005C94 File Offset: 0x00003E94
		public virtual MetaMesh GetFirstMetaMesh()
		{
			return EngineApplicationInterface.IGameEntityComponent.GetFirstMetaMesh(this);
		}
	}
}

using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C9 RID: 713
	public class AgentVisualHolder : IAgentVisual
	{
		// Token: 0x0600292E RID: 10542 RVA: 0x0009AD00 File Offset: 0x00098F00
		public AgentVisualHolder(MatrixFrame frame, Equipment equipment, string name, BodyProperties bodyProperties)
		{
			this.SetFrame(ref frame);
			this._equipment = equipment;
			this._characterObjectStringID = name;
			this._bodyProperties = bodyProperties;
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x0009AD26 File Offset: 0x00098F26
		public void SetAction(in ActionIndexCache actionName, float startProgress = 0f, bool forceFaceMorphRestart = true)
		{
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x0009AD28 File Offset: 0x00098F28
		public GameEntity GetEntity()
		{
			return null;
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x0009AD2B File Offset: 0x00098F2B
		public MBAgentVisuals GetVisuals()
		{
			return null;
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x0009AD2E File Offset: 0x00098F2E
		public void SetFrame(ref MatrixFrame frame)
		{
			this._frame = frame;
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x0009AD3C File Offset: 0x00098F3C
		public MatrixFrame GetFrame()
		{
			return this._frame;
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x0009AD44 File Offset: 0x00098F44
		public BodyProperties GetBodyProperties()
		{
			return this._bodyProperties;
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x0009AD4C File Offset: 0x00098F4C
		public void SetBodyProperties(BodyProperties bodyProperties)
		{
			this._bodyProperties = bodyProperties;
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x0009AD55 File Offset: 0x00098F55
		public bool GetIsFemale()
		{
			return false;
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x0009AD58 File Offset: 0x00098F58
		public string GetCharacterObjectID()
		{
			return this._characterObjectStringID;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x0009AD60 File Offset: 0x00098F60
		public void SetCharacterObjectID(string id)
		{
			this._characterObjectStringID = id;
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x0009AD69 File Offset: 0x00098F69
		public Equipment GetEquipment()
		{
			return this._equipment;
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x0009AD71 File Offset: 0x00098F71
		public void RefreshWithNewEquipment(Equipment equipment)
		{
			this._equipment = equipment;
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x0009AD7A File Offset: 0x00098F7A
		public void SetClothingColors(uint color1, uint color2)
		{
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x0009AD7C File Offset: 0x00098F7C
		public void GetClothingColors(out uint color1, out uint color2)
		{
			color1 = uint.MaxValue;
			color2 = uint.MaxValue;
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x0009AD84 File Offset: 0x00098F84
		public AgentVisualsData GetCopyAgentVisualsData()
		{
			return null;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x0009AD87 File Offset: 0x00098F87
		public void Refresh(bool needBatchedVersionForWeaponMeshes, AgentVisualsData data, bool forceUseFaceCache = false)
		{
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x0009AD89 File Offset: 0x00098F89
		void IAgentVisual.SetAction(in ActionIndexCache actionName, float startProgress, bool forceFaceMorphRestart)
		{
			this.SetAction(in actionName, startProgress, forceFaceMorphRestart);
		}

		// Token: 0x04000FC8 RID: 4040
		private MatrixFrame _frame;

		// Token: 0x04000FC9 RID: 4041
		private Equipment _equipment;

		// Token: 0x04000FCA RID: 4042
		private string _characterObjectStringID;

		// Token: 0x04000FCB RID: 4043
		private BodyProperties _bodyProperties;
	}
}

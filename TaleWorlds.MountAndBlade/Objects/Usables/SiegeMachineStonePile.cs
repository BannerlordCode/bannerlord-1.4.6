using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Objects.Siege;

namespace TaleWorlds.MountAndBlade.Objects.Usables
{
	// Token: 0x020003AB RID: 939
	public class SiegeMachineStonePile : UsableMachine, ISpawnable
	{
		// Token: 0x0600352D RID: 13613 RVA: 0x000DAA69 File Offset: 0x000D8C69
		protected internal override void OnInit()
		{
			base.OnInit();
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000DAA74 File Offset: 0x000D8C74
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			if (usableGameObject.GameEntity.HasTag(this.AmmoPickUpTag))
			{
				TextObject textObject = new TextObject("{=jfcceEoE}{PILE_TYPE} Pile", null);
				textObject.SetTextVariable("PILE_TYPE", new TextObject("{=1CPdu9K0}Stone", null));
				return textObject;
			}
			return null;
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000DAABB File Offset: 0x000D8CBB
		public override TextObject GetDescriptionText(WeakGameEntity gameEntity)
		{
			if (gameEntity.HasTag(this.AmmoPickUpTag))
			{
				TextObject textObject = new TextObject("{=bNYm3K6b}{KEY} Pick Up", null);
				textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13), 1f));
				return textObject;
			}
			return null;
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000DAAFB File Offset: 0x000D8CFB
		public void SetSpawnedFromSpawner()
		{
			this._spawnedFromSpawner = true;
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000DAB04 File Offset: 0x000D8D04
		public override OrderType GetOrder(BattleSideEnum side)
		{
			return OrderType.None;
		}

		// Token: 0x04001698 RID: 5784
		private bool _spawnedFromSpawner;
	}
}

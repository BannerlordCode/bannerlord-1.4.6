using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Objects.Usables
{
	// Token: 0x020003A8 RID: 936
	public class EventTriggeringUsableMachine : UsableMachine
	{
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06003520 RID: 13600 RVA: 0x000DA841 File Offset: 0x000D8A41
		public TextObject ActionText
		{
			get
			{
				return GameTexts.FindText(this.ActionTextId, null);
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06003521 RID: 13601 RVA: 0x000DA84F File Offset: 0x000D8A4F
		public TextObject DescriptionText
		{
			get
			{
				return GameTexts.FindText(this.DescriptionTextId, null);
			}
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000DA860 File Offset: 0x000D8A60
		protected internal override void OnInit()
		{
			base.OnInit();
			base.SetScriptComponentToTick(ScriptComponentBehavior.TickRequirement.Tick);
			using (IEnumerator<ScriptComponentBehavior> enumerator = base.GameEntity.GetScriptComponents().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GenericMissionEventScript genericMissionEventScript;
					if ((genericMissionEventScript = enumerator.Current as GenericMissionEventScript) != null)
					{
						this._genericMissionEvents.Add(genericMissionEventScript);
					}
				}
			}
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000DA8D0 File Offset: 0x000D8AD0
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			for (int i = 0; i < base.StandingPoints.Count; i++)
			{
				if (base.StandingPoints[i].HasUser)
				{
					foreach (GenericMissionEventScript genericMissionEventScript in this._genericMissionEvents)
					{
						if (!genericMissionEventScript.IsDisabled)
						{
							Game.Current.EventManager.TriggerEvent<GenericMissionEvent>(new GenericMissionEvent(genericMissionEventScript.EventId, genericMissionEventScript.Parameter));
						}
					}
				}
			}
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000DA974 File Offset: 0x000D8B74
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			TextObject textObject = GameTexts.FindText("str_key_action", null);
			textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13), 1f));
			textObject.SetTextVariable("ACTION", this.ActionText);
			return textObject;
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000DA9C0 File Offset: 0x000D8BC0
		public override TextObject GetDescriptionText(WeakGameEntity gameEntity)
		{
			return this.DescriptionText;
		}

		// Token: 0x04001693 RID: 5779
		public string ActivatorAgentTags;

		// Token: 0x04001694 RID: 5780
		public string ActionTextId;

		// Token: 0x04001695 RID: 5781
		public string DescriptionTextId;

		// Token: 0x04001696 RID: 5782
		private List<GenericMissionEventScript> _genericMissionEvents = new List<GenericMissionEventScript>();
	}
}

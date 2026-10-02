using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission
{
	// Token: 0x0200002E RID: 46
	[DefaultView]
	public class MissionGauntletCategoryLoadManager : MissionView, IMissionListener
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000AD9C File Offset: 0x00008F9C
		public override void AfterStart()
		{
			base.AfterStart();
			if (this._fullBackgroundCategory == null)
			{
				this._fullBackgroundCategory = UIResourceManager.GetSpriteCategory("ui_fullbackgrounds");
			}
			if (this._encyclopediaCategory == null)
			{
				this._encyclopediaCategory = UIResourceManager.GetSpriteCategory("ui_encyclopedia");
			}
			if (this._mapBarCategory == null)
			{
				SpriteCategory spriteCategory = UIResourceManager.GetSpriteCategory("ui_mapbar");
				if (spriteCategory != null && spriteCategory.IsLoaded)
				{
					this._mapBarCategory = spriteCategory;
				}
			}
			if (this._optionsView == null)
			{
				this._optionsView = base.Mission.GetMissionBehavior<MissionGauntletOptionsUIHandler>();
				base.Mission.AddListener(this);
			}
			this.HandleCategoryLoadingUnloading();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000AE2F File Offset: 0x0000902F
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			this._optionsView = null;
			base.Mission.RemoveListener(this);
			this.LoadUnloadAllCategories(true);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000AE51 File Offset: 0x00009051
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			this.HandleCategoryLoadingUnloading();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000AE60 File Offset: 0x00009060
		private void HandleCategoryLoadingUnloading()
		{
			bool flag = true;
			if (base.Mission != null)
			{
				flag = this.IsBackgroundsUsedInMission(base.Mission);
			}
			this.LoadUnloadAllCategories(flag);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000AE8C File Offset: 0x0000908C
		private void LoadUnloadAllCategories(bool load)
		{
			if (load)
			{
				if (!this._fullBackgroundCategory.IsLoaded)
				{
					this._fullBackgroundCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.ResourceDepot);
				}
				if (!this._encyclopediaCategory.IsLoaded)
				{
					this._encyclopediaCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.ResourceDepot);
				}
				SpriteCategory mapBarCategory = this._mapBarCategory;
				if (mapBarCategory != null && !mapBarCategory.IsLoaded)
				{
					this._mapBarCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.ResourceDepot);
					return;
				}
			}
			else
			{
				if (this._fullBackgroundCategory.IsLoaded)
				{
					this._fullBackgroundCategory.Unload();
				}
				if (this._encyclopediaCategory.IsLoaded)
				{
					Mission mission = base.Mission;
					if (mission == null || mission.Mode != MissionMode.Conversation)
					{
						this._encyclopediaCategory.Unload();
					}
				}
				SpriteCategory mapBarCategory2 = this._mapBarCategory;
				if (mapBarCategory2 != null && mapBarCategory2.IsLoaded)
				{
					this._mapBarCategory.Unload();
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000AF76 File Offset: 0x00009176
		private bool IsBackgroundsUsedInMission(Mission mission)
		{
			return mission.IsInventoryAccessAllowed || mission.IsCharacterWindowAccessAllowed || mission.IsClanWindowAccessAllowed || mission.IsKingdomWindowAccessAllowed || mission.IsQuestScreenAccessAllowed || mission.IsPartyWindowAccessAllowed || mission.IsEncyclopediaWindowAccessAllowed;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000AFB0 File Offset: 0x000091B0
		void IMissionListener.OnEquipItemsFromSpawnEquipmentBegin(Agent agent, Agent.CreationType creationType)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000AFB2 File Offset: 0x000091B2
		void IMissionListener.OnEquipItemsFromSpawnEquipment(Agent agent, Agent.CreationType creationType)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000AFB4 File Offset: 0x000091B4
		void IMissionListener.OnEndMission()
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000AFB6 File Offset: 0x000091B6
		void IMissionListener.OnMissionModeChange(MissionMode oldMissionMode, bool atStart)
		{
			this.HandleCategoryLoadingUnloading();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000AFBE File Offset: 0x000091BE
		void IMissionListener.OnConversationCharacterChanged()
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000AFC0 File Offset: 0x000091C0
		void IMissionListener.OnResetMission()
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000AFC2 File Offset: 0x000091C2
		void IMissionListener.OnDeploymentPlanMade(Team team, bool isFirstPlan)
		{
		}

		// Token: 0x040000F1 RID: 241
		private SpriteCategory _fullBackgroundCategory;

		// Token: 0x040000F2 RID: 242
		private SpriteCategory _mapBarCategory;

		// Token: 0x040000F3 RID: 243
		private SpriteCategory _encyclopediaCategory;

		// Token: 0x040000F4 RID: 244
		private MissionGauntletOptionsUIHandler _optionsView;
	}
}

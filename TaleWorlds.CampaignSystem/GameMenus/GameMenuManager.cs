using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.GameMenus
{
	// Token: 0x020000E8 RID: 232
	public class GameMenuManager
	{
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00061C59 File Offset: 0x0005FE59
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x00061C61 File Offset: 0x0005FE61
		public string NextGameMenuId { get; private set; }

		// Token: 0x06001585 RID: 5509 RVA: 0x00061C6A File Offset: 0x0005FE6A
		public GameMenuManager()
		{
			this.NextGameMenuId = null;
			this._gameMenus = new Dictionary<string, GameMenu>();
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00061C98 File Offset: 0x0005FE98
		public GameMenu NextMenu
		{
			get
			{
				GameMenu gameMenu;
				this._gameMenus.TryGetValue(this.NextGameMenuId, out gameMenu);
				return gameMenu;
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00061CBA File Offset: 0x0005FEBA
		public void SetNextMenu(string name)
		{
			this.NextGameMenuId = name;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00061CC3 File Offset: 0x0005FEC3
		public void ExitToLast()
		{
			if (Campaign.Current.CurrentMenuContext != null)
			{
				Game.Current.GameStateManager.LastOrDefault<MapState>().ExitMenuMode();
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00061CE5 File Offset: 0x0005FEE5
		internal object GetSelectedRepeatableObject(MenuContext menuContext)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.LastSelectedMenuObject;
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetSelectedObject");
			}
			return 0;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00061D14 File Offset: 0x0005FF14
		internal object ObjectGetCurrentRepeatableObject(MenuContext menuContext)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.CurrentRepeatableObject;
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not return CurrentRepeatableIndex");
			}
			return null;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00061D3E File Offset: 0x0005FF3E
		public void SetCurrentRepeatableIndex(MenuContext menuContext, int index)
		{
			if (menuContext.GameMenu != null)
			{
				menuContext.GameMenu.CurrentRepeatableIndex = index;
				return;
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run SetCurrentRepeatableIndex");
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00061D68 File Offset: 0x0005FF68
		public bool GetMenuOptionConditionsHold(MenuContext menuContext, int menuItemNumber)
		{
			if (menuContext.GameMenu != null)
			{
				if (Game.Current == null)
				{
					throw new MBNullParameterException("Game");
				}
				return menuContext.GameMenu.GetMenuOptionConditionsHold(Game.Current, menuContext, menuItemNumber);
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run GetMenuOptionConditionsHold");
				}
				return false;
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00061DB8 File Offset: 0x0005FFB8
		public void RefreshMenuOptions(MenuContext menuContext)
		{
			if (menuContext.GameMenu == null)
			{
				Debug.FailedAssert("Current game menu empty, can not run RefreshMenuOptions", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\GameMenus\\GameMenuManager.cs", "RefreshMenuOptions", 143);
				return;
			}
			if (Game.Current == null)
			{
				Debug.FailedAssert("Game is null during RefreshMenuOptions", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\GameMenus\\GameMenuManager.cs", "RefreshMenuOptions", 148);
				return;
			}
			menuContext.Handler.OnMenuRefresh();
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00061E14 File Offset: 0x00060014
		public void RefreshMenuOptionConditions(MenuContext menuContext)
		{
			if (menuContext.GameMenu == null)
			{
				Debug.FailedAssert("Current game menu empty, can not run RefreshMenuOptionConditions", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\GameMenus\\GameMenuManager.cs", "RefreshMenuOptionConditions", 161);
				return;
			}
			if (Game.Current == null)
			{
				Debug.FailedAssert("Game is null during RefreshMenuOptionConditions", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\GameMenus\\GameMenuManager.cs", "RefreshMenuOptionConditions", 166);
				return;
			}
			int virtualMenuOptionAmount = Campaign.Current.GameMenuManager.GetVirtualMenuOptionAmount(menuContext);
			for (int i = 0; i < virtualMenuOptionAmount; i++)
			{
				this.GetMenuOptionConditionsHold(menuContext, i);
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00061E8A File Offset: 0x0006008A
		public string GetMenuOptionIdString(MenuContext menuContext, int menuItemNumber)
		{
			if (menuContext.GameMenu == null)
			{
				Debug.FailedAssert("Current game menu empty, can not run GetMenuOptionIdString", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\GameMenus\\GameMenuManager.cs", "GetMenuOptionIdString", 183);
				return "";
			}
			return menuContext.GameMenu.GetMenuOptionIdString(menuItemNumber);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00061EBF File Offset: 0x000600BF
		internal bool GetMenuOptionIsLeave(MenuContext menuContext, int menuItemNumber)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.GetMenuOptionIsLeave(menuItemNumber);
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetMenuOptionText");
			}
			return false;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00061EEA File Offset: 0x000600EA
		public void RunConsequencesOfMenuOption(MenuContext menuContext, int menuItemNumber)
		{
			if (menuContext.GameMenu != null)
			{
				if (Game.Current == null)
				{
					throw new MBNullParameterException("Game");
				}
				menuContext.GameMenu.RunMenuOptionConsequence(menuContext, menuItemNumber);
				return;
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run RunConsequencesOfMenuOption");
				}
				return;
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00061F27 File Offset: 0x00060127
		internal void SetRepeatObjectList(MenuContext menuContext, IEnumerable<object> list)
		{
			if (menuContext.GameMenu != null)
			{
				menuContext.GameMenu.SetMenuRepeatObjects(list);
				return;
			}
			Debug.FailedAssert("false", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\GameMenus\\GameMenuManager.cs", "SetRepeatObjectList", 237);
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00061F58 File Offset: 0x00060158
		public TextObject GetVirtualMenuOptionTooltip(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu != null && !menuContext.GameMenu.IsEmpty)
			{
				int num = ((menuContext.GameMenu.MenuRepeatObjects.Count > 0) ? menuContext.GameMenu.MenuRepeatObjects.Count : 1);
				if (virtualMenuItemIndex < num)
				{
					return this.GetMenuOptionTooltip(menuContext, 0);
				}
				return this.GetMenuOptionTooltip(menuContext, virtualMenuItemIndex + 1 - num);
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionText");
				}
				return null;
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00061FCF File Offset: 0x000601CF
		public GameMenu.MenuOverlayType GetMenuOverlayType(MenuContext menuContext)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.OverlayType;
			}
			return GameMenu.MenuOverlayType.SettlementWithCharacters;
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00061FE8 File Offset: 0x000601E8
		public TextObject GetVirtualMenuOptionText(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu != null && !menuContext.GameMenu.IsEmpty)
			{
				int num = ((menuContext.GameMenu.MenuRepeatObjects.Count > 0) ? menuContext.GameMenu.MenuRepeatObjects.Count : 1);
				if (virtualMenuItemIndex < num)
				{
					return this.GetMenuOptionText(menuContext, 0);
				}
				return this.GetMenuOptionText(menuContext, virtualMenuItemIndex + 1 - num);
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionText");
				}
				return null;
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00062060 File Offset: 0x00060260
		public GameMenuOption GetVirtualGameMenuOption(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetGameMenuOption");
			}
			int num = ((menuContext.GameMenu.MenuRepeatObjects.Count > 0) ? menuContext.GameMenu.MenuRepeatObjects.Count : 1);
			if (virtualMenuItemIndex < num)
			{
				return menuContext.GameMenu.GetGameMenuOption(0);
			}
			return menuContext.GameMenu.GetGameMenuOption(virtualMenuItemIndex + 1 - num);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000620C8 File Offset: 0x000602C8
		public TextObject GetVirtualMenuOptionText2(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu != null && !menuContext.GameMenu.IsEmpty)
			{
				int num = ((menuContext.GameMenu.MenuRepeatObjects.Count > 0) ? menuContext.GameMenu.MenuRepeatObjects.Count : 1);
				if (virtualMenuItemIndex < num)
				{
					return this.GetMenuOptionText2(menuContext, 0);
				}
				return this.GetMenuOptionText2(menuContext, virtualMenuItemIndex + 1 - num);
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionText");
				}
				return null;
			}
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0006213F File Offset: 0x0006033F
		public float GetVirtualMenuProgress(MenuContext menuContext)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.Progress;
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionText");
			}
			return 0f;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0006216D File Offset: 0x0006036D
		public GameMenu.MenuAndOptionType GetVirtualMenuAndOptionType(MenuContext menuContext)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.Type;
			}
			return GameMenu.MenuAndOptionType.RegularMenuOption;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00062184 File Offset: 0x00060384
		public bool GetVirtualMenuIsWaitActive(MenuContext menuContext)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.IsWaitActive;
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionText");
			}
			return false;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x000621AE File Offset: 0x000603AE
		public float GetVirtualMenuTargetWaitHours(MenuContext menuContext)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.TargetWaitHours;
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionText");
			}
			return 0f;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x000621DC File Offset: 0x000603DC
		public bool GetVirtualMenuOptionIsEnabled(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu != null && !menuContext.GameMenu.IsEmpty)
			{
				int num = ((menuContext.GameMenu.MenuRepeatObjects.Count > 0) ? menuContext.GameMenu.MenuRepeatObjects.Count : 1);
				if (virtualMenuItemIndex < num)
				{
					return menuContext.GameMenu.MenuOptions.ElementAt<GameMenuOption>(0).IsEnabled;
				}
				return menuContext.GameMenu.MenuOptions.ElementAt<GameMenuOption>(virtualMenuItemIndex + 1 - num).IsEnabled;
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionText");
				}
				return false;
			}
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00062270 File Offset: 0x00060470
		public int GetVirtualMenuOptionAmount(MenuContext menuContext)
		{
			if (menuContext.GameMenu != null)
			{
				int count = menuContext.GameMenu.MenuRepeatObjects.Count;
				int menuItemAmount = menuContext.GameMenu.MenuItemAmount;
				if (count == 0)
				{
					return menuItemAmount;
				}
				return menuItemAmount - 1 + count;
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionAmount");
				}
				return 0;
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x000622C4 File Offset: 0x000604C4
		public bool GetVirtualMenuOptionIsLeave(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu != null && !menuContext.GameMenu.IsEmpty)
			{
				int num = ((menuContext.GameMenu.MenuRepeatObjects.Count > 0) ? menuContext.GameMenu.MenuRepeatObjects.Count : 1);
				if (virtualMenuItemIndex < num)
				{
					return this.GetMenuOptionIsLeave(menuContext, 0);
				}
				return this.GetMenuOptionIsLeave(menuContext, virtualMenuItemIndex + 1 - num);
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionText");
				}
				return false;
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0006233B File Offset: 0x0006053B
		public GameMenuOption GetLeaveMenuOption(MenuContext menuContext)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.GetLeaveMenuOption(Game.Current, menuContext);
			}
			return null;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00062358 File Offset: 0x00060558
		internal void RunConsequenceOfVirtualMenuOption(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu != null)
			{
				int num = ((menuContext.GameMenu.MenuRepeatObjects.Count > 0) ? menuContext.GameMenu.MenuRepeatObjects.Count : 1);
				if (virtualMenuItemIndex < num)
				{
					if (menuContext.GameMenu.MenuRepeatObjects.Count > 0)
					{
						menuContext.GameMenu.LastSelectedMenuObject = menuContext.GameMenu.MenuRepeatObjects[virtualMenuItemIndex];
					}
					this.RunConsequencesOfMenuOption(menuContext, 0);
					return;
				}
				this.RunConsequencesOfMenuOption(menuContext, virtualMenuItemIndex + 1 - num);
				return;
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run RunVirtualMenuItemConsequence");
				}
				return;
			}
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x000623F0 File Offset: 0x000605F0
		public bool GetVirtualMenuOptionConditionsHold(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu != null && !menuContext.GameMenu.IsEmpty)
			{
				int num = ((menuContext.GameMenu.MenuRepeatObjects.Count > 0) ? menuContext.GameMenu.MenuRepeatObjects.Count : 1);
				if (virtualMenuItemIndex < num)
				{
					return this.GetMenuOptionConditionsHold(menuContext, 0);
				}
				return this.GetMenuOptionConditionsHold(menuContext, virtualMenuItemIndex + 1 - num);
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionConditionsHold");
				}
				return false;
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00062467 File Offset: 0x00060667
		public void OnFrameTick(MenuContext menuContext, float dt)
		{
			if (menuContext.GameMenu != null)
			{
				menuContext.GameMenu.RunOnTick(menuContext, dt);
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0006247E File Offset: 0x0006067E
		public TextObject GetMenuText(MenuContext menuContext)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.GetText();
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetMenuText");
			}
			return null;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x000624A8 File Offset: 0x000606A8
		private TextObject GetMenuOptionText(MenuContext menuContext, int menuItemNumber)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.GetMenuOptionText(menuItemNumber);
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetMenuOptionText");
			}
			return null;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000624D3 File Offset: 0x000606D3
		private TextObject GetMenuOptionText2(MenuContext menuContext, int menuItemNumber)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.GetMenuOptionText2(menuItemNumber);
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetMenuOptionText");
			}
			return null;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x000624FE File Offset: 0x000606FE
		private TextObject GetMenuOptionTooltip(MenuContext menuContext, int menuItemNumber)
		{
			if (menuContext.GameMenu != null && !menuContext.GameMenu.IsEmpty)
			{
				return menuContext.GameMenu.GetMenuOptionTooltip(menuItemNumber);
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetMenuOptionText");
			}
			return null;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00062536 File Offset: 0x00060736
		public void AddGameMenu(GameMenu gameMenu)
		{
			this._gameMenus.Add(gameMenu.StringId, gameMenu);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0006254C File Offset: 0x0006074C
		public void RemoveRelatedGameMenus(object relatedObject)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, GameMenu> keyValuePair in this._gameMenus)
			{
				if (keyValuePair.Value.RelatedObject == relatedObject)
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (string text in list)
			{
				this._gameMenus.Remove(text);
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00062600 File Offset: 0x00060800
		public void RemoveRelatedGameMenuOptions(object relatedObject)
		{
			foreach (KeyValuePair<string, GameMenu> keyValuePair in this._gameMenus.ToList<KeyValuePair<string, GameMenu>>())
			{
				foreach (GameMenuOption gameMenuOption in keyValuePair.Value.MenuOptions.ToList<GameMenuOption>())
				{
					if (gameMenuOption.RelatedObject == relatedObject)
					{
						keyValuePair.Value.RemoveMenuOption(gameMenuOption);
					}
				}
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x000626B0 File Offset: 0x000608B0
		internal void UnregisterNonReadyObjects()
		{
			MBList<KeyValuePair<string, GameMenu>> mblist = this._gameMenus.ToMBList<KeyValuePair<string, GameMenu>>();
			for (int i = mblist.Count - 1; i >= 0; i--)
			{
				if (!mblist[i].Value.IsReady)
				{
					this._gameMenus.Remove(mblist[i].Key);
				}
			}
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00062710 File Offset: 0x00060910
		public GameMenu GetGameMenu(string menuId)
		{
			GameMenu gameMenu;
			this._gameMenus.TryGetValue(menuId, out gameMenu);
			return gameMenu;
		}

		// Token: 0x04000719 RID: 1817
		private Dictionary<string, GameMenu> _gameMenus;

		// Token: 0x0400071B RID: 1819
		public int PreviouslySelectedGameMenuItem = -1;

		// Token: 0x0400071C RID: 1820
		public Location NextLocation;

		// Token: 0x0400071D RID: 1821
		public Location PreviousLocation;

		// Token: 0x0400071E RID: 1822
		public List<Location> MenuLocations = new List<Location>();

		// Token: 0x0400071F RID: 1823
		public object PreviouslySelectedGameMenuObject;
	}
}

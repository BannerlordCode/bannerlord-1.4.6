using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.CampaignSystem.GameMenus
{
	// Token: 0x020000E5 RID: 229
	public class GameMenuCallbackManager
	{
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x00061722 File Offset: 0x0005F922
		public static GameMenuCallbackManager Instance
		{
			get
			{
				return Campaign.Current.GameMenuCallbackManager;
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0006172E File Offset: 0x0005F92E
		public GameMenuCallbackManager()
		{
			this.FillInitializationHandlers();
			this.FillEventHandlers();
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00061744 File Offset: 0x0005F944
		private void FillInitializationHandlers()
		{
			this._gameMenuInitializationHandlers = new Dictionary<string, GameMenuInitializationHandlerDelegate>();
			Assembly assembly = typeof(GameMenuInitializationHandler).Assembly;
			this.FillInitializationHandlerWith(assembly);
			foreach (Assembly assembly2 in GameMenuCallbackManager.GetAssemblies())
			{
				this.FillInitializationHandlerWith(assembly2);
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00061792 File Offset: 0x0005F992
		private static Assembly[] GetAssemblies()
		{
			return typeof(GameMenu).Assembly.GetActiveReferencingGameAssembliesSafe();
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000617A8 File Offset: 0x0005F9A8
		public void OnGameLoad()
		{
			this.FillInitializationHandlers();
			this.FillEventHandlers();
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x000617B8 File Offset: 0x0005F9B8
		private void FillInitializationHandlerWith(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypesSafe(null))
			{
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					object[] customAttributesSafe = methodInfo.GetCustomAttributesSafe(typeof(GameMenuInitializationHandler), false);
					if (customAttributesSafe != null && customAttributesSafe.Length != 0)
					{
						foreach (GameMenuInitializationHandler gameMenuInitializationHandler in customAttributesSafe)
						{
							GameMenuInitializationHandlerDelegate gameMenuInitializationHandlerDelegate = Delegate.CreateDelegate(typeof(GameMenuInitializationHandlerDelegate), methodInfo) as GameMenuInitializationHandlerDelegate;
							if (!this._gameMenuInitializationHandlers.ContainsKey(gameMenuInitializationHandler.MenuId))
							{
								this._gameMenuInitializationHandlers.Add(gameMenuInitializationHandler.MenuId, gameMenuInitializationHandlerDelegate);
							}
							else
							{
								this._gameMenuInitializationHandlers[gameMenuInitializationHandler.MenuId] = gameMenuInitializationHandlerDelegate;
							}
						}
					}
				}
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000618C0 File Offset: 0x0005FAC0
		private void FillEventHandlers()
		{
			this._eventHandlers = new Dictionary<string, Dictionary<string, GameMenuEventHandlerDelegate>>();
			Assembly assembly = typeof(GameMenuEventHandler).Assembly;
			this.FillEventHandlersWith(assembly);
			foreach (Assembly assembly2 in GameMenuCallbackManager.GetAssemblies())
			{
				this.FillEventHandlersWith(assembly2);
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00061910 File Offset: 0x0005FB10
		private void FillEventHandlersWith(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypesSafe(null))
			{
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					object[] customAttributesSafe = methodInfo.GetCustomAttributesSafe(typeof(GameMenuEventHandler), false);
					if (customAttributesSafe != null && customAttributesSafe.Length != 0)
					{
						foreach (GameMenuEventHandler gameMenuEventHandler in customAttributesSafe)
						{
							GameMenuEventHandlerDelegate gameMenuEventHandlerDelegate = Delegate.CreateDelegate(typeof(GameMenuEventHandlerDelegate), methodInfo) as GameMenuEventHandlerDelegate;
							Dictionary<string, GameMenuEventHandlerDelegate> dictionary;
							if (!this._eventHandlers.TryGetValue(gameMenuEventHandler.MenuId, out dictionary))
							{
								dictionary = new Dictionary<string, GameMenuEventHandlerDelegate>();
								this._eventHandlers.Add(gameMenuEventHandler.MenuId, dictionary);
							}
							if (!dictionary.ContainsKey(gameMenuEventHandler.MenuOptionId))
							{
								dictionary.Add(gameMenuEventHandler.MenuOptionId, gameMenuEventHandlerDelegate);
							}
							else
							{
								dictionary[gameMenuEventHandler.MenuOptionId] = gameMenuEventHandlerDelegate;
							}
						}
					}
				}
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00061A48 File Offset: 0x0005FC48
		public void InitializeState(string menuId, MenuContext state)
		{
			GameMenuInitializationHandlerDelegate gameMenuInitializationHandlerDelegate = null;
			if (this._gameMenuInitializationHandlers.TryGetValue(menuId, out gameMenuInitializationHandlerDelegate))
			{
				MenuCallbackArgs menuCallbackArgs = new MenuCallbackArgs(state, null);
				gameMenuInitializationHandlerDelegate(menuCallbackArgs);
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00061A78 File Offset: 0x0005FC78
		public void OnConsequence(string menuId, GameMenuOption gameMenuOption, MenuContext state)
		{
			Dictionary<string, GameMenuEventHandlerDelegate> dictionary = null;
			if (this._eventHandlers.TryGetValue(menuId, out dictionary))
			{
				GameMenuEventHandlerDelegate gameMenuEventHandlerDelegate = null;
				if (dictionary.TryGetValue(gameMenuOption.IdString, out gameMenuEventHandlerDelegate))
				{
					MenuCallbackArgs menuCallbackArgs = new MenuCallbackArgs(state, gameMenuOption.Text);
					gameMenuEventHandlerDelegate(menuCallbackArgs);
				}
			}
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00061ABD File Offset: 0x0005FCBD
		public TextObject GetMenuOptionTooltip(MenuContext menuContext, int menuItemNumber)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.GetMenuOptionTooltip(menuItemNumber);
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetMenuOptionText");
			}
			return null;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00061AE8 File Offset: 0x0005FCE8
		public TextObject GetVirtualMenuOptionTooltip(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu != null)
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

		// Token: 0x06001577 RID: 5495 RVA: 0x00061B54 File Offset: 0x0005FD54
		public TextObject GetVirtualMenuOptionText(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu != null)
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

		// Token: 0x06001578 RID: 5496 RVA: 0x00061BBE File Offset: 0x0005FDBE
		public TextObject GetMenuOptionText(MenuContext menuContext, int menuItemNumber)
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

		// Token: 0x04000713 RID: 1811
		private Dictionary<string, GameMenuInitializationHandlerDelegate> _gameMenuInitializationHandlers;

		// Token: 0x04000714 RID: 1812
		private Dictionary<string, Dictionary<string, GameMenuEventHandlerDelegate>> _eventHandlers;
	}
}

using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.View.Map.Navigation.NavigationElements
{
	// Token: 0x0200006C RID: 108
	public class EscapeMenuNavigationElement : MapNavigationElementBase
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00024C16 File Offset: 0x00022E16
		public override string StringId
		{
			get
			{
				return "escape_menu";
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00024C1D File Offset: 0x00022E1D
		public override bool IsActive
		{
			get
			{
				if (base._game.GameStateManager.ActiveState is MapState)
				{
					MapScreen instance = MapScreen.Instance;
					return instance != null && instance.IsEscapeMenuOpened;
				}
				return false;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00024C48 File Offset: 0x00022E48
		public override bool IsLockingNavigation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00024C4B File Offset: 0x00022E4B
		public override bool HasAlert
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00024C4E File Offset: 0x00022E4E
		public EscapeMenuNavigationElement(MapNavigationHandler handler)
			: base(handler)
		{
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00024C58 File Offset: 0x00022E58
		protected override NavigationPermissionItem GetPermission()
		{
			if (!MapNavigationHelper.IsNavigationBarEnabled(this._handler))
			{
				return new NavigationPermissionItem(false, null);
			}
			if (this.IsActive)
			{
				return new NavigationPermissionItem(false, null);
			}
			return new NavigationPermissionItem(base._game.GameStateManager.ActiveState is MapState, null);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00024CA8 File Offset: 0x00022EA8
		protected override TextObject GetTooltip()
		{
			if (!Input.IsGamepadActive && (base.Permission.IsAuthorized || this.IsActive))
			{
				string text = Game.Current.GameTextManager.GetHotKeyGameText("GenericPanelGameKeyCategory", "ToggleEscapeMenu").ToString();
				TextObject textObject = GameTexts.FindText("str_hotkey_with_hint", null);
				textObject.SetTextVariable("TEXT", GameTexts.FindText("str_escape_menu", null).ToString());
				textObject.SetTextVariable("HOTKEY", text);
				return textObject;
			}
			return GameTexts.FindText("str_escape_menu", null);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00024D33 File Offset: 0x00022F33
		protected override TextObject GetAlertTooltip()
		{
			return TextObject.GetEmpty();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00024D3C File Offset: 0x00022F3C
		public override void OpenView()
		{
			if (base.Permission.IsAuthorized)
			{
				MapScreen instance = MapScreen.Instance;
				if (instance == null)
				{
					return;
				}
				instance.OpenEscapeMenu();
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00024D68 File Offset: 0x00022F68
		public override void OpenView(params object[] parameters)
		{
			Debug.FailedAssert("Escape menu shouldn't be opened with parameters from navigation", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\SandBox.View\\Map\\Navigation\\NavigationElements\\EscapeMenuNavigationElement.cs", "OpenView", 70);
			this.OpenView();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00024D86 File Offset: 0x00022F86
		public override void GoToLink()
		{
		}
	}
}

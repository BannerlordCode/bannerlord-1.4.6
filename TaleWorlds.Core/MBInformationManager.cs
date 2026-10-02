using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.Core
{
	// Token: 0x020000AF RID: 175
	public static class MBInformationManager
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000932 RID: 2354 RVA: 0x0001E1D8 File Offset: 0x0001C3D8
		// (remove) Token: 0x06000933 RID: 2355 RVA: 0x0001E20C File Offset: 0x0001C40C
		public static event Action<string, int, BasicCharacterObject, Equipment, string> FiringQuickInformation;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000934 RID: 2356 RVA: 0x0001E240 File Offset: 0x0001C440
		// (remove) Token: 0x06000935 RID: 2357 RVA: 0x0001E274 File Offset: 0x0001C474
		public static event Action ClearingQuickInformations;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000936 RID: 2358 RVA: 0x0001E2A8 File Offset: 0x0001C4A8
		// (remove) Token: 0x06000937 RID: 2359 RVA: 0x0001E2DC File Offset: 0x0001C4DC
		public static event Action<MultiSelectionInquiryData, bool, bool> OnShowMultiSelectionInquiry;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000938 RID: 2360 RVA: 0x0001E310 File Offset: 0x0001C510
		// (remove) Token: 0x06000939 RID: 2361 RVA: 0x0001E344 File Offset: 0x0001C544
		public static event Action<InformationData> OnAddMapNotice;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600093A RID: 2362 RVA: 0x0001E378 File Offset: 0x0001C578
		// (remove) Token: 0x0600093B RID: 2363 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		public static event Action<InformationData> OnRemoveMapNotice;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600093C RID: 2364 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		// (remove) Token: 0x0600093D RID: 2365 RVA: 0x0001E414 File Offset: 0x0001C614
		public static event Action<SceneNotificationData> OnShowSceneNotification;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600093E RID: 2366 RVA: 0x0001E448 File Offset: 0x0001C648
		// (remove) Token: 0x0600093F RID: 2367 RVA: 0x0001E47C File Offset: 0x0001C67C
		public static event Action OnHideSceneNotification;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000940 RID: 2368 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		// (remove) Token: 0x06000941 RID: 2369 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
		public static event Func<bool> IsAnySceneNotificationActive;

		// Token: 0x06000942 RID: 2370 RVA: 0x0001E517 File Offset: 0x0001C717
		public static void AddQuickInformation(TextObject message, int extraTimeInMs = 0, BasicCharacterObject announcerCharacter = null, Equipment equipment = null, string soundEventPath = "")
		{
			Action<string, int, BasicCharacterObject, Equipment, string> firingQuickInformation = MBInformationManager.FiringQuickInformation;
			if (firingQuickInformation != null)
			{
				firingQuickInformation(message.ToString(), extraTimeInMs, announcerCharacter, equipment, soundEventPath);
			}
			Debug.Print(message.ToString(), 0, Debug.DebugColor.White, 1125899906842624UL);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001E54B File Offset: 0x0001C74B
		public static void ClearQuickInformations()
		{
			Action clearingQuickInformations = MBInformationManager.ClearingQuickInformations;
			if (clearingQuickInformations == null)
			{
				return;
			}
			clearingQuickInformations();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001E55C File Offset: 0x0001C75C
		public static void ShowMultiSelectionInquiry(MultiSelectionInquiryData data, bool pauseGameActiveState = false, bool prioritize = false)
		{
			Action<MultiSelectionInquiryData, bool, bool> onShowMultiSelectionInquiry = MBInformationManager.OnShowMultiSelectionInquiry;
			if (onShowMultiSelectionInquiry == null)
			{
				return;
			}
			onShowMultiSelectionInquiry(data, pauseGameActiveState, prioritize);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001E570 File Offset: 0x0001C770
		public static void AddNotice(InformationData data)
		{
			Action<InformationData> onAddMapNotice = MBInformationManager.OnAddMapNotice;
			if (onAddMapNotice == null)
			{
				return;
			}
			onAddMapNotice(data);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001E582 File Offset: 0x0001C782
		public static void MapNoticeRemoved(InformationData data)
		{
			Action<InformationData> onRemoveMapNotice = MBInformationManager.OnRemoveMapNotice;
			if (onRemoveMapNotice == null)
			{
				return;
			}
			onRemoveMapNotice(data);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001E594 File Offset: 0x0001C794
		public static void ShowHint(string hint)
		{
			InformationManager.ShowTooltip(typeof(string), new object[] { hint });
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001E5AF File Offset: 0x0001C7AF
		public static void HideInformations()
		{
			InformationManager.HideTooltip();
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001E5B6 File Offset: 0x0001C7B6
		public static void ShowSceneNotification(SceneNotificationData data)
		{
			Action<SceneNotificationData> onShowSceneNotification = MBInformationManager.OnShowSceneNotification;
			if (onShowSceneNotification == null)
			{
				return;
			}
			onShowSceneNotification(data);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001E5C8 File Offset: 0x0001C7C8
		public static void HideSceneNotification()
		{
			Action onHideSceneNotification = MBInformationManager.OnHideSceneNotification;
			if (onHideSceneNotification == null)
			{
				return;
			}
			onHideSceneNotification();
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001E5DC File Offset: 0x0001C7DC
		public static bool? GetIsAnySceneNotificationActive()
		{
			Func<bool> isAnySceneNotificationActive = MBInformationManager.IsAnySceneNotificationActive;
			if (isAnySceneNotificationActive == null)
			{
				return null;
			}
			return new bool?(isAnySceneNotificationActive());
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001E606 File Offset: 0x0001C806
		public static void Clear()
		{
			MBInformationManager.FiringQuickInformation = null;
			MBInformationManager.OnShowMultiSelectionInquiry = null;
			MBInformationManager.OnAddMapNotice = null;
			MBInformationManager.OnRemoveMapNotice = null;
			MBInformationManager.OnShowSceneNotification = null;
			MBInformationManager.OnHideSceneNotification = null;
		}

		// Token: 0x02000120 RID: 288
		public enum NotificationPriority
		{
			// Token: 0x040007B3 RID: 1971
			Lowest,
			// Token: 0x040007B4 RID: 1972
			Low,
			// Token: 0x040007B5 RID: 1973
			Medium,
			// Token: 0x040007B6 RID: 1974
			High,
			// Token: 0x040007B7 RID: 1975
			Highest
		}

		// Token: 0x02000121 RID: 289
		public enum NotificationStatus
		{
			// Token: 0x040007B9 RID: 1977
			Inactive,
			// Token: 0x040007BA RID: 1978
			CurrentlyActive,
			// Token: 0x040007BB RID: 1979
			InQueue
		}

		// Token: 0x02000122 RID: 290
		public class DialogNotificationHandle
		{
		}
	}
}

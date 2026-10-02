using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.SaveSystem;
using TaleWorlds.SaveSystem.Load;

namespace TaleWorlds.Core
{
	// Token: 0x020000B6 RID: 182
	public static class MBSaveLoad
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0001EC5E File Offset: 0x0001CE5E
		public static char ModuleVersionSeperator
		{
			get
			{
				return ':';
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0001EC62 File Offset: 0x0001CE62
		public static char ModuleCodeSeperator
		{
			get
			{
				return ';';
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0001EC66 File Offset: 0x0001CE66
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0001EC6D File Offset: 0x0001CE6D
		public static ApplicationVersion LastLoadedGameVersion { get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0001EC75 File Offset: 0x0001CE75
		public static ApplicationVersion CurrentVersion { get; } = ApplicationVersion.FromParametersFile(null);

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0001EC7C File Offset: 0x0001CE7C
		public static bool IsUpdatingGameVersion
		{
			get
			{
				return MBSaveLoad.LastLoadedGameVersion.IsOlderThan(MBSaveLoad.CurrentVersion);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0001EC9B File Offset: 0x0001CE9B
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0001ECA2 File Offset: 0x0001CEA2
		public static int NumberOfCurrentSaves { get; private set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0001ECAA File Offset: 0x0001CEAA
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0001ECB1 File Offset: 0x0001CEB1
		public static string ActiveSaveSlotName { get; private set; } = null;

		// Token: 0x06000977 RID: 2423 RVA: 0x0001ECB9 File Offset: 0x0001CEB9
		private static string GetAutoSaveName()
		{
			return MBSaveLoad.AutoSaveNamePrefix + MBSaveLoad.AutoSaveIndex;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001ECCF File Offset: 0x0001CECF
		private static void IncrementAutoSaveIndex()
		{
			MBSaveLoad.AutoSaveIndex++;
			if (MBSaveLoad.AutoSaveIndex > 3)
			{
				MBSaveLoad.AutoSaveIndex = 1;
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001ECEC File Offset: 0x0001CEEC
		private static void InitializeAutoSaveIndex(string saveName)
		{
			string text = string.Empty;
			if (saveName.StartsWith(MBSaveLoad.AutoSaveNamePrefix))
			{
				text = saveName;
			}
			else
			{
				foreach (string text2 in MBSaveLoad.GetSaveFileNames())
				{
					if (text2.StartsWith(MBSaveLoad.AutoSaveNamePrefix))
					{
						text = text2;
						break;
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				MBSaveLoad.AutoSaveIndex = 1;
				return;
			}
			int num;
			if (int.TryParse(text.Substring(MBSaveLoad.AutoSaveNamePrefix.Length), out num) && num > 0 && num <= 3)
			{
				MBSaveLoad.AutoSaveIndex = num;
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001ED72 File Offset: 0x0001CF72
		public static void SetSaveDriver(ISaveDriver saveDriver)
		{
			MBSaveLoad._saveDriver = saveDriver;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001ED7C File Offset: 0x0001CF7C
		public static SaveGameFileInfo[] GetSaveFiles(Func<SaveGameFileInfo, bool> condition = null)
		{
			SaveGameFileInfo[] saveGameFileInfos = MBSaveLoad._saveDriver.GetSaveGameFileInfos();
			MBSaveLoad.NumberOfCurrentSaves = saveGameFileInfos.Length;
			List<SaveGameFileInfo> list = new List<SaveGameFileInfo>();
			foreach (SaveGameFileInfo saveGameFileInfo in saveGameFileInfos)
			{
				if (condition == null || condition(saveGameFileInfo))
				{
					list.Add(saveGameFileInfo);
				}
			}
			return list.OrderByDescending<SaveGameFileInfo, DateTime>((SaveGameFileInfo info) => info.MetaData.GetCreationTime()).ToArray<SaveGameFileInfo>();
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001EDF1 File Offset: 0x0001CFF1
		public static bool IsSaveGameFileExists(string saveFileName)
		{
			return MBSaveLoad._saveDriver.IsSaveGameFileExists(saveFileName);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001EDFE File Offset: 0x0001CFFE
		public static string[] GetSaveFileNames()
		{
			return MBSaveLoad._saveDriver.GetSaveGameFileNames();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001EE0C File Offset: 0x0001D00C
		public static LoadResult LoadSaveGameData(string saveName)
		{
			MBSaveLoad.InitializeAutoSaveIndex(saveName);
			ISaveDriver saveDriver = MBSaveLoad._saveDriver;
			LoadResult loadResult = SaveManager.Load(saveName, saveDriver, true);
			if (loadResult.Successful)
			{
				MBSaveLoad.ActiveSaveSlotName = saveName;
				return loadResult;
			}
			Debug.Print("Error: Could not load the game!", 0, Debug.DebugColor.White, 17592186044416UL);
			return null;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001EE58 File Offset: 0x0001D058
		public static SaveGameFileInfo GetSaveFileWithName(string saveName)
		{
			SaveGameFileInfo[] saveFiles = MBSaveLoad.GetSaveFiles((SaveGameFileInfo x) => x.Name.Equals(saveName));
			if (saveFiles.Length == 0)
			{
				return null;
			}
			return saveFiles[0];
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001EE8B File Offset: 0x0001D08B
		public static void QuickSaveCurrentGame(CampaignSaveMetaDataArgs campaignMetaData, Action<ValueTuple<SaveResult, string>> onSaveCompleted)
		{
			if (MBSaveLoad.ActiveSaveSlotName == null)
			{
				MBSaveLoad.ActiveSaveSlotName = MBSaveLoad.GetNextAvailableSaveName();
			}
			Debug.Print("QuickSaveCurrentGame: " + MBSaveLoad.ActiveSaveSlotName, 0, Debug.DebugColor.White, 17592186044416UL);
			MBSaveLoad.OverwriteSaveAux(campaignMetaData, MBSaveLoad.ActiveSaveSlotName, onSaveCompleted);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0001EECC File Offset: 0x0001D0CC
		public static void AutoSaveCurrentGame(CampaignSaveMetaDataArgs campaignMetaData, Action<ValueTuple<SaveResult, string>> onSaveCompleted)
		{
			MBSaveLoad.IncrementAutoSaveIndex();
			string autoSaveName = MBSaveLoad.GetAutoSaveName();
			Debug.Print("AutoSaveCurrentGame: " + autoSaveName, 0, Debug.DebugColor.White, 17592186044416UL);
			MBSaveLoad.OverwriteSaveAux(campaignMetaData, autoSaveName, onSaveCompleted);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001EF08 File Offset: 0x0001D108
		public static void SaveAsCurrentGame(CampaignSaveMetaDataArgs campaignMetaData, string saveName, Action<ValueTuple<SaveResult, string>> onSaveCompleted)
		{
			MBSaveLoad.ActiveSaveSlotName = saveName;
			Debug.Print("SaveAsCurrentGame: " + saveName, 0, Debug.DebugColor.White, 17592186044416UL);
			MBSaveLoad.OverwriteSaveAux(campaignMetaData, saveName, onSaveCompleted);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001EF34 File Offset: 0x0001D134
		private static void OverwriteSaveAux(CampaignSaveMetaDataArgs campaignMetaData, string saveName, Action<ValueTuple<SaveResult, string>> onSaveCompleted)
		{
			MetaData saveMetaData = MBSaveLoad.GetSaveMetaData(campaignMetaData);
			bool isOverwritingExistingSave = MBSaveLoad.IsSaveGameFileExists(saveName);
			if (!MBSaveLoad.IsMaxNumberOfSavesReached() || isOverwritingExistingSave)
			{
				MBSaveLoad.OverwriteSaveFile(saveMetaData, saveName, delegate(SaveResult r)
				{
					Action<ValueTuple<SaveResult, string>> onSaveCompleted3 = onSaveCompleted;
					if (onSaveCompleted3 != null)
					{
						onSaveCompleted3(new ValueTuple<SaveResult, string>(r, saveName));
					}
					if (r == SaveResult.Success && !isOverwritingExistingSave)
					{
						MBSaveLoad.NumberOfCurrentSaves++;
					}
				});
				return;
			}
			Action<ValueTuple<SaveResult, string>> onSaveCompleted2 = onSaveCompleted;
			if (onSaveCompleted2 == null)
			{
				return;
			}
			onSaveCompleted2(new ValueTuple<SaveResult, string>(SaveResult.SaveLimitReached, string.Empty));
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		public static bool DeleteSaveGame(string saveName)
		{
			bool flag = MBSaveLoad._saveDriver.Delete(saveName);
			if (flag)
			{
				if (MBSaveLoad.NumberOfCurrentSaves > 0)
				{
					MBSaveLoad.NumberOfCurrentSaves--;
				}
				if (MBSaveLoad.ActiveSaveSlotName == saveName)
				{
					MBSaveLoad.ActiveSaveSlotName = null;
				}
			}
			return flag;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001EFE7 File Offset: 0x0001D1E7
		public static void Initialize(GameTextManager localizedTextProvider)
		{
			MBSaveLoad._textProvider = localizedTextProvider;
			MBSaveLoad.NumberOfCurrentSaves = MBSaveLoad._saveDriver.GetSaveGameFileInfos().Length;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001F000 File Offset: 0x0001D200
		public static void OnNewGame()
		{
			MBSaveLoad.ActiveSaveSlotName = null;
			MBSaveLoad.LastLoadedGameVersion = MBSaveLoad.CurrentVersion;
			MBSaveLoad.AutoSaveIndex = 0;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001F018 File Offset: 0x0001D218
		public static void OnGameDestroy()
		{
			MBSaveLoad.LastLoadedGameVersion = ApplicationVersion.Empty;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001F024 File Offset: 0x0001D224
		public static void OnStartGame(LoadResult loadResult)
		{
			MBSaveLoad.LastLoadedGameVersion = loadResult.MetaData.GetApplicationVersion();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001F038 File Offset: 0x0001D238
		public static bool IsSaveFileNameReserved(string name)
		{
			for (int i = 1; i <= 3; i++)
			{
				if (name == MBSaveLoad.AutoSaveNamePrefix + i)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001F06C File Offset: 0x0001D26C
		private static string GetNextAvailableSaveName()
		{
			uint num = 0U;
			foreach (string text in MBSaveLoad.GetSaveFileNames())
			{
				uint num2;
				if (text.StartsWith(MBSaveLoad.DefaultSaveGamePrefix) && uint.TryParse(text.Substring(MBSaveLoad.DefaultSaveGamePrefix.Length), out num2) && num2 > num)
				{
					num = num2;
				}
			}
			num += 1U;
			return MBSaveLoad.DefaultSaveGamePrefix + num.ToString("000");
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001F0DC File Offset: 0x0001D2DC
		private static void OverwriteSaveFile(MetaData metaData, string name, Action<SaveResult> onSaveCompleted)
		{
			try
			{
				MBSaveLoad.SaveGame(name, metaData, delegate(SaveResult r)
				{
					onSaveCompleted(r);
					MBSaveLoad.ShowErrorFromResult(r);
				});
			}
			catch
			{
				MBSaveLoad.ShowErrorFromResult(SaveResult.GeneralFailure);
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001F124 File Offset: 0x0001D324
		private static void ShowErrorFromResult(SaveResult result)
		{
			if (result != SaveResult.Success)
			{
				if (result == SaveResult.PlatformFileHelperFailure)
				{
					Debug.FailedAssert("Save Failed:\n" + Common.PlatformFileHelper.GetError(), "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.Core\\MBSaveLoad.cs", "ShowErrorFromResult", 314);
				}
				if (!MBSaveLoad.DoNotShowSaveErrorAgain)
				{
					InformationManager.ShowInquiry(new InquiryData(MBSaveLoad._textProvider.FindText("str_save_unsuccessful_title", null).ToString(), MBSaveLoad._textProvider.FindText("str_game_save_result", result.ToString()).ToString(), true, false, MBSaveLoad._textProvider.FindText("str_ok", null).ToString(), MBSaveLoad._textProvider.FindText("str_dont_show_again", null).ToString(), null, delegate
					{
						MBSaveLoad.DoNotShowSaveErrorAgain = true;
					}, "", 0f, null, null, null), false, false);
				}
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001F208 File Offset: 0x0001D408
		private static void SaveGame(string saveName, MetaData metaData, Action<SaveResult> onSaveCompleted)
		{
			ISaveDriver saveDriver = MBSaveLoad._saveDriver;
			try
			{
				Game.Current.Save(metaData, saveName, saveDriver, onSaveCompleted);
			}
			catch (Exception ex)
			{
				Debug.Print("Unable to create save game data", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print(ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.SilentAssert(ModuleHelper.GetModules(null).Any<ModuleInfo>((ModuleInfo m) => !m.IsOfficial), ex.Message, false, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.Core\\MBSaveLoad.cs", "SaveGame", 347);
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
		private static MetaData GetSaveMetaData(CampaignSaveMetaDataArgs data)
		{
			MetaData metaData = new MetaData();
			List<ModuleInfo> moduleInfos = ModuleHelper.GetModuleInfos(data.ModuleNames);
			metaData["Modules"] = string.Join(MBSaveLoad.ModuleCodeSeperator.ToString(), moduleInfos.Select<ModuleInfo, string>((ModuleInfo q) => q.Id));
			foreach (ModuleInfo moduleInfo in moduleInfos)
			{
				metaData["Module_" + moduleInfo.Id] = moduleInfo.Version.ToString();
			}
			metaData.Add("ApplicationVersion", MBSaveLoad.CurrentVersion.ToString());
			metaData.Add("CreationTime", DateTime.Now.Ticks.ToString());
			foreach (KeyValuePair<string, string> keyValuePair in data.OtherData)
			{
				metaData.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return metaData;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		public static int GetMaxNumberOfSaves()
		{
			return int.MaxValue;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001F3FF File Offset: 0x0001D5FF
		public static bool IsMaxNumberOfSavesReached()
		{
			return MBSaveLoad.NumberOfCurrentSaves >= MBSaveLoad.GetMaxNumberOfSaves();
		}

		// Token: 0x04000532 RID: 1330
		private const int MaxNumberOfAutoSaveFiles = 3;

		// Token: 0x04000533 RID: 1331
		private static ISaveDriver _saveDriver = new FileDriver();

		// Token: 0x04000534 RID: 1332
		private static int AutoSaveIndex = 0;

		// Token: 0x04000535 RID: 1333
		private static string DefaultSaveGamePrefix = "save";

		// Token: 0x04000536 RID: 1334
		private static string AutoSaveNamePrefix = MBSaveLoad.DefaultSaveGamePrefix + "auto";

		// Token: 0x04000538 RID: 1336
		private static GameTextManager _textProvider;

		// Token: 0x04000539 RID: 1337
		private static bool DoNotShowSaveErrorAgain = false;
	}
}

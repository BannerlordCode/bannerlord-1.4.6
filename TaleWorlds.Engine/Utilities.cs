using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000099 RID: 153
	public static class Utilities
	{
		// Token: 0x06000D2B RID: 3371 RVA: 0x0000EC24 File Offset: 0x0000CE24
		public static void ConstructMainThreadJob(Delegate function, params object[] parameters)
		{
			Utilities.MainThreadJob mainThreadJob = new Utilities.MainThreadJob(function, parameters);
			Utilities.jobs.Enqueue(mainThreadJob);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0000EC44 File Offset: 0x0000CE44
		public static void ConstructMainThreadJob(Semaphore semaphore, Delegate function, params object[] parameters)
		{
			Utilities.MainThreadJob mainThreadJob = new Utilities.MainThreadJob(semaphore, function, parameters);
			Utilities.jobs.Enqueue(mainThreadJob);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0000EC68 File Offset: 0x0000CE68
		public static void RunJobs()
		{
			Utilities.MainThreadJob mainThreadJob;
			while (Utilities.jobs.TryDequeue(out mainThreadJob))
			{
				mainThreadJob.Invoke();
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0000EC8B File Offset: 0x0000CE8B
		public static void WaitJobs()
		{
			while (!Utilities.jobs.IsEmpty)
			{
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0000EC99 File Offset: 0x0000CE99
		public static void OutputBenchmarkValuesToPerformanceReporter()
		{
			EngineApplicationInterface.IUtil.OutputBenchmarkValuesToPerformanceReporter();
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0000ECA5 File Offset: 0x0000CEA5
		public static void SetLoadingScreenPercentage(float value)
		{
			EngineApplicationInterface.IUtil.SetLoadingScreenPercentage(value);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0000ECB2 File Offset: 0x0000CEB2
		public static void SetFixedDt(bool enabled, float dt)
		{
			EngineApplicationInterface.IUtil.SetFixedDt(enabled, dt);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0000ECC0 File Offset: 0x0000CEC0
		public static void SetBenchmarkStatus(int status, string def)
		{
			EngineApplicationInterface.IUtil.SetBenchmarkStatus(status, def);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0000ECCE File Offset: 0x0000CECE
		public static int GetBenchmarkStatus()
		{
			return EngineApplicationInterface.IUtil.GetBenchmarkStatus();
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0000ECDA File Offset: 0x0000CEDA
		public static string GetApplicationMemoryStatistics()
		{
			return EngineApplicationInterface.IUtil.GetApplicationMemoryStatistics();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0000ECE6 File Offset: 0x0000CEE6
		public static bool IsBenchmarkQuited()
		{
			return EngineApplicationInterface.IUtil.IsBenchmarkQuited();
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0000ECF2 File Offset: 0x0000CEF2
		public static string GetNativeMemoryStatistics()
		{
			return EngineApplicationInterface.IUtil.GetNativeMemoryStatistics();
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0000ECFE File Offset: 0x0000CEFE
		public static bool CommandLineArgumentExists(string str)
		{
			return EngineApplicationInterface.IUtil.CommandLineArgumentExists(str);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0000ED0B File Offset: 0x0000CF0B
		public static string GetConsoleHostMachine()
		{
			return EngineApplicationInterface.IUtil.GetConsoleHostMachine();
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0000ED17 File Offset: 0x0000CF17
		public static string ExportNavMeshFaceMarks(string file_name)
		{
			return EngineApplicationInterface.IUtil.ExportNavMeshFaceMarks(file_name);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0000ED24 File Offset: 0x0000CF24
		public static string TakeSSFromTop(string file_name)
		{
			return EngineApplicationInterface.IUtil.TakeSSFromTop(file_name);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0000ED31 File Offset: 0x0000CF31
		public static void CheckIfAssetsAndSourcesAreSame()
		{
			EngineApplicationInterface.IUtil.CheckIfAssetsAndSourcesAreSame();
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0000ED3D File Offset: 0x0000CF3D
		public static void DisableCoreGame()
		{
			EngineApplicationInterface.IUtil.DisableCoreGame();
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0000ED49 File Offset: 0x0000CF49
		public static float GetApplicationMemory()
		{
			return EngineApplicationInterface.IUtil.GetApplicationMemory();
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0000ED55 File Offset: 0x0000CF55
		public static void GatherCoreGameReferences(string scene_names)
		{
			EngineApplicationInterface.IUtil.GatherCoreGameReferences(scene_names);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0000ED62 File Offset: 0x0000CF62
		public static bool IsOnlyCoreContentEnabled()
		{
			return EngineApplicationInterface.IUtil.GetCoreGameState() != 0;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0000ED71 File Offset: 0x0000CF71
		public static void FindMeshesWithoutLods(string module_name)
		{
			EngineApplicationInterface.IUtil.FindMeshesWithoutLods(module_name);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0000ED7E File Offset: 0x0000CF7E
		public static void SetDisableDumpGeneration(bool value)
		{
			EngineApplicationInterface.IUtil.SetDisableDumpGeneration(value);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0000ED8B File Offset: 0x0000CF8B
		public static void SetPrintCallstackAtCrahses(bool value)
		{
			EngineApplicationInterface.IUtil.SetPrintCallstackAtCrahses(value);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0000ED98 File Offset: 0x0000CF98
		public static string[] GetModulesNames()
		{
			return EngineApplicationInterface.IUtil.GetModulesCode().Split(new char[] { '*' });
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		public static string GetFullFilePathOfScene(string sceneName)
		{
			string fullFilePathOfScene = EngineApplicationInterface.IUtil.GetFullFilePathOfScene(sceneName);
			if (fullFilePathOfScene == "SCENE_NOT_FOUND")
			{
				throw new Exception("Scene '" + sceneName + "' was not found!");
			}
			return fullFilePathOfScene.Replace("$BASE/", Utilities.GetBasePath());
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		public static bool TryGetFullFilePathOfScene(string sceneName, out string fullPath)
		{
			bool flag;
			try
			{
				fullPath = Utilities.GetFullFilePathOfScene(sceneName);
				flag = true;
			}
			catch (Exception)
			{
				fullPath = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0000EE28 File Offset: 0x0000D028
		public static bool TryGetUniqueIdentifiersForScene(string sceneName, out UniqueSceneId identifiers)
		{
			identifiers = null;
			string text;
			return Utilities.TryGetFullFilePathOfScene(sceneName, out text) && Utilities.TryGetUniqueIdentifiersForSceneFile(text, out identifiers);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0000EE4C File Offset: 0x0000D04C
		public static bool TryGetUniqueIdentifiersForSceneFile(string xsceneFilePath, out UniqueSceneId identifiers)
		{
			identifiers = null;
			using (XmlReader xmlReader = XmlReader.Create(new StreamReader(xsceneFilePath)))
			{
				string attribute;
				string attribute2;
				if (xmlReader.MoveToContent() == XmlNodeType.Element && xmlReader.Name == "scene" && (attribute = xmlReader.GetAttribute("unique_token")) != null && (attribute2 = xmlReader.GetAttribute("revision")) != null)
				{
					identifiers = new UniqueSceneId(attribute, attribute2);
				}
			}
			return identifiers != null;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0000EECC File Offset: 0x0000D0CC
		public static void PairSceneNameToModuleName(string sceneName, string moduleName)
		{
			EngineApplicationInterface.IUtil.PairSceneNameToModuleName(sceneName, moduleName);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0000EEDA File Offset: 0x0000D0DA
		public static string[] GetSingleModuleScenesOfModule(string moduleName)
		{
			return EngineApplicationInterface.IUtil.GetSingleModuleScenesOfModule(moduleName).Split(new char[] { '*' });
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0000EEF7 File Offset: 0x0000D0F7
		public static string GetFullCommandLineString()
		{
			return EngineApplicationInterface.IUtil.GetFullCommandLineString();
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0000EF03 File Offset: 0x0000D103
		public static void SetScreenTextRenderingState(bool state)
		{
			EngineApplicationInterface.IUtil.SetScreenTextRenderingState(state);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0000EF10 File Offset: 0x0000D110
		public static void SetMessageLineRenderingState(bool state)
		{
			EngineApplicationInterface.IUtil.SetMessageLineRenderingState(state);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0000EF1D File Offset: 0x0000D11D
		public static bool CheckIfTerrainShaderHeaderGenerationFinished()
		{
			return EngineApplicationInterface.IUtil.CheckIfTerrainShaderHeaderGenerationFinished();
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0000EF29 File Offset: 0x0000D129
		public static void GenerateTerrainShaderHeaders(string targetPlatform, string targetConfig, string output_path)
		{
			EngineApplicationInterface.IUtil.GenerateTerrainShaderHeaders(targetPlatform, targetConfig, output_path);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0000EF38 File Offset: 0x0000D138
		public static void CompileTerrainShadersDist(string targetPlatform, string targetConfig, string output_path)
		{
			EngineApplicationInterface.IUtil.CompileTerrainShadersDist(targetPlatform, targetConfig, output_path);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0000EF47 File Offset: 0x0000D147
		public static void SetCrashOnAsserts(bool val)
		{
			EngineApplicationInterface.IUtil.SetCrashOnAsserts(val);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0000EF54 File Offset: 0x0000D154
		public static void SetCrashOnWarnings(bool val)
		{
			EngineApplicationInterface.IUtil.SetCrashOnWarnings(val);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0000EF61 File Offset: 0x0000D161
		public static void SetCreateDumpOnWarnings(bool val)
		{
			EngineApplicationInterface.IUtil.SetCreateDumpOnWarnings(val);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0000EF6E File Offset: 0x0000D16E
		public static void ToggleRender()
		{
			EngineApplicationInterface.IUtil.ToggleRender();
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0000EF7A File Offset: 0x0000D17A
		public static void SetRenderAgents(bool value)
		{
			EngineApplicationInterface.IUtil.SetRenderAgents(value);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0000EF87 File Offset: 0x0000D187
		public static bool CheckShaderCompilation()
		{
			return EngineApplicationInterface.IUtil.CheckShaderCompilation();
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0000EF93 File Offset: 0x0000D193
		public static void CompileAllShaders(string targetPlatform)
		{
			EngineApplicationInterface.IUtil.CompileAllShaders(targetPlatform);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		public static string GetExecutableWorkingDirectory()
		{
			return EngineApplicationInterface.IUtil.GetExecutableWorkingDirectory();
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		public static void SetDumpFolderPath(string path)
		{
			EngineApplicationInterface.IUtil.SetDumpFolderPath(path);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0000EFB9 File Offset: 0x0000D1B9
		public static void CheckSceneForProblems(string sceneName)
		{
			EngineApplicationInterface.IUtil.CheckSceneForProblems(sceneName);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0000EFC6 File Offset: 0x0000D1C6
		public static void SetCoreGameState(int state)
		{
			EngineApplicationInterface.IUtil.SetCoreGameState(state);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0000EFD3 File Offset: 0x0000D1D3
		public static int GetCoreGameState()
		{
			return EngineApplicationInterface.IUtil.GetCoreGameState();
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public static string ExecuteCommandLineCommand(string command)
		{
			return EngineApplicationInterface.IUtil.ExecuteCommandLineCommand(command);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		public static void QuitGame()
		{
			EngineApplicationInterface.IUtil.QuitGame();
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		public static void ExitProcess(int exitCode)
		{
			EngineApplicationInterface.IUtil.ExitProcess(exitCode);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0000F005 File Offset: 0x0000D205
		public static string GetBasePath()
		{
			return EngineApplicationInterface.IUtil.GetBaseDirectory();
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0000F011 File Offset: 0x0000D211
		public static string GetVisualTestsValidatePath()
		{
			return EngineApplicationInterface.IUtil.GetVisualTestsValidatePath();
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0000F01D File Offset: 0x0000D21D
		public static string GetVisualTestsTestFilesPath()
		{
			return EngineApplicationInterface.IUtil.GetVisualTestsTestFilesPath();
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0000F029 File Offset: 0x0000D229
		public static string GetAttachmentsPath()
		{
			return EngineApplicationInterface.IUtil.GetAttachmentsPath();
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0000F035 File Offset: 0x0000D235
		public static void StartScenePerformanceReport(string folderPath)
		{
			EngineApplicationInterface.IUtil.StartScenePerformanceReport(folderPath);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0000F042 File Offset: 0x0000D242
		public static bool IsSceneReportFinished()
		{
			return EngineApplicationInterface.IUtil.IsSceneReportFinished();
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0000F04E File Offset: 0x0000D24E
		public static float GetFps()
		{
			return EngineApplicationInterface.IUtil.GetFps();
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0000F05A File Offset: 0x0000D25A
		public static float GetMainFps()
		{
			return EngineApplicationInterface.IUtil.GetMainFps();
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0000F066 File Offset: 0x0000D266
		public static float GetRendererFps()
		{
			return EngineApplicationInterface.IUtil.GetRendererFps();
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0000F072 File Offset: 0x0000D272
		public static void EnableSingleGPUQueryPerFrame()
		{
			EngineApplicationInterface.IUtil.EnableSingleGPUQueryPerFrame();
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0000F07E File Offset: 0x0000D27E
		public static void ClearDecalAtlas(DecalAtlasGroup atlasGroup)
		{
			EngineApplicationInterface.IUtil.clear_decal_atlas(atlasGroup);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0000F08B File Offset: 0x0000D28B
		public static void FlushManagedObjectsMemory()
		{
			Common.MemoryCleanupGC(false);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0000F093 File Offset: 0x0000D293
		public static void OnLoadingWindowEnabled()
		{
			EngineApplicationInterface.IUtil.OnLoadingWindowEnabled();
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0000F09F File Offset: 0x0000D29F
		public static void DebugSetGlobalLoadingWindowState(bool newState)
		{
			EngineApplicationInterface.IUtil.DebugSetGlobalLoadingWindowState(newState);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		public static void OnLoadingWindowDisabled()
		{
			EngineApplicationInterface.IUtil.OnLoadingWindowDisabled();
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		public static void DisableGlobalLoadingWindow()
		{
			EngineApplicationInterface.IUtil.DisableGlobalLoadingWindow();
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		public static void EnableGlobalLoadingWindow()
		{
			EngineApplicationInterface.IUtil.EnableGlobalLoadingWindow();
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0000F0D0 File Offset: 0x0000D2D0
		public static void EnableGlobalEditDataCacher()
		{
			EngineApplicationInterface.IUtil.EnableGlobalEditDataCacher();
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		public static void DoFullBakeAllLevelsAutomated(string module, string scene)
		{
			EngineApplicationInterface.IUtil.DoFullBakeAllLevelsAutomated(module, scene);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0000F0EA File Offset: 0x0000D2EA
		public static int GetReturnCode()
		{
			return EngineApplicationInterface.IUtil.GetReturnCode();
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0000F0F6 File Offset: 0x0000D2F6
		public static void DisableGlobalEditDataCacher()
		{
			EngineApplicationInterface.IUtil.DisableGlobalEditDataCacher();
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0000F102 File Offset: 0x0000D302
		public static void DoFullBakeSingleLevelAutomated(string module, string scene)
		{
			EngineApplicationInterface.IUtil.DoFullBakeSingleLevelAutomated(module, scene);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0000F110 File Offset: 0x0000D310
		public static void DoLightOnlyBakeSingleLevelAutomated(string module, string scene)
		{
			EngineApplicationInterface.IUtil.DoLightOnlyBakeSingleLevelAutomated(module, scene);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0000F11E File Offset: 0x0000D31E
		public static void DoLightOnlyBakeAllLevelsAutomated(string module, string scene)
		{
			EngineApplicationInterface.IUtil.DoLightOnlyBakeAllLevelsAutomated(module, scene);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0000F12C File Offset: 0x0000D32C
		public static bool DidAutomatedGIBakeFinished()
		{
			return EngineApplicationInterface.IUtil.DidAutomatedGIBakeFinished();
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0000F138 File Offset: 0x0000D338
		public static void GetSelectedEntities(ref List<GameEntity> gameEntities)
		{
			int editorSelectedEntityCount = EngineApplicationInterface.IUtil.GetEditorSelectedEntityCount();
			UIntPtr[] array = new UIntPtr[editorSelectedEntityCount];
			EngineApplicationInterface.IUtil.GetEditorSelectedEntities(array);
			for (int i = 0; i < editorSelectedEntityCount; i++)
			{
				gameEntities.Add(new GameEntity(array[i]));
			}
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0000F180 File Offset: 0x0000D380
		public static void DeleteEntitiesInEditorScene(List<GameEntity> gameEntities)
		{
			int count = gameEntities.Count;
			UIntPtr[] array = new UIntPtr[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = gameEntities[i].Pointer;
			}
			EngineApplicationInterface.IUtil.DeleteEntitiesInEditorScene(array, count);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
		public static void CreateSelectionInEditor(List<GameEntity> gameEntities, string name)
		{
			int count = gameEntities.Count;
			UIntPtr[] array = new UIntPtr[gameEntities.Count];
			for (int i = 0; i < count; i++)
			{
				array[i] = gameEntities[i].Pointer;
			}
			EngineApplicationInterface.IUtil.CreateSelectionInEditor(array, count, name);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0000F20C File Offset: 0x0000D40C
		public static void SelectEntities(List<GameEntity> gameEntities)
		{
			int count = gameEntities.Count;
			UIntPtr[] array = new UIntPtr[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = gameEntities[i].Pointer;
			}
			EngineApplicationInterface.IUtil.SelectEntities(array, count);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0000F250 File Offset: 0x0000D450
		public static void GetEntitiesOfSelectionSet(string selectionSetName, ref List<GameEntity> gameEntities)
		{
			int entityCountOfSelectionSet = EngineApplicationInterface.IUtil.GetEntityCountOfSelectionSet(selectionSetName);
			UIntPtr[] array = new UIntPtr[entityCountOfSelectionSet];
			EngineApplicationInterface.IUtil.GetEntitiesOfSelectionSet(selectionSetName, array);
			for (int i = 0; i < entityCountOfSelectionSet; i++)
			{
				gameEntities.Add(new GameEntity(array[i]));
			}
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0000F297 File Offset: 0x0000D497
		public static void AddCommandLineFunction(string concatName)
		{
			EngineApplicationInterface.IUtil.AddCommandLineFunction(concatName);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0000F2A4 File Offset: 0x0000D4A4
		public static int GetNumberOfShaderCompilationsInProgress()
		{
			return EngineApplicationInterface.IUtil.GetNumberOfShaderCompilationsInProgress();
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		public static int IsDetailedSoundLogOn()
		{
			return EngineApplicationInterface.IUtil.IsDetailedSoundLogOn();
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		public static ulong GetCurrentCpuMemoryUsageMB()
		{
			return EngineApplicationInterface.IUtil.GetCurrentCpuMemoryUsage();
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		public static ulong GetGpuMemoryOfAllocationGroup(string name)
		{
			return EngineApplicationInterface.IUtil.GetGpuMemoryOfAllocationGroup(name);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0000F2D5 File Offset: 0x0000D4D5
		public static void GetGPUMemoryStats(ref float totalMemory, ref float renderTargetMemory, ref float depthTargetMemory, ref float srvMemory, ref float bufferMemory)
		{
			EngineApplicationInterface.IUtil.GetGPUMemoryStats(ref totalMemory, ref renderTargetMemory, ref depthTargetMemory, ref srvMemory, ref bufferMemory);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0000F2E7 File Offset: 0x0000D4E7
		public static void GetDetailedGPUMemoryData(ref int totalMemoryAllocated, ref int totalMemoryUsed, ref int emptyChunkTotalSize)
		{
			EngineApplicationInterface.IUtil.GetDetailedGPUBufferMemoryStats(ref totalMemoryAllocated, ref totalMemoryUsed, ref emptyChunkTotalSize);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0000F2F6 File Offset: 0x0000D4F6
		public static void SetRenderMode(Utilities.EngineRenderDisplayMode mode)
		{
			EngineApplicationInterface.IUtil.SetRenderMode((int)mode);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0000F303 File Offset: 0x0000D503
		public static void SetForceDrawEntityID(bool value)
		{
			EngineApplicationInterface.IUtil.SetForceDrawEntityID(value);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0000F310 File Offset: 0x0000D510
		public static void AddPerformanceReportToken(string performance_type, string name, float loading_time)
		{
			EngineApplicationInterface.IUtil.AddPerformanceReportToken(performance_type, name, loading_time);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0000F31F File Offset: 0x0000D51F
		public static void AddSceneObjectReport(string scene_name, string report_name, float report_value)
		{
			EngineApplicationInterface.IUtil.AddSceneObjectReport(scene_name, report_name, report_value);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0000F32E File Offset: 0x0000D52E
		public static void OutputPerformanceReports()
		{
			EngineApplicationInterface.IUtil.OutputPerformanceReports();
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0000F33A File Offset: 0x0000D53A
		public static int EngineFrameNo
		{
			get
			{
				return EngineApplicationInterface.IUtil.GetEngineFrameNo();
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0000F346 File Offset: 0x0000D546
		public static bool EditModeEnabled
		{
			get
			{
				return EngineApplicationInterface.IUtil.IsEditModeEnabled();
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0000F352 File Offset: 0x0000D552
		public static void TakeScreenshot(PlatformFilePath path)
		{
			EngineApplicationInterface.IUtil.TakeScreenshotFromPlatformPath(path);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0000F35F File Offset: 0x0000D55F
		public static void TakeScreenshot(string path)
		{
			EngineApplicationInterface.IUtil.TakeScreenshotFromStringPath(path);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0000F36C File Offset: 0x0000D56C
		public static void SetAllocationAlwaysValidScene(Scene scene)
		{
			EngineApplicationInterface.IUtil.SetAllocationAlwaysValidScene((scene != null) ? scene.Pointer : UIntPtr.Zero);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0000F38E File Offset: 0x0000D58E
		public static void CheckResourceModifications()
		{
			EngineApplicationInterface.IUtil.CheckResourceModifications();
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0000F39A File Offset: 0x0000D59A
		public static void SetGraphicsPreset(int preset)
		{
			EngineApplicationInterface.IUtil.SetGraphicsPreset(preset);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0000F3A7 File Offset: 0x0000D5A7
		public static string GetLocalOutputPath()
		{
			return EngineApplicationInterface.IUtil.GetLocalOutputPath();
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0000F3B3 File Offset: 0x0000D5B3
		public static string GetPCInfo()
		{
			return EngineApplicationInterface.IUtil.GetPCInfo();
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0000F3BF File Offset: 0x0000D5BF
		public static int GetGPUMemoryMB()
		{
			return EngineApplicationInterface.IUtil.GetGPUMemoryMB();
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0000F3CB File Offset: 0x0000D5CB
		public static int GetCurrentEstimatedGPUMemoryCostMB()
		{
			return EngineApplicationInterface.IUtil.GetCurrentEstimatedGPUMemoryCostMB();
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0000F3D7 File Offset: 0x0000D5D7
		public static void DumpGPUMemoryStatistics(string filePath)
		{
			EngineApplicationInterface.IUtil.DumpGPUMemoryStatistics(filePath);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0000F3E4 File Offset: 0x0000D5E4
		public static int SaveDataAsTexture(string path, int width, int height, float[] data)
		{
			return EngineApplicationInterface.IUtil.SaveDataAsTexture(path, width, height, data);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		public static void ClearOldResourcesAndObjects()
		{
			EngineApplicationInterface.IUtil.ClearOldResourcesAndObjects();
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0000F400 File Offset: 0x0000D600
		public static void LoadVirtualTextureTileset(string name)
		{
			EngineApplicationInterface.IUtil.LoadVirtualTextureTileset(name);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0000F40D File Offset: 0x0000D60D
		public static float GetDeltaTime(int timerId)
		{
			return EngineApplicationInterface.IUtil.GetDeltaTime(timerId);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0000F41A File Offset: 0x0000D61A
		public static void LoadSkyBoxes()
		{
			EngineApplicationInterface.IUtil.LoadSkyBoxes();
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0000F426 File Offset: 0x0000D626
		public static string GetApplicationName()
		{
			return EngineApplicationInterface.IUtil.GetApplicationName();
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0000F432 File Offset: 0x0000D632
		public static void OpenConsoleStorePage(string productId)
		{
			EngineApplicationInterface.IUtil.OpenConsoleStorePage(productId);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0000F43F File Offset: 0x0000D63F
		public static void SetWindowTitle(string title)
		{
			EngineApplicationInterface.IUtil.SetWindowTitle(title);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0000F44C File Offset: 0x0000D64C
		public static string ProcessWindowTitle(string title)
		{
			return EngineApplicationInterface.IUtil.ProcessWindowTitle(title);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0000F459 File Offset: 0x0000D659
		public static uint GetCurrentProcessID()
		{
			return EngineApplicationInterface.IUtil.GetCurrentProcessID();
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0000F465 File Offset: 0x0000D665
		public static void DoDelayedexit(int returnCode)
		{
			EngineApplicationInterface.IUtil.DoDelayedexit(returnCode);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0000F472 File Offset: 0x0000D672
		public static void SetAssertionsAndWarningsSetExitCode(bool value)
		{
			EngineApplicationInterface.IUtil.SetAssertionsAndWarningsSetExitCode(value);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0000F47F File Offset: 0x0000D67F
		public static void SetReportMode(bool reportMode)
		{
			EngineApplicationInterface.IUtil.SetReportMode(reportMode);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0000F48C File Offset: 0x0000D68C
		public static void SetAssertionAtShaderCompile(bool value)
		{
			EngineApplicationInterface.IUtil.SetAssertionAtShaderCompile(value);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0000F499 File Offset: 0x0000D699
		public static void SetCrashReportCustomString(string customString)
		{
			EngineApplicationInterface.IUtil.SetCrashReportCustomString(customString);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0000F4A6 File Offset: 0x0000D6A6
		public static void SetCrashReportCustomStack(string customStack)
		{
			EngineApplicationInterface.IUtil.SetCrashReportCustomStack(customStack);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0000F4B3 File Offset: 0x0000D6B3
		public static int GetSteamAppId()
		{
			return EngineApplicationInterface.IUtil.GetSteamAppId();
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0000F4BF File Offset: 0x0000D6BF
		public static void SetForceVsync(bool value)
		{
			Debug.Print("Force VSync State is now " + (value ? "ACTIVE" : "DEACTIVATED"), 0, Debug.DebugColor.DarkBlue, 17592186044416UL);
			EngineApplicationInterface.IUtil.SetForceVsync(value);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0000F4F5 File Offset: 0x0000D6F5
		private static PlatformFilePath DefaultBannerlordConfigFullPath
		{
			get
			{
				return new PlatformFilePath(EngineFilePaths.ConfigsPath, "BannerlordConfig.txt");
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0000F508 File Offset: 0x0000D708
		public static string LoadBannerlordConfigFile()
		{
			PlatformFilePath defaultBannerlordConfigFullPath = Utilities.DefaultBannerlordConfigFullPath;
			if (!FileHelper.FileExists(defaultBannerlordConfigFullPath))
			{
				return "";
			}
			return FileHelper.GetFileContentString(defaultBannerlordConfigFullPath);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0000F530 File Offset: 0x0000D730
		public static SaveResult SaveConfigFile(string configProperties)
		{
			PlatformFilePath defaultBannerlordConfigFullPath = Utilities.DefaultBannerlordConfigFullPath;
			SaveResult saveResult;
			try
			{
				string text = configProperties.Substring(0, configProperties.Length - 1);
				FileHelper.SaveFileString(defaultBannerlordConfigFullPath, text);
				saveResult = SaveResult.Success;
			}
			catch
			{
				Debug.Print("Could not create Bannerlord Config file", 0, Debug.DebugColor.White, 17592186044416UL);
				saveResult = SaveResult.ConfigFileFailure;
			}
			return saveResult;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0000F58C File Offset: 0x0000D78C
		public static void OpenOnscreenKeyboard(string initialText, string descriptionText, int maxLength, int keyboardTypeEnum)
		{
			EngineApplicationInterface.IUtil.OpenOnscreenKeyboard(initialText, descriptionText, maxLength, keyboardTypeEnum);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0000F59C File Offset: 0x0000D79C
		public static string GetSystemLanguage()
		{
			return EngineApplicationInterface.IUtil.GetSystemLanguage();
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		public static int RegisterGPUAllocationGroup(string name)
		{
			return EngineApplicationInterface.IUtil.RegisterGPUAllocationGroup(name);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0000F5B5 File Offset: 0x0000D7B5
		public static int GetMemoryUsageOfCategory(int category)
		{
			return EngineApplicationInterface.IUtil.GetMemoryUsageOfCategory(category);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0000F5C2 File Offset: 0x0000D7C2
		public static string GetDetailedXBOXMemoryInfo()
		{
			return EngineApplicationInterface.IUtil.GetDetailedXBOXMemoryInfo();
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0000F5CE File Offset: 0x0000D7CE
		public static void SetFrameLimiterWithSleep(bool value)
		{
			EngineApplicationInterface.IUtil.SetFrameLimiterWithSleep(value);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0000F5DB File Offset: 0x0000D7DB
		public static bool GetFrameLimiterWithSleep()
		{
			return EngineApplicationInterface.IUtil.GetFrameLimiterWithSleep();
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0000F5E7 File Offset: 0x0000D7E7
		public static string GetPossibleCommandLineStartingWith(string command, int index)
		{
			return EngineApplicationInterface.IUtil.GetPossibleCommandLineStartingWith(command, index);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0000F5F5 File Offset: 0x0000D7F5
		public static bool IsDevkit()
		{
			return EngineApplicationInterface.IUtil.IsDevkit();
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0000F601 File Offset: 0x0000D801
		public static bool IsLockhartPlatform()
		{
			return EngineApplicationInterface.IUtil.IsLockhartPlatform();
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0000F60D File Offset: 0x0000D80D
		public static int GetVertexBufferChunkSystemMemoryUsage()
		{
			return EngineApplicationInterface.IUtil.GetVertexBufferChunkSystemMemoryUsage();
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0000F619 File Offset: 0x0000D819
		public static int GetBuildNumber()
		{
			return EngineApplicationInterface.IUtil.GetBuildNumber();
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0000F625 File Offset: 0x0000D825
		public static ApplicationVersion GetApplicationVersionWithBuildNumber()
		{
			return ApplicationVersion.FromParametersFile(null);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0000F62D File Offset: 0x0000D82D
		public static void ParallelFor(int startIndex, int endIndex, long curKey, int grainSize)
		{
			EngineApplicationInterface.IUtil.ManagedParallelFor(startIndex, endIndex, curKey, grainSize);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0000F63D File Offset: 0x0000D83D
		public static void ParallelForWithDt(int startIndex, int endIndex, long curKey, int grainSize)
		{
			EngineApplicationInterface.IUtil.ManagedParallelForWithDt(startIndex, endIndex, curKey, grainSize);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0000F64D File Offset: 0x0000D84D
		public static void ParallelForWithoutRenderThread(int startIndex, int endIndex, long curKey, int grainSize)
		{
			EngineApplicationInterface.IUtil.ManagedParallelForWithoutRenderThread(startIndex, endIndex, curKey, grainSize);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0000F65D File Offset: 0x0000D85D
		public static void ParallelForWithoutRenderThreadDt(int startIndex, int endIndex, long curKey, int grainSize)
		{
			EngineApplicationInterface.IUtil.ManagedParallelForWithoutRenderThreadDt(startIndex, endIndex, curKey, grainSize);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0000F66D File Offset: 0x0000D86D
		public static void ClearShaderMemory()
		{
			EngineApplicationInterface.IUtil.ClearShaderMemory();
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0000F679 File Offset: 0x0000D879
		public static void RegisterMeshForGPUMorph(string metaMeshName)
		{
			EngineApplicationInterface.IUtil.RegisterMeshForGPUMorph(metaMeshName);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0000F686 File Offset: 0x0000D886
		public static ulong GetMainThreadId()
		{
			return EngineApplicationInterface.IUtil.GetMainThreadId();
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0000F692 File Offset: 0x0000D892
		public static ulong GetCurrentThreadId()
		{
			return EngineApplicationInterface.IUtil.GetCurrentThreadId();
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0000F69E File Offset: 0x0000D89E
		public static void SetWatchdogValue(string fileName, string groupName, string key, string value)
		{
			EngineApplicationInterface.IUtil.SetWatchdogValue(fileName, groupName, key, value);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0000F6AE File Offset: 0x0000D8AE
		public static void SetWatchdogAutoreport(bool enabled)
		{
			EngineApplicationInterface.IUtil.SetWatchdogAutoreport(enabled);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0000F6BB File Offset: 0x0000D8BB
		public static void DetachWatchdog()
		{
			EngineApplicationInterface.IUtil.DetachWatchdog();
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0000F6C7 File Offset: 0x0000D8C7
		public static string GetPlatformModulePaths()
		{
			return EngineApplicationInterface.IUtil.GetPlatformModulePaths();
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0000F6D3 File Offset: 0x0000D8D3
		public static bool IsAsyncPhysicsThread()
		{
			return EngineApplicationInterface.IUtil.IsAsyncPhysicsThread();
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0000F6DF File Offset: 0x0000D8DF
		public static void StartLoadingStuckCheckState(float timeoutThresholdSeconds)
		{
			EngineApplicationInterface.IUtil.StartLoadingStuckCheckState(timeoutThresholdSeconds);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		public static void EndLoadingStuckCheckState()
		{
			EngineApplicationInterface.IUtil.EndLoadingStuckCheckState();
		}

		// Token: 0x04000201 RID: 513
		private static ConcurrentQueue<Utilities.MainThreadJob> jobs = new ConcurrentQueue<Utilities.MainThreadJob>();

		// Token: 0x04000202 RID: 514
		public static bool renderingActive = true;

		// Token: 0x020000D3 RID: 211
		public enum EngineRenderDisplayMode
		{
			// Token: 0x0400044B RID: 1099
			ShowNone,
			// Token: 0x0400044C RID: 1100
			ShowAlbedo,
			// Token: 0x0400044D RID: 1101
			ShowNormals,
			// Token: 0x0400044E RID: 1102
			ShowVertexNormals,
			// Token: 0x0400044F RID: 1103
			ShowSpecular,
			// Token: 0x04000450 RID: 1104
			ShowGloss,
			// Token: 0x04000451 RID: 1105
			ShowOcclusion,
			// Token: 0x04000452 RID: 1106
			ShowGbufferShadowMask,
			// Token: 0x04000453 RID: 1107
			ShowTranslucency,
			// Token: 0x04000454 RID: 1108
			ShowMotionVector,
			// Token: 0x04000455 RID: 1109
			ShowVertexColor,
			// Token: 0x04000456 RID: 1110
			ShowDepth,
			// Token: 0x04000457 RID: 1111
			ShowTiledLightOverdraw,
			// Token: 0x04000458 RID: 1112
			ShowTiledDecalOverdraw,
			// Token: 0x04000459 RID: 1113
			ShowMeshId,
			// Token: 0x0400045A RID: 1114
			ShowDisableSunLighting,
			// Token: 0x0400045B RID: 1115
			ShowDebugTexture,
			// Token: 0x0400045C RID: 1116
			ShowTextureDensity,
			// Token: 0x0400045D RID: 1117
			ShowOverdraw,
			// Token: 0x0400045E RID: 1118
			ShowVsComplexity,
			// Token: 0x0400045F RID: 1119
			ShowPsComplexity,
			// Token: 0x04000460 RID: 1120
			ShowDisableAmbientLighting,
			// Token: 0x04000461 RID: 1121
			ShowEntityId,
			// Token: 0x04000462 RID: 1122
			ShowPrtDiffuseAmbient,
			// Token: 0x04000463 RID: 1123
			ShowLightDebugMode,
			// Token: 0x04000464 RID: 1124
			ShowParticleShadingAtlas,
			// Token: 0x04000465 RID: 1125
			ShowTerrainAngle,
			// Token: 0x04000466 RID: 1126
			ShowParallaxDebug,
			// Token: 0x04000467 RID: 1127
			ShowAlbedoValidation,
			// Token: 0x04000468 RID: 1128
			NumDebugModes
		}

		// Token: 0x020000D4 RID: 212
		private class MainThreadJob
		{
			// Token: 0x06001016 RID: 4118 RVA: 0x00014BBB File Offset: 0x00012DBB
			internal MainThreadJob(Delegate function, object[] parameters)
			{
				this._function = function;
				this._parameters = parameters;
				this.wait_handle = null;
			}

			// Token: 0x06001017 RID: 4119 RVA: 0x00014BD8 File Offset: 0x00012DD8
			internal MainThreadJob(Semaphore sema, Delegate function, object[] parameters)
			{
				this._function = function;
				this._parameters = parameters;
				this.wait_handle = sema;
			}

			// Token: 0x06001018 RID: 4120 RVA: 0x00014BF5 File Offset: 0x00012DF5
			internal void Invoke()
			{
				this._function.DynamicInvoke(this._parameters);
				if (this.wait_handle != null)
				{
					this.wait_handle.Release();
				}
			}

			// Token: 0x04000469 RID: 1129
			private Delegate _function;

			// Token: 0x0400046A RID: 1130
			private object[] _parameters;

			// Token: 0x0400046B RID: 1131
			private Semaphore wait_handle;
		}

		// Token: 0x020000D5 RID: 213
		public class MainThreadPerformanceQuery : IDisposable
		{
			// Token: 0x06001019 RID: 4121 RVA: 0x00014C1D File Offset: 0x00012E1D
			public MainThreadPerformanceQuery(string parent, string name)
			{
				this._name = name;
				this._parent = parent;
				this._stopWatch = new Stopwatch();
				this._stopWatch.Start();
			}

			// Token: 0x0600101A RID: 4122 RVA: 0x00014C4C File Offset: 0x00012E4C
			public void Dispose()
			{
				this._stopWatch.Stop();
				float num = (float)this._stopWatch.Elapsed.TotalMilliseconds;
				num /= 1000f;
				EngineApplicationInterface.IUtil.AddMainThreadPerformanceQuery(this._parent, this._name, num);
			}

			// Token: 0x0400046C RID: 1132
			private string _name;

			// Token: 0x0400046D RID: 1133
			private string _parent;

			// Token: 0x0400046E RID: 1134
			private Stopwatch _stopWatch;
		}
	}
}

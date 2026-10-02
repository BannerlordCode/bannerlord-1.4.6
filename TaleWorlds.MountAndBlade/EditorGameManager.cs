using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200021A RID: 538
	public class EditorGameManager : MBGameManager
	{
		// Token: 0x06001F48 RID: 8008 RVA: 0x0006C578 File Offset: 0x0006A778
		protected override void DoLoadingForGameManager(GameManagerLoadingSteps gameManagerLoadingStep, out GameManagerLoadingSteps nextStep)
		{
			nextStep = GameManagerLoadingSteps.None;
			switch (gameManagerLoadingStep)
			{
			case GameManagerLoadingSteps.PreInitializeZerothStep:
				MBGameManager.LoadModuleData(false);
				MBGlobals.InitializeReferences();
				Game.CreateGame(new EditorGame(), this).DoLoading();
				nextStep = GameManagerLoadingSteps.FirstInitializeFirstStep;
				return;
			case GameManagerLoadingSteps.FirstInitializeFirstStep:
			{
				bool flag = true;
				foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
				{
					flag = flag && mbsubModuleBase.DoLoading(Game.Current);
				}
				nextStep = (flag ? GameManagerLoadingSteps.WaitSecondStep : GameManagerLoadingSteps.FirstInitializeFirstStep);
				return;
			}
			case GameManagerLoadingSteps.WaitSecondStep:
				MBGameManager.StartNewGame();
				nextStep = GameManagerLoadingSteps.SecondInitializeThirdState;
				return;
			case GameManagerLoadingSteps.SecondInitializeThirdState:
				nextStep = (Game.Current.DoLoading() ? GameManagerLoadingSteps.PostInitializeFourthState : GameManagerLoadingSteps.SecondInitializeThirdState);
				return;
			case GameManagerLoadingSteps.PostInitializeFourthState:
				nextStep = GameManagerLoadingSteps.FinishLoadingFifthStep;
				return;
			case GameManagerLoadingSteps.FinishLoadingFifthStep:
				nextStep = GameManagerLoadingSteps.None;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0006C64C File Offset: 0x0006A84C
		public override void OnAfterCampaignStart(Game game)
		{
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0006C64E File Offset: 0x0006A84E
		public override void OnLoadFinished()
		{
			base.OnLoadFinished();
		}
	}
}

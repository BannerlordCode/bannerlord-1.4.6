using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.BannerEditor
{
	// Token: 0x0200004F RID: 79
	[GameStateScreen(typeof(BannerEditorState))]
	public class GauntletBannerEditorScreen : ScreenBase, IGameStateListener
	{
		// Token: 0x060003CC RID: 972 RVA: 0x00017CC8 File Offset: 0x00015EC8
		public GauntletBannerEditorScreen(BannerEditorState bannerEditorState)
		{
			LoadingWindow.EnableGlobalLoadingWindow();
			this._clan = bannerEditorState.GetClan();
			this._bannerEditorLayer = new BannerEditorView(bannerEditorState.GetCharacter(), bannerEditorState.GetClan().Banner, new ControlCharacterCreationStage(this.OnDone), new TextObject("{=WiNRdfsm}Done", null), new ControlCharacterCreationStage(this.OnCancel), new TextObject("{=3CpNUnVl}Cancel", null), null, null, null, null, null);
			this._bannerEditorLayer.DataSource.SetClanRelatedRules(bannerEditorState.GetClan().Kingdom == null);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00017D59 File Offset: 0x00015F59
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			this._bannerEditorLayer.OnTick(dt);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00017D70 File Offset: 0x00015F70
		public void OnDone()
		{
			uint primaryColor = this._bannerEditorLayer.DataSource.BannerVM.Banner.GetPrimaryColor();
			uint firstIconColor = this._bannerEditorLayer.DataSource.BannerVM.Banner.GetFirstIconColor();
			this._clan.Color2 = firstIconColor;
			if (this._bannerEditorLayer.DataSource.CanChangeBackgroundColor)
			{
				this._clan.Color = primaryColor;
				this._clan.UpdateBannerColor(primaryColor, firstIconColor);
			}
			else
			{
				this._clan.UpdateBannerColor(this._clan.Color, firstIconColor);
			}
			Game.Current.GameStateManager.PopState(0);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00017E13 File Offset: 0x00016013
		public void OnCancel()
		{
			Game.Current.GameStateManager.PopState(0);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00017E25 File Offset: 0x00016025
		protected override void OnInitialize()
		{
			base.OnInitialize();
			Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
			InformationManager.HideAllMessages();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00017E42 File Offset: 0x00016042
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._bannerEditorLayer.OnFinalize();
			if (LoadingWindow.IsLoadingWindowActive)
			{
				LoadingWindow.DisableGlobalLoadingWindow();
			}
			Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00017E71 File Offset: 0x00016071
		protected override void OnActivate()
		{
			base.OnActivate();
			base.AddLayer(this._bannerEditorLayer.GauntletLayer);
			base.AddLayer(this._bannerEditorLayer.SceneLayer);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00017E9B File Offset: 0x0001609B
		protected override void OnDeactivate()
		{
			this._bannerEditorLayer.OnDeactivate();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00017EA8 File Offset: 0x000160A8
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00017EAA File Offset: 0x000160AA
		void IGameStateListener.OnDeactivate()
		{
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00017EAC File Offset: 0x000160AC
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00017EAE File Offset: 0x000160AE
		void IGameStateListener.OnFinalize()
		{
		}

		// Token: 0x040001BC RID: 444
		private const int ViewOrderPriority = 15;

		// Token: 0x040001BD RID: 445
		private readonly BannerEditorView _bannerEditorLayer;

		// Token: 0x040001BE RID: 446
		private readonly Clan _clan;
	}
}

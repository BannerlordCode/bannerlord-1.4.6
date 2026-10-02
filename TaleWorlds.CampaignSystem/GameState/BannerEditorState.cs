using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000387 RID: 903
	public class BannerEditorState : GameState
	{
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060034C5 RID: 13509 RVA: 0x000D946A File Offset: 0x000D766A
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x000D946D File Offset: 0x000D766D
		// (set) Token: 0x060034C7 RID: 13511 RVA: 0x000D9475 File Offset: 0x000D7675
		public IBannerEditorStateHandler Handler
		{
			get
			{
				return this._handler;
			}
			set
			{
				this._handler = value;
			}
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000D947E File Offset: 0x000D767E
		public BannerEditorState()
		{
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000D9486 File Offset: 0x000D7686
		public BannerEditorState(Action endAction)
		{
			this._onEndAction = endAction;
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000D9495 File Offset: 0x000D7695
		public Clan GetClan()
		{
			return Clan.PlayerClan;
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000D949C File Offset: 0x000D769C
		public CharacterObject GetCharacter()
		{
			return CharacterObject.PlayerCharacter;
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000D94A3 File Offset: 0x000D76A3
		protected override void OnFinalize()
		{
			base.OnFinalize();
			Action onEndAction = this._onEndAction;
			if (onEndAction == null)
			{
				return;
			}
			onEndAction();
		}

		// Token: 0x04000F17 RID: 3863
		private IBannerEditorStateHandler _handler;

		// Token: 0x04000F18 RID: 3864
		private Action _onEndAction;
	}
}

using System;
using TaleWorlds.Engine.Options;

namespace TaleWorlds.MountAndBlade.Options
{
	// Token: 0x02000395 RID: 917
	public class ActionOptionData : IOptionData
	{
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000D8DDD File Offset: 0x000D6FDD
		// (set) Token: 0x06003488 RID: 13448 RVA: 0x000D8DE5 File Offset: 0x000D6FE5
		public Action OnAction { get; private set; }

		// Token: 0x06003489 RID: 13449 RVA: 0x000D8DEE File Offset: 0x000D6FEE
		public ActionOptionData(ManagedOptions.ManagedOptionsType managedType, Action onAction)
		{
			this._managedType = managedType;
			this.OnAction = onAction;
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000D8E04 File Offset: 0x000D7004
		public ActionOptionData(NativeOptions.NativeOptionsType nativeType, Action onAction)
		{
			this._nativeType = nativeType;
			this.OnAction = onAction;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000D8E1A File Offset: 0x000D701A
		public ActionOptionData(string optionTypeId, Action onAction)
		{
			this._actionOptionTypeId = optionTypeId;
			this._nativeType = NativeOptions.NativeOptionsType.None;
			this.OnAction = onAction;
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000D8E37 File Offset: 0x000D7037
		public void Commit()
		{
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000D8E39 File Offset: 0x000D7039
		public float GetDefaultValue()
		{
			return 0f;
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000D8E40 File Offset: 0x000D7040
		public object GetOptionType()
		{
			if (this._nativeType != NativeOptions.NativeOptionsType.None)
			{
				return this._nativeType;
			}
			if (this._managedType != ManagedOptions.ManagedOptionsType.Language)
			{
				return this._managedType;
			}
			return this._actionOptionTypeId;
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000D8E71 File Offset: 0x000D7071
		public float GetValue(bool forceRefresh)
		{
			return 0f;
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000D8E78 File Offset: 0x000D7078
		public bool IsNative()
		{
			return this._nativeType != NativeOptions.NativeOptionsType.None;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000D8E86 File Offset: 0x000D7086
		public void SetValue(float value)
		{
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000D8E88 File Offset: 0x000D7088
		public bool IsAction()
		{
			return this._nativeType == NativeOptions.NativeOptionsType.None && this._managedType == ManagedOptions.ManagedOptionsType.Language;
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000D8E9E File Offset: 0x000D709E
		public ValueTuple<string, bool> GetIsDisabledAndReasonID()
		{
			return new ValueTuple<string, bool>(string.Empty, false);
		}

		// Token: 0x04001656 RID: 5718
		private ManagedOptions.ManagedOptionsType _managedType;

		// Token: 0x04001657 RID: 5719
		private NativeOptions.NativeOptionsType _nativeType;

		// Token: 0x04001658 RID: 5720
		private string _actionOptionTypeId;
	}
}

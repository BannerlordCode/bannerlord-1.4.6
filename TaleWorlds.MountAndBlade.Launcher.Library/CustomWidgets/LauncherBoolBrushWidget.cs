using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Launcher.Library.CustomWidgets
{
	// Token: 0x0200001F RID: 31
	public class LauncherBoolBrushWidget : BrushWidget
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00005F50 File Offset: 0x00004150
		public LauncherBoolBrushWidget(UIContext context)
			: base(context)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005F59 File Offset: 0x00004159
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			this.BoolVariableUpdated();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005F67 File Offset: 0x00004167
		private void BoolVariableUpdated()
		{
			(this.TargetWidget ?? this).Brush = (this.BoolVariable ? this.OnTrueBrush : this.OnFalseBrush);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005F8F File Offset: 0x0000418F
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00005F97 File Offset: 0x00004197
		[DataSourceProperty]
		public bool BoolVariable
		{
			get
			{
				return this._boolVariable;
			}
			set
			{
				if (value != this._boolVariable)
				{
					this._boolVariable = value;
					base.OnPropertyChanged(value, "BoolVariable");
					this.BoolVariableUpdated();
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00005FBB File Offset: 0x000041BB
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00005FC3 File Offset: 0x000041C3
		[DataSourceProperty]
		public BrushWidget TargetWidget
		{
			get
			{
				return this._targetWidget;
			}
			set
			{
				if (value != this._targetWidget)
				{
					this._targetWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "TargetWidget");
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005FE1 File Offset: 0x000041E1
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00005FE9 File Offset: 0x000041E9
		[DataSourceProperty]
		public Brush OnTrueBrush
		{
			get
			{
				return this._onTrueBrush;
			}
			set
			{
				if (value != this._onTrueBrush)
				{
					this._onTrueBrush = value;
					base.OnPropertyChanged<Brush>(value, "OnTrueBrush");
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006007 File Offset: 0x00004207
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000600F File Offset: 0x0000420F
		[DataSourceProperty]
		public Brush OnFalseBrush
		{
			get
			{
				return this._onFalseBrush;
			}
			set
			{
				if (value != this._onFalseBrush)
				{
					this._onFalseBrush = value;
					base.OnPropertyChanged<Brush>(value, "OnFalseBrush");
				}
			}
		}

		// Token: 0x0400009A RID: 154
		private bool _boolVariable;

		// Token: 0x0400009B RID: 155
		private BrushWidget _targetWidget;

		// Token: 0x0400009C RID: 156
		private Brush _onTrueBrush;

		// Token: 0x0400009D RID: 157
		private Brush _onFalseBrush;
	}
}

using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.Core
{
	// Token: 0x02000013 RID: 19
	public class BannerData
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000426A File Offset: 0x0000246A
		public int LocalVersion
		{
			get
			{
				return this._localVersion;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004272 File Offset: 0x00002472
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000427A File Offset: 0x0000247A
		public int MeshId
		{
			get
			{
				return this._meshId;
			}
			set
			{
				if (value != this._meshId)
				{
					this._meshId = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000429A File Offset: 0x0000249A
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000042A2 File Offset: 0x000024A2
		public int ColorId
		{
			get
			{
				return this._colorId;
			}
			set
			{
				if (value != this._colorId)
				{
					this._colorId = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000042C2 File Offset: 0x000024C2
		// (set) Token: 0x060000CA RID: 202 RVA: 0x000042CA File Offset: 0x000024CA
		public int ColorId2
		{
			get
			{
				return this._colorId2;
			}
			set
			{
				if (value != this._colorId2)
				{
					this._colorId2 = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000042EA File Offset: 0x000024EA
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000042F2 File Offset: 0x000024F2
		public Vec2 Size
		{
			get
			{
				return this._size;
			}
			set
			{
				if (value != this._size)
				{
					this._size = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004317 File Offset: 0x00002517
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000431F File Offset: 0x0000251F
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (value != this._position)
				{
					this._position = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004344 File Offset: 0x00002544
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x0000434C File Offset: 0x0000254C
		public bool DrawStroke
		{
			get
			{
				return this._drawStroke;
			}
			set
			{
				if (value != this._drawStroke)
				{
					this._drawStroke = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000436C File Offset: 0x0000256C
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004374 File Offset: 0x00002574
		public bool Mirror
		{
			get
			{
				return this._mirror;
			}
			set
			{
				if (value != this._mirror)
				{
					this._mirror = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004394 File Offset: 0x00002594
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000439C File Offset: 0x0000259C
		public float RotationValue
		{
			get
			{
				return this._rotationValue;
			}
			set
			{
				if (value != this._rotationValue)
				{
					this._rotationValue = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000043BC File Offset: 0x000025BC
		public float Rotation
		{
			get
			{
				return 6.2831855f * this.RotationValue;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000043CC File Offset: 0x000025CC
		public BannerData(int meshId, int colorId, int colorId2, Vec2 size, Vec2 position, bool drawStroke, bool mirror, float rotationValue)
		{
			this.MeshId = meshId;
			this.ColorId = colorId;
			this.ColorId2 = colorId2;
			this.Size = size;
			this.Position = position;
			this.DrawStroke = drawStroke;
			this.Mirror = mirror;
			this.RotationValue = rotationValue;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000441C File Offset: 0x0000261C
		public BannerData(BannerData bannerData)
			: this(bannerData.MeshId, bannerData.ColorId, bannerData.ColorId2, bannerData.Size, bannerData.Position, bannerData.DrawStroke, bannerData.Mirror, bannerData.RotationValue)
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004460 File Offset: 0x00002660
		public override bool Equals(object obj)
		{
			BannerData bannerData;
			return (bannerData = obj as BannerData) != null && bannerData.MeshId == this.MeshId && bannerData.ColorId == this.ColorId && bannerData.ColorId2 == this.ColorId2 && bannerData.Size.X == this.Size.X && bannerData.Size.Y == this.Size.Y && bannerData.Position.X == this.Position.X && bannerData.Position.Y == this.Position.Y && bannerData.DrawStroke == this.DrawStroke && bannerData.Mirror == this.Mirror && bannerData.RotationValue == this.RotationValue;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004555 File Offset: 0x00002755
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000455D File Offset: 0x0000275D
		internal static void AutoGeneratedStaticCollectObjectsBannerData(object o, List<object> collectedObjects)
		{
			((BannerData)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000456B File Offset: 0x0000276B
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000456D File Offset: 0x0000276D
		internal static object AutoGeneratedGetMemberValue_colorId2(object o)
		{
			return ((BannerData)o)._colorId2;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000457F File Offset: 0x0000277F
		internal static object AutoGeneratedGetMemberValue_size(object o)
		{
			return ((BannerData)o)._size;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004591 File Offset: 0x00002791
		internal static object AutoGeneratedGetMemberValue_position(object o)
		{
			return ((BannerData)o)._position;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000045A3 File Offset: 0x000027A3
		internal static object AutoGeneratedGetMemberValue_drawStroke(object o)
		{
			return ((BannerData)o)._drawStroke;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000045B5 File Offset: 0x000027B5
		internal static object AutoGeneratedGetMemberValue_mirror(object o)
		{
			return ((BannerData)o)._mirror;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000045C7 File Offset: 0x000027C7
		internal static object AutoGeneratedGetMemberValue_rotationValue(object o)
		{
			return ((BannerData)o)._rotationValue;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000045D9 File Offset: 0x000027D9
		internal static object AutoGeneratedGetMemberValue_meshId(object o)
		{
			return ((BannerData)o)._meshId;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000045EB File Offset: 0x000027EB
		internal static object AutoGeneratedGetMemberValue_colorId(object o)
		{
			return ((BannerData)o)._colorId;
		}

		// Token: 0x04000108 RID: 264
		public const float RotationPrecision = 0.0027777778f;

		// Token: 0x04000109 RID: 265
		[CachedData]
		private int _localVersion;

		// Token: 0x0400010A RID: 266
		[SaveableField(1)]
		private int _meshId;

		// Token: 0x0400010B RID: 267
		[SaveableField(2)]
		private int _colorId;

		// Token: 0x0400010C RID: 268
		[SaveableField(3)]
		public int _colorId2;

		// Token: 0x0400010D RID: 269
		[SaveableField(4)]
		public Vec2 _size;

		// Token: 0x0400010E RID: 270
		[SaveableField(5)]
		public Vec2 _position;

		// Token: 0x0400010F RID: 271
		[SaveableField(6)]
		public bool _drawStroke;

		// Token: 0x04000110 RID: 272
		[SaveableField(7)]
		public bool _mirror;

		// Token: 0x04000111 RID: 273
		[SaveableField(8)]
		public float _rotationValue;
	}
}

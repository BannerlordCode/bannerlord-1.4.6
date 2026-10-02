using System;

namespace TaleWorlds.Core.ImageIdentifiers
{
	// Token: 0x020000E7 RID: 231
	public abstract class ImageIdentifier
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0002564D File Offset: 0x0002384D
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x00025655 File Offset: 0x00023855
		public string Id { get; set; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002565E File Offset: 0x0002385E
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x00025666 File Offset: 0x00023866
		public string TextureProviderName { get; protected set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0002566F File Offset: 0x0002386F
		// (set) Token: 0x06000B92 RID: 2962 RVA: 0x00025677 File Offset: 0x00023877
		public string AdditionalArgs { get; protected set; }

		// Token: 0x06000B93 RID: 2963 RVA: 0x00025680 File Offset: 0x00023880
		public bool Equals(ImageIdentifier other)
		{
			return other != null && this.Id.Equals(other.Id) && this.AdditionalArgs.Equals(other.AdditionalArgs) && this.TextureProviderName.Equals(other.TextureProviderName);
		}
	}
}

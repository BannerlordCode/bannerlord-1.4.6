using System;

namespace TaleWorlds.Engine
{
	// Token: 0x0200009D RID: 157
	public struct WeakMaterial
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00011442 File Offset: 0x0000F642
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x0001144A File Offset: 0x0000F64A
		public UIntPtr Pointer { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00011453 File Offset: 0x0000F653
		public bool IsValid
		{
			get
			{
				return this.Pointer != UIntPtr.Zero;
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00011465 File Offset: 0x0000F665
		internal WeakMaterial(UIntPtr pointer)
		{
			this.Pointer = pointer;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0001146E File Offset: 0x0000F66E
		public Shader GetShader()
		{
			return EngineApplicationInterface.IMaterial.GetShader(this.Pointer);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00011480 File Offset: 0x0000F680
		public ulong GetShaderFlags()
		{
			return EngineApplicationInterface.IMaterial.GetShaderFlags(this.Pointer);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00011492 File Offset: 0x0000F692
		public void SetShaderFlags(ulong flagEntry)
		{
			EngineApplicationInterface.IMaterial.SetShaderFlags(this.Pointer, flagEntry);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x000114A5 File Offset: 0x0000F6A5
		public void SetMeshVectorArgument(float x, float y, float z, float w)
		{
			EngineApplicationInterface.IMaterial.SetMeshVectorArgument(this.Pointer, x, y, z, w);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000114BC File Offset: 0x0000F6BC
		public void SetTexture(Material.MBTextureType textureType, Texture texture)
		{
			EngineApplicationInterface.IMaterial.SetTexture(this.Pointer, (int)textureType, texture.Pointer);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000114D5 File Offset: 0x0000F6D5
		public void SetTextureAtSlot(int textureSlot, Texture texture)
		{
			EngineApplicationInterface.IMaterial.SetTextureAtSlot(this.Pointer, textureSlot, texture.Pointer);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000114EE File Offset: 0x0000F6EE
		public void SetAreaMapScale(float scale)
		{
			EngineApplicationInterface.IMaterial.SetAreaMapScale(this.Pointer, scale);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00011501 File Offset: 0x0000F701
		public void SetEnableSkinning(bool enable)
		{
			EngineApplicationInterface.IMaterial.SetEnableSkinning(this.Pointer, enable);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00011514 File Offset: 0x0000F714
		public bool UsingSkinning()
		{
			return EngineApplicationInterface.IMaterial.UsingSkinning(this.Pointer);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00011526 File Offset: 0x0000F726
		public Texture GetTexture(Material.MBTextureType textureType)
		{
			return EngineApplicationInterface.IMaterial.GetTexture(this.Pointer, (int)textureType);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00011539 File Offset: 0x0000F739
		public Texture GetTextureWithSlot(int textureSlot)
		{
			return EngineApplicationInterface.IMaterial.GetTexture(this.Pointer, textureSlot);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0001154C File Offset: 0x0000F74C
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x0001155E File Offset: 0x0000F75E
		public string Name
		{
			get
			{
				return EngineApplicationInterface.IMaterial.GetName(this.Pointer);
			}
			set
			{
				EngineApplicationInterface.IMaterial.SetName(this.Pointer, value);
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00011571 File Offset: 0x0000F771
		public void AddMaterialShaderFlag(string flagName, bool showErrors)
		{
			EngineApplicationInterface.IMaterial.AddMaterialShaderFlag(this.Pointer, flagName, showErrors);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00011585 File Offset: 0x0000F785
		public void RemoveMaterialShaderFlag(string flagName)
		{
			EngineApplicationInterface.IMaterial.RemoveMaterialShaderFlag(this.Pointer, flagName);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00011598 File Offset: 0x0000F798
		public static bool operator ==(WeakMaterial weakMaterial1, WeakMaterial weakMaterial2)
		{
			return weakMaterial1.Pointer == weakMaterial2.Pointer;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000115AD File Offset: 0x0000F7AD
		public static bool operator !=(WeakMaterial weakMaterial1, WeakMaterial weakMaterial2)
		{
			return weakMaterial1.Pointer != weakMaterial2.Pointer;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000115C2 File Offset: 0x0000F7C2
		public override bool Equals(object obj)
		{
			return ((Material)obj).Pointer == this.Pointer;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000115DC File Offset: 0x0000F7DC
		public override int GetHashCode()
		{
			return this.Pointer.GetHashCode();
		}

		// Token: 0x04000205 RID: 517
		public static readonly WeakMaterial Invalid = new WeakMaterial(UIntPtr.Zero);
	}
}

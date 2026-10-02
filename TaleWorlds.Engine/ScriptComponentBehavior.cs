using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000088 RID: 136
	public abstract class ScriptComponentBehavior : DotNetObject
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x0000D533 File Offset: 0x0000B733
		protected void InvalidateWeakPointersIfValid()
		{
			this._scriptComponent.ManualInvalidate();
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0000D540 File Offset: 0x0000B740
		public WeakGameEntity GameEntity
		{
			get
			{
				return this._gameEntity;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0000D548 File Offset: 0x0000B748
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0000D561 File Offset: 0x0000B761
		public ManagedScriptComponent ScriptComponent
		{
			get
			{
				WeakNativeObjectReference scriptComponent = this._scriptComponent;
				return ((scriptComponent != null) ? scriptComponent.GetNativeObject() : null) as ManagedScriptComponent;
			}
			private set
			{
				this._scriptComponent = new WeakNativeObjectReference(value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0000D56F File Offset: 0x0000B76F
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0000D577 File Offset: 0x0000B777
		private protected ManagedScriptHolder ManagedScriptHolder { protected get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0000D580 File Offset: 0x0000B780
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0000D599 File Offset: 0x0000B799
		public Scene Scene
		{
			get
			{
				WeakNativeObjectReference scene = this._scene;
				return ((scene != null) ? scene.GetNativeObject() : null) as Scene;
			}
			private set
			{
				this._scene = new WeakNativeObjectReference(value);
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0000D5A7 File Offset: 0x0000B7A7
		static ScriptComponentBehavior()
		{
			if (ScriptComponentBehavior.CachedFields == null)
			{
				ScriptComponentBehavior.CachedFields = new Dictionary<string, string[]>();
				ScriptComponentBehavior.CacheEditableFieldsForAllScriptComponents();
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0000D5DB File Offset: 0x0000B7DB
		internal void Construct(UIntPtr myEntityPtr, ManagedScriptComponent scriptComponent)
		{
			this._gameEntity = new WeakGameEntity(myEntityPtr);
			this.Scene = this._gameEntity.Scene;
			this.ScriptComponent = scriptComponent;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0000D601 File Offset: 0x0000B801
		internal void SetOwnerManagedScriptHolder(ManagedScriptHolder managedScriptHolder)
		{
			this.ManagedScriptHolder = managedScriptHolder;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0000D60A File Offset: 0x0000B80A
		private void SetScriptComponentToTickAux(ScriptComponentBehavior.TickRequirement value)
		{
			if (this._lastTickRequirement != value)
			{
				this.ManagedScriptHolder.UpdateTickRequirement(this, this._lastTickRequirement, value);
				this._lastTickRequirement = value;
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0000D62F File Offset: 0x0000B82F
		public void SetScriptComponentToTick(ScriptComponentBehavior.TickRequirement tickReq)
		{
			if (this.ManagedScriptHolder != null)
			{
				this.SetScriptComponentToTickAux(tickReq);
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0000D640 File Offset: 0x0000B840
		public void SetScriptComponentToTickMT(ScriptComponentBehavior.TickRequirement value)
		{
			if (this.ManagedScriptHolder != null)
			{
				object addRemoveLockObject = this.ManagedScriptHolder.AddRemoveLockObject;
				lock (addRemoveLockObject)
				{
					this.SetScriptComponentToTickAux(value);
				}
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0000D690 File Offset: 0x0000B890
		[EngineCallback(null, false)]
		internal void AddScriptComponentToTick()
		{
			List<ScriptComponentBehavior> prefabScriptComponents = ScriptComponentBehavior._prefabScriptComponents;
			lock (prefabScriptComponents)
			{
				if (!ScriptComponentBehavior._prefabScriptComponents.Contains(this))
				{
					ScriptComponentBehavior._prefabScriptComponents.Add(this);
				}
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0000D6E4 File Offset: 0x0000B8E4
		[EngineCallback(null, false)]
		internal void RegisterAsPrefabScriptComponent()
		{
			List<ScriptComponentBehavior> prefabScriptComponents = ScriptComponentBehavior._prefabScriptComponents;
			lock (prefabScriptComponents)
			{
				if (!ScriptComponentBehavior._prefabScriptComponents.Contains(this))
				{
					ScriptComponentBehavior._prefabScriptComponents.Add(this);
				}
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0000D738 File Offset: 0x0000B938
		[EngineCallback(null, false)]
		internal void DeregisterAsPrefabScriptComponent()
		{
			List<ScriptComponentBehavior> prefabScriptComponents = ScriptComponentBehavior._prefabScriptComponents;
			lock (prefabScriptComponents)
			{
				ScriptComponentBehavior._prefabScriptComponents.Remove(this);
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0000D780 File Offset: 0x0000B980
		[EngineCallback(null, false)]
		internal void RegisterAsUndoStackScriptComponent()
		{
			if (!ScriptComponentBehavior._undoStackScriptComponents.Contains(this))
			{
				ScriptComponentBehavior._undoStackScriptComponents.Add(this);
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0000D79A File Offset: 0x0000B99A
		[EngineCallback(null, false)]
		internal void DeregisterAsUndoStackScriptComponent()
		{
			if (ScriptComponentBehavior._undoStackScriptComponents.Contains(this))
			{
				ScriptComponentBehavior._undoStackScriptComponents.Remove(this);
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0000D7B5 File Offset: 0x0000B9B5
		[EngineCallback(null, false)]
		protected internal virtual void SetScene(Scene scene)
		{
			this.Scene = scene;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0000D7BE File Offset: 0x0000B9BE
		[EngineCallback(null, false)]
		protected internal virtual void OnInit()
		{
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		[EngineCallback(null, false)]
		protected internal void HandleOnRemoved(int removeReason)
		{
			this.OnRemoved(removeReason);
			this._scene = null;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		protected virtual void OnRemoved(int removeReason)
		{
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0000D7D2 File Offset: 0x0000B9D2
		public virtual ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.None;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0000D7D5 File Offset: 0x0000B9D5
		protected internal virtual bool CanPhysicsCollideBetweenTwoEntities(WeakGameEntity myEntity, BodyFlags myEntityBodyFlags, WeakGameEntity otherEntity, BodyFlags otherEntityBodyFlags)
		{
			return true;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		protected internal virtual void OnFixedTick(float fixedDt)
		{
			Debug.FailedAssert("This base function should never be called.", "C:\\BuildAgent\\work\\mb3\\Source\\Engine\\TaleWorlds.Engine\\ScriptComponentBehavior.cs", "OnFixedTick", 253);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0000D7F3 File Offset: 0x0000B9F3
		protected internal virtual void OnParallelFixedTick(float fixedDt)
		{
			Debug.FailedAssert("This base function should never be called.", "C:\\BuildAgent\\work\\mb3\\Source\\Engine\\TaleWorlds.Engine\\ScriptComponentBehavior.cs", "OnParallelFixedTick", 259);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0000D80E File Offset: 0x0000BA0E
		protected internal virtual void OnTick(float dt)
		{
			Debug.FailedAssert("This base function should never be called.", "C:\\BuildAgent\\work\\mb3\\Source\\Engine\\TaleWorlds.Engine\\ScriptComponentBehavior.cs", "OnTick", 265);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0000D829 File Offset: 0x0000BA29
		protected internal virtual void OnTickParallel(float dt)
		{
			Debug.FailedAssert("This base function should never be called.", "C:\\BuildAgent\\work\\mb3\\Source\\Engine\\TaleWorlds.Engine\\ScriptComponentBehavior.cs", "OnTickParallel", 271);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0000D844 File Offset: 0x0000BA44
		protected internal virtual void OnTickParallel2(float dt)
		{
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0000D846 File Offset: 0x0000BA46
		protected internal virtual void OnTickParallel3(float dt)
		{
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0000D848 File Offset: 0x0000BA48
		protected internal virtual void OnTickOccasionally(float currentFrameDeltaTime)
		{
			Debug.FailedAssert("This base function should never be called.", "C:\\BuildAgent\\work\\mb3\\Source\\Engine\\TaleWorlds.Engine\\ScriptComponentBehavior.cs", "OnTickOccasionally", 289);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0000D863 File Offset: 0x0000BA63
		[EngineCallback(null, false)]
		protected internal virtual void OnPreInit()
		{
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0000D865 File Offset: 0x0000BA65
		[EngineCallback(null, false)]
		protected internal virtual void OnEditorInit()
		{
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0000D867 File Offset: 0x0000BA67
		[EngineCallback(null, false)]
		protected internal virtual void OnEditorTick(float dt)
		{
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0000D869 File Offset: 0x0000BA69
		[EngineCallback(null, false)]
		protected internal virtual void OnEditorValidate()
		{
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0000D86B File Offset: 0x0000BA6B
		[EngineCallback(null, false)]
		protected internal virtual bool IsOnlyVisual()
		{
			return false;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0000D86E File Offset: 0x0000BA6E
		[EngineCallback(null, false)]
		protected internal virtual bool MovesEntity()
		{
			return true;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0000D871 File Offset: 0x0000BA71
		[EngineCallback(null, false)]
		protected internal virtual bool DisablesOroCreation()
		{
			return true;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0000D874 File Offset: 0x0000BA74
		[EngineCallback(null, false)]
		protected internal virtual void OnEditorVariableChanged(string variableName)
		{
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0000D876 File Offset: 0x0000BA76
		protected internal virtual bool SkeletonPostIntegrateCallback(AnimResult animResult)
		{
			return false;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0000D87C File Offset: 0x0000BA7C
		[EngineCallback(null, false)]
		internal static bool SkeletonPostIntegrateCallbackAux(ScriptComponentBehavior script, UIntPtr animResultPointer)
		{
			AnimResult animResult = AnimResult.CreateWithPointer(animResultPointer);
			return script.SkeletonPostIntegrateCallback(animResult);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0000D897 File Offset: 0x0000BA97
		[EngineCallback(null, false)]
		protected internal virtual void OnSceneSave(string saveFolder)
		{
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0000D899 File Offset: 0x0000BA99
		[EngineCallback(null, false)]
		protected internal virtual bool OnCheckForProblems()
		{
			return false;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0000D89C File Offset: 0x0000BA9C
		[EngineCallback(null, false)]
		protected internal virtual void OnSaveAsPrefab()
		{
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0000D89E File Offset: 0x0000BA9E
		[EngineCallback(null, false)]
		protected internal virtual void OnTerrainReload(int step)
		{
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
		[EngineCallback(null, false)]
		protected internal void OnPhysicsCollisionAux(ref PhysicsContact contact, UIntPtr entity0, UIntPtr entity1)
		{
			this.OnPhysicsCollision(ref contact, new WeakGameEntity(entity0), new WeakGameEntity(entity1));
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0000D8B5 File Offset: 0x0000BAB5
		protected internal virtual void OnPhysicsCollision(ref PhysicsContact contact, WeakGameEntity entity0, WeakGameEntity entity1)
		{
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0000D8B7 File Offset: 0x0000BAB7
		[EngineCallback(null, false)]
		protected internal virtual void OnEditModeVisibilityChanged(bool currentVisibility)
		{
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0000D8B9 File Offset: 0x0000BAB9
		[EngineCallback(null, false)]
		protected internal virtual void OnBoundingBoxValidate()
		{
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0000D8BB File Offset: 0x0000BABB
		[EngineCallback(null, false)]
		protected internal virtual void OnDynamicNavmeshVertexUpdate()
		{
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0000D8C0 File Offset: 0x0000BAC0
		private static void CacheEditableFieldsForAllScriptComponents()
		{
			foreach (KeyValuePair<string, Type> keyValuePair in Managed.ModuleTypes)
			{
				Type value = keyValuePair.Value;
				string text = keyValuePair.Key;
				object[] customAttributesSafe = value.GetCustomAttributesSafe(typeof(ScriptComponentParams), true);
				if (customAttributesSafe.Length != 0)
				{
					ScriptComponentParams scriptComponentParams = (ScriptComponentParams)customAttributesSafe[0];
					if (scriptComponentParams.NameOverride.Length > 0)
					{
						text = scriptComponentParams.NameOverride;
					}
				}
				ScriptComponentBehavior.CachedFields.Add(text, ScriptComponentBehavior.CollectEditableFields(value));
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0000D968 File Offset: 0x0000BB68
		private static string[] CollectEditableFields(Type type)
		{
			List<string> list = new List<string>();
			List<FieldInfo> list2 = new List<FieldInfo>();
			while (type != null)
			{
				list2.AddRange(type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
				type = type.BaseType;
			}
			for (int i = 0; i < list2.Count; i++)
			{
				FieldInfo fieldInfo = list2[i];
				string text = list2[i].Name;
				object[] customAttributesSafe = fieldInfo.GetCustomAttributesSafe(typeof(EditableScriptComponentVariable), true);
				bool flag = false;
				if (customAttributesSafe.Length != 0)
				{
					EditableScriptComponentVariable editableScriptComponentVariable = (EditableScriptComponentVariable)customAttributesSafe[0];
					bool isStatic = fieldInfo.IsStatic;
					bool isInitOnly = fieldInfo.IsInitOnly;
					if (editableScriptComponentVariable.OverrideFieldName.Length > 0)
					{
						text = editableScriptComponentVariable.OverrideFieldName;
					}
					flag = editableScriptComponentVariable.Visible;
				}
				else if (!fieldInfo.IsPrivate && !fieldInfo.IsFamily)
				{
					flag = true;
				}
				if (fieldInfo.IsStatic)
				{
					flag = false;
				}
				if (flag)
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0000DA58 File Offset: 0x0000BC58
		[EngineCallback(null, false)]
		internal static string[] GetEditableFields(string className)
		{
			string[] array;
			ScriptComponentBehavior.CachedFields.TryGetValue(className, out array);
			return array;
		}

		// Token: 0x040001B6 RID: 438
		private static List<ScriptComponentBehavior> _prefabScriptComponents = new List<ScriptComponentBehavior>();

		// Token: 0x040001B7 RID: 439
		private static List<ScriptComponentBehavior> _undoStackScriptComponents = new List<ScriptComponentBehavior>();

		// Token: 0x040001B8 RID: 440
		private WeakGameEntity _gameEntity;

		// Token: 0x040001B9 RID: 441
		private WeakNativeObjectReference _scriptComponent;

		// Token: 0x040001BA RID: 442
		private ScriptComponentBehavior.TickRequirement _lastTickRequirement;

		// Token: 0x040001BB RID: 443
		private static readonly Dictionary<string, string[]> CachedFields;

		// Token: 0x040001BD RID: 445
		private WeakNativeObjectReference _scene;

		// Token: 0x020000D0 RID: 208
		[Flags]
		public enum TickRequirement : uint
		{
			// Token: 0x0400043D RID: 1085
			None = 0U,
			// Token: 0x0400043E RID: 1086
			TickOccasionally = 1U,
			// Token: 0x0400043F RID: 1087
			Tick = 2U,
			// Token: 0x04000440 RID: 1088
			TickParallel = 4U,
			// Token: 0x04000441 RID: 1089
			TickParallel2 = 8U,
			// Token: 0x04000442 RID: 1090
			FixedTick = 16U,
			// Token: 0x04000443 RID: 1091
			FixedParallelTick = 32U,
			// Token: 0x04000444 RID: 1092
			TickParallel3 = 64U
		}
	}
}

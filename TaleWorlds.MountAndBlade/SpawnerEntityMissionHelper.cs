using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects.Siege;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000356 RID: 854
	public class SpawnerEntityMissionHelper
	{
		// Token: 0x06003106 RID: 12550 RVA: 0x000C72E4 File Offset: 0x000C54E4
		public SpawnerEntityMissionHelper(SpawnerBase spawner, bool fireVersion = false)
		{
			this._spawner = spawner;
			this._fireVersion = fireVersion;
			this._ownerEntity = GameEntity.CreateFromWeakEntity(this._spawner.GameEntity);
			this._gameEntityName = this._ownerEntity.Name;
			if (this.SpawnPrefab(this._ownerEntity, this.GetPrefabName()) != null)
			{
				this.SyncMatrixFrames();
			}
			else
			{
				Debug.FailedAssert("Spawner couldn't spawn a proper entity.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\SpawnerEntityMissionHelper.cs", ".ctor", 34);
			}
			this._spawner.AssignParameters(this);
			this.CallSetSpawnedFromSpawnerOfScripts();
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000C7378 File Offset: 0x000C5578
		private GameEntity SpawnPrefab(GameEntity parent, string entityName)
		{
			this.InstantiateEntity(parent, entityName);
			this.SpawnedEntity.SetMobility(GameEntity.Mobility.Dynamic);
			this.SpawnedEntity.EntityFlags = this.SpawnedEntity.EntityFlags | EntityFlags.DontSaveToScene;
			parent.AddChild(this.SpawnedEntity, false);
			MatrixFrame identity = MatrixFrame.Identity;
			this.SpawnedEntity.SetFrame(ref identity, true);
			foreach (string text in this._ownerEntity.Tags)
			{
				this.SpawnedEntity.AddTag(text);
			}
			return this.SpawnedEntity;
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000C7406 File Offset: 0x000C5606
		protected virtual void InstantiateEntity(GameEntity parent, string entityName)
		{
			this.SpawnedEntity = GameEntity.Instantiate(parent.Scene, entityName, false, true, "");
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000C7421 File Offset: 0x000C5621
		private void RemoveChildEntity(GameEntity child)
		{
			child.CallScriptCallbacks(false);
			child.Remove(85);
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000C7434 File Offset: 0x000C5634
		private void SyncMatrixFrames()
		{
			List<GameEntity> list = new List<GameEntity>();
			this.SpawnedEntity.GetChildrenRecursive(ref list);
			foreach (GameEntity gameEntity in list)
			{
				if (SpawnerEntityMissionHelper.HasField(this._spawner, gameEntity.Name))
				{
					MatrixFrame matrixFrame = (MatrixFrame)SpawnerEntityMissionHelper.GetFieldValue(this._spawner, gameEntity.Name);
					gameEntity.SetFrame(ref matrixFrame, true);
				}
				if (SpawnerEntityMissionHelper.HasField(this._spawner, gameEntity.Name + "_enabled") && !(bool)SpawnerEntityMissionHelper.GetFieldValue(this._spawner, gameEntity.Name + "_enabled"))
				{
					this.RemoveChildEntity(gameEntity);
				}
			}
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000C7510 File Offset: 0x000C5710
		private void CallSetSpawnedFromSpawnerOfScripts()
		{
			foreach (GameEntity gameEntity in this.SpawnedEntity.GetEntityAndChildren())
			{
				foreach (ScriptComponentBehavior scriptComponentBehavior in from x in gameEntity.GetScriptComponents()
					where x is ISpawnable
					select x)
				{
					(scriptComponentBehavior as ISpawnable).SetSpawnedFromSpawner();
				}
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000C75BC File Offset: 0x000C57BC
		private string GetPrefabName()
		{
			string text;
			if (this._spawner.ToBeSpawnedOverrideName != "")
			{
				text = this._spawner.ToBeSpawnedOverrideName;
			}
			else
			{
				text = this._gameEntityName;
				text = text.Remove(this._gameEntityName.Length - this._gameEntityName.Split(new char[] { '_' }).Last<string>().Length - 1);
			}
			if (this._fireVersion)
			{
				if (this._spawner.ToBeSpawnedOverrideNameForFireVersion != "")
				{
					text = this._spawner.ToBeSpawnedOverrideNameForFireVersion;
				}
				else
				{
					text += "_fire";
				}
			}
			return text;
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000C7664 File Offset: 0x000C5864
		private static object GetFieldValue(object src, string propName)
		{
			return src.GetType().GetField(propName).GetValue(src);
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000C7678 File Offset: 0x000C5878
		private static bool HasField(object obj, string propertyName)
		{
			return obj.GetType().GetField(propertyName) != null;
		}

		// Token: 0x0400148E RID: 5262
		private const string EnabledSuffix = "_enabled";

		// Token: 0x0400148F RID: 5263
		public GameEntity SpawnedEntity;

		// Token: 0x04001490 RID: 5264
		private GameEntity _ownerEntity;

		// Token: 0x04001491 RID: 5265
		private SpawnerBase _spawner;

		// Token: 0x04001492 RID: 5266
		private string _gameEntityName;

		// Token: 0x04001493 RID: 5267
		private bool _fireVersion;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000355 RID: 853
	public class SpawnerEntityEditorHelper
	{
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x000C62D6 File Offset: 0x000C44D6
		// (set) Token: 0x060030F0 RID: 12528 RVA: 0x000C62DE File Offset: 0x000C44DE
		public bool IsValid { get; private set; }

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x000C62E7 File Offset: 0x000C44E7
		// (set) Token: 0x060030F2 RID: 12530 RVA: 0x000C62EF File Offset: 0x000C44EF
		public GameEntity SpawnedGhostEntity { get; private set; }

		// Token: 0x060030F3 RID: 12531 RVA: 0x000C62F8 File Offset: 0x000C44F8
		public SpawnerEntityEditorHelper(ScriptComponentBehavior spawner)
		{
			this.spawner_ = spawner;
			if (this.AddGhostEntity(this.spawner_.GameEntity, this.GetGhostName()) != null)
			{
				this.SyncMatrixFrames(true);
				this.IsValid = true;
				return;
			}
			Debug.FailedAssert("No prefab found. Spawner script will remove itself.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\SpawnerEntityEditorHelper.cs", ".ctor", 75);
			spawner.GameEntity.RemoveScriptComponent(this.spawner_.ScriptComponent.Pointer, 11);
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000C63A0 File Offset: 0x000C45A0
		public GameEntity GetGhostEntityOrChild(string name)
		{
			if (this.SpawnedGhostEntity.Name == name)
			{
				return this.SpawnedGhostEntity;
			}
			List<GameEntity> list = new List<GameEntity>();
			this.SpawnedGhostEntity.GetChildrenRecursive(ref list);
			GameEntity gameEntity = list.FirstOrDefault<GameEntity>((GameEntity x) => x.Name == name);
			if (gameEntity != null)
			{
				return gameEntity;
			}
			return null;
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000C640C File Offset: 0x000C460C
		public void Tick(float dt)
		{
			if (this.SpawnedGhostEntity.Parent != this.spawner_.GameEntity)
			{
				this.IsValid = false;
				this.spawner_.GameEntity.RemoveScriptComponent(this.spawner_.ScriptComponent.Pointer, 12);
			}
			if (this.IsValid)
			{
				if (this.LockGhostParent)
				{
					MatrixFrame frame = this.SpawnedGhostEntity.GetFrame();
					MatrixFrame identity = MatrixFrame.Identity;
					bool flag = (in frame) != (in identity);
					MatrixFrame identity2 = MatrixFrame.Identity;
					this.SpawnedGhostEntity.SetFrame(ref identity2, true);
					if (flag)
					{
						this.SpawnedGhostEntity.UpdateTriadFrameForEditor();
					}
				}
				this.SyncMatrixFrames(false);
				if (this._ghostMovementMode)
				{
					this.UpdateGhostMovement(dt);
				}
			}
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000C64C4 File Offset: 0x000C46C4
		public void GivePermission(string childName, SpawnerEntityEditorHelper.Permission permission, Action<float> onChangeFunction)
		{
			this._stableChildrenPermissions.Add(Tuple.Create<string, SpawnerEntityEditorHelper.Permission, Action<float>>(childName, permission, onChangeFunction));
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000C64DC File Offset: 0x000C46DC
		private void ApplyPermissions()
		{
			using (List<Tuple<string, SpawnerEntityEditorHelper.Permission, Action<float>>>.Enumerator enumerator = this._stableChildrenPermissions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Tuple<string, SpawnerEntityEditorHelper.Permission, Action<float>> item = enumerator.Current;
					KeyValuePair<string, MatrixFrame> keyValuePair = this.stableChildrenFrames.Find((KeyValuePair<string, MatrixFrame> x) => x.Key == item.Item1);
					MatrixFrame frame = this.GetGhostEntityOrChild(item.Item1).GetFrame();
					if (!frame.NearlyEquals(keyValuePair.Value, 1E-05f))
					{
						SpawnerEntityEditorHelper.PermissionType typeOfPermission = item.Item2.TypeOfPermission;
						if (typeOfPermission != SpawnerEntityEditorHelper.PermissionType.scale)
						{
							if (typeOfPermission == SpawnerEntityEditorHelper.PermissionType.rotation)
							{
								switch (item.Item2.PermittedAxis)
								{
								case SpawnerEntityEditorHelper.Axis.x:
								{
									MatrixFrame matrixFrame = keyValuePair.Value;
									if (!frame.rotation.f.NearlyEquals(in matrixFrame.rotation.f, 1E-05f))
									{
										MatrixFrame matrixFrame2 = keyValuePair.Value;
										if (!frame.rotation.u.NearlyEquals(in matrixFrame2.rotation.u, 1E-05f))
										{
											MatrixFrame matrixFrame3 = keyValuePair.Value;
											if (frame.rotation.s.NearlyEquals(in matrixFrame3.rotation.s, 1E-05f))
											{
												this.ChangeStableChildMatrixFrame(item.Item1, frame);
												item.Item3(frame.rotation.GetEulerAngles().x);
											}
										}
									}
									break;
								}
								case SpawnerEntityEditorHelper.Axis.y:
								{
									MatrixFrame matrixFrame = keyValuePair.Value;
									if (!frame.rotation.s.NearlyEquals(in matrixFrame.rotation.s, 1E-05f))
									{
										MatrixFrame matrixFrame2 = keyValuePair.Value;
										if (!frame.rotation.u.NearlyEquals(in matrixFrame2.rotation.u, 1E-05f))
										{
											MatrixFrame matrixFrame3 = keyValuePair.Value;
											if (frame.rotation.f.NearlyEquals(in matrixFrame3.rotation.f, 1E-05f))
											{
												this.ChangeStableChildMatrixFrame(item.Item1, frame);
												item.Item3(frame.rotation.GetEulerAngles().y);
											}
										}
									}
									break;
								}
								case SpawnerEntityEditorHelper.Axis.z:
								{
									MatrixFrame matrixFrame = keyValuePair.Value;
									if (!frame.rotation.f.NearlyEquals(in matrixFrame.rotation.f, 1E-05f))
									{
										MatrixFrame matrixFrame2 = keyValuePair.Value;
										if (!frame.rotation.s.NearlyEquals(in matrixFrame2.rotation.s, 1E-05f))
										{
											MatrixFrame matrixFrame3 = keyValuePair.Value;
											if (frame.rotation.u.NearlyEquals(in matrixFrame3.rotation.u, 1E-05f))
											{
												this.ChangeStableChildMatrixFrame(item.Item1, frame);
												item.Item3(frame.rotation.GetEulerAngles().z);
											}
										}
									}
									break;
								}
								}
							}
						}
						else
						{
							MatrixFrame matrixFrame = keyValuePair.Value;
							if (frame.origin.NearlyEquals(in matrixFrame.origin, 0.0001f))
							{
								Vec3 vec = frame.rotation.f.NormalizedCopy();
								MatrixFrame matrixFrame2 = keyValuePair.Value;
								Vec3 vec2 = matrixFrame2.rotation.f.NormalizedCopy();
								if (vec.NearlyEquals(in vec2, 0.0001f))
								{
									vec = frame.rotation.u.NormalizedCopy();
									matrixFrame2 = keyValuePair.Value;
									Vec3 vec3 = matrixFrame2.rotation.u.NormalizedCopy();
									if (vec.NearlyEquals(in vec3, 0.0001f))
									{
										vec = frame.rotation.s.NormalizedCopy();
										matrixFrame2 = keyValuePair.Value;
										Vec3 vec4 = matrixFrame2.rotation.s.NormalizedCopy();
										if (vec.NearlyEquals(in vec4, 0.0001f))
										{
											switch (item.Item2.PermittedAxis)
											{
											case SpawnerEntityEditorHelper.Axis.x:
												matrixFrame = keyValuePair.Value;
												if (!frame.rotation.f.NearlyEquals(in matrixFrame.rotation.f, 1E-05f))
												{
													this.ChangeStableChildMatrixFrame(item.Item1, frame);
													item.Item3(frame.rotation.f.Length);
												}
												break;
											case SpawnerEntityEditorHelper.Axis.y:
												matrixFrame = keyValuePair.Value;
												if (!frame.rotation.s.NearlyEquals(in matrixFrame.rotation.s, 1E-05f))
												{
													this.ChangeStableChildMatrixFrame(item.Item1, frame);
													item.Item3(frame.rotation.s.Length);
												}
												break;
											case SpawnerEntityEditorHelper.Axis.z:
												matrixFrame = keyValuePair.Value;
												if (!frame.rotation.u.NearlyEquals(in matrixFrame.rotation.u, 1E-05f))
												{
													this.ChangeStableChildMatrixFrame(item.Item1, frame);
													item.Item3(frame.rotation.u.Length);
												}
												break;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000C6A74 File Offset: 0x000C4C74
		private void ChangeStableChildMatrixFrame(string childName, MatrixFrame matrixFrame)
		{
			this.stableChildrenFrames.RemoveAll((KeyValuePair<string, MatrixFrame> x) => x.Key == childName);
			KeyValuePair<string, MatrixFrame> keyValuePair = new KeyValuePair<string, MatrixFrame>(childName, matrixFrame);
			this.stableChildrenFrames.Add(keyValuePair);
			if (SpawnerEntityEditorHelper.HasField(this.spawner_, childName, true))
			{
				SpawnerEntityEditorHelper.SetSpawnerMatrixFrame(this.spawner_, childName, matrixFrame);
			}
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000C6AE7 File Offset: 0x000C4CE7
		public void ChangeStableChildMatrixFrameAndApply(string childName, MatrixFrame matrixFrame, bool updateTriad = true)
		{
			this.ChangeStableChildMatrixFrame(childName, matrixFrame);
			this.GetGhostEntityOrChild(childName).SetFrame(ref matrixFrame, true);
			if (updateTriad)
			{
				this.SpawnedGhostEntity.UpdateTriadFrameForEditorForAllChildren();
			}
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000C6B10 File Offset: 0x000C4D10
		private GameEntity AddGhostEntity(WeakGameEntity parent, List<string> possibleEntityNames)
		{
			this.spawner_.GameEntity.RemoveAllChildren();
			foreach (string text in possibleEntityNames)
			{
				if (GameEntity.PrefabExists(text))
				{
					this.SpawnedGhostEntity = GameEntity.Instantiate(parent.Scene, text, true, true, "");
					break;
				}
			}
			if (this.SpawnedGhostEntity == null)
			{
				return null;
			}
			this.SpawnedGhostEntity.SetMobility(GameEntity.Mobility.Dynamic);
			this.SpawnedGhostEntity.EntityFlags |= EntityFlags.DontSaveToScene;
			parent.AddChild(this.SpawnedGhostEntity.WeakEntity, false);
			MatrixFrame identity = MatrixFrame.Identity;
			this.SpawnedGhostEntity.SetFrame(ref identity, true);
			this.GetChildrenInitialFrames();
			this.SpawnedGhostEntity.UpdateTriadFrameForEditorForAllChildren();
			return this.SpawnedGhostEntity;
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000C6C00 File Offset: 0x000C4E00
		private void SyncMatrixFrames(bool first)
		{
			this.ApplyPermissions();
			List<GameEntity> list = new List<GameEntity>();
			this.SpawnedGhostEntity.GetChildrenRecursive(ref list);
			using (List<GameEntity>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GameEntity item = enumerator.Current;
					if (SpawnerEntityEditorHelper.HasField(this.spawner_, item.Name, false))
					{
						if (first)
						{
							MatrixFrame matrixFrame = (MatrixFrame)SpawnerEntityEditorHelper.GetFieldValue(this.spawner_, item.Name);
							if (!matrixFrame.IsZero)
							{
								item.SetFrame(ref matrixFrame, true);
							}
						}
						else
						{
							SpawnerEntityEditorHelper.SetSpawnerMatrixFrame(this.spawner_, item.Name, item.GetFrame());
						}
					}
					else
					{
						MatrixFrame value = this.stableChildrenFrames.Find((KeyValuePair<string, MatrixFrame> x) => x.Key == item.Name).Value;
						if (!value.NearlyEquals(item.GetFrame(), 1E-05f))
						{
							item.SetFrame(ref value, true);
							this.SpawnedGhostEntity.UpdateTriadFrameForEditorForAllChildren();
						}
					}
				}
			}
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000C6D40 File Offset: 0x000C4F40
		private void GetChildrenInitialFrames()
		{
			List<GameEntity> list = new List<GameEntity>();
			this.SpawnedGhostEntity.GetChildrenRecursive(ref list);
			foreach (GameEntity gameEntity in list)
			{
				if (!SpawnerEntityEditorHelper.HasField(this.spawner_, gameEntity.Name, false))
				{
					this.stableChildrenFrames.Add(new KeyValuePair<string, MatrixFrame>(gameEntity.Name, gameEntity.GetFrame()));
				}
			}
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000C6DCC File Offset: 0x000C4FCC
		private List<string> GetGhostName()
		{
			string text = this.GetPrefabName();
			List<string> list = new List<string>();
			list.Add(text + "_ghost");
			text = text.Remove(text.Length - text.Split(new char[] { '_' }).Last<string>().Length - 1);
			list.Add(text + "_ghost");
			return list;
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000C6E34 File Offset: 0x000C5034
		public string GetPrefabName()
		{
			return this.spawner_.GameEntity.Name.Remove(this.spawner_.GameEntity.Name.Length - this.spawner_.GameEntity.Name.Split(new char[] { '_' }).Last<string>().Length - 1);
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000C6EA4 File Offset: 0x000C50A4
		public void SetupGhostMovement(string pathName)
		{
			this._ghostMovementMode = true;
			this._pathName = pathName;
			Path pathWithName = this.SpawnedGhostEntity.Scene.GetPathWithName(pathName);
			Vec3 scaleVector = this.SpawnedGhostEntity.GetFrame().rotation.GetScaleVector();
			this._tracker = new PathTracker(pathWithName, scaleVector);
			this._ghostObjectPosition = ((pathWithName != null) ? pathWithName.GetTotalLength() : 0f);
			this.SpawnedGhostEntity.UpdateTriadFrameForEditor();
			List<GameEntity> list = new List<GameEntity>();
			this.SpawnedGhostEntity.GetChildrenRecursive(ref list);
			this._wheels.Clear();
			this._wheels.AddRange(list.Where<GameEntity>((GameEntity x) => x.HasTag("wheel")));
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000C6F6C File Offset: 0x000C516C
		public void SetEnableAutoGhostMovement(bool enableAutoGhostMovement)
		{
			this._enableAutoGhostMovement = enableAutoGhostMovement;
			if (!this._enableAutoGhostMovement && this._tracker.IsValid)
			{
				this._ghostObjectPosition = this._tracker.GetPathLength();
			}
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000C6F9C File Offset: 0x000C519C
		private void UpdateGhostMovement(float dt)
		{
			if (this._tracker.HasChanged)
			{
				this.SetupGhostMovement(this._pathName);
				this._tracker.Advance(this._tracker.GetPathLength());
			}
			if (this.spawner_.GameEntity.IsSelectedOnEditor() || this.SpawnedGhostEntity.IsSelectedOnEditor())
			{
				if (this._tracker.IsValid)
				{
					float num = 10f;
					if (Input.DebugInput.IsShiftDown())
					{
						num = 1f;
					}
					if (Input.DebugInput.IsKeyDown(InputKey.MouseScrollUp))
					{
						this._ghostObjectPosition += dt * num;
					}
					else if (Input.DebugInput.IsKeyDown(InputKey.MouseScrollDown))
					{
						this._ghostObjectPosition -= dt * num;
					}
					if (this._enableAutoGhostMovement)
					{
						this._ghostObjectPosition += dt * num;
						if (this._ghostObjectPosition >= this._tracker.GetPathLength())
						{
							this._ghostObjectPosition = 0f;
						}
					}
					this._ghostObjectPosition = MBMath.ClampFloat(this._ghostObjectPosition, 0f, this._tracker.GetPathLength());
				}
				else
				{
					this._ghostObjectPosition = 0f;
				}
			}
			if (this._tracker.IsValid)
			{
				MatrixFrame globalFrame = this.spawner_.GameEntity.GetGlobalFrame();
				this._tracker.Advance(0f);
				MatrixFrame matrixFrame;
				Vec3 vec;
				this._tracker.CurrentFrameAndColor(out matrixFrame, out vec);
				if ((in globalFrame) != (in matrixFrame))
				{
					this.spawner_.GameEntity.SetGlobalFrame(in matrixFrame, true);
					this.spawner_.GameEntity.UpdateTriadFrameForEditor();
				}
				this._tracker.Advance(this._ghostObjectPosition);
				this._tracker.CurrentFrameAndColor(out matrixFrame, out vec);
				if (this._wheels.Count == 2)
				{
					matrixFrame = this.LinearInterpolatedIK(ref this._tracker);
				}
				if ((in globalFrame) != (in matrixFrame))
				{
					this.SpawnedGhostEntity.SetGlobalFrame(in matrixFrame, true);
					this.SpawnedGhostEntity.UpdateTriadFrameForEditor();
				}
				this._tracker.Reset();
				return;
			}
			MatrixFrame matrixFrame2 = this.SpawnedGhostEntity.GetGlobalFrame();
			MatrixFrame globalFrame2 = this.spawner_.GameEntity.GetGlobalFrame();
			if ((in matrixFrame2) != (in globalFrame2))
			{
				GameEntity spawnedGhostEntity = this.SpawnedGhostEntity;
				matrixFrame2 = this.spawner_.GameEntity.GetGlobalFrame();
				spawnedGhostEntity.SetGlobalFrame(in matrixFrame2, true);
				this.SpawnedGhostEntity.UpdateTriadFrameForEditor();
			}
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000C720C File Offset: 0x000C540C
		private MatrixFrame LinearInterpolatedIK(ref PathTracker pathTracker)
		{
			MatrixFrame matrixFrame;
			Vec3 vec;
			pathTracker.CurrentFrameAndColor(out matrixFrame, out vec);
			MatrixFrame matrixFrame2 = SiegeWeaponMovementComponent.FindGroundFrameForWheelsStatic(ref matrixFrame, 2.45f, 1.3f, this.SpawnedGhostEntity.WeakEntity, this._wheels, this.SpawnedGhostEntity.Scene);
			return MatrixFrame.Lerp(in matrixFrame, in matrixFrame2, vec.x);
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000C7261 File Offset: 0x000C5461
		private static object GetFieldValue(object src, string propName)
		{
			return src.GetType().GetField(propName).GetValue(src);
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000C7275 File Offset: 0x000C5475
		private static bool HasField(object obj, string propertyName, bool findRestricted)
		{
			return obj.GetType().GetField(propertyName) != null && (findRestricted || obj.GetType().GetField(propertyName).GetCustomAttribute<RestrictedAccess>() == null);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000C72A8 File Offset: 0x000C54A8
		private static bool SetSpawnerMatrixFrame(object target, string propertyName, MatrixFrame value)
		{
			value.Fill();
			FieldInfo field = target.GetType().GetField(propertyName);
			if (field != null)
			{
				field.SetValue(target, value);
				return true;
			}
			return false;
		}

		// Token: 0x04001482 RID: 5250
		private List<Tuple<string, SpawnerEntityEditorHelper.Permission, Action<float>>> _stableChildrenPermissions = new List<Tuple<string, SpawnerEntityEditorHelper.Permission, Action<float>>>();

		// Token: 0x04001483 RID: 5251
		private ScriptComponentBehavior spawner_;

		// Token: 0x04001484 RID: 5252
		private List<KeyValuePair<string, MatrixFrame>> stableChildrenFrames = new List<KeyValuePair<string, MatrixFrame>>();

		// Token: 0x04001487 RID: 5255
		public bool LockGhostParent = true;

		// Token: 0x04001488 RID: 5256
		private bool _ghostMovementMode;

		// Token: 0x04001489 RID: 5257
		private PathTracker _tracker;

		// Token: 0x0400148A RID: 5258
		private float _ghostObjectPosition;

		// Token: 0x0400148B RID: 5259
		private string _pathName;

		// Token: 0x0400148C RID: 5260
		private bool _enableAutoGhostMovement;

		// Token: 0x0400148D RID: 5261
		private readonly List<GameEntity> _wheels = new List<GameEntity>();

		// Token: 0x02000631 RID: 1585
		public enum Axis
		{
			// Token: 0x040020DC RID: 8412
			x,
			// Token: 0x040020DD RID: 8413
			y,
			// Token: 0x040020DE RID: 8414
			z
		}

		// Token: 0x02000632 RID: 1586
		public enum PermissionType
		{
			// Token: 0x040020E0 RID: 8416
			scale,
			// Token: 0x040020E1 RID: 8417
			rotation
		}

		// Token: 0x02000633 RID: 1587
		public struct Permission
		{
			// Token: 0x0600400B RID: 16395 RVA: 0x000F8157 File Offset: 0x000F6357
			public Permission(SpawnerEntityEditorHelper.PermissionType permission, SpawnerEntityEditorHelper.Axis axis)
			{
				this.TypeOfPermission = permission;
				this.PermittedAxis = axis;
			}

			// Token: 0x040020E2 RID: 8418
			public SpawnerEntityEditorHelper.PermissionType TypeOfPermission;

			// Token: 0x040020E3 RID: 8419
			public SpawnerEntityEditorHelper.Axis PermittedAxis;
		}
	}
}

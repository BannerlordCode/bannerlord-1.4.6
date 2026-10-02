using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200004E RID: 78
	public static class GameEntityPhysicsExtensions
	{
		// Token: 0x060007EA RID: 2026 RVA: 0x00005D32 File Offset: 0x00003F32
		public static bool HasBody(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasBody(gameEntity.Pointer);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00005D44 File Offset: 0x00003F44
		public static bool HasBody(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasBody(gameEntity.Pointer);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00005D57 File Offset: 0x00003F57
		public static void AddSphereAsBody(this GameEntity gameEntity, Vec3 sphere, float radius, BodyFlags bodyFlags)
		{
			EngineApplicationInterface.IGameEntity.AddSphereAsBody(gameEntity.Pointer, sphere, radius, (uint)bodyFlags);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00005D6C File Offset: 0x00003F6C
		public static void AddCapsuleAsBody(this GameEntity gameEntity, Vec3 p1, Vec3 p2, float radius, BodyFlags bodyFlags, string physicsMaterialName = "")
		{
			EngineApplicationInterface.IGameEntity.AddCapsuleAsBody(gameEntity.Pointer, p1, p2, radius, (uint)bodyFlags, physicsMaterialName);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00005D85 File Offset: 0x00003F85
		public static void UpdateBodyRestOffset(this WeakGameEntity gameEntity, float restOffset)
		{
			EngineApplicationInterface.IGameEntity.UpdateBodyRestOffset(gameEntity.Pointer, restOffset);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00005D99 File Offset: 0x00003F99
		public static void PushCapsuleShapeToEntityBody(this WeakGameEntity gameEntity, Vec3 p1, Vec3 p2, float radius, string physicsMaterialName)
		{
			EngineApplicationInterface.IGameEntity.PushCapsuleShapeToEntityBody(gameEntity.Pointer, p1, p2, radius, physicsMaterialName);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00005DB1 File Offset: 0x00003FB1
		public static void AddSphereAsBody(this WeakGameEntity gameEntity, Vec3 sphere, float radius, BodyFlags bodyFlags)
		{
			EngineApplicationInterface.IGameEntity.AddSphereAsBody(gameEntity.Pointer, sphere, radius, (uint)bodyFlags);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00005DC7 File Offset: 0x00003FC7
		public static void AddCapsuleAsBody(this WeakGameEntity gameEntity, Vec3 p1, Vec3 p2, float radius, BodyFlags bodyFlags, string physicsMaterialName = "")
		{
			EngineApplicationInterface.IGameEntity.AddCapsuleAsBody(gameEntity.Pointer, p1, p2, radius, (uint)bodyFlags, physicsMaterialName);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00005DE1 File Offset: 0x00003FE1
		public static void PopCapsuleShapeFromEntityBody(this WeakGameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.PopCapsuleShapeFromEntityBody(gameEntity.Pointer);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00005DF4 File Offset: 0x00003FF4
		public static void RemovePhysics(this GameEntity gameEntity, bool clearingTheScene = false)
		{
			EngineApplicationInterface.IGameEntity.RemovePhysics(gameEntity.Pointer, clearingTheScene);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00005E07 File Offset: 0x00004007
		public static void RemovePhysics(this WeakGameEntity gameEntity, bool clearingTheScene = false)
		{
			EngineApplicationInterface.IGameEntity.RemovePhysics(gameEntity.Pointer, clearingTheScene);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00005E1B File Offset: 0x0000401B
		public static bool GetPhysicsState(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetPhysicsState(gameEntity.Pointer);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00005E2D File Offset: 0x0000402D
		public static bool GetPhysicsState(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetPhysicsState(gameEntity.Pointer);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00005E40 File Offset: 0x00004040
		public static int GetPhysicsTriangleCount(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetPhysicsTriangleCount(gameEntity.Pointer);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00005E53 File Offset: 0x00004053
		public static int GetPhysicsTriangleCount(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetPhysicsTriangleCount(gameEntity.Pointer);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00005E65 File Offset: 0x00004065
		public static bool HasPhysicsDefinitionWithoutFlags(this GameEntity gameEntity, int excludeFlags)
		{
			return EngineApplicationInterface.IGameEntity.HasPhysicsDefinition(gameEntity.Pointer, excludeFlags);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00005E78 File Offset: 0x00004078
		public static bool HasPhysicsDefinitionWithoutFlags(this WeakGameEntity gameEntity, int excludeFlags)
		{
			return EngineApplicationInterface.IGameEntity.HasPhysicsDefinition(gameEntity.Pointer, excludeFlags);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00005E8C File Offset: 0x0000408C
		public static bool HasPhysicsBody(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasPhysicsBody(gameEntity.Pointer);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00005E9E File Offset: 0x0000409E
		public static bool HasPhysicsBody(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasPhysicsBody(gameEntity.Pointer);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00005EB1 File Offset: 0x000040B1
		public static bool HasDynamicRigidBody(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasDynamicRigidBody(gameEntity.Pointer);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00005EC3 File Offset: 0x000040C3
		public static bool HasDynamicRigidBody(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasDynamicRigidBody(gameEntity.Pointer);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00005ED6 File Offset: 0x000040D6
		public static bool HasKinematicRigidBody(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasKinematicRigidBody(gameEntity.Pointer);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00005EE8 File Offset: 0x000040E8
		public static bool HasKinematicRigidBody(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasKinematicRigidBody(gameEntity.Pointer);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00005EFB File Offset: 0x000040FB
		public static bool HasStaticPhysicsBody(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasStaticPhysicsBody(gameEntity.Pointer);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00005F0D File Offset: 0x0000410D
		public static bool HasStaticPhysicsBody(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasStaticPhysicsBody(gameEntity.Pointer);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00005F20 File Offset: 0x00004120
		public static bool HasDynamicRigidBodyAndActiveSimulation(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasDynamicRigidBodyAndActiveSimulation(gameEntity.Pointer);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00005F32 File Offset: 0x00004132
		public static bool HasDynamicRigidBodyAndActiveSimulation(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasDynamicRigidBodyAndActiveSimulation(gameEntity.Pointer);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00005F45 File Offset: 0x00004145
		public static void CreateVariableRatePhysics(this GameEntity gameEntity, bool forChildren)
		{
			EngineApplicationInterface.IGameEntity.CreateVariableRatePhysics(gameEntity.Pointer, forChildren);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00005F58 File Offset: 0x00004158
		public static void CreateVariableRatePhysics(this WeakGameEntity gameEntity, bool forChildren)
		{
			EngineApplicationInterface.IGameEntity.CreateVariableRatePhysics(gameEntity.Pointer, forChildren);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00005F6C File Offset: 0x0000416C
		public static void SetPhysicsState(this GameEntity gameEntity, bool isEnabled, bool setChildren)
		{
			EngineApplicationInterface.IGameEntity.SetPhysicsState(gameEntity.Pointer, isEnabled, setChildren);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00005F80 File Offset: 0x00004180
		public static void SetPhysicsState(this WeakGameEntity gameEntity, bool isEnabled, bool setChildren)
		{
			EngineApplicationInterface.IGameEntity.SetPhysicsState(gameEntity.Pointer, isEnabled, setChildren);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00005F95 File Offset: 0x00004195
		public static void SetPhysicsStateOnlyVariable(this GameEntity gameEntity, bool isEnabled, bool setChildren)
		{
			EngineApplicationInterface.IGameEntity.SetPhysicsStateOnlyVariable(gameEntity.Pointer, isEnabled, setChildren);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00005FA9 File Offset: 0x000041A9
		public static void SetPhysicsStateOnlyVariable(this WeakGameEntity gameEntity, bool isEnabled, bool setChildren)
		{
			EngineApplicationInterface.IGameEntity.SetPhysicsStateOnlyVariable(gameEntity.Pointer, isEnabled, setChildren);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00005FBE File Offset: 0x000041BE
		public static void RemoveEnginePhysics(this GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.RemoveEnginePhysics(gameEntity.Pointer);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00005FD0 File Offset: 0x000041D0
		public static void RemoveEnginePhysics(this WeakGameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.RemoveEnginePhysics(gameEntity.Pointer);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00005FE3 File Offset: 0x000041E3
		public static bool IsEngineBodySleeping(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.IsEngineBodySleeping(gameEntity.Pointer);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00005FF5 File Offset: 0x000041F5
		public static bool IsEngineBodySleeping(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.IsEngineBodySleeping(gameEntity.Pointer);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00006008 File Offset: 0x00004208
		public static bool IsDynamicBodyStationary(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.IsDynamicBodyStationary(gameEntity.Pointer);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0000601A File Offset: 0x0000421A
		public static bool IsDynamicBodyStationary(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.IsDynamicBodyStationary(gameEntity.Pointer);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0000602D File Offset: 0x0000422D
		public static bool IsDynamicBodyStationaryMT(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.IsDynamicBodyStationary(gameEntity.Pointer);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0000603F File Offset: 0x0000423F
		public static bool IsDynamicBodyStationaryMT(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.IsDynamicBodyStationary(gameEntity.Pointer);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00006052 File Offset: 0x00004252
		public static void ReplacePhysicsBodyWithQuadPhysicsBody(this GameEntity gameEntity, UIntPtr vertices, int numberOfVertices, PhysicsMaterial physicsMaterial, BodyFlags bodyFlags, UIntPtr indices, int numberOfIndices)
		{
			EngineApplicationInterface.IGameEntity.ReplacePhysicsBodyWithQuadPhysicsBody(gameEntity.Pointer, vertices, physicsMaterial.Index, bodyFlags, numberOfVertices, indices, numberOfIndices);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00006072 File Offset: 0x00004272
		public static void ReplacePhysicsBodyWithQuadPhysicsBody(this WeakGameEntity gameEntity, UIntPtr vertices, int numberOfVertices, PhysicsMaterial physicsMaterial, BodyFlags bodyFlags, UIntPtr indices, int numberOfIndices)
		{
			EngineApplicationInterface.IGameEntity.ReplacePhysicsBodyWithQuadPhysicsBody(gameEntity.Pointer, vertices, physicsMaterial.Index, bodyFlags, numberOfVertices, indices, numberOfIndices);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00006093 File Offset: 0x00004293
		public static PhysicsShape GetBodyShape(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetBodyShape(gameEntity.Pointer);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000060A5 File Offset: 0x000042A5
		public static PhysicsShape GetBodyShape(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetBodyShape(gameEntity.Pointer);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000060B8 File Offset: 0x000042B8
		public static void SetBodyShape(this GameEntity gameEntity, PhysicsShape shape)
		{
			EngineApplicationInterface.IGameEntity.SetBodyShape(gameEntity.Pointer, (shape == null) ? ((UIntPtr)0UL) : shape.Pointer);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000060E2 File Offset: 0x000042E2
		public static void SetBodyShape(this WeakGameEntity gameEntity, PhysicsShape shape)
		{
			EngineApplicationInterface.IGameEntity.SetBodyShape(gameEntity.Pointer, (shape == null) ? ((UIntPtr)0UL) : shape.Pointer);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00006110 File Offset: 0x00004310
		public static void AddPhysics(this GameEntity gameEntity, float mass, Vec3 localCenterOfMass, PhysicsShape body, Vec3 initialGlobalVelocity, Vec3 angularGlobalVelocity, PhysicsMaterial physicsMaterial, bool isStatic, int collisionGroupID)
		{
			EngineApplicationInterface.IGameEntity.AddPhysics(gameEntity.Pointer, (body != null) ? body.Pointer : UIntPtr.Zero, mass, ref localCenterOfMass, ref initialGlobalVelocity, ref angularGlobalVelocity, physicsMaterial.Index, isStatic, collisionGroupID);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00006158 File Offset: 0x00004358
		public static void AddPhysics(this WeakGameEntity gameEntity, float mass, Vec3 localCenterOfMass, PhysicsShape body, Vec3 initialVelocity, Vec3 angularVelocity, PhysicsMaterial physicsMaterial, bool isStatic, int collisionGroupID)
		{
			EngineApplicationInterface.IGameEntity.AddPhysics(gameEntity.Pointer, (body != null) ? body.Pointer : UIntPtr.Zero, mass, ref localCenterOfMass, ref initialVelocity, ref angularVelocity, physicsMaterial.Index, isStatic, collisionGroupID);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0000619E File Offset: 0x0000439E
		public static void SetVelocityLimits(this GameEntity gameEntity, float maxLinearVelocity, float maxAngularVelocity)
		{
			EngineApplicationInterface.IGameEntity.SetVelocityLimits(gameEntity.Pointer, maxLinearVelocity, maxAngularVelocity);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000061B2 File Offset: 0x000043B2
		public static void SetVelocityLimits(this WeakGameEntity gameEntity, float maxLinearVelocity, float maxAngularVelocity)
		{
			EngineApplicationInterface.IGameEntity.SetVelocityLimits(gameEntity.Pointer, maxLinearVelocity, maxAngularVelocity);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000061C7 File Offset: 0x000043C7
		public static void SetMaxDepenetrationVelocity(this GameEntity gameEntity, float maxDepenetrationVelocity)
		{
			EngineApplicationInterface.IGameEntity.SetMaxDepenetrationVelocity(gameEntity.Pointer, maxDepenetrationVelocity);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000061DA File Offset: 0x000043DA
		public static void SetMaxDepenetrationVelocity(this WeakGameEntity gameEntity, float maxDepenetrationVelocity)
		{
			EngineApplicationInterface.IGameEntity.SetMaxDepenetrationVelocity(gameEntity.Pointer, maxDepenetrationVelocity);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000061EE File Offset: 0x000043EE
		public static void SetSolverIterationCounts(this GameEntity gameEntity, int positionIterationCount, int velocityIterationCount)
		{
			EngineApplicationInterface.IGameEntity.SetSolverIterationCounts(gameEntity.Pointer, positionIterationCount, velocityIterationCount);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00006202 File Offset: 0x00004402
		public static void SetSolverIterationCounts(this WeakGameEntity gameEntity, int positionIterationCount, int velocityIterationCount)
		{
			EngineApplicationInterface.IGameEntity.SetSolverIterationCounts(gameEntity.Pointer, positionIterationCount, velocityIterationCount);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00006217 File Offset: 0x00004417
		public static void ApplyLocalImpulseToDynamicBody(this GameEntity gameEntity, Vec3 localPosition, Vec3 impulse)
		{
			EngineApplicationInterface.IGameEntity.ApplyLocalImpulseToDynamicBody(gameEntity.Pointer, ref localPosition, ref impulse);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0000622D File Offset: 0x0000442D
		public static void ApplyLocalImpulseToDynamicBody(this WeakGameEntity gameEntity, Vec3 localPosition, Vec3 impulse)
		{
			EngineApplicationInterface.IGameEntity.ApplyLocalImpulseToDynamicBody(gameEntity.Pointer, ref localPosition, ref impulse);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00006244 File Offset: 0x00004444
		public static void ApplyForceToDynamicBody(this GameEntity gameEntity, Vec3 force, GameEntityPhysicsExtensions.ForceMode forceMode)
		{
			EngineApplicationInterface.IGameEntity.ApplyForceToDynamicBody(gameEntity.Pointer, ref force, forceMode);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00006259 File Offset: 0x00004459
		public static void ApplyForceToDynamicBody(this WeakGameEntity gameEntity, Vec3 force, GameEntityPhysicsExtensions.ForceMode forceMode)
		{
			EngineApplicationInterface.IGameEntity.ApplyForceToDynamicBody(gameEntity.Pointer, ref force, forceMode);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0000626F File Offset: 0x0000446F
		public static void ApplyGlobalForceAtLocalPosToDynamicBody(this GameEntity gameEntity, Vec3 localPosition, Vec3 globalForce, GameEntityPhysicsExtensions.ForceMode forceMode)
		{
			EngineApplicationInterface.IGameEntity.ApplyGlobalForceAtLocalPosToDynamicBody(gameEntity.Pointer, ref localPosition, ref globalForce, forceMode);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00006286 File Offset: 0x00004486
		public static void ApplyGlobalForceAtLocalPosToDynamicBody(this WeakGameEntity gameEntity, Vec3 localPosition, Vec3 globalForce, GameEntityPhysicsExtensions.ForceMode forceMode)
		{
			EngineApplicationInterface.IGameEntity.ApplyGlobalForceAtLocalPosToDynamicBody(gameEntity.Pointer, ref localPosition, ref globalForce, forceMode);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0000629E File Offset: 0x0000449E
		public static void ApplyTorqueToDynamicBody(this GameEntity gameEntity, Vec3 torque, GameEntityPhysicsExtensions.ForceMode forceMode)
		{
			EngineApplicationInterface.IGameEntity.ApplyTorqueToDynamicBody(gameEntity.Pointer, ref torque, forceMode);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000062B3 File Offset: 0x000044B3
		public static void ApplyTorqueToDynamicBody(this WeakGameEntity gameEntity, Vec3 torque, GameEntityPhysicsExtensions.ForceMode forceMode)
		{
			EngineApplicationInterface.IGameEntity.ApplyTorqueToDynamicBody(gameEntity.Pointer, ref torque, forceMode);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000062C9 File Offset: 0x000044C9
		public static void ApplyLocalForceAtLocalPosToDynamicBody(this GameEntity gameEntity, Vec3 localPosition, Vec3 localForce, GameEntityPhysicsExtensions.ForceMode forceMode)
		{
			EngineApplicationInterface.IGameEntity.ApplyLocalForceAtLocalPosToDynamicBody(gameEntity.Pointer, ref localPosition, ref localForce, forceMode);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000062E0 File Offset: 0x000044E0
		public static void ApplyLocalForceAtLocalPosToDynamicBody(this WeakGameEntity gameEntity, Vec3 localPosition, Vec3 localForce, GameEntityPhysicsExtensions.ForceMode forceMode)
		{
			EngineApplicationInterface.IGameEntity.ApplyLocalForceAtLocalPosToDynamicBody(gameEntity.Pointer, ref localPosition, ref localForce, forceMode);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000062F8 File Offset: 0x000044F8
		public static void ApplyAccelerationToDynamicBody(this GameEntity gameEntity, Vec3 acceleration)
		{
			EngineApplicationInterface.IGameEntity.ApplyAccelerationToDynamicBody(gameEntity.Pointer, ref acceleration);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0000630C File Offset: 0x0000450C
		public static void ApplyAccelerationToDynamicBody(this WeakGameEntity gameEntity, Vec3 acceleration)
		{
			EngineApplicationInterface.IGameEntity.ApplyAccelerationToDynamicBody(gameEntity.Pointer, ref acceleration);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00006321 File Offset: 0x00004521
		public static void DisableDynamicBodySimulation(this GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.DisableDynamicBodySimulation(gameEntity.Pointer);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00006333 File Offset: 0x00004533
		public static void DisableDynamicBodySimulation(this WeakGameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.DisableDynamicBodySimulation(gameEntity.Pointer);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00006346 File Offset: 0x00004546
		public static void DisableDynamicBodySimulationMT(this GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.DisableDynamicBodySimulation(gameEntity.Pointer);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00006358 File Offset: 0x00004558
		public static void DisableDynamicBodySimulationMT(this WeakGameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.DisableDynamicBodySimulation(gameEntity.Pointer);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0000636B File Offset: 0x0000456B
		public static void ConvertDynamicBodyToRayCast(this GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.ConvertDynamicBodyToRayCast(gameEntity.Pointer);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0000637D File Offset: 0x0000457D
		public static void ConvertDynamicBodyToRayCast(this WeakGameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.ConvertDynamicBodyToRayCast(gameEntity.Pointer);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00006390 File Offset: 0x00004590
		public static void SetPhysicsMoveToBatched(this GameEntity gameEntity, bool value)
		{
			EngineApplicationInterface.IGameEntity.SetPhysicsMoveToBatched(gameEntity.Pointer, value);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000063A3 File Offset: 0x000045A3
		public static void SetPhysicsMoveToBatched(this WeakGameEntity gameEntity, bool value)
		{
			EngineApplicationInterface.IGameEntity.SetPhysicsMoveToBatched(gameEntity.Pointer, value);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000063B7 File Offset: 0x000045B7
		public static void EnableDynamicBody(this GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.EnableDynamicBody(gameEntity.Pointer);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000063C9 File Offset: 0x000045C9
		public static void EnableDynamicBody(this WeakGameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.EnableDynamicBody(gameEntity.Pointer);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000063DC File Offset: 0x000045DC
		public static float GetMass(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetMass(gameEntity.Pointer);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000063EE File Offset: 0x000045EE
		public static float GetMass(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetMass(gameEntity.Pointer);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00006401 File Offset: 0x00004601
		public static void SetMassAndUpdateInertiaAndCenterOfMass(this GameEntity gameEntity, float mass)
		{
			EngineApplicationInterface.IGameEntity.SetMassAndUpdateInertiaAndCenterOfMass(gameEntity.Pointer, mass);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00006414 File Offset: 0x00004614
		public static void SetMassAndUpdateInertiaAndCenterOfMass(this WeakGameEntity gameEntity, float mass)
		{
			EngineApplicationInterface.IGameEntity.SetMassAndUpdateInertiaAndCenterOfMass(gameEntity.Pointer, mass);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00006428 File Offset: 0x00004628
		public static void SetCenterOfMass(this GameEntity gameEntity, Vec3 localCenterOfMass)
		{
			EngineApplicationInterface.IGameEntity.SetCenterOfMass(gameEntity.Pointer, ref localCenterOfMass);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0000643C File Offset: 0x0000463C
		public static void SetCenterOfMass(this WeakGameEntity gameEntity, Vec3 centerOfMass)
		{
			EngineApplicationInterface.IGameEntity.SetCenterOfMass(gameEntity.Pointer, ref centerOfMass);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00006451 File Offset: 0x00004651
		public static Vec3 GetMassSpaceInertia(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetMassSpaceInertia(gameEntity.Pointer);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00006463 File Offset: 0x00004663
		public static Vec3 GetMassSpaceInertia(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetMassSpaceInertia(gameEntity.Pointer);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00006476 File Offset: 0x00004676
		public static Vec3 GetMassSpaceInverseInertia(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetMassSpaceInverseInertia(gameEntity.Pointer);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00006488 File Offset: 0x00004688
		public static Vec3 GetMassSpaceInverseInertia(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetMassSpaceInverseInertia(gameEntity.Pointer);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0000649B File Offset: 0x0000469B
		public static void SetMassSpaceInertia(this GameEntity gameEntity, Vec3 inertia)
		{
			EngineApplicationInterface.IGameEntity.SetMassSpaceInertia(gameEntity.Pointer, ref inertia);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000064AF File Offset: 0x000046AF
		public static void SetMassSpaceInertia(this WeakGameEntity gameEntity, Vec3 inertia)
		{
			EngineApplicationInterface.IGameEntity.SetMassSpaceInertia(gameEntity.Pointer, ref inertia);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000064C4 File Offset: 0x000046C4
		public static void SetDamping(this GameEntity gameEntity, float linearDamping, float angularDamping)
		{
			EngineApplicationInterface.IGameEntity.SetDamping(gameEntity.Pointer, linearDamping, angularDamping);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000064D8 File Offset: 0x000046D8
		public static void SetDamping(this WeakGameEntity gameEntity, float linearDamping, float angularDamping)
		{
			EngineApplicationInterface.IGameEntity.SetDamping(gameEntity.Pointer, linearDamping, angularDamping);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000064ED File Offset: 0x000046ED
		public static void SetDampingMT(this GameEntity gameEntity, float linearDamping, float angularDamping)
		{
			EngineApplicationInterface.IGameEntity.SetDamping(gameEntity.Pointer, linearDamping, angularDamping);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00006501 File Offset: 0x00004701
		public static void SetDampingMT(this WeakGameEntity gameEntity, float linearDamping, float angularDamping)
		{
			EngineApplicationInterface.IGameEntity.SetDamping(gameEntity.Pointer, linearDamping, angularDamping);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00006516 File Offset: 0x00004716
		public static void DisableGravity(this GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.DisableGravity(gameEntity.Pointer);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00006528 File Offset: 0x00004728
		public static void DisableGravity(this WeakGameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.DisableGravity(gameEntity.Pointer);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0000653B File Offset: 0x0000473B
		public static bool IsGravityDisabled(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.IsGravityDisabled(gameEntity.Pointer);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0000654D File Offset: 0x0000474D
		public static bool IsGravityDisabled(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.IsGravityDisabled(gameEntity.Pointer);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00006560 File Offset: 0x00004760
		public static Vec3 GetLinearVelocity(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetLinearVelocity(gameEntity.Pointer);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00006572 File Offset: 0x00004772
		public static Vec3 GetLinearVelocity(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetLinearVelocity(gameEntity.Pointer);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00006585 File Offset: 0x00004785
		public static void SetLinearVelocity(this GameEntity gameEntity, Vec3 newLinearVelocity)
		{
			EngineApplicationInterface.IGameEntity.SetLinearVelocity(gameEntity.Pointer, newLinearVelocity);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00006598 File Offset: 0x00004798
		public static void SetLinearVelocity(this WeakGameEntity gameEntity, Vec3 newLinearVelocity)
		{
			EngineApplicationInterface.IGameEntity.SetLinearVelocity(gameEntity.Pointer, newLinearVelocity);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000065AC File Offset: 0x000047AC
		public static Vec3 GetLinearVelocityMT(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetLinearVelocity(gameEntity.Pointer);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000065BE File Offset: 0x000047BE
		public static Vec3 GetLinearVelocityMT(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetLinearVelocity(gameEntity.Pointer);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000065D1 File Offset: 0x000047D1
		public static Vec3 GetAngularVelocity(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetAngularVelocity(gameEntity.Pointer);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000065E3 File Offset: 0x000047E3
		public static Vec3 GetAngularVelocity(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetAngularVelocity(gameEntity.Pointer);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000065F6 File Offset: 0x000047F6
		public static Vec3 GetAngularVelocityMT(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetAngularVelocity(gameEntity.Pointer);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00006608 File Offset: 0x00004808
		public static Vec3 GetAngularVelocityMT(this WeakGameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetAngularVelocity(gameEntity.Pointer);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0000661B File Offset: 0x0000481B
		public static void SetAngularVelocity(this GameEntity gameEntity, Vec3 newAngularVelocity)
		{
			EngineApplicationInterface.IGameEntity.SetAngularVelocity(gameEntity.Pointer, in newAngularVelocity);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0000662F File Offset: 0x0000482F
		public static void SetAngularVelocity(this WeakGameEntity gameEntity, Vec3 newAngularVelocity)
		{
			EngineApplicationInterface.IGameEntity.SetAngularVelocity(gameEntity.Pointer, in newAngularVelocity);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00006644 File Offset: 0x00004844
		public static void GetPhysicsMinMax(this GameEntity gameEntity, bool includeChildren, out Vec3 bbmin, out Vec3 bbmax, bool returnLocal)
		{
			bbmin = Vec3.Zero;
			bbmax = Vec3.Zero;
			EngineApplicationInterface.IGameEntity.GetPhysicsMinMax(gameEntity.Pointer, includeChildren, ref bbmin, ref bbmax, returnLocal);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00006671 File Offset: 0x00004871
		public static void GetPhysicsMinMax(this WeakGameEntity gameEntity, bool includeChildren, out Vec3 bbmin, out Vec3 bbmax, bool returnLocal)
		{
			bbmin = Vec3.Zero;
			bbmax = Vec3.Zero;
			EngineApplicationInterface.IGameEntity.GetPhysicsMinMax(gameEntity.Pointer, includeChildren, ref bbmin, ref bbmax, returnLocal);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000066A0 File Offset: 0x000048A0
		public static BoundingBox GetLocalPhysicsBoundingBox(this GameEntity gameEntity, bool includeChildren)
		{
			BoundingBox boundingBox;
			EngineApplicationInterface.IGameEntity.GetLocalPhysicsBoundingBox(gameEntity.Pointer, includeChildren, out boundingBox);
			return boundingBox;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000066C4 File Offset: 0x000048C4
		public static BoundingBox GetLocalPhysicsBoundingBox(this WeakGameEntity gameEntity, bool includeChildren)
		{
			BoundingBox boundingBox;
			EngineApplicationInterface.IGameEntity.GetLocalPhysicsBoundingBox(gameEntity.Pointer, includeChildren, out boundingBox);
			return boundingBox;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000066E8 File Offset: 0x000048E8
		public static Vec3 GetLinearVelocityAtGlobalPointForEntityWithDynamicBody(this WeakGameEntity entity, Vec3 globalPoint)
		{
			MatrixFrame bodyWorldTransform = entity.GetBodyWorldTransform();
			Vec3 centerOfMass = entity.CenterOfMass;
			Vec3 vec = globalPoint - bodyWorldTransform.TransformToParent(in centerOfMass);
			Vec3 vec2 = Vec3.CrossProduct(entity.GetAngularVelocity(), vec);
			return entity.GetLinearVelocity() + vec2;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00006730 File Offset: 0x00004930
		public static Vec3 GetLinearVelocityAtGlobalPointForEntityWithDynamicBody(this GameEntity entity, Vec3 globalPoint)
		{
			MatrixFrame bodyWorldTransform = entity.GetBodyWorldTransform();
			Vec3 centerOfMass = entity.CenterOfMass;
			Vec3 vec = globalPoint - bodyWorldTransform.TransformToParent(in centerOfMass);
			Vec3 vec2 = Vec3.CrossProduct(entity.GetAngularVelocity(), vec);
			return entity.GetLinearVelocity() + vec2;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00006774 File Offset: 0x00004974
		public static void ComputeVelocityDeltaFromImpulse(this WeakGameEntity gameEntity, in Vec3 impulseGlobal, in Vec3 impulsiveTorqueGlobal, out Vec3 deltaGlobalLinearVelocity, out Vec3 deltaGlobalAngularVelocity)
		{
			EngineApplicationInterface.IGameEntity.ComputeVelocityDeltaFromImpulse(gameEntity.Pointer, in impulseGlobal, in impulsiveTorqueGlobal, out deltaGlobalLinearVelocity, out deltaGlobalAngularVelocity);
		}

		// Token: 0x020000BE RID: 190
		[EngineStruct("rglPhysics_engine_body::Force_mode", false, null)]
		public enum ForceMode : sbyte
		{
			// Token: 0x040003A4 RID: 932
			Force,
			// Token: 0x040003A5 RID: 933
			Impulse,
			// Token: 0x040003A6 RID: 934
			VelocityChange,
			// Token: 0x040003A7 RID: 935
			Acceleration
		}
	}
}

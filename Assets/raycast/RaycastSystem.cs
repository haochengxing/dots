using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;
using Unity.Physics;
using Unity.Physics.Systems;

public class RaycastSystem : ComponentSystem
{

    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //获取物理世界
            BuildPhysicsWorld physicsWorld = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
            NativeArray<RigidBody> rigidBodies = new NativeArray<RigidBody>(1, Allocator.TempJob);
            //获取射线发射位置
            UnityEngine.Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastJonHande raycastJonHande = new RaycastJonHande()
            {
                mStartPos = ray.origin,
                mEndPos = ray.direction,
                physicsWorld = physicsWorld.PhysicsWorld,
                Bodies = rigidBodies,
            };
            JobHandle jobHandle = raycastJonHande.Schedule();
            jobHandle.Complete();
            Debug.Log("射线击中目标" + rigidBodies[0].Entity);
            rigidBodies.Dispose();
        }
    }
    [BurstCompile]
    public struct RaycastJonHande : IJob
    {
        public NativeArray<RigidBody> Bodies;
        public float3 mStartPos;
        public float3 mEndPos;
        public PhysicsWorld physicsWorld;
        public void Execute()
        {
            //创建输入
            RaycastInput raycastInput = new RaycastInput()
            {
                Start = mStartPos,
                End = mEndPos * 100,
                //声明碰撞过滤器，用来过滤某些层级下的物体是否进行射线检测
                Filter = new CollisionFilter() { BelongsTo = ~0u, CollidesWith = ~0u, GroupIndex = 0, }
            };
            Unity.Physics.RaycastHit raycastHit = new Unity.Physics.RaycastHit();
            //发射射线去检测Entity实体
            if (physicsWorld.CollisionWorld.CastRay(raycastInput, out raycastHit))
            {
                //拿到我们射线击中的entity
                Bodies[0] = physicsWorld.Bodies[raycastHit.RigidBodyIndex];
            }
        }
    }
}


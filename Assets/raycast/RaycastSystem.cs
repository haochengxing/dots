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
            //��ȡ��������
            BuildPhysicsWorld physicsWorld = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
            NativeArray<RigidBody> rigidBodies = new NativeArray<RigidBody>(1, Allocator.TempJob);
            //��ȡ���߷���λ��
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
            Debug.Log("���߻���Ŀ��" + rigidBodies[0].Entity);
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
            //��������
            RaycastInput raycastInput = new RaycastInput()
            {
                Start = mStartPos,
                End = mEndPos * 100,
                //������ײ����������������ĳЩ�㼶�µ������Ƿ�������߼��
                Filter = new CollisionFilter() { BelongsTo = ~0u, CollidesWith = ~0u, GroupIndex = 0, }
            };
            Unity.Physics.RaycastHit raycastHit = new Unity.Physics.RaycastHit();
            //��������ȥ���Entityʵ��
            if (physicsWorld.CollisionWorld.CastRay(raycastInput, out raycastHit))
            {
                //�õ��������߻��е�entity
                Bodies[0] = physicsWorld.Bodies[raycastHit.RigidBodyIndex];
            }
        }
    }
}


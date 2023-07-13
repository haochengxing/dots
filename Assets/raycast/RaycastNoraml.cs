
using Unity.Mathematics;
using UnityEngine;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Entities;
using Unity.Jobs;
public class RaycastNoraml : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Entity entity = Raycast(ray.origin, ray.direction * 100);
            Debug.Log(entity);
        }
    }

    public Entity Raycast(float3 startPos, float3 endPos)
    {
        //���Ȼ�ȡ��������
        BuildPhysicsWorld physicsWorld = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
        //Ȼ���ȡ���ǵ���ײ����
        CollisionWorld collisionWorld = physicsWorld.PhysicsWorld.CollisionWorld;

        RaycastInput raycastInput = new RaycastInput()
        {
            Start = startPos,
            End = endPos,
            //������ײ����������������ĳЩ�㼶�µ������Ƿ�������߼��
            //1.Filter = CollisionFilter.Default,
            Filter = new CollisionFilter()
            {
                BelongsTo = ~0u,
                CollidesWith = ~0u,
                GroupIndex = 0,
            }
        };
        Unity.Physics.RaycastHit raycastHit = new Unity.Physics.RaycastHit();
        //��������ȥ���Entityʵ�� 
        if (collisionWorld.CastRay(raycastInput, out raycastHit))
        {
            //�õ��������߻��е�entity
            Entity entity = physicsWorld.PhysicsWorld.Bodies[raycastHit.RigidBodyIndex].Entity;
            return entity;
        }
        else
        {
            return Entity.Null;
        }
    }
}
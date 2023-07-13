
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
        //首先获取物理世界
        BuildPhysicsWorld physicsWorld = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
        //然后获取咱们的碰撞世界
        CollisionWorld collisionWorld = physicsWorld.PhysicsWorld.CollisionWorld;

        RaycastInput raycastInput = new RaycastInput()
        {
            Start = startPos,
            End = endPos,
            //声明碰撞过滤器，用来过滤某些层级下的物体是否进行射线检测
            //1.Filter = CollisionFilter.Default,
            Filter = new CollisionFilter()
            {
                BelongsTo = ~0u,
                CollidesWith = ~0u,
                GroupIndex = 0,
            }
        };
        Unity.Physics.RaycastHit raycastHit = new Unity.Physics.RaycastHit();
        //发射射线去检测Entity实体 
        if (collisionWorld.CastRay(raycastInput, out raycastHit))
        {
            //拿到我们射线击中的entity
            Entity entity = physicsWorld.PhysicsWorld.Bodies[raycastHit.RigidBodyIndex].Entity;
            return entity;
        }
        else
        {
            return Entity.Null;
        }
    }
}
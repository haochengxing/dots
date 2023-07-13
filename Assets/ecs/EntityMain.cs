
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Transforms;
using Unity.Rendering;
public class EntityMain : MonoBehaviour
{
    EntityManager mEentityManager;
    [SerializeField]
    private Mesh[] mFishMeshArray;//模型Mesh数组
    [SerializeField]
    private Material[] mFishMaterialArray;//模型材质数组
    void Start()
    {
        mEentityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //创建原型
        EntityArchetype entityArchetype = mEentityManager.CreateArchetype(
            typeof(MoveSpeedComponent),
            typeof(Translation),
            //下面三个实体一定要加不然是看不到实例化的物体的
            typeof(RenderBounds),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            //添加旋转数据
            typeof(Rotation)
            );
        //创建实体数组 5000代表就生成5000个
        NativeArray<Entity> entityArray = new NativeArray<Entity>(5000, Allocator.Temp);
        //创建实体
        mEentityManager.CreateEntity(entityArchetype, entityArray);
        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            //设置数据
            mEentityManager.SetComponentData(entity, new Rotation { Value = quaternion.Euler(0, -130, 0) });
            mEentityManager.SetComponentData(entity, new MoveSpeedComponent { mMoveSpeed = UnityEngine.Random.Range(3, 10) });
            mEentityManager.SetComponentData(entity, new Translation { Value = new float3(UnityEngine.Random.Range(-20, 20), UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(5, 100)) });
            int index = UnityEngine.Random.Range(0, 3);
            //设置材质和渲染网格
            mEentityManager.SetSharedComponentData(entity, new RenderMesh { mesh = mFishMeshArray[index], material = mFishMaterialArray[index], layerMask = 1, });
        }
        //一定要记得释放
        entityArray.Dispose();
    }
}

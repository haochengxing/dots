
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
    private Mesh[] mFishMeshArray;//ģ��Mesh����
    [SerializeField]
    private Material[] mFishMaterialArray;//ģ�Ͳ�������
    void Start()
    {
        mEentityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //����ԭ��
        EntityArchetype entityArchetype = mEentityManager.CreateArchetype(
            typeof(MoveSpeedComponent),
            typeof(Translation),
            //��������ʵ��һ��Ҫ�Ӳ�Ȼ�ǿ�����ʵ�����������
            typeof(RenderBounds),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            //�����ת����
            typeof(Rotation)
            );
        //����ʵ������ 5000���������5000��
        NativeArray<Entity> entityArray = new NativeArray<Entity>(5000, Allocator.Temp);
        //����ʵ��
        mEentityManager.CreateEntity(entityArchetype, entityArray);
        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            //��������
            mEentityManager.SetComponentData(entity, new Rotation { Value = quaternion.Euler(0, -130, 0) });
            mEentityManager.SetComponentData(entity, new MoveSpeedComponent { mMoveSpeed = UnityEngine.Random.Range(3, 10) });
            mEentityManager.SetComponentData(entity, new Translation { Value = new float3(UnityEngine.Random.Range(-20, 20), UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(5, 100)) });
            int index = UnityEngine.Random.Range(0, 3);
            //���ò��ʺ���Ⱦ����
            mEentityManager.SetSharedComponentData(entity, new RenderMesh { mesh = mFishMeshArray[index], material = mFishMaterialArray[index], layerMask = 1, });
        }
        //һ��Ҫ�ǵ��ͷ�
        entityArray.Dispose();
    }
}

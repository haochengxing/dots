using Unity.Jobs;
using Unity.Entities;
using Unity.Physics;
using Unity.Burst;
using Unity.Physics.Systems;

public partial class TriggerSystem : SystemBase
{
    //�ݶ���������
    private StepPhysicsWorld mSetpPhysicsWorld;

    private EndSimulationEntityCommandBufferSystem commandBufferSystem;
    protected override void OnCreate()
    {
        //��ȡ�ݶ���������
        mSetpPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();

        commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {

        TriggerJob triggerJob = new TriggerJob
        {
            mPhysicsVelocityGroup = GetComponentDataFromEntity<PhysicsVelocity>(),
        };

        Dependency = triggerJob.Schedule(mSetpPhysicsWorld.Simulation, Dependency);
        commandBufferSystem.AddJobHandleForProducer(Dependency);
    }

    /// <summary>
    /// ����������
    /// </summary>
    [BurstCompile]
    public struct TriggerJob : ITriggerEventsJob
    {
        //��������
        public ComponentDataFromEntity<PhysicsVelocity> mPhysicsVelocityGroup;
        public void Execute(TriggerEvent triggerEvent)
        {
            // ��������ͨ��HasComponent�ж��ĸ�Entity��������ϵͳ(Physics Body)  
            //ע�⣺�����������������������嶼��Collider ��DOTS������Physics Shape��
            //�ұ�����һ��������и������(����ϵͳ)DOTS������Physics Body��
            if (mPhysicsVelocityGroup.HasComponent(triggerEvent.EntityA))
            {
                PhysicsVelocity velocity = mPhysicsVelocityGroup[triggerEvent.EntityA];
                velocity.Linear.y = 5;
                mPhysicsVelocityGroup[triggerEvent.EntityA] = velocity;
            }
            //����ע�⣺����������嶼��������ϵͳ ��ô�������߼�����ִ�� �������Ǿ�����ѭԭ��
            //Ҳ����ͨ���ĸ������Ϸ�Physics Body�������ĸ�����ȥ�����¼�
            if (mPhysicsVelocityGroup.HasComponent(triggerEvent.EntityB))
            {
                PhysicsVelocity velocity = mPhysicsVelocityGroup[triggerEvent.EntityB];
                velocity.Linear.y = 5;
                mPhysicsVelocityGroup[triggerEvent.EntityB] = velocity;
            }
        }
    }

}

using Unity.Jobs;
using Unity.Entities;
using Unity.Physics;
using Unity.Burst;
using Unity.Physics.Systems;

public partial class TriggerSystem : SystemBase
{
    //梯度物理世界
    private StepPhysicsWorld mSetpPhysicsWorld;

    private EndSimulationEntityCommandBufferSystem commandBufferSystem;
    protected override void OnCreate()
    {
        //获取梯度物理世界
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
    /// 创建触发器
    /// </summary>
    [BurstCompile]
    public struct TriggerJob : ITriggerEventsJob
    {
        //触发器组
        public ComponentDataFromEntity<PhysicsVelocity> mPhysicsVelocityGroup;
        public void Execute(TriggerEvent triggerEvent)
        {
            // 这里我们通过HasComponent判断哪个Entity上有物理系统(Physics Body)  
            //注意：触发器触发条件是两个物体都有Collider 在DOTS中则是Physics Shape，
            //且必须有一个物体带有刚体组件(物理系统)DOTS中则是Physics Body。
            if (mPhysicsVelocityGroup.HasComponent(triggerEvent.EntityA))
            {
                PhysicsVelocity velocity = mPhysicsVelocityGroup[triggerEvent.EntityA];
                velocity.Linear.y = 5;
                mPhysicsVelocityGroup[triggerEvent.EntityA] = velocity;
            }
            //这里注意：如果两个物体都具有物理系统 那么这两个逻辑都会执行 所以我们尽量遵循原则
            //也可以通过哪个物体上放Physics Body来决定哪个物体去触发事件
            if (mPhysicsVelocityGroup.HasComponent(triggerEvent.EntityB))
            {
                PhysicsVelocity velocity = mPhysicsVelocityGroup[triggerEvent.EntityB];
                velocity.Linear.y = 5;
                mPhysicsVelocityGroup[triggerEvent.EntityB] = velocity;
            }
        }
    }

}

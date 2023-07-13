using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public class MoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref MoveSpeedComponent moveCmpt, ref Translation translation, ref Rotation rotation) =>
        {
            translation.Value.x += moveCmpt.mMoveSpeed * Time.DeltaTime;

            if (translation.Value.x > 30)
            {
                //��ת�ƶ��ٶȺ���ת
                rotation.Value = quaternion.Euler(new float3(0, 130, 0));
                moveCmpt.mMoveSpeed = -math.abs(moveCmpt.mMoveSpeed);
            }
            else if (translation.Value.x < -30)
            {
                //��ת�ƶ��ٶȺ���ת
                rotation.Value = quaternion.Euler(new float3(0, -130, 0));
                moveCmpt.mMoveSpeed = math.abs(moveCmpt.mMoveSpeed);
            }
        });
    }
}

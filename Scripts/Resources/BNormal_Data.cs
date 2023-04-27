using Godot;
using MonoCustomResourceRegistry;

[RegisteredType(nameof(BNormal_Data), "", nameof(Resource))]
public partial class BNormal_Data : BaseMoveData
{
    [ExportCategory("Movement Variables")]
    [Export] public float MoveSpeedBase {get; set;}
    [Export] public float MoveSpeedAcceleration {get; set;}
    [Export] public float MoveSpeedFriction {get; set;}

    public BNormal_Data()
    {
        MoveSpeedBase = 1f;
        MoveSpeedAcceleration = 1f;
        MoveSpeedFriction = 1f;
    }
}

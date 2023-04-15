using Godot;
using MonoCustomResourceRegistry;

[RegisteredType(nameof(BaseTestShipData), "", nameof(Resource))]
public partial class BaseTestShipData : Resource
{
    [ExportCategory("Movement Variables")]
    [Export] public float MoveSpeedBase {get; set;}
    [Export] public float MoveSpeedAcceleration {get; set;}
    [Export] public float MoveSpeedFriction {get; set;}

    public BaseTestShipData()
    {
        MoveSpeedBase = 1f;
        MoveSpeedAcceleration = 1f;
        MoveSpeedFriction = 1f;
    }
}

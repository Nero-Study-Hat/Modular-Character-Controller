using Godot;
using MonoCustomResourceRegistry;

[RegisteredType(nameof(TestShip1Data), "", nameof(Resource))]
public partial class TestShip1Data : Resource
{
    [ExportCategory("Movement Variables")]
    [Export] public float MoveSpeedBase {get; set;}
    [Export] public float MoveSpeedAcceleration {get; set;}
    [Export] public float MoveSpeedFriction {get; set;}

    public TestShip1Data()
    {
        MoveSpeedBase = 1f;
        MoveSpeedAcceleration = 1f;
        MoveSpeedFriction = 1f;
    }
}

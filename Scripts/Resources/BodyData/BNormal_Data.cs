using Godot;
using MonoCustomResourceRegistry;

[RegisteredType(nameof(BNormal_Data), "", nameof(BaseBodyData))]
public partial class BNormal_Data : BaseBodyData
{
    [ExportCategory("Movement Variables")]
    [Export] public float MoveSpeedBase {get; set;} = 0;
    [Export] public float MoveSpeedAcceleration {get; set;} = 0;
    [Export] public float MoveSpeedFriction {get; set;} = 0;
}

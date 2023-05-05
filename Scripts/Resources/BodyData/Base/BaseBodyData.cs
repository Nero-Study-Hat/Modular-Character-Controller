using Godot;

public partial class BaseBodyData : Resource
{
    [ExportCategory("Movement Variables")]
    [Export] public float MoveSpeedBase {get; set;} = 0;
    [Export] public float MoveSpeedAcceleration {get; set;} = 0;
    [Export] public float MoveSpeedFriction {get; set;} = 0;
}
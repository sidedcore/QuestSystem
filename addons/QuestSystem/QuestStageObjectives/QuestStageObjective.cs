using Godot;

[GlobalClass]
public partial class QuestStageObjective : Resource
{
    [Export]
    public string TargetName;
    [Export]
    public int ObjectiveTargetQuantity = 1;
    [Export]
    public bool ObjectiveComplete = false;
}
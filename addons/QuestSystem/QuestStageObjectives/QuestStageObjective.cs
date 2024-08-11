using Godot;

[GlobalClass]
public partial class QuestStageObjective : Resource
{
    [Signal]
    public delegate void QuestStageObjectiveUpdateEventHandler(QuestStageObjective questStageObjective);
    [Export]
    public string ObjectiveTargetName;
    [Export]
    public int ObjectiveTargetQuantity = 1;
    [Export]
     public bool ObjectiveComplete;
    public bool IsObjectiveComplete => ObjectiveComplete;

    public virtual void CompleteQuestStageObjective()
    {
        ObjectiveComplete = true;
        EmitSignal(nameof(QuestStageObjectiveUpdate), this);
    }

    public virtual void CompleteQuestStageObjective(string targetName, int targetQuantity)
    {
        if (targetName == ObjectiveTargetName && targetQuantity == ObjectiveTargetQuantity)
        {
            ObjectiveComplete = true;
            EmitSignal(nameof(QuestStageObjectiveUpdate), this);
        }
    }

}
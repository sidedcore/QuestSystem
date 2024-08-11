using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class QuestStage : Resource
{
    [Signal]
    public delegate void QuestStageUpdateEventHandler(QuestStage questStage);
    [Signal]
    public delegate void QuestStageCompletedEventHandler(QuestStage questStage);
    [Export]
    public int QuestStageId;
    [Export]
    public bool QuestStageComplete;

    public bool IsQuestStageActive;
    public bool IsQuestStageComplete
    {
        get
        {
            VerifyQuestStageComplete();
            return QuestStageComplete;
        }
        set
        {
            return;
        }
    }

    [Export]
    public string QuestStageDescription;
    [Export(PropertyHint.ResourceType)]
    public Array<QuestStageObjective> QuestStageObjectives = new();

    [Export]
    //-1 no next quest stage
    public int NextQuestStage = -1;

    public void SetQuestStageActive()
    {
        GD.Print($"Starting questStage: {QuestStageId}");
        IsQuestStageActive = true;
        QuestStageComplete = false;
        EmitSignal(nameof(QuestStageUpdate), this);
    }

    private void VerifyQuestStageComplete()
    {
        var allObjectivesCompleted = QuestStageObjectives.All(x => x.IsObjectiveComplete);
        if (QuestStageObjectives.Count > 1 && allObjectivesCompleted)
        {
            IsQuestStageActive = false;
            QuestStageComplete = true;
            EmitSignal(nameof(QuestStageCompleted), this);
        }
    }
}

using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class QuestStage : Resource
{
    [Export]
    public int QuestStageId { get; set; }
    public bool IsQuestStageActive { get; set; } = false;
    public bool IsQuestStageComplete { get; set; } = false;
    [Export]
    public string QuestStageDescription { get; set; }
    [Export(PropertyHint.ResourceType)]
    public Array<QuestStageObjective> stageObjectives { get; set; }

    [Export]
    //-1 no next quest stage
    public int NextQuestStage = -1;

    public void SetStageActive()
    {
        GD.Print($"Starting questStage: {QuestStageId}");
        IsQuestStageActive = true;
        IsQuestStageComplete = false;
    }

    public bool HasObjectiveBeenMet
    {
        get
        {
            if (!IsQuestStageActive) return false;
            
            return true;
        }
    }

    public void CompleteQuestStage()
    {
        IsQuestStageActive = false;
        IsQuestStageComplete = true;
    }
}

using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Quest : Resource
{
    [Signal]
    public delegate void QuestUpdateEventHandler(Quest quest);
    [Signal]
    public delegate void QuestNewStageEventHandler(Quest quest);

    [Export]
    public int QuestId;
    [Export]
    public QuestStatus QuestStatus;
    [Export(PropertyHint.ResourceType)]
    public Texture2D QuestIcon;
    [Export]
    public string QuestTitle;
    [Export]
    public string QuestDescription;
    [Export]
    public bool isQuestActive;
    [Export(PropertyHint.ResourceType)]
    public Array<QuestStage> QuestStages;
    [Export]
    public QuestCategory QuestCategory;
    public void StartQuest()
    {
        QuestStatus = QuestStatus.Started;
        GD.Print($"Starting quest: {QuestId}");
        isQuestActive = true;
        EmitSignal(nameof(QuestUpdate), this);

        SetQuestStateActive();
    }

    private void SetQuestStateActive()
    {
        GD.Print($"quest:{QuestId}. In progress");
        QuestStatus = QuestStatus.InProgress;
        QuestStages.First(x => x.IsQuestStageActive == false).SetQuestStageActive();
        EmitSignal(nameof(QuestUpdate), this);
    }

    public void FailQuest()
    {
        QuestStatus = QuestStatus.Failed;
        isQuestActive = false;
        EmitSignal(nameof(QuestUpdate), this);
    }

    public void CompleteQuest()
    {
        QuestStatus = QuestStatus.Completed;
        isQuestActive = false;
        EmitSignal(nameof(QuestUpdate), this);
    }

    public QuestStage GetCurrentQuestStage()
    {
        return QuestStages.Where(x => x.IsQuestStageActive).First();
    }

    public void MarkQuestStageObjectiveComplete(QuestStageObjective questStageObjective)
    {
        var questStage = QuestStages.Where(x => x.QuestStageObjectives.Contains(questStageObjective)).First();

        var questObj = questStage.QuestStageObjectives.First(x => x == questStageObjective);

        if (questObj == null) return;

        questObj.CompleteQuestStageObjective();
        if (questStage.IsQuestStageComplete)
        {
            var questStillContainsStages = QuestStages.All(x => x.IsQuestStageActive == false);
            if (questStillContainsStages)
            {
                if (questStage.NextQuestStage == -1)
                {
                    QuestStages.First(x => x.IsQuestStageActive == false && x.IsQuestStageComplete == false).SetQuestStageActive();
                }
                else
                {
                    QuestStages.First(x => x.QuestStageId == questStage.NextQuestStage).SetQuestStageActive();
                }

                EmitSignal(nameof(QuestNewStage), this);
            }
            else
            {
                CompleteQuest();
            }
        }
        EmitSignal(nameof(QuestUpdate), this);
    }
}

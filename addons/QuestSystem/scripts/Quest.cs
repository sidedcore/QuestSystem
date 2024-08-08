using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Quest : Resource
{
    [Export]
    public int QuestId { get; set; }
    [Export]
    public QuestStatus QuestStatus { get; set; }
    [Export(PropertyHint.ResourceType)]
    public Texture2D QuestIcon { get; set; }
    [Export]
    public string QuestTitle { get; set; }
    [Export]
    public string QuestDescription { get; set; }
    [Export]
    public bool isActive;
    [Export(PropertyHint.ResourceType)]
    public Array<QuestStage> QuestStages { get; set; }

    public void StartQuest()
    {
        QuestStatus = QuestStatus.Started;
        GD.Print($"Starting quest: {QuestId}");
        isActive = true;
        QuestStages.First().SetStageActive();

        GD.Print($"quest:{QuestId}. In progress");
        QuestStatus = QuestStatus.InProgress;
    }

    public void CompleteQuest()
    {
        QuestStatus = QuestStatus.Completed;
        isActive = false;
    }
    public QuestStage GetCurrentQuestStage()
    {
        return QuestStages.Where(x => x.IsQuestStageActive).First();
    }

    public void AdvanceQuestStage()
    {
        var currentQuestStage = GetCurrentQuestStage();
        if (currentQuestStage.HasObjectiveBeenMet)
        {
            GD.Print("Quest stages complete");
            //check if there are any next stages
            var stageIndex = QuestStages.IndexOf(currentQuestStage);

            if (stageIndex != -1 && stageIndex < QuestStages.Count)
            {
                if (currentQuestStage.NextQuestStage != -1)
                {
                    currentQuestStage.CompleteQuestStage();
                    QuestStages.Where(x => x.QuestStageId == currentQuestStage.NextQuestStage).First().SetStageActive();

                }
                else
                {
                    currentQuestStage.CompleteQuestStage();
                    QuestStages[stageIndex + 1].SetStageActive();
                }
            }
            else
            {
                //at the end of quest stages??
                CompleteQuest();
            }
        }
    }

}

using System.Linq;
using Godot;

public partial class QuestElement : Node
{
    Quest linkedQuest;
    [Export]
    Label QuestName_Label { get; set; }
    [Export]
    Label QuestDescription_Label { get; set; }
    [Export]
    PackedScene QuestStageElement;
    [Export]
    VBoxContainer QuestStages_VBoxContainer { get; set; }

    public void Initiate(Quest quest)
    {
        linkedQuest = quest;
        linkedQuest.QuestUpdate += OnQuestUpdate;
        linkedQuest.QuestNewStage += OnQuestNewStage;
        SetInformation();
        CreateQuestStageObjectives();
    }


    void CreateQuestStageObjectives()
    {
        if (linkedQuest.QuestStages.Count <= 0) return;

        foreach (var n in QuestStages_VBoxContainer.GetChildren())
        {
            RemoveChild(n);
            n.QueueFree();
        }

        foreach (var qs in linkedQuest.QuestStages.Where(x => x.IsQuestStageActive).ToList())
        {
            var questStageElement = GD.Load<PackedScene>(QuestStageElement.ResourcePath).Instantiate();
            questStageElement.GetNode<QuestStageElement>(".").Initiate(qs);
            QuestStages_VBoxContainer.AddChild(questStageElement);
        }
    }

    private void OnQuestNewStage(Quest quest)
    {
        foreach (var n in QuestStages_VBoxContainer.GetChildren())
        {
            RemoveChild(n);
            n.QueueFree();
        }

        foreach (var qs in linkedQuest.QuestStages.Where(x => x.IsQuestStageActive).ToList())
        {
            var questStageElement = GD.Load<PackedScene>(QuestStageElement.ResourcePath).Instantiate();
            questStageElement.GetNode<QuestStageElement>(".").Initiate(qs);
            QuestStages_VBoxContainer.AddChild(questStageElement);
        }
    }

    private void OnQuestUpdate(Quest quest)
    {
        SetInformation();
    }

    void SetInformation()
    {
        QuestName_Label.Text = linkedQuest.QuestTitle;
        QuestDescription_Label.Text = linkedQuest.QuestDescription;
    }
}
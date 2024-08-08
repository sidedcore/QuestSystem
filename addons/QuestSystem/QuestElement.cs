using Godot;

public partial class QuestElement : Node
{
    public Quest linkedQuest;
    [Export]
    public Label QuestName_Label { get; set; }
    [Export]
    public Label QuestDescription_Label { get; set; }

    [Export]
    public PackedScene QuestObjectiveElement;
    [Export]
    public VBoxContainer QuestStages_VBoxContainer { get; set; }

    public void Initiate(Quest quest)
    {
        linkedQuest = quest;
        QuestName_Label.Text = linkedQuest.QuestTitle;
        QuestDescription_Label.Text = linkedQuest.QuestDescription;

        foreach (var n in QuestStages_VBoxContainer.GetChildren())
        {
            RemoveChild(n);
            n.QueueFree();
        }

        foreach (var qs in linkedQuest.QuestStages)
        {
            var questStageElement = GD.Load<PackedScene>(QuestObjectiveElement.ResourcePath).Instantiate();
            questStageElement.GetNode<QuestStageElement>(".").SetStageInformation(qs);
            QuestStages_VBoxContainer.AddChild(questStageElement);
        }
    }
}
using System;
using System.Linq;
using Godot;

public partial class QuestMangerDisplay : Control
{
    [Export]
    public QuestLog QuestLog { get; set; }
    [Export]
    public VBoxContainer VBox_QuestContainer { get; set; }
    [Export]
    public PackedScene QuestElement;

    [Export]
    public Quest newquest;
    public override void _Ready()
    {
        base._Ready();
        if (QuestLog == null) return;

        QuestLog.NewQuestAdded += DisplayNewQuest;
        QuestLog.QuestComplete += OnQuestCompleted;

        foreach (var n in VBox_QuestContainer.GetChildren())
        {
            RemoveChild(n);
            n.QueueFree();
        }

        foreach (var q in QuestLog._QuestLog)
        {
            DisplayNewQuest(q.Value);
        }
    }

    private void OnQuestCompleted(Quest updatedQuest)
    {
        GD.Print("remove quest or like filter it or somehting");
    }

    public void DisplayNewQuest(Quest quest)
    {
        var questElement = GD.Load<PackedScene>(QuestElement.ResourcePath).Instantiate();
        questElement.GetNode<QuestElement>(".").Initiate(quest);
        VBox_QuestContainer.AddChild(questElement);
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            if (keyEvent.Keycode == Key.F1)
            {
                QuestLog.AddNewQuest(newquest);
            }
            if (keyEvent.Keycode == Key.F2)
            {
                QuestLog.AdvanceQuestStageObjective(QuestLog._QuestLog.First().Value);
            }
            if (keyEvent.Keycode == Key.F3)
            {
            }
            if (keyEvent.Keycode == Key.F4)
            {
            }
        }
    }
}

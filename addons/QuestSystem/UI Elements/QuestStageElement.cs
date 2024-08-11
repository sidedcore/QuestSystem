using System;
using Godot;

public partial class QuestStageElement : Node
{
    QuestStage linkedQuestStage;
    [Export]
    Label QuestStageDescription;
    [Export]
    CheckBox CheckBox;
    [Export]
    MarginContainer QuestObjectiveContainer;
    [Export]
    PackedScene QuestStageObjectiveElement;

    public void Initiate(QuestStage questStage)
    {
        linkedQuestStage = questStage;
        linkedQuestStage.QuestStageUpdate += OnQuestStageUpdate;
        linkedQuestStage.QuestStageCompleted += OnQuestStageComplete;

        SetInformation();
        CreateQuestStageObjectives();
    }



    void CreateQuestStageObjectives()
    {
        if (linkedQuestStage.QuestStageObjectives.Count <= 0) return;
        foreach (var n in QuestObjectiveContainer.GetChild(0).GetChildren())
        {
            RemoveChild(n);
            n.QueueFree();
        }
        foreach (var qsObj in linkedQuestStage.QuestStageObjectives)
        {
            var qsObjElem = GD.Load<PackedScene>(QuestStageObjectiveElement.ResourcePath).Instantiate();
            qsObjElem.GetNode<QuestStageObjectiveElement>(".").Initiate(qsObj);
            QuestObjectiveContainer.GetChild(0).AddChild(qsObjElem);
        }
    }

    void OnQuestStageUpdate(QuestStage questStage)
    {
        if (linkedQuestStage != questStage) return;
        SetInformation();
    }
    private void OnQuestStageComplete(QuestStage questStage)
    {
        throw new NotImplementedException();
    }

    private void SetInformation()
    {
        QuestStageDescription.Text = linkedQuestStage.QuestStageDescription;
        CheckBox.ButtonPressed = linkedQuestStage.IsQuestStageComplete;
    }

}

using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class QuestStageElement : Node
{
    public QuestStage linkedQuestStage;
    [Export]
    public Label QuestStageDescription; 
    [Export]
    public CheckBox CheckBox;
    [Export]
    public MarginContainer QuestObjectiveContainer;
    [Export]
    public PackedScene QuestObjectiveElement;

    public void SetStageInformation(QuestStage questStage)
    {
        linkedQuestStage = questStage;
        QuestStageDescription.Text = linkedQuestStage.QuestStageDescription;
        CheckBox.ButtonPressed = linkedQuestStage.IsQuestStageComplete;

        foreach (var qObj in linkedQuestStage.stageObjectives)
        {
            var qObjElem = GD.Load<PackedScene>(QuestObjectiveElement.ResourcePath).Instantiate();
            string value =String.Format("{0} {1}", qObj.ObjectiveTargetQuantity.ToString(), qObj.TargetName);

            qObjElem.GetNode<Label>("HBoxContainer/MarginContainer/QuestObjective_Label").Text = value;

            QuestObjectiveContainer.GetChild(0).AddChild(qObjElem);
        }
    }
}

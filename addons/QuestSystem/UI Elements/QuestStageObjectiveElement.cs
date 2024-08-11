using System;
using Godot;

public partial class QuestStageObjectiveElement : Node
{
    QuestStageObjective linkedQuestStageObjective;
    [Export]
    CheckBox checkBox;
    [Export]
    Label questLabel;

    public void Initiate(QuestStageObjective questStageObjective)
    {
        linkedQuestStageObjective = questStageObjective;
        linkedQuestStageObjective.QuestStageObjectiveUpdate += OnQuestStageObjectiveUpdate;
        SetInformation();
    }

    void OnQuestStageObjectiveUpdate(QuestStageObjective questStageObjective)
    {
        if (linkedQuestStageObjective != questStageObjective) return;
        SetInformation();
    }

    void SetInformation()
    {
        questLabel.Text = String.Format("{0} {1}", linkedQuestStageObjective.ObjectiveTargetQuantity.ToString(), linkedQuestStageObjective.ObjectiveTargetName);
        checkBox.ButtonPressed = linkedQuestStageObjective.IsObjectiveComplete;
    }
}

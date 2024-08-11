using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class QuestLog : Resource
{
    [Signal]
    public delegate void NewQuestAddedEventHandler(Quest addedQuest);
    [Signal]
    public delegate void QuestCompleteEventHandler(Quest updatedQuest);

    [Export]
    public Dictionary<int, Quest> _QuestLog = new();

    public void AddNewQuest(Quest quest)
    {
        if (_QuestLog.ContainsKey(quest.QuestId))
        {
            GD.Print("Possible duplicate quest attempting to be added");
            return;
        }

        _QuestLog.Add(quest.QuestId, quest);
        quest.StartQuest();
        EmitSignal(nameof(NewQuestAdded), quest);
    }

    public void AdvanceQuestStageObjective(Quest quest)
    {
        Quest targetQuest = HasQuestAndIsActive(quest);
        if (targetQuest == null) return;

        targetQuest.MarkQuestStageObjectiveComplete(quest.QuestStages.First().QuestStageObjectives.First());
    }

    void CompleteQuest(Quest quest)
    {
        Quest targetQuest = HasQuestAndIsActive(quest);
        if (targetQuest == null) return;
        targetQuest.CompleteQuest();
        EmitSignal(nameof(QuestComplete), quest);
    }

    Quest HasQuestAndIsActive(Quest quest)
    {
        _QuestLog.TryGetValue(quest.QuestId, out Quest targetQuest);
        if (targetQuest.QuestStatus.Equals(QuestStatus.InProgress) && targetQuest.isQuestActive)
        {
            return targetQuest;
        }
        return null;
    }
}

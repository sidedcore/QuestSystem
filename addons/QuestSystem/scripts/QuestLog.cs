using Godot;
using Godot.Collections;

[GlobalClass]
public partial class QuestLog : Resource
{
    [Export]
    public Dictionary<int, Quest> _QuestLog = new Dictionary<int, Quest>();

    public void StartQuest(Quest quest)
    {
        quest.StartQuest();
        _QuestLog.Add(quest.QuestId, quest);
    }

    public void AdvanceQuestStage(Quest quest)
    {
        Quest targetQuest = HasQuestAndIsActive(quest);
        if (targetQuest == null) return;

        targetQuest.AdvanceQuestStage();
        //Emit quest updated event
    }

    public void CompleteQuest(Quest quest)
    {
        Quest targetQuest = HasQuestAndIsActive(quest);
        if (targetQuest == null) return;
        targetQuest.CompleteQuest();
    }

    private Quest HasQuestAndIsActive(Quest quest)
    {
        _QuestLog.TryGetValue(quest.QuestId, out Quest targetQuest);
        if (targetQuest.QuestStatus.Equals(QuestStatus.InProgress) && targetQuest.isActive)
        {
            return targetQuest;
        }
        return null;
    }
}

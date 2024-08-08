using Godot;

public partial class QuestMangerDisplay : Control
{
    [Export]
    public QuestLog QuestLog { get; set; }
    [Export]
    public VBoxContainer VBox_QuestContainer { get; set; }

    [Export]
    public PackedScene QuestElement;


    public override void _Ready()
    {
        base._Ready();
        if (QuestLog != null)
        {
            foreach (var n in VBox_QuestContainer.GetChildren())
            { 
                RemoveChild(n);
                n.QueueFree();
            }

            foreach (var q in QuestLog._QuestLog)
            {
                var questElement = GD.Load<PackedScene>(QuestElement.ResourcePath).Instantiate();
                questElement.GetNode<QuestElement>(".").Initiate(q.Value);
                VBox_QuestContainer.AddChild(questElement);
            }
        }
    }
}

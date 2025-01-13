namespace BossMod.Global.MaskedCarnivale.Stage11.Act2;

public enum OID : uint
{
    Boss = 0x2719 //R=1.2
}

public enum AID : uint
{
    Fulmination = 14583 // 2719->self, 23.0s cast, range 60 circle
}

class Hints(BossModule module) : BossComponent(module)
{
    public override void AddGlobalHints(GlobalHints hints)
    {
        hints.Add("Same as last act, but this time there are 4 bombs. Pull them to the\nmiddle with Sticky Tongue and attack them with any AOE to keep them\ninterrupted. They are weak against wind and strong against fire.");
    }
}

class Stage11Act2States : StateMachineBuilder
{
    public Stage11Act2States(BossModule module) : base(module)
    {
        TrivialPhase()
            .DeactivateOnEnter<Hints>()
            .Raw.Update = () => module.Enemies(OID.Boss).All(e => e.IsDeadOrDestroyed);
    }
}

[ModuleInfo(BossModuleInfo.Maturity.Verified, Contributors = "Malediktus", GroupType = BossModuleInfo.GroupType.MaskedCarnivale, GroupID = 621, NameID = 2280, SortOrder = 2)]
public class Stage11Act2 : BossModule
{
    public Stage11Act2(WorldState ws, Actor primary) : base(ws, primary, Layouts.ArenaCenter, Layouts.Layout4Quads)
    {
        ActivateComponent<Hints>();
    }

    protected override void DrawEnemies(int pcSlot, Actor pc)
    {
        Arena.Actors(Enemies(OID.Boss));
    }
}

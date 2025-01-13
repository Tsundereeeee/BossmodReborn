namespace BossMod.Global.MaskedCarnivale.Stage04.Act2;

public enum OID : uint
{
    Boss = 0x25D5, //R=2.5
    Beetle = 0x25D6 //R=0.6
}

public enum AID : uint
{
    AutoAttack1 = 6497, // Boss->player, no cast, single-target
    AutoAttack2 = 6499, // Beetle->player, no cast, single-target

    GrandStrike = 14366, // Boss->self, 1.5s cast, range 75+R width 2 rect
    MagitekField = 14369, // Boss->self, 5.0s cast, single-target
    Spoil = 14362, // Beetle->self, no cast, range 6+R circle
    MagitekRay = 14368 // Boss->location, 3.0s cast, range 6 circle
}

class GrandStrike(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.GrandStrike), new AOEShapeRect(77.5f, 2));
class MagitekRay(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.MagitekRay), 6);
class MagitekField(BossModule module) : Components.CastInterruptHint(module, ActionID.MakeSpell(AID.MagitekField));

class Hints(BossModule module) : BossComponent(module)
{
    public override void AddGlobalHints(GlobalHints hints)
    {
        hints.Add($"{Module.PrimaryActor.Name} is weak to lightning spells.\nDuring the fight he will spawn 6 beetles.\nIf available use the Ram's Voice + Ultravibration combo for the instant kill.");
    }
}

class Hints2(BossModule module) : BossComponent(module)
{
    public override void AddGlobalHints(GlobalHints hints)
    {
        hints.Add($"{Module.PrimaryActor.Name} is weak against lightning spells and can be frozen.");
    }
}

class Stage04Act2States : StateMachineBuilder
{
    public Stage04Act2States(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<MagitekField>()
            .ActivateOnEnter<MagitekRay>()
            .ActivateOnEnter<GrandStrike>()
            .ActivateOnEnter<Hints2>()
            .DeactivateOnEnter<Hints>();
    }
}

[ModuleInfo(BossModuleInfo.Maturity.Verified, Contributors = "Malediktus", GroupType = BossModuleInfo.GroupType.MaskedCarnivale, GroupID = 614, NameID = 8087, SortOrder = 2)]
public class Stage04Act2 : BossModule
{
    public Stage04Act2(WorldState ws, Actor primary) : base(ws, primary, Layouts.ArenaCenter, Layouts.CircleBig)
    {
        ActivateComponent<Hints>();
    }

    protected override void DrawEnemies(int pcSlot, Actor pc)
    {
        Arena.Actor(PrimaryActor);
        Arena.Actors(Enemies(OID.Beetle));
    }

    protected override void CalculateModuleAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        for (var i = 0; i < hints.PotentialTargets.Count; ++i)
        {
            var e = hints.PotentialTargets[i];
            e.Priority = (OID)e.Actor.OID switch
            {
                OID.Beetle => 1,
                _ => 0
            };
        }
    }
}

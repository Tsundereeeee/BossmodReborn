﻿namespace BossMod.RealmReborn.Extreme.Ex3Titan;

class WeightOfTheLand(BossModule module) : Components.LocationTargetedAOEs(module, ActionID.MakeSpell(AID.WeightOfTheLandAOE), 6);
class GaolerVoidzone(BossModule module) : Components.PersistentVoidzone(module, 5, m => m.Enemies(OID.GaolerVoidzone).Where(e => e.EventState != 7));

[ModuleInfo(BossModuleInfo.Maturity.Verified, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 64, NameID = 1801, PlanLevel = 50)]
public class Ex3Titan : BossModule
{
    private readonly IReadOnlyList<Actor> _heart;
    public Actor? Heart() => _heart.FirstOrDefault();

    public IReadOnlyList<Actor> Gaolers;
    public IReadOnlyList<Actor> Gaols;
    public IReadOnlyList<Actor> Bombs;

    public Ex3Titan(WorldState ws, Actor primary) : base(ws, primary, new(0, 0), new ArenaBoundsCircle(25))
    {
        _heart = Enemies(OID.TitansHeart);
        Gaolers = Enemies(OID.GraniteGaoler);
        Gaols = Enemies(OID.GraniteGaol);
        Bombs = Enemies(OID.BombBoulder);
    }

    public override void CalculateAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        var heart = Heart();
        if (heart != null && heart.IsTargetable)
        {
            // heart is not added by default, since it has weird actor type
            // boss is not really a valid target, but it still hits tank pretty hard, so we want to set attacker strength (?)
            hints.PotentialTargets.Add(new(heart, false));
            //hints.PotentialTargets.Add(new(PrimaryActor, false));
        }
        base.CalculateAIHints(slot, actor, assignment, hints);
    }

    protected override void DrawEnemies(int pcSlot, Actor pc)
    {
        Arena.Actor(PrimaryActor, Colors.Enemy, true);
        Arena.Actors(Gaolers);
        Arena.Actors(Gaols, Colors.Object);
        Arena.Actors(Bombs, Colors.Object);
    }
}

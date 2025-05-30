using static BossMod.Dawntrail.Raid.BruteAmbombinatorSharedBounds.BruteAmbombinatorSharedBounds;

namespace BossMod.Dawntrail.Raid.M07NBruteAbombinator;

class BrutalImpact(BossModule module) : Components.RaidwideCast(module, ActionID.MakeSpell(AID.BrutalImpact));
class RevengeOfTheVines1(BossModule module) : Components.RaidwideCast(module, ActionID.MakeSpell(AID.RevengeOfTheVines1));
class NeoBombarianSpecial(BossModule module) : Components.RaidwideCast(module, ActionID.MakeSpell(AID.NeoBombarianSpecial));
class Powerslam(BossModule module) : Components.RaidwideCast(module, ActionID.MakeSpell(AID.Powerslam));
class Slaminator(BossModule module) : Components.CastTowers(module, ActionID.MakeSpell(AID.Slaminator), 8f, 8, 8);

class ElectrogeneticForce(BossModule module) : Components.SpreadFromCastTargets(module, ActionID.MakeSpell(AID.ElectrogeneticForce), 6f);
class SporeSac(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.SporeSac), 8f);
class Pollen(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.Pollen), 8f);
class Sporesplosion : Components.SimpleAOEs
{
    public Sporesplosion(BossModule module) : base(module, ActionID.MakeSpell(AID.Sporesplosion), 8f, 12)
    {
        MaxDangerColor = 6;
    }
}

class Explosion : Components.SimpleAOEs
{
    public Explosion(BossModule module) : base(module, ActionID.MakeSpell(AID.Explosion), 20f, 2)
    {
        MaxDangerColor = 1;
    }
}

class ItCameFromTheDirt(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.ItCameFromTheDirt), 6f);
class CrossingCrosswinds(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.CrossingCrosswinds), new AOEShapeCross(50f, 5f));
class CrossingCrosswindsHint(BossModule module) : Components.CastInterruptHint(module, ActionID.MakeSpell(AID.CrossingCrosswinds), showNameInHint: true);
class TheUnpotted(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.TheUnpotted), new AOEShapeCone(60f, 15f.Degrees()));
class WindingWildwinds(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.WindingWildwinds), new AOEShapeDonut(5f, 60f));
class WindingWildwindsHint(BossModule module) : Components.CastInterruptHint(module, ActionID.MakeSpell(AID.WindingWildwinds), showNameInHint: true);
class GlowerPower(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.GlowerPower), new AOEShapeRect(65f, 7f));

class BrutishSwingCircle2(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.BrutishSwingCircle2), 12f);
class BrutishSwingDonut(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.BrutishSwingDonut), new AOEShapeDonut(9f, 60f));

abstract class BrutishSwingCone(BossModule module, AID aid) : Components.SimpleAOEs(module, ActionID.MakeSpell(aid), new AOEShapeCone(25f, 90f.Degrees()));
class BrutishSwingCone1(BossModule module) : BrutishSwingCone(module, AID.BrutishSwingCone1);
class BrutishSwingCone2(BossModule module) : BrutishSwingCone(module, AID.BrutishSwingCone2);

abstract class BrutishSwingDonutSegment(BossModule module, AID aid) : Components.SimpleAOEs(module, ActionID.MakeSpell(aid), new AOEShapeDonutSector(22f, 88f, 90f.Degrees()));
class BrutishSwingDonutSegment1(BossModule module) : BrutishSwingDonutSegment(module, AID.BrutishSwingDonutSegment1);
class BrutishSwingDonutSegment2(BossModule module) : BrutishSwingDonutSegment(module, AID.BrutishSwingDonutSegment2);

abstract class LashingLariat(BossModule module, AID aid) : Components.SimpleAOEs(module, ActionID.MakeSpell(aid), new AOEShapeRect(70f, 16f));
class LashingLariat1(BossModule module) : LashingLariat(module, AID.LashingLariat1);
class LashingLariat2(BossModule module) : LashingLariat(module, AID.LashingLariat2);

class NeoBombarianSpecialKB(BossModule module) : Components.SimpleKnockbacks(module, ActionID.MakeSpell(AID.NeoBombarianSpecial), 58f, true)
{
    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        if (Casters.Count != 0)
        {
            hints.GoalZones.Add(hints.GoalSingleTarget(new WPos(100f, 80f), 5f, 5f));
        }
    }
}

class PulpSmash(BossModule module) : Components.StackWithIcon(module, (uint)IconID.PulpSmash, ActionID.MakeSpell(AID.PulpSmash), 6f, 5.2f, 8, 8);

[ModuleInfo(BossModuleInfo.Maturity.Verified, Contributors = "The Combat Reborn Team (Malediktus)", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 1023, NameID = 13756)]
public class M07NBruteAbombinator(WorldState ws, Actor primary) : BossModule(ws, primary, FirstCenter, DefaultArena)
{
    protected override void DrawEnemies(int pcSlot, Actor pc)
    {
        Arena.Actor(PrimaryActor);
        Arena.Actors(Enemies((uint)OID.BloomingAbomination));
    }

    protected override void CalculateModuleAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        var count = hints.PotentialTargets.Count;
        for (var i = 0; i < count; ++i)
        {
            var e = hints.PotentialTargets[i];
            e.Priority = e.Actor.OID switch
            {
                (uint)OID.BloomingAbomination => 1,
                _ => 0
            };
        }
    }
}

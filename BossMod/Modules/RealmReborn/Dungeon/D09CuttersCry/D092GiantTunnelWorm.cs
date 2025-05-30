﻿namespace BossMod.RealmReborn.Dungeon.D09CuttersCry.D092GiantTunnelWorm;
// TODO: Revist when it gets duty support to finish.
public enum OID : uint
{
    Boss = 0x536, // x1
    BottomlessDesertHelper = 0x64A, // x1
    SandPillarHelper = 0x64B // x7
}

public enum AID : uint
{
    AutoAttack = 870, // Boss->player, no cast

    Sandstorm = 529, // Boss->self, no cast, range 10.5 90-degree cleave
    SandCyclone = 1111, // Boss->player, no cast, random single-target
    Earthbreak = 531, // Boss->self, no cast, range 14.5 aoe
    BottomlessDesert = 1112, // BottomlessDesertHelper->self, no cast, raidwide drawin
    SandPillar = 1113 // SandPillarHelper->self, no cast, range 4.5 aoe
}

class Sandstorm(BossModule module) : Components.Cleave(module, ActionID.MakeSpell(AID.Sandstorm), new AOEShapeCone(10.5f, 45f.Degrees()));

// TODO: pillars teleport right before cast, so we don't show them for now...
class Submerge(BossModule module) : Components.GenericAOEs(module, ActionID.MakeSpell(AID.Earthbreak))
{
    private readonly AOEShapeCircle _shape = new(14.5f);

    public override ReadOnlySpan<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        // TODO: proper timings...
        if (!Module.PrimaryActor.IsTargetable)
            return new AOEInstance[1] { new(_shape, Module.PrimaryActor.Position, Module.PrimaryActor.Rotation) };
        return [];
    }
}

class D092GiantTunnelWormStates : StateMachineBuilder
{
    public D092GiantTunnelWormStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<Sandstorm>()
            .ActivateOnEnter<Submerge>();
    }
}

[ModuleInfo(BossModuleInfo.Maturity.Verified, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 12, NameID = 1589)]
public class D092GiantTunnelWorm(WorldState ws, Actor primary) : BossModule(ws, primary, new(-140f, 150f), new ArenaBoundsCircle(35f));

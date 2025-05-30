﻿namespace BossMod.Shadowbringers.Foray.DelubrumReginae.DRS2StygimolochWarrior;

class ViciousSwipe(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.ViciousSwipe), 15f);
class CrazedRampage(BossModule module) : Components.SimpleKnockbacks(module, ActionID.MakeSpell(AID.CrazedRampage), 13f);
class Coerce(BossModule module) : Components.StatusDrivenForcedMarch(module, 4, (uint)SID.ForwardMarch, (uint)SID.AboutFace, (uint)SID.LeftFace, (uint)SID.RightFace);

[ModuleInfo(BossModuleInfo.Maturity.Verified, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 761, NameID = 9754, PlanLevel = 80)]
public class DRS2(WorldState ws, Actor primary) : BossModule(ws, primary, new(-160f, 78f), new ArenaBoundsSquare(17.5f));

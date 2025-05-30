﻿namespace BossMod.Shadowbringers.Alliance.A37HerInfloresence;

class UnevenFooting(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.UnevenFooting), new AOEShapeRect(80, 15));
class Crash(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.Crash), new AOEShapeRect(50, 5));
class ScreamingScore(BossModule module) : Components.RaidwideCast(module, ActionID.MakeSpell(AID.ScreamingScore));
class DarkerNote1(BossModule module) : Components.SpreadFromCastTargets(module, ActionID.MakeSpell(AID.DarkerNote1), 6);
class HeavyArms1(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.HeavyArms1), new AOEShapeRect(44, 50));
class HeavyArms3(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.HeavyArms3), new AOEShapeRect(100, 6));
class PlaceOfPower(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.PlaceOfPower), 6);
class Shockwave1(BossModule module) : Components.SimpleKnockbacks(module, ActionID.MakeSpell(AID.Shockwave1), 35);
class Shockwave2(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.Shockwave2), 7);
class Towerfall2(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.Towerfall2), new AOEShapeRect(70, 7));

[ModuleInfo(BossModuleInfo.Maturity.WIP, Contributors = "The Combat Reborn Team", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 779, NameID = 9949)]
public class A37HerInfloresence(WorldState ws, Actor primary) : BossModule(ws, primary, new(-700, -700), new ArenaBoundsSquare(25));

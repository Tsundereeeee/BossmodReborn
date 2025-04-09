namespace BossMod.Dawntrail.Savage.M07SBruteAbombinator;

class PulpSmash(BossModule module) : Components.StackWithIcon(module, (uint)IconID.PulpSmash, ActionID.MakeSpell(AID.PulpSmash), 6f, 5.1f, 8, 8);
class ItCameFromTheDirt(BossModule module) : Components.SimpleAOEs(module, ActionID.MakeSpell(AID.ItCameFromTheDirt), 6f);

class TheUnpotted(BossModule module) : Components.GenericBaitAway(module, ActionID.MakeSpell(AID.TheUnpotted))
{
    private static readonly AOEShapeCone cone = new(60f, 15f.Degrees());

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action.ID == (uint)AID.ItCameFromTheDirt)
        {
            var act = Module.CastFinishAt(spell, 0.1f);
            var party = Raid.WithoutSlot(false, true, true);
            var source = Module.PrimaryActor;
            var len = party.Length;

            for (var i = 0; i < len; ++i)
            {
                ref readonly var p = ref party[i];
                CurrentBaits.Add(new(source, p, cone, act));
            }
        }
    }
}

﻿namespace BossMod.Endwalker.Alliance.A34Eulogia;

class Hydrostasis(BossModule module) : Components.Knockback(module)
{
    private readonly List<Source> _sources = new(3);

    public override IEnumerable<Source> Sources(int slot, Actor actor) => _sources;

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID is AID.HydrostasisAOE1 or AID.HydrostasisAOE2 or AID.HydrostasisAOE3)
        {
            _sources.Add(new(caster.Position, 28, Module.CastFinishAt(spell)));
            _sources.SortBy(x => x.Activation);
        }
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID is AID.HydrostasisAOE1 or AID.HydrostasisAOE2 or AID.HydrostasisAOE3)
        {
            ++NumCasts;
            if (_sources.Count != 0)
                _sources.RemoveAt(0);
        }
    }
}

namespace BossMod.Dawntrail.Savage.M04SWickedThunder;

class WideningNarrowingWitchHunt(BossModule module) : Components.GenericAOEs(module)
{
    public readonly List<AOEInstance> AOEs = new(4);

    private static readonly AOEShapeCircle _shapeOut = new(10f);
    private static readonly AOEShapeDonut _shapeIn = new(10f, 60f);

    public override ReadOnlySpan<AOEInstance> ActiveAOEs(int slot, Actor actor) => AOEs.Count != 0 ? CollectionsMarshal.AsSpan(AOEs)[..1] : [];

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        var (first, second) = spell.Action.ID switch
        {
            (uint)AID.WideningWitchHunt => (_shapeOut, _shapeIn),
            (uint)AID.NarrowingWitchHunt => (_shapeIn, _shapeOut),
            _ => ((AOEShape?)null, (AOEShape?)null)
        };
        if (first != null && second != null)
        {
            var pos = spell.LocXZ;
            AOEs.Add(new(first, pos, default, Module.CastFinishAt(spell, 1.1f)));
            AOEs.Add(new(second, pos, default, Module.CastFinishAt(spell, 4.6f)));
            AOEs.Add(new(first, pos, default, Module.CastFinishAt(spell, 8.1f)));
            AOEs.Add(new(second, pos, default, Module.CastFinishAt(spell, 11.6f)));
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if (spell.Action.ID is (uint)AID.LightningVortexAOE or (uint)AID.ThunderingAOE)
        {
            ++NumCasts;
            if (AOEs.Count != 0)
                AOEs.RemoveAt(0);
        }
    }
}

class WideningNarrowingWitchHuntBait(BossModule module) : Components.GenericBaitAway(module, ActionID.MakeSpell(AID.WitchHuntAOE), centerAtTarget: true)
{
    public enum Mechanic { None, Near, Far }

    public Mechanic CurMechanic;
    private DateTime _activation;

    private static readonly AOEShapeCircle _shape = new(6f);

    public override void Update()
    {
        CurrentBaits.Clear();
        if (CurMechanic != Mechanic.None)
        {
            var party = Raid.WithoutSlot(false, true, true);
            Array.Sort(party, (a, b) =>
                {
                    var distA = (a.Position - Arena.Center).LengthSq();
                    var distB = (b.Position - Arena.Center).LengthSq();
                    return distA.CompareTo(distB);
                });
            var len = party.Length;
            var isNear = CurMechanic == Mechanic.Near;
            var startIndex = isNear ? 0 : Math.Max(0, len - 2);
            var endIndex = isNear ? Math.Min(2, len) : len;

            for (var i = startIndex; i < endIndex; ++i)
            {
                ref readonly var p = ref party[i];
                CurrentBaits.Add(new(Module.PrimaryActor, p, _shape, _activation));
            }
        }
    }

    public override void AddGlobalHints(GlobalHints hints)
    {
        if (CurMechanic != Mechanic.None)
            hints.Add($"Next bait: {CurMechanic}");
    }

    public override void OnStatusGain(Actor actor, ActorStatus status)
    {
        if (status.ID == (uint)SID.Marker && CurMechanic == Mechanic.None)
        {
            _activation = status.ExpireAt.AddSeconds(12.2d);
            CurMechanic = status.Extra switch
            {
                0x2F6 => Mechanic.Near,
                0x2F7 => Mechanic.Far,
                _ => Mechanic.None
            };
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if (spell.Action == WatchedAction)
        {
            ++NumCasts;
            ForbiddenPlayers[Raid.FindSlot(spell.MainTargetID)] = true;
            _activation = WorldState.FutureTime(3.5d);
            if ((NumCasts & 1) == 0)
                CurMechanic = CurMechanic == Mechanic.Near ? Mechanic.Far : Mechanic.Near;
        }
    }
}

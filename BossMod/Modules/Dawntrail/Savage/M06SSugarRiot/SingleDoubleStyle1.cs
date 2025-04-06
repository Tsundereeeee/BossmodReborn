namespace BossMod.Dawntrail.Savage.M06SSugarRiot;

class SingleDoubleStyle1(BossModule module) : Components.GenericAOEs(module)
{
    public readonly List<AOEInstance> AOEs = new(12);
    private static readonly AOEShapeCircle circle = new(15f), circleBig = new(30f);
    private static readonly AOEShapeCone cone = new(50f, 50f.Degrees());

    public override ReadOnlySpan<AOEInstance> ActiveAOEs(int slot, Actor actor) => CollectionsMarshal.AsSpan(AOEs);

    public override void OnTethered(Actor source, ActorTetherInfo tether)
    {
        switch (tether.ID)
        {
            case (uint)TetherID.ActivateMechanicDoubleStyle1:
                HandleDoubleStyle1(source);
                break;
            case (uint)TetherID.ActivateMechanicDoubleStyle2:
                HandleDoubleStyle2(source);
                break;
        }
    }

    private void HandleDoubleStyle1(Actor source)
    {
        switch (source.OID)
        {
            case (uint)OID.PaintBomb:
                AddAOE(circle, source.Position, 13.1d);
                break;
            case (uint)OID.HeavenBomb:
                AddAOE(circle, source.Position + 16f * source.Rotation.ToDirection(), 13.1d);
                break;
            case (uint)OID.SweetShot:
                HandleSweetShot(source, 7.2d);
                break;
        }
    }

    private void HandleDoubleStyle2(Actor source)
    {
        switch (source.OID)
        {
            case (uint)OID.MouthwateringMorbol:
                AddAOE(cone, source.Position, 13.1d, source.Rotation);
                break;
            case (uint)OID.CandiedSuccubus:
                AddAOE(circleBig, source.Position, 13.1d, source.Rotation);
                break;
        }
    }

    private void HandleSweetShot(Actor source, double time)
    {
        var sourceP = source.Position;
        var direction = source.Rotation.ToDirection();
        var dirVector = WPos.ClampToGrid(sourceP + 60f * direction) - sourceP;
        AOEs.Add(new(new AOEShapeRect(dirVector.Length(), 3.5f), WPos.ClampToGrid(sourceP), Angle.FromDirection(dirVector), WorldState.FutureTime(time)));
    }

    private void AddAOE(AOEShape shape, WPos position, double time, Angle rotation = default)
    {
        AOEs.Add(new(shape, WPos.ClampToGrid(position), rotation, WorldState.FutureTime(time)));
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        switch (spell.Action.ID)
        {
            case (uint)AID.Burst1:
            case (uint)AID.Burst2:
            case (uint)AID.BadBreath:
            case (uint)AID.Rush:
            case (uint)AID.DarkMist:
                AOEs.Clear();
                ++NumCasts;
                break;
        }
    }
}

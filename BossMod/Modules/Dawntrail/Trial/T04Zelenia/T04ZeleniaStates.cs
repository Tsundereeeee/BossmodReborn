namespace BossMod.Dawntrail.Trial.T04Zelenia;

class T04ZeleniaStates : StateMachineBuilder
{
    public T04ZeleniaStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<ArenaChanges>()
            .ActivateOnEnter<AlexandrianThunderIV>()
            .ActivateOnEnter<AlexandrianThunderIII>()
            .ActivateOnEnter<ShockSpread>()
            .ActivateOnEnter<ShockAOE>()
            .ActivateOnEnter<PowerBreak1>()
            .ActivateOnEnter<PowerBreak2>()
            .ActivateOnEnter<HolyHazard>()
            .ActivateOnEnter<SpecterOfTheLost>()
            .ActivateOnEnter<ThunderSlash>()
            .ActivateOnEnter<RosebloodBloom>()
            .ActivateOnEnter<PerfumedQuietus>()
            .ActivateOnEnter<ValorousAscension>()
            .ActivateOnEnter<ThornedCatharsis>()
            .ActivateOnEnter<StockBreak>()
            .ActivateOnEnter<ValorousAscensionRect>();
    }
}

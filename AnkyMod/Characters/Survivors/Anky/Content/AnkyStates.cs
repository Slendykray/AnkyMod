using AnkyMod.Survivors.Anky.SkillStates;

namespace AnkyMod.Survivors.Anky
{
    public static class AnkyStates
    {
        public static void Init()
        {
            Modules.Content.AddEntityState(typeof(ThrowFireball));

            Modules.Content.AddEntityState(typeof(Roar));

            Modules.Content.AddEntityState(typeof(Deflect));

            Modules.Content.AddEntityState(typeof(Parry));

            Modules.Content.AddEntityState(typeof(Swipe));

            Modules.Content.AddEntityState(typeof(Headbutt));

            Modules.Content.AddEntityState(typeof(Spikes));

            Modules.Content.AddEntityState(typeof(Pump));

            Modules.Content.AddEntityState(typeof(StartPump));

            Modules.Content.AddEntityState(typeof(StartCharge));

            Modules.Content.AddEntityState(typeof(Charge));

        }
    }
}

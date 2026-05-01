using HenryMod.Survivors.Henry.SkillStates;

namespace HenryMod.Survivors.Henry
{
    public static class HenryStates
    {
        public static void Init()
        {
            Modules.Content.AddEntityState(typeof(SlashCombo));

            Modules.Content.AddEntityState(typeof(Shoot));

            Modules.Content.AddEntityState(typeof(Roll));

            Modules.Content.AddEntityState(typeof(ThrowBomb));

            Modules.Content.AddEntityState(typeof(Roar));

            Modules.Content.AddEntityState(typeof(Deflect));

            Modules.Content.AddEntityState(typeof(Parry));

            Modules.Content.AddEntityState(typeof(Swipe));

            Modules.Content.AddEntityState(typeof(Headbutt));

        }
    }
}

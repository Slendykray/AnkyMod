using AnkyMod.Survivors.Anky.Achievements;
using RoR2;
using UnityEngine;

namespace AnkyMod.Survivors.Anky
{
    public static class AnkyUnlockables
    {
        public static UnlockableDef characterUnlockableDef = null;
        public static UnlockableDef masterySkinUnlockableDef = null;

        public static void Init()
        {
            masterySkinUnlockableDef = Modules.Content.CreateAndAddUnlockbleDef(
                AnkyMasteryAchievement.unlockableIdentifier,
                Modules.Tokens.GetAchievementNameToken(AnkyMasteryAchievement.identifier),
                AnkySurvivor.instance.assetBundle.LoadAsset<Sprite>("texMasteryAchievement"));
        }
    }
}

using RoR2;
using AnkyMod.Modules.Achievements;

namespace AnkyMod.Survivors.Anky.Achievements
{
    //automatically creates language tokens "ACHIEVMENT_{identifier.ToUpper()}_NAME" and "ACHIEVMENT_{identifier.ToUpper()}_DESCRIPTION" 
    [RegisterAchievement(identifier, unlockableIdentifier, null, 10, null)]
    public class AnkyMasteryAchievement : BaseMasteryAchievement
    {
        public const string identifier = AnkySurvivor.HENRY_PREFIX + "masteryAchievement";
        public const string unlockableIdentifier = AnkySurvivor.HENRY_PREFIX + "masteryUnlockable";

        public override string RequiredCharacterBody => AnkySurvivor.instance.bodyName;

        //difficulty coeff 3 is monsoon. 3.5 is typhoon for grandmastery skins
        public override float RequiredDifficultyCoefficient => 3;
    }
}
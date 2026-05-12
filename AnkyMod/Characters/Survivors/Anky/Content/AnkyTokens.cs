using System;
using AnkyMod.Modules;
using AnkyMod.Survivors.Anky.Achievements;
using AnkyMod.Survivors.Anky.SkillStates;

namespace AnkyMod.Survivors.Anky
{
    public static class AnkyTokens
    {
        public static void Init()
        {
            AddHenryTokens();

            ////uncomment this to spit out a lanuage file with all the above tokens that people can translate
            ////make sure you set Language.usingLanguageFolder and printingEnabled to true
            //Language.PrintOutput("Henry.txt");
            ////refer to guide on how to build and distribute your mod with the proper folders
        }

        public static void AddHenryTokens()
        {
            string prefix = AnkySurvivor.HENRY_PREFIX;

            //string desc = "Henry is a skilled fighter who makes use of a wide arsenal of weaponry to take down his foes.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine
            // + "< ! > Sword is a good all-rounder while Boxing Gloves are better for laying a beatdown on more powerful foes." + Environment.NewLine + Environment.NewLine
            // + "< ! > Pistol is a powerful anti air, with its low cooldown and high damage." + Environment.NewLine + Environment.NewLine
            // + "< ! > Roll has a lingering armor buff that helps to use it aggressively." + Environment.NewLine + Environment.NewLine
            // + "< ! > Bomb can be used to wipe crowds with ease." + Environment.NewLine + Environment.NewLine;

            string desc = "";

            string lore = "";

            string outro = "..and so he left, searching for a new identity.";
            string outroFailure = "..and so he vanished, forever a blank slate.";

            Language.Add(prefix + "NAME", "Anky");
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", "The Chosen One");
            Language.Add(prefix + "LORE", lore);
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Passive
            Language.Add(prefix + "PASSIVE_NAME", "Anky passive");
            Language.Add(prefix + "PASSIVE_DESCRIPTION", $"Does not gain damage when leveling up. {Tokens.GreenText(100f * AnkyStaticValues.passiveCoefficent + "% of your bonus health")} is applied to damage.");
            #endregion

            #region Primary
            Language.Add(prefix + "PRIMARY_HEADBUTT_NAME", "Headbutt");
            Language.Add(prefix + "PRIMARY_HEADBUTT_DESCRIPTION", $"Headbutt in front of your crosshair for {Tokens.DamageValueText(AnkyStaticValues.headbuttDamageCoefficient)}.");
            #endregion

            #region Secondary
            Language.Add(prefix + "SECONDARY_SWIPE_NAME", "Swipe");
            Language.Add(prefix + "SECONDARY_SWIPE_DESCRIPTION", $"Swipe your tail around you for {Tokens.DamageValueText(AnkyStaticValues.swipeDamageCoefficient)}. {Tokens.ImprovedText()} {Tokens.IgnitePrefix()} Shoot an exploding fireball that uses all charges for {Tokens.DamageValueText(AnkyStaticValues.fireballImproved)}. Every charge grants {Tokens.DamageValueText(ThrowFireball.stockImproved)}.");

            Language.Add(prefix + "SECONDARY_FIREBALL_NAME", "Fireball");
            Language.Add(prefix + "SECONDARY_FIREBALL_DESCRIPTION", $"{Tokens.IgnitePrefix()} Shoot an exploding fireball that uses all charges for {Tokens.DamageValueText(AnkyStaticValues.fireballDamageCoefficient)}. Every charge grants {Tokens.DamageValueText(ThrowFireball.stockDamageCoefficent)}. {Tokens.ImprovedText()} Swipe your tail around you for {Tokens.DamageValueText(AnkyStaticValues.swipeImproved)}.");

            Language.Add(prefix + "SECONDARY_SPIKES_NAME", "Spikes");
            Language.Add(prefix + "SECONDARY_SPIKES_DESCRIPTION", $"Stomp, shoot homing fissures for {Tokens.DamageValueText(AnkyStaticValues.spikeDamageCoefficient)}. " +
                $"{Tokens.ImprovedText()} Fissures home into airborne enemies, dealing {Tokens.DamageValueText(AnkyStaticValues.spikeImproved)}.");
            #endregion


            #region Utility
            Language.Add(prefix + "UTILITY_DEFLECT_NAME", "Deflect");
            Language.Add(prefix + "UTILITY_DEFLECT_DESCRIPTION", $"{Tokens.UtilityText("Hold.")} Stand your ground, absorbing damage. Deflect attacks for {Tokens.DamageValueText(AnkyStaticValues.deflectDamageCoefficient)} + {Tokens.DamageText(100f * AnkyStaticValues.deflectArmorDamageCoefficient + "% of your armor")} of their original damage." +
                $"{Tokens.ImprovedText()} Grants {Tokens.GreenText("temporary barrier")} every second you stand. Rrelease at the right time to send the next attacks for {Tokens.DamageValueText(AnkyStaticValues.parryDamageCoefficient)}.");

            Language.Add(prefix + "UTILITY_PUMP_NAME", "Pump");
            Language.Add(prefix + "UTILITY_PUMP_DESCRIPTION", $"{Tokens.RedText("Slow yourself")}. Make ground pump around you, pulling enemies in for {Tokens.DamageValueText(AnkyStaticValues.pumpDamageCoefficient)} + {Tokens.GreenText(100f * AnkyStaticValues.pumpHealthDamageCoefficient + "% bonus health")}. " +
                $"{Tokens.ImprovedText()} Every wave grants {Tokens.GreenText("temporary barrier")}." +
                $" Your attacks are granted bonus {Tokens.GreenText(100f * AnkyStaticValues.pumpBuff + "% damage of your health")}. {Tokens.RedText("Duration reduced")}.");

            //Language.Add(prefix + "UTILITY_CHARGE_NAME", "Charge");
            //Language.Add(prefix + "UTILITY_CHARGE_DESCRIPTION", "After preparing for 1 second, start charging, gaining velocity over time. Pushes small enemies away and deals 5% MaxHP to bigger enemies, stopping your tracks and damaging you for 5% current health. Heavy.");
            #endregion

            #region Special
            Language.Add(prefix + "SPECIAL_ROAR_NAME", "ROAR!");
            Language.Add(prefix + "SPECIAL_ROAR_DESCRIPTION", $"ROAR! Dealing {Tokens.MultDamageValueText(Roar.maxAttacks, AnkyStaticValues.roarDamageCoefficient)} and {Tokens.UtilityText("improving your next skill")}. {Tokens.ImprovedText()} Roar again, {Tokens.RedText("damaging yourself")} but {Tokens.GreenText("granting permanent 35 health")}.");

            Language.Add(prefix + "SPECIAL_CHARGE_NAME", "Charge");
            Language.Add(prefix + "SPECIAL_CHARGE_DESCRIPTION", $"{Tokens.UtilityText("Heavy.")} Prepare, then start charging for {Tokens.DamageValueText(AnkyStaticValues.chargeDamageCoefficient)}, gaining velocity over time. Bumping into big enemies {Tokens.GreenText("grants you permanent 25 health")} and {Tokens.RedText("damaging you for 5% current health")}.");
            #endregion

            #region Achievements
            Language.Add(Tokens.GetAchievementNameToken(AnkyMasteryAchievement.identifier), "Henry: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(AnkyMasteryAchievement.identifier), "As Henry, beat the game or obliterate on Monsoon.");
            #endregion
        }
    }
}

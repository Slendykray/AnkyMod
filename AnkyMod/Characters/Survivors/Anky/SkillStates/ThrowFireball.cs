using EntityStates;
using AnkyMod.Survivors.Anky;
using AnkyMod.Survivors.Anky.Components;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace AnkyMod.Survivors.Anky.SkillStates
{
    public class ThrowFireball : GenericProjectileBaseState
    {
        public static float BaseDuration = 0.65f;
        //delays for projectiles feel absolute ass so only do this if you know what you're doing, otherwise it's best to keep it at 0
        public static float BaseDelayDuration = 0.0f;

        public static float DamageCoefficient = AnkyStaticValues.fireballDamageCoefficient;

        private AnkyController ankyController;

        private int stocks;

        public static float stockDamageCoefficent = 0.7f;
        public static float stockImproved = 0.5f;
        public override void OnEnter()
        {
            ankyController = GetComponent<AnkyController>();
            ankyController.ClearSkillOverrides();


            stocks = skillLocator.secondary.stock;

            skillLocator.secondary.stock = 0;

            if (ankyController.improved)
            {
                DamageCoefficient = AnkyStaticValues.fireballImproved + stocks * stockDamageCoefficent;

                ankyController.improved = false;
            }
            else
            {
                DamageCoefficient = AnkyStaticValues.fireballDamageCoefficient + stocks * stockImproved;
            }

           

            projectilePrefab = AnkyAssets.bombProjectilePrefab;
            //base.effectPrefab = Modules.Assets.SomeMuzzleEffect;
            //targetmuzzle = "muzzleThrow"

            //attackSoundString = "HenryBombThrow";
            attackSoundString = "Play_mage_m1_shoot";

            baseDuration = BaseDuration;
            baseDelayBeforeFiringProjectile = BaseDelayDuration;

            damageCoefficient = DamageCoefficient;
            //proc coefficient is set on the components of the projectile prefab
            force = 80f;

            //base.projectilePitchBonus = 0;
            //base.minSpread = 0;
            //base.maxSpread = 0;

            recoilAmplitude = 0.1f;
            bloom = 10;

            base.OnEnter();
        }

        public override void ModifyProjectileInfo(ref FireProjectileInfo fireProjectileInfo)
        {
            base.ModifyProjectileInfo(ref fireProjectileInfo);
            fireProjectileInfo.damageTypeOverride = DamageTypeCombo.GenericSpecial;
            fireProjectileInfo.speedOverride = 100f;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

        public override void PlayAnimation(float duration)
        {

            if (GetModelAnimator())
            {
                PlayAnimation("Gesture, Override", "ThrowBomb", "ThrowBomb.playbackRate", this.duration);
            }
        }
    }
}
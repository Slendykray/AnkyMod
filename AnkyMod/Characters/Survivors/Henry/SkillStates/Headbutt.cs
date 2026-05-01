using EntityStates;
using HenryMod.Modules.BaseStates;
using HenryMod.Survivors.Henry.Components;
using RoR2;
using UnityEngine;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class Headbutt : BaseState
    {
        HenryWeaponComponent ankyController;

        public float damageCoefficient = HenryStaticValues.headbuttDamageCoefficient;
        public float procCoefficient = 1f;
        public float baseDuration = 0.8f;
        //delay on firing is usually ass-feeling. only set this if you know what you're doing
        public float firePercentTime = 0.0f;
        public float force = 800f;
        public float recoil = 1.5f;
        public float range = 10f;
        //public static GameObject tracerEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerGoldGat");

        private float duration;
        private float fireTime;
        private bool hasFired;
        private string muzzleString;

        private DamageTypeCombo damageType;

        public override void OnEnter()
        {
         

            ankyController = GetComponent<HenryWeaponComponent>();
            ankyController.ClearSkillOverrides();

            damageType = DamageTypeCombo.GenericPrimary;

            if (ankyController.improved)
            {
                damageType.damageType = DamageType.Stun1s;
                damageCoefficient = HenryStaticValues.headbuttImproved;

                ankyController.improved = false;
            }

            duration = baseDuration / attackSpeedStat;
            fireTime = firePercentTime * duration;
            characterBody.SetAimTimer(2f);

            //PlayCrossfade("Gesture, Override", "Slash" + 1, "Slash.playbackRate", duration, 0.1f * duration);

            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (fixedAge >= fireTime)
            {
                Fire();
            }

            if (fixedAge >= duration && isAuthority)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        private void Fire()
        {

            if (!hasFired)
            {
                hasFired = true;

                //characterBody.AddSpreadBloom(1f);
                //EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, gameObject, muzzleString, false);
                //GetComponent<StarPlatinum>().StarFingerEnd();

                if (isAuthority)
                {
                    Ray aimRay = GetAimRay();
                    AddRecoil(-1f * recoil, -2f * recoil, -0.5f * recoil, 0.5f * recoil);

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = damageCoefficient * damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = damageType,
                        falloffModel = BulletAttack.FalloffModel.None,
                        maxDistance = range,
                        force = force,
                        hitMask = LayerIndex.CommonMasks.bullet,
                        minSpread = 0f,
                        maxSpread = 0f,
                        isCrit = RollCrit(),
                        owner = gameObject,
                        muzzleName = muzzleString,
                        smartCollision = true,
                        procChainMask = default,
                        procCoefficient = procCoefficient,
                        radius = 2f,
                        sniper = false,
                        stopperMask = LayerIndex.CommonMasks.bullet,
                        weapon = null,
                        //tracerEffectPrefab = tracerEffectPrefab,
                        spreadPitchScale = 1f,
                        spreadYawScale = 1f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        //hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.hitEffectPrefab,
                        hitEffectPrefab = HenryAssets.swordHitImpactEffect,
                    }.Fire();
                }
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}
using EntityStates;
using HenryMod.Survivors.Henry;
using HenryMod.Survivors.Henry.Components;
using RoR2;
using RoR2.Projectile;
using System;
using UnityEngine;


namespace HenryMod.Survivors.Henry.SkillStates
{
    public class Spikes : BaseSkillState
    {
        HenryWeaponComponent ankyController;

        private float stopwatch;
        private float duration = 0.4f;

        private int spikesNum = 5;

        public override void OnEnter()
        {
            base.OnEnter();

            ankyController = GetComponent<HenryWeaponComponent>();
            ankyController.ClearSkillOverrides();


            float damage = HenryStaticValues.spikeDamageCoefficient;


            SpikeController spikeController = HenryAssets.spikeProjectilePrefab.GetComponent<SpikeController>();
            //spikeController.downVector = spikeController.downVectorDefault;
            spikeController.fly = ankyController.improved;
            if (ankyController.improved)
            {
                //spikeController.downVector = Vector3.zero;

                damage = HenryStaticValues.spikeImproved;
                ankyController.improved = false;
            }

            float baseDamage = characterBody.damage * damage;

            Ray aimRay = base.GetAimRay();

            for (int i = 0; i < spikesNum; i++)
            {
                float angle = i * (360f / spikesNum);

                Vector3 dir = Quaternion.AngleAxis(angle, Vector3.up) * aimRay.direction;
                Quaternion rot = Util.QuaternionSafeLookRotation(dir);


                RoR2.Projectile.ProjectileManager.instance.FireProjectile(
                  HenryAssets.spikeProjectilePrefab,
                  characterBody.corePosition,
                   rot,
                  gameObject,
                  baseDamage,
                  0f,
                  Util.CheckRoll(characterBody.crit, characterBody.master),
                  damageType: DamageSource.Primary
                  );




            }
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            float deltaTime = base.GetDeltaTime();
            this.stopwatch += deltaTime;

            if (this.stopwatch >= duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }



        }

        public override void OnExit()
        {
           
            base.OnExit();
        }

    }
}
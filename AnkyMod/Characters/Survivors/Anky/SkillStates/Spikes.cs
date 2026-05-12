using EntityStates;
using AnkyMod.Survivors.Anky;
using AnkyMod.Survivors.Anky.Components;
using RoR2;
using RoR2.Projectile;
using System;
using UnityEngine;


namespace AnkyMod.Survivors.Anky.SkillStates
{
    public class Spikes : BaseSkillState
    {
        private AnkyController ankyController;

        private float duration = 0.3f;

        private int spikesNum = 1;

        public override void OnEnter()
        {
            base.OnEnter();

            ankyController = GetComponent<AnkyController>();
            ankyController.ClearSkillOverrides();


            float damage = AnkyStaticValues.spikeDamageCoefficient;

            SpikeController spikeController = AnkyAssets.spikeProjectilePrefab.GetComponent<SpikeController>();

            spikeController.fly = ankyController.improved;

            if (ankyController.improved)
            {
                damage = AnkyStaticValues.spikeImproved;
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
                  AnkyAssets.spikeProjectilePrefab,
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

            if (this.fixedAge >= duration && base.isAuthority)
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
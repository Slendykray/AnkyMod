using AnkyMod.Survivors.Anky;
using AnkyMod.Survivors.Anky.Components;
using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace AnkyMod.Survivors.Anky.SkillStates
{
    public class Charge: BaseSkillState
    {
        private AnkyController ankyController;

        public static float velocityDamageCoefficient = 0.3f;
        public static float damageCoefficient = 2f;

        private float bonusDamage;

        private float duration = 4.5f;
        private OverlapAttack overlapAttack;

        private float proc = 1f;

        private float stopMass = 300f;

        private float force = 3000f;

        private float attackStopwatch;
        private float damageFrequency = 2f;

        public override void OnEnter()
        {
            base.OnEnter();

            ankyController = GetComponent<AnkyController>();

            this.overlapAttack = base.InitMeleeOverlap(AnkyStaticValues.roarDamageCoefficient, AnkyAssets.swordHitImpactEffect, base.GetModelTransform(), "SwordGroup");

            this.overlapAttack.damageType.damageSource = DamageSource.Utility;

            this.overlapAttack.procCoefficient = proc;

            overlapAttack.pushAwayForce = force;
             
        }

        //public static float chargeMovementSpeedCoefficient;
        private Vector3 targetMoveVector;

        private Vector3 targetMoveVectorVelocity;

        //public static float turnSpeed;

        //public static float turnSmoothTime;

        float speed = 1f;
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            //base.characterMotor.rootMotion += dashVector * (this.moveSpeedStat * dashSpeed * base.GetDeltaTime());

            this.targetMoveVector = Vector3.ProjectOnPlane(Vector3.SmoothDamp(this.targetMoveVector, base.inputBank.aimDirection, ref this.targetMoveVectorVelocity, EntityStates.Bison.Charge.turnSmoothTime, EntityStates.Bison.Charge.turnSpeed), Vector3.up).normalized;
            base.characterDirection.moveVector = this.targetMoveVector;

            speed += 1f * Time.fixedDeltaTime;
            Vector3 forward = base.characterDirection.forward;
            base.characterMotor.moveDirection = forward * speed;

            //characterDirection.forward = base.inputBank.aimDirection;
            ////if (characterMotor.velocity.magnitude < maxVelocity)
            //    characterMotor.velocity += characterDirection.forward * 1.5f;


            float punchSpeed = base.characterMotor.velocity.magnitude;
            this.bonusDamage = punchSpeed * (velocityDamageCoefficient * this.damageStat);

            this.overlapAttack.damage = damageStat * damageCoefficient + bonusDamage;

            //this.overlapAttack.damage = 0f;

            List<HurtBox> hitResults = new List<HurtBox>();

            if (overlapAttack.Fire(hitResults))
            {


                for (int i = 0; i < hitResults.Count; i++)
                {
                    HealthComponent healthComponent = hitResults[i].healthComponent;
                    if (healthComponent)
                    {
                        CharacterMotor motor = healthComponent.body.characterMotor;

                        if (motor.mass >= stopMass)
                        {

                            characterBody.inventory.GiveItemPermanent(AnkyPlugin.roarAddHealth, 1);

                            if (NetworkServer.active && base.healthComponent)
                            {
                                DamageInfo damageInfo = new DamageInfo();
                                damageInfo.damage = base.healthComponent.combinedHealth * 0.25f;
                                damageInfo.position = base.characterBody.corePosition;
                                damageInfo.force = Vector3.zero;
                                damageInfo.damageColorIndex = DamageColorIndex.Default;
                                damageInfo.crit = false;
                                damageInfo.attacker = null;
                                damageInfo.inflictor = null;
                                damageInfo.damageType = (DamageType.NonLethal | DamageType.BypassArmor);
                                damageInfo.procCoefficient = 0f;
                                damageInfo.procChainMask = default(ProcChainMask);
                                base.healthComponent.TakeDamage(damageInfo);
                            }

                            this.outer.SetNextStateToMain();
                      
                        }
                     

                    }
                }
            }


            this.attackStopwatch += base.GetDeltaTime();
            float num = 1f / damageFrequency / this.attackSpeedStat;
            if (this.attackStopwatch >= 1f)
            {
                this.attackStopwatch -= num;
                overlapAttack.ResetIgnoredHealthComponents();

            }

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
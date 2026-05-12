using EntityStates;
using HenryMod.Survivors.Henry;
using HenryMod.Survivors.Henry.Components;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using EntityStates.BeetleGuardMonster;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class Pump : AnkySkill
    {
        //AnkyController ankyController;

        private float duration = 7f;

        private float barrierStopwatch;
        private float barrierDelay = 1.2f;
        private float barrierValue = 20f;


        private float slamForce = 2100f;
        private float slamRadius = 25f;

        //private bool improved;


        public override void OnEnter()
        {
            base.OnEnter();

            if (improved)
            {
                characterBody.AddTimedBuff(HenryBuffs.damageBuff, duration);
                duration = 5f;
            }

            Fire();
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            this.barrierStopwatch += base.GetDeltaTime();
            float num = barrierDelay;

            if (this.barrierStopwatch >= num)
            {
                this.barrierStopwatch -= num;

                Fire();
            }


            if (this.fixedAge >= duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }

        }

        private void Fire()
        {

            if (improved)
            {
                healthComponent.AddBarrier(barrierValue);
            }
           
            //Util.PlaySound("Play_falseson_skill1_impact_full", base.gameObject);
            //Util.PlaySound("Play_loader_R_variant_slam", base.gameObject);

            EffectManager.SimpleMuzzleFlash(GroundSlam.slamEffectPrefab, base.gameObject, "Muzzle", true);


            List<HurtBox> HurtBoxes = new List<HurtBox>();
            HurtBoxes = new SphereSearch
            {
                radius = slamRadius,
                mask = LayerIndex.entityPrecise.mask,
                origin = transform.position
            }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(base.teamComponent.teamIndex)).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

            foreach (HurtBox hurtbox in HurtBoxes)
            {
                Vector3 force = Vector3.zero;

                HealthComponent healthComponent = hurtbox.healthComponent;
                if (healthComponent)
                {
                    Vector3 dir = (characterBody.corePosition - healthComponent.body.corePosition).normalized * slamForce;
                    float massFactor = 0f;

                    CharacterMotor motor = healthComponent.body.characterMotor;
                    Rigidbody rb = healthComponent.body.rigidbody;
                    if (motor)
                    {
                        massFactor = motor.mass / 100f;
                    }
                    else if (rb)
                    {
                        massFactor = rb.mass / 100f;
                    }

                    force = Vector3.zero + dir * massFactor;
                }

                DamageInfo damageInfo = new DamageInfo();
                //damageInfo.damage = this.damageStat * HenryStaticValues.pumpDamageCoefficient + (HenryStaticValues.pumpHealthDamageCoefficient * GetBonusHealth(characterBody));
                damageInfo.damage = this.damageStat * HenryStaticValues.pumpDamageCoefficient + (HenryStaticValues.pumpHealthDamageCoefficient * GetBonusHealth(characterBody));
                damageInfo.attacker = base.gameObject;
                damageInfo.inflictor = base.gameObject;
                damageInfo.force = force;
                damageInfo.crit = base.RollCrit();
                damageInfo.procCoefficient = 1f;
                damageInfo.position = hurtbox.gameObject.transform.position;
                //damageInfo.damageType = DamageType.Stun1s;
                damageInfo.damageType = DamageTypeCombo.GenericUtility;

                hurtbox.healthComponent.TakeDamage(damageInfo);
                GlobalEventManager.instance.OnHitEnemy(damageInfo, hurtbox.healthComponent.gameObject);
                GlobalEventManager.instance.OnHitAll(damageInfo, hurtbox.healthComponent.gameObject);

            }
        }

        private float GetBonusHealth(CharacterBody characterBody)
        {
            return characterBody.maxBonusHealth - (characterBody.baseMaxHealth + characterBody.levelMaxHealth * (characterBody.level - 1f));
        }

        public override void OnExit()
        {
            base.OnExit();
        }

    }
}
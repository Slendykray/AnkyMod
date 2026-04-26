using EntityStates;
using HenryMod.Modules.BaseStates;
using HenryMod.Survivors.Henry.Components;
using RoR2;
using RoR2.Skills;
using System.Collections.Generic;
using UnityEngine;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class Roar : BaseState
    {
        HenryWeaponComponent ankyController;


        private float attackStopwatch;

        private int maxAttacks = 5;
        private int curAttacks;

        private float damageFrequency = 6f;

        private float proc = 1f;

        private float attackRecoil = 2f;

        private OverlapAttack overlapAttack;

        public override void OnEnter()
        {
            base.OnEnter();

            ankyController = GetComponent<HenryWeaponComponent>();
            ankyController.ClearSkillOverrides();


            this.overlapAttack = base.InitMeleeOverlap(HenryStaticValues.roarDamageCoefficient, HenryAssets.swordHitImpactEffect, base.GetModelTransform(), "SwordGroup");

            this.overlapAttack.damageType.damageSource = DamageSource.Special;

            overlapAttack.procCoefficient = proc;

           

            if (ankyController.improved)
            {
                //DamageInfo damageInfo = new DamageInfo();
                ////damageInfo.attacker = attacker;
                ////damageInfo.inflictor = inflictor;
                ////damageInfo.force = forceVector + pushAwayForce * overlapInfo.pushDirection;
                ////damageInfo.physForceFlags = forceInfoFlags;

                //damageInfo.damage = 35f;
                ////damageInfo.crit = isCrit;
                ////damageInfo.position = overlapInfo.hitPosition;
                ////damageInfo.procChainMask = procChainMask;
                ////damageInfo.procCoefficient = procCoefficient;
                ////damageInfo.damageColorIndex = damageColorIndex;
                ////damageInfo.inflictedHurtbox = overlapInfo.hurtBox;
                ////damageInfo.damageType = damageType;
                //healthComponent.TakeDamage(damageInfo);
                ankyController.improved = false;
            }           
            else
            {
                ImproveSkills();        
            }
              
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (curAttacks >= maxAttacks)
            {
                this.outer.SetNextStateToMain();
            }

            this.attackStopwatch += base.GetDeltaTime();
            float num = 1f / damageFrequency / this.attackSpeedStat;
            if (this.attackStopwatch >= num)
            {
                this.attackStopwatch -= num;
                curAttacks++;

                if (isAuthority)
                {
                    AddRecoil(-1f * attackRecoil, -2f * attackRecoil, -0.5f * attackRecoil, 0.5f * attackRecoil);
                }

                                   
                overlapAttack.ResetIgnoredHealthComponents();

                overlapAttack.Fire();
 

            }
        }

        public void ImproveSkills()
        {
            SkillStateOverrideData skillOverrides = new SkillStateOverrideData(characterBody);
            skillOverrides.simulateRestockForOverridenSkills = false;

            foreach (GenericSkill skill in skillLocator.allSkills)
            {
                if (skill.skillDef == skillLocator.primary.skillDef)
                {
                    skillOverrides.primarySkillOverride = FindImprovedDef(skill);
                }
                if (skill.skillDef == skillLocator.secondary.skillDef)
                {
                    skillOverrides.secondarySkillOverride = FindImprovedDef(skill);
                }
                if (skill.skillDef == skillLocator.utility.skillDef)
                {
                    skillOverrides.utilitySkillOverride = FindImprovedDef(skill);
                }
                if (skill.skillDef == skillLocator.special.skillDef)
                {
                    skillOverrides.specialSkillOverride = FindImprovedDef(skill);
                }
            }

            skillOverrides.OverrideSkills(skillLocator);

            ankyController.skillOverrides = skillOverrides;
            ankyController.improved = true;

        }

        SkillDef FindImprovedDef(GenericSkill skill)
        {
            string name = skill.skillDef.skillName;
            int index = SkillCatalog.FindSkillIndexByName(name + "Improved");

            return SkillCatalog.GetSkillDef(index);
        }

   


        //public virtual void ImproveSkills()
        //{
        //    foreach (GenericSkill skill in skillLocator.allSkills)
        //    {
        //        ImproveSkill(skill);
        //    }
        //}

        //void ImproveSkill(GenericSkill skill)
        //{
        //    if (!FindImprovedDef(skill))
        //        return;

        //    skill.SetSkillOverride(base.characterBody, FindImprovedDef(skill), GenericSkill.SkillOverridePriority.Contextual);
        //}



        //public static void UnsetSkills(CharacterBody body)
        //{
        //    foreach (GenericSkill skill in body.skillLocator.allSkills)
        //    {
        //        skill.UnsetSkillOverride(body, skill.skillDef, GenericSkill.SkillOverridePriority.Contextual);
        //    }

        //}

         
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

        //protected override void PlayAttackAnimation()
        //{
        //    PlayCrossfade("Gesture, Override", "Slash" + (1 + swingIndex), playbackRateParam, duration, 0.1f * duration);
        //}

        //protected override void PlaySwingEffect()
        //{
        //    base.PlaySwingEffect();
        //}

        //protected override void OnHitEnemyAuthority()
        //{
        //    base.OnHitEnemyAuthority();
        //}

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
using EntityStates;
using HenryMod.Modules.BaseStates;
using HenryMod.Survivors.Henry.Components;
using RoR2;
using RoR2.Skills;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class Roar : BaseState
    {
        private AnkyController ankyController;


        private float attackStopwatch;

        private int maxAttacks = 5;
        private int curAttacks; 

        private float damageFrequency = 10f;

        private float proc = 1f;

        private float attackRecoil = 1f;

        private OverlapAttack overlapAttack;

        private float healthCost = 0.5f;

        public override void OnEnter()
        {
            base.OnEnter();

            Util.PlaySound("Play_acrid_sprint_start", gameObject);

            float delay = 1f / damageFrequency / this.attackSpeedStat;
            float dur = delay * maxAttacks;
            PlayCrossfade("FullBody, Override", "Roar", "Roll.playbackRate", dur, 0.05f);



            ankyController = GetComponent<AnkyController>();
            ankyController.ClearSkillOverrides();
              

            this.overlapAttack = base.InitMeleeOverlap(HenryStaticValues.roarDamageCoefficient, HenryAssets.swordHitImpactEffect, base.GetModelTransform(), "RoarGroup");

            this.overlapAttack.damageType.damageSource = DamageSource.Special;

            overlapAttack.procCoefficient = proc;

            

            if (ankyController.improved)
            {
                if (NetworkServer.active && base.healthComponent)
                {
                    DamageInfo damageInfo = new DamageInfo();
                    damageInfo.damage = base.healthComponent.combinedHealth * healthCost;
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

                characterBody.inventory.GiveItemPermanent(HenryPlugin.roarAddHealth, 1);

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

        public override void OnExit()
        {
            base.OnExit();
        }

        //public override InterruptPriority GetMinimumInterruptPriority()
        //{
        //    return InterruptPriority.PrioritySkill;
        //}
    }
}
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
    public class AnkySkill : BaseSkillState
    {
        protected AnkyController ankyController;
        protected bool improved;

        //protected float duration;

        public override void OnEnter()
        {
            base.OnEnter();

            ankyController = GetComponent<AnkyController>();
            ankyController.ClearSkillOverrides();
            improved = ankyController.improved;
        
            if (improved)
            {
                ankyController.improved = false;
            }
        }

        //public override void FixedUpdate()
        //{
        //    base.FixedUpdate();

        //    if (this.fixedAge >= duration && base.isAuthority)
        //    {
        //        this.outer.SetNextStateToMain();
        //    }
        //}

        public override void OnExit()
        {
            base.OnExit();
        }

    }
}
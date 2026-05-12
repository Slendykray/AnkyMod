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
    public class StartCharge: BaseSkillState
    {
        private AnkyController ankyController;

        private float duration = 1f;
        private float improvedDuration = 0.55f;

        public override void OnEnter()
        {
            base.OnEnter();

            ankyController = GetComponent<AnkyController>();
            ankyController.ClearSkillOverrides();

            if (ankyController.improved)
                duration = improvedDuration;

        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (this.fixedAge >= duration && base.isAuthority)
            {
                this.outer.SetNextState(new SkillStates.Charge());
            }

        }

        public override void OnExit()
        {
            base.OnExit();
        }

    }
}
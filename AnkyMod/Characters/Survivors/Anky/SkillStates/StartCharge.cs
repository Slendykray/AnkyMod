using EntityStates;
using AnkyMod.Survivors.Anky;
using AnkyMod.Survivors.Anky.Components;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using EntityStates.BeetleGuardMonster;

namespace AnkyMod.Survivors.Anky.SkillStates
{
    public class StartCharge: BaseSkillState
    {
        private float duration = 0.55f;

        public override void OnEnter()
        {
            base.OnEnter();
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
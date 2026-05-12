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
    public class StartPump : BaseSkillState
    {

        private float duration = 0.4f;


        public override void OnEnter()
        {
            base.OnEnter();

            EntityStateMachine entityStateMachine2 = EntityStateMachine.FindByCustomName(base.gameObject, "Pump");
            Pump state = new Pump();
            EntityState entityState = state;

            if (entityStateMachine2 && entityState != null)
            {
                entityStateMachine2.SetNextState(entityState);
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
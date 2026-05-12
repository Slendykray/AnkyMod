using EntityStates;
using HenryMod.Survivors.Henry;
using HenryMod.Survivors.Henry.Components;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class Deflect : BaseSkillState
    {
        AnkyController ankyController;

        private float stopwatch;
        private float duration = 5f;

        private float barrierStopwatch;
        private float barrierDelay = 1f;
        private float barrierValue = 20f;


        public override void OnEnter()
        {
            base.OnEnter();

            ankyController = GetComponent<AnkyController>();
            ankyController.ClearSkillOverrides();
            ankyController.deflecting = true;

            GetModelAnimator().SetBool("deflecting", true);
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            float deltaTime = base.GetDeltaTime();
            this.stopwatch += deltaTime;

            this.barrierStopwatch += deltaTime;
            float num = barrierDelay;

            if (this.barrierStopwatch >= num && ankyController.improved)
            {
                this.barrierStopwatch -= num;

                healthComponent.AddBarrier(barrierValue);
            }


            //if (this.stopwatch >= duration && base.isAuthority)
            //{
            //    this.outer.SetNextStateToMain();
            //}


            if (IsKeyJustReleasedAuthority())
            {

                if (ankyController.improved)
                {
                    this.outer.SetNextState(new Parry());
                }
                else
                {
                    this.outer.SetNextStateToMain();
                }

            }
        }

        public override void OnExit()
        {
            base.OnExit();

            ankyController.deflecting = false;
            GetModelAnimator().SetBool("deflecting", false);

            if (ankyController.improved)
                ankyController.improved = false;
        }

    }
}
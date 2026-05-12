using EntityStates;
using AnkyMod.Survivors.Anky;
using AnkyMod.Survivors.Anky.Components;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace AnkyMod.Survivors.Anky.SkillStates
{
    public class Deflect : BaseSkillState
    {
        private AnkyController ankyController;

        private float duration = 6f;

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

            this.barrierStopwatch += base.GetDeltaTime();
            float num = barrierDelay;

            if (this.barrierStopwatch >= num && ankyController.improved)
            {
                this.barrierStopwatch -= num;

                healthComponent.AddBarrier(barrierValue);
            }


            if (this.fixedAge >= duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }


            if (IsKeyJustReleasedAuthority())
            {
                this.outer.SetNextStateToMain();

                //if (ankyController.improved)
                //{
                //    this.outer.SetNextState(new Parry());
                //}
                //else
                //{
                //    this.outer.SetNextStateToMain();
                //}

            }
        }

        public override void OnExit()
        {
            base.OnExit();

            ankyController.deflecting = false;
            GetModelAnimator().SetBool("deflecting", false);

            if (ankyController.improved)
            {
                ankyController.improved = false;
                this.outer.SetNextState(new Parry());
            }
               


        }

    }
}
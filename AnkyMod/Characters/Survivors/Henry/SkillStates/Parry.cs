using EntityStates;
using HenryMod.Survivors.Henry;
using HenryMod.Survivors.Henry.Components;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class Parry : BaseSkillState
    {
        HenryWeaponComponent ankyController;
        private float stopwatch;
        private float duration = 0.5f;

        public override void OnEnter()
        {
            base.OnEnter();
            ankyController = GetComponent<HenryWeaponComponent>();
            ankyController.parry = true;

            PlayCrossfade("FullBody, Override", "Parry", "Roll.playbackRate", duration, 0.05f);

            Vector3 vector = characterBody.corePosition;
            EffectData effectData = new EffectData
            {
                origin = vector,
                start = vector,
                rotation = Quaternion.identity,
                scale = characterBody.radius
            };

            EffectManager.SpawnEffect(CharacterBody.CommonAssets.parryActivateEffect, effectData, true);
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            float deltaTime = base.GetDeltaTime();
            this.stopwatch += deltaTime;



            if (this.stopwatch >= duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }


        }

        public override void OnExit()
        {
            base.OnExit();

            ankyController.parry = false;
        }

    }
}
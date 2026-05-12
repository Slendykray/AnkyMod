using EntityStates;
using AnkyMod.Survivors.Anky;
using AnkyMod.Survivors.Anky.Components;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace AnkyMod.Survivors.Anky.SkillStates
{
    public class Parry : BaseSkillState
    {
        private AnkyController ankyController;
        private float duration = 0.5f;

        public override void OnEnter()
        {
            base.OnEnter();
            ankyController = GetComponent<AnkyController>();
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

            if (this.fixedAge >= duration && base.isAuthority)
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
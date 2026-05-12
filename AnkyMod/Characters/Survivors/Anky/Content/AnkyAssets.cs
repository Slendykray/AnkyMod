using AnkyMod.Modules;
using AnkyMod.Survivors.Anky.Components;
using RoR2;
using RoR2.Projectile;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AnkyMod.Survivors.Anky
{
    public static class AnkyAssets
    {
        // particle effects
        public static GameObject swordSwingEffect;
        public static GameObject swordHitImpactEffect;

        public static GameObject bombExplosionEffect;

        // networked hit sounds
        public static NetworkSoundEventDef swordHitSoundEvent;

        //projectiles
        public static GameObject bombProjectilePrefab;

        private static AssetBundle _assetBundle;

        public static GameObject spikeProjectilePrefab;

        public static GameObject fallProjectilePrefab;

        public static void Init(AssetBundle assetBundle)
        {

            _assetBundle = assetBundle;

            swordHitSoundEvent = Content.CreateAndAddNetworkSoundEventDef("HenrySwordHit");

            CreateEffects();

            CreateProjectiles();
        }

        #region effects
        private static void CreateEffects()
        {
            CreateBombExplosionEffect();

            swordSwingEffect = _assetBundle.LoadEffect("HenrySwordSwingEffect", true);
            swordHitImpactEffect = _assetBundle.LoadEffect("ImpactHenrySlash");
        }

        private static void CreateBombExplosionEffect()
        {
            bombExplosionEffect = _assetBundle.LoadEffect("BombExplosionEffect", "HenryBombExplosion");

            if (!bombExplosionEffect)
                return;

            ShakeEmitter shakeEmitter = bombExplosionEffect.AddComponent<ShakeEmitter>();
            shakeEmitter.amplitudeTimeDecay = true;
            shakeEmitter.duration = 0.5f;
            shakeEmitter.radius = 200f;
            shakeEmitter.scaleShakeRadiusWithLocalScale = false;

            shakeEmitter.wave = new Wave
            {
                amplitude = 1f,
                frequency = 40f,
                cycleOffset = 0f
            };

        }
        #endregion effects

        #region projectiles
        private static void CreateProjectiles()
        {
            CreateBombProjectile();
            Content.AddProjectilePrefab(bombProjectilePrefab);
        }

        private static void CreateBombProjectile()
        {
            //highly recommend setting up projectiles in editor, but this is a quick and dirty way to prototype if you want
            //bombProjectilePrefab = Asset.CloneProjectilePrefab("CommandoGrenadeProjectile", "HenryBombProjectile");

            bombProjectilePrefab = Asset.CloneProjectilePrefab("MageFireboltBasic", "AnkyFireballProjectile");

            //remove their ProjectileImpactExplosion component and start from default values
            //UnityEngine.Object.Destroy(bombProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
            //ProjectileImpactExplosion bombImpactExplosion = bombProjectilePrefab.AddComponent<ProjectileImpactExplosion>();
            ProjectileImpactExplosion bombImpactExplosion = bombProjectilePrefab.GetComponent<ProjectileImpactExplosion>();

            bombImpactExplosion.blastRadius = 16f;
            bombImpactExplosion.blastDamageCoefficient = 1f;
            bombImpactExplosion.falloffModel = BlastAttack.FalloffModel.None;
            bombImpactExplosion.destroyOnEnemy = true;
            bombImpactExplosion.lifetime = 12f;
            bombImpactExplosion.impactEffect = bombExplosionEffect;
            bombImpactExplosion.lifetimeExpiredSound = Content.CreateAndAddNetworkSoundEventDef("HenryBombExplosion");
            bombImpactExplosion.timerAfterImpact = true;
            //bombImpactExplosion.lifetimeAfterImpact = 0.1f;
            bombImpactExplosion.lifetimeAfterImpact = 0f;

            ProjectileController bombController = bombProjectilePrefab.GetComponent<ProjectileController>();

            //if (_assetBundle.LoadAsset<GameObject>("HenryBombGhost") != null)
            //    bombController.ghostPrefab = _assetBundle.CreateProjectileGhostPrefab("HenryBombGhost");
            
            bombController.startSound = "";


            //GameObject atg = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/MissileProjectile.prefab").WaitForCompletion();
            //GameObject missileGhost = R2API.PrefabAPI.InstantiateClone(atg, "AnkySpikeProjectile");

            GameObject atg = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/BeetleGuard/Sunder.prefab").WaitForCompletion();
            GameObject missileGhost = R2API.PrefabAPI.InstantiateClone(atg, "AnkySpikeProjectile");
            spikeProjectilePrefab = missileGhost;
            ProjectileOverlapAttack projectileOverlapAttack = spikeProjectilePrefab.GetComponent<ProjectileOverlapAttack>();
            projectileOverlapAttack.forceVector.z = 1400f;

            UnityEngine.Object.Destroy(spikeProjectilePrefab.GetComponent<ProjectileCharacterController>());
            spikeProjectilePrefab.AddComponent<SpikeController>();
            spikeProjectilePrefab.AddComponent<ProjectileTargetComponent>();
            spikeProjectilePrefab.GetComponent<ProjectileOverlapAttack>().resetInterval = 1f;


            fallProjectilePrefab = Asset.CloneProjectilePrefab("CommandoGrenadeProjectile", "AnkyFall");
            ProjectileImpactExplosion fireExplosion = fallProjectilePrefab.GetComponent<ProjectileImpactExplosion>();
            fireExplosion.lifetime = 0f;
            //fireExplosion.dotIndex = DotController.DotIndex.Burn;
            //fireExplosion.dotDuration = 5f;
            //fireExplosion.applyDot = true;
            fireExplosion.blastRadius = 16f;
            fireExplosion.blastImpactEffect = GlobalEventManager.CommonAssets.igniteOnKillExplosionEffectPrefab;
            fireExplosion.bonusBlastForce = Vector3.zero;
            fireExplosion.falloffModel = BlastAttack.FalloffModel.None;
            ProjectileDamage damage = fallProjectilePrefab.GetComponent<ProjectileDamage>();
            damage.damageType = DamageType.Generic;



        }
        #endregion projectiles
    }
}

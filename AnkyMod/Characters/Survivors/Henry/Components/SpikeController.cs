using EntityStates;
using RoR2;
using RoR2.Projectile;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace HenryMod.Survivors.Henry.Components
{
    internal class SpikeController : MonoBehaviour
    {
        private void Awake()
        {
            this.downVector = Vector3.down * 3f;
            this.projectileController = base.GetComponent<ProjectileController>();
            this.characterController = base.GetComponent<CharacterController>();
            this.teamFilter = base.GetComponent<TeamFilter>();
            this.targetComponent = base.GetComponent<ProjectileTargetComponent>();
        }

        private void FixedUpdate()
        {
            if (!this.targetComponent.target)
            {
                this.targetComponent.target = this.FindTarget();
            }
            else
            {
                HealthComponent component = this.targetComponent.target.GetComponent<HealthComponent>();
                if (component && !component.alive)
                {
                    this.targetComponent.target = this.FindTarget();
                }
            }

            
            if (!fly)
            {
                newDownVector = this.downVector;
            }
            else
            {
                newDownVector = Vector3.zero;
            }




            if (NetworkServer.active || this.projectileController.isPrediction)
            {
                if (targetComponent.target && timer >= startTime)
                {
                    Vector3 dir = (targetComponent.target.transform.position - transform.position).normalized;

                    Vector3 finalDir = dir + newDownVector;

                    this.characterController.Move(finalDir * (this.velocity * Time.fixedDeltaTime));
                }
                else
                {
                    this.characterController.Move((base.transform.forward + this.newDownVector) * (this.velocity * Time.fixedDeltaTime));
                    //this.characterController.Move((base.transform.forward + this.downVector) * (this.velocity * Time.fixedDeltaTime));
                }
            }
            if (NetworkServer.active)
            {
                this.timer += Time.fixedDeltaTime;
                if (this.timer > this.lifetime)
                {
                    UnityEngine.Object.Destroy(base.gameObject);
                }
            }
        }

        private Transform FindTarget()
        {
            this.search.searchOrigin = this.transform.position;
            this.search.searchDirection = this.transform.forward;
            this.search.teamMaskFilter.RemoveTeam(this.teamFilter.teamIndex);
            this.search.RefreshCandidates();
            HurtBox hurtBox = this.search.GetResults().FirstOrDefault<HurtBox>();
            if (hurtBox == null)
            {
                return null;
            }

            if (!fly && hurtBox.healthComponent.transform.GetComponent<RigidbodyMotor>())
            {
                return null;
            }
            if (fly && !hurtBox.healthComponent.transform.GetComponent<RigidbodyMotor>())
            {
                return null;
            }
            return hurtBox.transform;
        }
        private float startTime = 0.5f;

        public bool fly;
        private Vector3 newDownVector;

        private ProjectileTargetComponent targetComponent;
        private TeamFilter teamFilter;
        private BullseyeSearch search = new BullseyeSearch();

        // Token: 0x04007B6D RID: 31597
        private Vector3 downVector;

        // Token: 0x04007B6E RID: 31598
        public float velocity = 40f;

        // Token: 0x04007B6F RID: 31599
        public float lifetime = 4f;

        // Token: 0x04007B70 RID: 31600
        private float timer;

        // Token: 0x04007B71 RID: 31601
        private ProjectileController projectileController;

        // Token: 0x04007B72 RID: 31602
        private CharacterController characterController;

    }
}
using EntityStates;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace AnkyMod.Survivors.Anky.Components
{
    public class AnkyController : MonoBehaviour
    {
        public SkillStateOverrideData skillOverrides;
        private CharacterBody characterBody;

        public bool improved;

        public bool deflecting;

        public bool parry;

        private void Awake()
        {
            this.characterBody = base.GetComponent<CharacterBody>();       
        }

        private void Start()
        {
            //characterBody.inventory.GiveItemPermanent(HenryPlugin.ankyHelper, 1);
        }

        public void ClearSkillOverrides()
        {
            if (this.skillOverrides != null)
            {
                this.skillOverrides.ClearOverrides();
            }
            this.skillOverrides = null;
        }

    }
}
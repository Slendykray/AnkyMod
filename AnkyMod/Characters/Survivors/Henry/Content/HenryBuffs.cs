using RoR2;
using UnityEngine;

namespace HenryMod.Survivors.Henry
{
    public static class HenryBuffs
    {
        // armor buff gained during roll
        public static BuffDef armorBuff;

        public static BuffDef damageBuff;

        public static void Init(AssetBundle assetBundle)
        {
            armorBuff = Modules.Content.CreateAndAddBuff("HenryArmorBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.white,
                false,
                false);


            damageBuff = Modules.Content.CreateAndAddBuff("AnkyDamageBuff",
             LegacyResourcesAPI.Load<BuffDef>("BuffDefs/AttackSpeedOnCrit").iconSprite,
             Color.red,
             false,
             false);

        }
    }
}

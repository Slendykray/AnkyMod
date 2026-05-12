using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AnkyMod.Survivors.Anky
{
    public static class AnkyBuffs
    {
        // armor buff gained during roll
        public static BuffDef armorBuff;

        public static BuffDef damageBuff;

        public static BuffDef slowBuff;

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


            slowBuff = Modules.Content.CreateAndAddBuff("AnkySlowBuff",
             Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/texBuffSlow50Icon.tif").WaitForCompletion(),
             Color.red,
             false,
             false);

        }
    }
}

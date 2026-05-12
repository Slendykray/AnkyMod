using BepInEx;
using HenryMod.Survivors.Henry;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

//rename this namespace
namespace HenryMod
{
    //[BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(ItemAPI.PluginGUID)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    public class HenryPlugin : BaseUnityPlugin
    {
        // if you do not change this, you are giving permission to deprecate the mod-
        //  please change the names to your own stuff, thanks
        //   this shouldn't even have to be said
        public const string MODUID = "com.rob.HenryMod";
        public const string MODNAME = "HenryMod";
        public const string MODVERSION = "1.0.0";

        // a prefix for name tokens to prevent conflicts- please capitalize all name tokens for convention
        public const string DEVELOPER_PREFIX = "ROB";

        public static HenryPlugin instance;


        //public static ItemDef ankyHelper;

        public static ItemDef roarAddHealth;

        void Awake()
        {
            instance = this;

            AddItems();

            //easy to use logger
            Log.Init(Logger);

            // used when you want to properly set up language folders
            Modules.Language.Init();

            // character initialization
            new HenrySurvivor().Initialize();

            // make a content pack and add it. this has to be last
            new Modules.ContentPacks().Initialize();
          
        }

        void AddItems()
        {
            //ankyHelper = ScriptableObject.CreateInstance<ItemDef>();

            //ankyHelper.name = "ITEM_ANKYHELPER_NAME";
            //ankyHelper.nameToken = "ITEM_ANKYHELPER_NAME";
            //ankyHelper.pickupToken = "ITEM_ANKYHELPER_PICKUP";
            //ankyHelper.descriptionToken = "ITEM_ANKYHELPER_DESC";
            //ankyHelper.loreToken = "ITEM_ANKYHELPER_LORE";

            //ankyHelper.pickupIconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Core/texNullIcon.png").WaitForCompletion();

            //ankyHelper.canRemove = false;
            //ankyHelper.hidden = true;
            //ankyHelper.tags = new ItemTag[] { ItemTag.WorldUnique };
            //ankyHelper.deprecatedTier = ItemTier.NoTier;

            //var displayRules = new ItemDisplayRuleDict(null);

            //ItemAPI.Add(new CustomItem(ankyHelper, displayRules));


            roarAddHealth = ScriptableObject.CreateInstance<ItemDef>();

            roarAddHealth.name = "ITEM_ROARADDHEALTH_NAME";
            roarAddHealth.nameToken = "ITEM_ROARADDHEALTH_NAME";
            roarAddHealth.pickupToken = "ITEM_ROARADDHEALTH_PICKUP";
            roarAddHealth.descriptionToken = "ITEM_ROARADDHEALTH_DESC";
            roarAddHealth.loreToken = "ITEM_ROARADDHEALTH_LORE";

            roarAddHealth.pickupIconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Core/texNullIcon.png").WaitForCompletion();

            roarAddHealth.canRemove = false;
            roarAddHealth.hidden = true;
            roarAddHealth.tags = new ItemTag[] { ItemTag.WorldUnique };
            roarAddHealth.deprecatedTier = ItemTier.NoTier;

            var displayRules = new ItemDisplayRuleDict(null);

            ItemAPI.Add(new CustomItem(roarAddHealth, displayRules));
        }
    }
}

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


        public static ItemDef myItemDef;

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
            myItemDef = ScriptableObject.CreateInstance<ItemDef>();

            myItemDef.name = "ANKY_HELPER_NAME";
            myItemDef.nameToken = "ANKY_HELPER_NAME";
            myItemDef.pickupToken = "ANKY_HELPER_PICKUP";
            myItemDef.descriptionToken = "ANKY_HELPER_DESC";
            myItemDef.loreToken = "ANKY_HELPER_LORE";

            myItemDef.pickupIconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Core/texNullIcon.png").WaitForCompletion();

            myItemDef.canRemove = false;
            myItemDef.hidden = true;
            myItemDef.tags = new ItemTag[] { ItemTag.WorldUnique };
            myItemDef.deprecatedTier = ItemTier.NoTier;

            var displayRules = new ItemDisplayRuleDict(null);

            ItemAPI.Add(new CustomItem(myItemDef, displayRules));
        }
    }
}

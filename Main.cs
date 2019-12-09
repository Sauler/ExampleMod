using System;
using System.Reflection;
using Harmony;

namespace TestMod
{
    public class Main : Mod
    {
        public override void Activate()
        {
            var harmony = HarmonyInstance.Create("com.sauler.cms.testmod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override void Deactivate()
        {
            
        }

        public override ModInfo GetInfo()
        {
            return new ModInfo
            {
                Name = "Test Mod",
                Description = "Show popup with window name on window show",
                Author = "Rafał Babiarz",
                Version = "1.0.0"
            };
        }
    }
    
    [HarmonyPatch(typeof(UIManager))]
    [HarmonyPatch("Show")]
    [HarmonyPatch(new[] { typeof(string) })]
    class ShowPopupWithWindowNameOnShow
    {
        [HarmonyPrefix]
        public static void Prefix(UIManager __instance, string windowName)
        {
            __instance.ShowPopup("Opened window", windowName, PopupType.Normal);
        }
    }
}
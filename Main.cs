using System.Reflection;
using Harmony;

namespace ExampleMod
{
    public class Main : Mod
    {
        private HarmonyInstance _harmonyInstance;
        public override void Activate()
        {
            _harmonyInstance = HarmonyInstance.Create("com.sauler.cms.examplemod");
            _harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            
        }

        public override void Deactivate()
        {
            _harmonyInstance.UnpatchAll();
        }

        public override ModInfo GetInfo()
        {
            return new ModInfo
            {
                Name = "Example Mod",
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
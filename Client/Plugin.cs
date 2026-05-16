using System;
using acidphantasm_temporaryfixes.Patches;
using acidphantasm_temporaryfixes.VersionCheck;
using BepInEx;

namespace acidphantasm_temporaryfixes
{
    [BepInPlugin("com.acidphantasm.temporaryfixes", "acidphantasm-temporaryfixes", "4.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            if (!VersionChecker.CheckEftVersion(Logger, Info, Config))
            {
                throw new Exception($"Invalid EFT Version");
            }
            
            new FixKeylessDoorsPatch().Enable();
        }
    }
}
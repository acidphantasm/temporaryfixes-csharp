using System.Reflection;
using EFT;
using EFT.Interactive;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace acidphantasm_temporaryfixes.Patches;


/*
 * This entire patch is copy paste from Cj - I'm just putting it in 4.0
 * https://github.com/sp-tarkov/modules/commit/f1946b2288e8b99a3d4ba74f9e4896fadb7a001a
 */

public class FixKeylessDoorsPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(GameWorld), nameof(GameWorld.OnGameStarted));
    }

    [PatchPostfix]
    private static void Postfix(GameWorld __instance)
    {
        var doors = LocationScene.GetAllObjects<WorldInteractiveObject>();

        foreach (var door in doors)
        {
            // Fix Military checkpoint key because BSG cant assign a key to doors
            if (door.Id == "door_custom_multiScene_00000")
            {
                door.KeyId = "5913915886f774123603c392";
                break;
            }
        }
    }
}
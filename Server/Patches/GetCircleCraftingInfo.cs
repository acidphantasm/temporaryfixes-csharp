using System.Reflection;
using HarmonyLib;
using SPTarkov.Reflection.Patching;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Utils;

namespace _temporaryFixes.Patches;

public class PatchGetDirectRewardHashKey : AbstractPatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(CircleOfCultistService),"GetDirectRewardHashKey");
    }

    [PatchPrefix]
    public static bool Prefix(DirectRewardSettings directReward, ref string __result)
    {
        var hashUtil = ServiceLocator.ServiceProvider.GetRequiredService<HashUtil>();
        
        directReward.RequiredItems.Sort();
        directReward.Reward.Sort();

        var required = string.Join(",", directReward.RequiredItems);
        var reward = string.Join(",", directReward.Reward);
        // Key is sacrificed items separated by commas, a dash, then the rewards separated by commas
        var key = $"{{{required}-{reward}}}";

        __result = hashUtil.GenerateHashForData(HashingAlgorithm.MD5, key);
        return false;
    }
}
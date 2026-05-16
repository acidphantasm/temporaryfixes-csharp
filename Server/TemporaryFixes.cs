using _temporaryFixes.Patches;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;

namespace _temporaryFixes;

[Injectable(InjectionType.Singleton, TypePriority = OnLoadOrder.PreSptModLoader)]
public class TemporaryFixes() : IOnLoad
{
    public Task OnLoad()
    {
        new PatchGetDirectRewardHashKey().Enable();
        
        return Task.CompletedTask;
    }
}

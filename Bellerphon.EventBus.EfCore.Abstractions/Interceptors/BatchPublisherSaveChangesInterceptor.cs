using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Bellerphon.EventBus.EfCore.Abstractions.Interceptors;

public class BatchPublisherSaveChangesInterceptor : SaveChangesInterceptor
{
    
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        Console.WriteLine(result.HasResult);
        
        return base.SavingChanges(eventData, result);
    }
    
}
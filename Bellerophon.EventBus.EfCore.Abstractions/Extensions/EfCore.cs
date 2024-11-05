using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Bellerophon.EventBus.EfCore.Abstractions.Configs;
using Bellerophon.EventBus.EfCore.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bellerophon.EventBus.EfCore.Abstractions.Extensions;

public static class EfCore
{
    public static DbContextOptionsBuilder AddEventsBus(this DbContextOptionsBuilder optionsBuilder, EventBusConfigs configs)
    {
        var dbContextType = optionsBuilder.GetType().GetGenericArguments()[0];
        configs.Validate();
        optionsBuilder.UseModel(CreateEventBusModel(dbContextType, configs));
        return optionsBuilder;
    }

    private static IModel CreateEventBusModel(Type dbContextType, EventBusConfigs configs) => new ModelBuilder()
        .ConfigureEventBusEntities(configs)
        .ExecuteOnModelCreatingMethod(dbContextType)
        .FinalizeModel();
    
    private static ModelBuilder ConfigureEventBusEntities(this ModelBuilder modelBuilder, EventBusConfigs configs) => modelBuilder
        .ConfigureOutBoxEntity(configs);

    private static ModelBuilder ConfigureOutBoxEntity(this ModelBuilder modelBuilder, EventBusConfigs configs) =>
        modelBuilder.Entity<OutBoxMessage>(
            entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Body)
                    .IsRequired();
                
                entity.Property(m => m.Headers)
                    .IsRequired(false);
                
                entity.Property(m => m.IsSent)
                    .IsRequired();
                
                entity.Property(m => m.TryCount)
                    .IsRequired();

                entity.Property(m => m.ExpireDate)
                    .IsRequired(false);
                
                entity.Property(m => m.InsertDate)
                    .IsRequired();
            });
    
    private static ModelBuilder ExecuteOnModelCreatingMethod(this ModelBuilder modelBuilder, Type dbContextType)
    {
        DbContext context = GetInitializedDbContext(dbContextType);
        MethodInfo onModelCreatingMethodInfo = GetOnModelCreatingMethodInfo(dbContextType);
        onModelCreatingMethodInfo.Invoke(context, new object[] { modelBuilder });
        return modelBuilder;
    }
    
    private static MethodInfo GetOnModelCreatingMethodInfo(Type dbContextType)
    {
        MethodInfo? result = dbContextType.GetMethod("OnModelCreating", BindingFlags.Instance | BindingFlags.NonPublic);
        if (result is null) throw new Exception("OnModelCreating method was not found in DbContext");
        
        return result;
    }
    private static DbContext GetInitializedDbContext(Type dbContextType)
    {
        object context = RuntimeHelpers.GetUninitializedObject(dbContextType);
        if (context is null) throw new Exception($"{dbContextType.FullName} cannot be initialized. [reason: unknown]");
        
        if (context is DbContext dbContext) return dbContext;
        
        throw new Exception($"{dbContextType.FullName} is not a DbContext type.");
    }
}
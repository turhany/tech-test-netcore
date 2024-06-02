#### Task 1: 
###### Solution 1:
Add nuget package ("Microsoft.EntityFrameworkCore.Tools") for use "updated-database" ef core command to create and apply all migrations

###### Solution 2:
Add ef core database create codes in "Startup.cs > Configuraiton" method 

```csharp
using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    context.Database.Migrate();
}
```
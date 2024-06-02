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

#### Task 2:

###### Solution:
Give exact number to "Importance" enum for use order. Then add asc order query where get todo list items. ("ApplicationDbContextConvenience > SingleTodoList")
Eager loading order feature come with .Net 5.(https://learn.microsoft.com/en-us/ef/core/querying/related-data/eager#filtered-include)

###### Suggestion: (For now i dont do it just add as suggestion, if this task come to me for development i will ask product owner this question.)
For the future importance sub level request we can give big numbers to "Importance" enum members.

###### For Example
```csharp
public enum Importance
{
    Urgent = 5, //if we want add this level it will be easy
    High = 10, 
    Medium = 20,
    Low = 30,
}
```

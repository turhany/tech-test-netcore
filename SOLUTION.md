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

#### Task 3:

###### Solution:
Change fixed importance  mappign in "TodoItemEditFields" mappig code.

#### Task 4:

###### Solution:
For this situation we use MVC because of that i decide to use "DisplayNameArrtibute" this is not the best practice.
The usage attribute for ui naming means break SOLID  > S(single responsibility). Best practice is create a separate way to handle ui field names and store your names there(Seperation of concerns). For example create a resx(c# resource file) and put you naming there.

Changed Models
* TodoItemCreateFields
* TodoItemEditFields

Doc: https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.displaynameattribute?view=net-8.0
Doc: https://en.wikipedia.org/wiki/SOLID
Doc: https://en.wikipedia.org/wiki/Separation_of_concerns

#### Task 5:

###### Solution:
If we don't have plan to add paging for this page and my assumption this page can not will show million items because of that we can use javascipt for hide done items. This option help us to reduce server requests(server round trips).

###### Best Practice:
Add paging for this page and handle hide done item logic at server side. Dont expose logic to ui client.

#### Task 6:

###### Solution:
Update todo list query to get user assined todo lists.("ApplicationDbContextConvenience > RelevantTodoLists")

###### Suggestion:
* Need to add check for user only can update own items in other owners list. 

#### Task 7:

###### Solution:
Server side order flow refactored and developed.

Migration Codes For Rank:
* add-migration 20240602174500_AddRankFieldToTodoItem
* update-database

#### Task 8:

###### Solution:
Added get gravatar preferred user name method. This save data to inmemory cache. My previous task i think you can see my ef core usage because of that i dont add database save code.

Doc: https://docs.gravatar.com/api/profiles/json/

###### Best Practice:
For gravatar best practice is get image and profile data with async way and store them in database and cache and update gravatar data with a scheduled job.

#### Task 9:
I dont develop this tasks because my main expertise is backend. To develop this tasks i need the read, check, make some sample about mvc ui usage. Search on stackoverflow, watch videos to remember frontend and it will taken more time. I can'not complete this task in 5 hours.
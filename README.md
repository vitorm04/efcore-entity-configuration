# EF Core - Configuring data model

Suppose we are creating a service that will be responsible for managing customer information. We decided to use SQL Server as database, but we will create all tables and indexes using code first, by EF Core. It sounds easy, doesn't it? All we need to do is add the class to represent customers and EF Core will create the whole structure for us. See the `Customer` class below:

![image](https://user-images.githubusercontent.com/24477296/112676812-ac54e880-8e47-11eb-99d0-f55d5121aaff.png)

All we need to do now is create our context and define this class for a DbSet and EF Core will automatically create the table with the fields.

![image](https://user-images.githubusercontent.com/24477296/112678573-d8716900-8e49-11eb-9d64-742c3a2344ec.png)


Ok, we just need to start the application and we will have the following structure in the database

![antes mapeamento](https://user-images.githubusercontent.com/24477296/112678664-f76ffb00-8e49-11eb-86e4-cb49f44486bd.png)

But, look at the fields. We have `NVARCHAR (MAX)` for all types of `string` and the birthday is entered as the date and time. I think we have a problem here. By default, if we don't define the data type rules, EF Core will create it with a generic type according to the type of property. For example:
 - String : NVARCHAR(MAX)
 - Date : DateTime

In our case, we don't want these default data types, because they are not ideal for our business and `VARCHAR (MAX)` can degrade the speed of our queries. So the idea is  define the data type correctly.

In C# we have some options to configure our data model using code first, some of them:

- Using Data Annotations on your class 
- Using inline configuration inside the context
- Using specific classes to configure each domain entity

## Data Annotations
   
   Data annotations are the fastest way to add our data model rule, all information is attached to our `Customer`, and the table will follow these rules.It is most used when we have a small application with no data layer.
   
   ![image](https://user-images.githubusercontent.com/24477296/112679236-b0ced080-8e4a-11eb-9f7a-0bbea5825a53.png)

   
## Inline configuration inside the context
It is another way to define the structure of our table, in my opinion it is better than Data Annorations, because we are removing the overload of the domain entities and delegating it to the database layer. It will work very well on small projects, with few entities, but when we have a lot of them, we can adopt another strategy.

![image](https://user-images.githubusercontent.com/24477296/112681605-bda0f380-8e4d-11eb-87fe-97feb6440ed0.png)


## Specific configuration class
I particularly like to use it that way because it is easy to see and add new information. We will have a configuration class for each domain class. 
 
 ![image](https://user-images.githubusercontent.com/24477296/112680904-b7f6de00-8e4c-11eb-9682-b8bf0b55d180.png)

Each entity has a configuration class, like the one below:

![image](https://user-images.githubusercontent.com/24477296/112680984-d2c95280-8e4c-11eb-848a-2cd93fffc983.png)

And now, we just load the configuration class into our context, using the `ApplyConfigurationsFromAssembly` statement. It will search all configuration classes within our assembly context and apply the rules to the database.

![Sem título](https://user-images.githubusercontent.com/24477296/112755418-fd371f00-8fb6-11eb-9458-bb3073c66813.png)


## Result

Both ways will produce the same result, a table with specific rules for each field, as shown in the image below:

![depois](https://user-images.githubusercontent.com/24477296/112703228-e25c9180-8e74-11eb-8c28-b9a4d8207baa.png)



The `VARCHAR` fields now avoid unnecessary resource allocation by setting the maximum size. The Birthday field has also been improved, it is now a `Date` formmart, with no time information.


### What is the best option?

There is no correct answer, everything will depend on your situation. But on large projects, the 'Specific configuration class' can be powerful and provide a better organization.

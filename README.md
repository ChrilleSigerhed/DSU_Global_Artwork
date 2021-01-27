# DSU21_5

Koppla upp sig mot de lokala databaserna: 
- Radera "migrations"-mappen i solution explorer
- Klicka på Tools -> Nuget Package Manager -> Package Manager Console
- Skriv consolen och tryck enter efter varje rad: 

    add-migration "initial-create" -context ImageDbContext    (TRYCK ENTER)
    
    add-migration "initial-create" -context AuthDbContext     (TRYCK ENTER)
    
    update-database -context ImageDbContext                   (TRYCK ENTER)
    
    update-database -context AuthDbContext                    (TRYCK ENTER)
    

Ångra ändringarna som blivit i Team explorer/Git changes (så slipper man pusha upp onödig kod:) ) 

Koppla upp sig mot skarp databas:
- Gå in på SQL Server Object Explorer
- Högerklicka på SQL Server och välj Add SQL server
- Ange server name: dsvkurs.miun.se
- Ändra authentication till SQL server authentication
- User name och password finns i discordgruppen
- välj Database name i rullistan: grupp5


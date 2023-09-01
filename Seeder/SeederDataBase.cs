using System;
using webApi.Managers;

namespace webApi.Seeder;

public static class SeederDataBase
{
    public static string Seeder( )
    {
        try
        {
            using (DatabaseMysqlHandler databaseManager = new DatabaseMysqlHandler())
            {
               
            }
        }catch(Exception ex)
        {
            return $"Error : {ex.Message}";
        }
        return "Seeded";
    }
}

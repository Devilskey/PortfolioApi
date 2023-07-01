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
                databaseManager.SendImport("CREATE TABLE `admin` (\r\n  `AdminId` int(3) NOT NULL,\r\n  `AdminName` varchar(16) NOT NULL,\r\n  `AdminPassword` varchar(32) NOT NULL,\r\n  `AdminPhoneNumber` varchar(10) NOT NULL\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;");
                databaseManager.SendImport("CREATE TABLE `post` (\r\n  `postId` int(10) NOT NULL,\r\n  `PostTitle` varchar(32) NOT NULL,\r\n  `PostContent` longtext NOT NULL,\r\n  `PostTag` varchar(20) NOT NULL,\r\n  `thumbnail` mediumtext NOT NULL\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;");
                databaseManager.SendImport("INSERT INTO `admin` (`AdminId`, `AdminName`, `AdminPassword`, `AdminPhoneNumber`) VALUES\r\n(0, 'root', 'zlymc9E7NhGNVKfPE66wygEjg793HnE0IbTR/YQfU5o=', '');");
                databaseManager.SendImport("ALTER TABLE `admin`\r\n  ADD PRIMARY KEY (`AdminId`);");
                databaseManager.SendImport("ALTER TABLE `post`\r\n  ADD PRIMARY KEY (`postId`);");
                databaseManager.SendImport("ALTER TABLE `post`\r\n  MODIFY `postId` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=0;");
            }
        }catch(Exception ex)
        {
            return $"Error : {ex.Message}";
        }
        return "Seeded";
    }
}

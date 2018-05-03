using System;
using SQLite;

namespace StoryboardTables
{
    public class DBSingleton
    {
        private static SQLiteConnection db;

        private DBSingleton(){}

        public static SQLiteConnection DB{
            get{
                if ( db == null){
                    string dbPath = System.IO.Path.Combine(
     Environment.GetFolderPath(Environment.SpecialFolder.Personal),
     "users.db3");
                    db = new SQLiteConnection(dbPath);
                }
                return db;
            }
        }

    }
}

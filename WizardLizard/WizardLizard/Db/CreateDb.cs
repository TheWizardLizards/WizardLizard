using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

namespace WizardLizard.Db
{
    public class CreateDb
    {
        

        public void CreateDatabase()
        {
            if (!File.Exists(@"data.db"))
            {

            SQLiteConnection.CreateFile("data.db");

            SQLiteConnection con = new SQLiteConnection("Data source = data.db");
            con.Open();
            using (var reader = new SQLiteCommand("SELECT * FROM sqlite_master WHERE type='table'", con).ExecuteReader())
            {
             if(!reader.Read())
              {
                var str= @"
                CREATE TABLE spell(ID primary key, primaryspell integer not null, secondaryspell integer not null, shield integer not null);
                CREATE TABLE pet(ID primary key, health integer not null);
                CREATE TABLE character (ID integer primary key,spellID integer not null, level integer not null, petID integer not null,  name string not null, health integer not null, FOREIGN key(spellID) REFERENCES spell(ID), FOREIGN key(PetID) REFERENCES pet(ID));";
                    new SQLiteCommand(str, con).ExecuteNonQuery();

               }

            };
        }

    }
        
    }
}

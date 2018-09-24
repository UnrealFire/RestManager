using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

//Репозитарий таблицы добавок, класс добавок
namespace RestManager.DBModels
{
    public class DBAdditivesRep
    {
        //Класс добавок и объявление его таблицей
        [Table("Additives")]
        public class Additive
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }

            public string Название { get; set; }
            public int id_блюда { get; set; }
            public double Цена { get; set; }
        }

        //Репозитарий
        public class AdditivesRepository
        {
            SQLiteConnection database;
            public AdditivesRepository(string filename) //создание базы
            {
                string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
                database = new SQLiteConnection(databasePath);
                database.CreateTable<Additive>();
            }

            public IEnumerable<Additive> GetItems() //возвращает все строчки листом
            {
                return (from i in database.Table<Additive>() select i).ToList();
            }

            public Additive GetItem(int id) //возвращает строчку по id
            {
                return database.Get<Additive>(id);
            }

            public int DeleteItem(int id) //удаляет
            {
                return database.Delete<Additive>(id);
            }

            public bool Clear()
            {
                database.DeleteAll<Additive>();
                return true;
            }

            public int SaveItem(Additive item) //сохраняет строчку
            {
                if (item.Id != 0) //если объект существует (id>0) значит обновляет объект
                {
                    database.Update(item);
                    return item.Id;
                }
                else //если не существует (с id=0, значит добавляет в конец). для добавления в конец используется id=0.
                {
                    return database.Insert(item);
                }
            }
        }
    }
}

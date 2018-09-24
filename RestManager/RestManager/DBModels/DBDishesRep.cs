using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using XLabs.Forms.Controls; 

//Репозитарий и таблица блюд
namespace RestManager.DBModels
{
    public class DBDishesRep
    {
        //Таблица блюд
        [Table("Dishes")]
        public class Dish
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }

            public string Название { get; set; }
            public string Категория { get; set; }
            public double Цена { get; set; }
            public int Курс { get; set; }
            public int Калории { get; set; }
        }
        //Класс категорий
        public class Cat
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }

            public string Название { get; set; }
        }

        //Репозитарий таблицы блюдD
        public class DishesRepository
        {
            SQLiteConnection database;
            public DishesRepository(string filename) //создание базы
            {
                string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
                database = new SQLiteConnection(databasePath);
                database.CreateTable<Dish>();
            }

            public IEnumerable<Dish> GetItems() //возвращает все строчки листом
            {
                return (from i in database.Table<Dish>() select i).ToList();
            }

            public Dish GetItem(int id) //возвращает строчку по id
            {
                return database.Get<Dish>(id);
            }

            public int DeleteItem(int id) //удаляет
            {
                return database.Delete<Dish>(id);
            }

            public int SaveItem(Dish item) //сохраняет строчку
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

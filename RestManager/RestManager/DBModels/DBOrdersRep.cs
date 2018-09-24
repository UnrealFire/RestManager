using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace RestManager.DBModels
{
    public class DBOrdersRep
    {
        //Таблица и класс столов
        [Table("Tables")]
        public class Table
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            public int номерстола { get; set; }
        }
        //Класс Guest. Используется для хранения информации о всех гостях в текущем столе в List Guests и обо всех гостях в базе. В каждом госте содержится информация обо всех блюдах и их параметрах
        [Table("Guests")]
        public class Guest
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }

            public int номергостя { get; set; } //1;2;3;
            public string idблюд { get; set; } //5;7;1;
            public string idдобавок { get; set; } //5 2;N;2 4;
            public string курс { get; set; } //1;2;2;
            public string комментарии { get; set; } //комм;комм;комм;
            public string заказ { get; set; } //1;0;1;
            public int номерстола { get; set; } //1;2;3
        }   

        //Класс GF, используется для вывода в Листинг заказа. Формируется в коде из Guests. Объект GuestFinal содержит параметры одного блюда. Не имеет таблицы в базе
        public class GuestFinal
        {
            public int номерблюдавзаказе { get; set; }
            public int idблюда { get; set; }
            public string названиеблюда { get; set; }
            public string названиедобавок { get; set; } //Сметана;Гренки;
            public int номеркурса { get; set; }
            public int номергостя { get; set; }
            public double цена { get; set; }
            public int номерстола { get; set; }
            public string комментарий { get; set; }
            public int калории { get; set; }
            public int заказ { get; set; } //1 или 0
        }

        //Репозитарий таблицы Tables
        public class TablesRepository
        {
            SQLiteConnection database;
            public TablesRepository(string filename) //создание базы
            {
                string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
                database = new SQLiteConnection(databasePath);
                database.CreateTable<Table>();
            }

            public IEnumerable<Table> GetItems() //возвращает все строчки листом
            {
                return (from i in database.Table<Table>() select i).ToList();
            }

            public Table GetItem(int id) //возвращает строчку по id
            {
                return database.Get<Table>(id);
            }

            public int DeleteItem(int id) //удаляет
            {
                return database.Delete<Table>(id);
            }

            public int SaveItem(Table item) //сохраняет строчку
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
        //Репозитарий таблицы Guests
        public class GuestsRepository
        {
            private SQLiteConnection database;
            public GuestsRepository(string filename) //создание базы
            {
                string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
                database = new SQLiteConnection(databasePath);
                database.CreateTable<Guest>();
            }

            public IEnumerable<Guest> GetItems() //возвращает все строчки листом
            {
                return (from i in database.Table<Guest>() select i).ToList();
            }

            public bool Clear()
            {
                database.DeleteAll<Guest>();
                return true;
            }

            public Guest GetItem(int id) //возвращает строчку по id
            {
                return database.Get<Guest>(id);
            }

            public int DeleteItem(int id) //удаляет
            {
                return database.Delete<Guest>(id);
            }

            public int SaveItem(Guest item) //сохраняет строчку
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


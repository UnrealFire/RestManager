using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestManager.DBModels;
using Xamarin.Forms;

namespace RestManager
{
    public partial class App : Application
    {
        //Определение путей подключения для базы данных. Нужна для инициализации в разных устройствах
        public const string DATABASE_NAME = "Rest.db";
        //public static DBDishesRep.DishesRepository Dishesdatabase;
        //public static DBDishesRep.DishesRepository DishesDatabase
        //{
        //    get
        //    {
        //        if (DishesDatabase == null)
        //        {
        //            Dishesdatabase = new DBDishesRep.DishesRepository(DATABASE_NAME);
        //        }
        //        return Dishesdatabase;
        //    }
        //}
        public static DBOrdersRep.GuestsRepository Guestsdatabase;
        public static DBOrdersRep.GuestsRepository GuestsDatabase
        {
            get
            {
                if (Guestsdatabase != null)
                {
                    return Guestsdatabase;
                }
                return Guestsdatabase =  new DBOrdersRep.GuestsRepository(DATABASE_NAME);
            }
        }

        public static DBOrdersRep.TablesRepository Tablesdatabase;
        public static DBOrdersRep.TablesRepository TablesDatabase
        {
            get
            {
                if (Tablesdatabase != null)
                {
                    return Tablesdatabase;
                }
                return Tablesdatabase = new DBOrdersRep.TablesRepository(DATABASE_NAME);
            }
        }

        public static DBAdditivesRep.AdditivesRepository Adddatabase;
        public static DBAdditivesRep.AdditivesRepository AddDatabase
        {
            get
            {
                if (Adddatabase != null)
                {
                    return Adddatabase;
                }
                return Adddatabase = new DBAdditivesRep.AdditivesRepository(DATABASE_NAME);
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new Pages.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

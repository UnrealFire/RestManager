using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestManager.DBModels;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace RestManager.Pages
{
    
    public partial class MainPage : ContentPage
    {
        //класс группировки по гостям 
        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Name { get; private set; }
            public Grouping(K name, IEnumerable<T> items)
            {
                Name = name;
                foreach (T item in items)
                    Items.Add(item);
            }
        }
        
        // объявление коллекций и листингов
        public ObservableCollection<Grouping<int, DBOrdersRep.GuestFinal>> GuestsGroups { get; set; }
        public ObservableCollection<DBOrdersRep.Guest> GuestsOC = new ObservableCollection<DBOrdersRep.Guest>();
        public ObservableCollection<DBOrdersRep.Guest> GuestsFinal { get; set; }
        public ObservableCollection<DBOrdersRep.Guest> Categories { get; set; }

        public List<DBDishesRep.Dish> Dishes { get; set; }
        public List<DBOrdersRep.Table> Tables { get; set; }
        public List<DBOrdersRep.Guest> Guests { get; set; }
        public List<DBAdditivesRep.Additive> Additives { get; set; }
        public List<DBOrdersRep.GuestFinal> GuestsFinalList { get; set; }
        public List<DBDishesRep.Cat> CategoriesList { get; set; }
        public List<DBDishesRep.Dish> DishCat { get; set; }
        public List<DBDishesRep.Dish> DishSch { get; set; }
        public List<DBAdditivesRep.Additive> AdditivesTemp { get; set; }

        //теги для удобства переноса значений
        public int Tag = 0;
        public DBDishesRep.Dish Tag2 { get; set; }
        public DBOrdersRep.Guest Tag3 { get; set; }
        public DBOrdersRep.GuestFinal Tag4 { get; set; }
        public string Tag5;


        //класс обновления листинга добавок добавок
        public void ЗаполнениеДобавок()
        {
            Additives = new List<DBAdditivesRep.Additive>();
            Additives.Clear();
            Additives = new List<DBAdditivesRep.Additive>
            {
                new DBAdditivesRep.Additive() {Id = -1, Название= "Без добавок", id_блюда= -1, Цена= 0 },
                //Пельмени
                new DBAdditivesRep.Additive() {Id = 1, Название= "С бульоном", id_блюда= 14, Цена= 0 },
                new DBAdditivesRep.Additive() {Id = 2, Название= "Без зелени", id_блюда= 14, Цена= 0 },
                new DBAdditivesRep.Additive() {Id = 3, Название= "Сметана", id_блюда= 14, Цена= 70 },
                new DBAdditivesRep.Additive() {Id = 4, Название= "Кетчуп", id_блюда= 14, Цена= 0 },
                new DBAdditivesRep.Additive() {Id = 5, Название= "Соевый соус", id_блюда= 14, Цена= 0 },
                //Шницель
                new DBAdditivesRep.Additive() {Id = 6, Название= "Без соуса", id_блюда= 15, Цена= 0 },
                new DBAdditivesRep.Additive() {Id = 7, Название= "Соус отдельно", id_блюда= 15, Цена= 0 },
                new DBAdditivesRep.Additive() {Id = 8, Название= "Без салата", id_блюда= 15, Цена= 0 },
                new DBAdditivesRep.Additive() {Id = 9, Название= "Без гарнира", id_блюда= 15, Цена= 0 },
                new DBAdditivesRep.Additive() {Id = 10, Название= "Без панировки", id_блюда= 15, Цена= 0 },
                //Уха
                new DBAdditivesRep.Additive() {Id = 11, Название= "Без картофеля", id_блюда= 23, Цена= 0 },
                new DBAdditivesRep.Additive() {Id = 12, Название= "Без зелени", id_блюда= 23, Цена= 0 },
                new DBAdditivesRep.Additive() {Id = 13, Название= "Без пассировки", id_блюда= 23, Цена= 0 },
                new DBAdditivesRep.Additive() {Id = 14, Название= "Хлеб", id_блюда= 23, Цена= 50 },
                new DBAdditivesRep.Additive() {Id = 15, Название= "Без рыбы", id_блюда= 23, Цена= 0 },
            };
            
            

            App.AddDatabase.Clear();
            foreach (var additive in Additives)
            {

                App.AddDatabase.SaveItem(additive);
            }
        }

        //класс страницы
        public MainPage()
        {

            InitializeComponent();

            
            //Заполние листинга добавок
            Dishes = new List<DBDishesRep.Dish>
            {
                //кофе
                new DBDishesRep.Dish() {Id = 0, Название= "Эспрессо", Категория= "Кофе", Цена= 150, Курс = 1, Калории = 9},
                new DBDishesRep.Dish() {Id = 1, Название= "Доппио", Категория= "Кофе", Цена= 290, Курс = 1, Калории = 12},
                new DBDishesRep.Dish() {Id = 2, Название= "Капучино", Категория= "Кофе", Цена= 330, Курс = 1, Калории = 72},
                new DBDishesRep.Dish() {Id = 3, Название= "Гранде", Категория= "Кофе", Цена= 450, Курс = 1, Калории = 250},
                new DBDishesRep.Dish() {Id = 4, Название= "Латте 300мл", Категория= "Кофе", Цена= 400, Курс = 1, Калории = 355},
                new DBDishesRep.Dish() {Id = 5, Название= "Латте 400мл", Категория= "Кофе", Цена= 490, Курс = 1, Калории = 440},
                new DBDishesRep.Dish() {Id = 6, Название= "Американо", Категория= "Кофе", Цена= 290, Курс = 1, Калории = 10},
                //не кофе
                new DBDishesRep.Dish() {Id = 7, Название= "Манго-маракуйя 300мл", Категория= "Не кофе", Цена= 390, Курс = 1, Калории = 100},
                new DBDishesRep.Dish() {Id = 8, Название= "Манго-маракуйя 500мл", Категория= "Не кофе", Цена= 490, Курс = 1, Калории = 156},
                new DBDishesRep.Dish() {Id = 9, Название= "Фреш грин 350мл", Категория= "Не кофе", Цена= 390, Курс = 1, Калории = 98},
                new DBDishesRep.Dish() {Id = 10, Название= "Фреш грин 500мл", Категория= "Не кофе", Цена= 490, Курс = 1, Калории = 150},
                new DBDishesRep.Dish() {Id = 11, Название= "Ла нинья", Категория= "Не кофе", Цена= 420, Курс = 1, Калории = 200},
                new DBDishesRep.Dish() {Id = 12, Название= "Фруктовый микс 350мл", Категория= "Не кофе", Цена= 390, Курс = 1, Калории = 178},
                new DBDishesRep.Dish() {Id = 13, Название= "Фруктовый микс 500мл", Категория= "Не кофе", Цена= 490, Курс = 1, Калории = 267},
                //горячее
                new DBDishesRep.Dish() {Id = 14, Название= "Пельмени", Категория= "Горячее", Цена= 590, Курс = 1, Калории = 480},
                new DBDishesRep.Dish() {Id = 15, Название= "Шницель", Категория= "Горячее", Цена= 1100, Курс = 1, Калории = 800},
                new DBDishesRep.Dish() {Id = 16, Название= "Курица зелёный карри", Категория= "Горячее", Цена= 690, Курс = 1, Калории = 570},
                new DBDishesRep.Dish() {Id = 17, Название= "Голубцы", Категория= "Горячее", Цена= 590, Курс = 1, Калории = 790},
                new DBDishesRep.Dish() {Id = 18, Название= "Ризотто", Категория= "Горячее", Цена= 590, Курс = 1, Калории = 500},
                new DBDishesRep.Dish() {Id = 19, Название= "Печень по-берлински", Категория= "Горячее", Цена= 890, Курс = 1, Калории = 700},
                new DBDishesRep.Dish() {Id = 20, Название= "Утка по-пекински", Категория= "Горячее", Цена= 1200, Курс = 1, Калории = 640},
                new DBDishesRep.Dish() {Id = 21, Название= "Цыпленок с черным трюфелем", Категория= "Горячее", Цена= 590, Курс = 1, Калории = 470},
                //супы
                new DBDishesRep.Dish() {Id = 22, Название= "Домашняя лапша", Категория= "Супы", Цена= 530, Курс = 1, Калории = 450},
                new DBDishesRep.Dish() {Id = 23, Название= "Уха", Категория= "Супы", Цена= 590, Курс = 1, Калории = 490},
                new DBDishesRep.Dish() {Id = 24, Название= "Грибной", Категория= "Супы", Цена= 490, Курс = 1, Калории = 380},
                new DBDishesRep.Dish() {Id = 25, Название= "Бессарабский", Категория= "Супы", Цена= 670, Курс = 1, Калории = 550},
                new DBDishesRep.Dish() {Id = 26, Название= "Борщ", Категория= "Супы", Цена= 530, Курс = 1, Калории = 630},
                new DBDishesRep.Dish() {Id = 27, Название= "Свекольник", Категория= "Супы", Цена= 490, Курс = 1, Калории = 380},
                new DBDishesRep.Dish() {Id = 28, Название= "Щавелевый", Категория= "Супы", Цена= 430, Курс = 1, Калории = 330},
                //салаты
                new DBDishesRep.Dish() {Id = 29, Название= "Рио-Рита", Категория= "Салаты и закуски", Цена= 360, Курс = 1, Калории = 370},
                new DBDishesRep.Dish() {Id = 30, Название= "Цезарь", Категория= "Салаты и закуски", Цена= 480, Курс = 1, Калории = 350},
                new DBDishesRep.Dish() {Id = 31, Название= "Оливье", Категория= "Салаты и закуски", Цена= 530, Курс = 1, Калории = 520},
                new DBDishesRep.Dish() {Id = 32, Название= "Бора-Бора", Категория= "Салаты и закуски", Цена= 430, Курс = 1, Калории = 310},
                new DBDishesRep.Dish() {Id = 33, Название= "Бакинский", Категория= "Салаты и закуски", Цена= 480, Курс = 1, Калории = 430},
                new DBDishesRep.Dish() {Id = 34, Название= "Хумус", Категория= "Салаты и закуски", Цена= 350, Курс = 1, Калории = 290},
                new DBDishesRep.Dish() {Id = 35, Название= "Тар-Тар тунец", Категория= "Салаты и закуски", Цена= 490, Курс = 1, Калории = 330},
                new DBDishesRep.Dish() {Id = 36, Название= "Тар-Тар телятина", Категория= "Салаты и закуски", Цена= 580, Курс = 1, Калории = 300},
                //десерт
                new DBDishesRep.Dish() {Id = 37, Название= "Любит - не любит", Категория= "Десерты", Цена= 430, Курс = 1, Калории = 320},
                new DBDishesRep.Dish() {Id = 38, Название= "Белый лес", Категория= "Десерты", Цена= 290, Курс = 1, Калории = 430},
                new DBDishesRep.Dish() {Id = 39, Название= "Сердолик", Категория= "Десерты", Цена= 350, Курс = 1, Калории = 510},
                new DBDishesRep.Dish() {Id = 40, Название= "Капризе", Категория= "Десерты", Цена= 510, Курс = 1, Калории = 290},
                new DBDishesRep.Dish() {Id = 41, Название= "Счастье", Категория= "Десерты", Цена= 340, Курс = 1, Калории = 380},
                new DBDishesRep.Dish() {Id = 42, Название= "Сметанник", Категория= "Десерты", Цена= 460, Курс = 1, Калории = 530},
                new DBDishesRep.Dish() {Id = 43, Название= "Бьянка", Категория= "Десерты", Цена= 520, Курс = 1, Калории = 430},
                new DBDishesRep.Dish() {Id = 44, Название= "Кимура", Категория= "Десерты", Цена= 280, Курс = 1, Калории = 290},
                //чай
                new DBDishesRep.Dish() {Id = 45, Название= "Сбитень 270мл", Категория= "Чаи", Цена= 280, Курс = 1, Калории = 50},
                new DBDishesRep.Dish() {Id = 46, Название= "Сбитень 500мл", Категория= "Чаи", Цена= 380, Курс = 1, Калории = 100},
                new DBDishesRep.Dish() {Id = 47, Название= "Шиповник 230мл", Категория= "Чаи", Цена= 280, Курс = 1, Калории = 40},
                new DBDishesRep.Dish() {Id = 48, Название= "Шиповник 500мл", Категория= "Чаи", Цена= 380, Курс = 1, Калории = 80},
                new DBDishesRep.Dish() {Id = 49, Название= "Ассам", Категория= "Чаи", Цена= 350, Курс = 1, Калории = 30},
                new DBDishesRep.Dish() {Id = 50, Название= "Дарджилинг", Категория= "Чаи", Цена= 350, Курс = 1, Калории = 80},
                new DBDishesRep.Dish() {Id = 51, Название= "Жасмин", Категория= "Чаи", Цена= 350, Курс = 1, Калории = 50},
                new DBDishesRep.Dish() {Id = 52, Название= "Пуэр", Категория= "Чаи", Цена= 350, Курс = 1, Калории = 60}
            };
            ЗаполнениеДобавок();

            //Заполнение категорий
            CategoriesList = new List<DBDishesRep.Cat>
            {
                new DBDishesRep.Cat() {Id = 0, Название= "Кофе"},
                new DBDishesRep.Cat() {Id = 1, Название= "Не кофе"},
                new DBDishesRep.Cat() {Id = 2, Название= "Морс и фреш"},
                new DBDishesRep.Cat() {Id = 3, Название= "Чаи"},
                new DBDishesRep.Cat() {Id = 4, Название= "Салаты и закуски"},
                new DBDishesRep.Cat() {Id = 5, Название= "Супы"},
                new DBDishesRep.Cat() {Id = 6, Название= "Рыбные блюда"},
                new DBDishesRep.Cat() {Id = 7, Название= "Горячее"},
                new DBDishesRep.Cat() {Id = 8, Название= "Десерты"}

            };

            //Другие листинги
            DishCat = new List<DBDishesRep.Dish> {};
            DishSch = new List<DBDishesRep.Dish> { };

            AdditivesTemp = new List<DBAdditivesRep.Additive> {};

            Tables = new List<DBOrdersRep.Table>{};

            GuestsFinalList = new List<DBOrdersRep.GuestFinal> { };

            Guests = new List<DBOrdersRep.Guest> {};

            //Открытие списка столов
            ButtonСтол_OnClicked(this, null);
            this.BindingContext = this;

    }

        //кнопка Столы, отобразить список столов
        private void ButtonСтол_OnClicked(object sender, EventArgs e)
        {
            //Отображение
            VisibleMethod(true,false,false,false,false,false,false,false, false);
            CatList.ItemsSource = CategoriesList;

            //Заполнение из бд
            Tables.Clear();
            if (App.TablesDatabase.GetItems().Any())
            {
                Tables.AddRange(App.TablesDatabase.GetItems());
            }

            //Сохранение в бд гостей и очистка списка гостей
            if (Guests.Count != 0)
            {
                foreach (DBOrdersRep.Guest guest in Guests)
                {
                    App.GuestsDatabase.SaveItem(guest);
                }
            }
            Guests.Clear();
            foreach (var dish in Dishes)
            {
                dish.Курс = 1;
            }

            //Обновление содержимого
            tablesList.ItemsSource = null;
            tablesList.ItemsSource = Tables;
        }

        //Функция заполнения или обновления списка заказа (стола)
        public void GuestsRefresh()
        {
            GuestsFinalList.Clear();

            //Объявление массивов
            string[] DishesIDMass;
            string[] КурсыIDMass;
            string[] ДобавкиMass;
            string[] ДобавкиMassБлюдо;
            string[] КоммMass;
            string[] ЗаказMass;

            //Для каждого гостя
            foreach (var guest in Guests)
            {
                int н_бл_взаказе = 0;

                //Перевод строки из БД в массивы (см класс БД)
                DishesIDMass = guest.idблюд.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                КурсыIDMass = guest.курс.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                ДобавкиMass = guest.idдобавок.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries); 
                КоммMass = guest.комментарии.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                ЗаказMass = guest.заказ.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                //Поиск совпадений по блюдам
                foreach (var dish in Dishes) 
                {
                    for (int i = 1; i <= DishesIDMass.Length; i++)
                    {
                        if (dish.Id == Convert.ToInt32(DishesIDMass[i - 1]))
                        {
                            //Найдено
                            н_бл_взаказе++;
                            //Добавление в листинг параметров блюда. Есть 2 листинга - Guests и GuestsFinal. Первый содержит в себе объекты как они лежат в БД, второй уже готовые к выводу
                            GuestsFinalList.Add(new DBOrdersRep.GuestFinal { idблюда = dish.Id, названиеблюда = dish.Название, номергостя = guest.номергостя, номеркурса = Convert.ToInt32(КурсыIDMass[i-1]), цена = dish.Цена, названиедобавок = "", номерблюдавзаказе = н_бл_взаказе, номерстола = guest.номерстола, комментарий = "", заказ = 0, калории = dish.Калории});
                            //Добавление в листинг добавок
                            if (ДобавкиMass.Length >0)
                            {
                                ДобавкиMassБлюдо = ДобавкиMass[GuestsFinalList[GuestsFinalList.Count - 1].номерблюдавзаказе - 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var s in ДобавкиMassБлюдо)
                                {
                                    foreach (var additive in Additives)
                                    {
                                        if (s == additive.Id.ToString())
                                        {
                                            GuestsFinalList[GuestsFinalList.Count - 1].названиедобавок +=
                                                additive.Название + ";";
                                        }
                                    }
                                }                            
                            }

                            //Добавление в листинг комментария
                            if (КоммMass.Length > 0)
                            {

                                GuestsFinalList[GuestsFinalList.Count - 1].комментарий =
                                    КоммMass[GuestsFinalList[GuestsFinalList.Count - 1].номерблюдавзаказе-1];

                            }

                            //Добавление информации о статусе блюда. Заказано или нет
                            if (ЗаказMass.Length >0)
                            {
                                GuestsFinalList[GuestsFinalList.Count - 1].заказ =
                                    Convert.ToInt32(
                                        ЗаказMass[GuestsFinalList[GuestsFinalList.Count - 1].номерблюдавзаказе - 1]);
                            }
                        }
                    }
                }
            }

            //Группировка для вывода
            var groups = GuestsFinalList.GroupBy(p => p.номергостя).Select(g => new Grouping<int, DBOrdersRep.GuestFinal>(g.Key, g));
            GuestsGroups = new ObservableCollection<Grouping<int, DBOrdersRep.GuestFinal>>(groups);
            //Обновление содержимого
            ordersList.ItemsSource = null;
            ordersList.ItemsSource = GuestsGroups;

            this.BindingContext = this;
        }

        //Выбор стола в списке столов
        private void TablesList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            VisibleMethod(false,true,true,false,false,true,true,false,false);
            CatList.IsEnabled = true;

            //Заголовок
            var tItem = e.Item as DBOrdersRep.Table;
            if (e.Item != null)
            {
                LabelN_TB.Text = "Стол " + tItem.номерстола.ToString();
            }

            //Получение списка из БД
            var GItems = App.GuestsDatabase.GetItems();
            
            Guests.Clear();
            if (GItems.Any()) //если не пустой
            {
                foreach (var guest in GItems) //то для каждого элемента guest в коллекции
                {
                    if (guest.номерстола == tItem.номерстола) //поиск совпадений по столам
                    {
                        Guests.Add(guest); //добавление при совпадении
                    }
                }
            }
            //Если нет гостей в столе то добавляет
            if (!Guests.Any()) Guests.Add(new DBOrdersRep.Guest { номерстола = tItem.номерстола, номергостя = 1, idблюд = "", idдобавок = "", курс = "", комментарии = "", заказ = ""});

            //Каждое изменение записывается в класс Guests (листинг) и после этого требуется преобразовать данные для отображения. Вызов функции. Суть в том что Guests хранится в БД.
            GuestsRefresh();

            //Обновление списка заказа
            ordersList.ItemsSource = GuestsGroups;
        }

        //Добавление нового гостя
        private void ButtonГость_OnClicked(object sender, EventArgs e)
        {
            //Добавление
            if (ordersList.IsVisible)
            {
                Guests.Add(new DBOrdersRep.Guest {номергостя = Guests.Count+1, idблюд = "", idдобавок = "", курс = "", комментарии = "", номерстола = Guests.ElementAt(0).номерстола, заказ = ""});
            }

            //Преобразование
            GuestsRefresh();

            //Обновление
            ordersList.ItemsSource = GuestsGroups;
        }

        //Выбор блюда из меню без добавок или с ними
        async void DishAdd_OnTapped(object sender, ItemTappedEventArgs e)
        {
            //Получаем объект блюда
            DBDishesRep.Dish dishItem; 
            string actionГость = "";

            //Если таг=1 то идет передача информации о блюде и выборе гостя (был выбран в классе добавления добавок с блюдом). 
            if (Tag == 1)
            {
                dishItem = Tag2;
                actionГость = Tag5;
            }
            else //Иначе добавление блюда без добавок, выбор гостя
            {
                dishItem = e.Item as DBDishesRep.Dish;
                actionГость = await DisplayActionSheet("Выберите гостя", "Отмена", null, this.Guests.Select(guest => guest.номергостя.ToString()).ToArray()); //выбор гостя к кому добавить блюдо
            }


            //Если не выбрана отмена у выбора гостя то идет заполнение блюда в бд к выбранному гостю
            if (actionГость != "Отмена")
            {
                Guests[Convert.ToInt32(actionГость) - 1].idблюд += dishItem.Id.ToString() + ";";

                Guests[Convert.ToInt32(actionГость) - 1].курс += 1 + ";";

                Guests[Convert.ToInt32(actionГость) - 1].idдобавок += "-1;";

                Guests[Convert.ToInt32(actionГость) - 1].комментарии += "Нет;";

                Guests[Convert.ToInt32(actionГость) - 1].заказ += "0;";
            }

            //Преобразование и обновление
            GuestsRefresh();
        }

        //Удаление добавок, выбор добавки для удаления
        async void ButtonDelДобавки_OnClicked(object sender, EventArgs e)
        {
            //Получение объекта блюда в заказе
            var button = sender as Button;
            var GF = button.BindingContext as DBOrdersRep.GuestFinal;


            string[] ДобMass = Guests[GF.номергостя-1].idдобавок.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries); //формируем массив добавок по каждому блюду
            string[] ДобMassБлюдо = ДобMass[GF.номерблюдавзаказе - 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); //формируем массив добавок по текущему блюду
            string actionDelДоб = "";

            if (ДобMassБлюдо.Length == 1 && ДобMassБлюдо[0] == "-1") //при отсутствии добавок сообщение об ошибке
            {
                await DisplayAlert("Ошибка", "Добавок нет", "ОК");
            }
            else //Если они есть то формируем список добавок
            {
                for (int i = 0; i < ДобMassБлюдо.Length; i++)
                {
                    ДобMassБлюдо[i] = (i+1) + ": " + Additives[Convert.ToInt32(ДобMassБлюдо[i])].Название;
                }
                actionDelДоб = await DisplayActionSheet("Удаление добавки", "Отмена", null, ДобMassБлюдо);

                //Передаем на обработку. Необходимо разделение из за требования асинхронного выполнения при отображении списка выбора. 
                ОбработкаВыбораДобавкиDel(actionDelДоб,ДобMass,ДобMassБлюдо,GF);
            }
        }

        //Удаление добавок, обработка
        private void ОбработкаВыбораДобавкиDel(string actionDelДоб, string[] ДобMass, string[] ДобMassБлюдо, DBOrdersRep.GuestFinal GF)
        {
            //Если выбрана отмена то завершает функцию
            if (actionDelДоб == "Отмена")
            {
                return;
            }
            //Иначе добавляет добавку в текущий объект Guest
            else
            {
                int index = Convert.ToInt32(actionDelДоб[0].ToString()); //получаем номер выбранной позиции 

                ДобMassБлюдо = ДобMass[GF.номерблюдавзаказе - 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); //формируем массив добавок по текущему блюду

                ДобMass[GF.номерблюдавзаказе - 1] = ""; //очищаем строку массива с добавками по блюдам с нужным блюдом

                for (int i = 0; i < ДобMassБлюдо.Length; i++) //добавляем из массива по блюду добавки в строку массива по блюдам, без выбранной позиции
                {
                    if (i != index - 1)
                    {
                        ДобMass[GF.номерблюдавзаказе - 1] += ДобMassБлюдо[i] + " ";
                    }
                }

                for (int i = 0; i < ДобMass.Length; i++)
                {
                    if (ДобMass[i] == "" || ДобMass[i] == " ")
                    {
                        ДобMass[i] = "-1";
                    }
                }

                Guests[GF.номергостя - 1].idдобавок = ""; //очищаем добавки выбранного гостя
                foreach (var s in ДобMass) //добавляем  к выбранному гостю массив добавок по блюдам
                {
                    Guests[GF.номергостя - 1].idдобавок += s + ";";
                }

                GuestsRefresh(); //Преобразуем и обновляем
            }
        }

        //Выбор блюда с добавкой, С/Д
        async void ButtonAddДобавкиИБлюдо_OnClicked(object sender, EventArgs e)
        {
            //Получаем объект
            var button = sender as Button;
            var dish = button.BindingContext as DBDishesRep.Dish;

            //Заполняем новый временный листинг добавок, для того чтобы заполнить основной добавками для блюда
            AdditivesTemp.Clear();
            foreach (var additive in Additives)
            {
                AdditivesTemp.Add(additive);
            }
            Additives.Clear();

            //Заполнение основного добавками для блюда
            for (int i = 0; i < AdditivesTemp.Count; i++)
            {
                var additive = AdditivesTemp[i];
                if (additive.id_блюда == dish.Id)
                {
                    Additives.Add(additive);
                }
            }
            AdditivesList.ItemsSource = null;
            AdditivesList.ItemsSource = Additives;

            //Смена отображений на StackLayout добавок
            VisibleMethod(false,false,false,false,true,false,false,false,false);
            

            Tag = 1; //таг на добавку с блюдом
            Tag2 = dish; //с каким блюдом
            Tag5 = await DisplayActionSheet("Выберите гостя", "Отмена", null, this.Guests.Select(guest => guest.номергостя.ToString()).ToArray()); //выбор гостя к кому добавить блюдо

        }

        //клик по добавке из списка при двух условиях вызова
        private void ButtonAddДоб_OnClicked(object sender, ItemTappedEventArgs e) 
        {
            //Вариант 1: добавляем из меню с добавками. Создается блюдо с -1, -1 убирается с посл места и добавляется id. Profit
            //Вариант 2: добавляем из заказа. 
            //Вариант 2.1: в заказе уже есть блюдо с -1. В этом случае -1 заменяется на id.
            //Вариант 2.2: в заказе есть блюдо с id. Значит до этого -1 не было и значит добавляется нормально. 
            //Вариант 3: добавляем без добавок. в этом случае появляется блюдо с -1. 

            //Получаем объект
            var Additive = e.Item as DBAdditivesRep.Additive;

            if (Tag == 1) //если добавка из меню
            {
                DishAdd_OnTapped(this, null);

                foreach (var guest in Guests)
                {
                    if (guest.номерстола == GuestsFinalList[GuestsFinalList.Count - 1].номерстола &&
                        guest.номергостя == GuestsFinalList[GuestsFinalList.Count - 1].номергостя)
                    {
                        //удаляем последнюю добавленную -1
                        string[] idДобавокMass;
                        idДобавокMass = guest.idдобавок.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries); //заполняем массив строчками с id добавок
                        guest.idдобавок = "";
                        for (int i = 0; i < idДобавокMass.Length - 1; i++)
                        {
                            guest.idдобавок += idДобавокMass[i] + ";";
                        }
                        guest.idдобавок += Additive.Id + ";";
                    }
                }
                Tag = 0;
            }
            else //если добавка заказа
            {
                string[] idДобавокMass;
                foreach (var guest in Guests)
                {
                    if (guest == Tag3)
                    {
                        idДобавокMass = guest.idдобавок.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries); //заполняем массив строчками с id добавок
                        
                            if (idДобавокMass[Tag4.номерблюдавзаказе - 1].Trim() == (-1).ToString()) //если первая равняется -1
                            {
                                guest.idдобавок = "";
                                idДобавокMass[Tag4.номерблюдавзаказе - 1] = "";
                            }
                            idДобавокMass[Tag4.номерблюдавзаказе - 1] += " " + Additive.Id;
                            guest.idдобавок = "";
                            foreach (var s in idДобавокMass)
                            {
                                guest.idдобавок += s + ";";
                            }
                    }
                }
            }

            //Обновляем список
            ЗаполнениеДобавок();

            GuestsRefresh();

            //обновление отображения
            VisibleMethod(false,true,true,false,false,true,true,false,false);

            ordersList.IsEnabled = true;
        }

        //Выбор категории
        private void CatList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            //Получаем объект
            var catItem = e.Item as DBDishesRep.Cat;
            //Отображение
            VisibleMethod(false,true,true,false,false,true,false,true,false);
            //Очистка категорий
            DishCat.Clear();
            // Поиск и доавление в листинг блюд из категории
            foreach (var dish in Dishes)
            {
                if (catItem.Название == dish.Категория)
                    DishCat.Add(dish);
            }
            //Обновление
            MenuList.ItemsSource = null;
            MenuList.ItemsSource = DishCat;
        }

        //Смена курса по клику
        private void ButtonChКурс_OnClicked(object sender, EventArgs e)
        {
            //Получаем объект
            var button = sender as Button;
            var GF = button.BindingContext as DBOrdersRep.GuestFinal;

            //Ищем нужную запись гостя
            foreach (var guest in Guests)
            {
                if (GF.номерстола == guest.номерстола && GF.номергостя == guest.номергостя)
                {
                    //Добавляем в нужную позицию увеличение курса. Разбираем строку -> меняем -> собираем. Далее "пересборка"
                    string[] курсMass = guest.курс.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    guest.курс = "";
                    if (Convert.ToInt32(курсMass[GF.номерблюдавзаказе - 1]) + 1 < 5)
                    {
                        курсMass[GF.номерблюдавзаказе - 1] =
                        (Convert.ToInt32(курсMass[GF.номерблюдавзаказе - 1]) + 1).ToString();
                    }
                    else
                    {
                        курсMass[GF.номерблюдавзаказе - 1] = 1.ToString();
                    }
                    
                    foreach (var s in курсMass)
                    {
                        guest.курс += s + ";";
                    }
                }
            }
            GuestsRefresh();
        }

        //Добавление добавки из заказа, клик
        private void ButtonДобавкиЗаказ_OnClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var GF = button.BindingContext as DBOrdersRep.GuestFinal;

            //Открываем окно с добавками к блюду
            AdditivesTemp.Clear();
            foreach (var additive in Additives)
            {
                AdditivesTemp.Add(additive);
            }
            Additives.Clear();

            for (int i = 0; i < AdditivesTemp.Count; i++)
            {
                var additive = AdditivesTemp[i];
                if (additive.id_блюда == GF.idблюда)
                {
                    Additives.Add(additive);
                }
            }

            VisibleMethod(false, false,false, false, true, false, false, false,false);

            AdditivesList.ItemsSource = null;
            AdditivesList.ItemsSource = Additives;

            //Заносим информацию в теги
            foreach (var guestFinal in GuestsFinalList)
            {
                if (guestFinal == GF)
                {
                    foreach (var guest in Guests)
                    {
                        if (guest.номерстола == GF.номерстола && guest.номергостя == GF.номергостя)
                        {
                            Tag3 = guest;
                            Tag4 = GF;
                        }
                    }
                }
            }
        }

        //Удаление гостя
        async void ButtonGuestDel_OnClicked(object sender, EventArgs e)
        {
            //Выбор гостя для удаления
            var actionGuest = await DisplayActionSheet("Выберите гостя для удаления", "Отмена", null, this.Guests.Select(guest => guest.номергостя.ToString()).ToArray()); //выбор гостя для удаления

            //Если не отмена то удаляем из листинга
            if (actionGuest != "Отмена")
            {
                DBOrdersRep.Guest gDel = new DBOrdersRep.Guest();
                foreach (var guest in Guests)
                {
                    if (guest.номергостя == Convert.ToInt32(actionGuest))
                    {
                        gDel = guest;
                    }
                }
                Guests.Remove(gDel);
            }

            //если гостей не осталось то идет выброс на список столов
            if (Guests.Count == 0)
                ButtonСтол_OnClicked(this, e);

            //обновляем
            GuestsRefresh();
        }

        //клик по комментарию
        private void ButtonКомментарий_OnClicked(object sender, EventArgs e)
        {
            //показать окно комментария
            VisibleMethod(false,true,false,true,false,true,CatList.IsVisible,MenuList.IsVisible,false);
            //получаем объект
            var GF = (sender as Button).BindingContext as DBOrdersRep.GuestFinal;
            
            //получаем текущий комментарий и записываем в поле
            string коммент = "";
            foreach (var guest in Guests)
            {
                if (guest.номерстола == GF.номерстола)
                {
                    string[] коммMass = guest.комментарии.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    коммент = коммMass[GF.номерблюдавзаказе - 1];
                    if (коммент == "Нет") коммент = "";
                    CommEntry.Text = коммент;
                }
            }
            //передаем объект в обработку
            Tag4 = (sender as Button).BindingContext as DBOrdersRep.GuestFinal;

            CommEntry.Focus();
        }

        //сохранение комментария
        private void BCommSave_OnClicked(object sender, EventArgs e)
        {

            string комм = CommEntry.Text;

            //запись нет вместо отсутствия
            if (комм == "")
            {
                комм = "Нет";
            }

            //сохранения комментария
            foreach (var guest in Guests)
            {
                if (guest.номерстола == Tag4.номерстола &&
                    guest.номергостя == Tag4.номергостя)
                {
                    string[] КоммMass = guest.комментарии.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    КоммMass[Tag4.номерблюдавзаказе - 1] = комм;
                    guest.комментарии = "";
                    foreach (var s in КоммMass)
                    {
                        guest.комментарии += s + ";";
                    }
                }
            }

            GuestsRefresh();

            //Открываем окно заказа
            VisibleMethod(false, true, true, false, false, true, CatList.IsVisible, MenuList.IsVisible, false);

        }

        //отмена комментария
        private void BCanComm_OnClicked(object sender, EventArgs e)
        {
            //открываем окно заказа
            VisibleMethod(false, true, true, false, false, true, CatList.IsVisible, MenuList.IsVisible, false);
        }

        //закрываем поиск
        private void BackSchButton_OnClicked(object sender, EventArgs e)
        {
            VisibleMethod(false, true, true, false, false, true, true, false, false);
        }

        //добавить стол
        private void BAddTable_OnClicked(object sender, EventArgs e)
        {
                //попытка удалить пробелы у номера, если ошибка то сообщение. Так же проверка на наличие стола в списке
                try
                {
                    TableAddEntry.Text.Trim();
                    if (Tables.Any(table => table.номерстола == Convert.ToInt32(TableAddEntry.Text)))
                    {
                        DisplayAlert("Ошибка", "Такой стол уже существует", "Исправить");
                        return;
                    }
                }
                catch (Exception)
                {
                    DisplayAlert("Ошибка", "Нет номера стола", "Исправить");
                    return;
                }

                //попытка добавить стол, если ошибка то завершение функции
                try
                {
                    //добавление
                    if (Tables.Any())
                    {
                        Tables.Add(new DBOrdersRep.Table { Id = Tables[Tables.Count - 1].Id + 1, номерстола = Convert.ToInt32(TableAddEntry.Text) });
                    }
                    else
                    {
                        Tables.Add(new DBOrdersRep.Table { Id = 0, номерстола = Convert.ToInt32(TableAddEntry.Text) });
                    }

                    Tables[Tables.Count - 1].Id = 0;
                    App.TablesDatabase.SaveItem(Tables[Tables.Count - 1]);

                }
                catch (Exception)
                {

                    return;
                }

                //Обновляем отображение списка столов
                tablesList.ItemsSource = null;
                tablesList.ItemsSource = Tables;
        }

        //Открытие поиска
        private void SearchMenuButton_OnClicked(object sender, EventArgs e)
        {
            //поиск по всем категориям или по текущей
            if (CatList.IsVisible)
            {
                DishSch = Dishes;
            }
            else
            {
                DishSch = DishCat;
            }

            //обновление отображения списка столов на нужный для поиска
            SchList.ItemsSource = null;
            SchList.ItemsSource = DishSch;

            //открываем окно поиска
            VisibleMethod(false,false,false,false,false,false,false,false,true);

            //фокус на поле ввода для поиска
            SearchBar.Focus();
        }

        //Поиск
        private void SearchBar_OnSearchButtonPressed(object sender, EventArgs e)
        {
            //получаем введенное
            string keyword = SearchBar.Text;

            //повышение регистра первой буквы, все блюда должны быть с большой буквы в базе
            if (keyword.Length > 0)
            {
                keyword = Char.ToUpper(keyword[0]) + keyword.Substring(1);
            }
                
            //получаем коллекцию блюд с началом строки совпадающей с искомой
            IEnumerable<DBDishesRep.Dish> dishSchFinal = DishSch.Where(dish => dish.Название.StartsWith(keyword));
            //обновляем отображения результатов
            SchList.ItemsSource = null;
            SchList.ItemsSource = dishSchFinal;
        }

        //Метод смены окон путем IsVisible =true/false
        public void VisibleMethod(
            bool TBListSL, bool OrdSL, bool OrdL, bool CommentSL, bool AdditSL, bool MenuCatSL, bool Cat, bool Dishes, bool Sch
            )
        {
            OrderSL.IsVisible = OrdSL;
            ordersList.IsVisible = OrdL;
            CommSL.IsVisible = CommentSL;
            AddSL.IsVisible = AdditSL;
            BotSL.IsVisible = MenuCatSL;
            CatList.IsVisible = Cat;
            MenuList.IsVisible = Dishes;
            SchSL.IsVisible = Sch;
            TableSL.IsVisible = TBListSL;
            if (TBListSL)
            {
                DelGuest.IsEnabled = false;
                NewGuest.IsEnabled = false;
                SumB.IsEnabled = false;
                TablesB.IsEnabled = false;
                DelTable.IsEnabled = true;
            }
            else
            {
                DelGuest.IsEnabled = true;
                NewGuest.IsEnabled = true;
                SumB.IsEnabled = true;
                TablesB.IsEnabled = true;
                DelTable.IsEnabled = false;
            }
        }

        //Кнопка назад из категории в меню категорий
        private void BackMenuButton_OnClicked(object sender, EventArgs e)
        {
            VisibleMethod(false,true,true,false,false,true,true,false,false);
        }

        //Удаление стола, выбор
        async void DelTable_OnClicked(object sender, EventArgs e)
        {
            //окно выбора
            string actionDelTB = await DisplayActionSheet("Удалить стол", "Отмена", null, this.Tables.Select(table => table.номерстола.ToString()).ToArray());

            //если отмена
            if (actionDelTB == "Отмена")
            {
                return;
            }
            //переход к обработке
            УдалениеСтола(actionDelTB);
        }

        //Удаление стола, обработка
        private void УдалениеСтола(string actionDelTB)
        {
            //переменная для удаляемого стола
            DBOrdersRep.Table TableDel = null;
            //получаем стол для удаления
            foreach (var table in Tables)
            {
                if (table.номерстола.ToString() == actionDelTB)
                {
                    TableDel = table;
                } 
            }
            //получаем список гостей в текущий момент из базы
            var GItems = App.GuestsDatabase.GetItems();

            //перебор по базе и удаление гостей из удаляемого стола
            foreach (var guest in GItems)
            {
                if (guest.номерстола == TableDel.номерстола)
                {
                    App.GuestsDatabase.DeleteItem(guest.Id);
                }
            }

            //очистка
            Guests.Clear();

            //удаление стола
            Tables.Remove(TableDel);
            App.TablesDatabase.DeleteItem(TableDel.Id);

            
            //обновление отображения
            tablesList.ItemsSource = null;
            tablesList.ItemsSource = Tables;

        }

        //Сумма
        private void ButtonСумм_OnClicked(object sender, EventArgs e)
        {
            //обнуление
            double Summ = 0;

            //для каждого GuestFinal (на 1 блюдо в заказе 1 GF) происходит суммирование цен блюд и добавок
            foreach (var guest in GuestsFinalList)
            {
                foreach (var dish in Dishes)
                {
                    if (dish.Название == guest.названиеблюда)
                    {
                        Summ += dish.Цена;
                    }
                }

                string[] Добавки = guest.названиедобавок.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                ЗаполнениеДобавок();

                foreach (var additive in Additives)
                {
                    foreach (var s in Добавки)
                    {
                        if (additive.Название == s)
                        {
                            Summ += additive.Цена;
                        }
                    }
                }               
            }

            //Вывод
            DisplayAlert("Сумма заказа", Convert.ToString(Summ) + ", руб", "OK");
        }

        //Удаление блюда
        private void ButtonDelDish_OnClicked(object sender, EventArgs e)
        {
            //получаем объект
            var button = sender as Button;
            var GF = button.BindingContext as DBOrdersRep.GuestFinal;

            DBOrdersRep.Guest Guest = new DBOrdersRep.Guest();

            //находим нужного гостя
            foreach (var guest in Guests)
            {
                if (guest.номергостя == GF.номергостя)
                {
                    Guest = guest;
                }
            }

            

                string[] БлюдаMass = null;
                string[] ДобавкиMass = null;
                string[] КомментарииMass = null;
                string[] КурсыMass = null;
                string[] ЗаказMass = null;
            //Заполняем массивы параметров GF
                if (GF.номергостя == Guest.номергостя)
                {
                    БлюдаMass = Guest.idблюд.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    ДобавкиMass = Guest.idдобавок.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    КомментарииMass = Guest.комментарии.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    КурсыMass = Guest.курс.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    ЗаказMass = Guest.заказ.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                }
                //Удаляем параметры с нужной позиции строки параметра в объекте Guest
                List<string> БлюдаList = new List<string>(БлюдаMass);
                БлюдаList.RemoveAt(GF.номерблюдавзаказе-1);
                Guest.idблюд = "";
                foreach (var s in БлюдаList)
                {
                    Guest.idблюд += s + ";";
                }

                List<string> ДобавкиList = new List<string>(ДобавкиMass);
                ДобавкиList.RemoveAt(GF.номерблюдавзаказе-1);
                Guest.idдобавок = "";
                foreach (var s in ДобавкиList)
                {
                    Guest.idдобавок += s + ";";
                }

                List<string> КомментарииList = new List<string>(КомментарииMass);
                КомментарииList.RemoveAt(GF.номерблюдавзаказе-1);
                Guest.комментарии = "";
                foreach (var s in КомментарииList)
                {
                    Guest.комментарии += s + ";";
                }

                List<string> КурсыList = new List<string>(КурсыMass);
                КурсыList.RemoveAt(GF.номерблюдавзаказе-1);
                Guest.курс = "";
                foreach (var s in КурсыList)
                {
                    Guest.курс += s + ";";
                }

                List<string> ЗаказList = new List<string>(ЗаказMass);
                ЗаказList.RemoveAt(GF.номерблюдавзаказе - 1);
                Guest.заказ = "";
                foreach (var s in КурсыList)
                {
                    Guest.заказ += s + ";";
                }

            //обновление
            GuestsRefresh();
        }

        //Тап по блюду, заказ
        private void OrdersList_OnItemTappedЗаказ(object sender, ItemTappedEventArgs e)
        {
            //получаем объект
            var GF = e.Item as DBOrdersRep.GuestFinal;

            //смена параметра в Guest
            foreach (var guest in Guests)
            {
                string[] ЗаказMass = null;
                if (guest.номергостя == GF.номергостя)
                {
                    ЗаказMass = guest.заказ.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    if (ЗаказMass[GF.номерблюдавзаказе - 1] == "0")
                    {
                        ЗаказMass[GF.номерблюдавзаказе - 1] = "1";
                    }
                    else
                    {
                        ЗаказMass[GF.номерблюдавзаказе - 1] = "0";
                    }

                    guest.заказ = "";

                    foreach (var s in ЗаказMass)
                    {
                        guest.заказ += s + ";";
                    }
                }
                
            }

            //обновление
            GuestsRefresh();

            
        }

        //Кнопка назад из добавок
        private void BackAddButton_OnClicked(object sender, EventArgs e)
        {
            VisibleMethod(false, true, true, false, false, true, true, false, false);
            ЗаполнениеДобавок();
        }

    }

    //класс конвертера цветаю Нужен для привязки в xaml цвета заказа при заказе блюда
    public class TapColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value.ToString() == "1")
                return Color.Coral;

            return Color.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        

    }

}

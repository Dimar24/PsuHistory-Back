using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Data.EF.SQL.Data
{
    public static class BurialInitialization
    {
        public static List<Burial> GetBurialInitialization(List<TypeBurial> typeBurials, DateTime dateTimeInitialization)
        {
            var id = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id;

            var burials = new List<Burial>()
            {
                new Burial()
                {
                    Name = string.Empty,
                    NumberBurial = 2353,
                    Location = "г. Полопцк, Боровуха-2",
                    KnownNumber = 183,
                    UnknownNumber = 327,
                    Year = 1955,
                    Latitude = 55.585541,
                    Longitude = 28.581422,
                    Description = "В 2007 году памятник реконструирован, установлена гранитная стела высотой 4 м и гранитные доски на постаментах с именами погибших. Основание для увековечения: похоронены воины 119-й, 311-й, 360-й стрелковых дивизий, которые погибли в боях за освобождение г. Полоцка; воины, которые умерли от ран и болезней в эвакогоспиталях № 3329, № 3438, кроме того, в 1960 г. сюда перезахоронены неизвестный солдат из одиночной могилы, которая находилась возле здания СШ № 6, а также танкист В.М. Малявко из одиночной могилы, которая находилась на территории воинской части; 11 воинов, которые были похоронены в поселке Боровуха-3. А в 2005 г. сюда перезахоронены останки 356 военнослужащих РККА, обнаруженных при поисковых работах с раскопками в районе ул. Вологина (200 м. севернее ДОСа № 371). Из них установлены 3 фамилии: Бритвин А.В., Лебедев А.И., Ушаков И.Г. ",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = string.Empty,
                    NumberBurial = 4250,
                    Location = "г. Полопцк, пл. Свободы",
                    KnownNumber = 16,
                    UnknownNumber = 0,
                    Year = 1944,
                    Latitude = 55.585541,
                    Longitude = 28.581422,
                    Description = "Основание увековечить – по решению Военного Совета 1-го Прибалтийского фронта, здесь были захоронены 15 командиров частей и подразделений, участвовавших  в освобождении Полоцка и Полоцкого района. Инициатор – Военный Совет 1-го Прибалтийского фронта и Полоцкий городской исполнительный комитет. Автор мемориальной экспозиции – по 1981 г. архитектор В. Аладов, скульптор Г. Муромцев. Материал памятника – бронза, гранит, бетон. В центре стела (15×5×3 м), облицованная серым гранитом. Справа на стеле бронзовый горельеф с изображением трех воинов (раненный воин, воин с автоматом и воин со знаменем), слева – памятная надпись. На левой боковой грани и на титульной стороне стелы – бронзовые доски с текстом Приказа Верховного главнокомандующего Вооруженными силами СССР по случаю освобождения города с перечислением войсковых частей, которые освобождали Полоцк от немецко-фашистских захватчиков, и частей, которые получили почетное наименование «Полоцких». Перед стелой темно-красное гранитное надгробие (3×3) с именами захороненных. Текст оснвной надписи – «Освободителям Полоцка».",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = string.Empty,
                    NumberBurial = 4251,
                    Location = "г. Полопцк, Громовское клабдище",
                    KnownNumber = 459,
                    UnknownNumber = 0,
                    Year = 1944,
                    Latitude = 55.490717,
                    Longitude = 28.81554,
                    Description = "Основание увековечения — первичное место захоронения воинов 47-й и 51-й сд 22-го гвск, погибшие при освобождении г. Полоцка с 2 по 4 июля 1944 г. а также солдат, умерших от полученных на фронте ран в госпитале № 2918. Автор мемориальной экспозиции - неизвестен. Материал памятника — обелиск был изготовлен из бетона, оштукатуренный. Высота - 4 м., в плане 2х3 м., обнесён металлической оградой Текст надписи — «Вечная память героям павшим в боях за свободу нашей Родины».",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = string.Empty,
                    NumberBurial = 4252,
                    Location = "г. Полопцк, ул. Дзержинского, Фатыновское кладбище",
                    KnownNumber = 93,
                    UnknownNumber = 0,
                    Year = 1949,
                    Latitude = 55.479171,
                    Longitude = 28.763218,
                    Description = "Основание увековечения — захоронены воины 71-й гв. сд, 51-й гв. сд и танкисты 47-го отд. огнеметного танкового полка и 19 отд. инженерно-танкового полка, погибшие при освобождении г. Полоцка с 1 по 4 июля 1944, года, а также солдаты, умершие от полученных на фронте ран в госпиталях № 1319 и № 733. Автор мемориальной экспозиции - неизвестен. Материал памятника - обелиск был изготовлен из бетона, оштукатуренный. Высота - 4 м., в плане 2х3 м. обнесён металлической оградой. Текст надписи — «Вечная слава и память героям, погибшим за освобождение г Полоцка от немецко-фашистских захватчиков».",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = string.Empty,
                    NumberBurial = 4253,
                    Location = "г. Полопцк, ул. Гагарина, Красное кладбище",
                    KnownNumber = 44,
                    UnknownNumber = 156,
                    Year = 1984,
                    Latitude = 55.483743,
                    Longitude = 28.803708,
                    Description = "Основание увековечения — захоронены воины, которые умерли от ран и болезней в эвакогоспитале № 1969, сортировальном эвакогоспитале № 1822 и сортировально-полевом передвижном госпитале № 2479 Автор мемориальной экспозиции — автор А.Ф. Сидюк. Материол памятника - железобетонная стена с изображением звезды и накладным текстом, высота 2 м, длина 9 м. Текст надписи -— «В память о 160-и советских воинах, погибших при освобождении города Полоцка от немецко-фашистских захватчиков».",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = string.Empty,
                    NumberBurial = 4254,
                    Location = "г. Полопцк, ул. 4-й пер. Фрунзе, Ксаверьевское кладбище",
                    KnownNumber = 11,
                    UnknownNumber = 4,
                    Year = 1951,
                    Latitude = 55.498715,
                    Longitude = 28.778089,
                    Description = "Основание увековечения — захоронены воины, умершие от полученных ранений в госпитали № 1105 на протяжении с июля по 1 ноября 1944 г. Автор мемориальной экспозиции — архитектор Глазунов Н.П Материал памятника - железобетонный белиск, высота 4,8 м, в плане 4,3х5/ м, обнесен металлической оградой.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = "Место массового уничтожения Мемориальный комплекс памяти жертв нацизма «Урочище пески»",
                    NumberBurial = 6807,
                    Location = "г. Полопцк, урочище Пески",
                    KnownNumber = 107,
                    UnknownNumber = 37894,
                    Year = 0,
                    Latitude = 55.506784,
                    Longitude = 28.784838,
                    Description = "Железобетонная стела на символической могиле, установленная в 1955 году, на которой расположена информационная табличка. Строительство мемориального комплекса начато в 2010 г и продолжается по настоящее время. Захоронены останки десятков тысяч военнопленных Дулага-125, подпольщиков, партизан и мирных жителей, погибших от рук нацистов и их пособников в 1941 - 1944. гг.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 2344,
                    Location = "г.п. Боровуха-1, ул. Армейская",
                    KnownNumber = 125,
                    UnknownNumber = 17875,
                    Year = 1955,
                    Latitude = 55.593056,
                    Longitude = 28.594444,
                    Description = "Основание увековечения - захоронение советских воинов и мирных жителей на месте бывшего лагеря $11 354. Материал центрального памятника — 4 Железобетонные колонны высотой 3-35 м. диаметр 2,5-3 м.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 2345,
                    Location = "г.п. Боровуха-1, ул. Школьная",
                    KnownNumber = 1,
                    UnknownNumber = 0,
                    Year = 1968,
                    Latitude = 55.593056,
                    Longitude = 28.596667,
                    Description = "Основание увековечения — захоронен пионер Дмитрий Потапенко 1926 гр. принимавший участие в партизанском движении. Погиб в 1941 г. Материал памятника — железобетонный обелиск 1,5х0,5х2.5 м.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Индивидуальная могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4261,
                    Location = "г.п. Боровуха-1, ул. Садовая, 21",
                    KnownNumber = 131,
                    UnknownNumber = 60,
                    Year = 1968,
                    Latitude = 55.586699,
                    Longitude = 28.600750,
                    Description = "Основание увековечения - захоронены солдаты, погибшие при Полоцкой наступательной операции лета 1944 г. Материал центрального памятника — постамент железобетонный 25>2,5«25 м, скульптура двух воинов высотой 25-3 м. рисутствует табличка историко-культурной — ценности. — Состояние удовлетворительное.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4262,
                    Location = "г.п. Боровуха-1, ул. Озерная",
                    KnownNumber = 0,
                    UnknownNumber = 50,
                    Year = 1965,
                    Latitude = 55.579676,
                    Longitude = 28.615694,
                    Description = "Основание увековечения — захоронены воины 119 сд и 360 сд Красной армии, погибшие и умершие от рон в ходе Полоцкой наступательной операции лета 944 г. Материал памятника - железобетонный постамент 2,5х2,5=2,5 м. и обелиск высотой 2. м.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4874,
                    Location = "г. Новополоцк, ул. Молодежная, д. 73",
                    KnownNumber = 13,
                    UnknownNumber = 0,
                    Year = 1996,
                    Latitude = 55.534201,
                    Longitude = 28.655838,
                    Description = "Основание увековечения — захоронены воины 71 гвсд., погибшие в ходе Полоцкой наступательной операции при освобождении деревень позднее вошедших в состав г Новополоцка. Материал памятника — железобетонноя панель, высота 2 м",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4875,
                    Location = "г. Новополоцк, санитарно-защитная полоса автодорога на завод «Полимир» в 300 м. от города",
                    KnownNumber = 1,
                    UnknownNumber = 0,
                    Year = 1974,
                    Latitude = 55.521549,
                    Longitude = 28.644888,
                    Description = "Основание увековечения — место гибели советского штурмовика Ил-2 и командира экипажа в ходе выполнения боевого зодония 3 июля 1944 г. Захоронен командир экипажа Алексей Шкарпетов. Получивший ранение стрелок-радист Григорий Каргин умер в 699-м ХППГ и захоронен в братской могиле №4263 в гл. Ветрино. Материал центрального памятника — камень высотой 25 м; железобетонная воронка диаметром 4-5 м.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Индивидуальная могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 2355,
                    Location = "Гомельский с/c, д. Пукановка-2",
                    KnownNumber = 31,
                    UnknownNumber = 0,
                    Year = 0,
                    Latitude = 55.350560,
                    Longitude = 28.898600,
                    Description = "Захоронены воины 51 гв сд и 270 сд Красной армии и партизаны Ушачской партизанской бригады «Смерть фашизму», Партизанской бригады им. ВИ. Чопаева, погибшие в 1943 - 1944 г.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 2363,
                    Location = "Гомельский с/c, д. Пашки",
                    KnownNumber = 14,
                    UnknownNumber = 0,
                    Year = 0,
                    Latitude = 55.350149,
                    Longitude = 28.740789,
                    Description = "Установлен памятник - 2 плиты - на средства ВПК «Поиск» г.Витебск. Захоронены воины 71 гв. сд Красной армии и партизаны Партизанской бригады им. ВИ. Чапаева, Смоленского партизанского полка ИФ. Содчикова, Ушачской партизанской бригады «Смерть фошизму», имеющие первичное место захоронения «д. Пашки Гомельского с/с» и погибшие в 1943 — 1944 г.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 2365,
                    Location = "Гомельский с/c, д. Плуссы",
                    KnownNumber = 0,
                    UnknownNumber = 2,
                    Year = 0,
                    Latitude = 55.323470,
                    Longitude = 28.792032,
                    Description = "Заоронено 2 неизвестных летчика",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4271,
                    Location = "Горянский с/c, д. Горяны",
                    KnownNumber = 180,
                    UnknownNumber = 0,
                    Year = 0,
                    Latitude = 55.419670,
                    Longitude = 29.034881,
                    Description = "Захоронены воины 28 сд, 47 сд, 51 сд, 90 гв сд, 9 сд, 332 сд, 360 сд Красной армии и партизан 3-й Белорусской партизанской бригады Среди них - Герои Советского Союза Николай Петрович Фомин и Иван Андреевич Филатьев.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4272,
                    Location = "Горянский с/c, д. Домники",
                    KnownNumber = 678,
                    UnknownNumber = 0,
                    Year = 1965,
                    Latitude = 55.507737,
                    Longitude = 29.176828,
                    Description = "1965 г. - установлен обелиск. Захоронены воины 332 сд, 357 сд, 360 сд, 28 сд, 99 гв сд, 51 сд, 19 сд, 166 сд, 16 Литовской сд Красной армии, а также партизаны 1-й. Сиротинской партизанской бригады им. Ленина и 3-й Белорусской партизанской бригады, погибшие в 1941 — 1944 гг.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4274,
                    Location = "Горянский с/c, д. Межно-3",
                    KnownNumber = 41,
                    UnknownNumber = 0,
                    Year = 1971,
                    Latitude = 55.367432,
                    Longitude = 28.831937,
                    Description = "В 1971 г. на могиле поставлен обелиск. В 2010-ых реконструирован. Захоронены воины 51гв сд и 71 гв сд Красной Армии и партизаны Партизанской бригады им. В.И. Чапаева, Смоленского портизансуого полка И.Ф. Содчикова, а также Ушачской партизанской бригады «Смерть фашизму», погибшие в 1943 — 1944 гг.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4280,
                    Location = "Зеленковский с/c, д. Дмитровщина",
                    KnownNumber = 3,
                    UnknownNumber = 0,
                    Year = 1967,
                    Latitude = 55.633841,
                    Longitude = 28.28775677,
                    Description = "1967 г. - поставлен обелиск. Захоронены партизаны партизанской бригады «Неуловимые», погибшие в 1943 г.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 5698,
                    Location = "Бобыничский с/c, д. Боярово",
                    KnownNumber = 5,
                    UnknownNumber = 80,
                    Year = 1975,
                    Latitude = 55.295439,
                    Longitude = 28.426201,
                    Description = "Время возведения - 1975 г. Основание увековечивания — захоронены мирные жители и партизаны, которых в 1944 г. сожгли нацисты. Размер воинского захоронения- 3,5*4. Памятник — стела.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 6288,
                    Location = "Бобыничский с/c, д. Бобыничи",
                    KnownNumber = 0,
                    UnknownNumber = 108,
                    Year = 2007,
                    Latitude = 55.290157,
                    Longitude = 28.413603,
                    Description = "Время возведения - 2007 г. Размер воинского захоронения - 3х10. Памятник установлен погибшим евреям в ходе реализации нацистскими оккупационными властями политики геноцида при помощи фонда семьи Майкла и Дайяны Лазарусов. Описание - большой камень с надписями на белорусском, английском языках и иврите.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4260,
                    Location = "Бобыничский с/c, д. Богушево",
                    KnownNumber = 1,
                    UnknownNumber = 0,
                    Year = 0,
                    Latitude = 55.357493,
                    Longitude = 28.411702,
                    Description = "Захоронен партизан партизанской бригады им. К.Е. Ворошилова Серафимов Виктор Семенович 1925 г.р. Погиб 24 мая 1944 г. Размер воинского захоронения - 2>1.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Индивидуальная могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4273,
                    Location = "Горянский с/c, д. Залесье",
                    KnownNumber = 1,
                    UnknownNumber = 0,
                    Year = 1956,
                    Latitude = 55.415628,
                    Longitude = 29.159343,
                    Description = "1956 г. - поставлен обелиск. Могила Серова Евгения Константиновича, гвардии старшего лейтенанта Красной Армии, погибшего 1 июля 1944 г.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Индивидуальная могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 2359,
                    Location = "Гомельский с/c, д. Межно-3",
                    KnownNumber = 1,
                    UnknownNumber = 0,
                    Year = 0,
                    Latitude = 55.367757,
                    Longitude = 28.830844,
                    Description = "Захоронен Щербаков Алексей Иосифович 1894 гр. - партизан 2-й Ушачской бригады им. П.К. Пономаренко (п/о им. Я.М. Свердлова), погибший 8 мая 1944 г",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Индивидуальная могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 2350,
                    Location = "Полотовский с/c, д. Полота",
                    KnownNumber = 201,
                    UnknownNumber = 10,
                    Year = 1968,
                    Latitude = 55.6119,
                    Longitude = 29.1041,
                    Description = "Возведён обелиск в 1968 г. Основание для увековечения — захоронены воины 4-й Ударной армии и 16 Литовской сд Красной армии, и партизаны бригады им С.М. Короткина, погибшие в 1942 - 1944 гг.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4255,
                    Location = "Азинский с/c, д. Грамоще",
                    KnownNumber = 549,
                    UnknownNumber = 9,
                    Year = 1951,
                    Latitude = 55.664253,
                    Longitude = 28.457299,
                    Description = "Время возведения - 1951 г. Основание увековечения — захоронены воины ЗП сд, 200 сд, 332 сд, 378 сд, 339 сд, 21 гв сд Красной армии, погибшие в 1941 — 1944 гг. Материал памятника — гранитный обелиск.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4257,
                    Location = "Азинский с/c, д. Азино, ул. Кульнева 1",
                    KnownNumber = 195,
                    UnknownNumber = 74,
                    Year = 1960,
                    Latitude = 55.622738,
                    Longitude = 28.643377,
                    Description = "Время возведения - 1960 г. Основание увековечения - захоронены воины ЗП сд, 332 сд, 360 сд, 378 сд, (9 сд, 28 сд, 16 Литовской сд, 21 гв сд, 200 сд Красной армии и партизаны, погибшие в 1941 - 1944 гг. Материал памятника — три железобетонных обелиска, обнесен металлической оградой.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4258,
                    Location = "Бобыничский с/c, д. Бобыничи",
                    KnownNumber = 21,
                    UnknownNumber = 63,
                    Year = 1960,
                    Latitude = 55.281221,
                    Longitude = 28.410712,
                    Description = "Время возведения - 1960 г. Основание увековечения — похоронены воины 21 ад и 237 сд Красной армии и партизаны Ветринской партизанской бригады, Партизанских бригад им. АФ. Данукалова, им. ВИ. Ленина, им. ВИ. Чапаева, им. КЕ. Ворошилова, им. СМ. Короткина, Смоленского партизанского полк ИФ Садчикова, погибшие в 1942 — 1944 п: Размер воинского захоронения — 3.0, установлена скульптура воина.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4266,
                    Location = "Вороничский с/c, д. Заскорки",
                    KnownNumber = 67,
                    UnknownNumber = 0,
                    Year = 1960,
                    Latitude = 55.383459,
                    Longitude = 28.645873,
                    Description = "В 1960 г. на могиле поставлен памятник — скульптура воина в честь погибших во время Великой Отечественной войны воинов и партизан, среди них - Герой Советского Союза Абдула Жанзахов, погибший 30 июня 1944 г. Захоронены воины 67 гв сд и ЛП гв сд, а также партизаны партизанской бригады им. ВИ Чапаева, Смоленского партизанского полка ИФ. Садчикова, Ушачской партизанской бригады «Смерть фашизму».",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4268,
                    Location = "Вороничский с/c, д. Кунцевичи",
                    KnownNumber = 17,
                    UnknownNumber = 0,
                    Year = 1970,
                    Latitude = 55.359978,
                    Longitude = 28.654290,
                    Description = "В 1970 г. на могиле была установлена стела в честь партизан Смоленского партизанского полка И.Ф. Садчикова, которые погибли в 1943 - 1944 гг. в боях против немецко-фашистских захватчиков.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 5700,
                    Location = "Азинский с/c, д. Владычино",
                    KnownNumber = 73,
                    UnknownNumber = 0,
                    Year = 1974,
                    Latitude = 55.6491139,
                    Longitude = 28.606257,
                    Description = "Время возведения - 1974 г. Основание увековечения - похоронены жители д. Борки, Кевлы, Матюши, Парфеновцы, Слабощуки, сожжённые в 1943 г. Материол памятника - железобетонный обелиск",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 5699,
                    Location = "Азинский с/c, д. 800 м на юго-восток от д. Дохнары",
                    KnownNumber = 8,
                    UnknownNumber = 0,
                    Year = 1968,
                    Latitude = 55.594982,
                    Longitude = 28.727826,
                    Description = "Время возведения - 1968 г. Основание увековечения — похоронены мирные жители д. Дохнары, расстрелянные немецко-фашистскими захватчиками 15 мая 1942 г. Размер воинского захоронения - 5х4, установлен обелиск",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 2351,
                    Location = "Солоникский с/c, д. Струнье",
                    KnownNumber = 1,
                    UnknownNumber = 0,
                    Year = 1947,
                    Latitude = 55.4921,
                    Longitude = 29.0282,
                    Description = "1947 г. поставлена стела, реконструирован в 2010-х гг. Могила мл.лейтенанта 47 сд Страшинского Игоря Сергеевича 1925 гр. погибшего при освобождении Полоцкого р-на 3 июля 1944 г.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Индивидуальная могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4291,
                    Location = "Полотовский с/c, д. Юровичи",
                    KnownNumber = 677,
                    UnknownNumber = 1,
                    Year = 1962,
                    Latitude = 55.5771,
                    Longitude = 29.0052,
                    Description = "Возведён обелиск в 1962 г. Основание для увековечения — захоронены воины 31 сд, 332 сд, 357 сд, 360 сд, 378 сд, 16 Литовской сд, 9 сд Красной армии и партизаны 1-й Сиротинской партизанской бригады, 3-й Белорусской партизанской бригады, Партизанских бригад им. ВИ. Ленина и К.М. Короткина.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 4293,
                    Location = "Полотовский с/c, ст. Позднякова",
                    KnownNumber = 1,
                    UnknownNumber = 0,
                    Year = 0,
                    Latitude = 55.548991,
                    Longitude = 29.903734,
                    Description = "Могила партизана Богданова Владимира 1928 гр., погибшего в 1942 г. Размер воинского захоронения 2х2, установлен обелиск.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Индивидуальная могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 5369,
                    Location = "Полотовский с/c, ст. Сарнополье",
                    KnownNumber = 1,
                    UnknownNumber = 0,
                    Year = 0,
                    Latitude = 55.577108,
                    Longitude = 29.059430,
                    Description = "Могила лейтенанта Щеглова. Установлен памятный знак.",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Индивидуальная могила")).Id,
                },

                new Burial()
                {
                    Name = String.Empty,
                    NumberBurial = 2349,
                    Location = "Островщинский с/c, д. Островщина",
                    KnownNumber = 15,
                    UnknownNumber = 10,
                    Year = 1965,
                    Latitude = 55.5069086,
                    Longitude = 28.3975178,
                    Description = "Памятник поставлен в 1965 г. Реконструирован в 2010-е г: Основание увековечения — захоронен воин 199 гв сп 67 гв сд Жуков Николай Романович, погибший 4 июля 1944 г, и партизаны Ветринской партизанской бригады, Партизанской бригад «Неуловимые», Партизанских бригад им. КЕ. Ворошилова и им. СМ. Короткина, погибшие в период 1942 - 1944 гг",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },

                // table on page 44
                // page of last burial 63
                // page of next berial 64
            };

            burials.ForEach(d =>
            {
                d.Id = Guid.NewGuid();
                d.CreatedAt = dateTimeInitialization;
                d.UpdatedAt = dateTimeInitialization;
            });

            return burials;
        }
    }
}

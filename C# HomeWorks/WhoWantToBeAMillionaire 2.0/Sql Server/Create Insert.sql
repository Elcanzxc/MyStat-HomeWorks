
CREATE DATABASE MillionaireGame;


USE MillionaireGame;


CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    LoginName NVARCHAR(50) NOT NULL UNIQUE 
);


CREATE TABLE Questions (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Difficulty INT NOT NULL, 
    QuestionText NVARCHAR(MAX) NOT NULL,
    CorrectAnswer NVARCHAR(MAX) NOT NULL, 
    IncorrectAnswer1 NVARCHAR(MAX) NOT NULL,
    IncorrectAnswer2 NVARCHAR(MAX) NOT NULL, 
    IncorrectAnswer3 NVARCHAR(MAX) NOT NULL
);


CREATE TABLE GameSessions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    CurrentQuestionIndex INT NOT NULL DEFAULT 0, 
    CurrentWinnings MONEY NOT NULL DEFAULT 0, 
    UsedQuestionIds NVARCHAR(MAX) NOT NULL, 
    IsFinished BIT NOT NULL DEFAULT 0, 
    StartTime DATETIME NOT NULL DEFAULT GETDATE(), 
    EndTime DATETIME NULL, 
    FOREIGN KEY (UserId) REFERENCES Users(Id) 
);


CREATE TABLE Leaderboard (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT UNIQUE NOT NULL, 
    FinalScore MONEY NOT NULL,
    GameEndTime DATETIME NOT NULL, 
    FOREIGN KEY (UserId) REFERENCES Users(Id),
);




SELECT * FROM Questions





INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (1, 'Какая планета находится ближе всего к Солнцу?', 'Меркурий', 'Венера', 'Марс', 'Юпитер');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (2, 'Сколько континентов на планете Земля?', '7', '5', '6', '8');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (3, 'Какое химическое вещество имеет формулу H2O?', 'Вода', 'Кислород', 'Азот', 'Углекислый газ');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (4, 'Какой город является столицей Японии?', 'Токио', 'Киото', 'Осака', 'Нагоя');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (5, 'Какой язык является наиболее распространенным в мире по числу носителей?', 'Китайский', 'Испанский', 'Английский', 'Арабский');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (6, 'Кто написал роман "Война и мир"?', 'Лев Толстой', 'Федор Достоевский', 'Антон Чехов', 'Александр Пушкин');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (7, 'Какая самая длинная река в мире?', 'Нил', 'Амазонка', 'Миссисипи', 'Янцзы');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (8, 'Как называется наука, изучающая насекомых?', 'Энтомология', 'Орнитология', 'Зоология', 'Ботаника');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (9, 'Какое животное изображено на логотипе Ferrari?', 'Гарцующий жеребец', 'Бык', 'Лев', 'Орел');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (10, 'Какая страна первой отправила человека в космос?', 'СССР', 'США', 'Китай', 'Франция');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (11, 'Какое имя носил первый человек, ступивший на Луну?', 'Нил Армстронг', 'Юрий Гагарин', 'Алексей Леонов', 'Владимир Комаров');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (12, 'Какой город находится на двух континентах - Европе и Азии?', 'Стамбул', 'Анкара', 'Баку', 'Ереван');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (13, 'Какое растение является символом Шотландии?', 'Чертополох', 'Клевер', 'Роза', 'Ландыш');


INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (14, 'В какой стране родился футболист Криштиану Роналду?', 'Португалия', 'Бразилия', 'Аргентина', 'Испания');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (15, 'Кто изобрел телефон?', 'Александр Белл', 'Томас Эдисон', 'Никола Тесла', 'Альберт Эйнштейн');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (10, 'Какое животное известно как "корабль пустыни"?', 'Верблюд', 'Слон', 'Жираф', 'Зебра');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (12, 'Какое имя носит самый известный детектив, созданный Артуром Конан Дойлом?', 'Шерлок Холмс', 'Эркюль Пуаро', 'Лорд Питер Уимзи', 'Филип Марлоу');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (15, 'Какой предмет из игры "Тетрис" называется "Z-Тетромино"?', 'Красный блок', 'Синий блок', 'Зеленый блок', 'Желтый блок');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (8, 'Какая столица у Австралии?', 'Канберра', 'Сидней', 'Мельбурн', 'Брисбен');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (4, 'Что означает аббревиатура HTML?', 'HyperText Markup Language', 'High Tech Modern Language', 'Hyperlink and Text Management Language', 'Home Tool Markup Language');



-- Difficulty 1 (уже 1 вопрос → добавим ещё 4)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (1, 'Сколько будет 2 + 2?', '4', '3', '5', '6');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (1, 'Какой цвет получается при смешении синего и жёлтого?', 'Зелёный', 'Красный', 'Фиолетовый', 'Оранжевый');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (1, 'Сколько ног у паука?', '8', '6', '10', '12');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (1, 'Какой месяц идёт после марта?', 'Апрель', 'Май', 'Февраль', 'Июнь');


-- Difficulty 2 (уже 1 → добавим 4)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (2, 'Какой океан самый большой?', 'Тихий океан', 'Атлантический океан', 'Индийский океан', 'Северный Ледовитый океан');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (2, 'Какая планета известна как красная планета?', 'Марс', 'Венера', 'Юпитер', 'Сатурн');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (2, 'Сколько будет 5 × 6?', '30', '25', '36', '26');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (2, 'Какая самая высокая гора в мире?', 'Эверест', 'Килиманджаро', 'Эльбрус', 'Монблан');


-- Difficulty 3 (уже 1 → добавим 4)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (3, 'Кто изобрёл лампочку?', 'Томас Эдисон', 'Никола Тесла', 'Александр Белл', 'Джеймс Ватт');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (3, 'Какая планета самая большая в Солнечной системе?', 'Юпитер', 'Сатурн', 'Нептун', 'Уран');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (3, 'Какой элемент обозначается символом O?', 'Кислород', 'Золото', 'Олово', 'Серебро');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (3, 'Сколько сторон у треугольника?', '3', '4', '5', '6');


-- Difficulty 4 (уже 2 → добавим 3)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (4, 'Какой океан омывает Европу с запада?', 'Атлантический океан', 'Тихий океан', 'Индийский океан', 'Северный Ледовитый океан');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (4, 'Кто написал пьесу "Ромео и Джульетта"?', 'Уильям Шекспир', 'Мольер', 'Лев Толстой', 'Чарльз Диккенс');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (4, 'Какой город является столицей Италии?', 'Рим', 'Милан', 'Флоренция', 'Венеция');


-- Difficulty 5 (уже 1 → добавим 4)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (5, 'Сколько лет длится високосный цикл?', '4 года', '3 года', '5 лет', '6 лет');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (5, 'Кто написал роман "Преступление и наказание"?', 'Федор Достоевский', 'Антон Чехов', 'Лев Толстой', 'Иван Тургенев');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (5, 'Как называется столица Канады?', 'Оттава', 'Торонто', 'Монреаль', 'Ванкувер');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (5, 'Кто считается автором теории относительности?', 'Альберт Эйнштейн', 'Исаак Ньютон', 'Галилео Галилей', 'Никола Тесла');


-- Difficulty 6 (уже 1 → добавим 4)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (6, 'Кто написал пьесу "Чайка"?', 'Антон Чехов', 'Иван Тургенев', 'Александр Островский', 'Николай Гоголь');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (6, 'Какая страна изобрела бумагу?', 'Китай', 'Индия', 'Египет', 'Греция');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (6, 'Кто был первым президентом США?', 'Джордж Вашингтон', 'Авраам Линкольн', 'Томас Джефферсон', 'Джон Адамс');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (6, 'Кто написал "Мастера и Маргариту"?', 'Михаил Булгаков', 'Илья Ильф', 'Алексей Толстой', 'Максим Горький');


-- Difficulty 7 (уже 1 → добавим 4)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (7, 'Кто был первым человеком в космосе?', 'Юрий Гагарин', 'Нил Армстронг', 'Алексей Леонов', 'Герман Титов');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (7, 'Какая страна известна как "страна восходящего солнца"?', 'Япония', 'Китай', 'Корея', 'Вьетнам');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (7, 'Кто написал роман "1984"?', 'Джордж Оруэлл', 'Олдос Хаксли', 'Фрэнц Кафка', 'Артур Конан Дойл');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (7, 'Какая столица Бразилии?', 'Бразилиа', 'Рио-де-Жанейро', 'Сан-Паулу', 'Сальвадор');


-- Difficulty 8 (уже 2 → добавим 3)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (8, 'Какая валюта используется в Японии?', 'Иена', 'Юань', 'Вон', 'Доллар');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (8, 'Как называется столица Кении?', 'Найроби', 'Каир', 'Дакар', 'Лагос');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (8, 'Кто является автором симфонии №9 "Ода к радости"?', 'Людвиг ван Бетховен', 'Йозеф Гайдн', 'Вольфганг Амадей Моцарт', 'Иоганн Себастьян Бах');


-- Difficulty 9 (уже 1 → добавим 4)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (9, 'Какая столица Канады?', 'Оттава', 'Торонто', 'Монреаль', 'Ванкувер');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (9, 'Кто написал "Гамлета"?', 'Уильям Шекспир', 'Данте Алигьери', 'Джон Мильтон', 'Джон Локк');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (9, 'Какая страна является родиной Олимпийских игр?', 'Греция', 'Италия', 'Франция', 'Испания');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (9, 'Какая планета известна своими кольцами?', 'Сатурн', 'Юпитер', 'Нептун', 'Уран');


-- Difficulty 10 (уже 2 → добавим 3)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (10, 'Кто написал "Одиссею"?', 'Гомер', 'Софокл', 'Эсхил', 'Платон');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (10, 'Какая столица Египта?', 'Каир', 'Александрия', 'Гиза', 'Луксор');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (10, 'Кто является автором картины "Мона Лиза"?', 'Леонардо да Винчи', 'Микеланджело', 'Рафаэль', 'Боттичелли');


-- Difficulty 11 (уже 1 → добавим 4)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (11, 'Кто написал роман "Анна Каренина"?', 'Лев Толстой', 'Федор Достоевский', 'Антон Чехов', 'Иван Тургенев');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (11, 'Какая столица Канады?', 'Оттава', 'Монреаль', 'Торонто', 'Ванкувер');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (11, 'Кто написал "Божественную комедию"?', 'Данте Алигьери', 'Джон Мильтон', 'Петрарка', 'Боккаччо');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (11, 'Какая самая маленькая страна в мире?', 'Ватикан', 'Монако', 'Сан-Марино', 'Лихтенштейн');


-- Difficulty 12 (уже 2 → добавим 3)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (12, 'Кто является богом грома в греческой мифологии?', 'Зевс', 'Арес', 'Посейдон', 'Аполлон');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (12, 'Какая столица Турции?', 'Анкара', 'Стамбул', 'Измир', 'Бурса');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (12, 'Кто написал роман "Три мушкетёра"?', 'Александр Дюма', 'Виктор Гюго', 'Оноре де Бальзак', 'Жюль Верн');


-- Difficulty 13 (уже 1 → добавим 4)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (13, 'Кто написал трагедию "Царь Эдип"?', 'Софокл', 'Эсхил', 'Еврипид', 'Аристофан');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (13, 'Какая планета имеет самый длинный день?', 'Венера', 'Меркурий', 'Марс', 'Юпитер');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (13, 'Кто изобрёл печатный станок?', 'Иоганн Гутенберг', 'Леонардо да Винчи', 'Роберт Фултон', 'Джеймс Уатт');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (13, 'Какое море омывает Италию с востока?', 'Адриатическое море', 'Тирренское море', 'Эгейское море', 'Средиземное море');


-- Difficulty 14 (уже 1 → добавим 4)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (14, 'Кто был первым императором Римской империи?', 'Октавиан Август', 'Юлий Цезарь', 'Нерон', 'Тиберий');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (14, 'Какая страна изобрела бумажные деньги?', 'Китай', 'Индия', 'Египет', 'Персия');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (14, 'Кто написал роман "Фауст"?', 'Иоганн Вольфганг Гёте', 'Фридрих Шиллер', 'Иммануил Кант', 'Лессинг');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (14, 'Какая столица Индии?', 'Нью-Дели', 'Мумбаи', 'Калькутта', 'Ченнаи');


-- Difficulty 15 (уже 2 → добавим 3)
INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (15, 'Какая наука изучает звезды и планеты?', 'Астрономия', 'Геология', 'Физика', 'Химия');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (15, 'Кто был первым человеком, увидевшим спутники Юпитера через телескоп?', 'Галилео Галилей', 'Исаак Ньютон', 'Коперник', 'Кеплер');

INSERT INTO Questions (Difficulty, QuestionText, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3)
VALUES (15, 'Какая столица Швеции?', 'Стокгольм', 'Осло', 'Копенгаген', 'Хельсинки');




using System;
using System.Media;
using System.Threading.Channels;

class Program
{
    public const int questionCount = 10;
    public const int questionAnswersCount = 4;
    public const int immutableSumCount = 2;

    public static bool firstFool = true;
    public static bool fiftyFift = true;
    public static bool friendHelp = true;
    public static bool audienceHelp = true;


    public static string[] questions = new string[questionCount]
    {
        "Какого цвета трава летом?",
        "Сколько будет 2 + 2?",
        "Какая планета ближе всего к Солнцу?",
        "Кто написал роман «Преступление и наказание»?",
        "Какая страна самая большая по площади?",
        "Как называется химический элемент с символом «Au»?",
        "Кто первым вышел в открытый космос?",
        "В каком году распался Советский Союз?",
        "Какой элемент назван в честь планеты?",
        "Кто автор картины «Тайная вечеря»?",
    };

    public static string[,] questionAnswers = new string[questionCount, questionAnswersCount]
    {
       { "Жёлтая", "Синяя", "Красная", "Зелёная" },
       { "5", "4", "3", "6" },
       { "Земля", "Венера", "Меркурий", "Марс" },
       { "Фёдор Достоевский", "Лев Толстой", "Антон Чехов", "Николай Гоголь" },
       { "Россия", "США", "Канада", "Китай" },
       { "Серебро", "Золото", "Железо", "Медь" },
       { "Нил Армстронг", "Юрий Гагарин", "Алексей Леонов", "Герман Титов" },
       { "1990", "1989", "1991", "1992" },
       { "Водород", "Неон", "Гелий", "Уран" },
       { "Рафаэль", "Леонардо да Винчи", "Микеланджело", "Донателло" }
    };

    public static int[] questionCorrectAnswersIndex = new int[questionCount]
    {
        4,2,3,1,1,2,3,3,4,2
    };

    public static int[] questionRewards = new int[questionCount]
    {
       500 , 2_000, 10_000, 25_000,50_000,100_000, 400_000,800_000,1_500_000,3_000_000
    };



    public static void ShowAllQuestions()
    {
        for (int i = 0; i < questionCount; ++i)
        {
            Console.WriteLine(questions[i]);
        }
    }
    public static void ShowAllAnswers()
    {
        for (int i = 0; i < questionCount; ++i)
        {
            char option = 'A';
            for (int j = 0; j < questionAnswersCount; ++j)
            {
                Console.WriteLine($"{option}) {questionAnswers[i, j]}");
            }
            Console.WriteLine();
        }
    }
    public static void ShowQuestionByIndex(int i)
    {
        Console.WriteLine(questions[i]);
    }
    public static void ShowAnswersByIndex(int i)
    {
        char option = 'A';
        for (int j = 0; j < questionAnswersCount; ++j, ++option)
        {
            Console.WriteLine($"{option}) {questionAnswers[i, j]}");
        }

    }
    public static void ShowQuestionAndAnswersByIndex(int indexOfQuestion)
    {

        ShowQuestionByIndex(indexOfQuestion);
        ShowAnswersByIndex(indexOfQuestion);

    }
    public static void ShowAllQuestionsAndAnswers()
    {
        for (int i = 0; i < questionCount; ++i)
        {
            ShowQuestionByIndex(i);
            ShowAnswersByIndex(i);
        }


    }



    public static void StartMenu()
    {


        Console.WriteLine("Добро пожаловать на игру кто хочет стать миллионером! ");
        Console.WriteLine("У вас будет 4 подсказки ( 50\\50 , помощь зала, помощь друга)");
        Console.WriteLine("Чтобы открыть подсказки во время игры нажмете Букву 'H' ");
        Console.WriteLine("Если готовы играть нажмите 'Enter' ");


        while (true)
        {
            string enter = Console.ReadLine();
            if (enter == "")
            {
                Console.WriteLine("И так игра началась удачи! ");
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Неверный ввод!");
                Console.WriteLine("Если готовы играть нажмите 'Enter' ");

            }

        }


    }
    public static int HintsMenu(int questionOrder, out char userAnswer)
    {
        userAnswer = default;
        Console.WriteLine("Выберите какую подсказку хотите выбрать - ");
        Console.WriteLine("1) 50\\50");
        Console.WriteLine("2) Помощь зала");
        Console.WriteLine("3) Помощь друга");
        Console.WriteLine("4) Выйти из меню подсказок");

        while (true) {

        int inputHint = Convert.ToInt32(Console.ReadLine());

        if(inputHint == 1)
        {
            if(fiftyFift == true)
            {
                FiftyFift(questionOrder);
                fiftyFift = false;
                    break;
            }
            else
                Console.WriteLine("Вы уже использвали подсказку - 50\\50!");
        }

        else if(inputHint == 2)
        {
            if (audienceHelp == true)
            {
                AudienceHelp(questionOrder);
                audienceHelp = false;
                    break;
                }
            else
                Console.WriteLine("Вы уже использвали подсказку - Помощь зала!");
        }

        else if(inputHint == 3)
        {
            if (friendHelp == true)
            {
                FriendHelp(questionOrder);
                friendHelp = false;
                    break;
                }
            else
                Console.WriteLine("Вы уже использвали подсказку - Помощь друга!");
        }
        else if(inputHint == 4)
            { 
                return 1;
            }
         else
            {
                Console.WriteLine("Выберите диапозон от 1 до 4 (включительно) !");
            }


            

        }

        userAnswer = InputAnswer();
        return 0;


    }

    public static void FiftyFift(int questionOrder)
    {
        Console.WriteLine(questions[questionOrder]);

        Random rnd = new Random();

        int random = questionCorrectAnswersIndex[questionOrder];
         while (random == questionCorrectAnswersIndex[questionOrder])
        {
            random = rnd.Next(1, 5);
            Console.WriteLine($"{(char)(64 + random)}) {questionAnswers[questionOrder, random - 1]}");
        }
        Console.WriteLine($"{(char)(64 + questionCorrectAnswersIndex[questionOrder])}) {questionAnswers[questionOrder, questionCorrectAnswersIndex[questionOrder] - 1]}");


    }
    public static void FriendHelp(int questionOrder)
    {
        Random rnd = new Random();

        int RightOrFalse = rnd.Next(1, 11);

        Console.WriteLine("Друг думмает что правильный ответ это - ");
        if (RightOrFalse == 1)
        {
      
            Random falseRnd = new Random();
            int random = questionCorrectAnswersIndex[questionOrder];
            while (random == questionCorrectAnswersIndex[questionOrder])
            {
                random = falseRnd.Next(1, 5);
                Console.WriteLine($"{(char)(64 + random)}) {questionAnswers[questionOrder, random - 1]}");
            }
        }
        else
        {
            Console.WriteLine($"{(char)(64 + questionCorrectAnswersIndex[questionOrder])}) {questionAnswers[questionOrder, questionCorrectAnswersIndex[questionOrder] - 1]}");
        }
    }
    public static void AudienceHelp(int questionOrder)
    {
        Console.WriteLine("Помощь зала распределила свои голоса следующим образом:");

        Random rnd = new Random();

   
        int correctAnswerIndex = questionCorrectAnswersIndex[questionOrder] - 1;

        
        int correctAnswerPercentage = rnd.Next(50, 81);

        
        int remaining = 100 - correctAnswerPercentage;

        
        int first = rnd.Next(0, remaining + 1);
        int second = rnd.Next(0, remaining - first + 1);
        int third = remaining - first - second;

        int[] distribution = new int[4];
        distribution[correctAnswerIndex] = correctAnswerPercentage;

        
        int distIndex = 0;
        for (int i = 0; i < 4; i++)
        {
            if (i == correctAnswerIndex)
                continue;

            if (distIndex == 0)
                distribution[i] = first;
            else if (distIndex == 1)
                distribution[i] = second;
            else
                distribution[i] = third;

            distIndex++;
        }

       
        for (int i = 0; i < 4; i++)
        {
            char option = (char)(65 + i);
            Console.WriteLine($"{distribution[i]}% - {option}) {questionAnswers[questionOrder, i]}");
        }
    } // ** прочти



    public static void ImmutableSum(out int[]  immutableSum)
    {
        immutableSum = new int[immutableSumCount];

        Console.WriteLine("Выберите Несгораемую сумму (Пример - 100_000): ");
        
        for(int questionRewardsIndex = 0; questionRewardsIndex < questionCount; ++questionRewardsIndex)
        {
            Console.WriteLine($"{questionRewardsIndex+1}) Вопрос на {questionRewards[questionRewardsIndex]} рублей ");
        }

        

        for(int i = 0; i < immutableSumCount; ++i)
        {
            Console.Write($"{i+1}) Несгораемая сумма: ");
            immutableSum[i] = Convert.ToInt32(Console.ReadLine());
        }
       

    }



    public static char InputAnswer()
    {
        string inputUser = Console.ReadLine();
        char userAnswer;

        while (!CheckCorrectCharInput(inputUser, out userAnswer))
        {
            Console.WriteLine("Пожалуйста, введите корректный ответ (A, B, C, D, H — подсказка, L — забрать деньги):");
            inputUser = Console.ReadLine();
        }

        return userAnswer;
    }

    public static bool CheckUserAnswer(char userAnswer , int questionOrder, int[] immutableSum)
    {
        return CheckIsCorrectUserAnswer(userAnswer, questionOrder) ? true : false;
    }
    public static bool CheckCorrectCharInput(string input, out char userInputAnswer)
    {
        userInputAnswer = default;

        if (string.IsNullOrWhiteSpace(input) || input.Length != 1)
        {
            return false;
        }

        char ch = char.ToUpper(input[0]);

        if (!"ABCDHL".Contains(ch))
            return false;

        userInputAnswer = ch;
        return true;
    }
    // ** прочти
    public static bool CheckIsCorrectUserAnswer(char userAnswer,int questionOrder)
    {
        

        int correctAnswerInInt = questionCorrectAnswersIndex[questionOrder];

        if(! (userAnswer - 64 == correctAnswerInInt) ) //
        {
           return false;
        }
        return true;
    }  // -- улучши
    

    public static void LooseMenu(int questionOrder, int[] immutableSum)
    {
   


        Console.WriteLine("К сожалению вы проиграли в нашей игре");

        int maxImmutable = 0;

        for (int i = 0; i < immutableSum.Length; ++i)
        {
            if (immutableSum[i] <= questionRewards[questionOrder])
            {
                maxImmutable = immutableSum[i];
            }
        }

        if (maxImmutable > 0)
        {
            Console.WriteLine($"Но за то вы выиграли несгораемую сумму {maxImmutable} рублей\nПоздравляем!");
        }
        else
        {
            Console.WriteLine("И ничего не выиграли");
        }
    }
    public static bool IsFirstFool()
    {
        if (firstFool)
        {
            Console.WriteLine("Вы потеряли одно право на ошибку!");
            firstFool = false;
            return true;
        }
        return false;
    }

    public static void WinMenu(int questionOrder)
    {
        Console.WriteLine("И это правильный ответ на вопрос! ");
    }

    public static void WinGameMenu()
    {
        Console.WriteLine("Поздравляем вас участник, вы ответили на все вопросы и стали победителем шоу Кто Хочет Стать Миллионером!");
    }

    public static void TakeMoneyMenu(int questionOrder)
    {
        int reward = questionOrder == 0 ? 0 : questionRewards[questionOrder - 1];
        Console.WriteLine($"Вы решили забрать деньги. Ваш выигрыш составил: {reward} рублей.");
        Console.WriteLine("Спасибо за игру! До свидания.");
    }









    public static void PlayGame()
    {

        StartMenu();

        ImmutableSum(out int[] immutableSum);

        Console.Clear();


            for (int questionOrder = 0; questionOrder < questionCount; ++questionOrder)
            {
                Console.WriteLine($"{questionOrder + 1}) Вопрос на {questionRewards[questionOrder]} рублей - ");



                ShowQuestionByIndex(questionOrder);

                ShowAnswersByIndex(questionOrder);

                Console.Write("Ваш ответ: ");
            char userAnswer = InputAnswer();
            Console.WriteLine();

            if (userAnswer == 'H')
            {
                int check = HintsMenu(questionOrder, out char userAnswerAfterHint);
                if (check == 1)
                {
                    questionOrder--;
                    continue;
                }
                else
                {
                    userAnswer = userAnswerAfterHint;
                }
            }
            else if (userAnswer == 'L')
            {
                TakeMoneyMenu(questionOrder);
                return;
            }


            else
            {
                if (CheckUserAnswer(userAnswer, questionOrder, immutableSum))
                {
                    WinMenu(questionOrder);
                }
                else
                {
                    if (IsFirstFool())
                    {
                        questionOrder--;
                        continue;

                    }
                    else
                    {
                        LooseMenu(questionOrder, immutableSum);
                        return;
                    }
                }
            }





            }
        


        WinGameMenu();


    }
    public static void Main()
    {
         PlayGame();
        //throw new Exception("dwad");




    }

}
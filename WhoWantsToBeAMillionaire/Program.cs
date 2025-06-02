using System;
using System.Media;
using System.Threading.Channels;



// Iznhacalno vse stroki bili na russkom yazike, perevod ot chatgpt ( na slucay esli budut oshibki v perevode)
// prosto github chasto ruqaetsya na kiriliccu

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
        "What color is grass in summer?",
        "What is 2 + 2?",
        "Which planet is closest to the Sun?",
        "Who wrote the novel 'Crime and Punishment'?",
        "Which country is the largest by area?",
        "What is the chemical element with the symbol 'Au'?",
        "Who was the first to go on a spacewalk?",
        "In which year did the Soviet Union collapse?",
        "Which element is named after a planet?",
        "Who is the author of the painting 'The Last Supper'?",
    };

    public static string[,] questionAnswers = new string[questionCount, questionAnswersCount]
    {
       { "Yellow", "Blue", "Red", "Green" },
       { "5", "4", "3", "6" },
       { "Earth", "Venus", "Mercury", "Mars" },
       { "Fyodor Dostoevsky", "Leo Tolstoy", "Anton Chekhov", "Nikolai Gogol" },
       { "Russia", "USA", "Canada", "China" },
       { "Silver", "Gold", "Iron", "Copper" },
       { "Neil Armstrong", "Yuri Gagarin", "Alexei Leonov", "Gherman Titov" },
       { "1990", "1989", "1991", "1992" },
       { "Hydrogen", "Neon", "Helium", "Uranium" },
       { "Raphael", "Leonardo da Vinci", "Michelangelo", "Donatello" }
    };

    public static int[] questionCorrectAnswersIndex = new int[questionCount]
    {
        4, 2, 3, 1, 1, 2, 3, 3, 4, 2
    };

    public static int[] questionRewards = new int[questionCount]
    {
       500 , 2_000, 10_000, 25_000, 50_000, 100_000, 400_000, 800_000, 1_500_000, 3_000_000
    };

    public static void StartMenu()
    {
        Console.WriteLine("Welcome to the Who Wants to Be a Millionaire game!");
        Console.WriteLine("You will have 3 lifelines (50/50, audience help, friend help)");
        Console.WriteLine("To open lifelines during the game press the letter 'H'");
        Console.WriteLine("If you want to quit the game early press 'L'");
        Console.WriteLine("If you are ready to play press 'Enter'");
        Console.WriteLine("If you are not ready to play press any other key");
    }
    public static bool IsUserWantToPlay()
    {
        string enter = Console.ReadLine();

        if (enter != "")
            return false;

        Console.WriteLine("So, the game has started! Good luck!");
        return true;
    }
    public static void ImmutableSum(out int[] immutableSums)
    {
        Console.WriteLine("Choose your guaranteed sums (Example - 100_000): ");

        immutableSums = new int[immutableSumCount];

        for (int questionRewardsNumber = 0; questionRewardsNumber < questionCount; ++questionRewardsNumber)
        {
            Console.WriteLine($"{questionRewardsNumber + 1}) Question for {questionRewards[questionRewardsNumber]} rubles");
        }

        for (int immutableSumNumber = 0; immutableSumNumber < immutableSumCount; ++immutableSumNumber)
        {
            Console.Write($"{immutableSumNumber + 1}) Guaranteed sum: ");
            immutableSums[immutableSumNumber] = Convert.ToInt32(Console.ReadLine());
        }
    }

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

    public static char InputAnswer()
    {
        char inputUserAnswer = Convert.ToChar(Console.ReadLine());

        char userAnswer;

        while (!CheckCorrectCharInput(inputUserAnswer, out userAnswer))
        {
            Console.WriteLine("Please enter a correct answer (A, B, C, D, H — lifeline, L — take money):");
            inputUserAnswer = Convert.ToChar(Console.ReadLine());
        }

        return userAnswer;
    }

    public static bool CheckCorrectCharInput(char inputUserAnswer, out char userAnswer)
    {
        userAnswer = default;

        if (char.IsWhiteSpace(inputUserAnswer))
            return false;

        char ch = char.ToUpper(inputUserAnswer);

        if (!"ABCDHL".Contains(ch))
            return false;

        userAnswer = ch;
        return true;
    }

    public static int HintsMenu(int questionOrder, out char userAnswer)
    {
        userAnswer = default;
        Console.WriteLine("Choose which lifeline you want to use - ");
        Console.WriteLine("1) 50/50");
        Console.WriteLine("2) Audience help");
        Console.WriteLine("3) Friend help");
        Console.WriteLine("4) Exit lifelines menu");

        while (true)
        {
            int inputHint = Convert.ToInt32(Console.ReadLine());

            if (inputHint == 1)
            {
                if (fiftyFift == true)
                {
                    FiftyFift(questionOrder);
                    fiftyFift = false;
                    break;
                }
                else
                    Console.WriteLine("You have already used the 50/50 lifeline!");
            }
            else if (inputHint == 2)
            {
                if (audienceHelp == true)
                {
                    AudienceHelp(questionOrder);
                    audienceHelp = false;
                    break;
                }
                else
                    Console.WriteLine("You have already used the Audience Help lifeline!");
            }
            else if (inputHint == 3)
            {
                if (friendHelp == true)
                {
                    FriendHelp(questionOrder);
                    friendHelp = false;
                    break;
                }
                else
                    Console.WriteLine("You have already used the Friend Help lifeline!");
            }
            else if (inputHint == 4)
            {
                return 1;
            }
            else
            {
                Console.WriteLine("Please choose a number between 1 and 4 (inclusive)!");
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

        Console.WriteLine("Your friend thinks the correct answer is - ");
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
        Console.WriteLine("Audience distributed their votes as follows:");

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
    }

    public static bool CheckUserAnswer(char userAnswer, int questionOrder, int[] immutableSum)
    {
        return CheckIsCorrectUserAnswer(userAnswer, questionOrder) ? true : false;
    }

    public static bool CheckIsCorrectUserAnswer(char userAnswer, int questionOrder)
    {
        int correctAnswerInInt = questionCorrectAnswersIndex[questionOrder];

        if (!(userAnswer - 64 == correctAnswerInInt))
        {
            return false;
        }
        return true;
    }

    public static void LooseMenu(int questionOrder, int[] immutableSum)
    {
        Console.WriteLine("Unfortunately, you lost in our game.");

        int maxImmutable = 0;

        for (int i = 0; i < immutableSum.Length; ++i)
        {
            if (immutableSum[i] < questionRewards[questionOrder])
            {
                maxImmutable = immutableSum[i];
            }
        }

        if (maxImmutable > 0)
        {
            Console.WriteLine($"But you won a guaranteed sum of {maxImmutable} rubles.\nCongratulations!");
        }
        else
        {
            Console.WriteLine("And you didn't win anything.");
        }
    }
    public static bool IsFirstFool()
    {
        if (firstFool)
        {
            Console.WriteLine("You lost one chance to make a mistake!");
            firstFool = false;
            return true;
        }
        return false;
    }

    public static void WinMenu(int questionOrder)
    {
        Console.WriteLine("That's the correct answer to the question!");
    }

    public static void WinGameMenu()
    {
        Console.WriteLine("Congratulations contestant, you answered all the questions and became the winner of 'Who Wants to Be a Millionaire!'");
    }

    public static void TakeMoneyMenu(int questionOrder)
    {
        int reward = questionOrder == 0 ? 0 : questionRewards[questionOrder - 1];
        Console.WriteLine($"You decided to take the money. Your winnings: {reward} rubles.");
        Console.WriteLine("Thank you for playing! Goodbye.");
    }

    public static void PlayGame()
    {
        StartMenu();

        if (!IsUserWantToPlay())
            return;

        ImmutableSum(out int[] immutableSum);

        Console.Clear();

        for (int questionOrder = 0; questionOrder < questionCount; ++questionOrder)
        {
            Console.WriteLine($"{questionOrder + 1}) Question for {questionRewards[questionOrder]} rubles - ");

            ShowQuestionAndAnswersByIndex(questionOrder);

            Console.Write("Your answer: ");

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
    }
}

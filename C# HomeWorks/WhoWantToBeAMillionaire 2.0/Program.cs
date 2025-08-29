
using WhoWantToBeAMillionaire_2._0.Models;

public class Program
{
    // private static readonly string connectionString = "Server=localhost;Database=MillionaireGame;Integrated Security=True;TrustServerCertificate=True;";

    public static void Main()
    {
        var userRepository = new UserRepository();
        var sessionRepository = new GameSessionRepository();
        var questionRepository = new QuestionRepository();
        var leaderboardRepository = new LeaderboardRepository();

        var userService = new UserService(userRepository);
        var gameService = new GameService(  userService,    sessionRepository,  questionRepository,  leaderboardRepository,  userRepository
        );

        Console.WriteLine("Добро пожаловать в игру 'Кто хочет стать миллионером'!");

        while (true)
        {
            Console.WriteLine("\nВведите ваше имя для входа:");
            var loginName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(loginName))
            {
                Console.WriteLine("Имя не может быть пустым. Попробуйте снова.");
                continue;
            }

            Console.WriteLine($"\nАвторизация как '{loginName}'...");
            var session = gameService.StartOrResumeGame(loginName);
            Console.WriteLine($"Сессия загружена. Текущий выигрыш: {session.CurrentWinnings:C}");

            while (!session.IsFinished)
            {
                var question = gameService.GetNextQuestion(session);

                if (question == null)
                {
                    Console.WriteLine("Похоже, вопросы закончились! Поздравляем, ты прошёл игру!");
                    session.IsFinished = true;
                    gameService.SubmitAnswer(session, 0, "");
                    break;
                }

                Console.WriteLine($"\nВопрос #{session.CurrentQuestionIndex} за {MillionaireRules.GetWinnings(session.CurrentQuestionIndex - 1):C}:");
                Console.WriteLine(question.QuestionText);

                var answers = new List<string> { question.CorrectAnswer, question.IncorrectAnswer1, question.IncorrectAnswer2, question.IncorrectAnswer3 };
                var shuffledAnswers = answers.OrderBy(a => Guid.NewGuid()).ToList();

                for (int i = 0; i < shuffledAnswers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {shuffledAnswers[i]}");
                }

                Console.Write("Введите номер ответа: ");
                if (int.TryParse(Console.ReadLine(), out int answerIndex) && answerIndex >= 1 && answerIndex <= 4)
                {
                    var userAnswer = shuffledAnswers[answerIndex - 1];
                    session = gameService.SubmitAnswer(session, question.Id, userAnswer);

                    if (session.IsFinished)
                    {
                        Console.WriteLine("Ответ неверный. Игра окончена.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Верно! Переходим к следующему вопросу.");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Игра окончена.");
                    session.IsFinished = true;
                    gameService.SubmitAnswer(session, question.Id, "неверный ввод");
                    break;
                }
            }

            Console.WriteLine($"\nИгра завершена. Ваш итоговый выигрыш: {session.CurrentWinnings:C}");

            Console.WriteLine("\n--- Таблица лидеров ---");
            var topScores = gameService.GetTopScores();
            if (topScores.Any())
            {
                int rank = 1;
                foreach (var entry in topScores)
                {
                    Console.WriteLine($"{rank++}. {entry.LoginName}: {entry.FinalScore:C}");
                }
            }
            else
            {
                Console.WriteLine("Таблица лидеров пока пуста.");
            }

            Console.WriteLine("\nНажмите Enter, чтобы сыграть снова");
            Console.ReadLine();
        }
    }
}
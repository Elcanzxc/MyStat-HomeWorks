using WhoWantToBeAMillionaire_2._0.Entities;
using WhoWantToBeAMillionaire_2._0.Models;

public class GameService
{
    private readonly UserService userService;
    private readonly GameSessionRepository sessionRepository;
    private readonly QuestionRepository questionRepository;
    private readonly LeaderboardRepository leaderboardRepository;
    private readonly UserRepository userRepository; 


    public GameService(  UserService userService, GameSessionRepository sessionRepository,QuestionRepository questionRepository,LeaderboardRepository leaderboardRepository,UserRepository userRepository)
    {
        this.userService = userService;
        this.sessionRepository = sessionRepository;
        this.questionRepository = questionRepository;
        this.leaderboardRepository = leaderboardRepository;
        this.userRepository = userRepository;
    }

    public GameSession StartOrResumeGame(string loginName)
    {
        var user = userService.Login(loginName);
        var session = sessionRepository.FindActiveSessionByUserId(user.Id);

        if (session == null)
        {
            session = new GameSession
            {
                UserId = user.Id,
                CurrentQuestionIndex = 1,
                CurrentWinnings = 0,
                IsFinished = false,
                StartTime = DateTime.Now,
                UsedQuestionIds = string.Empty
            };
            session.Id = sessionRepository.CreateGameSession(session);
        }

        return session;
    }

    public Question GetNextQuestion(GameSession session)
    {
        var difficultyLevel = session.CurrentQuestionIndex;
        var usedIds = string.IsNullOrWhiteSpace(session.UsedQuestionIds)
            ? new List<int>()
            : session.UsedQuestionIds.Split(',').Select(int.Parse).ToList();

        var nextQuestion = questionRepository.GetRandomQuestionByDifficulty(difficultyLevel, usedIds);

        if (nextQuestion != null)
        {
            usedIds.Add(nextQuestion.Id);
            session.UsedQuestionIds = string.Join(",", usedIds);
            sessionRepository.UpdateGameSession(session);
        }

        return nextQuestion;
    }

    public GameSession SubmitAnswer(GameSession session, int questionId, string answer)
    {
        var question = questionRepository.GetQuestionById(questionId);

        if (question != null && answer.Trim().Equals(question.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            var nextIndex = session.CurrentQuestionIndex + 1;
            session.CurrentWinnings = MillionaireRules.GetWinnings(nextIndex - 1);
            session.CurrentQuestionIndex = nextIndex;

            if (session.CurrentQuestionIndex > 15)
            {
                session.IsFinished = true;
                session.EndTime = DateTime.Now;
                UpdateLeaderboard(session);
            }
        }
        else
        {
            session.IsFinished = true;
            session.EndTime = DateTime.Now;
        }

        sessionRepository.UpdateGameSession(session);

        return session;
    }

    private void UpdateLeaderboard(GameSession finishedSession)
    {
        if (finishedSession.CurrentWinnings > 0)
        {
            var existingEntry = leaderboardRepository.GetLeaderboardEntryByUserId(finishedSession.UserId);

            if (existingEntry != null)
            {
                if (finishedSession.CurrentWinnings > existingEntry.FinalScore)
                {
                    existingEntry.FinalScore = finishedSession.CurrentWinnings;
                    existingEntry.GameEndTime = finishedSession.EndTime.Value;
                    leaderboardRepository.UpdateLeaderboardEntry(existingEntry);
                }
            }
            else
            {
                var newEntry = new Leaderboard
                {
                    UserId = finishedSession.UserId,
                    FinalScore = finishedSession.CurrentWinnings,
                    GameEndTime = finishedSession.EndTime.Value
                };
                leaderboardRepository.CreateLeaderboardEntry(newEntry);
            }
        }
    }
    public IEnumerable<LeaderboardEntryDto> GetTopScores()
    {
        return leaderboardRepository.GetTopScoresWithLoginNames();
    }
}


public static class MillionaireRules
{
    private static readonly decimal[] winnings = { 100, 200, 300, 500, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 250000, 500000, 1000000 };

    public static decimal GetWinnings(int questionIndex)
    {
        if (questionIndex >= 0 && questionIndex < winnings.Length)
        {
            return winnings[questionIndex];
        }
        return 0;
    }
}

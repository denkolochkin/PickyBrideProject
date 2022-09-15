using PickyBrideProblem.Entity;
using PickyBrideProblem.Dto;

using System.Configuration;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace PickyBrideProblem.Service
{
    public class Princess : IHostedService
    {
        private readonly int ContendersCount =
            int.Parse(ConfigurationManager.AppSettings["ContendersCount"]);

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IHostApplicationLifetime _appLifetime;

        private readonly Hall _hall;
        private readonly Friend _friend;
        private readonly ContenderGenerator _generator;

        private readonly List<Contender> FirstContenders = new();

        private int DatesCount = 1;

        public Princess(IHostApplicationLifetime appLifetime,
        Hall hall, Friend friend, ContenderGenerator generator)
        {
            _appLifetime = appLifetime;
            _hall = hall;
            _friend = friend;
            _generator = generator;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(() =>
                {
                    try
                    {
                        _hall.contenders = _generator.GenerateContenders();

                        for (int i = 0; i < ContendersCount; i++)
                        {
                            Contender contender = _hall.GetNextContender();
                            _friend.ProcessedContenders.Add(contender);
                            PrincessAnswer princessAnswer = DateContender(contender, _friend);
                            if (ConfigurationManager.AppSettings["PositiveAnswer"].Equals(princessAnswer.Answer))
                            {
                                break;
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        log.Error(e.StackTrace);
                        _appLifetime.StopApplication();
                    }
                    finally
                    {
                        _appLifetime.StopApplication();
                    }
                });
            });

            return Task.CompletedTask;
        }

        public PrincessAnswer DateContender(Contender contender, Friend friend)
        {
            LogStatus(contender);

            friend.ProcessedContenders.Add(contender);

            if (DatesCount < (ContendersCount / Math.E))
            {
                FirstContenders.Add(contender);
            }
            else 
            {
                Contender bestOfFirst = FindBestOfFirst(friend);

                if (!friend.Compare(contender, bestOfFirst).Equals(bestOfFirst)
                    || DatesCount.Equals(ContendersCount))
                {
                    LogResult(contender);
                    return new PrincessAnswer
                    {
                        Answer = ConfigurationManager.AppSettings["PositiveAnswer"],
                        Quality = contender.Quality
                    };
                }
            }

            DatesCount++;

            return new PrincessAnswer
            {
                Answer = ConfigurationManager.AppSettings["NegativeAnswer"]
            };
        }

        private Contender FindBestOfFirst(Friend friend)
        {
            Contender bestContenter = FirstContenders.First();
            for (int i = 0; i < FirstContenders.Count; i++)
            {
                if (!bestContenter.Equals(friend.Compare(bestContenter, FirstContenders[i])))
                {
                    bestContenter = FirstContenders[i];
                }
            }
            return bestContenter;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void LogStatus(Contender contender)
        {
            log.Info("Date #" + DatesCount
                + " : " + contender.Name + " " + contender.Lastname);
        }

        private void LogResult(Contender contender)
        {
            log.Info("Result: " + contender.Quality + " Dates count: " + DatesCount);
        }
    }
}


using LenaSoft.Converters;
using LenaSoft.Interfaces;
using LenaSoft.Models;
using System.Data;

namespace LenaSoft
{
    public class Executor
    {
        private List<TradingSessions> listTradingSessions;
        private List<ContractInfo> listContractInfo;
        public Executor(List<TradingSessions> listTradingSessions, List<ContractInfo> listContractInfo) 
        {
            this.listTradingSessions = listTradingSessions;
            this.listContractInfo = listContractInfo;
        }

        public IEnumerable<Session> GetSessionsOnDate(DateTime date)
        {
            var result = new List<Session>();

            foreach (var session in listTradingSessions) 
            {
                if (session.StartTime <= date && date < session.EndTime)
                    yield return new Session
                    {
                        Name = session.Name,
                        Pit = session.Pit,
                        StartTime = session.StartTime,
                        EndTime = session.EndTime,
                        BusinessDay = session.TradingDate
                    };
            }

            if (result.Count == 0)
                foreach (var contract in listContractInfo)
                {
                    if (contract.StartTime <= date && date < contract.EndTime)
                        yield return new Session
                        {
                            Name = contract.Name,
                            Pit = contract.Pit,
                            StartTime = contract.StartTime,
                            EndTime = contract.EndTime
                        };
                }

            yield break;
        }

        public DateTime GetBusinessDay(string name, int pit, DateTime date)
        {
            var result = new DateTime();

            var session = GetSessionByContractKeyAndDate(name, pit, date);

            if (session != null)
                result = session.BusinessDay;
            
            return result; 
        }

        public Session GetSessionByContractKeyAndDate(string name, int pit, DateTime date) 
        {
            var result = new Session();

            var sessions = GetSessionsOnDate(date);
            foreach (var session in sessions)
            {
                if(session.Name == name && session.Pit == pit)
                    result = session;
            }

            return result;
        }

        public IEnumerable<Session> GetSessionsByContractKey(string name, int pit)
        {
            var result = new List<Session>();

            foreach (var session in listTradingSessions)
            {
                if (session.Name == name && pit == session.Pit)
                    yield return new Session
                    {
                        Name = session.Name,
                        Pit = session.Pit,
                        StartTime = session.StartTime,
                        EndTime = session.EndTime,
                        BusinessDay = session.TradingDate
                    };
            }

            if (result.Count == 0)
                foreach (var contract in listContractInfo)
                {
                    if (contract.Name == name && pit == contract.Pit)
                        yield return new Session
                        {
                            Name = contract.Name,
                            Pit = contract.Pit,
                            StartTime = contract.StartTime,
                            EndTime = contract.EndTime
                        };
                }

            yield break;
        }

        public Session GetNextSessionForContractKey(Session currentSession)
        {
            var result = new Session();
            var sessions = new List<Session>();

            foreach (var session in listTradingSessions)
            {
                if (session.Name == currentSession.Name && session.Pit == currentSession.Pit && session.TradingDate > currentSession.BusinessDay)
                    sessions.Add(new Session
                    {
                        Name= session.Name,
                        Pit = session.Pit,
                        StartTime = session.StartTime,
                        EndTime = session.EndTime,
                        BusinessDay = session.TradingDate
                    });
            }

            sessions.Sort((x, y) => DateTime.Compare(x.BusinessDay.Date, y.BusinessDay.Date));

            result = sessions.FirstOrDefault();

            return result;
        }

        public Session GetPreviosSessionForContractKey(Session currentSession)
        {
            var result = new Session();
            var sessions = new List<Session>();

            foreach (var session in listTradingSessions)
            {
                if (session.Name == currentSession.Name && session.Pit == currentSession.Pit && session.TradingDate < currentSession.BusinessDay)
                    sessions.Add(new Session
                    {
                        Name = session.Name,
                        Pit = session.Pit,
                        StartTime = session.StartTime,
                        EndTime = session.EndTime,
                        BusinessDay = session.TradingDate
                    });
            }

            sessions.Sort((x, y) => DateTime.Compare(x.BusinessDay.Date, y.BusinessDay.Date));

            result = sessions.LastOrDefault();

            return result;
        }
        public void Order66(){ }
    }
}

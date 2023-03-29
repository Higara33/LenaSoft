using LenaSoft;
using LenaSoft.Converters;
using LenaSoft.Interfaces;
using LenaSoft.Models;
using LenaSoft.Readers;

IReader readerContractInfo = new CSVReader(@"C:\Users\duha3\Downloads\ContractInfo.csv");
IReader readerTradingSessions = new CSVReader(@"C:\Users\duha3\Downloads\TradingSessions.csv");

IConverter<ContractInfo> contractInfoConverter = new ContractInfoConverter();
IConverter<TradingSessions> tradingSessionsConverter = new TradingSessionsConverter();

var executor = new Executor(
    tradingSessionsConverter.Convert(readerTradingSessions.GetDataTableFile()), 
    contractInfoConverter.Convert(readerContractInfo.GetDataTableFile()));

ShowContractInfo();
Console.WriteLine();

ShowTradingSessions();
Console.WriteLine();

string dataForTest = "2022-09-04 23:00:00.000";

Console.WriteLine($"Сессии по дате {dataForTest}");
var test = executor.GetSessionsOnDate(DateTime.Parse(dataForTest));
foreach (var session in test)
{
    Console.WriteLine("{0}       {1}       {2}      {3}", session.Name, session.Pit, session.StartTime, session.EndTime);
}
Console.WriteLine();

string nameForTest = "ES";
int pitForTest = 0;
dataForTest = "2022-09-05";

Console.WriteLine($"Сессии по ContractKey {nameForTest} {pitForTest}");
var testSessions = executor.GetSessionsByContractKey(nameForTest, pitForTest);
foreach (var session in testSessions)
{
    Console.WriteLine("{0}       {1}       {2}      {3}", session.Name, session.Pit, session.StartTime, session.EndTime);
}
Console.WriteLine();

Console.WriteLine($"Одна сессия по ключу  {nameForTest} {pitForTest} {dataForTest}");
var currentSession = executor.GetSessionByContractKeyAndDate(nameForTest, pitForTest, DateTime.Parse(dataForTest));
Console.WriteLine("{0}       {1}       {2}      {3}      {4}", currentSession.Name, currentSession.Pit, currentSession.StartTime, currentSession.EndTime, currentSession.BusinessDay);
Console.WriteLine();

Console.WriteLine("Некст сессия");
var nextSession = executor.GetNextSessionForContractKey(currentSession);
Console.WriteLine("{0}       {1}       {2}      {3}      {4}", nextSession.Name, nextSession.Pit, nextSession.StartTime, nextSession.EndTime, nextSession.BusinessDay);
Console.WriteLine();

Console.WriteLine("Предыдущая сессия");
var previosSession = executor.GetPreviosSessionForContractKey(currentSession);
Console.WriteLine("{0}       {1}       {2}      {3}      {4}", previosSession.Name, previosSession.Pit, previosSession.StartTime, previosSession.EndTime, previosSession.BusinessDay);
Console.WriteLine();

Console.ReadLine();

void ShowContractInfo()
{
    var colums = readerContractInfo.GetDataTableFile().Columns;

    for (int i = 0; i < colums.Count; i++)
    {
        Console.Write($"{colums[i].ColumnName}     ");
    }

    Console.WriteLine();

    foreach (var item in contractInfoConverter.Convert(readerContractInfo.GetDataTableFile()))
    {
        var startTime = item.StartTime.ToString("T");
        var endTime = item.EndTime.ToString("T");

        if (startTime.Length < 8 || endTime.Length < 8)
        {
            if (8 - startTime.Length != 0)
                for (int i = 0; i < 8 - startTime.Length; i++)
                {
                    startTime = startTime + " ";
                }

            if (8 - endTime.Length != 0)
                for (int i = 0; i < 8 - endTime.Length; i++)
                {
                    endTime = endTime + " ";
                }

            Console.WriteLine("{0}       {1}       {2}      {3}    {4}", item.Name, item.Pit, startTime, endTime, item.TimeZone);
        }
        else
            Console.WriteLine("{0}       {1}       {2}      {3}    {4}", item.Name, item.Pit, startTime, endTime, item.TimeZone);
    }
}
void ShowTradingSessions()
{
    var colums = readerTradingSessions.GetDataTableFile().Columns;

    for (int i = 0; i < colums.Count; i++)
    {
        Console.Write($"{colums[i].ColumnName}               ");
    }

    Console.WriteLine();

    foreach (var session in tradingSessionsConverter.Convert(readerTradingSessions.GetDataTableFile()))
    {
        if (session.StartTime.ToString().Length < 19 || session.EndTime.ToString().Length < 19)
        {
            var startTime = session.StartTime.ToString();
            var endTime = session.EndTime.ToString();

            if (19 - startTime.Length != 0)
                for (int i = 0; i < 19 - startTime.Length; i++)
                {
                    startTime = startTime + " ";
                }

            if (19 - endTime.Length != 0)
                for (int i = 0; i < 19 - endTime.Length; i++)
                {
                    endTime = endTime + " ";
                }

            Console.WriteLine("{0}                 {1}                 {2}     {3}   {4}", session.Name, session.Pit, startTime, endTime, session.TradingDate.ToString("d"));
        }
        else
            Console.WriteLine("{0}                 {1}                 {2}     {3}   {4}", session.Name, session.Pit, session.StartTime, session.EndTime, session.TradingDate.ToString("d"));
    }
}

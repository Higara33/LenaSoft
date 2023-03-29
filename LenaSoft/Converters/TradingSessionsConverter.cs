using LenaSoft.Interfaces;
using LenaSoft.Models;
using System.Data;

namespace LenaSoft.Converters
{
    public class TradingSessionsConverter : IConverter<TradingSessions>
    {
        public List<TradingSessions> Convert(DataTable data)
        {
            var result = new List<TradingSessions>();

            var rows = data.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                var items = rows[i].ItemArray;
                result.Add(new TradingSessions()
                {
                    Name = items[0].ToString(),
                    Pit = int.Parse(items[1].ToString()),
                    StartTime = DateTime.Parse(items[2].ToString()),
                    EndTime = DateTime.Parse(items[3].ToString()),
                    TradingDate = DateTime.Parse(items[4].ToString())
                });
            }
            return result;
        }
    }
}

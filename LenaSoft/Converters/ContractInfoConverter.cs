using LenaSoft.Interfaces;
using LenaSoft.Models;
using System.Data;

namespace LenaSoft.Converters
{
    public class ContractInfoConverter : IConverter<ContractInfo>
    {
        public List<ContractInfo> Convert(DataTable data)
        {
            var result = new List<ContractInfo>();

            var rows = data.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                var items = rows[i].ItemArray;
                result.Add(new ContractInfo()
                {
                    Name = items[0].ToString(),
                    Pit = int.Parse(items[1].ToString()),
                    StartTime = DateTime.Parse(items[2].ToString()),
                    EndTime = DateTime.Parse(items[3].ToString()),
                    TimeZone = TimeZoneInfo.Utc
                });
            }
            return result;
        }
    }
}

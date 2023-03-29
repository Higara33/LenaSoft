using LenaSoft.Interfaces;
using Microsoft.VisualBasic.FileIO;
using System.Data;

namespace LenaSoft.Readers
{
    public class CSVReader : IReader
    {
        private string filePath;
        public CSVReader(string input)
        {
            filePath = input;
        }

        public DataTable GetDataTableFile()
        {
            DataTable csvData = new DataTable();

                using (TextFieldParser csvReader = new TextFieldParser(filePath))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();

                    foreach (string column in colFields)
                    {
                        DataColumn datacolumn = new DataColumn(column);
                        datacolumn.AllowDBNull = true;
                        csvData.Columns.Add(datacolumn);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();

                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }

                        csvData.Rows.Add(fieldData);
                    }
                }

            return csvData;
        }
    }
}
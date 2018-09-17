namespace YNABCompanion.Services
{
    using CsvHelper;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class TransactionIO : ITransactionIO
    {
        public IEnumerable<string> ReadLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        public void WriteLines<T>(string filePath, IEnumerable<T> data)
        {
            using (var streamWriter = new StreamWriter(filePath))
            {
                var writer = new CsvWriter(streamWriter);
                writer.Configuration.Delimiter = ",";
                writer.WriteRecords(data);
            }
        }
    }
}

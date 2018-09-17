namespace YNABCompanion.Services
{
    using System.Collections.Generic;

    interface ITransactionIO
    {
        IEnumerable<string> ReadLines(string filePath);

        void WriteLines<T>(string filePath, IEnumerable<T> data);
    }
}

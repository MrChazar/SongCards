using CsvHelper;
using Microsoft.VisualBasic;
using System;
using System.Globalization;

public class FileHandle
{
    public class Lines
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public Lines(int a, string b)
        {
            Id = a;
            Word = b;
        }
    }

    public static void FileSave(List<string> data)
    {
        var Collection = new List<Lines>();
        int numb = 1;
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i] == null || data[i] == "" || data[i] == "1" || data[i] == "2" || data[i].Length <= 3)
            {
                continue;
            }
            Collection.Add(new Lines(numb, data[i]));
            numb++;
        }
        
        using (var writer = new StreamWriter("Cards/Song.csv", append: true))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(Collection);
        }
    }
}

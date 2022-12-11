using Genius;
using HtmlAgilityPack;
using System;
using System.ComponentModel;
using System.Net;
using System.Web;

public class Lyrics
{

    public static List<string> Dict(string words)
    {
        List<string> Result = new List<string>();
        string temp = "";
        foreach (char a in words)
        {
            if (a != ' ' && a != '[' && a != ']' && Char.IsUpper(a) != true)
            {
                temp += a;
            }
            else if (Char.IsUpper(a))
            {
                Result.Add(temp);
                temp = "";
                temp = Convert.ToString(Char.ToLower(a));
            }
            else
            {
                Result.Add(temp);
                temp = "";
            }
        }
        return Result;
    }

    public static async void GetLyric (GeniusClient client, string title, string author)
    {
        List<string> lista = new List<string>();
        bool s = true;
        int i = 1;
        while (s)
        {
            var search = client.SearchClient.Search($"per_page=20&page={i}&q=" + title);
            if (search.Result.Response.Hits.Count() == 0)
            {
                Console.WriteLine("Not found");
                s = false;
            }
            // Console.WriteLine(search.Result.Response.Hits.Count());
            var hits = search.Result.Response.Hits;
            foreach (var item in hits)
            {
                if (item.Result.PrimaryArtist.Name.ToUpper().Contains(author.ToUpper()))
                {
                    Console.WriteLine(item.Result.FullTitle);
                    Console.WriteLine(item.Result.Id);
                    Console.WriteLine("Song Found");
                    var song = await client.SongClient.GetSong(item.Result.Id);

                    using (WebClient hclient = new WebClient())
                    {
                        HtmlWeb hw = new HtmlWeb();
                        HtmlDocument doc = hw.Load(song.Response.Song.Url);
                        var divs = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'Lyrics__Container')]");
                        var node = divs.InnerText;
                        node = HttpUtility.HtmlDecode(node);
                        lista = Dict(node);
                        FileHandle.FileSave(lista);
                    }
                    s = false;
                }
            }
            i += 1;
        }
    }
    
}




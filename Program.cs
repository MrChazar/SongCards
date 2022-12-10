using Genius;
using System.Net;
using HtmlAgilityPack;
using System.Text;
using System.Web;




// Estabilishing Connection to api
var client = new GeniusClient("a7Ur37IuSMicNb9pcqzbbwoUy49msqwPEyKndx8kBivPGRH17tpV4bteO410Rlp4");

// Setting Encoding
Console.OutputEncoding = Encoding.UTF8;

// Variables for menu

bool on = true;
int action = 0;
while (on)
{
    Console.Write("Welcome in SongCards, Choose your action:\n\t1: Get lyrics\n\t2: Quit\nAction:");
    try
    {
        action = Convert.ToInt16(Console.ReadLine());
    }
    catch
    {
        continue;
    }
    switch (action)
    {
        case 1:
            Console.Write("Give song name:");
            string title = Console.ReadLine();
            Console.Write("Give author name:");
            string author = Console.ReadLine();
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
                        Console.WriteLine("Found");
                        var song = await client.SongClient.GetSong(item.Result.Id);

                        using (WebClient hclient = new WebClient())
                        {
                            HtmlWeb hw = new HtmlWeb();
                            HtmlDocument doc = hw.Load(song.Response.Song.Url);
                            var divs = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'Lyrics__Container')]");
                            var node = divs.InnerText;
                            node = HttpUtility.HtmlDecode(node);
                            Console.WriteLine(node);
                            List<string> lista = Dict(node);
                            FileHandle.FileSave(lista);

                        }
                        s = false;
                    }
                }
                Console.WriteLine();
                i += 1;
            }
            break;
        case 2:
            Console.WriteLine("Thanks For Using");
            on = false;
            break;
        default:
            Console.WriteLine("You made action that doesnt exists");
            break;
    }
}







List<string> Dict(string words)
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





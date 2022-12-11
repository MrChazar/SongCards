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
            Lyrics.GetLyric(client, title, author);
            
            Console.WriteLine("Words saved to Card");
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












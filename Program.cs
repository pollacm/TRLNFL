using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TRLNFL.Slack;
using TRLNFL.Teams;

namespace TRLNFL
{
    internal class Program
    {
        private static readonly string RosterPage = "https://baseball.fantasysports.yahoo.com/b1/125071/7";
        private static readonly string PlayerPage = "https://baseball.fantasysports.yahoo.com/b1/125071/players?status=A&pos=S_P&cut_type=33&stat1=S_AL30&myteam=0&sort=PTS&sdir=1";
        private static List<string> AddPlayerPages = new List<string>
        {
            "https://baseball.fantasysports.yahoo.com/b1/125071/addplayer?apid=7599",
            "https://baseball.fantasysports.yahoo.com/b1/125071/addplayer?apid=7701",
            "https://baseball.fantasysports.yahoo.com/b1/125071/addplayer?apid=10597"
        };
        private static List<string> PitchersToNotReplace = new List<string>()
        {
            "Berríos",
            "Verlander",
            "Berríos",
            "Clevinger",
            "Allen",
            "Lynn",
            "Giolito"
        };
        private static readonly string EmailAddressForLogin = "pollacm";

        //private static readonly string RosterPage = "https://baseball.fantasysports.yahoo.com/b1/189961/1";
        //private static readonly string PlayerPage = "https://baseball.fantasysports.yahoo.com/b1/189961/players?status=A&pos=S_P&cut_type=33&stat1=S_AL30&myteam=0&sort=AR&sdir=1";
        //private static List<string> AddPlayerPages = new List<string>
        //{
        //    "https://baseball.fantasysports.yahoo.com/b1/189961/addplayer?apid=10131",
        //    "https://baseball.fantasysports.yahoo.com/b1/189961/addplayer?apid=8270"
        //};
        //private static List<string> PitchersToNotReplace = new List<string>()
        //{
        //    "Verlander",
        //    "Berríos",
        //    "Clevinger",
        //    "Jansen",
        //    "Montas",
        //    "Lynn",
        //    "Giolito",
        //    "Jansen",
        //    "Doolittle"
        //};
        private static void Main(string[] args)
        {
            //var options = new ChromeOptions();
            //////options.AddArgument("--headless");
            //var driver = new ChromeDriver(options);
            //driver.NavigateToUrl("https://football.fantasysports.yahoo.com/f1/900565/matchup?week=11&mid1=4");
            ////home
            //var homeTeam2 = new Team(driver.FindElement(By.XPath("//div[@class='RedZone']/div/div/div[1]/div/div/a")).Text);
            //var awayTeam2 = new Team(driver.FindElement(By.XPath("//div[@class='RedZone']/div/div/div[3]/div/div/a")).Text);

            //var starters = driver.FindElements(By.XPath("//table[1]/tbody/tr"));
            //var benchPlayers = driver.FindElements(By.XPath("//table[2]/tbody/tr"));

            //foreach (var starter in starters)
            //{
            //    var rmlPlayer = new RmlPlayer.RmlPlayer();

            //    var hasName = starter.FindElements(By.XPath("./td[2]/div/a"));

            //    if (hasName.Any())
            //    {
            //        //home starter
            //        rmlPlayer.Name = starter.FindElement(By.XPath("./td[2]/div/a")).Text;
            //        rmlPlayer.Projection = Convert.ToDecimal(starter.FindElement(By.XPath("./td[3]/div/span/span")).Text);
            //        var pointsExist = starter.FindElements(By.XPath("./td[4]/div/span/div/span/span"));
            //        if (pointsExist.Any())
            //        {
            //            rmlPlayer.Points = Convert.ToDecimal(starter.FindElement(By.XPath("./td[4]/div/span/div/span/span")).Text);
            //        }
            //        else
            //        {
            //            rmlPlayer.Points = (decimal)0;
            //        }

            //        rmlPlayer.Position = starter.FindElement(By.XPath("./td[2]/div/span")).Text.Split('-')[1].Trim();
            //        rmlPlayer.Starter = true;
            //        homeTeam2.Players.Add(rmlPlayer);
            //    }

            //    hasName = starter.FindElements(By.XPath("./td[8]/div/a"));
            //    if (hasName.Any())
            //    {
            //        //away starter
            //        rmlPlayer = new RmlPlayer.RmlPlayer();
            //        rmlPlayer.Name = starter.FindElement(By.XPath("./td[8]/div/a")).Text;
            //        rmlPlayer.Projection = Convert.ToDecimal(starter.FindElement(By.XPath("./td[7]/div/span/span")).Text);
            //        var pointsExist = starter.FindElements(By.XPath("./td[6]/div/span/div/span/span"));
            //        if (pointsExist.Any())
            //        {
            //            rmlPlayer.Points = Convert.ToDecimal(starter.FindElement(By.XPath("./td[4]/div/span/div/span/span")).Text);
            //        }
            //        else
            //        {
            //            rmlPlayer.Points = (decimal)0;
            //        }
            //        rmlPlayer.Position = starter.FindElement(By.XPath("./td[8]/div/span")).Text.Split('-')[1].Trim();
            //        rmlPlayer.Starter = true;
            //        awayTeam2.Players.Add(rmlPlayer);
            //    }
                
            //}

            //foreach (var benchPlayer in benchPlayers)
            //{
            //    var rmlPlayer = new RmlPlayer.RmlPlayer();
                
            //    var hasName = benchPlayer.FindElements(By.XPath("./td[2]/div/a"));
            //    if (hasName.Any())
            //    {
            //        //home bench
            //        rmlPlayer.Name = benchPlayer.FindElement(By.XPath("./td[2]/div/a")).Text;
            //        rmlPlayer.Projection = Convert.ToDecimal(benchPlayer.FindElement(By.XPath("./td[3]/div/span/span")).Text);
            //        var pointsExist = benchPlayer.FindElements(By.XPath("./td[4]/div/span/div/span/span"));
            //        if (pointsExist.Any())
            //        {
            //            rmlPlayer.Points = Convert.ToDecimal(benchPlayer.FindElement(By.XPath("./td[4]/div/span/div/span/span")).Text);
            //        }
            //        else
            //        {
            //            rmlPlayer.Points = (decimal)0;
            //        }

            //        rmlPlayer.Position = benchPlayer.FindElement(By.XPath("./td[2]/div/span")).Text.Split('-')[1].Trim();
            //        rmlPlayer.Starter = false;
            //        homeTeam2.Players.Add(rmlPlayer);
            //    }

            //    hasName = benchPlayer.FindElements(By.XPath("./td[8]/div/a"));
            //    if (hasName.Any())
            //    {
            //        //away bench
            //        rmlPlayer.Name = benchPlayer.FindElement(By.XPath("./td[8]/div/a")).Text;
            //        rmlPlayer.Projection = Convert.ToDecimal(benchPlayer.FindElement(By.XPath("./td[7]/div/span/span")).Text);

            //        var pointsExist = benchPlayer.FindElements(By.XPath("./td[6]/div/span/div/span/span"));
            //        if (pointsExist.Any())
            //        {
            //            rmlPlayer.Points = Convert.ToDecimal(benchPlayer.FindElement(By.XPath("./td[6]/div/span/div/span/span")).Text);
            //        }
            //        else
            //        {
            //            rmlPlayer.Points = (decimal)0;
            //        }

            //        rmlPlayer.Position = benchPlayer.FindElement(By.XPath("./td[8]/div/span")).Text.Split('-')[1].Trim();
            //        rmlPlayer.Starter = false;
            //        awayTeam2.Players.Add(rmlPlayer);
            //    }
            //}



            //home
            var homeTeam = new Team("Dalvin");
            
            //qb - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Hurts", Projection = (decimal?) 171.93, Points = (decimal) 141.9, SubPoints = (decimal?) null, SamePosition = true, SubName = "" });
            //wr - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer{ Name = "Olave", Projection = (decimal?)null, Points = (decimal)59, SubPoints = (decimal?)148.00, SamePosition = true, SubName = "Hopkins" }); 
            //rb - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Cook", Projection = (decimal?)null, Points = (decimal)218.6, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //te - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Schultz", Projection = (decimal?)null, Points = (decimal)107, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //wrt1 - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Pierce", Projection = (decimal?)null, Points = (decimal)139.8, SubPoints = (decimal?)null, SamePosition = true, SubName = ""});
            //wrt2 - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Connor", Projection = (decimal?)null, Points = (decimal)143.4, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //sup - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Spiller", Projection = (decimal?)null, Points = (decimal)4.6, SubPoints = (decimal?)165.4, SamePosition = true, SubName = "Foreman" });
            //k - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Myers", Projection = (decimal?)null, Points = (decimal)66.83, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //d - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Walker", Projection = (decimal?)null, Points = (decimal)99, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //db - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Elliot", Projection = (decimal?)null, Points = (decimal)15, SubPoints = (decimal?)81, SamePosition = true, SubName = "Kearse" });
            //dl - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Hunter", Projection = (decimal?)null, Points = (decimal)75, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //lb1 - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Foye", Projection = (decimal?)null, Points = (decimal)105, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //lb2 - me
            homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Long", Projection = (decimal?)null, Points = (decimal)85, SubPoints = (decimal?)190, SamePosition = true, SubName = "Shaq" });
            homeTeam.CalculatePoints();


            //away
            var awayTeam = new Team("Opponent");
            //qb - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Tua", Projection = (decimal?)162.19, Points = (decimal)162.19, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //wr - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Palmer", Projection = (decimal?)null, Points = (decimal)63, SubPoints = (decimal?)220.00, SamePosition = true, SubName = "Kirk" });
            //rb - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Williams", Projection = (decimal?)null, Points = (decimal)85.4, SubPoints = (decimal?)183.00, SamePosition = true, SubName = "Wilson" });
            //te - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Ertz", Projection = (decimal?)null, Points = (decimal)20, SubPoints = (decimal?)200, SamePosition = false, SubName = "Mclaurin" });
            //wrt1 - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Hubbard", Projection = (decimal?)null, Points = (decimal)16.00, SubPoints = (decimal?)128, SamePosition = true, SubName = "Herbert" });
            //wrt2 - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Moore", Projection = (decimal?)null, Points = (decimal)51.00, SubPoints = (decimal?)153.8, SamePosition = true, SubName = "Toney" });
            //sup - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Geno", Projection = (decimal?)null, Points = (decimal)148.81, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //k - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Gano", Projection = (decimal?)null, Points = (decimal)66.83, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //d - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Garrett", Projection = (decimal?)null, Points = (decimal)23, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //db - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Grant", Projection = (decimal?)null, Points = (decimal)147, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //dl - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Crosby", Projection = (decimal?)null, Points = (decimal)153, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //lb1 - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Wagner", Projection = (decimal?)null, Points = (decimal)145, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //lb2 - opponent
            awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Collins", Projection = (decimal?)null, Points = (decimal)96, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            awayTeam.CalculatePoints();
            var x = 1;



            //Thread.Sleep(5000);

            //driver.Navigate().GoToUrl($"{RosterPage}");

            //driver.Close();
            //Refresh Pitcher Names
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Xml.Schema;

namespace TRLNFL.Teams
{
    public class Team
    {
        public Team(string teamName)
        {
            TeamName = teamName;
            Players = new List<RmlPlayer.RmlPlayer>();
        }

        public string TeamName { get; set; }
        public decimal TeamPoints { get; set; }
        public List<RmlPlayer.RmlPlayer> Players { get; set; }

        public string CalculatePoints()
        {
            var calculatedPoints = string.Empty;
            Debug.WriteLine(TeamName);
            Debug.WriteLine("----------------------------------------");
            foreach (var player in Players)
            {
                TeamPoints += player.PointWithSub;
                var x = player.ToString();
            }
            Debug.WriteLine("----------------------------------------");
            Debug.WriteLine($"TOTAL: {TeamPoints}");
            Debug.WriteLine("----------------------------------------");
            Debug.WriteLine("----------------------------------------");

            return calculatedPoints;
        }
    }
}

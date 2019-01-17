using System.Collections.Generic;

namespace BookRank.Contracts
{
    public class BookResponse
    {
        public string BookName { get; set; }

        public string Description { get; set; }

        public List<string> Genres { get; set; }

        public int Ranking { get; set; }

        public string TimeRanked { get; set; }
    }
}

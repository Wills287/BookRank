namespace BookRank.Contracts
{
    public class BookUpdateRequest
    {
        public string BookName { get; set; }

        public int Ranking { get; set; }
    }
}

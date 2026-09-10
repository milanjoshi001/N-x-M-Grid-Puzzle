namespace NMGrid.Grid
{
    public class Board
    {
        public Tile[,]  Tiles { get; private set; }
        public int Score { get; private set; }
        public int Moves { get; private set; }
        
        public Board(Tile[,] tiles, int score, int moves)
        {
            Tiles = tiles;
            Score = score;
            Moves = moves;
        }

        public void AddMoves() => Moves++;

        public void AddScore(int amount) => Score += amount;
    }
}
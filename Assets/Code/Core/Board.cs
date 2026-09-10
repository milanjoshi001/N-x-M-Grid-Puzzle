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

        public Board CloneBoard()
        {
            int width = Tiles.GetLength(0);
            int height = Tiles.GetLength(1);

            Tile[,] tiles = new Tile[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = Tiles[x, y];
                    
                    if(tile != null)
                        tiles[x, y] = new Tile(tile);
                }
            }
            
            return new Board(tiles, Score, Moves);
        }
    }
}
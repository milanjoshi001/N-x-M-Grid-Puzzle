namespace NMGrid.Grid
{
    public class Tile
    {
        public int ID { get; private set; }
        public int Value { get; private set; }
        
        public  Tile(int id, int value)
        {
            ID = id;
            Value = value;
        }

        public Tile(Tile tile)
        {
            ID = tile.ID;
            Value = tile.Value;
        }

        public void SetValue(int value) => Value = value;
    }
}
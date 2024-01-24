namespace CandyCrush.Scripts
{
    public class GridObject<T>
    {
        
        private GridSystem2D<GridObject<T>> _grid;
        int _x;
        int _y;

        public GridObject(GridSystem2D<GridObject<T>> grid, int x, int y)
        {
            _grid = grid;
            _x = x;
            _y = y;
        }
        
    }
}
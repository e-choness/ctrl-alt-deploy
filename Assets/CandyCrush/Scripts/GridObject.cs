namespace CandyCrush.Scripts
{
    public class GridObject<T>
    {
        private GridSystem<GridObject<T>> _grid;
        int _x;
        int _y;
        private T _gem;

        public GridObject(GridSystem<GridObject<T>> grid, int x, int y)
        {
            _grid = grid;
            _x = x;
            _y = y;
        }

        public void SetValue(T gem)
        {
            _gem = gem;
        }

        public T GetValue() => _gem;
    }
}
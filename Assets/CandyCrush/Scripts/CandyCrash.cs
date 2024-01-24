using System;
using UnityEngine;

namespace CandyCrush.Scripts
{
    public class CandyCrash : MonoBehaviour
    {
        [SerializeField] private int width = 0;
        [SerializeField] private int height = 0;
        [SerializeField] private float cellSize = 1.0f;
        [SerializeField] private Vector3 originPosition = Vector3.zero;
        [SerializeField] private bool debug = true;

        private GridSystem2D<GridObject<Gem>> _grid;

        private void Start()
        {
            _grid = GridSystem2D<GridObject<Gem>>.VerticalGrid(width, height, cellSize, originPosition, debug);
        }
    }
}
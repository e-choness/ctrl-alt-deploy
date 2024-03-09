using System;
using UnityEngine;

namespace CandyCrush.Scripts
{
    public class CandyCrash : MonoBehaviour
    {
        [SerializeField] private int width;
        [SerializeField] private int height ;
        [SerializeField] private float cellSize = 1.0f;
        [SerializeField] private Vector3 originPosition = Vector3.zero;
        [SerializeField] private bool debug;

        private GridSystem<GridObject<Gem>> _grid;

        private void Start()
        {
            _grid = GridSystem<GridObject<Gem>>.VerticalGrid(width, height, cellSize, originPosition, debug);
        }
    }
}
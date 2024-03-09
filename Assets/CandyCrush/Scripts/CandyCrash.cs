using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CandyCrush.Scripts
{
    public class CandyCrash : MonoBehaviour
    {
        [Header("Grid Properties")]
        [SerializeField] private int width;
        [SerializeField] private int height ;
        [SerializeField] private float cellSize = 1.0f;
        [SerializeField] private Vector3 originPosition = Vector3.zero;
        [SerializeField] private bool debug;

        [Header("Gem Properties")]
        [SerializeField] private Gem gemPrefab;
        [SerializeField] private GemType[] gemTypes;

        [Header("Animation Properties")] 
        [SerializeField] private float easeDuration = 0.5f;
        [SerializeField] private Ease ease = Ease.InQuad;
        [SerializeField] private GameObject explosion;

        private InputReader _inputReader;
        private AudioManager _audioManager;
        
        private GridSystem<GridObject<Gem>> _grid;

        private Vector2Int _selectedGem = Vector2Int.one * -1;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _audioManager = GetComponent<AudioManager>();
        }

        private void Start()
        {
            InitializeGrid();
            _inputReader.Fire += OnSelectGem;
        }

        private void OnDestroy()
        {
            _inputReader.Fire -= OnSelectGem;
        }

        private void InitializeGrid()
        {
            _grid = GridSystem<GridObject<Gem>>.VerticalGrid(width, height, cellSize, originPosition, debug);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    CreateGem(x, y);
                }
            }
        }

        private void CreateGem(int x, int y)
        {
            // Instantiate gem and define its type
            var gem = Instantiate(gemPrefab, _grid.GetWorldPositionCenter(x, y), Quaternion.identity, transform);
            gem.SetType(gemTypes[Random.Range(0, gemTypes.Length)]);
            
            // Put the gem into the grid by assigning it to the grid object and position
            var gridObject = new GridObject<Gem>(_grid,x,y);
            gridObject.SetValue(gem);
            _grid.SetValue(x,y,gridObject);
        }

        private void OnSelectGem()
        {
            var gridPosition = _grid.GetXY(Camera.main.ScreenToWorldPoint(_inputReader.Selected));

            if (!IsValidPosition(gridPosition) || IsEmptyPosition(gridPosition)) return;
            if (_selectedGem == gridPosition)
            {
                DeselectGem();
                _audioManager.PlayDeselect();
            }else if (_selectedGem == Vector2Int.one * -1)
            {
                SelectGem(gridPosition);
                _audioManager.PlayClick();
            }
            else
            {
                StartCoroutine(RunGameLoop(_selectedGem, gridPosition));
            }
        }

        private IEnumerator RunGameLoop(Vector2Int gridPositionFirst, Vector2Int gridPositionLast)
        {
            yield return StartCoroutine(SwapGems(gridPositionFirst, gridPositionLast));

            List<Vector2Int> matches = FindMatches();

            yield return StartCoroutine(ExplodeGems(matches));

            yield return StartCoroutine(MakeGemsFall());

            yield return StartCoroutine(FillEmptySpots());
        }

        private IEnumerator SwapGems(Vector2Int gridPositionFirst, Vector2Int gridPositionLast)
        {
            var gridObjectFirst = _grid.GetValue(gridPositionFirst.x, gridPositionFirst.y);
            var gridObjectLast = _grid.GetValue(gridPositionLast.x, gridPositionLast.y);

            gridObjectFirst.GetValue().transform
                .DOLocalMove(_grid.GetWorldPositionCenter(gridPositionLast.x, gridPositionLast.y), easeDuration)
                .SetEase(ease);
            gridObjectLast.GetValue().transform
                .DOLocalMove(_grid.GetWorldPositionCenter(gridPositionFirst.x, gridPositionFirst.y), easeDuration)
                .SetEase(ease);

            _grid.SetValue(gridPositionFirst.x, gridPositionFirst.y, gridObjectLast);
            _grid.SetValue(gridPositionLast.x, gridPositionLast.y, gridObjectFirst);
            
            yield return new WaitForSeconds(easeDuration);
        }

        private List<Vector2Int> FindMatches()
        {
            HashSet<Vector2Int> matches = new();

            // Search matching gems horizontally
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width-2; x++)
                {
                    var gemA = _grid.GetValue(x, y);
                    var gemB = _grid.GetValue(x + 1, y);
                    var gemC = _grid.GetValue(x + 2, y);
                    
                    if(gemA == null || gemB == null || gemC == null) continue;

                    if (gemA.GetValue().GetType() == gemB.GetValue().GetType() &&
                        gemB.GetValue().GetType() == gemC.GetValue().GetType())
                    {
                        matches.Add(new Vector2Int(x, y));
                        matches.Add(new Vector2Int(x + 1, y));
                        matches.Add(new Vector2Int(x + 2, y));
                    }
                }
            }

            // Search matching gems vertically
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height-2; y++)
                {
                    var gemA = _grid.GetValue(x, y);
                    var gemB = _grid.GetValue(x, y + 1);
                    var gemC = _grid.GetValue(x, y + 2);
                    
                    if(gemA == null || gemB == null || gemC == null) continue;
                    if (gemA.GetValue().GetType() == gemB.GetValue().GetType() &&
                        gemB.GetValue().GetType() == gemC.GetValue().GetType())
                    {
                        matches.Add(new Vector2Int(x, y));
                        matches.Add(new Vector2Int(x, y + 1));
                        matches.Add(new Vector2Int(x, y + 2));
                    }
                }
            }

            if (matches.Count == 0)
            {
                _audioManager.PlayNoMatch();
            }
            else
            {
                _audioManager.PlayMatch();
            }

            return new List<Vector2Int>(matches);
        }

        private IEnumerator ExplodeGems(List<Vector2Int> matches)
        {
            _audioManager.PlayPop();

            foreach (var match in matches)
            {
                var gem = _grid.GetValue(match.x, match.y).GetValue();
                _grid.SetValue(match.x, match.y, null);
                ExplodeVFX(match);
                gem.transform.DOPunchScale(Vector3.one * 0.1f, 0.1f, 1, 0.5f);

                yield return new WaitForSeconds(0.1f);
                Destroy(gem.gameObject, 0.1f);
            }
        }

        private void ExplodeVFX(Vector2Int match)
        {
            var fx = Instantiate(explosion, transform);
            fx.transform.position = _grid.GetWorldPositionCenter(match.x, match.y);
            Destroy(fx, 5.0f);
        }

        private IEnumerator MakeGemsFall()
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (_grid.GetValue(x, y) == null)
                    {
                        for (var i = y + 1; i < height; i++)
                        {
                            if (_grid.GetValue(x, i) != null)
                            {
                                var gem = _grid.GetValue(x, i).GetValue();
                                _grid.SetValue(x,y,_grid.GetValue(x,i));
                                _grid.SetValue(x,i,null);
                                gem.transform
                                    .DOLocalMove(_grid.GetWorldPositionCenter(x, y), easeDuration)
                                    .SetEase(ease);
                                _audioManager.PlayWoosh();
                                yield return new WaitForSeconds(0.1f);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private IEnumerator FillEmptySpots()
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (_grid.GetValue(x, y) == null)
                    {
                        CreateGem(x,y);
                        _audioManager.PlayPop();
                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }
        }
        void DeselectGem() => _selectedGem = Vector2Int.one * -1;
        void SelectGem(Vector2Int gridPosition) => _selectedGem = gridPosition;

        bool IsEmptyPosition(Vector2Int gridPosition) => _grid.GetValue(gridPosition.x, gridPosition.y) == null;

        bool IsValidPosition(Vector2Int gridPosition)
        {
            return gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height;
        }
    }
}
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerMatch3 : MonoBehaviour
{
    private Player owner;
    [SerializeField] private MultiplayerEventSystem _eventSystem;
    private MatchPieceMovement pieceMover;

    [Header("Board")]
    [SerializeField] private Inspector2DArrayLayout gameBoardLayout;
    private BoardCell[,] gameBoard;
    [SerializeField] private int _boardWidth = 8;
    [SerializeField] private int _boardHeight = 8;

    [Header("Pieces")]
    [SerializeField] private MatchPieceSO _wallPiece;
    [SerializeField] private MatchPieceSO _empty;
    [SerializeField] private MatchPieceSO[] _matchPieces = new MatchPieceSO[0];
    [SerializeField] private GameObject _matchPieceObjectPrefab;
    [SerializeField] private RectTransform _matchPieceHolder;
    [SerializeField] private Vector2 _holderStartOffset;
    [SerializeField] private Vector2 _pieceSize;

    private List<SwappedPieces> swappedPieces = new List<SwappedPieces>();
    private ActivePieceController startSwapPiece = null;
    private ActivePieceController endSwapPiece = null;

    private System.Random randomSeed;
    private List<ActivePieceController> piecesUpdating = new List<ActivePieceController>();
    //private SelectStack<ActivePieceController> selectedPieces = new SelectStack<ActivePieceController>(2);

    #region Get/Setters
    public Vector2 HolderStartOffset { get => _holderStartOffset; set => _holderStartOffset = value; }
    public Vector2 PieceSize { get => _pieceSize; set => _pieceSize = value; }
    public MultiplayerEventSystem EventSystem { get => _eventSystem; set => _eventSystem = value; }
    //public SelectStack<ActivePieceController> SelectedPieces { get => selectedPieces; set => selectedPieces = value; }
    public MatchPieceMovement PieceMover { get => pieceMover; set => pieceMover = value; }
    public BoardCell[,] GameBoard { get => gameBoard; set => gameBoard = value; }
    public int BoardWidth { get => _boardWidth; set => _boardWidth = value; }
    public int BoardHeight { get => _boardHeight; set => _boardHeight = value; }
    public Player Owner { get => owner; set => owner = value; }
    public MatchPieceSO EmptyPiece { get => _empty; set => _empty = value; }
    public ActivePieceController StartSwapPiece { get => startSwapPiece; set => startSwapPiece = value; }
    public ActivePieceController EndSwapPiece { get => endSwapPiece; set => endSwapPiece = value; }

    #endregion

    private void Start()
    {
        pieceMover = GetComponent<MatchPieceMovement>();
        StartGame();
    }
    private void Update()
    {
        List<ActivePieceController> piecesFinishedUpdating = new List<ActivePieceController>();
        for (int index = 0; index < piecesUpdating.Count; index++)
        {
            ActivePieceController piece = piecesUpdating[index];
            if (!piece.UpdatePiece())
                piecesFinishedUpdating.Add(piece);
        }
        for (int index = 0; index < piecesFinishedUpdating.Count; index++)
        {
            ActivePieceController piece = piecesFinishedUpdating[index];
            SwappedPieces swapped = GetSwappedPieces(piece);
            ActivePieceController swappedPiece = null;

            List<GridPoint> connectedPieces = GetConnectedPieces(piece.GridPoint, true);
            bool wasSwapped = (swapped != null);
            if (wasSwapped)
            {
                swappedPiece = swapped.GetOtherPiece(piece);
                CombineGridPoints(ref connectedPieces, GetConnectedPieces(swappedPiece.GridPoint, true));
            }

            if (connectedPieces.Count == 0)
            {
                if (wasSwapped)
                    SwapPieces(false, piece, swappedPiece);
            }
            else
            {
                foreach(GridPoint gridPoint in connectedPieces)
                {
                    ActivePieceController cellPiece = GetCellAtGridPoint(gridPoint).ActivePieceController;
                    //BoardCell cell = GetCellAtGridPoint(gridPoint);
                    //ActivePieceController cellPiece = cell.ActivePieceController;
                    if (cellPiece != null)
                        cellPiece.GetComponent<Image>().enabled = false; //cellPiece.gameObject.SetActive(false);
                    cellPiece.SetUp(EmptyPiece); 
                }
                ApplyGravityToBoard();
            }
            swappedPieces.Remove(swapped);
            piecesUpdating.Remove(piece);
        }
    }
    private void StartGame()
    {
        randomSeed = new System.Random(GenerateNewSeed().GetHashCode());
        piecesUpdating = new List<ActivePieceController>();

        GenerateGameBoard();
        ValidateGameBoard();
        InstantiateGameBoard();
        SetSelectedPieceToStartPiece();
    }

    //Selects the first selectable piece in the board starting in the top left.
    private void SetSelectedPieceToStartPiece()
    {
        bool hasSelectedButton = false;
        for (int x = 0; x < _boardWidth; x++)
        {
            for (int y = 0; y < _boardHeight; y++)
            {
                if (gameBoard[x, y].MatchPiece.BoardFunction != Enums.MatchPieceFunction.Unmoveable)
                {
                    _eventSystem.SetSelectedGameObject(gameBoard[x, y].ActivePieceController.gameObject);
                    hasSelectedButton = true;
                    break;
                }
            }
            if (hasSelectedButton)
                break;
        }
    }

    //Creates a new board filled in with random pieces.
    private void GenerateGameBoard()
    {
        gameBoard = new BoardCell[_boardWidth, _boardHeight];
        for (int y = 0; y < _boardHeight; y++)
        {
            for (int x = 0; x < _boardWidth; x++)
                gameBoard[x, y] = new BoardCell((gameBoardLayout.Columns[y].Row[x]) ? _wallPiece : GetRandomPiece(), new GridPoint(x, y));
        }
    }

    //Checks over all pieces on the board and removes matches.
    private void ValidateGameBoard()
    {
        List<Enums.Element> elementsToRemove = new List<Enums.Element>();
        for (int x = 0; x < _boardWidth; x++)
        {
            for (int y = 0; y < _boardHeight; y++)
            {
                GridPoint pointChecked = new GridPoint(x, y);
                Enums.Element element = GetElementAtGridPoint(pointChecked);
                if (element <= 0)
                    continue;

                while (GetConnectedPieces(pointChecked, true).Count > 0)
                {
                    element = GetElementAtGridPoint(pointChecked);
                    if (!elementsToRemove.Contains(element))
                        elementsToRemove.Add(element);
                    SetElementAtGridPoint(pointChecked, NewElement(ref elementsToRemove));
                }
            }
        }
    }

    //Spawns all the piece game objects.
    private void InstantiateGameBoard()
    {
        for (int x = 0; x < _boardWidth; x++)
        {
            for (int y = 0; y < _boardHeight; y++)
            {
                Enums.Element element = gameBoard[x, y].MatchPiece.Element;
                //if ((int)element <= 0)
                //    continue;

                GameObject matchPieceObject = Instantiate(_matchPieceObjectPrefab, _matchPieceHolder);
                matchPieceObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(_holderStartOffset.x + (_pieceSize.x * x), _holderStartOffset.y - (_pieceSize.y * y));
                matchPieceObject.GetComponent<ActivePieceController>().SetUp(owner, gameBoard[x, y].MatchPiece, new GridPoint(x, y));

                gameBoard[x, y].ActivePieceController = matchPieceObject.GetComponent<ActivePieceController>(); //!!!
            }
        }
    }



    //Returns a list of the grid points that are in a match with gridPoint.
    public List<GridPoint> GetConnectedPieces(GridPoint gridPoint, bool isFirstPieceChecked)
    {
        List<GridPoint> connectedPieces = new List<GridPoint>();
        Enums.Element element = GetElementAtGridPoint(gridPoint);
        GridPoint[] directions = {GridPoint.up, GridPoint.right, GridPoint.down, GridPoint.left};

        //Checking for a line of three or more of the same element starting at the end piece.
        foreach (GridPoint direction in directions)
        {
            List<GridPoint> combo = new List<GridPoint>();
            int sameElement = 0;
            for (int index = 1; index < 3; index++)
            {
                GridPoint pointChecked = GridPoint.Add(gridPoint, GridPoint.Multiply(direction, index));
                if (GetElementAtGridPoint(pointChecked) == element)
                {
                    combo.Add(pointChecked);
                    sameElement++;
                }
            }
            if (sameElement > 1)
                CombineGridPoints(ref connectedPieces, combo);
        }

        //Checking for a line of three or more of the same element starting at a middle piece.
        for (int index = 0; index < directions.Length / 2; index++)//(int index = 0; index < 2; index++)
        {
            List<GridPoint> combo = new List<GridPoint>();
            int sameElement = 0;
            GridPoint[] sidePoints = { GridPoint.Add(gridPoint, directions[index]), GridPoint.Add(gridPoint, directions[index + 2]) };
            foreach (GridPoint pointChecked in sidePoints)
            {
                if (GetElementAtGridPoint(pointChecked) == element)
                {
                    combo.Add(pointChecked);
                    sameElement++;
                }
            }
            if (sameElement > 1)
                CombineGridPoints(ref connectedPieces, combo);
        }

        //Checking for a square of the same element
        //for (int index = 0; index < 4; index++) //(int index = 0; index < 4; index++)
        //{
        //    List<GridPoint> combo = new List<GridPoint>();
        //    int sameElement = 0;
        //    int nextChecked = index + 1;
        //    if (nextChecked >= _matchPieces.Length)//if (nextChecked >= 4)
        //        nextChecked -= _matchPieces.Length;//nextChecked -= 4;
        //    GridPoint[] checkedPoints = {GridPoint.Add(gridPoint, directions[index]), GridPoint.Add(gridPoint, directions[nextChecked]), GridPoint.Add(gridPoint, GridPoint.Add(directions[index], directions[nextChecked])) };
        //    foreach (GridPoint pointChecked in checkedPoints)
        //    {
        //        if (GetElementAtGridPoint(pointChecked) == element)
        //        {
        //            combo.Add(pointChecked);
        //            sameElement++;
        //        }
        //    }
        //    if (sameElement > 2)
        //        CombineGridPoints(ref connectedPieces, combo);
        //}

        //Checks if a piece is used in multiple matches.
        if (isFirstPieceChecked)
        {
            for (int index = 0; index < connectedPieces.Count; index++)
                CombineGridPoints(ref connectedPieces, GetConnectedPieces(connectedPieces[index], false));
        }

        if (connectedPieces.Count > 0)
            connectedPieces.Add(gridPoint);
        return connectedPieces;
    }

    //Adds the grid points from addedGridPoints to gridPoints if gridPoints does not already contain the grid point.
    private void CombineGridPoints(ref List<GridPoint> gridPoints, List<GridPoint> addedGridPoints)
    {
        foreach (GridPoint addedPoint in addedGridPoints)
        {
            bool pointNotPresent = true;
            for (int index = 0; index < gridPoints.Count; index++)
            {
                if (gridPoints[index].Equals(addedPoint))
                {
                    pointNotPresent = false;
                    break;
                }
            }
            if (pointNotPresent)
                gridPoints.Add(addedPoint);
        }
    }

    //Returns the element of the piece at gridPoint.
    private Enums.Element GetElementAtGridPoint(GridPoint gridPoint)
    {
        if (!IsGridPointInBounds(gridPoint))
            return Enums.Element.nil;
        return gameBoard[gridPoint.X, gridPoint.Y].MatchPiece.Element;
    }

    //Sets the matchPiece at gridPoint to the matchPiece with the same element.
    private void SetElementAtGridPoint(GridPoint gridPoint, Enums.Element element)
    {
        if ((int)element <= 0)
            gameBoard[gridPoint.X, gridPoint.Y].MatchPiece = _matchPieces[(int)element + 2];
        gameBoard[gridPoint.X, gridPoint.Y].MatchPiece = _matchPieces[(int)element - 1]; //!!!!!!!!!!!! - 1
    }

    //Returns true if the gridPoint is in bounds and false if it isn't.
    public bool IsGridPointInBounds(GridPoint gridPoint)
    {
        if (gridPoint.X < 0 || gridPoint.X >= _boardWidth || gridPoint.Y < 0 || gridPoint.Y >= _boardHeight)
            return false;
        return true;
    }

    public Vector2 GetPositionFromGridPoint(GridPoint gridPoint)
    {
        return new Vector2(_holderStartOffset.x + (_pieceSize.x * gridPoint.X), _holderStartOffset.y - (_pieceSize.y * gridPoint.Y));
    }

    public void SwapPieces(bool firstSwap, ActivePieceController startPiece = null, ActivePieceController endPiece = null)
    {
        if (startPiece == null)
            startPiece = startSwapPiece;
        if (endPiece == null)
            endPiece = endSwapPiece;
        if (GetCellAtGridPoint(startPiece.GridPoint).MatchPiece.BoardFunction == Enums.MatchPieceFunction.Unmoveable)
            return;

        //BoardCell cellOne = GetCellAtGridPoint(gridPointOne);
        //ActivePieceController pieceOne = cellOne.ActivePieceController;
        if ((int)GetElementAtGridPoint(endPiece.GridPoint) > 0) //((int)GetElementAtGridPoint(gridPointTwo) > 0)
        {
            //BoardCell cellTwo = GetCellAtGridPoint(gridPointTwo);
            //ActivePieceController pieceTwo = cellTwo.ActivePieceController;

            //cellOne.SetPiece(pieceTwo);
            //cellTwo.SetPiece(pieceOne);

            //pieceOne.PieceSwappedWith = pieceTwo;
            //pieceTwo.PieceSwappedWith = pieceOne;


            MatchPieceSO endPieceHolder = endPiece.MatchPiece;
            endPiece.SetUp(startPiece.MatchPiece);
            startPiece.SetUp(endPieceHolder);

            if (firstSwap)
                swappedPieces.Add(new SwappedPieces(startPiece, endPiece));

            piecesUpdating.Add(startPiece);
            piecesUpdating.Add(endPiece);
        }
        else
            ResetPiece(startPiece);
    }

    private SwappedPieces GetSwappedPieces(ActivePieceController piece)
    {
        SwappedPieces thisSwappedPieces = null;
        for (int index = 0; index < swappedPieces.Count; index++)
        {
            if (swappedPieces[index].GetOtherPiece(piece) != null)
            {
                thisSwappedPieces = swappedPieces[index];
                break;
            }
        }
        return thisSwappedPieces;
    }

    public void ResetPiece(ActivePieceController piece)
    {
        piece.ResetPositionOnBoard();
        piecesUpdating.Add(piece);
    }
    public void DeselectAllPieces()
    {
        for (int x = 0; x < owner.Game.BoardWidth; x++)
        {
            for (int y = 0; y < owner.Game.BoardHeight; y++)
            {
                if (gameBoard[x, y].MatchPiece.BoardFunction == Enums.MatchPieceFunction.Unmoveable)
                    continue;
                gameBoard[x, y].ActivePieceController.GetComponent<Button>().enabled = true;
                gameBoard[x, y].ActivePieceController.SelectedBorder.enabled = false;
            }
        }
        startSwapPiece = null;
        endSwapPiece = null;
        //Debug.Log(owner.Name + " - deselects all pieces");
    }

    private void ApplyGravityToBoard()
    {
        for (int x = 0; x < _boardWidth; x++)
        {
            for (int y = (_boardHeight - 1); y >= 0; y--)
            {
                GridPoint gridPoint = new GridPoint(x, y);
                ActivePieceController cellPiece = GetCellAtGridPoint(gridPoint).ActivePieceController;
                Enums.Element element = GetElementAtGridPoint(gridPoint);

                //If the cell isn't a hole.
                if (element != Enums.Element.Empty) 
                    continue;
                for (int nextY = (y - 1); nextY >= -1; nextY--)
                {
                    GridPoint nextGridPoint = new GridPoint(x, nextY);
                    Enums.Element nextElement = GetElementAtGridPoint(nextGridPoint);
                    if (nextElement == Enums.Element.nil)
                        continue;
                    if (nextElement != Enums.Element.Empty)
                    {
                        ActivePieceController gotPiece = GetCellAtGridPoint(nextGridPoint).ActivePieceController; /////!!!!!!!
                        cellPiece.SetUp(gotPiece.MatchPiece);
                        piecesUpdating.Add(gotPiece);

                        gotPiece.SetUp(EmptyPiece); //gotPiece.SetUp(null);

                    }
                    else
                    {
                        Enums.Element newElement = GetRandomPiece().Element;
                    }
                    break;
                }
            }
        }
    }
    //Returns an element not in elementsNotUsed.
    private Enums.Element NewElement(ref List<Enums.Element> elementsNotUsed)
    {
        List<Enums.Element> avaliableElements = new List<Enums.Element>();
        for (int index = 0; index < _matchPieces.Length; index++)
            avaliableElements.Add(_matchPieces[index].Element); //!!!!!!!!!!!!! index + 1
        foreach (Enums.Element element in elementsNotUsed)
            avaliableElements.Remove(element);

        if (avaliableElements.Count <= 0)
            return Enums.Element.nil;
        return avaliableElements[randomSeed.Next(0, avaliableElements.Count)];
    }

    
    private BoardCell GetCellAtGridPoint(GridPoint gridPoint)
    {
        return gameBoard[gridPoint.X, gridPoint.Y];
    }

    //Returns a random piece from Match Pieces.
    private MatchPieceSO GetRandomPiece()
    {
        int matchPieceIndex = 1;
        matchPieceIndex = (randomSeed.Next(0, 100) / (100 / _matchPieces.Length)); // +1
        return _matchPieces[matchPieceIndex];
    }

    //Generates a random seed that determines the board piece generation.
    private string GenerateNewSeed()
    {
        string seed = "";
        string acceptableChars = "ABCEDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        for (int iteration = 0; iteration < 20; iteration++)
            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
        return seed;
    }
}

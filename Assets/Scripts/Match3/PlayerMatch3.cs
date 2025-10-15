using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerMatch3 : MonoBehaviour
{
    private Player owner;
    [SerializeField] private MultiplayerEventSystem _eventSystem;
    private MatchPieceMovement pieceMover;

    [Header("Board")]
    private Inspector2DArrayLayout gameBoardLayout;
    private BoardCell[,] gameBoard;
    [SerializeField] private int _boardWidth = 8;
    [SerializeField] private int _boardHeight = 8;

    [Header("Pieces")]
    [SerializeField] private GameObject _matchPieceObjectPrefab;
    [SerializeField] private RectTransform _matchPieceHolder;
    [SerializeField] private Vector2 _holderStartOffset;
    [SerializeField] private Vector2 _pieceSize;

    private List<SwappedPieces> swappedPieces = new List<SwappedPieces>();
    private ActivePieceController startSwapPiece = null;
    private ActivePieceController endSwapPiece = null;
    private bool isSelecting = false;

    private int[] pieceFills;

    private System.Random randomSeed;
    private List<ActivePieceController> piecesUpdating = new List<ActivePieceController>();

    #region Get/Setters
    public Vector2 HolderStartOffset { get => _holderStartOffset; set => _holderStartOffset = value; }
    public Vector2 PieceSize { get => _pieceSize; set => _pieceSize = value; }
    public MultiplayerEventSystem EventSystem { get => _eventSystem; set => _eventSystem = value; }
    public MatchPieceMovement PieceMover { get => pieceMover; set => pieceMover = value; }
    public BoardCell[,] GameBoard { get => gameBoard; set => gameBoard = value; }
    public int BoardWidth { get => _boardWidth; set => _boardWidth = value; }
    public int BoardHeight { get => _boardHeight; set => _boardHeight = value; }
    public Player Owner { get => owner; set => owner = value; }
    public ActivePieceController StartSwapPiece { get => startSwapPiece; set => startSwapPiece = value; }
    public ActivePieceController EndSwapPiece { get => endSwapPiece; set => endSwapPiece = value; }
    public bool IsSelecting { get => isSelecting; set => isSelecting = value; }

    #endregion

    private void Start()
    {
        pieceFills = new int[_boardWidth];
        gameBoardLayout = BoardManager.Instance.GetRandomBoard();
        pieceMover = GetComponent<MatchPieceMovement>();
        StartGame();
        //if (_matchPieceHolder.GetComponent<GridLayoutGroup>() != null)
        //    _matchPieceHolder.GetComponent<GridLayoutGroup>().enabled = false;
    }
    private void FixedUpdate()
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

            int x = (int)piece.GridPoint.X;
            pieceFills[x] = Mathf.Clamp(pieceFills[x] - 1, 0, _boardWidth);

            List<GridPoint> connectedPieces = GetConnectedPieces(piece.GridPoint, true);
            List<GridPoint> connectedPiecesToPiece = GetConnectedPieces(piece.GridPoint, true);
            List<GridPoint> connectedPiecesToSwappedPiece = new List<GridPoint>();
            bool wasSwapped = (swapped != null);
            if (wasSwapped)
            {
                swappedPiece = swapped.GetOtherPiece(piece);
                connectedPiecesToSwappedPiece = GetConnectedPieces(swappedPiece.GridPoint, true);
                CombineGridPoints(ref connectedPieces, GetConnectedPieces(swappedPiece.GridPoint, true));
            }

            //Swap back if no match.
            if (connectedPieces.Count == 0)
            {
                if (wasSwapped)
                    SwapPieces(false, piece, swappedPiece);
            }
            else
            {
                //If two matches are made with the same swap.
                if (connectedPiecesToSwappedPiece.Count > 0)
                    RegisterMatch(new Match(owner, swappedPiece.MatchPiece.Element, connectedPiecesToPiece)); //!!!
                if (connectedPiecesToPiece.Count > 0)
                    RegisterMatch(new Match(owner, piece.MatchPiece.Element, connectedPieces)); //!!!
                foreach (GridPoint gridPoint in connectedPieces)
                {
                    ActivePieceController cellPiece = GetCellAtGridPoint(gridPoint).ActivePieceController;
                    if (cellPiece != null)
                        cellPiece.GetComponent<Image>().enabled = false;
                    cellPiece.PlayBreakParticles();
                    cellPiece.SetUp(GameManager.Instance.EmptyPiece); 
                    
                }
               
            }
            ApplyGravityToBoard();
            FillEmptyPieces();
            swappedPieces.Remove(swapped);
            RemoveUpdatingPiece(ref piecesUpdating, piece);
            ElimateConnectedPieces();
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
                gameBoard[x, y] = new BoardCell((gameBoardLayout.Columns[y].Row[x]) ? GameManager.Instance.WallPiece : GetRandomPiece(), new GridPoint(x, y));
        }
    }
    
    //Reshuffles board with new pieces.
    public void ReshuffleBoard()
    {
        owner.AudioManager.PlaySound("ReshuffleBoard");
        for (int index = _matchPieceHolder.childCount - 1; index >= 0; index--)
            Destroy(_matchPieceHolder.GetChild(index).gameObject);
        //_matchPieceHolder.GetComponent<GridLayoutGroup>().enabled = true;
        for (int x = 0; x < _boardWidth; x++)
        {
            for (int y = 0; y < _boardHeight; y++)
            {
                if (gameBoard[x, y].MatchPiece != GameManager.Instance.WallPiece)
                    gameBoard[x, y].ActivePieceController.SetUp(GetRandomPiece());
            }
        }
        ValidateGameBoard();
        InstantiateGameBoard();
        SetSelectedPieceToStartPiece();
    }

    //Checks over all pieces on the board and removes matches.
    private void ValidateGameBoard(bool pieceObjectsSpawned = false)
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
                    if (pieceObjectsSpawned)
                        gameBoard[x, y].ActivePieceController.SetUp(GameManager.Instance.MatchPieces[(int)NewElement(ref elementsToRemove) - 1]);
                    else
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
                Enums.Element element = gameBoard[x, y].MatchPiece.Element.Element;
                //if ((int)element <= 0)
                //    continue;

                GameObject matchPieceObject = Instantiate(_matchPieceObjectPrefab, _matchPieceHolder);
                matchPieceObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(_holderStartOffset.x + (_pieceSize.x * x), _holderStartOffset.y - (_pieceSize.y * y));
                matchPieceObject.GetComponent<ActivePieceController>().SetUp(owner, gameBoard[x, y].MatchPiece, new GridPoint(x, y));

                gameBoard[x, y].ActivePieceController = matchPieceObject.GetComponent<ActivePieceController>(); //!!!
            }
        }
        //_matchPieceHolder.GetComponent<GridLayoutGroup>().enabled = false;
    }

    public void ChangePercentOfPiecesToElement(ElementSO element, float percentage)
    {
        int numberOfPiecesToChange = (int)(GetNumberOfPossiblePiecesOnBoard() * percentage);
        List<HolderPiece> piecesChanged = new List<HolderPiece>();
        List<HolderPiece> piecesReverted = new List<HolderPiece>();
        while (piecesChanged.Count < numberOfPiecesToChange)
        {
            for (int x = 0; x < _boardWidth; x++)
            {
                for (int y = 0; y < _boardHeight; y++)
                {
                    if (gameBoard[x, y].MatchPiece == GameManager.Instance.WallPiece)// || gameBoard[x, y].MatchPiece.Element == GameManager.Instance.MatchPieces[(int)element.Element - 1])
                        continue;
                    if (UnityEngine.Random.Range(0, 100) < (int)(percentage * 100))
                    {
                        piecesChanged.Add(new HolderPiece(new GridPoint(x, y), gameBoard[x, y].MatchPiece.Element));
                        gameBoard[x, y].ActivePieceController.SetUp(GameManager.Instance.MatchPieces[(int)element.Element - 1]);
                    }
                }
            }
            ValidateGameBoard(true);
            for (int index = piecesChanged.Count - 1; index >= 0; index--)
            {
                if (GetElementAtGridPoint(piecesChanged[index].GridPoint) != element.Element)
                {
                    piecesReverted.Add(piecesChanged[index]);
                    piecesChanged.RemoveAt(index);
                }
            }
        }
        foreach (HolderPiece piece in piecesReverted)
            gameBoard[piece.GridPoint.X, piece.GridPoint.Y].ActivePieceController.SetUp(GameManager.Instance.MatchPieces[(int)piece.Element.Element - 1]);
        ValidateGameBoard(true);
    }

    public void ChangeNumberOfPiecesToPiece(MatchPieceSO piece, int numberOfPieces)
    {
        List<GridPoint> changedPieces = new List<GridPoint>();
        while (changedPieces.Count < numberOfPieces)
        {
            int randomX = UnityEngine.Random.Range(0, _boardWidth);
            int randomY = UnityEngine.Random.Range(0, _boardHeight);
            if (gameBoard[randomX, randomY].MatchPiece == GameManager.Instance.WallPiece)
                continue;
            changedPieces.Add(new GridPoint(randomX, randomY));
        }
        foreach (GridPoint gridPoint in changedPieces)
        {
            gameBoard[gridPoint.X, gridPoint.Y].ActivePieceController.SetUp(piece);
        }
    }
    public IEnumerator HackOpponentBoardSuperDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        for (int x = 0; x < _boardWidth; x++)
        {
            for (int y = 0; y < _boardHeight; y++)
            {
                if (gameBoard[x, y].MatchPiece == GameManager.Instance.VirusPiece)
                {
                    gameBoard[x,y].ActivePieceController.GetComponent<Image>().enabled = false;
                    gameBoard[x, y].ActivePieceController.SetUp(GameManager.Instance.EmptyPiece);
                    AddUpdatingPiece(ref piecesUpdating, gameBoard[x, y].ActivePieceController);
                }
            }
        }
        GameManager.Instance.GetOpponent(owner).UiHandler.DeactivateSuperVisual();
    }


    private int GetNumberOfPossiblePiecesOnBoard()
    {
        int pieceCount = 0;
        for (int x = 0; x < _boardWidth; x++)
        {
            for (int y = 0; y < _boardHeight; y++)
            {
                if (gameBoard[x, y].MatchPiece != GameManager.Instance.WallPiece)
                    pieceCount++;
            }
        }
        return pieceCount;
    }

    //Gets rid of all matches once all the pieces have landed.
    private void ElimateConnectedPieces()
    {
        if (!IsBoardFull())
            return;
        for (int x = 0; x < _boardWidth; x++)
        {
            for (int y = 0; y < _boardHeight; y++)
            {
                GridPoint pointChecked = new GridPoint(x, y);
                if (!GetCellAtGridPoint(pointChecked).ActivePieceController.GetComponent<Button>().isActiveAndEnabled)//IsUpdating)
                    continue;
                Enums.Element element = GetElementAtGridPoint(pointChecked);
                if (element <= 0)
                    continue;
                List<GridPoint> connectedPieces = GetConnectedPieces(pointChecked, true);
                if (connectedPieces.Count > 0)
                {
                    RegisterMatch(new Match(owner, GameManager.Instance.MatchPieces[(int)element - 1].Element, connectedPieces)); //!!!
                    foreach (GridPoint gridPoint in connectedPieces)
                    {
                        ActivePieceController cellPiece = GetCellAtGridPoint(gridPoint).ActivePieceController;
                        if (cellPiece != null)
                            cellPiece.GetComponent<Image>().enabled = false; //cellPiece.gameObject.SetActive(false);
                        cellPiece.SetUp(GameManager.Instance.EmptyPiece);
                    }

                }
            }
        }
        //Detect if no matches left & reshuffle.
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

    private void RegisterMatch(Match match)
    {
        if (match.Element.Element <= 0)
            return;
        owner.CombatManager.AttackOpponent(GameManager.Instance.GetOpponent(owner), match);
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
        return gameBoard[gridPoint.X, gridPoint.Y].MatchPiece.Element.Element;
    }

    //Sets the matchPiece at gridPoint to the matchPiece with the same element.
    private void SetElementAtGridPoint(GridPoint gridPoint, Enums.Element element)
    {
        try
        {
            gameBoard[gridPoint.X, gridPoint.Y].MatchPiece = GameManager.Instance.MatchPieces[(int)element - 1];
        }
        catch (IndexOutOfRangeException)
        {
            gameBoard[gridPoint.X, gridPoint.Y].MatchPiece = GameManager.Instance.MatchPieces[(int)element + 2];
        }
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

    private void AddUpdatingPiece(ref List<ActivePieceController> updating, ActivePieceController piece)
    {
        updating.Add(piece);
        piece.GetComponent<Button>().enabled = false;
    }
    private void RemoveUpdatingPiece(ref List<ActivePieceController> updating, ActivePieceController piece)
    {
        updating.Remove(piece);
        piece.GetComponent<Button>().enabled = true;
    }
    public void SwapPieces(bool firstSwap, ActivePieceController startPiece = null, ActivePieceController endPiece = null)
    {
        if (startPiece == null)
            startPiece = startSwapPiece;
        if (endPiece == null)
            endPiece = endSwapPiece;
        if (GetCellAtGridPoint(startPiece.GridPoint).MatchPiece.BoardFunction == Enums.MatchPieceFunction.Unmoveable)
            return;

        if ((int)GetElementAtGridPoint(endPiece.GridPoint) > 0) //((int)GetElementAtGridPoint(gridPointTwo) > 0)
        {
            MatchPieceSO endPieceHolder = endPiece.MatchPiece;
            endPiece.SetUp(startPiece.MatchPiece);
            startPiece.SetUp(endPieceHolder);

            if (firstSwap)
                swappedPieces.Add(new SwappedPieces(startPiece, endPiece));

            AddUpdatingPiece(ref piecesUpdating, startPiece);//piecesUpdating.Add(startPiece);
            AddUpdatingPiece(ref piecesUpdating, endPiece);//piecesUpdating.Add(endPiece);
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
        AddUpdatingPiece(ref piecesUpdating, piece); //piecesUpdating.Add(piece);
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
        isSelecting = false;
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

                        GridPoint fallPoint = new GridPoint(x, (1 - pieceFills[x]));//(-1 - pieceFills[x]));
                        gotPiece.GetComponent<RectTransform>().anchoredPosition = GetPositionFromGridPoint(fallPoint);
                        
                        cellPiece.SetUp(gotPiece.MatchPiece);
                        AddUpdatingPiece(ref piecesUpdating, gotPiece); //piecesUpdating.Add(gotPiece);

                        gotPiece.SetUp(GameManager.Instance.EmptyPiece); //gotPiece.SetUp(null);
                        pieceFills[x]++;
                    }
                    break;
                }
            }

        }
    }
    private void FillEmptyPieces(bool topIsFull = true)
    {
        List<ActivePieceController> emptyPiecesTopRow = new List<ActivePieceController>();
        for (int x = 0; x < _boardWidth; x++)
        {
            ActivePieceController piece = GetCellAtGridPoint(new GridPoint(x, 0)).ActivePieceController;
            if (piece.MatchPiece.Element.Element == Enums.Element.Empty)
                emptyPiecesTopRow.Add(piece);
            if (piece.MatchPiece == GameManager.Instance.WallPiece)
            {
                for (int y = 1; y < _boardHeight; y++)
                {
                    ActivePieceController lowerPiece = GetCellAtGridPoint(new GridPoint(x, y)).ActivePieceController;
                    if (lowerPiece.MatchPiece.Element.Element == Enums.Element.Empty)
                        emptyPiecesTopRow.Add(lowerPiece);
                }
            }
        }
        for (int index = 0; index < emptyPiecesTopRow.Count; index++)
        {
            ActivePieceController filledPiece = emptyPiecesTopRow[index];
            if (filledPiece.MatchPiece == GameManager.Instance.WallPiece)
                continue;
            filledPiece.SetUp(GetRandomPiece());
            filledPiece.GetComponent<RectTransform>().anchoredPosition = GetPositionFromGridPoint(new GridPoint(filledPiece.GridPoint.X, -1));
            ResetPiece(filledPiece);
        }
    }

    //Returns true if the board is full and false if it isn't.
    private bool IsBoardFull()
    {
        for (int x = 0; x < _boardWidth; x++)
        {
            for (int y = 0; y < _boardHeight; ++y)
            {
                if (!gameBoard[x, y].ActivePieceController.GetComponent<Image>().isActiveAndEnabled)
                    return false;
            }
        }
        return true;
    }

    //Returns an element not in elementsNotUsed.
    private Enums.Element NewElement(ref List<Enums.Element> elementsNotUsed)
    {
        List<Enums.Element> avaliableElements = new List<Enums.Element>();
        for (int index = 0; index < GameManager.Instance.MatchPieces.Length; index++)
            avaliableElements.Add(GameManager.Instance.MatchPieces[index].Element.Element); //!!!!!!!!!!!!! index + 1
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
        matchPieceIndex = (randomSeed.Next(0, 100) / (100 / GameManager.Instance.MatchPieces.Length)); // +1
        return GameManager.Instance.MatchPieces[matchPieceIndex];
    }

    //Generates a random seed that determines the board piece generation.
    private string GenerateNewSeed()
    {
        string seed = "";
        string acceptableChars = "ABCEDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        for (int iteration = 0; iteration < 20; iteration++)
            seed += acceptableChars[UnityEngine.Random.Range(0, acceptableChars.Length)];
        return seed;
    }
}

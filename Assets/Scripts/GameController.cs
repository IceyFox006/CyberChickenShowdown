using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    public static GameController Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }
}

    //[Header("Game Board")]
    //[SerializeField] private Inspector2DArrayLayout _boardLayout;
    //private BoardCell[,] gameBoard;
    //[SerializeField] private RectTransform _matchPieceArea;
    //[SerializeField] private int _boardWidth = 10;
    //[SerializeField] private int _boardHeight = 10;

    //[Header("Match Pieces")]
    //[SerializeField] private MatchPieceSO[] _matchPieces;
    //[SerializeField] private MatchPieceSO _nilMatchPiece;
    //[SerializeField] private GameObject _matchPieceObjectPrefab;
    //[SerializeField] private Vector2 _topLeftPiecePosition;
    //[SerializeField] private Vector2 _matchPieceSize;

    //#region
    //public int BoardWidth { get => _boardWidth; set => _boardWidth = value; }
    //public int BoardHeight { get => _boardHeight; set => _boardHeight = value; }
    //public static GameController Instance { get => instance; set => instance = value; }
    //public Vector2 TopLeftPiecePosition { get => _topLeftPiecePosition; set => _topLeftPiecePosition = value; }
    //public Vector2 MatchPieceSize { get => _matchPieceSize; set => _matchPieceSize = value; }
    //#endregion

    //System.Random randomSeed;
    //private void Start()
    //{
    //    instance = this;

    //    if (_boardLayout.Columns[1].Row.Length <= 0)
    //    {
    //        for (int y = 0; y < _boardHeight; y++)
    //        {
    //            for (int x = 0; x < _boardWidth; x++)
    //            {
    //                _boardLayout.Columns[y].Row = new bool[_boardWidth];
    //            }
    //        }
    //    }
    //    StartGame();
    //}
//    private void StartGame()
//    {
//        randomSeed = new System.Random(GenerateNewSeed().GetHashCode());
//        GenerateGameBoard();
//        ValidateGameBoard();
//        GenerateGameBoardPieces();
//    }

//    //Generates a new random game board.
//    private void GenerateGameBoard()
//    {
//        //Sets board size
//        gameBoard = new BoardCell[_boardWidth, _boardHeight];

//        //Generates board cells and the match piece data they contain.
//        for (int y  = 0; y < _boardHeight; y++)
//        {
//            for (int x = 0; x < _boardWidth; x++)
//                gameBoard[x, y] = new BoardCell(_boardLayout.Columns[y].Row[x] ? FillCellWithPiece(true) : FillCellWithPiece(), new GridPoint(x, y));
//        }
//    }

//    //Generates the game board pieces game objects.
//    private void GenerateGameBoardPieces()
//    {
//        for (int x = 0; x < _boardWidth; x++)
//        {
//            for (int y = 0; y < _boardHeight; y++)
//            {
//                Enums.MatchPieceElement element = gameBoard[x, y].MatchPiece.Element;
//                //if (element == Enums.MatchPieceElement.nil)
//                //    continue;
//                GameObject matchPieceObject = Instantiate(_matchPieceObjectPrefab, _matchPieceArea);
//                matchPieceObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(_topLeftPiecePosition.x + (_matchPieceSize.x * x), - _topLeftPiecePosition.y - (_matchPieceSize.y * y));
//                if (element == Enums.MatchPieceElement.nil)
//                {
//                    matchPieceObject.GetComponent<ActivePieceController>().SetUp(_nilMatchPiece, new GridPoint(x, y));
//                    continue;
//                }
//                matchPieceObject.GetComponent<ActivePieceController>().SetUp(_matchPieces[(int)element], new GridPoint(x, y));
//            }
//        }
//    }
    
//    //Fills the cell with a random match piece.
//    private MatchPieceSO FillCellWithPiece(bool isNilPiece = false)
//    {
//        if (isNilPiece)
//            return _nilMatchPiece;
//        int matchPieceIndex = randomSeed.Next(0, 100) / (100 / _matchPieces.Length);//Random.Range(0, _matchPieces.Length);
//        return _matchPieces[matchPieceIndex];
//    }

//    //If the board already has 3 in a row or more in will reshuffle until there are no matches.
//    private void ValidateGameBoard()
//    {
//        List<Enums.MatchPieceElement> removedElements;
//        for (int x = 0; x < _boardWidth; x++)
//        {
//            for (int y = 0; y < _boardHeight; y++)
//            {
//                GridPoint pointChecked = new GridPoint(x, y);
//                Enums.MatchPieceElement element = GetElementAtGridPoint(pointChecked);
//                if ((int)element < 0)
//                    continue;

//                removedElements = new List<Enums.MatchPieceElement>();
//                while (PiecesAreConnected(pointChecked, true).Count > 0)
//                {
//                    element = GetElementAtGridPoint(pointChecked);
//                    if (!removedElements.Contains(element))
//                    {
//                        Debug.Log(pointChecked.X + "," + pointChecked.Y + "\t:" + element.ToString());
//                        removedElements.Add(element);
//                    }
//                    SetElementAtGridPoint(pointChecked, NewElement(ref removedElements));
//                    break;
//                }
//            }
//        }
//    }

//    //Gets the element of the piece at a grid point if the grid point is within the board bounds.
//    private Enums.MatchPieceElement GetElementAtGridPoint(GridPoint gridPoint)
//    {
//        //If gridPoint is in bounds of the board size.
//        if (gridPoint.X < 0 || gridPoint.X >= _boardWidth || gridPoint.Y < 0 || gridPoint.Y >= _boardHeight)
//            return Enums.MatchPieceElement.nil;
//        return gameBoard[gridPoint.X, gridPoint.Y].MatchPiece.Element;
//    }

//    //Sets the match piece at the grid point to the match point that has the given element.
//    private void SetElementAtGridPoint(GridPoint gridPoint, Enums.MatchPieceElement element)
//    {
//        if ((int)element < 0)
//            gameBoard[gridPoint.X, gridPoint.Y].MatchPiece = _nilMatchPiece;
//        else
//            gameBoard[gridPoint.X, gridPoint.Y].MatchPiece = _matchPieces[(int)element];

//    }

//    //Returns an element that is not in removedElements.
//    private Enums.MatchPieceElement NewElement(ref List<Enums.MatchPieceElement> removedElements)
//    {
//        List<Enums.MatchPieceElement> avaliableElements = new List<Enums.MatchPieceElement>();
//        for (int index = 0; index < _matchPieces.Length; index++)
//            avaliableElements.Add(_matchPieces[index].Element); //!!!!!
//        foreach (Enums.MatchPieceElement element in removedElements)
//            avaliableElements.Remove(element);

//        if (avaliableElements.Count <= 0)
//            return Enums.MatchPieceElement.nil;
//        return avaliableElements[randomSeed.Next(0, avaliableElements.Count)];
//    }

//    //Returns a list of pieces of the same element that are connected to eachother.
//    private List<GridPoint> PiecesAreConnected(GridPoint gridPoint, bool isFirstPieceChecked)
//    {
//        List<GridPoint> connectedPieces = new List<GridPoint>();
//        Enums.MatchPieceElement element = GetElementAtGridPoint(gridPoint);
//        GridPoint[] directions = {GridPoint.up, GridPoint.right, GridPoint.down, GridPoint.left};

//        //If GridPoint being checked is on the END of a straight line.
//        foreach (GridPoint directionPoint in directions)
//        {
//            List<GridPoint> piecesInALine = new List<GridPoint>();
//            int numberOfPiecesInALine = 0;

//            for (int index = 0; index < 3; index++)
//            {
//                GridPoint nextPointChecked = GridPoint.Add(gridPoint, GridPoint.Multiply(directionPoint, index));
//                if (GetElementAtGridPoint(nextPointChecked) == element)
//                {
//                    piecesInALine.Add(nextPointChecked);
//                    numberOfPiecesInALine++;
//                }
//            }
//            //If a row has more than 1 pieces.
//            if (numberOfPiecesInALine > 1)
//                AddGridPoints(ref connectedPieces, piecesInALine);
//        }

//        //If GridPoint being checked is in the MIDDLE in a straight line.
//        for (int index = 0; index < 2; index++)
//        {
//            List<GridPoint> piecesInALine = new List<GridPoint>();
//            int numberOfPiecesInALine = 0;

//            GridPoint[] checkedPoints = { GridPoint.Add(gridPoint, directions[index]), GridPoint.Add(gridPoint, directions[index + 2]) };
//            foreach (GridPoint checkedPoint in checkedPoints)
//            {
//                if (GetElementAtGridPoint(checkedPoint) == element)
//                {
//                    piecesInALine.Add(checkedPoint);
//                    numberOfPiecesInALine++;
//                }
//            }
//            //If a row has more than 1 pieces.
//            if (numberOfPiecesInALine > 1)
//                AddGridPoints(ref connectedPieces, piecesInALine);
//        }

//        //Checks for a 2x2 match.
//        for (int index = 0; index < 4; index++)
//        {
//            List<GridPoint> piecesInASquare = new List<GridPoint>();
//            int numberOfPiecesInASquare = 0;
//            int nextDirection = index + 1;
//            if (nextDirection >= 4)
//                nextDirection -= 4;

//            GridPoint[] checkedPoints = { GridPoint.Add(gridPoint, directions[index]), GridPoint.Add(gridPoint, directions[nextDirection]), GridPoint.Add(gridPoint, directions[index]), GridPoint.Add(gridPoint, directions[nextDirection]) };
//            foreach (GridPoint checkedPoint in checkedPoints)
//            {
//                if (GetElementAtGridPoint(checkedPoint) == element)
//                {
//                    piecesInASquare.Add(checkedPoint);
//                    numberOfPiecesInASquare++;
//                }
//            }
//            if (numberOfPiecesInASquare > 2)
//                AddGridPoints(ref connectedPieces, piecesInASquare);
//        }

//        //Checks for other connected matches along the current piece being matched.
//        if (isFirstPieceChecked)
//        {
//            for (int index = 0; index < connectedPieces.Count; index++)
//                AddGridPoints(ref connectedPieces, PiecesAreConnected(connectedPieces[index], false));
//        }
//        if (connectedPieces.Count > 0)
//            connectedPieces.Add(gridPoint);
//        return connectedPieces;
//    }

//    //Adds the points of added points to grid points if the added point is not equal to any of the grid points.
//    private void AddGridPoints(ref List<GridPoint> gridPoints, List<GridPoint> addedPoints)
//    {
//        foreach(GridPoint addedPoint in addedPoints)
//        {
//            bool canAddPoints = true;
//            for (int index = 0; index < gridPoints.Count; index++)
//            {
//                //Checks if the values of the grid point is equal to the values of added point
//                if (gridPoints[index].Equals(addedPoint))
//                {
//                    canAddPoints = false;
//                    break;
//                }
//            }
//            if (canAddPoints)
//                gridPoints.Add(addedPoint);
//        }
//    }

//    //Generates a random seed that determines the board set up.
//    private string GenerateNewSeed()
//    {
//        string seed = "";
//        string acceptableChars = "ABCEDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
//        for (int iteration = 0; iteration < 20; iteration++)
//            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
//        return seed;
//    }
//}

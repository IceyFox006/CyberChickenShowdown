/*
 * Marlow Greenan
 * 8/31/2025
 * 
 * Stores enums.
 */
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum MatchPieceFunction
    {
        Unmoveable,
        Normal,
        RowKO,
        ColumnKO,
        CrossKOsmall,
        CrossKOlarge,
    }
    public enum Element
    {
        nil = -1,
        Blank = 0,
        Plasma = 1,
        Gravity = 2,
        Fire = 3,
        Hack = 4,
        Metal = 5,
    }
    public enum MatchPieceElement
    {
        nil     = -1,
        Blank   = 0,
        Fire    = 1,
        Air     = 2,
        Water   = 3,
        Earth   = 4,
        Aether  = 5,
    }
}

/*
 * Marlow Greenan
 * 8/31/2025
 */
using UnityEngine;

[System.Serializable]
public class Inspector2DArrayLayout
{
    [System.Serializable]
    public struct rowData
    {
        [SerializeField] private bool[] row;

        public bool[] Row { get => row; set => row = value; }
    }
    //public Grid grid;
    [SerializeField] private rowData[] columns = new rowData[8];

    public rowData[] Columns { get => columns; set => columns = value; }
}

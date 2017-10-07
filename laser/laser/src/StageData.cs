using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laser
{
    [Serializable]
    public struct StageData
    {
        public Cell[,] MapCells;
        public LaserData[,] LaserDatas;
        public int[] TowerData;
        public string Next;
    }

    [Serializable]
    public struct LaserData
    {
        public bool IsLaser0;
        public bool IsLaser1;
        public bool IsLaser3;
        public int X;
        public int Y;
    }

    [Serializable]
    public struct Cell
    {
        public CellState State;
        public int X;
        public int Y;
    }

    public enum CellState
    {
        Other = 0,
        Path = 1,
        Start = 2,
        End = 3
    }
}

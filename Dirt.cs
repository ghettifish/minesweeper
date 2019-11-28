using System.Collections.Generic;

namespace minesweeper
{
    public class Dirt
    {
        public TileStateEnum TileState { get; set; } = TileStateEnum.HIDDEN;
        public int MineCount { get; set; } = 0;
        public bool IsMine { get; set; } = false;
        public enum TileStateEnum { HIDDEN, FLAGGED, REVEALED }
        public bool Dig() {
            return !IsMine;
        }
        public string GetConsoleElement() {
            Dictionary<TileStateEnum, string> elements = new Dictionary<TileStateEnum, string>();
            elements.Add(TileStateEnum.HIDDEN, "*");
            elements.Add(TileStateEnum.FLAGGED, "|");
            elements.Add(TileStateEnum.REVEALED, MineCount.ToString());
            if(MineCount == 0 && TileState == TileStateEnum.REVEALED)
                return " ";

            return elements[TileState];

        }
    }
}
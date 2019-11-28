using System;
using System.Collections.Generic;

namespace minesweeper
{
    public class Difficulty
    {
        DifficultyEnum DifficultySetting {get; set; }
        public Difficulty(string difficulty) {
           Enum.TryParse(difficulty, out Difficulty.DifficultyEnum difficultySetting);
           DifficultySetting = difficultySetting;
        }
        public DifficultyDef GetSettings() {
            return DifficultyDictionary[DifficultySetting];
        }
        public DifficultyDef SetDifficulty(DifficultyEnum difficulty) {
            DifficultySetting = difficulty;
            return DifficultyDictionary[DifficultySetting];
        }
        public Dictionary<DifficultyEnum, DifficultyDef> DifficultyDictionary = new Dictionary<DifficultyEnum, DifficultyDef>() 
        {
            {DifficultyEnum.EASY, new DifficultyDef(3, 4)},
            {DifficultyEnum.MEDIUM, new DifficultyDef(12, 12)},
            {DifficultyEnum.HARD, new DifficultyDef(24, 24)}
        };
        public enum DifficultyEnum { EASY, MEDIUM, HARD}
    }
    public class DifficultyDef {
        public DifficultyDef(int mineCount, int boardSize) {
            MineCount = mineCount;
            BoardSize = boardSize;
        }
        public int MineCount {get;}
        public int BoardSize {get;}
    }
}
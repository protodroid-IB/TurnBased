using System.Collections;
using System.Collections.Generic;

// public enums that can be accessed by any class 
public enum GameState {Overworld, TransitionToBattle, Battle, TransitionToOverworld};

public enum BattleState {Begin, TurnSelect, PlayerTurn, EnemyTurn, ApplyMove, CheckBattle, TurnSwitch, End};

public enum FighterType { Normal, Poison, Fire, Arcane};

public enum Move { Punch, Mansplain, Flex, Squat, Spit, Eat, Flatulence, Feces, Burn, Warm, Fuel, Play, BuildWall, Tweet, Threaten, Flail };


[System.Serializable]
public struct Moves
{
    public Move move01, move02, move03, move04;
}

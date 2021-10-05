using System;
using System.Collections.Generic;

[Serializable]
public class Section
{
    public Section() { }
    public object collectable_itens;
    public string conclusion;
    public string dateTimeStart;
    public string dateTimeFinish;
    public List<string> finalized_challenges;
    public List<string> path_player;
    public int performance;
    public string route_image_b64;
}

[Serializable]
public class Phase
{
    public Phase() { }
    public string phase_id;
    public List<Section> sections = new List<Section>();
    public string status;
}

[Serializable]
public class GameData
{
    public GameData() { }
    public string custom_report;
    public int number_phases;
    public List<Phase> phases = new List<Phase>();
    public int phases_unlocked;
    public int player_minutes_in_game;
}

[Serializable]
public class PlayerData
{
    public PlayerData() { }
    public string day_birthday;
    public string gender;
    public string id;
    public string name;
}

[Serializable]
public class TemplateGLBoard
{
    public TemplateGLBoard() { }
    public GameData game_data = new GameData();
    public PlayerData player_data = new PlayerData();
}


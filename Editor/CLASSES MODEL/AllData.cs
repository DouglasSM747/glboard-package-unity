using System;
using System.Collections.Generic;

[Serializable]
public class Section
{
    public object collectable_itens;
    public string conclusion;
    public DateTime dateTimeStart;
    public DateTime dateTimeFinish;
    public List<string> finalized_challenges;
    public int performance;
    public string route_image_b64;
}

[Serializable]
public class Phase
{
    public string phase_id;
    public List<Section> sections;
    public string status;
}

[Serializable]
public class GameData
{
    public string custom_report;
    public int number_phases;
    public List<Phase> phases;
    public int phases_unlocked;
    public int player_hours_in_game;
}

[Serializable]
public class PlayerData
{
    public string day_birthday;
    public string gender;
    public string id;
    public string name;
}

[Serializable]
public class TemplateGLA
{
    public GameData game_data;
    public PlayerData player_data;
}


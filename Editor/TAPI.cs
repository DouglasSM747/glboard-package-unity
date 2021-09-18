using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class TAPI
{
    private TemplateGLA data = new TemplateGLA();

    private string userId;
    private string gameId;

    public string USER_ID { get => userId; set => userId = value; }
    public string GAME_ID { get => gameId; set => gameId = value; }


    public TAPI(string game_id, string userId)
    {
        this.GAME_ID = game_id;
        this.USER_ID = userId;
    }


    public async Task LOAD_USER_DATA(string idUser)
    {
        string result = await CommonCode.Get("http://192.168.1.3:5000/data-user/" + GAME_ID + "/" + idUser);
        data = JsonUtility.FromJson<TemplateGLA>(result);
    }

    public void SetPlayerData(string name = "", string id = "", GENDER gender = GENDER.OUTROS, string day_birthday = "")
    {
        this.data.player_data.name = name;
        this.data.player_data.id = id;
        this.data.player_data.gender = gender.ToString();
        this.data.player_data.day_birthday = day_birthday;
    }

    public PlayerData GetPlayerData()
    {
        return this.data.player_data;
    }

    public void SetGameData(GameData data)
    {
        this.data.game_data = data;
    }

    public GameData GetGameData()
    {
        return this.data.game_data;
    }

    public void SetQuantPhaseGame(int quantPhases)
    {
        this.data.game_data.number_phases = quantPhases;
    }

    public int GetQuantPhaseGame()
    {
        return this.data.game_data.number_phases;
    }

    public void SetCustomReport(string customReport)
    {
        this.data.game_data.custom_report = customReport;
    }

    public string GetCustomReport()
    {
        return this.data.game_data.custom_report;
    }

    public void SetPhasesUnlockedPlayer(int quant)
    {
        this.data.game_data.phases_unlocked = quant;
    }
    public int GetPhasesUnlockedPlayer()
    {
        return this.data.game_data.phases_unlocked;
    }

    public void SetPlayerHoursGames(int quant)
    {
        this.data.game_data.player_hours_in_game = quant;
    }

    public int GetPlayerHoursGames()
    {
        return this.data.game_data.player_hours_in_game;
    }
    public void SetPhasesGames(List<Phase> phases)
    {
        this.data.game_data.phases = phases;
    }

    public List<Phase> GetPhasesGames()
    {
        return this.data.game_data.phases;
    }

    public void PushPhaseGame(string phase_id)
    {
        if (this.data.game_data.phases_unlocked + 1 <= this.data.game_data.number_phases)
        {
            Phase phase = new Phase();
            phase.phase_id = phase_id;
            phase.sections = new List<Section>();
            phase.status = STATUS_PHASE.DESBLOQUEADA.ToString();
            this.data.game_data.phases.Add(phase);
        }
        else
        {
            throw new InvalidDataGameException("Número de fases excede o números de fases informado [number_phases]");
        }
    }

    public void SetPhaseGame(string phase_id, Phase phase)
    {
        for (int i = 0; i < this.data.game_data.phases.Count; i++)
        {
            if (this.data.game_data.phases[i].phase_id == phase_id)
            {
                this.data.game_data.phases[i] = phase;
                return;
            }
        }

        throw new InvalidDataGameException("Phase ID não foi encontrado na lista de fases");

    }

    public void DeletePhaseGame(string phase_id)
    {
        for (int i = 0; i < this.data.game_data.phases.Count; i++)
        {
            if (this.data.game_data.phases[i].phase_id == phase_id)
            {
                this.data.game_data.phases.RemoveAt(i);
                return;
            }
        }

        throw new InvalidDataGameException("Phase ID não foi encontrado na lista de fases");

    }

    private bool IsValidPhase(string phase_id)
    {
        for (int i = 0; i < this.data.game_data.phases.Count; i++)
        {
            if (this.data.game_data.phases[i].phase_id == phase_id)
            {
                return true;
            }
        }
        return false;
    }

    public Phase GetPhaseGame(string phase_id)
    {
        for (int i = 0; i < this.data.game_data.phases.Count; i++)
        {
            if (this.data.game_data.phases[i].phase_id == phase_id)
            {
                return this.data.game_data.phases[i];
            }
        }

        throw new InvalidDataGameException("Phase ID não foi encontrado na lista de fases");
    }

    public void PushSectionInPhase(
        string phase_id,
        STATUS_SECTION conclusion,
        int perfomance,
        DateTime dateTimeStartSection,
        DateTime dateTimeFinishSection,
        List<string> finalized_challenges,
        string route_image_b64 = "")
    {

        Section section = new Section();
        section.conclusion = conclusion.ToString();
        section.performance = perfomance;
        section.dateTimeStart = dateTimeStartSection;
        section.dateTimeFinish = dateTimeFinishSection;
        section.finalized_challenges = finalized_challenges;
        section.route_image_b64 = route_image_b64;

        if (!IsValidPhase(phase_id))
        {
            throw new InvalidDataGameException("O Phase ID fornecido é invalido");
        }

        for (int i = 0; i < this.data.game_data.phases.Count; i++)
        {
            if (this.data.game_data.phases[i].phase_id == phase_id)
            {
                this.data.game_data.phases[i].sections.Add(section);
            }
        }
    }
}

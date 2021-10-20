using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class GLBoard
{
    private TemplateGLBoard data = new TemplateGLBoard();

    private string userId = null;
    private string gameId = null;

    public string USER_ID { get => userId; set => userId = value; }
    public string GAME_ID { get => gameId; set => gameId = value; }

    /// <summary>
    /// Construtor. Recebe como parametro o ID do Game e Id do User
    /// </summary>
    /// <param name="game_id"></param>
    /// <param name="userId"></param>
    public GLBoard(string game_id, string userId)
    {
        this.GAME_ID = game_id;
        this.USER_ID = userId;
    }

    /// <summary>
    /// Carrega os dados do usuario que foi informado no construtor.
    /// </summary>
    /// <returns>Não possui retorno, mas atribui a variavel data os dados do usuario</returns>
    public async Task LOAD_USER_DATA()
    {

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            throw new Exception("Não é possivel carregar os dados do usuario pois não há conexão com Internet!");
        }

        try
        {
            var result = await CommonCode.Get(CommonCode.API_HOST + "data-user/" + GAME_ID + "/" + USER_ID);

            if (result == null)
            {
                Debug.Log("Esse usuario ainda não possue registros! Adicione informações.");
            }
            else
            {
                data = JsonUtility.FromJson<TemplateGLBoard>(result);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    /// <summary>
    /// Envia os dados do usuario para o banco de dados
    /// </summary>
    /// <returns></returns>
    public IEnumerator SEND_USER_DATA()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            throw new Exception("Não é possivel enviar os dados do usuario pois não há conexão com Internet!");
        }

        try
        {
            return CommonCode.Post(CommonCode.API_HOST + "data-user/" + GAME_ID + "/" + USER_ID, JsonUtility.ToJson(data));
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }


        return null;
    }

    /// <summary>
    /// Atribui os dados de perfil do usuario
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="Exception">Retorna um erro caso a data de nascimento esteja em um formato não DateTime</exception>
    /// <param name="day_birthday"></param>
    /// <param name="gender"></param>
    public void SetPlayerData(string name, string day_birthday, GENDER gender = GENDER.OUTROS)
    {
        this.data.player_data.name = name;
        this.data.player_data.id = USER_ID;
        this.data.player_data.gender = gender.ToString();

        try
        {
            this.data.player_data.day_birthday = DateTime.Parse(day_birthday).ToString();
        }
        catch (Exception e)
        {
            throw new Exception("O dia de nascimento do jogador deve ser uma string que segue o modelo DateTime");
        }
    }

    /// <summary>
    /// Retorna os dados de perfil do usuario
    /// </summary>
    /// <returns>Retorna os dados de perfil do usuario</returns>
    public PlayerData GetPlayerData()
    {
        return this.data.player_data;
    }

    /// <summary>
    /// Atribui ao campo game_data um objeto do mesmo tipo.
    /// </summary>
    /// <param name="data">Objeto do tipo GameData</param>
    public void SetGameData(GameData data)
    {
        this.data.game_data = data;
    }

    /// <summary>
    /// Retorna os dados de GameData do usuario
    /// </summary>
    /// <returns>Retorna os dados de GameData do usuario</returns>
    public GameData GetGameData()
    {
        return this.data.game_data;
    }

    /// <summary>
    /// Metodo para definir o número de fases do jogo
    /// </summary>
    /// <param name="quant">Quantidade de fases</param>
    public void SetQuantPhaseGame(int quantPhases)
    {
        this.data.game_data.number_phases = quantPhases;
    }

    /// <summary>
    /// Retorna um numero de fases do jogo
    /// </summary>
    /// <returns>Retorna um numero de fases do jogo</returns>
    public int GetQuantPhaseGame()
    {
        return this.data.game_data.number_phases;
    }

    /// <summary>
    /// Metodo para definir o horario do ultimo login do jogador
    /// </summary>
    /// <param name="dateTime">Horario do ultimo login</param>
    public void SetLastLogin(DateTime dateTime)
    {
        this.data.game_data.date_last_login = dateTime.ToString();
    }

    /// <summary>
    /// Retorna o horario do ultimo login do jogador
    /// </summary>
    /// <returns>Retorna um numero de fases do jogo</returns>
    public DateTime GetLastLogin()
    {
        return DateTime.Parse(this.data.game_data.date_last_login);
    }


    /// <summary>
    /// Define o report customizado relacionado ao usuario
    /// </summary>
    /// <param name="customReport"></param>
    public void SetCustomReport(string customReport)
    {
        this.data.game_data.custom_report = customReport;
    }

    /// <summary>
    /// Retorna o relatorio customizado do usuario
    /// </summary>
    /// <returns>Retorna o relatorio customizado do usuario</returns>
    public string GetCustomReport()
    {
        return this.data.game_data.custom_report;
    }

    /// <summary>
    /// Retorna a quantidade de fases que o jogador desbloqueou
    /// </summary>
    /// <returns>Retorna a quantidade de fases que o jogador desbloqueou</returns>
    public int GetPhasesUnlockedPlayer()
    {
        return this.data.game_data.phases_unlocked;
    }

    /// <summary>
    /// Define a quantidade de tempo que o jogador jogou
    /// </summary>
    /// <param name="quant"></param>
    public void SetPlayerMinutesGame(int quant)
    {
        this.data.game_data.player_minutes_in_game = quant;
    }

    /// <summary>
    /// Retorna a quantidade de tempo que o jogador jogou
    /// </summary>
    /// <returns>Retorna a quantidade de tempo que o jogador jogou</returns>
    public int GetPlayerMinutesGame()
    {
        return this.data.game_data.player_minutes_in_game;
    }

    /// <summary>
    /// Define a lista de fases do jogo
    /// </summary>
    /// <param name="phases"></param>
    public void SetPhasesGames(List<Phase> phases)
    {
        this.data.game_data.phases = phases;
    }

    /// <summary>
    /// Retorna as fases do jogo
    /// </summary>
    /// <returns>Retorna as fases do jogo</returns>
    public List<Phase> GetPhasesGames()
    {
        return this.data.game_data.phases;
    }

    /// <summary>
    /// Insere uma nova fase no jogo
    /// </summary>
    /// <exception cref="InvalidDataGameException">Retorna um erro caso esteja tentando inserir uma fase e o número de fases seja 
    /// maior do que o informado em SetQuantPhaseGame</exception>
    /// <param name="phase_id">ID unico relacionado a fase, pode ser o nome da fase</param>
    public void AddPhaseGame(string phase_id)
    {
        Debug.Log(phase_id);
        Debug.Log(this.data.game_data.phases_unlocked);
        Debug.Log(this.data.game_data.number_phases);

        if (this.data.game_data.phases_unlocked + 1 <= this.data.game_data.number_phases)
        {
            Phase phase = new Phase();
            phase.phase_id = phase_id;
            phase.sections = new List<Section>();
            phase.status = STATUS_PHASE.NAO_FINALIZADA.ToString();
            this.data.game_data.phases.Add(phase);
        }
        else
        {
            throw new InvalidDataGameException("Número de fases excede o números de fases informado [number_phases]");
        }
    }

    /// <summary>
    /// Deleta uma fase e todas suas informações
    /// </summary>
    /// <exception cref="InvalidDataGameException">Retorna um erro caso o phase_id não esteja relacionado a nenhuma fase</exception>
    /// <param name="phase_id">ID unico de uma fase que já foi adicionada</param>
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

    /// <summary>
    /// Retorna os dados de uma fase
    /// </summary>
    /// <param name="phase_id"></param>
    /// <exception cref="InvalidDataGameException">Retorna um erro caso o phase_id não esteja relacionado a nenhuma fase</exception>
    /// <returns>Retorna um objeto do tipo Phase</returns>
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

    /// <summary>
    /// Insere uma nova sessão em uma determinada fase
    /// </summary>
    /// <exception cref="InvalidDataGameException">Retorna um erro caso o phase_id não esteja relacionado a nenhuma fase</exception>
    /// <param name="phase_id"></param>
    /// <param name="conclusion"></param>
    /// <param name="perfomance"></param>
    /// <param name="dateTimeStartSection"></param>
    /// <param name="dateTimeFinishSection"></param>
    /// <param name="finalized_challenges"></param>
    /// <param name="path_player"></param>
    /// <param name="route_image_b64"></param>
    public void AddSectionInPhase(
        string phase_id,
        STATUS_SECTION conclusion,
        int perfomance,
        DateTime dateTimeStartSection,
        DateTime dateTimeFinishSection,
        List<string> finalized_challenges = null,
        List<string> path_player = null,
        string route_image_b64 = null)
    {

        Section section = new Section();
        section.conclusion = conclusion.ToString();
        section.performance = perfomance;
        section.dateTimeStart = dateTimeStartSection.ToString();
        section.dateTimeFinish = dateTimeFinishSection.ToString();

        if (finalized_challenges == null)
        {
            section.finalized_challenges = new List<string>();
        }
        else
        {
            section.finalized_challenges = finalized_challenges;
        }

        section.path_player = path_player ?? null;
        section.route_image_b64 = route_image_b64 ?? null;

        if (!IsValidPhase(phase_id))
        {
            throw new InvalidDataGameException("O Phase ID fornecido é invalido");
        }

        for (int i = 0; i < this.data.game_data.phases.Count; i++)
        {
            if (this.data.game_data.phases[i].phase_id == phase_id)
            {
                if (section.conclusion == STATUS_SECTION.VITORIA.ToString())
                {
                    this.data.game_data.phases[i].status = STATUS_PHASE.FINALIZADA.ToString();
                    this.data.game_data.phases_unlocked++;

                }
                this.data.game_data.phases[i].sections.Add(section);
            }
        }
    }
}

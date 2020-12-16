using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MatchState { START, FP_TURN, SP_TURN, FP_END_TURN, SP_END_TURN }

public class MatchHandler : MonoBehaviour
{
    public static MatchHandler _mHandler;

    public static MatchState state;
    public int roundTime;
    [SerializeField] private GameObject playerPrefab;

    //public GameObject projPrefab;

    private GameObject matchUI;
    private Timer timerUI;
    private Scoreboard sb1;
    private Scoreboard sb2;

    private GameObject resultsUI;
    private ResultsPanel resultsPanelUI;

    private GameObject arena;
    private ArenaBuilder arenaBuilder;

    private GameObject player1;
    private GameObject player2;

    private Coroutine lastRoutine = null;

    private GameObject projectileInstance;

    void Awake()
    {

        if (_mHandler == null)
        {
            _mHandler = this;
        }
        else
        {
            Destroy(gameObject);
        }

        matchUI = GameObject.Find("Canvas/MatchUI");
        timerUI = matchUI.transform.Find("Timer").GetComponent<Timer>();
        sb1 = matchUI.transform.Find("P1_Score").GetComponent<Scoreboard>();
        sb2 = matchUI.transform.Find("P2_Score").GetComponent<Scoreboard>();

        resultsUI = GameObject.Find("Canvas/ResultsUI");
        resultsPanelUI = resultsUI.transform.Find("Panel").GetComponent<ResultsPanel>();

        arena = GameObject.Find("Arena");
        arenaBuilder = arena.GetComponent<ArenaBuilder>();
    }

    // Start is called before the first frame update
    void Start()
    {
        arena.SetActive(false);
        matchUI.SetActive(false);
        resultsUI.SetActive(false);
        roundTime = 10;
        state = MatchState.START;
        StartCoroutine(SetupMatch());
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(state);
    }

    public void EndMatch()
    {
        StopAllCoroutines();
        Destroy(player1);
        Destroy(player2);
        arena.SetActive(false);
        matchUI.SetActive(false);

        resultsPanelUI.SetWinner(sb1.score, sb2.score);
        resultsUI.SetActive(true);
    }

    public bool CheckWinCondition(int pScore)
    {
        if (pScore >= 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ScorePoint(int player)
    {
        bool win = false;
        if (player == 1)
        {
            sb1.ScorePoint();
            win = CheckWinCondition(sb1.score);
        } else if (player == 2)
        {
            sb2.ScorePoint();
            win = CheckWinCondition(sb2.score);
        }
        return win;
    }

    public void OnPlayerDeath(int player)
    {
        if(ScorePoint(2 / player))
        {
            EndMatch();
        } else
        {
            SetupRound(player);
        }
    }

    public void SpawnProjectile(ShootParams sp)
    {
        if (sp.pNum == 1) {
            projectileInstance = Instantiate(sp.prefab, sp.spawnPos, sp.spawnRot);
            state = MatchState.FP_END_TURN;
        } else if (sp.pNum == 2) {
            projectileInstance = Instantiate(sp.prefab, sp.spawnPos, sp.spawnRot);
            state = MatchState.SP_END_TURN;
        }
        StopCoroutine(lastRoutine);
        lastRoutine = StartCoroutine(WaitingForTurnToEnd(projectileInstance));
    }

    IEnumerator WaitingForTurnToEnd(GameObject projectile)
    {
        yield return new WaitUntil(() => projectile == null);
        if (state == MatchState.FP_END_TURN)
        {
            state = MatchState.SP_TURN;
        }
        else if (state == MatchState.SP_END_TURN)
        {
            state = MatchState.FP_TURN;
        }
        PlayerTurn();
    }

    IEnumerator TurnCountdown()
    {
        timerUI.SendMessage("SwapColor", roundTime);
        yield return new WaitForSeconds(roundTime);

        if(state == MatchState.FP_TURN)
        {
            state = MatchState.SP_TURN;
        } else if (state == MatchState.SP_TURN)
        {
            state = MatchState.FP_TURN;
        }
        PlayerTurn();
    }


    private void PlayerTurn()
    {
        lastRoutine = StartCoroutine(TurnCountdown());
    }

    public void SetupRound(int player)
    {
        StopAllCoroutines();
        Destroy(player1);
        Destroy(player2);

        arenaBuilder.BuildRandomLayout();

        player1 = SpawnPlayer(true);
        player2 = SpawnPlayer(false);

        ProjectileHandler._pHandler.SetInitialSprite();

        state = (MatchState)player;
        PlayerTurn();
    }

    IEnumerator SetupMatch()
    {
        resultsUI.SetActive(false);
        yield return new WaitForSeconds(1f);
        matchUI.SetActive(true);
        arena.SetActive(true);

        arenaBuilder.BuildRandomLayout();

        player1 = SpawnPlayer(true);
        player2 = SpawnPlayer(false);
        sb1.ResetScore();
        sb2.ResetScore();

        ProjectileHandler._pHandler.SetInitialSprite();

        state = (MatchState)Random.Range(1, 3);        
        PlayerTurn();
    }

    public void RestartMatch()
    {
        StopAllCoroutines();
        state = MatchState.START;
        StartCoroutine(SetupMatch());
    }
    
    private GameObject SpawnPlayer(bool isPlayerOne)
    {
        GameObject go;
        if (isPlayerOne)
        {
            go = playerPrefab;
            go.SetActive(false);
            go = Instantiate(go, new Vector3(-18.75f, -2f, 0), Quaternion.identity);
            go.tag = "Player1";
            go.SetActive(true);
        }
        else
        {
            go = playerPrefab;
            go.SetActive(false);
            go = Instantiate(go, new Vector3(18.75f, -2f, 0), transform.rotation * Quaternion.Euler(0, 0, 180f));
            go.tag = "Player2";
            go.SetActive(true);
        }
        return go;
    }

}

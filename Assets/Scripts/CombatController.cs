using Effekseer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameContext.GameContext;

public class CombatController : MonoBehaviour
{
    Vector3 SlideTargetPosition;
    Vector3 PlayerStartingPosition;
    Action OnSlideComplete;

    GameObject CurrentPlayer;
    GameObject CurrentEnemy;

    Animator CurrentPlayerAnimator;
    Animator CurrentEnemyAnimator;

    public GameObject AdditionText;

    private GameObject PlayerEffek;
    string effectName = "animationpack_elemental/ELM_Blizzard01_A";
    string guardEffek = "animationpack_support/SUP_Healing04_A";
    string slash1Effek = "animationpack_weapons/WPN_Slash02_A";

    void Start()
    {
        //PlayerData currentPlayerData = CurrentGameContext.Players.FirstOrDefault(x => x.Value.IsActiveTurn == true).Value;
        //EnemyData currentEnemyData = CurrentGameContext.Enemies.FirstOrDefault(x => x.Value.IsCurrentTarget == true).Value;


        // DEBUGGING
        CurrentEnemy = GameObject.Find("Feyrbrand");
        CurrentPlayer = GameObject.Find("Dart");
        PlayerEffek = GameObject.Find("PlayerEffek");
        CurrentPlayerAnimator = CurrentPlayer.GetComponent<Animator>();
        CurrentEnemyAnimator = CurrentEnemy.GetComponent<Animator>();

        AdditionText = GameObject.Find("AdditionText");
        AdditionText.SetActive(true);
        AdditionText.SetActive(false);
        //
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //PlayOnPlayerEffek();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // DEBUGGING
            Context.CombatTurn = CombatTurn.Player;
            //
            PlayerStartingPosition = CurrentPlayer.transform.position;
            PlayerSlideToPosition(CurrentEnemy.transform.position, CombatState.SlidingForward, () => 
            {
                Context.CombatState = CombatState.PlayerAttacking;

                Debug.Log("Slide complete");
                //TODO: Full player Attack system

                // DEBUGGING
                StartCoroutine(PerformAddition("DoubleSlash", 2f));
                
            });
        }

        // Player sliding
        if (Context.CombatState == CombatState.SlidingForward && Context.CombatTurn == CombatTurn.Player)
        {
            float slideSpeed = 10f;

            CurrentPlayer.transform.position += new Vector3((SlideTargetPosition.x - CurrentPlayer.transform.position.x) * slideSpeed * Time.deltaTime, 0, 0);

            float reachedDistance = 3.5f;
            if (Math.Round(Math.Abs(CurrentPlayer.transform.position.x - SlideTargetPosition.x), 4) <= reachedDistance)
            {
                OnSlideComplete();
            }
        }
        else if (Context.CombatState == CombatState.SlidingBack && Context.CombatTurn == CombatTurn.Player)
        {
            float slideSpeed = 10f;

            CurrentPlayer.transform.position += new Vector3((SlideTargetPosition.x - CurrentPlayer.transform.position.x) * slideSpeed * Time.deltaTime, 0, 0);

            float reachedDistance = 0f;
            if (Math.Round(Math.Abs(CurrentPlayer.transform.position.x - SlideTargetPosition.x), 4) <= reachedDistance)
            {
                OnSlideComplete();
            }
        }

        // Player performing addition
        //if (CurrentGameContext.CombatState == CombatState.PlayerAttacking && CurrentGameContext.CombatTurn == CombatTurn.Player)
        //{

        //    //TODO: START AWAITING COROUTINE TO ACTUALLY PERFORM ATTACK ANIMATIONS AND ADDITION CHECK
        //    PlayerSlideToPosition(PlayerStartingPosition, () =>
        //    {
        //        Debug.Log("Done sliding back");
        //        CurrentGameContext.CombatState = CombatState.NoState;
        //    });
        //}
    }

    private void PlayerSlideToPosition(Vector3 targetPosition, CombatState direction, Action onSlideComplete)
    {
        SlideTargetPosition = targetPosition;
        OnSlideComplete = onSlideComplete;
        Context.CombatState = direction;
    }

    private IEnumerator PerformAddition(string addition, float waitTime)
    {
        CurrentPlayerAnimator.Play(addition);
        Context.PlayOnEnemyEffek(slash1Effek);
        yield return new WaitForSecondsRealtime(waitTime/2f);
        AdditionText.SetActive(true);
        Context.PlayOnEnemyEffek(slash1Effek);
        yield return new WaitForSecondsRealtime(waitTime / 2f);
        AdditionText.SetActive(false);
        Debug.Log("Done waiting");
        // Addition complete, slide back to start position
        PlayerSlideToPosition(PlayerStartingPosition, CombatState.SlidingBack, () =>
        {
            Debug.Log("Done sliding back");
            ResetCombatState();
        });
    }

    public void PlayerGuard()
    {
        CurrentPlayerAnimator.Play("Guard");
    }

    public void DebugAttack()
    {
        // DEBUGGING
        Context.CombatTurn = CombatTurn.Player;
        //
        PlayerStartingPosition = CurrentPlayer.transform.position;
        PlayerSlideToPosition(CurrentEnemy.transform.position, CombatState.SlidingForward, () =>
        {
            Context.CombatState = CombatState.PlayerAttacking;

            Debug.Log("Slide complete");
            //TODO: Full player Attack system

            // DEBUGGING
            StartCoroutine(PerformAddition("DoubleSlash", 2f));

        });
    }

    public void ResetCombatState()
    {
        Context.CombatState = CombatState.NoState;
        Context.AdditionState = AdditionState.NoState;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameContext.GameContext;

public class AdditionController : MonoBehaviour
{
    public CombatController CombatController;

    GameObject AdditionSquare;
    GameObject AdditionInnerSquare;
    Animator AdditionAnimator;
    Vector3 AdditionScale;
    public float SkillCheckDifficultyScale;
    public static bool InAdditionWindow = false;
    public static bool InAdditionSuccessWindow = false;
    public float AdditionTimeLength = 0.5f;
    public float AdditionSuccessWindowLength = 0.7f;

    public bool SetupAdditionController = false;

    Vector3 ADDITION_SCALE_CONSTANT = new Vector3(2, 2, 0);

    // Vars for addition square shrinkage and skill window period
    public float scaleSpeed = -0.9f;
    float additionWindowSeconds;

    void Start()
    {
        CombatController = GetComponent<CombatController>();
        AdditionSquare = GameObject.Find("OuterSquare");
        AdditionAnimator = AdditionSquare.GetComponent<Animator>();
        AdditionScale = ADDITION_SCALE_CONSTANT;
        scaleSpeed = -1.5f;

        additionWindowSeconds = scaleSpeed * 7.5f; // TODO: build logic to multiply by SkillCheckDifficultyScale

        AdditionInnerSquare = GameObject.Find("InnerAdditionSquare");

       


    }

    public void FixedUpdate()
    {
        if (SetupAdditionController == false)
        {
            AdditionInnerSquare.SetActive(false);
            SetupAdditionController = true;
        }
        //if (CurrentGameContext.CurrentCombatState == CombatState.InAddition && CurrentGameContext.CurrentAdditionState == AdditionState.NoState)
        //{
        //    Debug.Log("In addition controller addition start trigger");
        //    AdditionAnimator.SetBool("InAdditionCheck", true);
        //    CurrentGameContext.CurrentAdditionState = AdditionState.AdditionStart;
        //}
        if (AdditionScale.x >= 0.2f && Context.AdditionState != AdditionState.NoState &&
        Context.CombatState != CombatState.NoState)
        {
            AdditionSquare.transform.localScale = (AdditionScale);
            AdditionScale = new Vector3(Mathf.Lerp(AdditionScale.x, AdditionScale.x + scaleSpeed, Time.deltaTime), Mathf.Lerp(AdditionScale.y, AdditionScale.y + scaleSpeed, Time.deltaTime), 0);
        }
    }

    void Update()
    {
        if (InAdditionSuccessWindow == true)
        {
            // Addition succeeded
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Addition Success!");
                CombatController.DebugAttack();
                InAdditionSuccessWindow = false;
            }
        }
    }

    public void AdditionStart()
    {
        AdditionScale = ADDITION_SCALE_CONSTANT;
        AdditionSquare.transform.localScale = new Vector3(1, 1);

        StartCoroutine(StartAdditionRoutine(true, AdditionTimeLength, AdditionSuccessWindowLength));
        AdditionInnerSquare.SetActive(true);

        //Debug.Log("In addition controller addition start trigger");
        //AdditionAnimator.SetBool("InAdditionCheck", true);
        //CurrentGameContext.CurrentAdditionState = AdditionState.AdditionStart;   
    }
    public void AdditionEnd()
    {
        AdditionInnerSquare.SetActive(false);
        AdditionAnimator.SetBool("InAdditionCheck", false);
        AdditionScale = ADDITION_SCALE_CONSTANT;
        AdditionAnimator.SetBool("InAdditionCheck", false);
        AdditionSquare.transform.localScale = new Vector3(1, 1);
        Context.AdditionState = AdditionState.AdditionEnd;
    }

    public void AdditionCheckWindowBegin()
    {
        StartCoroutine(SetAdditionCheckFlagRoutine(true, 0.6f));
    }

    public void AdditionCheckWindowEnd()
    {
        StartCoroutine(SetAdditionCheckFlagRoutine(false, 0));
    }
    private IEnumerator SetAdditionSuccessRoutine(float waitTime)
    {
        InAdditionSuccessWindow = true;
        Debug.Log("!!!In Addition Success Flag!!!");

        yield return new WaitForSecondsRealtime(waitTime);

        InAdditionSuccessWindow = false;

    }

    private IEnumerator SetAdditionCheckFlagRoutine(bool flag, float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Debug.Log("In Addition Flag: " + flag);
        InAdditionWindow = flag;
    }

    private IEnumerator StartAdditionRoutine(bool flag, float waitTime, float successWindowTime)
    {
        Debug.Log("In UpdateInAdditionFlag");
        InAdditionWindow = flag;

        if (flag == true)
        {
            AdditionAnimator.SetBool("InAdditionCheck", true);
            Context.AdditionState = AdditionState.AdditionStart;
            yield return new WaitForSeconds(waitTime);
            Debug.Log("Addition Success Window Start");
            InAdditionSuccessWindow = true;
            yield return new WaitForSecondsRealtime(successWindowTime);
            if (InAdditionSuccessWindow == false)
            {

            }
            else
            {
                InAdditionSuccessWindow = false;
                Debug.Log("Addition Success Window End");
                yield return new WaitForSecondsRealtime(0f);
                //Context.CombatState = CombatState.NoState;
                Context.AdditionState = AdditionState.NoState;
            }
            

            AdditionEnd();
        }
    }
}

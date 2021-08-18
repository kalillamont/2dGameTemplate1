using UnityEngine;
using TMPro;
using static GameContext.GameContext;

public class CombatUiController : MonoBehaviour
{
    Animator attackBtn;
    Animator guardBtn;
    Animator itemBtn;
    Animator escapeBtn;

    GameObject attackText;
    GameObject guardText;
    GameObject itemText;
    GameObject escapeText;


    public AdditionController additionController;
    public CombatController combatController;

    string effectName = "animationpack_elemental/ELM_Blizzard01_A";
    string guardEffek = "animationpack_support/SUP_Healing04_A";

    // Start is called before the first frame update
    void Start()
    {
        //DEBUG OPTION
        Context.GameState = GameState.InCombat;
        Context.CombatMenu = CombatMenu.Attack;

        additionController = GetComponent<AdditionController>();
        combatController = GetComponent<CombatController>();

        attackBtn = GameObject.Find("AttackBtn").GetComponent<Animator>();
        guardBtn = GameObject.Find("GuardBtn").GetComponent<Animator>();
        itemBtn = GameObject.Find("UseItemBtn").GetComponent<Animator>();
        escapeBtn = GameObject.Find("EscapeBtn").GetComponent<Animator>();
        attackBtn.SetBool("selected", true);
        guardBtn.SetBool("selected", false);
        itemBtn.SetBool("selected", false);
        escapeBtn.SetBool("selected", false);
        attackText = GameObject.Find("attackText");
        guardText = GameObject.Find("guardText");
        itemText = GameObject.Find("itemText");
        escapeText = GameObject.Find("escapeText");
        attackText.SetActive(true);
        guardText.SetActive(false);
        itemText.SetActive(false);
        escapeText.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Context.GameState == GameState.InCombat &&  Context.CombatState == CombatState.NoState)
        {
            float x = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.D))
            {
                switch (Context.CombatMenu)
                {
                    case CombatMenu.Attack:
                        Context.CombatMenu = CombatMenu.Guard;
                        attackBtn.SetBool("selected", false);
                        guardBtn.SetBool("selected", true);
                        itemBtn.SetBool("selected", false);
                        escapeBtn.SetBool("selected", false);
                        attackText.SetActive(false);
                        guardText.SetActive(true);
                        itemText.SetActive(false);
                        escapeText.SetActive(false);
                        break;

                    case CombatMenu.Guard:
                        Context.CombatMenu = CombatMenu.Item;
                        attackBtn.SetBool("selected", false);
                        guardBtn.SetBool("selected", false);
                        itemBtn.SetBool("selected", true);
                        escapeBtn.SetBool("selected", false);
                        attackText.SetActive(false);
                        guardText.SetActive(false);
                        itemText.SetActive(true);
                        escapeText.SetActive(false);
                        break;

                    case CombatMenu.Item:
                        Context.CombatMenu = CombatMenu.Escape;
                        attackBtn.SetBool("selected", false);
                        guardBtn.SetBool("selected", false);
                        itemBtn.SetBool("selected", false);
                        escapeBtn.SetBool("selected", true);
                        attackText.SetActive(false);
                        guardText.SetActive(false);
                        itemText.SetActive(false);
                        escapeText.SetActive(true);
                        break;

                    case CombatMenu.Escape:
                        Context.CombatMenu = CombatMenu.Attack;
                        attackBtn.SetBool("selected", true);
                        guardBtn.SetBool("selected", false);
                        itemBtn.SetBool("selected", false);
                        escapeBtn.SetBool("selected", false);
                        attackText.SetActive(true);
                        guardText.SetActive(false);
                        itemText.SetActive(false);
                        escapeText.SetActive(false);
                        break;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                switch (Context.CombatMenu)
                {
                    case CombatMenu.Attack:
                        Context.CombatMenu = CombatMenu.Escape;
                        attackBtn.SetBool("selected", false);
                        guardBtn.SetBool("selected", false);
                        itemBtn.SetBool("selected", false);
                        escapeBtn.SetBool("selected", true);
                        attackText.SetActive(false);
                        guardText.SetActive(false);
                        itemText.SetActive(false);
                        escapeText.SetActive(true);
                        break;

                    case CombatMenu.Guard:
                        Context.CombatMenu = CombatMenu.Attack;
                        attackBtn.SetBool("selected", true);
                        guardBtn.SetBool("selected", false);
                        itemBtn.SetBool("selected", false);
                        escapeBtn.SetBool("selected", false);
                        attackText.SetActive(true);
                        guardText.SetActive(false);
                        itemText.SetActive(false);
                        escapeText.SetActive(false);
                        break;

                    case CombatMenu.Item:
                        Context.CombatMenu = CombatMenu.Guard;
                        attackBtn.SetBool("selected", false);
                        guardBtn.SetBool("selected", true);
                        itemBtn.SetBool("selected", false);
                        escapeBtn.SetBool("selected", false);
                        attackText.SetActive(false);
                        guardText.SetActive(true);
                        itemText.SetActive(false);
                        escapeText.SetActive(false);
                        break;

                    case CombatMenu.Escape:
                        Context.CombatMenu = CombatMenu.Item;
                        attackBtn.SetBool("selected", false);
                        guardBtn.SetBool("selected", false);
                        itemBtn.SetBool("selected", true);
                        escapeBtn.SetBool("selected", false);
                        attackText.SetActive(false);
                        guardText.SetActive(false);
                        itemText.SetActive(true);
                        escapeText.SetActive(false);
                        break;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(Context.CombatMenu);
                switch (Context.CombatMenu)
                {
                    case CombatMenu.Attack:
                        Debug.Log("Attack button clicked");
                        Context.CombatMenu = CombatMenu.Attack;
                        Context.CombatState = CombatState.InAddition;
                        additionController.AdditionStart();
                        break;

                    case CombatMenu.Guard:
                        Debug.Log("Guard button clicked");
                        Context.CombatMenu = CombatMenu.Guard;
                        Context.PlayOnPlayerEffek(guardEffek);
                        combatController.PlayerGuard();
                        break;

                    case CombatMenu.Item:
                        break;

                    case CombatMenu.Escape:
                        break;
                }
            }
        }
    }
}

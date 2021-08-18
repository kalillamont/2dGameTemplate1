using Effekseer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace GameContext
{
    public class GameContext : ScriptableObject
    {
        GameContext()
        {

        }

        public enum GameState
        {
            NoState = 0,
            InCombat = 1,
            InDialogue = 2,
            InMenu = 3,
            PlayerExplore = 4
        }

        public enum CombatState
        {
            NoState = 0,
            PlayerTurn = 1,
            EnemyTurn = 2,
            EnemyAttacking = 3,
            PlayerAttacking = 4,
            InAddition = 5,
            SlidingForward = 6,
            SlidingBack = 7,
            PlayerAttackDone = 8,
            EnemyAttackDone = 9
        }

        public enum CombatTurn
        {
            None = 0,
            Player = 1,
            Enemy = 2
        }

        public enum CombatMenu
        {
            None = 0,
            Attack = 1,
            Guard = 2,
            Item = 3,
            Escape = 4
        }

        public enum AdditionState
        {
            NoState = 0,
            AdditionStart = 1,
            SuccessWindowStart = 2,
            SuccessWindowEnd = 3,
            AdditionEnd = 4
        }

        public enum StatusEffect
        {
            None = 0,
            Poison = 1,
            Stun = 2,
            Fear = 3,
            Confusion = 4,
            Disable = 5,
            Bewitchment = 6,
            Despirit = 7,
            Petrification = 8,
            KO = 9
        }

        public class PlayerData
        {
            public string Name { get; set; }
            public int MaxHp { get; set; }
            public int CurrentHp { get; set; }
            public int MaxMp { get; set; }
            public int CurrentMp { get; set; }
            public bool InDragoonForm { get; set; }
            public StatusEffect CurrentStatus { get; set; }
            public bool InParty { get; set; }
            public bool IsActiveTurn { get; set; }
        }

        public class EnemyData
        {
            public int MaxHp { get; set; }
            public int CurrentHp { get; set; }
            public int MaxMp { get; set; }
            public int CurrentMp { get; set; }
            public StatusEffect CurrentStatus { get; set; }
            public bool IsCurrentTarget { get; set; }
            public bool IsActiveTurn { get; set; }

        }

        // Game Context
        public static class Context
        {
            public static string StateName { get; set; }

            public static bool PlayerCanMove { get; set; }

            public static Dictionary<int, PlayerData> Players { get; set; }

            public static Dictionary<int, EnemyData> Enemies { get; set; }

            public static GameState GameState { get; set; }

            public static CombatState CombatState { get; set; }

            public static CombatMenu CombatMenu { get; set; }

            public static AdditionState AdditionState { get; set; }

            public static CombatTurn CombatTurn { get; set; }

            //public static Func<Task> PlayEffekCallback { get; set; }

            public static void PlayAnimationPackEffek(string effek, Vector3 targetPosition, Quaternion? rotation, float scale = 1)
            {
                Debug.Log("Playing effek: " + effek);
                // Play hit effect
                //PlayerEffek.transform.position = CurrentEnemy.transform.position;
                EffekseerEffectAsset effect = Resources.Load<EffekseerEffectAsset>("Effeks/Effeks/" + effek);
                effect.Scale = scale;
                EffekseerHandle handle = EffekseerSystem.PlayEffect(effect, (targetPosition));
                if (rotation != null && rotation.Value != null)
                {
                    handle.SetRotation(rotation.Value);
                }
                handle.SetScale(new Vector3(scale, scale));
            }

            public static void PlayOnPlayerEffek(string effekName)
            {

                Debug.Log("Playing effek");
                //PlayerData currentPlayerData = Players.FirstOrDefault(x => x.Value.IsActiveTurn == true)?.Value;

                // DEBUGGING
                string currentPlayerName = "Dart"; //currentPlayerData.Name;
                if (!string.IsNullOrWhiteSpace(currentPlayerName))
                {
                    GameObject CurrentPlayer = GameObject.Find(currentPlayerName);
                    var debugTargetPosition = new Vector3(CurrentPlayer.transform.position.x, CurrentPlayer.transform.position.y - 1.6f, CurrentPlayer.transform.position.z); //DEBUGGING
                    PlayAnimationPackEffek(effekName, debugTargetPosition/*CurrentPlayer.transform.position*/, null, 0.4f);
                } 
            }

            public static void PlayOnEnemyEffek(string effekName)
            {

                Debug.Log("Playing enemy effek");
                //PlayerData currentPlayerData = Players.FirstOrDefault(x => x.Value.IsActiveTurn == true)?.Value;

                // DEBUGGING
                string currentEnemyName = "Feyrbrand"; //currentPlayerData.Name;
                if (!string.IsNullOrWhiteSpace(currentEnemyName))
                {
                    GameObject CurrentEnemy = GameObject.Find(currentEnemyName);
                    var debugTargetPosition = new Vector3(CurrentEnemy.transform.position.x, CurrentEnemy.transform.position.y - 1.6f, CurrentEnemy.transform.position.z); //DEBUGGING
                    PlayAnimationPackEffek(effekName, debugTargetPosition/*CurrentPlayer.transform.position*/, null, 1f);
                }
            }
        }

        void Start()
        {
            // For test we go straight into combat
            Context.GameState = GameState.InCombat;

            //CurrentGameContext.CurrentGameState = GameState.NoState;
            Context.CombatMenu = CombatMenu.None;
            Context.AdditionState = AdditionState.NoState;
            Context.CombatState = CombatState.NoState;

            // DEBUGGING    
            Context.Players.Add(0, new PlayerData { Name = "Dart", MaxHp = 100, MaxMp = 100, CurrentHp = 100, CurrentMp = 100, CurrentStatus = StatusEffect.None, InDragoonForm = false, InParty = true, IsActiveTurn = true });
        }

        void Update()
        {

        }

        
    }

}

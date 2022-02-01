using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAi
{
    public Player Player { get; private set; }
    private int Delay { get; set; }

    private List<BuildBaseLogic> BuildingLogics = new List<BuildBaseLogic>();

    public PlayerAi(Player player)
    {
        Player = player;
        switch (GameManager.Instance.Difficulty)
        {
            case (Difficulty.EASY):
                {
                    Delay = 3;
                    break;
                }
            case (Difficulty.NORMAL):
                {
                    Delay = 2;
                    break;
                }
            case (Difficulty.HARD):
                {
                    Delay = 1;
                    break;
                }
            default:
                {
                    Delay = 2;
                    break;
                }
        }
        BuildingLogics.Add(new GatheringHallLogic(player));
    }

    public IEnumerator Process()
    {
        while (AiManager.Instance.enabled &&
            GameManager.Instance.GameState == GameState.PLAYING)
        {
            foreach (BuildBaseLogic logic in BuildingLogics)
            {
                logic.FindSpotsToBuild();
                if (logic.ConditionsMet())
                    logic.Build();
                logic.YELLINFO();
            }

            yield return new WaitForSeconds(Delay);
        }
    }

}


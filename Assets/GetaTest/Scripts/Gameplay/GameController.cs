using UnityEngine;

public class GameController : MonoBehaviour
{
   public int maxLaps;
   private int maxRacers = 6;
   private int maxIas;
   private int maxPlayers;
   private bool lose;
   
   public Transform[] spawnPositions;
   
   private bool isPlay = false;
   public GameObject player;
   public GameObject[] ia;

   public delegate void raceRules(int maxLaps);
   public event raceRules onRaceRules;

   public delegate void winGame(bool isWIn);
   public event  winGame onWinGame;

   private void Start()
   {
      FindObjectOfType<UIManager>().onLaunchGame += OnGameStart;
      FindObjectOfType<UIManager>().onTimeEnd += OnLooseGame;
   }

   public void OnGameStart()
   {
      onRaceRules(maxLaps);
      player.GetComponent<WheelSpring>().isPlay = true;
   }

   public void OnLooseGame()
   {
      player.GetComponent<WheelSpring>().isPlay = false;
      lose = true;
   }

   public void OnWinGame()
   {
      if (lose) {return;}

      onWinGame(true);
      player.GetComponent<WheelSpring>().isPlay = false;
   }

   public void CreateRacers()
   {
      maxIas = maxRacers - maxPlayers;
      for (int i = 0; i < maxPlayers; i++)
      {
         if (i < maxIas)
         {
            GameObject go = Instantiate(ia[Random.Range(0, ia.Length)]);
            go.transform.SetPositionAndRotation(spawnPositions[i].position, spawnPositions[i].rotation);
            //personalizar apariencia ia
            //Personalizar dificultad ia
         }
         else
         {
            //CrearJugador
         }
      }
   }
}

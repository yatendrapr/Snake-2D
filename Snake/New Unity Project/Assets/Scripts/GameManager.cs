using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Fields

    //Grid Variables
    [SerializeField] private float xGridStart = 0;
    [SerializeField] private float yGridStart = 0;
    [SerializeField] private int xGridSize = 0;
    [SerializeField] private int yGridSize = 0;

    //Food Spawner Refrence
    private FoodSpawner foodSpawner = null;

    //Snake Movement Controller Refrence
    private SnakeMovementController snakeMovementController = null;

    #endregion

    #region Constructor

    private void Awake()
    {
        foodSpawner = new FoodSpawner(this.xGridStart, this.yGridStart,xGridSize,yGridSize);
    }

    #endregion

    #region Methods

    private void Start()
    {
        snakeMovementController = RefrenceManager.getSingleton.snakeMovementControllerRefrence;
        snakeMovementController.Set_Food_Spawn_Manager(this.foodSpawner);
    }

    private void Update()
    {
        Check_For_Exit();
    }

    private static void Check_For_Exit()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    #endregion

}

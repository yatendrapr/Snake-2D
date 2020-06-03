using UnityEngine;
using System.Collections.Generic;

public class SnakeMovementController : MonoBehaviour
{

    #region Fields

    //Snake Position List
    private List<Vector3> snakePositionList = new List<Vector3>(1);

    //Direction Variables
    private readonly int leftDirection = 180;
    private readonly int rightDirection = 0;
    private readonly int upDirection = 90;
    private readonly int downDirection = -90;

    //Movement Direction Variable
    private MovementDirection lastMovementDirection = MovementDirection.None;

    //Movement Variables
    [SerializeField] private float snakeSpeed = 5;
    private bool nextMoveAlreadySelected = false;
    private Vector3 nextMovePosition;
    private Vector3 nextMoveDirection;
    private Vector3 currentPosition;
    private Vector3 startingPostition = Vector3.zero;

    //Screen Wrap Point
    [SerializeField] private float screenWrapYPosition = 0f;

    //Food Spawn Manager Refrence
    private FoodSpawner foodSpawner = null;

    //Snake Body Parts Variables
    [SerializeField] private int totalBodyParts = 7;

    //Snake Body Controller Refrence 
    private SnakeBodyController snakeBodyController = null;

    //Snake Body Variable
    private int currentBodyCount = 0;
    private GameObject snakeBody = null;

    //Time Variables
    private float currentTime = 0f;
    [SerializeField] private float timeToWait = 1f;

    //Boundary Variables
    [SerializeField] private float xBoundary = 0f;
    [SerializeField] private int screenLength = 30;

    #endregion

    #region Constructor

    private void Awake()
    {
        RefrenceManager.getSingleton.snakeMovementControllerRefrence = this;
        snakeBodyController = new SnakeBodyController(this.totalBodyParts);
        startingPostition = this.transform.position;
    }

    #endregion

    #region Methods

    private void Update()
    {
        Calculate_Movement_And_Direction();
        Move();
    }

    private void Calculate_Movement_And_Direction()
    {
        if (Input.GetKeyDown(KeyCode.A) && lastMovementDirection != MovementDirection.Right && !nextMoveAlreadySelected)
        {
            nextMoveAlreadySelected = true;
            nextMovePosition = new Vector3(-snakeSpeed, 0f, 0f);
            nextMoveDirection = new Vector3(0f, 0f, leftDirection + 90);
            lastMovementDirection = MovementDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && lastMovementDirection != MovementDirection.Left && !nextMoveAlreadySelected )
        {
            nextMoveAlreadySelected = true;
            nextMovePosition = new Vector3(snakeSpeed, 0f, 0f);
            nextMoveDirection = new Vector3(0f, 0f, rightDirection + 90);
            lastMovementDirection = MovementDirection.Right;
        }
        else if (Input.GetKeyDown(KeyCode.W) && lastMovementDirection != MovementDirection.Down && !nextMoveAlreadySelected)
        {
            nextMoveAlreadySelected = true;
            nextMovePosition = new Vector3(0f, snakeSpeed, 0f);
            nextMoveDirection = new Vector3(0f, 0f, upDirection + 90);
            lastMovementDirection = MovementDirection.Up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && lastMovementDirection != MovementDirection.Up && !nextMoveAlreadySelected)
        {
            nextMoveAlreadySelected = true;
            nextMovePosition = new Vector3(0f, -snakeSpeed,0f);
            nextMoveDirection = new Vector3(0f, 0f, downDirection + 90);
            lastMovementDirection = MovementDirection.Down;
        }


    }

    private void Move()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToWait)
        {
            currentTime = 0f;
            currentPosition = this.transform.position;
            snakePositionList.Insert(0, currentPosition);
            this.transform.position += nextMovePosition;
            Screen_Wrap();
            Check_For_Death();
            Check_For_Boundary();
            nextMoveAlreadySelected = false;
            Occupy_Space();
            this.transform.eulerAngles = nextMoveDirection;
            Check_For_Body_Size();
            Add_Body_Parts();
            Check_Whether_Food_Eaten();
        }
    }

    private void Occupy_Space()
    {
        foodSpawner.Occupy_Space_In_Position_State_List(this.transform.position);
    }

    private void Check_Whether_Food_Eaten()
    {
        if(foodSpawner.Check_For_Food_Eaten(this.transform.position))
        {
            currentBodyCount++;
        }
        else
        {
            return;
        }
        
    }

    private void Check_For_Body_Size()
    {
        if(snakePositionList.Count > currentBodyCount)
        {
            snakePositionList.RemoveAt(snakePositionList.Count - 1);
        }
    }

    private void Add_Body_Parts()
    {
        for (int i = 0; i < snakePositionList.Count; i++)
        {
            snakeBody = snakeBodyController.Return_Body_Part(i);
            snakeBody.transform.position = snakePositionList[i];
        }
    }

    public void Set_Food_Spawn_Manager(FoodSpawner _foodSpawner)
    {
        this.foodSpawner = _foodSpawner;
    }

    private void Check_For_Death()
    {
        for (int i = 0; i < snakePositionList.Count; i++)
        {
            if(snakePositionList[i] == this.transform.position)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        this.transform.position = startingPostition;
        currentBodyCount = 0;
        snakePositionList.Clear();
        snakeBodyController.Disable_Body_Parts();
    }

    private void Check_For_Boundary()
    {
        if(Mathf.Abs(this.transform.position.x) >= xBoundary)
        {
            Death();
        }
    }

    private void Screen_Wrap()
    {
        if(Mathf.Abs(this.transform.position.y) >= screenWrapYPosition)
        {
            if(this.transform.position.y > 0)
            {
                this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - (screenLength + 1)), 0f);
            }
            else if(this.transform.position.y < 0)
            {
                this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + (screenLength + 1)), 0f);
            }
        }
    }

    #endregion

    #region Movement Enum

    private enum MovementDirection
    {
        Left,
        Right,
        Up,
        Down,
        None
    }

    #endregion

}

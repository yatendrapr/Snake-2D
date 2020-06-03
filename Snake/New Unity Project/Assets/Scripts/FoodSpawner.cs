using UnityEngine;

public class FoodSpawner
{

    #region Fields

    //Grid Variables
    private readonly float xGridStartPos;
    private readonly float yGridStartPos;
    private readonly int xGridSize;
    private readonly int yGridSize;

    //Food Sprite
    private Sprite foodSprite = null;

    //Game Variables
    private GameObject snakeFood = null;
    private PositionState[] positionStateList;

    //Sorting Layer Variables
    private string sortingLayerName = "Game World";
    private int sortingLayerOrder = 0;


    #endregion

    #region Constructor

    public FoodSpawner(float _xGridStartPos,float _yGridStartPos,int _xGridSize,int _yGridSize)
    { 
        foodSprite = RefrenceManager.getSingleton.assetManagerRefrence.snakeFood;
        this.xGridStartPos = _xGridStartPos;
        this.yGridStartPos = _yGridStartPos;
        this.xGridSize = _xGridSize;
        this.yGridSize = _yGridSize;
        positionStateList = new PositionState[xGridSize * yGridSize];
        Create_Position_Grid();
        Spawn_Food();
    }

    #endregion

    #region Methods

    private void Create_Position_Grid()
    {
        int iterator = 0;
        float xGridStart = this.xGridStartPos;
        float yGridStart = this.yGridStartPos;
        for (int i = 0; i < (xGridSize * yGridSize); i++)
        {
            iterator++;
            if (iterator % xGridSize != 0)
            {
                positionStateList[i] = new PositionState(new Vector3(xGridStart, yGridStart, 0f));
                xGridStart += 1f;
            }
            else
            {
                positionStateList[i] = new PositionState(new Vector3(xGridStart, yGridStart, 0f));
                yGridStart -= 1f;
                xGridStart = this.xGridStartPos;
            }
        }
    }

    private void Spawn_Food()
    {
        snakeFood = new GameObject("Food", typeof(SpriteRenderer));
        snakeFood.GetComponent<SpriteRenderer>().sprite = foodSprite;
        snakeFood.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
        snakeFood.GetComponent<SpriteRenderer>().sortingOrder = sortingLayerOrder;
        snakeFood.transform.position = Get_Grid_Position();
        snakeFood.SetActive(true);
    }

    private Vector3 Get_Grid_Position()
    {
        int index = Random.Range(0, positionStateList.Length);
        if (positionStateList[index].occupiedStatus == false)
        {
            return positionStateList[index].Return_Position();
        }
        else
        {
            while (positionStateList[index].occupiedStatus != false)
            {
                index = Random.Range(0, positionStateList.Length);
            }
            return positionStateList[index].Return_Position();
        }
    }

    private void Reactivate_Food()
    {
        snakeFood.transform.position = Get_Grid_Position();
        snakeFood.SetActive(true);
    }

    public bool Check_For_Food_Eaten(Vector3 _snakePosition)
    {
        if(_snakePosition == snakeFood.transform.position)
        {
            snakeFood.SetActive(false);
            Reactivate_Food();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Occupy_Space_In_Position_State_List(Vector3 _headPosition)
    {
        for (int i = 0; i < positionStateList.Length; i++)
        {
            if(positionStateList[i].Return_Position() == _headPosition)
            {
                positionStateList[i].occupiedStatus = true;
            }
        }
    }

    #endregion

}

using UnityEngine;
using System.Collections.Generic;

public class SnakeBodyController 
{
    #region Fields

    //Game Variables
    private int totalSnakeBodyParts = 0;

    //Body Parts Variables
    private GameObject bodyPartsParent = new GameObject("Body Parts");
    private List<GameObject> bodyPartsList = null;

    //Asset Manager Refrences
    private Sprite snakeBody = null;

    //Sorting Order Variables
    private readonly string sortingLayerName = "Game World";
    private readonly int sortingOrder = 1;


    #endregion

    #region Constructor

    public SnakeBodyController(int _totalBodyParts)
    {
        this.totalSnakeBodyParts = _totalBodyParts;
        snakeBody = RefrenceManager.getSingleton.assetManagerRefrence.snakeBody;
        bodyPartsList = new List<GameObject>(totalSnakeBodyParts);
        Create_Body_Parts_Pool();
    }

    #endregion

    #region Methods

    private void Create_Body_Parts_Pool()
    {
        for (int i = 0; i < totalSnakeBodyParts; i++)
        {
            GameObject body = new GameObject($"Body {i}", typeof(SpriteRenderer));
            body.GetComponent<SpriteRenderer>().sprite = snakeBody;
            body.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
            body.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            body.SetActive(false);
            bodyPartsList.Add(body);
            body.transform.SetParent(bodyPartsParent.transform);
        }
    }

    public GameObject Return_Body_Part(int _index)
    {
        GameObject part = bodyPartsList[_index];
        part.SetActive(true);
        return part; 
    }

    public void Disable_Body_Parts()
    {
        foreach (GameObject item in bodyPartsList)
        {
            item.SetActive(false);
        }
    }

    #endregion
}

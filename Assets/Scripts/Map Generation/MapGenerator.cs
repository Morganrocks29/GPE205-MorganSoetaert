using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth;
    public float roomHeight;
    private Room[,] grid;
    public int mapSeed;
    public bool isMapOfTheDay;
    public int DateToInt(DateTime dateToUse)
    {
        // Add our date up and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();

        if (isMapOfTheDay == true)
        {
            mapSeed = DateToInt(DateTime.Now.Date);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateMap()
    {
        // Set our seed
        UnityEngine.Random.seed = mapSeed;

        grid = new Room[rows, cols];

        //For each grid row
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            //For each grid column
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                //Figure out the location
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0, zPosition);

                //Create a new room at the target location 
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                //Set its parent
                tempRoomObj.transform.parent = this.transform;

                //Give it a meaningful name
                tempRoomObj.name = "Room_" + currentCol + ", " + currentRow;

                //Get the Room component
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                //Save it to the grid array
                grid[currentCol, currentRow] = tempRoom;

                //Open the doors
                //If we are on the bottom row, open the north door
                if (currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentRow == rows - 1)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }
            }
        }
    }
}

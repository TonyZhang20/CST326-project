using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        //Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        // Go through the rows from bottom to top
        int row = 0;
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            int column = 0;
            char[] letters = currentLine.ToCharArray();
            foreach (var letter in letters)
            {
                var stoneObject = Instantiate(stonePrefab);

                var brickObject = Instantiate(brickPrefab);

                var rockObject = Instantiate(rockPrefab);

                var questionBoxObject = Instantiate(questionBoxPrefab);

                if (letter == 'x')
                {
                    rockObject.transform.position = new Vector3(column + 0.5f,row + 0.5f,0);
                }

                if(letter == 'b')
                {
                    brickObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0);
                }

                if(letter == 's')
                {
                    stoneObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0);
                }

                if(letter == '?')
                {
                    questionBoxObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0);
                }
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
                column++;
            }
            row++;
        }
    }

    // --------------------------------------------------------------------------
    public void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class BoxPuzzle : MonoBehaviour, Completion
{

    public Material greenMaterial;
    public Material redMaterial;
    // Start is called before the first frame update
    public float sizeOfCube = 0.5f;

    public Vector3 startingSolutionSize;

    public Vector3 maxSolutionSize;

    public int numberOfPuzzles;

    public float cubeSpacing = 1.5f;

    private bool[,,] solution;

    private DropSlot[,,] slots;

    public GameObject psyche;

    private int puzzlesCompleted;
    private Vector3 solutionStepSize;
    private Vector3 currentSolutionSize;

    void Start()
    {
        if (maxSolutionSize == null) maxSolutionSize = startingSolutionSize;
        puzzlesCompleted = 0;
        solutionStepSize = (maxSolutionSize + Vector3.one - startingSolutionSize) / numberOfPuzzles;
        Debug.Log("Step Size: " + solutionStepSize);
        currentSolutionSize = startingSolutionSize;
        BuildPuzzle();
    }

    void ClearPuzzleCubes()
    {
        foreach (DropSlot slot in slots)
        {
            if (slot.slotMatch != null) UnityEngine.Object.Destroy(slot.slotMatch);
            UnityEngine.Object.Destroy(slot.gameObject);
        }
    }

    void BuildPuzzle()
    {
        solution = new bool[(int)currentSolutionSize.x, (int)currentSolutionSize.y, (int)currentSolutionSize.z];
        slots = new DropSlot[solution.GetLength(0), solution.GetLength(1), solution.GetLength(2)];
        System.Random rnd = new System.Random();
        for (int x = 0; x < solution.GetLength(0); x++)
        {
            for (int y = 0; y < solution.GetLength(1); y++)
            {
                for (int z = 0; z < solution.GetLength(2); z++)
                {
                    solution[x, y, z] = rnd.Next(0, 2) > 0;
                }
            }
        }
        if (!hasTrue()) solution[0, 0, 0] = true;
        for (int x = 0; x < solution.GetLength(0); x++)
        {
            for (int y = 0; y < solution.GetLength(1); y++)
            {
                for (int z = 0; z < solution.GetLength(2); z++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = Vector3.one * sizeOfCube;
                    cube.transform.localPosition = new Vector3(x * cubeSpacing, y * cubeSpacing, z * cubeSpacing);
                    slots[x, y, z] = cube.AddComponent<DropSlot>();
                    cube.GetComponent<Collider>().isTrigger = true;
                    cube.GetComponent<Renderer>().material = solution[x, y, z] ? greenMaterial : redMaterial;

                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (maxSolutionSize == Vector3.zero) maxSolutionSize = startingSolutionSize;
        // solution = new bool[(int)solutionSize.x, (int)solutionSize.y];
        for (int x = 0; x < (int)maxSolutionSize.x; x++)
        {
            for (int y = 0; y < (int)maxSolutionSize.y; y++)
            {
                for (int z = 0; z < (int)maxSolutionSize.z; z++)
                {
                    // Draw a yellow cube at the transform position
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireCube(transform.position + new Vector3(x * cubeSpacing, y*cubeSpacing, z * cubeSpacing), Vector3.one * sizeOfCube);
                }
            }
        }
    }

    public bool IsCompleted()
    {
        if (solution == null) Debug.Log("solution null");
        for (int x = 0; x < solution.GetLength(0); x++)
        {
            for (int y = 0; y < solution.GetLength(1); y++)
            {
                for (int z = 0; z < solution.GetLength(2); z++)
                {
                    if (solution[x, y, z] != slots[x, y, z].IsCompleted())
                    { //will return false if solution and slot don't match
                        Debug.Log("Box Not complete");
                        return false;
                    }
                }
            }
        }
        Debug.Log("Puzzle Completed");
        return true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCompletion()
    {
        // Debug.Log("Current Puzzle: " + puzzlesCompleted);
        ClearPuzzleCubes(); //deletes the puzzle
        if (++puzzlesCompleted < numberOfPuzzles)
        {
            currentSolutionSize += solutionStepSize; //increments the puzzle size
            BuildPuzzle();  //re-initiates the puzzle
        }
        else
        {
            SceneManager.LoadScene("Hub");
        }
        Debug.Log("Puzzle size: " + solution.GetLength(0) + " "+ solution.GetLength(1) + " "+ solution.GetLength(2) + " ");
        Debug.Log("Puzzles Completed: " + puzzlesCompleted);
    }

    private bool hasTrue()
    {
        foreach (bool b in solution)
        {
            if (b) return true;
        }
        return false;
    }
}

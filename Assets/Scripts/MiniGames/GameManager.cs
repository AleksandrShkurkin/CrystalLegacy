using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public UnityEngine.UIElements.Button[] puzzlePieces;
    private int[] collectOrder = { 0, 1, 2, 3 };
    // Start is called before the first frame update
    void Start() { ShufflePuzzle(); }
    void ShufflePuzzle() { }

    public void OnPieceClicked(int index)
    {
        if (index > 0)
        {
            SwapPieces(index, index - 1);
        }
    }
    public void SwapPieces(int indexA, int indexB) { 

      Button temp  = puzzlePieces[indexA];
      puzzlePieces[indexA] = puzzlePieces[indexB] ; 
      puzzlePieces[indexB] = temp; 
    }

    // Update is called once per frame
    void Update() { }
}

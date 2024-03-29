using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComGameData : MonoBehaviour
{

    public float[] getWinPosition(int position)
    {
        // blank tile win position
        Tuple<float, float> pos00 = new Tuple<float, float>(5.5F, 2.3F);

        Tuple<float, float> pos11 = new Tuple<float, float>(-2.75F, 2.3F);
        Tuple<float, float> pos12 = new Tuple<float, float>(0.0F, 2.3F);
        Tuple<float, float> pos13 = new Tuple<float, float>(2.75F, 2.3F);        
        Tuple<float, float> pos21 = new Tuple<float, float>(-2.75F, 0.4F);
        Tuple<float, float> pos22 = new Tuple<float, float>(0.0F, 0.4F);
        Tuple<float, float> pos23 = new Tuple<float, float>(2.75F, 0.4F);
        Tuple<float, float> pos24 = new Tuple<float, float>(5.5F, 0.4F);
        Tuple<float, float> pos31 = new Tuple<float, float>(-2.75F, -1.5F);
        Tuple<float, float> pos32 = new Tuple<float, float>(0.0F, -1.5F);
        Tuple<float, float> pos33 = new Tuple<float, float>(2.75F, -1.5F);
        Tuple<float, float> pos34 = new Tuple<float, float>(5.5F, -1.5F);

        float[] winPosition = { 0.0F, 0.0F };
        float winPosX = 0.0F;
        float winPosY = 0.0F;

        switch (position)
        {
            // blank tile case
            case 0:
                winPosX = pos00.Item1;
                winPosY = pos00.Item2;
                break;

            case 11:
                winPosX = pos11.Item1;
                winPosY = pos11.Item2;
                break;
            case 12:
                winPosX = pos12.Item1;
                winPosY = pos12.Item2;
                break;
            case 13:
                winPosX = pos13.Item1;
                winPosY = pos13.Item2;
                break;
            case 21:
                winPosX = pos21.Item1;
                winPosY = pos21.Item2;
                break;
            case 22:
                winPosX = pos22.Item1;
                winPosY = pos22.Item2;
                break;
            case 23:
                winPosX = pos23.Item1;
                winPosY = pos23.Item2;
                break;
            case 24:
                winPosX = pos24.Item1;
                winPosY = pos24.Item2;
                break;
            case 31:
                winPosX = pos31.Item1;
                winPosY = pos31.Item2;
                break;
            case 32:
                winPosX = pos32.Item1;
                winPosY = pos32.Item2;
                break;
            case 33:
                winPosX = pos33.Item1;
                winPosY = pos33.Item2;
                break;
            case 34:
                winPosX = pos34.Item1;
                winPosY = pos34.Item2;
                break;
        }

        winPosition[0] = winPosX;
        winPosition[1] = winPosY;


        return winPosition;
    }

    public float[] getStartPosition(int position)
    {
        // blank tile
        Tuple<float, float> pos00 = new Tuple<float, float>(2.75F, -1.5F);

        Tuple<float, float> pos11 = new Tuple<float, float>(0.0F, 0.4F);
        Tuple<float, float> pos12 = new Tuple<float, float>(-2.75F, 2.3F);
        Tuple<float, float> pos13 = new Tuple<float, float>(2.75F, 0.4F);
        Tuple<float, float> pos21 = new Tuple<float, float>(-2.75F, -1.5F);
        Tuple<float, float> pos22 = new Tuple<float, float>(-2.75F, 0.4F);
        Tuple<float, float> pos23 = new Tuple<float, float>(2.75F, 2.3F);
        Tuple<float, float> pos24 = new Tuple<float, float>(5.5F, 2.3F);
        Tuple<float, float> pos31 = new Tuple<float, float>(5.5F, -1.5F);
        Tuple<float, float> pos32 = new Tuple<float, float>(0.0F, -1.5F);
        Tuple<float, float> pos33 = new Tuple<float, float>(0.0F, 2.3F);
        Tuple<float, float> pos34 = new Tuple<float, float>(5.5F, 0.4F);

        float[] winPosition = { 0.0F, 0.0F };
        float winPosX = 0.0F;
        float winPosY = 0.0F;

        switch (position)
        {
            // blank tile case
            case 0:
                winPosX = pos00.Item1;
                winPosY = pos00.Item2;
                break;

            case 11:
                winPosX = pos11.Item1;
                winPosY = pos11.Item2;
                break;
            case 12:
                winPosX = pos12.Item1;
                winPosY = pos12.Item2;
                break;
            case 13:
                winPosX = pos13.Item1;
                winPosY = pos13.Item2;
                break;
            case 21:
                winPosX = pos21.Item1;
                winPosY = pos21.Item2;
                break;
            case 22:
                winPosX = pos22.Item1;
                winPosY = pos22.Item2;
                break;
            case 23:
                winPosX = pos23.Item1;
                winPosY = pos23.Item2;
                break;
            case 24:
                winPosX = pos24.Item1;
                winPosY = pos24.Item2;
                break;
            case 31:
                winPosX = pos31.Item1;
                winPosY = pos31.Item2;
                break;
            case 32:
                winPosX = pos32.Item1;
                winPosY = pos32.Item2;
                break;
            case 33:
                winPosX = pos33.Item1;
                winPosY = pos33.Item2;
                break;
            case 34:
                winPosX = pos34.Item1;
                winPosY = pos34.Item2;
                break;
        }

        winPosition[0] = winPosX;
        winPosition[1] = winPosY;


        return winPosition;
    }


    public string getWinLetter(int row, int letterPos)
    {
        string letter = "";
        string [] row1Word = { "S", "U", "R", "F", "A", "C", "E"};
        string [] row2Word = { "C", "O", "M", "P", "O", "S", "I", "T", "I", "O", "N" };
        string [] row3Word = { "M", "A", "G", "N", "E", "T", "I", "C" };
        string [] row4Word = { "G", "R", "A", "V", "I", "T", "Y" };
        string[] rowFinalWord = { "N", "O", "C", "O", "M", "E", "T" };

        switch (row)
        {
            case 1:
                letter = row1Word[letterPos];
                break;
            case 2:
                letter = row2Word[letterPos];
                break;
            case 3:
                letter = row3Word[letterPos];
                break;
            case 4:
                letter = row4Word[letterPos];
                break;
            case 5:
                letter = rowFinalWord[letterPos];
                break;
        }

        return letter;
    }

    public string [] getSpecialLetters(int row)
    {
        string[] specialLetterButtons = { };
        string[] word1special = { "6" };
        string[] word2special = { "9", "12", "18" };
        string[] word3special = { "19", "23" };
        string[] word4special = { "32" };
        string[] word5special = { "34", "35", "36", "37", "38", "39", "40" };

        switch (row)
        {
            case 1:
                specialLetterButtons = word1special;
                break;
            case 2:
                specialLetterButtons = word2special;
                break;
            case 3:
                specialLetterButtons = word3special;
                break;
            case 4:
                specialLetterButtons = word4special;
                break;
            case 5:
                specialLetterButtons = word5special;
                break;
        }

        return specialLetterButtons;
    }


}
using System.Collections;
using UnityEngine;


public class IsPalindrome : MonoBehaviour
{
    private void Start()
    {
        string inputSingle = "aabbabbaa";
        string inputDouble = "aabbaa";
        string falseinputSingle = "aaaabbbbbaa";
        string falseinputDouble = "aaaabbbbbaab";

        Debug.Log("fist true input with single lenght " + IsPalindromeFunction(inputSingle));
        Debug.Log("second true input with double lenght " + IsPalindromeFunction(inputDouble));
        Debug.Log("frist false input with single lenght " + IsPalindromeFunction(falseinputSingle));
        Debug.Log("frist false input with double lenght " + IsPalindromeFunction(falseinputDouble));

        Debug.Log("third true input with double lenght " + IsPalindromeFunction("aaaabbbbbbbbaaaa"));
    }


    bool IsPalindromeFunction(string letter)
    {
        Stack stack = new Stack();
        int counter = 0;

        if (letter.Length % 2 == 0) //çift
        {
            for (int i = 0; i < (letter.Length / 2); i++)
                stack.Push(letter[i]);

            for (int j = (letter.Length / 2); j < letter.Length; j++)
            {
                if (letter[j] == (char)stack.Pop())
                {
                    counter++;

                    if (counter == letter.Length / 2)
                    {
                        return true;
                    }
                }

            }
        }
        else //tek
        {
            for (int i = 0; i < (letter.Length / 2); i++)
            {
                stack.Push(letter[i]);
            }
            for (int j = (letter.Length / 2) + 1; j < letter.Length; j++)
            {
                if (letter[j] == (char)stack.Pop())
                {
                    counter++;
                    if (counter == (letter.Length / 2))
                    {
                        return true;
                    }
                }

            }
        }
        return false;
    }
}


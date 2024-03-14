using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    // 0 = idle
    // 1 = right
    // 2 = left
    // 3 = jump
    int aiState = 0;
    public float nextStateChange = 2.0f;
    public float stateTimer = 0.0f;
    public override void Update()
    {
        JustAILogic();
    }


    private void JustAILogic()
    {
        stateTimer += Time.deltaTime;
        if (nextStateChange <= stateTimer)
        {
            stateTimer = 0.0f;
            nextStateChange = UnityEngine.Random.Range(0.2f, 1.0f);
            var newState = UnityEngine.Random.Range(0, 4);
            if (aiState == newState)
            { 
                stateTimer = nextStateChange;
                JustAILogic();
                return; 
            }
            aiState = newState;

            if (aiState == 3)
            {
                aiState = (UnityEngine.Random.Range(0, 3));
                if (aiState == 1)
                    MovementLogic(1, true);
                if (aiState == 2)
                    MovementLogic(-1, true);
            }

            if (aiState == 0)
            {
                // (❁´◡`❁) Awesome Unity Google Sheet xD
            }
            if (aiState == 1)
            {
                MovementLogic(1, false);
            }
            if (aiState == 2)
            {
                MovementLogic(-1, false);
            }
        }
    }
}

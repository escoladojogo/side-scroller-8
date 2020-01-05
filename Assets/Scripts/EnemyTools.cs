using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyTools
{
    float proximityLimit;

    public EnemyTools(float limit)
    {
        this.proximityLimit = limit;
    }

    public bool IsPlayerClose(float enemyX)
    {
        GameObject player = GameObject.FindWithTag("Player");

        float diffX = Mathf.Abs(player.transform.position.x - enemyX);

        if (diffX <= proximityLimit)
        {
            return true;
        }

        return false;
    }

    public bool IsPlayerLeft(float enemyX)
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player.transform.position.x < enemyX)
        {
            return true;
        }

        return false;
    }
}

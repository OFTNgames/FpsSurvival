﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
   public void SendDamage (int damage) 
  {
        PlayerHealth playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerStats.PlayerTakeDamage(damage);
        
  } 
}

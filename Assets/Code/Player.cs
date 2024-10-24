using System.Collections.Generic;
using UnityEngine;


class Player : MonoBehaviour
{
      List<Card> inventory;
      int balance;

      void Start()
      {
            inventory = new List<Card>();
            balance = 1000;
      }
}
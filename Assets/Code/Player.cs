using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
      List<Card> inventory;
      int balance;

      void Start()
      {
            inventory = new List<Card>();
            balance = 1000;
      }

      public int GetBalance()
      {
            return balance;
      }
      public int SetBalance(int b)
      {
            return balance = b;
      }
}
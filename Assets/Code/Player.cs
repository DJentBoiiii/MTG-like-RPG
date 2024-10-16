using System.Collections.Generic;


class Player
{
      List<Card> inventory;
      int balance;

      public Player()
      {
            inventory = new List<Card>();
            balance = 0;
      }
}
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Coin> _coins = new List<Coin>();

    public void Take(Coin coin)
    {
        _coins.Add(coin);
    }

    public Coin GetCoin(Coin coin)
    {
        if (_coins.Count > 0)
        {
            _coins.Remove(coin);
        }

        return coin;
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        foreach (Item item in Items)
        {
            //convert name of item to lowercase to see if contains certain word using in condition
            string name = item.Name;
            name = name.ToLower();
            if (item.Quality >= 0 && item.Quality < 50)
            {
                int qualityRemove = item.SellIn < 0 ? 2 : 1;
                switch (name)
                {
                    case var _ when name.Contains("conjured"):
                        item.Quality -= qualityRemove * 2;
                        break;
                    case var _ when name.Contains("aged brie") || name.Contains("backstage passes"):
                        if (!name.Contains("sulfuras"))
                        {
                            AddQuality(name, item);
                        }
                        break;
                    default:
                        item.Quality -= qualityRemove;
                        break;
                }
                item.Quality = Math.Clamp(item.Quality, 0, 50);
            }
            
            DecreaseSellIn(name, item);
        }
    }
    private void AddQuality(string name, Item item)
    {
        int qualityAdd = 1;

        if (name.Contains("backstage passes"))
        {
            if (item.SellIn < 11)
            {
                qualityAdd = 2;
            }
            if (item.SellIn < 6)
            {
                qualityAdd = 3;
            }
        }
        item.Quality += qualityAdd;
    }
    private void DecreaseSellIn(string name, Item item)
    {
         if (!name.Contains("sulfuras"))
            {
                item.SellIn = item.SellIn - 1;

                if (item.SellIn < 0 && name.Contains("backstage passes"))
                {
                    item.Quality = 0;
                }
            }
    }
}
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
        for (var i = 0; i < Items.Count; i++)
        {
             //convert name of item to lowercase to see if contains certain word using in condition
            string name = Items[i].Name;
            name = name.ToLower();

            if (Items[i].Quality > 0 && Items[i].Quality < 50)
            {
                int qualityRemove = Items[i].SellIn < 0 ? 2 : 1;

                if (name.Contains("conjured"))
                {
                    Items[i].Quality = Items[i].Quality - qualityRemove * 2;
                }
                else
                {
                    if (!name.Contains("aged brie") && !name.Contains("backstage passes"))
                    {
                        if (!name.Contains("sulfuras"))
                        {
                            Items[i].Quality = Items[i].Quality - qualityRemove;
                        }
                    }
                    else
                    {
                        AddQuality(name, Items[i]);
                    }
                }
            }
            else if(Items[i].Quality == 0)
            {
                if (name.Contains("aged brie") || name.Contains("backstage passes"))AddQuality(name, Items[i]);                
            }

            Items[i].Quality = Math.Clamp(Items[i].Quality, 0, 50);

            if (!name.Contains("sulfuras"))
            {
                Items[i].SellIn = Items[i].SellIn - 1;

                if (Items[i].SellIn < 0 && (name.Contains("aged brie") || name.Contains("backstage passes")))
                {
                    Items[i].Quality = 0;
                }
            }
        }
    }
    private void AddQuality(string name, Item item)
    {
        if (item.SellIn > 0)
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
    }
}
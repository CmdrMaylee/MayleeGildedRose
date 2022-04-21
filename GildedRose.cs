using System.Collections.Generic;

namespace csharp;

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
            if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                if (Items[i].Quality > 0)
                {
                    if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                    {
                        Items[i].Quality = Items[i].Quality - 1;
                    }
                }
            }
            else
            {
                if (Items[i].Quality < 50)
                {
                    Items[i].Quality = Items[i].Quality + 1;

                    if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (Items[i].SellIn < 11)
                        {
                            if (Items[i].Quality < 50)
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }

                        if (Items[i].SellIn < 6)
                        {
                            if (Items[i].Quality < 50)
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }
                    }
                }
            }

            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            {
                Items[i].SellIn = Items[i].SellIn - 1;
            }

            if (Items[i].SellIn < 0)
            {
                if (Items[i].Name != "Aged Brie")
                {
                    if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (Items[i].Quality > 0)
                        {
                            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                            {
                                Items[i].Quality = Items[i].Quality - 1;
                            }
                        }
                    }
                    else
                    {
                        Items[i].Quality = Items[i].Quality - Items[i].Quality;
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;
                    }
                }
            }
        }
    }

    public void UpdateQualityV2()
    {
        int deductQualityBy = 0;
        int defaultDeductionValue = 1;

        bool defaultDeduction = true;

        foreach (Item i in Items)
        {
            defaultDeduction = true;
            i.SellIn -= 1;
            deductQualityBy = 0;
            if (i.Name == "Sulfuras, Hand of Ragnaros") { continue; }


            defaultDeductionValue = 1;
            if (i.SellIn >= 0)
            {
                if (i.Name.Contains("Backstage passes"))
                {
                    defaultDeduction = false;
                    if (i.SellIn > 10) deductQualityBy -= 1;
                    if (i.SellIn > 5 && i.SellIn <= 10) deductQualityBy -= 2;
                    if (i.SellIn <= 5) deductQualityBy -= 3;
                }
                if (i.Name.StartsWith("Conjured"))
                {
                    defaultDeduction = false;
                    deductQualityBy += defaultDeductionValue * 2;
                }
                if (i.Name == "Aged Brie" && i.Quality != 50)
                {
                    defaultDeduction = false;
                    deductQualityBy -= 1;
                }

                if (defaultDeduction) deductQualityBy += defaultDeductionValue;
            }

            defaultDeductionValue = 2;
            if (i.SellIn < 0)
            {
                if (i.Name.Contains("Backstage passes"))
                {
                    i.Quality = 0;
                    continue;
                }
                if (i.Name.StartsWith("Conjured"))
                {
                    defaultDeduction = false;
                    deductQualityBy += defaultDeductionValue * 2;
                }
                if (i.Name == "Aged Brie" && i.Quality != 50)
                {
                    defaultDeduction = false;
                    deductQualityBy -= 1;
                }

                if (defaultDeduction) deductQualityBy += defaultDeductionValue;
            }

            i.Quality -= deductQualityBy;
            if (i.Name != "Sulfuras, Hand of Ragnaros" && i.Quality > 50) i.Quality = 50;
        }
    }
}

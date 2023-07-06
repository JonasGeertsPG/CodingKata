using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<Item> Items;
        IList<Item> LegenderyItems;
        IList<Item> SmellyItems;

        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;

            //Twee lists toegevoegd, om in de toekomst meerdere items hieraan te kunnen toevoegen
            this.LegenderyItems = new List<Item>
                {
                new Item(){ Name = "B-DAWG Keychain", SellIn = 0, Quality = 0 }
                };
            this.SmellyItems = new List<Item>
            {
                new Item() { Name = "Duplicate Code", SellIn = 0, Quality = 0 },
                new Item() { Name = "Long Methods", SellIn = 0, Quality = 0 },
                new Item() { Name = "Ugly Variable Names", SellIn = 0, Quality = 0 }
            };
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                Item CurrentItem = Items[i];

                #region //Legendary Items//
                bool Legendary = false;
                foreach (var Item in LegenderyItems) 
                {
                    if (CurrentItem.Name == Item.Name) 
                    {
                        CurrentItem.SellIn = 0; //Gezien deze nooit verkocht moest worden blijven deze waarden volgens mij altijd 0 en 80?
                        CurrentItem.Quality = 80;
                        Items[i] = CurrentItem;
                        Legendary = true;
                        continue;
                    }
                }
                if (Legendary == true) continue;
                #endregion

                #region //Smelly Items//
                bool Smelly = false;
                foreach (var Item in SmellyItems)
                {
                    if (CurrentItem.Name == Item.Name)
                    {
                        if (CurrentItem.Quality != 0)
                        {

                            if (CurrentItem.SellIn < 0) CurrentItem.Quality -= 4;
                            else CurrentItem.Quality -= 2;
                            if (CurrentItem.Quality == -1) CurrentItem.Quality = 0;
                            CurrentItem.SellIn -= 1;
                            Items[i] = CurrentItem;
                            Smelly = true;
                            break;
                        }
                        else
                        {
                            CurrentItem.SellIn -= 1;
                            Items[i] = CurrentItem;
                            Smelly = true;
                            break;
                        }
                    }
                }
                if (Smelly == true) continue;
                #endregion

                #region //Good Wine//
                if (CurrentItem.Name == "Good Wine") 
                {
                    if (CurrentItem.Quality != 50) 
                    {
                        CurrentItem.Quality += 1;
                    }
                    CurrentItem.SellIn -= 1;
                    Items[i] = CurrentItem;
                    continue;
                }
                #endregion

                #region //Backstage Passes//
                if (CurrentItem.Name.ToLower().Contains("backstage"))
                {
                    if (CurrentItem.Quality != 0 && CurrentItem.Quality != 50)
                    {
                        if (CurrentItem.SellIn > 5 && CurrentItem.SellIn <= 10) CurrentItem.Quality += 2;
                        else if (CurrentItem.SellIn <= 5 && CurrentItem.SellIn >= 0) CurrentItem.Quality += 3;
                        else CurrentItem.Quality -= 1; //Ik ging er vanuit dat de gewone regels gelden als deze niet zijn SellinCriteria benaderd, mocht deze ook moeten stijgen, dan is het uiteraard += 1;
                    }
                    if (CurrentItem.Quality > 50) CurrentItem.Quality = 50;
                    CurrentItem.SellIn -= 1;
                    if (CurrentItem.SellIn < 0) CurrentItem.Quality = 0; //Ook ga ik ervanuit dat op SellIn date 0 de tickets nog waarde hebben als ze binnen die dag verkocht worden?
                    Items[i] = CurrentItem;
                    continue;
                }
                #endregion

                #region //Other Products//
                if (CurrentItem.Quality != 0) 
                {
                    if (CurrentItem.SellIn < 0) CurrentItem.Quality -= 2;
                    else CurrentItem.Quality -= 1;
                }
                CurrentItem.SellIn -= 1;
                Items[i] = CurrentItem;
                #endregion
            }
        }
    }
}

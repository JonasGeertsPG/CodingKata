using System.Collections.Generic;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest
    {
        /*BL: Quality always 80, never has to be sold so SellIn = 0*/
        [Fact]
        public void LegendaryItem()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "B-DAWG Keychain", SellIn = 0, Quality = 0 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(80, Items[0].Quality);
            Assert.Equal(0, Items[0].SellIn);
        }

        /*BL: Smelly Items decrease faster in Quality
          -2 when SellIn is still positive, -4 when SellIn gets negative*/
        [Fact]
        public void SmellyItem()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Long Methods", SellIn = 3, Quality = 6 }, new Item { Name = "Duplicate Code", SellIn = -1, Quality = 6 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(4, Items[0].Quality);
            Assert.Equal(2, Items[0].SellIn);
            Assert.Equal(2, Items[1].Quality);
            Assert.Equal(-2, Items[1].SellIn);
        }

        /*BL: Good Wine increases in Quality
          +1 when SellIn gets lower*/
        [Fact]
        public void GoodWine()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 3, Quality = 6 }, new Item { Name = "Good Wine", SellIn = -1, Quality = 50 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(7, Items[0].Quality);
            Assert.Equal(2, Items[0].SellIn);
            Assert.Equal(50, Items[1].Quality);
            Assert.Equal(-2, Items[1].SellIn);
        }

        /*BL: Backstage Passes decrease in Quality, but increase when the event is closeby
          -1 when SellIn is < 10, +2 when SellIn <= 10 && > 5, +3 when SellIn <= 5 en >= 0, quality gets to 0 after the event*/
        [Fact]
        public void BackstagePasses()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 38 }, new Item { Name = "Backstage passes for Axxes Interview", SellIn = 4, Quality = 38 }, new Item { Name = "Backstage passes for Moria Afterparty", SellIn = -1, Quality = 38 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(40, Items[0].Quality);
            Assert.Equal(9, Items[0].SellIn);
            Assert.Equal(41, Items[1].Quality);
            Assert.Equal(3, Items[1].SellIn);
            Assert.Equal(0, Items[1].Quality);
            Assert.Equal(-2, Items[1].SellIn);
        }

        /*BL: other Products decrease in Quality
          -1 when SellIn gets lower, -2 when SellIn is negative*/
        [Fact]
        public void OtherProducts()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Coding Kata Handbook", SellIn = 3, Quality = 6 }, new Item { Name = "TROS Exclusive Membership Card", SellIn = -1, Quality = 50 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(5, Items[0].Quality);
            Assert.Equal(2, Items[0].SellIn);
            Assert.Equal(48, Items[1].Quality);
            Assert.Equal(-2, Items[1].SellIn);
        }
    }
}
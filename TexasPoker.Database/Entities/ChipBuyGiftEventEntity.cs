﻿using GameCloud.Database;
using GameCloud.Database.Attributes;

namespace TexasPoker.Database.Entities
{
    [CollectionName("EventChipBuyGift")]
    public class ChipBuyGiftEventEntity : EntityBase
    {
        public string BuyPlayerEtGuid { get; set; }
        public string Target { get; set; }
        public int GiftTbId { get; set; }
        public int CostChips { get; set; }
        public int AfterBuyChipsNum { get; set; }
        public string EventType { get; set; }
        public string EventTm { get; set; }
    }
}

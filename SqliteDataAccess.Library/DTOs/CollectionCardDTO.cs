using System;

namespace SqliteDataAccess.Library.DTOs
{
    public class CollectionCardDTO
    {

        public int SetId { get; set; }

        public int CardId { get; set; }

        public string RarityName { get; set; }

        public int Quantity { get; set; }

        public CollectionCardDTO(int setId, int cardId, string rarityName, int quantity)
        {
            SetId = setId;
            CardId = cardId;
            RarityName = rarityName;
            Quantity = quantity;
        }

    }
}

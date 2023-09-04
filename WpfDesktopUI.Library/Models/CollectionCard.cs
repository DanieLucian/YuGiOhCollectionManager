namespace WpfDesktopUI.Library.Models
{
    public class CollectionCard
    {

        public int SetId { get; set; }

        public string SetName { get; set; }

        public string SetCode { get; set; }

        public int CardId { get; set; }

        public string CardName { get; set; }

        public string RarityName { get; set; }

        public string RarityCode { get; set; }

        public int Quantity { get; set; }

        public CollectionCard(
        int setId,
        string setName,
        string setCode,
        int cardId,
        string cardName,
        string rarityName,
        string rarityCode,
        int quantity)
        {
            SetId = setId;
            SetName = setName;
            SetCode = setCode;
            CardId = cardId;
            CardName = cardName;
            RarityName = rarityName;
            RarityCode = rarityCode == string.Empty ?
                         rarityName :
                         rarityCode.Replace("(", string.Empty).Replace(")", string.Empty);
            Quantity = quantity;
        }

    }
}

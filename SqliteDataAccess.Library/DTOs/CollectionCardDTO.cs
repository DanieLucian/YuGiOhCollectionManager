using System;
using System.Collections.Generic;
using System.Text;

namespace SqliteDataAccess.Library.DTOs
{
    public class CollectionCardDTO
    {

        public int SetId { get; set; }

        public string SetName { get; set; }

        public string SetCode { get; set; }

        public int CardId { get; set; }

        public string CardName { get; set; }

        public string RarityName { get; set; }

        public string RarityCode { get; set; }

        public int Quantity { get; set; }

    }
}

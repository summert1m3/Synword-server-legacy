﻿using Newtonsoft.Json;

namespace SynWord_Server_CSharp.Model.Purchase {
    public class PurchaseModel {
        [JsonProperty("uId")]
        public string Uid { get; set; }
        [JsonProperty("productId")]
        public string ProductId { get; set; }
        [JsonProperty("purchaseToken")]
        public string PurchaseToken { get; set; }
    }
}

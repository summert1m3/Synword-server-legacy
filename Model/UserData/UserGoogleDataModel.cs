﻿using MongoDB.Bson.Serialization.Attributes;

namespace SynWord_Server_CSharp.Model.UserData {
    [BsonIgnoreExtraElements]
    public class UserGoogleDataModel {
        public string id { get; set; }
        public string email { get; set; }
        public string verified_email { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string picture { get; set; }
        public string locale { get; set; }
    }
}

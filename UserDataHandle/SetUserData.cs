﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using SynWord_Server_CSharp.Constants;

namespace SynWord_Server_CSharp.UserData {
    public class SetUserData {
        readonly private IMongoClient _client = new MongoClient(ConfigurationManager.AppSettings["connectionString"]);
        private string uId;

        public SetUserData(string uId) {
            this.uId = uId;
        }

        public void SetPremium() {
            SetPremiumIsTrue();

            SetUniqueCheckMaxSymbolLimit(PremiumUserLimits.UniqueCheckMaxSymbolLimit);
            SetUniqueUpMaxSymbolLimit(PremiumUserLimits.UniqueUpMaxSymbolLimit);

            SetUniqueUpRequest(PremiumUserLimits.UniqueUpRequests);
            SetUniqueCheckRequest(PremiumUserLimits.UniqueCheckRequests);
        }

        private void SetPremiumIsTrue() {
            List<BsonDocument> userData = GetData();

            var filter = new BsonDocument("uId", uId);

            if (userData.Count == 0) {
                throw new Exception("User data does not exist");
            }

            var update = Builders<BsonDocument>.Update.Set("isPremium", true);

            IMongoCollection<BsonDocument> collection = GetCollection();

            collection.UpdateOne(filter, update);
        }

        public void SetUniqueCheckRequest(int count) {
            SetRequests("uniqueCheckRequests", count);
        }

        public void SetUniqueUpRequest(int count) {
            SetRequests("uniqueUpRequests", count);
        }

        public void SetUniqueCheckMaxSymbolLimit(int count) {
            SetMaxSymbolLimit("uniqueCheckMaxSymbolLimit", count);
        }

        public void SetUniqueUpMaxSymbolLimit(int count) {
            SetMaxSymbolLimit("uniqueUpMaxSymbolLimit", count);
        }

        private void SetMaxSymbolLimit(String valueName, int count) {
            List<BsonDocument> userData = GetData();

            var filter = new BsonDocument("uId", uId);

            if (userData.Count == 0) {
                throw new Exception("User data does not exist");
            }

            var update = Builders<BsonDocument>.Update.Set(valueName, count);

            IMongoCollection<BsonDocument> collection = GetCollection();

            collection.UpdateOne(filter, update);
        }

        private void SetRequests(String valueName, int count) {
            List<BsonDocument> userData = GetData();

            var filter = new BsonDocument("uId", uId);

            if (userData.Count == 0) {
                throw new Exception("User data does not exist");
            }

            var update = Builders<BsonDocument>.Update.Set(valueName, count);

            IMongoCollection<BsonDocument> collection = GetCollection();

            collection.UpdateOne(filter, update);
        }

        private List<BsonDocument> GetData() {
            IMongoDatabase database = _client.GetDatabase("synword");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("userData");
            var filter = new BsonDocument("uId", uId);
            List<BsonDocument> _userData = collection.Find(filter).ToList();
            return _userData;
        }

        private IMongoCollection<BsonDocument> GetCollection() {
            IMongoDatabase database = _client.GetDatabase("synword");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("userData");
            return collection;
        }
    }
}

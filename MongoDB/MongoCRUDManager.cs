using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDB
{
    public class MongoCRUDManager
    {
        private IMongoDatabase db;
        public MongoCRUDManager(string database)
        {
            //change your server url here
            String connectionString = "mongodb://localhost:27017";
            //var client = new MongoClient();
            var client = new MongoClient(connectionString);
            //create connection to database
            db = client.GetDatabase(database);

        }
        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecord<T>(string table)
        {
            var collection = db.GetCollection<T>(table);

            //SELECT * from table
            //get all the row
            return collection.Find(new BsonDocument()).ToList();//find return a fluent
        }

        public T LoadRecordById<T>(string table, Guid id) {
            var collection = db.GetCollection<T>(table);

            //where clause, condition statement
            //Eq means Equals ==, got others also
            var filter = Builders<T>.Filter.Eq("Id",id);

            //return 1 only cause its by ID
            return collection.Find(filter).First();
            //if return by condition
            //return collection.Find(filter).ToList();
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            //insert or an update base on what is needed.
            //is like merge , if availbale update else create

            var collection = db.GetCollection<T>(table);

            //replace one : if find an id that match with the id we pass in in the collection
            //if yes , delete the record and put new record in the place
            //if not , insert new record.
            var result = collection.ReplaceOne(
                    new BsonDocument("_id", id),
                    record,
                    new UpdateOptions { IsUpsert = true}
                );
        }
        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }



        ///------------------Course Testing
        ///



    }
}

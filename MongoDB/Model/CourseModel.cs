using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDB
{
    public class CourseModel {
        [BsonId]//id this will let mongo know that i want to name the id as 'Id'
        public Guid Id { get; set; }//mongo will check if it exist , if not it will create 
        public string name { get; set; }
        public double polyIGP { get; set; }
        public UniversityModel university { get; set; }
        

    }
}

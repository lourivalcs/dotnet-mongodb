using MongoDB.Bson;

namespace MongoDB
{
    public class TestCollection
    {
        public Bson.ObjectId _id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Test2 Test2 { get; set; }

    }

    public class Test2
    {
        public string Address { get; set; }
        public string City { get; set; }
    }
}

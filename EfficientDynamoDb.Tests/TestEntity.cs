using EfficientDynamoDb.Attributes;

namespace EfficientDynamoDb.Tests;

[DynamoDbTable("TestTable")]
internal class TestEntity
{
    [DynamoDbProperty("PK", DynamoDbAttributeType.PartitionKey)]
    public string PK { get; set; }
    
    [DynamoDbProperty("SK", DynamoDbAttributeType.SortKey)]
    public string SK { get; set; }
    
    [DynamoDbProperty("test_field")] 
    public string TestField { get; init; }
}
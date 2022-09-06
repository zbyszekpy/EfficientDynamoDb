using System;
using System.Threading.Tasks;
using EfficientDynamoDb.Operations.Shared;
using NUnit.Framework;

namespace EfficientDynamoDb.Tests;

[TestFixture]
public class ToResponseAsyncTest
{
    [Test]
    public async Task Test()
    {
        IDynamoDbContext context = new DynamoDbContext(DynamoDbContextTestConfig.Config);
        
        // Arrange
        await PutTestItem(context);
        await PutTestItem(context);
        
        // Act
        var query = await context
            .Query<TestEntity>()
            .WithKeyExpression(Condition<TestEntity>
                .On(x => x.PK)
                .EqualTo("Test"))
            .ReturnConsumedCapacity(ReturnConsumedCapacity.Total)
            .WithPaginationToken(null)
            .ToResponseAsync(default);
        
        Assert.True(query.Items.Count > 0);
        Assert.True(query.Items.Count <= 20);
    }

    private static async Task PutTestItem(IDynamoDbContext context)
    {
        await context.PutItemAsync(new TestEntity()
        {
            PK = "Test",
            SK = Guid.NewGuid().ToString(),
            TestField = Guid.NewGuid().ToString(),
        });
    }
}
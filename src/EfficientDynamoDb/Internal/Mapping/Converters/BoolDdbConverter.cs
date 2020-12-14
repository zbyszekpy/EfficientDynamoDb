using EfficientDynamoDb.DocumentModel.AttributeValues;

namespace EfficientDynamoDb.Internal.Mapping.Converters
{
    public sealed class BoolDdbConverter : DdbConverter<bool>
    {
        public override bool Read(AttributeValue attributeValue) => attributeValue.AsBool();
    }
}
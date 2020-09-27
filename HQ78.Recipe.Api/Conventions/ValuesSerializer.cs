using HQ78.Recipe.Api.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Schema.NET;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HQ78.Recipe.Api.Conventions
{
    public class ValuesSerializer<T1, T2> : SerializerBase<Values<T1,T2>>
    {
        public override Values<T1, T2> Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        )
        {
            var currentBsonType = context.Reader.GetCurrentBsonType();

            // Edge case where the bson type is an object id and it needs to
            // be mapped to a string.
            if (currentBsonType == BsonType.ObjectId)
            {
                var objectId = context.Reader.ReadObjectId();

                return new Values<T1, T2>(objectId.ToString());
            }

            var allowedTypes = args.NominalType.GetGenericArguments();
            var exceptions = new List<Exception>();

            foreach (var allowedType in allowedTypes)
            {
                try
                {
                    var obj = BsonSerializer.Deserialize(
                        context.Reader,
                        allowedType
                    );

                    return new Values<T1, T2>(obj);
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            exceptions.Add(CreateCannotBeDeserializedException());
            throw new AggregateException(exceptions);
        }
    }

    public class ValuesSerializer<T1, T2, T3> : SerializerBase<Values<T1, T2, T3>>
    {
        public override Values<T1, T2, T3> Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        )
        {
            var currentBsonType = context.Reader.GetCurrentBsonType();

            // Edge case where the bson type is an object id and it needs to
            // be mapped to a string.
            if (currentBsonType == BsonType.ObjectId)
            {
                var objectId = context.Reader.ReadObjectId();

                return new Values<T1, T2, T3>(objectId.ToString());
            }

            var allowedTypes = args.NominalType.GetGenericArguments();

            if (!context.Reader.TryDeserializeToAny(allowedTypes, out var result))
            {
                if (currentBsonType == BsonType.String)
                {
                    var stringValue = context.Reader.ReadString();

                    return new Values<T1, T2, T3>(stringValue);
                }
                else
                {
                    throw CreateCannotBeDeserializedException();
                }
            }

            return result;
        }
    }

    public class ValuesSerializer<T1, T2, T3, T4> : SerializerBase<Values<T1, T2, T3, T4>>
    {
        public override Values<T1, T2, T3, T4> Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        )
        {
            var currentBsonType = context.Reader.GetCurrentBsonType();

            // Edge case where the bson type is an object id and it needs to
            // be mapped to a string.
            if (currentBsonType == BsonType.ObjectId)
            {
                var objectId = context.Reader.ReadObjectId();

                return new Values<T1, T2, T3, T4>(objectId.ToString());
            }

            var allowedTypes = args.NominalType.GetGenericArguments();

            foreach (var allowedType in allowedTypes)
            {
                try
                {
                    var obj = BsonSerializer.Deserialize(
                        context.Reader,
                        allowedType
                    );

                    return new Values<T1, T2, T3, T4>(obj);
                }
                catch
                { }
            }

            throw CreateCannotBeDeserializedException();
        }
    }

    public class ValuesSerializer<T1, T2, T3, T4, T5, T6, T7> : SerializerBase<Values<T1, T2, T3, T4, T5, T6, T7>>
    {
        public override Values<T1, T2, T3, T4, T5, T6, T7> Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        )
        {
            var currentBsonType = context.Reader.GetCurrentBsonType();

            // Edge case where the bson type is an object id and it needs to
            // be mapped to a string.
            if (currentBsonType == BsonType.ObjectId)
            {
                var objectId = context.Reader.ReadObjectId();

                return new Values<T1, T2, T3, T4, T5, T6, T7>(objectId.ToString());
            }

            var allowedTypes = args.NominalType.GetGenericArguments();

            foreach (var allowedType in allowedTypes)
            {
                try
                {
                    var obj = BsonSerializer.Deserialize(
                        context.Reader,
                        allowedType
                    );

                    return new Values<T1, T2, T3, T4, T5, T6, T7>(obj);
                }
                catch
                { }
            }

            throw CreateCannotBeDeserializedException();
        }
    }
}

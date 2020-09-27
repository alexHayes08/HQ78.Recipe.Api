using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Schema.NET;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HQ78.Recipe.Api.Extensions
{
    public static class BsonReaderExtensions
    {
        public static bool TryDeserializeToAny(
            this IBsonReader reader,
            IEnumerable<Type> nominalTypes,
            [MaybeNull] out object[] values
        )
        {
            values = default;
            var success = false;

            // TODO: Prioritize primitives (including string), value types, then objects.
            var orderedTypes = nominalTypes
                .OrderBy(t => GetTypePriority(t))
                .ToArray();

            // Oddly enough BsonSerializer.Deserialize is fine with primitive
            // arrays but not arrays of documents. Go figure.
            if (reader.GetCurrentBsonType() == BsonType.Array)
            {
                var tempList = new List<object>();
                success = true;
                reader.ReadStartArray();

                while (reader.State != BsonReaderState.EndOfArray)
                {
                    object? value = null;

                    foreach (var nominalType in nominalTypes)
                    {
                        try
                        {
                            value = BsonSerializer.Deserialize(
                                reader,
                                nominalType
                            );

                            break;
                        }
                        catch
                        { }
                    }

                    if (value is null)
                        success = false;
                    else
                        tempList.Add(value);
                }

                reader.ReadEndArray();
                values = tempList.ToArray();
                success = values.Any();
            }
            else
            {
                values = new object[1];

                foreach (var nominalType in nominalTypes)
                {
                    try
                    {

                        values[0] = BsonSerializer.Deserialize(
                            reader,
                            nominalType
                        );

                        success = true;
                        break;
                    }
                    catch
                    { }
                }
            }

            return success;
        }

        /// <summary>
        /// Strings, ints, and other built in types have priority. Then come
        /// other value types like structs and lastly classes.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static int GetTypePriority(Type type)
        {
            if (type.IsPrimitive || type == typeof(string))
                return 0;
            else if (type.IsValueType)
                return 1;
            else
                return 2;
        }
    }
}

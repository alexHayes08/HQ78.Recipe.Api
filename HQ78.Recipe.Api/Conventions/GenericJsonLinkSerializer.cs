using HQ78.Recipe.Api.Extensions;
using HQ78.Recipe.Api.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace HQ78.Recipe.Api.Conventions
{
    /// <summary>
    /// Since some classes (like Thing) are represented by a string (which is
    /// the name) instead of the full object. This serializer handles those
    /// classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericJsonLinkSerializer<T> : SerializerBase<T> where T : new()
    {
        private readonly Action<T, string> setter;
        private readonly Func<T, string> getter;

        public GenericJsonLinkSerializer(
            Func<T, string> getter,
            Action<T, string> setter
        )
        {
            this.getter = getter;
            this.setter = setter;
        }

        public GenericJsonLinkSerializer(
            Expression<Func<T, string>> fieldOrPropAccessor
        )
        {
            getter = fieldOrPropAccessor.Compile();

            var memberName = ExpressionHelper.GetAccessorExpressionName(fieldOrPropAccessor);
            var member = typeof(T).GetMember(memberName).FirstOrDefault();
            var memberType = member.GetMemberType();
            var stringType = typeof(string);

            if (member is null)
                throw new ArgumentException();

            var paramA = Expression.Parameter(typeof(T), "obj");
            var paramB = Expression.Parameter(stringType, "value");

            this.setter = Expression.Lambda<Action<T, string>>(
                Expression.Assign(
                    Expression.MakeMemberAccess(
                        paramA,
                        member
                    ),
                    Expression.Convert(paramB, memberType)
                ),
                paramA,
                paramB
            ).Compile();
        }

        public override T Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        )
        {
            switch (context.Reader.GetCurrentBsonType())
            {
                case BsonType.String:
                    var value = context.Reader.ReadString();
                    var result = new T();
                    setter(result, value);

                    return result;

                default:
                    return (T)BsonSerializer.Deserialize(
                        context.Reader,
                        typeof(T)
                    );
            }
        }

        public override void Serialize(
            BsonSerializationContext context,
            BsonSerializationArgs args,
            T value
        )
        {
            if (UseShorthandString(value))
            {
                var result = getter(value);
                context.Writer.WriteString(result);
            }
            else
            {
                base.Serialize(context, args, value);
            }
        }

        private bool UseShorthandString(T obj)
        {
            // TODO: Check all fields/properties on object and see if only the
            // property/field designated by the getter is set.
            return false;
        }
    }
}

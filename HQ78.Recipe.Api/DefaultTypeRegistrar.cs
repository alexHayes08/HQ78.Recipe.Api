using HQ78.Recipe.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HQ78.Recipe.Api
{
    public class DefaultTypeRegistrar<T>
    {
        #region Fields

        private readonly Dictionary<Type, T> typeDictionary;
        private readonly PlaceHolderGeneratorDel<T> placeholderGenerator;
        private readonly TryGetValueDelegateDel<T>? interceptor;
        private readonly Queue<(Type, T)> generatedValues;

        #endregion

        #region Constructor

        public DefaultTypeRegistrar(
            PlaceHolderGeneratorDel<T> placeholderGenerator,
            TryGetValueDelegateDel<T>? interceptor = null
        )
        {
            this.placeholderGenerator = placeholderGenerator;
            this.interceptor = interceptor;

            typeDictionary = new Dictionary<Type, T>();
            generatedValues = new Queue<(Type, T)>();
        }

        #endregion

        #region Properties

        public bool HasGeneratedValues => generatedValues.Any();

        #endregion

        #region Methods

        public bool TryGetValue(
            Type type,
            [MaybeNullWhen(false)] out T value,
            bool generateValueIfMissing = true
        )
        {
            if (interceptor is object)
            {
                var interceptorSucceeded = interceptor(type, out value);

                if (interceptorSucceeded)
                    return interceptorSucceeded;
            }

            var result = typeDictionary.TryGetValue(type, out value);

            if (generateValueIfMissing && !result)
            {
                value = placeholderGenerator(type, out var isCompleted);
                typeDictionary[type] = value;

                if (!isCompleted)
                    generatedValues.Enqueue((type, value));

                result = true;
            }

            if (result)
            {
                if (value is null)
                    throw new Exception();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Register(Type type, T value)
        {
            typeDictionary.Add(type, value);
        }

        public (Type, T) PeekNextGeneratedValue()
        {
            return generatedValues.Peek();
        }

        public (Type, T) DequeueNextGeneratedValue()
        {
            return generatedValues.Dequeue();
        }

        #endregion
    }
}

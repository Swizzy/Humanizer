﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Humanizer.Localisation.CollectionFormatters
{
    class DefaultCollectionFormatter : ICollectionFormatter
    {
        protected string DefaultSeparator = "";

        public DefaultCollectionFormatter(string defaultSeparator)
        {
            DefaultSeparator = defaultSeparator;
        }

        public virtual string Humanize<T>(IEnumerable<T> collection)
        {
            return Humanize(collection, o => o.ToString(), DefaultSeparator);
        }

        public virtual string Humanize<T>(IEnumerable<T> collection, Func<T, string> objectFormatter)
        {
            return Humanize(collection, objectFormatter, DefaultSeparator);
        }

        public virtual string Humanize<T>(IEnumerable<T> collection, string separator)
        {
            return Humanize(collection, o => o.ToString(), separator);
        }

        public virtual string Humanize<T>(IEnumerable<T> collection, Func<T, string> objectFormatter, string separator)
        {
            if (collection == null)
                throw new ArgumentException("collection");

            T[] itemsArray = collection as T[] ?? collection.ToArray();

            int count = itemsArray.Length;

            if (count == 0)
                return "";

            if (count == 1)
                return objectFormatter(itemsArray[0]);

            IEnumerable<T> itemsBeforeLast = itemsArray.Take(count - 1);
            T lastItem = itemsArray.Skip(count - 1).First();

            return string.Format("{0} {1} {2}",
                string.Join(", ", itemsBeforeLast.Select(objectFormatter)),
                separator,
                objectFormatter(lastItem));
        }
    }
}

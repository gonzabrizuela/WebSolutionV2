﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyChain.Client.HelperService
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Metodo de extension para metodos que implementan IENUMERABLE 
        /// podes 
        /// </summary>
        public static IEnumerable<T> Map<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
        {
            foreach (var item in enumerable)
            {
                await action(item);
            }
        }


    }
}

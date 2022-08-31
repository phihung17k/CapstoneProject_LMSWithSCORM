using System;
using System.Collections.Generic;
using System.Linq;

namespace LMS.Infrastructure.Utils
{
    public class OtherUtils
    {
        public static List<T> GetRandomElements<T>(IEnumerable<T> list, int elementsCount)
        {
            return list.OrderBy(x => Guid.NewGuid()).Take(elementsCount).ToList();
        }
    }
}

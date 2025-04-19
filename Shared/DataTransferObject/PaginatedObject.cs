using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObject.Products;

namespace Shared.DataTransferObject
{
    public record PaginatedObject<T>(int PageIndex,int PageSize,int TotalCount,IEnumerable<T> Data);
  
}

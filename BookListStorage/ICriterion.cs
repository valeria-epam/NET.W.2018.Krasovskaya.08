using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListStorage
{
    public interface ICriterion
    {
        bool IsMatch(Book book);
    }
}

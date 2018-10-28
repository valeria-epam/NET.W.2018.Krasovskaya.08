using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListStorage
{
    /// <summary>
    /// Represents a criterion which a book can match.
    /// </summary>
    public interface ICriterion
    {
        /// <summary>
        /// Checks whether the <paramref name="book"/> matches the criterion.
        /// </summary>
        bool IsMatch(Book book);
    }
}

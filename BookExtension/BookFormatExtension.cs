using System;
using System.Globalization;
using BookListStorage;

namespace BookExtension
{
    /// <summary>
    /// Class that implements interfaces for custom representation of book.
    /// </summary>
    public class BookFormatExtension : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">An object that specifies the type of format object to return.</param>
        /// <returns>
        /// An instance of the object specified by <paramref name="formatType" />, if the <see cref="T:System.IFormatProvider" /> 
        /// implementation can supply that type of object; otherwise, <see langword="null" />.
        /// </returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation using specified format and 
        /// culture-specific formatting information.
        /// </summary>
        /// <param name="format">A format string containing formatting specifications.</param>
        /// <param name="arg">An object to format.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <returns>
        /// The string representation of the value of <paramref name="arg" />, formatted as specified by <paramref name="format" /> 
        /// and <paramref name="formatProvider" />.
        /// </returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string thisFmt = string.Empty;
            if (!string.IsNullOrEmpty(format))
            {
                thisFmt = format.Length > 1 ? format.Substring(0, 1) : format;
            }

            if (arg is Book book)
            {
                switch (thisFmt.ToUpper())
                {
                    case "Q":
                        return string.Format($"Author: {book.Author}");
                    case "Z":
                        return string.Format($"Title: {book.Name}");
                    case "R":
                        return string.Format($"Author: {book.Author}, Price: {book.Price:C0}");
                }
            }

            try
            {
                return HandleOtherFormats(format, arg);
            }
            catch (FormatException e)
            {
                throw new FormatException($"The format of '{format}' is invalid.", e);
            }
        }

        /// <summary>
        /// Handles the other formats.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg">The argument.</param>
        private string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable formattable)
            {
                return formattable.ToString(format, CultureInfo.CurrentCulture);
            }
            else if (arg != null)
            {
                return arg.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
using System.Linq;
using AleRoe.CecSharp.Model;

namespace AleRoe.CecSharp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="EdidMessage"/>
    /// </summary>
    public static class EdidMessageExtensions
    {
        /// <summary>
        /// Gets the physical address from the EDID structure
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static PhysicalAddress GetPhysicalAddress(this EdidMessage message)
        {
            return new PhysicalAddress(message.Data.ElementAt(0x28), message.Data.ElementAt(0x29));
        }
    }
}
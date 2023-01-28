using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AleRoe.CecSharp.Model
{
    /// <summary>
    /// Represents a HDMI-CEC physical address
    /// </summary>
    public readonly struct PhysicalAddress : IEquatable<PhysicalAddress>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicalAddress"/> struct.
        /// </summary>
        /// <param name="firstByte">The first byte.</param>
        /// <param name="secondByte">The second byte.</param>
        public PhysicalAddress(byte firstByte, byte secondByte)
        {
            Address = ValueTuple.Create(firstByte, secondByte);
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        public ValueTuple<byte, byte> Address { get; }

        /// <summary>
        /// Gets an unassigned physical address.
        /// </summary>
        public static PhysicalAddress None => Parse("F.F.F.F");
        

        /// <summary>
        /// Converts this instance to a string in the format "n.n.n.n"
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var result = (Address.Item1.ToString("X2") + Address.Item2.ToString("X2"))
                .Select(x => x.ToString())
                .ToArray();
            return string.Join(".", result);
        }

        /// <summary>
        /// Converts the string representation of a <c>PhysicalAddress</c> to the equivalent <see cref="PhysicalAddress"/> struct.
        /// </summary>
        /// <param name="value">The string to convert. Must be in n.n.n.n format.</param>
        /// <returns>A <c>PhysicalAddress</c> structure that contains the value that was parsed.</returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static PhysicalAddress Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            var segments = value.Split(new[] { Constants.AddressDelimiter });
            if (segments.Length != 4 || value.Length != 7)
                throw new ArgumentException("Value is not in the format n.n.n.n");

            var firstByte = Convert.ToByte(segments[0] + segments[1], 16);
            var secondByte = Convert.ToByte(segments[2] + segments[3], 16);

            return new PhysicalAddress(firstByte, secondByte);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                var hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ Address.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc/>
        public bool Equals([AllowNull] PhysicalAddress value)
        {
            return Equals(Address, value.Address);
        }

        /// <inheritdoc/>
        public override bool Equals(object value)
        {
            // Is null?
            if (ReferenceEquals(null, value))
                return false;

            // Is the same type?
            if (value.GetType() != this.GetType())
                return false;

            return Equals((PhysicalAddress)value);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(PhysicalAddress left, PhysicalAddress right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(PhysicalAddress left, PhysicalAddress right)
        {
            return !(left == right);
        }
    }
}
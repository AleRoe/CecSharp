using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;

namespace AleRoe.CecSharp
{
    /// <summary>
    /// A structure representing a EDID (Extended Display Identification Data) message.
    /// </summary>
    public readonly struct EdidMessage : IHdmiMessage, IEquatable<EdidMessage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdidMessage"/> struct.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="data">The data.</param>
        public EdidMessage(byte block, byte[] data)
        {
            Block = block;
            Data = data;
        }

        /// <summary>
        /// Gets the block.
        /// </summary>
        public byte Block { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Converts the string representation of a <c>EdidMessage</c> to the equivalent <see cref="EdidMessage"/> structure.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A <c>EdidMessage</c> structure that contains the value that was parsed.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static EdidMessage Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            //check if the message contains the expected number of sections
            var sections = value.Split(Constants.SectionDelimiter);
            if (sections.Length != 2)
                throw new ArgumentException($"Incorrect message format. Wrong number of sections. Expected '0xNN NN:NN:NN:NN:[NN:]' but was '{value}'.");

            var block = Convert.ToByte(sections[0], 16);
            var data = sections[1]
                .Split(Constants.BytesDelimiter)
                .Select(x => Convert.ToByte(x, 16))
                .ToArray();

            return new EdidMessage(block, data);
        }


        /// <inheritdoc cref="IHdmiMessage"/>
        public override string ToString()
        {
            var result = string.Format($"0x{Block:X2} {Data.ToHex()}");
            return result;
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

            return Equals((EdidMessage)value);
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
                hash = (hash * hashingMultiplier) ^ Block.GetHashCode();
                hash = (hash * hashingMultiplier) ^ Data.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc/>
        public bool Equals([AllowNull] EdidMessage value)
        {
            return Equals(Block, value.Block)
                   && Equals(Data, value.Data);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(EdidMessage left, EdidMessage right)
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
        public static bool operator !=(EdidMessage left, EdidMessage right)
        {
            return !(left == right);
        }
    }
}
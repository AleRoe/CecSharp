using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;

namespace AleRoe.CecSharp
{
    /// <summary>
    /// A structure representing a HDMI-CEC message.
    /// </summary>
    public readonly struct CecMessage : IHdmiMessage, IEquatable<CecMessage>
    {
        private readonly bool isAcknowledged;

        /// <summary>
        /// Initializes a new instance of the <see cref="CecMessage"/> struct.
        /// </summary>
        /// <param name="source">The source address.</param>
        /// <param name="destination">The destination address.</param>
        public CecMessage(LogicalAddress source, LogicalAddress destination)
            : this(source, destination, Command.None, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CecMessage"/> struct.
        /// </summary>
        /// <param name="source">The source address.</param>
        /// <param name="destination">The destination address.</param>
        /// <param name="command">The message command.</param>
        public CecMessage(LogicalAddress source, LogicalAddress destination, Command command)
            : this(source, destination, command, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CecMessage"/> struct.
        /// </summary>
        /// <param name="source">The source address.</param>
        /// <param name="destination">The destination address.</param>
        /// <param name="command">The message command.</param>
        /// <param name="parameters">The message parameters.</param>
        public CecMessage(LogicalAddress source, LogicalAddress destination, Command command, byte[] parameters) 
        : this(source, destination, command, false, parameters) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CecMessage"/> struct.
        /// </summary>
        /// <param name="source">The source address.</param>
        /// <param name="destination">The destination address.</param>
        /// <param name="command">The message command.</param>
        /// <param name="isAcknowledged">if set to <c>true</c> [is acknowledged].</param>
        /// <param name="parameters">The message parameters.</param>
        public CecMessage(LogicalAddress source, LogicalAddress destination, Command command, bool isAcknowledged, byte[] parameters)
        {
            Source = source;
            Destination = destination;
            Command = command;
            Parameters = parameters;
            this.isAcknowledged = isAcknowledged;
        }

        /// <summary>
        /// Gets the message source.
        /// </summary>
        /// <value>
        /// <see cref="LogicalAddress"/>
        /// </value>
        public LogicalAddress Source { get; }

        /// <summary>
        /// Gets the message destination.
        /// </summary>
        /// <value>
        /// <see cref="LogicalAddress"/>
        /// </value>
        public LogicalAddress Destination { get; }

        /// <summary>
        /// Gets the message id command.
        /// </summary>
        /// <value>
        /// <see cref="Model.Command"/>
        /// </value>
        public Command Command { get; }

        /// <summary>
        /// Gets the message parameters.
        /// </summary>
        public byte[] Parameters { get; }

        /// <summary>
        /// Determines whether this message is acknowledged.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is acknowledged; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAcknowledged() => isAcknowledged;
        
        /// <summary>
        /// Gets a noop message
        /// </summary>
        /// <value>
        /// <see cref="CecMessage"/>
        /// </value>
        public static CecMessage None => new CecMessage(LogicalAddress.Unregistered, LogicalAddress.Unregistered, Command.None, null);

        /// <summary>
        /// Converts the string representation of a CecMessage to the equivalent <see cref="CecMessage"/> structure.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A <c>CecMessage</c> structure that contains the value that was parsed.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static CecMessage Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            //parse the data
            try
            {
                var isAcknowledged = false;
                if (value.EndsWith(Constants.AckIdentifier))
                {
                    isAcknowledged = true;
                    value = value.TrimEnd(Constants.AckIdentifier);
                }

                var bytes = value.Split(Constants.BytesDelimiter);

                var source = EnumHelper.ParseExact<LogicalAddress>(int.Parse(bytes[0].Substring(0, 1), NumberStyles.HexNumber));
                var destination = EnumHelper.ParseExact<LogicalAddress>(int.Parse(bytes[0].Substring(1, 1), NumberStyles.HexNumber));
                var messageId = bytes.Length > 1 ? EnumHelper.ParseExact<Command>(int.Parse(bytes[1], NumberStyles.HexNumber)) : Command.None;
                var parameters = bytes.Length > 2 ? bytes.Skip(2).Select(x => Convert.ToByte(x, 16)).ToArray() : null;

                return new CecMessage(source, destination, messageId, isAcknowledged, parameters);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Error parsing value: {value}.", e);
            }
        }
        
        /// <inheritdoc cref="IHdmiMessage"/>
        public override string ToString()
        {
            return $"{this.ToCec()} ({this.ToVerbose()})";
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
                hash = (hash * hashingMultiplier) ^ Source.GetHashCode();
                hash = (hash * hashingMultiplier) ^ Destination.GetHashCode();
                hash = (hash * hashingMultiplier) ^ Command.GetHashCode();
                hash = (hash * hashingMultiplier) ^ isAcknowledged.GetHashCode();
                hash = (hash * hashingMultiplier) ^ (Parameters != null ? Parameters.GetHashCode() : 0);
                return hash;
            }
        }

        /// <inheritdoc/>
        public bool Equals([AllowNull] CecMessage value)
        {
            return Equals(Source, value.Source)
                && Equals(Destination, value.Destination)
                && Equals(Command, value.Command)
                && Equals(IsAcknowledged(), value.IsAcknowledged())
                && StructuralComparisons.StructuralEqualityComparer.Equals(Parameters, value.Parameters);
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

            return Equals((CecMessage)value);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(CecMessage left, CecMessage right)
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
        public static bool operator !=(CecMessage left, CecMessage right)
        {
            return !left.Equals(right);
        }
    }
}
using System;

namespace AleRoe.CecSharp.Model
{
    /// <summary>
    /// Used to indicate the CEC feature that a field belongs to.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class FeaturesAttribute : Attribute
    {
        /// <summary>
        /// Gets the features.
        /// </summary>
        public Features Features { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturesAttribute"/> class.
        /// </summary>
        /// <param name="features">The features.</param>
        public FeaturesAttribute(Features features)
        {
            this.Features = features;
        }
    }
}
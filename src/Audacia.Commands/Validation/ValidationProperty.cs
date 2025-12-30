using System;
using System.Linq.Expressions;
using Audacia.Core.Extensions;

namespace Audacia.Commands.Validation
{
    /// <summary>
    /// Validates a single property.
    /// </summary>
    /// <typeparam name="TProperty">Type of the member being validated.</typeparam>
    public class ValidationProperty<TProperty> : ValidationBase
    {
        private readonly IValidationModel _validationModel;

        /// <summary>
        /// Initializes an instance of <see cref="ValidationProperty{TProperty}"/>.
        /// </summary>
        /// <param name="validationModel">The <see cref="IValidationModel"/> instance.</param>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="displayName">The user friendly name of the property.</param>
        /// <param name="value">The property value.</param>
        public ValidationProperty(
            IValidationModel validationModel,
            string propertyName,
            string? displayName,
            TProperty value)
        {
            _validationModel = validationModel;
            PropertyName = propertyName;
            DisplayName = displayName ?? propertyName.SplitCamelCase();
            Value = value;
        }

        /// <summary>
        /// Adds a validation error to the property.
        /// </summary>
        /// <param name="message">Description of the validation error.</param>
        public void AddError(string message) => _validationModel.AddModelError(PropertyName, message);

        /// <summary>
        /// Gets a user-friendly model name.
        /// </summary>
        public string ModelName => _validationModel.ModelName;

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Gets a user-friendly property name.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        public TProperty Value { get; }

        /// <summary>
        /// Creates a child validation property on a property object.
        /// </summary>
        /// <typeparam name="TChild">Type of the member being validated.</typeparam>
        /// <param name="property">Expression referencing the member being validated.</param>
        /// <param name="displayName">User-friendly member name.</param>
        /// <returns>A validation property.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>
        public ValidationProperty<TChild> Property<TChild>(
            Expression<Func<TProperty, TChild>> property,
            string? displayName = null)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            var propertyName = GetNameForProperty(property);
            var value = property.Compile()(Value);
            var childPropertyName = $"{PropertyName}.{propertyName}";

            return new ValidationProperty<TChild>(_validationModel, childPropertyName, displayName, value);
        }
    }
}
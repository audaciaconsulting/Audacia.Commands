using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Audacia.Commands.Validation
{
    /// <summary>
    /// Interface representing a validation model.
    /// </summary>
    public interface IValidationModel
    {
        /// <summary>
        /// Gets a dictionary of key value pairs of Property Name and Validation Errors.
        /// Any errors against the Model uses a blank string as the Property Name.
        /// </summary>
        IDictionary<string, IEnumerable<string>> ModelErrors { get; }
        
        /// <summary>
        /// Gets all validation errors regardless of property.
        /// </summary>
        IEnumerable<string> AllErrors { get; }

        /// <summary>
        /// Gets a value indicating whether there are any errors.
        /// </summary>
        bool IsValid { get; }
        
        /// <summary>
        /// Gets a user-friendly model name.
        /// </summary>
        string ModelName { get; }

        /// <summary>
        /// Adds a validation error for a property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="errorMessage">Description of the validation error.</param>
        void AddModelError(string propertyName, string errorMessage);
        
        /// <summary>
        /// Checks if the model is <see langword="null"/>.
        /// Adds a validation error when <see langword="null"/>.
        /// </summary>
        /// <returns><see langword="true"/> if the model is <see langword="null"/>.</returns>
        bool IsModelNull();
        
        /// <summary>
        /// Appends any validation errors to the command result.
        /// </summary>
        /// <returns>A <see cref="CommandResult"/>.</returns>
        CommandResult ToCommandResult();
    }

    /// <summary>
    /// Interface representing a validation model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IValidationModel<TModel> : IValidationModel where TModel : class
    {
        /// <summary>
        /// Creates a validation property.
        /// </summary>
        /// <typeparam name="TProperty">Type of the member being validated.</typeparam>
        /// <param name="property">Expression referencing the member being validated.</param>
        /// <param name="displayName">User-friendly member name.</param>
        /// <returns>A validation property.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Name best represents the purpose of the method.")]
        ValidationProperty<TProperty> Property<TProperty>(Expression<Func<TModel, TProperty>> property, string? displayName = null);
    }
}
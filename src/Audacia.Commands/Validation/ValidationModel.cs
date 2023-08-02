using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Audacia.Core.Extensions;

namespace Audacia.Commands.Validation
{
    /// <summary>
    /// Validates a command or DTO.
    /// </summary>
    /// <typeparam name="TModel">Type of the model being validated.</typeparam>
    public class ValidationModel<TModel> : ValidationBase, IValidationModel<TModel> where TModel : class
    {
        private readonly TModel _model;
        private readonly IDictionary<string, HashSet<string>> _errors;

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationModel{TModel}"/>.
        /// </summary>
        /// <param name="model">The <typeparamref name="TModel"/> instance to validate.</param>
        /// <param name="modelName">The name of the model.</param>
        public ValidationModel(TModel model, string modelName)
        {
            ModelName = string.IsNullOrWhiteSpace(modelName) 
                ? typeof(TModel).Name.SplitCamelCase()
                : modelName;
            _model = model;
            _errors = new Dictionary<string, HashSet<string>>();
        }

        /// <inheritdoc />
        public string ModelName { get; }

        /// <inheritdoc />
        public bool IsValid => !_errors.Any();

        /// <inheritdoc />
        public IDictionary<string, IEnumerable<string>> ModelErrors => _errors as IDictionary<string, IEnumerable<string>> ?? new Dictionary<string, IEnumerable<string>>();

        /// <inheritdoc />
        public IEnumerable<string> AllErrors => _errors.SelectMany(kvp => kvp.Value).ToArray();

        /// <inheritdoc />
        public void AddModelError(string propertyName, string errorMessage)
        {
            if (propertyName == null)
            {
                throw new ArgumentException("Property Name cannot be null", nameof(propertyName));
            }

            if (string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentException("Model Error Message cannot be empty", nameof(errorMessage));
            }

            if (_errors.ContainsKey(propertyName))
            {
                _errors[propertyName].Add(errorMessage);
            }
            else
            {
                _errors.Add(propertyName, new HashSet<string> { errorMessage });
            }
        }

        /// <inheritdoc />
        public bool IsModelNull()
        {
            if (_model == default(TModel))
            {
                AddModelError(string.Empty, "No data received");

                return true;
            }

            return false;
        }
        
        /// <inheritdoc />
        public ValidationProperty<TProperty> Property<TProperty>(Expression<Func<TModel, TProperty>> property, string? displayName = null)
        { 
            if (property == null) throw new ArgumentNullException(nameof(property));

            var propertyName = GetNameForProperty(property);
            var value = property.Compile()(_model); 

            return new ValidationProperty<TProperty>(this, propertyName, displayName, value);
        }

        /// <inheritdoc />
        public Task<CommandResult> ToCommandResultAsync()
        {
            return Task.FromResult(new CommandResult(IsValid, AllErrors));
        }
    }
}

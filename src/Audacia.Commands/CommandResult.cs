using System;
using System.Collections.Generic;
using System.Linq;
using Audacia.Core;

namespace Audacia.Commands
{
    /// <summary>
    /// Represents the result of handling an <see cref="ICommand"/>.
    /// If the result represents a failure then a collection of errors will contain the details.
    /// </summary>
    public class CommandResult : IResult
    {
        private readonly List<string> _errors;

        /// <summary>
        /// Gets a value indicating whether the handling of the <see cref="ICommand"/> succeeded (true) or failed (false).
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Gets a collection of errors; this collection should only be populated if the result represents a failure.
        /// </summary>
        public IEnumerable<string> Errors => _errors;

        /// <summary>
        /// Initializes an instance of <see cref="CommandResult"/> with the given <paramref name="errors"/>.
        /// </summary>
        /// <param name="errors">The <see cref="ICollection{T}"/> object containing the errors.</param>
        public CommandResult(ICollection<string> errors)
        {
            IsSuccess = errors == null || errors.Count == 0;
            _errors = new List<string>(errors ?? Enumerable.Empty<string>());
        }

        /// <summary>
        /// Initializes an instance of <see cref="CommandResult"/> with the given <paramref name="errors"/> and <paramref name="isSuccess"/> flag.
        /// </summary>
        /// <param name="isSuccess">A <see cref="bool"/> indicating whether the result represents success or not.</param>
        /// <param name="errors">The errors.</param>
        public CommandResult(bool isSuccess, params string[] errors)
        {
            IsSuccess = isSuccess;
            _errors = new List<string>(errors);
        }

        /// <summary>
        /// Initializes an instance of <see cref="CommandResult"/> with the given <paramref name="errors"/> and <paramref name="isSuccess"/> flag.
        /// </summary>
        /// <param name="isSuccess">A <see cref="bool"/> indicating whether the result represents success or not.</param>
        /// <param name="errors">The errors.</param>
        public CommandResult(bool isSuccess, IEnumerable<string> errors)
        {
            IsSuccess = isSuccess;
            _errors = new List<string>(errors);
        }

        /// <summary>
        /// Creates a <see cref="CommandResult"/> object representing the successful handling of an <see cref="ICommand"/>.
        /// </summary>
        /// <returns>A <see cref="CommandResult"/> object indicating success.</returns>
        public static CommandResult Success()
        {
            return new CommandResult(true);
        }

        /// <summary>
        /// Creates a <see cref="CommandResult{T}"/> object representing the failed handling of an <see cref="ICommand"/>.
        /// The error details are contained in the <paramref name="errors"/> collection.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <returns>A <see cref="CommandResult{T}"/> object containing the errors.</returns>
        public static CommandResult<TOutput> Failure<TOutput>(params string[] errors)
        {
            return new CommandResult<TOutput>(false, errors);
        }

        /// <summary>
        /// Creates a <see cref="CommandResult{T}"/> object representing the failed handling of an <see cref="ICommand"/>.
        /// The error details are contained in the <paramref name="errors"/> collection.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <returns>A <see cref="CommandResult{T}"/> object containing the errors.</returns>
        public static CommandResult<TOutput> Failure<TOutput>(ICollection<string> errors)
        {
            return new CommandResult<TOutput>(errors);
        }

        /// <summary>
        /// Creates a <see cref="CommandResult"/> from the given <paramref name="result"/>.
        /// </summary>
        /// <param name="result">The <see cref="IResult"/> object from which to create the <see cref="CommandResult"/>.</param>
        /// <returns>A new <see cref="CommandResult"/> object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="result"/> is <see langword="null"/>.</exception>
        public static CommandResult FromExistingResult(IResult result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            return new CommandResult(result.IsSuccess, result.Errors.ToArray());
        }

        /// <summary>
        /// Creates a <see cref="CommandResult{TOutput}"/> object from the given <paramref name="result"/>.
        /// </summary>
        /// <param name="result">The <see cref="IResult"/> object from which to create the <see cref="CommandResult{T}"/>.</param>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <returns>A new <see cref="CommandResult{T}"/> object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="result"/> is <see langword="null"/>.</exception>
        public static CommandResult<TOutput> FromExistingResult<TOutput>(IResult result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            return new CommandResult<TOutput>(result.IsSuccess, result.Errors.ToArray());
        }

        /// <summary>
        /// Creates a <see cref="CommandResult{TOutput}"/> object from the given <paramref name="result"/>.
        /// </summary>
        /// <param name="result">The <see cref="IResult{T}"/> object from which to create the <see cref="CommandResult{T}"/>.</param>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <returns>A new <see cref="CommandResult{T}"/> object.</returns>
        public static CommandResult<TOutput> FromExistingResult<TOutput>(IResult<TOutput> result)
        {
            return new CommandResult<TOutput>(result);
        }

        /// <summary>
        /// Creates a <see cref="CommandResult"/> object representing the failed handling of an <see cref="ICommand"/>.
        /// The error details are contained in the <paramref name="errors"/> collection.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <returns>A new <see cref="CommandResult"/> object containing the given <paramref name="errors"/>.</returns>
        public static CommandResult Failure(params string[] errors)
        {
            return new CommandResult(false, errors);
        }
        
        /// <summary>
        /// Creates a <see cref="CommandResult{T}"/> object representing the successful handling of an <see cref="ICommand"/>,
        /// with the output of execution given by the <paramref name="output"/>.
        /// </summary>
        /// <typeparam name="T">The type of the output.</typeparam>
        /// <param name="output">The output from which to create the result.</param>
        /// <returns>A <see cref="CommandResult{T}"/> object containing the given <paramref name="output"/>.</returns>
        public static CommandResult<T> WithResult<T>(T output)
        {
            return new CommandResult<T>(output);
        }

        /// <summary>
        /// Adds the given <paramref name="error"/> to the <see cref="CommandResult"/>'s <see cref="Errors"/> property.
        /// </summary>
        /// <param name="error">The error to add.</param>
        public void AddError(string error)
        {
            _errors.Add(error);
            IsSuccess = false;
        }
        
        /// <summary>
        /// Adds the given <paramref name="errors"/> to the <see cref="CommandResult"/>'s <see cref="Errors"/> property.
        /// </summary>
        /// <param name="errors">The errors to add.</param>
        public void AddErrors(params string[] errors)
        {
            _errors.AddRange(errors);
            IsSuccess = !_errors.Any();
        }

        /// <summary>
        /// Adds the errors from the given <paramref name="commandResult"/> to the <see cref="CommandResult"/>'s <see cref="Errors"/> property.
        /// </summary>
        /// <param name="commandResult">The <see cref="CommandResult"/> from which to add the errors.</param>
        public void AddErrorsFrom(CommandResult commandResult)
        {
            _errors.AddRange(commandResult?.Errors ?? Enumerable.Empty<string>());
            IsSuccess = !_errors.Any();
        }
    }

    /// <summary>
    /// Represents the result of handling an <see cref="ICommand"/> where the handling requires an output (of type <typeparamref name="T"/>) to be returned to the client.
    /// </summary>
    /// <typeparam name="T">The type of the output.</typeparam>
    public class CommandResult<T> : CommandResult, IResult<T>
    {
        /// <summary>
        /// Gets the output.
        /// </summary>
        public T Output { get; } = default!;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandResult"/>.
        /// </summary>
        /// <param name="isSuccess">A <see cref="bool"/> value indicating whether or not the result represents success.</param>
        /// <param name="errors">The errors.</param>
        public CommandResult(bool isSuccess, params string[] errors)
            : base(isSuccess, errors)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CommandResult"/>.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public CommandResult(ICollection<string> errors)
            : base(errors)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CommandResult"/>.
        /// </summary>
        /// <param name="output">The output.</param>
        public CommandResult(T output)
            : base(true)
        {
            Output = output;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CommandResult"/>.
        /// </summary>
        /// <param name="result">An existing result.</param>
        public CommandResult(IResult<T> result)
            : base(result?.IsSuccess == true, result?.Errors ?? Enumerable.Empty<string>())
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            Output = result.Output;
        }
    }
}
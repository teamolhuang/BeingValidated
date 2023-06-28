using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeingValidated
{
    /// <summary>
    ///     A wrapper class for validating an enumerable. <br />
    ///     It's recommended that you use <see cref="BeingValidatedHelperMethods.StartValidateElements{T}" /> to get this.
    /// </summary>
    /// <typeparam name="TElement">Generic type for the element in the enumerable to be validated</typeparam>
    /// <typeparam name="TEnumerable">Generic type for the enumerable</typeparam>
    public class BeingValidatedEnumerable<TElement, TEnumerable> : IBeingValidated<TElement, TEnumerable>
        where TEnumerable : IEnumerable<TElement>
    {
        private readonly IBeingValidated<TEnumerable, TEnumerable> _inner;
        private readonly TEnumerable _targetEnumerable;
        private Predicate<TElement> _forceSkipCondition;

        /// <summary>
        ///     It's recommended that you use <see cref="BeingValidatedHelperMethods.StartValidateElements{T}" /> to get this.
        /// </summary>
        public BeingValidatedEnumerable(TEnumerable target, bool skipIfAlreadyInvalid = false)
        {
            _targetEnumerable = target;
            _inner = target.StartValidate(skipIfAlreadyInvalid);
        }

        /// <summary>
        ///     Validate every element in the enumerable with provided validation.
        /// </summary>
        /// <param name="validation">
        ///     A method for validation, which should accept TElement as input, and outputs boolean for
        ///     indicating if validation passed.
        /// </param>
        /// <param name="onFail">(Optional) Method to execute on validation failed.</param>
        /// <param name="onException">(Optional) Method to execute on exception. By default, exception will not be caught.</param>
        /// <returns>This IBeingValidated</returns>
        public IBeingValidated<TElement, TEnumerable> Validate(Func<TElement, bool> validation,
            Action<TElement> onFail = null, Action<TElement, Exception> onException = null)
        {
            if (onException == null)
                onException = DefaultOnException();

            foreach (TElement element in _targetEnumerable)
            {
                if (NeedForceSkipping(element))
                    continue;

                _inner.Validate(_ => validation.Invoke(element),
                    _ => onFail?.Invoke(element),
                    (_, e) => onException.Invoke(element, e)
                );
            }

            return this;
        }

        /// <summary>
        ///     Validate every element in the enumerable with provided validation.
        /// </summary>
        /// <param name="validation">
        ///     A method for validation, which should accept TElement as input, and has no output.
        /// </param>
        /// <param name="onException">(Optional) Method to execute on exception. By default, exception will not be caught.</param>
        /// <returns>This IBeingValidated</returns>
        public IBeingValidated<TElement, TEnumerable> Validate(Action<TElement> validation,
            Action<TElement, Exception> onException = null)
        {
            return Validate(element =>
            {
                validation.Invoke(element);
                return true;
            }, null, onException);
        }

        /// <summary>
        ///     Asynchronously validate every element in the enumerable with provided validation.
        /// </summary>
        /// <param name="validation">
        ///     A method for validation, which should accept TElement as input, and outputs boolean for
        ///     indicating if validation passed.
        /// </param>
        /// <param name="onFail">(Optional) Method to execute on validation failed.</param>
        /// <param name="onException">(Optional) Method to execute on exception. By default, exception will not be caught.</param>
        /// <returns>This IBeingValidated</returns>
        /// <remarks>
        ///     <b>
        ///         This doesn't distribute the validation of multiple elements towards different threads. It's only to support
        ///         validation methods that uses async/await.
        ///     </b>
        /// </remarks>
        public async Task<IBeingValidated<TElement, TEnumerable>> ValidateAsync(Func<TElement, Task<bool>> validation,
            Action<TElement> onFail = null, Action<TElement, Exception> onException = null)
        {
            if (onException == null)
                onException = DefaultOnException();

            foreach (TElement element in _targetEnumerable)
            {
                if (NeedForceSkipping(element))
                    continue;

                await _inner.ValidateAsync(async _ => await validation.Invoke(element),
                    _ => onFail?.Invoke(element),
                    (_, e) => onException.Invoke(element, e));
            }

            return this;
        }

        /// <summary>
        ///     Asynchronously validate every element in the enumerable with provided validation.
        /// </summary>
        /// <param name="validation">
        ///     A method for validation, which should accept TElement as input, and has no output.
        /// </param>
        /// <param name="onException">(Optional) Method to execute on exception. By default, exception will not be caught.</param>
        /// <returns>This IBeingValidated</returns>
        /// <remarks>
        ///     <b>
        ///         This doesn't distribute the validation of multiple elements towards different threads. It's only to support
        ///         validation methods that uses async/await.
        ///     </b>
        /// </remarks>
        public async Task<IBeingValidated<TElement, TEnumerable>> ValidateAsync(Func<TElement, Task> validation,
            Action<TElement, Exception> onException = null)
        {
            return await ValidateAsync(async element =>
                {
                    await validation.Invoke(element);
                    return true;
                },
                null,
                onException);
        }

        /// <inheritdoc />
        public bool IsValid()
        {
            return _inner.IsValid();
        }

        /// <inheritdoc />
        public IBeingValidated<TElement, TEnumerable> SkipIfAlreadyInvalid(bool setTo = true)
        {
            _inner.SkipIfAlreadyInvalid(setTo);
            return this;
        }

        /// <inheritdoc />
        public IBeingValidated<TElement, TEnumerable> ForceSkipIf(Predicate<TElement> predicate)
        {
            _forceSkipCondition = predicate;
            return this;
        }

        /// <inheritdoc />
        public IBeingValidated<TElement, TEnumerable> StopForceSkipping()
        {
            _forceSkipCondition = null;
            return this;
        }

        private static Action<TElement, Exception> DefaultOnException()
        {
            return (_, e) => throw e;
        }

        private bool NeedForceSkipping(TElement element)
        {
            // TODO: Caching? But if element state changed before clearing cache, might cause wrong result.
            return _forceSkipCondition?.Invoke(element) ?? false;
        }
    }
}
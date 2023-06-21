using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeingValidated
{
    /// <summary>
    ///     Extension methods for BeingValidated.
    /// </summary>
    public static class BeingValidatedHelperMethods
    {
        /// <summary>
        ///     Wrap an object into a <see cref="IBeingValidated{TInput,TOutput}" />> to start validation.
        /// </summary>
        /// <param name="target">Object to validate.</param>
        /// <param name="skipIfAlreadyInvalid">
        ///     (Optional) If set to true, skip further validations on any validation failure.
        ///     Default is false.
        /// </param>
        /// <typeparam name="T">Generic Type of object to validate.</typeparam>
        /// <returns>This IBeingValidated</returns>
        public static IBeingValidated<T, T> StartValidate<T>(this T target, bool skipIfAlreadyInvalid = false)
        {
            return new BeingValidated<T>(target, skipIfAlreadyInvalid);
        }

        /// <inheritdoc
        ///     cref="IBeingValidated{TInput,TOutput}.Validate(Func{TInput, bool}, Action{TInput}, Action{TInput, Exception})" />
        public static IBeingValidated<TInput, TOutput> Validate<TInput, TOutput>(
            this IBeingValidated<TInput, TOutput> target, Func<TInput, bool> validation, Action onFail = null,
            Action<Exception> onException = null)
        {
            // This allows user to use syntax like this for onException:
            // () => ...
            // If they don't need exception info for onException.
            return target.Validate(validation, _ => onFail?.Invoke(), (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc cref="IBeingValidated{TInput,TOutput}.Validate(Action{TInput}, Action{TInput, Exception})" />
        public static IBeingValidated<TInput, TOutput> Validate<TInput, TOutput>(
            this IBeingValidated<TInput, TOutput> target,
            Action<TInput> validation,
            Action<Exception> onException = null)
        {
            // This allows user to use syntax like this for onFail or onException:
            // () => ...
            // If they don't need validated object for onFail or exception info for onException.
            return target.Validate(validation, (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc
        ///     cref="IBeingValidated{TInput,TOutput}.ValidateAsync(Func{TInput, Task{bool}}, Action{TInput}, Action{TInput, Exception})" />
        public static async Task<IBeingValidated<TInput, TOutput>> ValidateAsync<TInput, TOutput>(
            this IBeingValidated<TInput, TOutput> target,
            Func<TInput, Task<bool>> validation,
            Action onFail = null,
            Action<Exception> onException = null)
        {
            // This allows user to use syntax like this for onFail or onException:
            // () => ...
            // If they don't need validated object for onFail or exception info for onException.
            return await target.ValidateAsync(validation, _ => onFail?.Invoke(), (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc cref="IBeingValidated{TInput,TOutput}.ValidateAsync(Func{TInput, Task}, Action{TInput, Exception})" />
        public static async Task<IBeingValidated<TInput, TOutput>> ValidateAsync<TInput, TOutput>(
            this IBeingValidated<TInput, TOutput> target,
            Func<TInput, Task> validation,
            Action<Exception> onException = null)
        {
            // This allows user to use syntax like this for onException:
            // () => ...
            // If they don't need exception info for onException.
            return await target.ValidateAsync(validation, (_, e) => onException?.Invoke(e));
        }

        /// <summary>
        ///     Wrap an enumerable into a <see cref="IBeingValidated{TInput,TOutput}" />> to start validation against its elements.
        /// </summary>
        /// <param name="target">Object to validate.</param>
        /// <param name="skipIfAlreadyInvalid">
        ///     (Optional) If set to true, skip further validations on any validation failure.
        ///     Default is false.
        /// </param>
        /// <typeparam name="TElement">Generic Type of elements in the enumerable to validate.</typeparam>
        /// <returns>This IBeingValidated</returns>
        public static IBeingValidated<TElement, IEnumerable<TElement>> StartValidateElements<TElement>(
            this IEnumerable<TElement> target,
            bool skipIfAlreadyInvalid = false)
        {
            return new BeingValidatedEnumerable<TElement, IEnumerable<TElement>>(target, skipIfAlreadyInvalid);
        }
    }
}
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
        ///     Wrap an object into a <see cref="IBeingValidated{TValidated,TOriginal}" /> to start validation.
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
        ///     cref="IBeingValidated{TValidated,TOriginal}.Validate(Func{TValidated, bool}, Action{TValidated}, Action{TValidated, Exception})" />
        public static IBeingValidated<TValidated, TOriginal> Validate<TValidated, TOriginal>(
            this IBeingValidated<TValidated, TOriginal> target, Func<TValidated, bool> validation, Action onFail = null,
            Action<Exception> onException = null)
        {
            // This allows user to use syntax like this for onException:
            // () => ...
            // If they don't need exception info for onException.
            return target.Validate(validation, _ => onFail?.Invoke(), (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc cref="IBeingValidated{TValidated,TOriginal}.Validate(Action{TValidated}, Action{TValidated, Exception})" />
        public static IBeingValidated<TValidated, TOriginal> Validate<TValidated, TOriginal>(
            this IBeingValidated<TValidated, TOriginal> target,
            Action<TValidated> validation,
            Action<Exception> onException = null)
        {
            // This allows user to use syntax like this for onFail or onException:
            // () => ...
            // If they don't need validated object for onFail or exception info for onException.
            return target.Validate(validation, (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc
        ///     cref="IBeingValidated{TValidated,TOriginal}.ValidateAsync(Func{TValidated, Task{bool}}, Action{TValidated}, Action{TValidated, Exception})" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> ValidateAsync<TValidated, TOriginal>(
            this IBeingValidated<TValidated, TOriginal> target,
            Func<TValidated, Task<bool>> validation,
            Action onFail = null,
            Action<Exception> onException = null)
        {
            // This allows user to use syntax like this for onFail or onException:
            // () => ...
            // If they don't need validated object for onFail or exception info for onException.
            return await target.ValidateAsync(validation, _ => onFail?.Invoke(), (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc cref="IBeingValidated{TValidated,TOriginal}.ValidateAsync(Func{TValidated, Task}, Action{TValidated, Exception})" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> ValidateAsync<TValidated, TOriginal>(
            this IBeingValidated<TValidated, TOriginal> target,
            Func<TValidated, Task> validation,
            Action<Exception> onException = null)
        {
            // This allows user to use syntax like this for onException:
            // () => ...
            // If they don't need exception info for onException.
            return await target.ValidateAsync(validation, (_, e) => onException?.Invoke(e));
        }

        /// <summary>
        ///     Wrap an enumerable into a <see cref="IBeingValidated{TValidated,TOriginal}" /> to start validation against its elements.
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
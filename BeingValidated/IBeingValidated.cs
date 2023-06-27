using System;
using System.Threading.Tasks;

namespace BeingValidated
{
    /// <summary>
    ///     The interface of wrapper classes for validating certain object.
    ///     It is recommended to use helper methods like <see cref="BeingValidatedHelperMethods.StartValidate{T}" /> to get
    ///     concrete classes.
    /// </summary>
    public interface IBeingValidated<TValidated, TOriginal>
    {
        /// <summary>
        ///     Execute validation against TInput.
        /// </summary>
        /// <param name="validation">
        ///     A method for validation, which should accept TInput as input, and outputs boolean for
        ///     indicating if validation passed.
        /// </param>
        /// <param name="onFail">(Optional) Method to execute on validation failed.</param>
        /// <param name="onException">(Optional) Method to execute on exception. By default, exception will not be caught.</param>
        /// <returns>This IBeingValidation</returns>
        IBeingValidated<TValidated, TOriginal> Validate(Func<TValidated, bool> validation,
            Action<TValidated> onFail = null,
            Action<TValidated, Exception> onException = null);

        /// <summary>
        ///     Execute validation against TInput.
        /// </summary>
        /// <param name="validation">
        ///     A method for validation, which should accept TInput as input, and has no output.
        /// </param>
        /// <param name="onException">(Optional) Method to execute on exception. By default, exception will not be caught.</param>
        /// <returns>This IBeingValidation</returns>
        IBeingValidated<TValidated, TOriginal> Validate(Action<TValidated> validation,
            Action<TValidated, Exception> onException = null);

        /// <summary>
        ///     Asynchronously validate TInput with provided validation.
        /// </summary>
        /// <param name="validation">
        ///     A method for validation, which should accept TInput as input, and outputs boolean for
        ///     indicating if validation passed.
        /// </param>
        /// <param name="onFail">(Optional) Method to execute on validation failed.</param>
        /// <param name="onException">(Optional) Method to execute on exception. By default, exception will not be caught.</param>
        /// <returns>This IBeingValidated</returns>
        Task<IBeingValidated<TValidated, TOriginal>> ValidateAsync(Func<TValidated, Task<bool>> validation,
            Action<TValidated> onFail = null, Action<TValidated, Exception> onException = null);

        /// <summary>
        ///     Asynchronously validate TInput with provided validation.
        /// </summary>
        /// <param name="validation">
        ///     A method for validation, which should accept TInput as input, and has no output.
        /// </param>
        /// <param name="onException">(Optional) Method to execute on exception. By default, exception will not be caught.</param>
        /// <returns>This IBeingValidated</returns>
        Task<IBeingValidated<TValidated, TOriginal>> ValidateAsync(Func<TValidated, Task> validation,
            Action<TValidated, Exception> onException = null);

        /// <summary>
        ///     Returns the result of validation.
        /// </summary>
        /// <returns>
        ///     true：Validation passed.<br />
        ///     false：Validation failed.
        /// </returns>
        bool IsValid();

        /// <summary>
        ///     Skip any further validation - or until SkipIfAlreadyFailed is set to false - when any validation failed.
        /// </summary>
        /// <param name="setTo">(Optional) Enable or disable this option. Default is true.</param>
        /// <returns>This IBeingValidated</returns>
        IBeingValidated<TValidated, TOriginal> SkipIfAlreadyInvalid(bool setTo = true);

        /// <summary>
        ///     Force skip any further validation - or until StopForceSkipping - when predication is true for TInput.
        /// </summary>
        /// <param name="predicate">Condition for force skipping.</param>
        /// <returns>This IBeingValidated</returns>
        IBeingValidated<TValidated, TOriginal> ForceSkipIf(Predicate<TValidated> predicate);

        /// <summary>
        ///     Stop force skipping.
        /// </summary>
        /// <returns>This IBeingValidated</returns>
        IBeingValidated<TValidated, TOriginal> StopForceSkipping();
    }
}
using System;
using System.Threading.Tasks;

namespace BeingValidated
{
    /// <summary>
    ///     Asynchronous extension methods for BeingValidated.
    /// </summary>
    public static class BeingValidatedTaskHelperMethods
    {
        /// <inheritdoc
        ///     cref="IBeingValidated{TValidated,TOriginal}.Validate(Func{TValidated, bool}, Action{TValidated}, Action{TValidated, Exception})" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> Validate<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated
            , Func<TValidated, bool> validation
            , Action<TValidated> onFail = null
            , Action<TValidated, Exception> onException = null)
        {
            IBeingValidated<TValidated, TOriginal> obj = await beingValidated;
            return obj.Validate(validation, onFail, onException);
        }

        /// <inheritdoc
        ///     cref="Validate{TValidated,TOriginal}(System.Threading.Tasks.Task{BeingValidated.IBeingValidated{TValidated,TOriginal}},System.Func{TValidated,bool},System.Action{TValidated},System.Action{TValidated,System.Exception})" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> Validate<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated
            , Func<TValidated, bool> validation
            , Action onFail = null
            , Action<Exception> onException = null)
        {
            return await beingValidated.Validate(validation, _ => onFail?.Invoke(), (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc
        ///     cref="IBeingValidated{TValidated,TOriginal}.Validate(Func{TValidated, bool}, Action{TValidated}, Action{TValidated, Exception})" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> Validate<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated
            , Action<TValidated> validation
            , Action<TValidated, Exception> onException = null)
        {
            IBeingValidated<TValidated, TOriginal> obj = await beingValidated;
            return obj.Validate(validation, onException);
        }

        /// <inheritdoc
        ///     cref="Validate{TValidated,TOriginal}(System.Threading.Tasks.Task{BeingValidated.IBeingValidated{TValidated,TOriginal}},System.Action{TValidated},System.Action{TValidated,System.Exception})" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> Validate<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated
            , Action<TValidated> validation
            , Action<Exception> onException)
        {
            return await beingValidated.Validate(validation, (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc
        ///     cref="IBeingValidated{TValidated,TOriginal}.ValidateAsync(Func{TValidated, Task{bool}}, Action{TValidated}, Action{TValidated, Exception})" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> ValidateAsync<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated
            , Func<TValidated, Task<bool>> validation
            , Action<TValidated> onFail = null
            , Action<TValidated, Exception> onException = null)
        {
            IBeingValidated<TValidated, TOriginal> obj = await beingValidated;
            return await obj.ValidateAsync(validation, onFail, onException);
        }

        /// <inheritdoc
        ///     cref="ValidateAsync{TValidated,TOriginal}(System.Threading.Tasks.Task{BeingValidated.IBeingValidated{TValidated,TOriginal}},System.Func{TValidated,System.Threading.Tasks.Task{bool}},System.Action{TValidated},System.Action{TValidated,System.Exception})" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> ValidateAsync<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated
            , Func<TValidated, Task<bool>> validation
            , Action onFail = null
            , Action<Exception> onException = null)
        {
            return await beingValidated.ValidateAsync(validation, _ => onFail?.Invoke(),
                (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc
        ///     cref="IBeingValidated{TValidated,TOriginal}.ValidateAsync(Func{TValidated, Task{bool}}, Action{TValidated}, Action{TValidated, Exception})" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> ValidateAsync<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated
            , Func<TValidated, Task> validation
            , Action<TValidated, Exception> onException = null)
        {
            IBeingValidated<TValidated, TOriginal> obj = await beingValidated;
            return await obj.ValidateAsync(validation, onException);
        }

        /// <inheritdoc
        ///     cref="ValidateAsync{TValidated,TOriginal}(System.Threading.Tasks.Task{BeingValidated.IBeingValidated{TValidated,TOriginal}},System.Func{TValidated,System.Threading.Tasks.Task},System.Action{TValidated,System.Exception})" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> ValidateAsync<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated
            , Func<TValidated, Task> validation
            , Action<Exception> onException)
        {
            return await beingValidated.ValidateAsync(validation, (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc cref="IBeingValidated{TValidated,TOriginal}.IsValid" />
        public static async Task<bool> IsValid<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated)
        {
            IBeingValidated<TValidated, TOriginal> obj = await beingValidated;
            return obj.IsValid();
        }

        /// <inheritdoc cref="IBeingValidated{TValidated,TOriginal}.SkipIfAlreadyInvalid" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> SkipIfAlreadyInvalid<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated, bool setTo = true)
        {
            IBeingValidated<TValidated, TOriginal> obj = await beingValidated;
            return obj.SkipIfAlreadyInvalid(setTo);
        }

        /// <inheritdoc cref="IBeingValidated{TValidated,TOriginal}.ForceSkipIf" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> ForceSkipIf<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated, Predicate<TValidated> predicate)
        {
            IBeingValidated<TValidated, TOriginal> obj = await beingValidated;
            return obj.ForceSkipIf(predicate);
        }

        /// <inheritdoc cref="IBeingValidated{TValidated,TOriginal}.StopForceSkipping" />
        public static async Task<IBeingValidated<TValidated, TOriginal>> StopForceSkipping<TValidated, TOriginal>(
            this Task<IBeingValidated<TValidated, TOriginal>> beingValidated)
        {
            IBeingValidated<TValidated, TOriginal> obj = await beingValidated;
            return obj.StopForceSkipping();
        }
    }
}
using System;
using System.Threading.Tasks;

namespace BeingValidated
{
    /// <summary>
    ///     A wrapper class for validating an object. <br />
    ///     It's recommended that you use <see cref="BeingValidatedHelperMethods.StartValidate{T}" /> to get this.
    /// </summary>
    /// <typeparam name="T">Generic type for object to be validated</typeparam>
    public class BeingValidated<T> : IBeingValidated<T, T>
    {
        private readonly T _target;
        private bool _forceSkip;
        private bool _isInvalid;
        private bool _skipIfInvalid;

        /// <summary>
        ///     It's recommended that you use <see cref="BeingValidatedHelperMethods.StartValidate{T}" /> to get this.
        /// </summary>
        public BeingValidated(T target, bool skipIfInvalid = false)
        {
            _target = target;
            _skipIfInvalid = skipIfInvalid;
            _forceSkip = false;
        }

        /// <inheritdoc />
        public bool IsValid()
        {
            return !_isInvalid;
        }

        /// <inheritdoc />
        public IBeingValidated<T, T> Validate(Func<T, bool> validation, Action<T> onFail = null,
            Action<T, Exception> onException = null)
        {
            if (CanSkip()) return this;

            try
            {
                if (!validation.Invoke(_target)) DoWhenFail(onFail);
            }
            catch (Exception e)
            {
                DoWhenException(onException, e);
            }

            return this;
        }

        /// <inheritdoc />
        public IBeingValidated<T, T> Validate(Action<T> validation, Action<T, Exception> onException = null)
        {
            if (CanSkip()) return this;

            try
            {
                validation.Invoke(_target);
            }
            catch (Exception e)
            {
                DoWhenException(onException, e);
            }

            return this;
        }

        /// <inheritdoc />
        public async Task<IBeingValidated<T, T>> ValidateAsync(Func<T, Task<bool>> validation, Action<T> onFail = null,
            Action<T, Exception> onException = null)
        {
            if (CanSkip()) return this;

            try
            {
                if (!await validation.Invoke(_target)) DoWhenFail(onFail);
            }
            catch (Exception e)
            {
                DoWhenException(onException, e);
            }

            return this;
        }

        /// <inheritdoc />
        public async Task<IBeingValidated<T, T>> ValidateAsync(Func<T, Task> validation,
            Action<T, Exception> onException = null)
        {
            if (CanSkip()) return this;

            try
            {
                await validation.Invoke(_target);
            }
            catch (Exception e)
            {
                DoWhenException(onException, e);
            }

            return this;
        }

        /// <inheritdoc />
        public IBeingValidated<T, T> SkipIfAlreadyInvalid(bool setTo = true)
        {
            _skipIfInvalid = setTo;
            return this;
        }

        /// <inheritdoc />
        public IBeingValidated<T, T> ForceSkipIf(Predicate<T> predicate)
        {
            if (predicate.Invoke(_target))
                _forceSkip = true;
            return this;
        }

        /// <inheritdoc />
        public IBeingValidated<T, T> StopForceSkipping()
        {
            _forceSkip = false;
            return this;
        }

        private void DoWhenException(Action<T, Exception> onException, Exception e)
        {
            _isInvalid = true;

            if (onException == null) throw e;
            onException.Invoke(_target, e);
        }

        private void DoWhenFail(Action<T> onFail)
        {
            onFail?.Invoke(_target);
            _isInvalid = true;
        }

        private bool CanSkip()
        {
            return _forceSkip || _skipIfInvalid && _isInvalid;
        }
    }
}
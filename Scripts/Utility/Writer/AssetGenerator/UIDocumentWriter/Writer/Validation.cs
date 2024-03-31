using System;

namespace UIElements
{
    public class Validation<T>
    {
        private bool isValidated;
        private readonly T item;
        
        public Validation(T item, Func<bool> predicate)
        {
            this.item = item;
            isValidated = predicate();
        }
        
        public Validation<T> And(Func<bool> predicate)
        {
            if (item == null) return this;
            if (!isValidated) return this;
            
            isValidated &= predicate();
            return this;
        }
        
        public Validation<T> Or(Func<bool> predicate)
        {
            if (item == null) return this;
            if (isValidated) return this;
            
            isValidated |= predicate();
            return this;
        }
        
        public Validation<T> TrueThen(Action<T> action)
        {
            if (item == null) return this;
            if (!isValidated) return this;
            
            action(item);
            return this;
        }
        
        public Validation<T> FalseThen(Action<T> action)
        {
            if (item == null) return this;
            if (isValidated) return this;
            
            action(item);
            return this;
        }

        public Validation<T> TrueThen(Action action)
        { 
            if (item == null) return this;
            if (!isValidated) return this;
            
            action();
            return this;
        }
        
        public Validation<T> FalseThen(Action action)
        {
            if (item == null) return this;
            if (isValidated) return this;
            
            action();
            return this;
        }
        public T GetBack()
        {
            return item;
        }
    }
}
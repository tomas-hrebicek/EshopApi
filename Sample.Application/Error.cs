using System.Runtime.CompilerServices;

namespace Sample.Application
{
    public abstract class Error : Dictionary<string, object>
    {
        protected TValue Get<TValue>([CallerMemberName] string name = null)
        {
            return this.TryGetValue(name, out object value) ? (TValue)value : default;
        }

        protected void Set(object value, [CallerMemberName] string name = null)
        {
            if (value is null)
            {
                this.Remove(name);
            }
            else
            {
                if (this.ContainsKey(name))
                {
                    this[name] = value;
                }
                else
                {
                    this.Add(name, value);
                }
            }
        }
    }

    public class ForbiddenOperationError : Error
    {
        public ForbiddenOperationError(string description)
        {
            this.Description = description;
        }

        public string Description
        {
            get => Get<string>();
            set => Set(value);
        }
    }

    public class AlreadyExistsError : Error
    { }

    public class AlreadyExistsError<TItem> : AlreadyExistsError
    {
        public AlreadyExistsError(TItem item)
            : base()
        {
            if (item is not null)
            {
                this.Item = item;
            }
        }

        public TItem Item
        {
            get => Get<TItem>();
            set => Set(value);
        }
    }

    public class NotFoundError : Error
    {
        public NotFoundError()
            : base()
        { }

        public NotFoundError(string description)
            : base()
        {
            this.Description = description;
        }

        public string Description
        {
            get => Get<string>();
            set => Set(value);
        }
    }

    public class SecurityError : Error
    {
        public SecurityError(string description)
            : base()
        {
            this.Description = description;
        }

        public string Description
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}
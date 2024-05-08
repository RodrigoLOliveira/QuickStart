namespace Template.Client.Components.DynamicFormComponent
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DropDownOptionsAttribute : Attribute
    {
        public Type OptionsType { get; }

        public DropDownOptionsAttribute(Type optionsType)
        {
            if (!optionsType.IsValueType || optionsType.IsPrimitive || optionsType.IsEnum)
                throw new ArgumentException("DropDownOptionsAttribute is only valid on structs with static fields", nameof(optionsType));

            OptionsType = optionsType;
        }
    }
}

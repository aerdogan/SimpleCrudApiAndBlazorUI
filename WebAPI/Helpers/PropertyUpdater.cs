namespace WebAPI.Helpers
{
    public static class PropertyUpdater
    {
        /// <summary>
        /// Kaynağın null olmayan property'lerini hedef objeye kopyalar
        /// </summary>
        public static void UpdateChangedProps<TSource, TDestination>(
            TSource source,
            TDestination destination,
            params string[] ignoreProperties)
        {
            var sourceProps = typeof(TSource).GetProperties();
            var destProps = typeof(TDestination).GetProperties();

            foreach (var prop in sourceProps)
            {
                if (ignoreProperties.Contains(prop.Name))
                    continue;

                var newValue = prop.GetValue(source);
                if (newValue != null)
                {
                    var destProp = destProps.FirstOrDefault(p => p.Name == prop.Name && p.CanWrite);
                    if (destProp != null && newValue != destProp)
                    {
                        destProp.SetValue(destination, newValue);
                    }
                }
            }
        }
    }

}

namespace FeatureFlagAPI.Model
{
    public class FeatureFlag
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        public string Description { get; set; }

        public bool IsEnabled { get; set; }
    }
}

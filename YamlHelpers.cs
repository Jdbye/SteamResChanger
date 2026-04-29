using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SteamResChanger
{
    public static class YamlHelpers
    {
        public static T? LoadYaml<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(string path)
        {
            var fpath = Path.GetFullPath(path);

            if (File.Exists(fpath))
            {
                try
                {
                    var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .IgnoreUnmatchedProperties()
                    .Build();

                    var instance = deserializer.Deserialize<T>(File.ReadAllText(fpath));
                    WriteYaml<T>(path, instance); // Rewrite it to add the new config options

                    return instance;
                }
                catch { }
            }

            return default;
        }

        public static T CreateYaml<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(string path) where T : new()
        {
            var fpath = Path.GetFullPath(path);
            T instance = new();
            WriteYaml(path, instance);
            return instance;
        }

        public static bool WriteYaml<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(string path, T instance)
        {
            var fpath = Path.GetFullPath(path);

            try
            {
                var serializer = new SerializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

                var yaml = serializer.Serialize(instance);
                File.WriteAllText(fpath, yaml);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

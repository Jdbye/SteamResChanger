using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SteamResChanger
{
    public static class YamlHelpers
    {
        public static T? LoadYaml<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(string path)
        {
            var fpath = Path.IsPathRooted(path) ? path : Path.Combine(Path.GetDirectoryName(Environment.ProcessPath)!, path);

            if (File.Exists(fpath))
            {
                try
                {
                    var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .IgnoreUnmatchedProperties()
                    .Build();

                    var instance = deserializer.Deserialize<T>(File.ReadAllText(fpath));
                    WriteYaml<T>(fpath, instance); // Rewrite it to add any new config options

                    return instance;
                }
                catch { }
            }

            return default;
        }

        public static T CreateYaml<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(string path) where T : new()
        {
            var fpath = Path.IsPathRooted(path) ? path : Path.Combine(Path.GetDirectoryName(Environment.ProcessPath)!, path);

            T instance = new();
            WriteYaml(fpath, instance);
            return instance;
        }

        public static T CreateYaml<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(string path, T defaults) where T : new()
        {
            var fpath = Path.IsPathRooted(path) ? path : Path.Combine(Path.GetDirectoryName(Environment.ProcessPath)!, path);

            WriteYaml(fpath, defaults);
            return defaults;
        }

        public static bool WriteYaml<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(string path, T instance)
        {
            var fpath = Path.IsPathRooted(path) ? path : Path.Combine(Path.GetDirectoryName(Environment.ProcessPath)!, path);

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

// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System.IO;
using System.Text;

namespace MyHalp.Editor.MyPreloader
{
    internal class MyPreloaderCodeGen
    {
        /// <summary>
        /// Generates code for selected assets and puts the script at given path.
        /// </summary>
        /// <param name="assets">The assets.</param>
        /// <param name="outputScript">The output script.</param>
        public static void GenerateFor(MyPreloaderSettings.Asset[] assets, string outputScript)
        {
            const string newLine = "\n";
            const string getset = "{ get; private set; }";

            var builder =  new StringBuilder();
            builder.Append("using UnityEngine;" + newLine +
                           "using UnityEngine.Audio;" + newLine +
                           "using System;" + newLine +
                           "using System.Collections;" + newLine +
                           newLine +
                           "namespace MyHalp {" + newLine +
                           newLine +
                           "\tpublic class MyAssets {" + newLine + newLine);

            // add all assets variables
            foreach (var asset in assets)
            {
                if (!string.IsNullOrEmpty(asset.Define))
                    builder.Append($"#if {asset.Define}" + newLine);

                var type = asset.AssetType.ToString().Split('.');
                var typeName = type[type.Length - 1];
                var arrayd = asset.IsArray ? "[]" : "";

                var assetstring = $"\t\tpublic static {typeName}{arrayd} {asset.FriendlyName} {getset} {newLine}" ;
                builder.Append(assetstring);

                if (!string.IsNullOrEmpty(asset.Define))
                    builder.Append("#endif" + newLine);
            }
            
            // add preloading method
            builder.Append(newLine);
            builder.Append(
                "\t\t/// <summary> "+ newLine +
                "\t\t/// Preloads all assets, this should be called only once, unless you call Unload." + newLine +
                "\t\t/// </summary>" + newLine +
                "\t\tpublic static void Init(Action onLoad = null, Action<string> onError = null) { " + newLine);

            builder.Append("\t\t\tIsLoaded = false;" + newLine);
            builder.Append("\t\t\tMyDispatcher.Handle.StartCoroutine(Loader(onLoad, onError));" + newLine);

            builder.Append("\t\t}" + newLine);

            // add loader enumerator
            builder.Append(newLine);
            builder.Append("\t\tprivate static IEnumerator Loader(Action onLoad, Action<string> onError) { " + newLine);
            builder.Append("\t\t\tResourceRequest request;" + newLine);
            foreach (var asset in assets)
            {
                if (!string.IsNullOrEmpty(asset.Define))
                    builder.Append($"#if {asset.Define}" + newLine);

                var type = asset.AssetType.ToString().Split('.');
                var typeName = type[type.Length - 1];

                if (asset.IsArray)
                {
                    builder.Append(newLine);
                    builder.Append($"\t\t\t{asset.FriendlyName} = new {typeName}[{asset.ResourcePath.Length}];" + newLine);

                    var i = 0;
                    foreach (var path in asset.ResourcePath)
                    {
                        var assetstring = $"\t\t\trequest = Resources.LoadAsync<{typeName}>(\"{path}\");" + newLine;

                        builder.Append(newLine);
                        builder.Append(assetstring);

                        builder.Append("\t\t\tyield return request;" + newLine);
                        builder.Append($"\t\t\tif(request.asset == null) onError(\"Failed to load asset: {asset.FriendlyName}\"); " + newLine);
                        builder.Append($"\t\t\t{asset.FriendlyName}[{i}] = request.asset as {typeName};" + newLine);
                        i++;
                    }
                }
                else
                {
                    var assetstring = $"\t\t\trequest = Resources.LoadAsync<{typeName}>(\"{asset.ResourcePath[0]}\");" + newLine;

                    builder.Append(newLine);
                    builder.Append(assetstring);

                    builder.Append("\t\t\tyield return request;" + newLine);
                    builder.Append($"\t\t\tif(request.asset == null) onError(\"Failed to load asset: {asset.FriendlyName}\"); " + newLine);
                    builder.Append($"\t\t\t{asset.FriendlyName} = request.asset as {typeName};" + newLine);
                }

                if (!string.IsNullOrEmpty(asset.Define))
                    builder.Append("#endif" + newLine);
            }
            builder.Append(newLine);
            builder.Append("\t\t\tIsLoaded = true;" + newLine);
            builder.Append("\t\t\tif(onLoad != null)onLoad();" + newLine);
            builder.Append("\t\t}" + newLine);
            builder.Append(newLine);

            // TODO: add Unload method

            // add IsLoaded
            builder.Append("\t\t" + "/// <summary>" + newLine +
                           "\t\t" + "/// Are the assets loaded?" + newLine +
                           "\t\t" + "/// </summary>" + newLine +
                           "\t\tpublic static bool IsLoaded { get; private set; }" + newLine);

            builder.Append("\t}" + newLine+
                           "}" + newLine);

            File.WriteAllText(outputScript, builder.ToString());
            
        }
    }
}

using Newtonsoft.Json;
using ProjectVersionIncrementer;

//args = new [] { @"D:\Temp\ExampleInput.json", "feature" }; 

if (!args.Length.Equals(2))
{
    throw new Exception(@"Two arguments are expected: an input file path and a release type, e.g. 'D:\Temp\ExampleInput.json' 'bugfix'");
}

ProductInfo productInfo = GetProductInfo(args[0]);
ReleaseType releaseType = GetReleaseType(args[1]);
var incrementer = new VersionIncrementerService();

switch (releaseType)
{
    case ReleaseType.Feature:
        productInfo.Version = incrementer.FeatureRelease(productInfo.Version);
        break;
    case ReleaseType.BugFix:
        productInfo.Version = incrementer.BugFixRelease(productInfo.Version);
        break;
}

File.WriteAllText(args[0], JsonConvert.SerializeObject(productInfo));

return;

static ProductInfo GetProductInfo(string inputPath)
{
    ProductInfo productInfo;
    try
    {
        using var inputFile = File.OpenRead(inputPath);
        using var reader = new StreamReader(inputFile);
        var fileContents = reader.ReadToEnd();
        productInfo = JsonConvert.DeserializeObject<ProductInfo>(fileContents);
        if(productInfo is null )
        {
            throw new ArgumentException("The input file could not be converted to a 'ProductInfo' object. Please check the contents");
        }
    }
    catch (Exception)
    {
        throw new ArgumentException("The first argument must be an input path for a .json file representing 'ProductInfo'");
    }
    return productInfo;
}

static ReleaseType GetReleaseType(string inputReleaseType)
{
    var enumParseSuccess = Enum.TryParse<ReleaseType>(inputReleaseType, ignoreCase: true, out var releaseType);
    if (!enumParseSuccess)
    {
        throw new ArgumentException("The second argument must be a release type of either 'feature' or 'bugfix'");
    }
    return releaseType;
}
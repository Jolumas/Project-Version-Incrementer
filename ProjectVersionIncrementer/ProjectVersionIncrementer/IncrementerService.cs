namespace ProjectVersionIncrementer
{
    public class VersionIncrementerService
    {
        //1.0.<major>.<minor> eg 1.0.20.3
        public string FeatureRelease(string currentVersion)
        {
            return IncrementVersion(currentVersion, releaseNumberPlacement: 2);
        }

        public string BugFixRelease(string currentVersion)
        {
            return IncrementVersion(currentVersion, releaseNumberPlacement: 3);
        }

        private string IncrementVersion(string currentVersion, int releaseNumberPlacement)
        {
            if (!VersionPassesValidation(currentVersion))
            {
                throw new Exception("Version number field from input file could not pass validation. Must be a collection of 4 numbers separated by full stops.");
            }
            var versionNumbers = currentVersion.Split('.').Select(vn => int.Parse(vn)).ToArray();
            var versionToUpdate = versionNumbers[releaseNumberPlacement];
            versionNumbers[releaseNumberPlacement] = versionToUpdate + 1;
            currentVersion = string.Join(".", versionNumbers);
            return currentVersion;
        }

        public bool VersionPassesValidation(string version)
        {
            var versionNumbers = version.Split('.');
            if(versionNumbers.Length.Equals(4) && versionNumbers.All(vn => int.TryParse(vn, out int result)))
            {
                return true;
            }
            return false;
        }
    }
}

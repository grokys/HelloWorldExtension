using Microsoft.VisualStudio.Package;

namespace HelloWorldExtension;

internal class HelloWorldLanguageFactory : LanguageBase
{
    public HelloWorldLanguageFactory(object site) : base(site) { }

    public override string Name => "Hello World";
    public override string[] FileExtensions => [ ".hello" ];

    public override void SetDefaultPreferences(LanguagePreferences preferences)
    {
    }
}

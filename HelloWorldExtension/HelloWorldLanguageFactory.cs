using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Package;

namespace HelloWorldExtension;

[Guid("6D7CB8E8-2274-4ED0-B98E-7E566B1D3D82")]
internal class HelloWorldLanguageFactory : LanguageBase
{
    public HelloWorldLanguageFactory(object site) : base(site) { }

    public override string Name => "Hello World";
    public override string[] FileExtensions => [ ".hello" ];

    public override void SetDefaultPreferences(LanguagePreferences preferences)
    {
    }
}

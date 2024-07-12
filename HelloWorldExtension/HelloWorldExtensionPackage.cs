global using System;
global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell.Interop;

namespace HelloWorldExtension;

[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
[ProvideEditorExtension(typeof(HelloWorldLanguageFactory), ".hello", 50)]
[ProvideLanguageService(typeof(HelloWorldLanguageFactory), "HelloWorld", 0)]
[ProvideMenuResource("Menus.ctmenu", 1)]
[Guid(PackageGuids.HelloWorldExtensionString)]
public sealed class HelloWorldExtensionPackage : ToolkitPackage
{
    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
        var languageFactory = new HelloWorldLanguageFactory(this);
        RegisterEditorFactory(languageFactory);
        await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
    }
}
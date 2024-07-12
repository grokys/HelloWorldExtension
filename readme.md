# HelloWorldExtension

Create a new project using "VSIX Project (Community)"

Add HelloWorldLanguageFactory.cs:

```csharp
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
```

Run the experimental instance: put a breakpoint on `HelloWorldExtensionPackage.InitializeAsync` - the package isn't loaded.

We need to add some attributes to the package class:

```csharp
[ProvideLanguageService(typeof(HelloWorldLanguageFactory), "HelloWorld", 0)]
```

This registers the language service with the editor. The language name is ?What is this for? and the 0 refers to a resource ID for the language service. This is not used in this example.

```csharp
[ProvideEditorExtension(typeof(HelloWorldLanguageFactory), ".hello", 50)]
```

This associates the `.hello` a file extension with our language factory. The `50` is the priority of the editor. The higher the number, the higher the priority in case of multiple editors for the same file extension.

Now when opening a `.hello` file, the package will be loaded, but a breakpoint on the `HelloWorldLanguageFactory` constructor will not be hit, meaning that the language service is still not being created. We need to register the language service again in the `InitializeAsync` method of the package:

```csharp
    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
        // highlight-start
        var languageFactory = new HelloWorldLanguageFactory(this);
        RegisterEditorFactory(languageFactory);
        // highlight-end
        await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
    }
```

After this change, the breakpoint in the `HelloWorldLanguageFactory` constructor will be hit, meaning that the language service is being created.
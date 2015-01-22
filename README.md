# CommandPipeline
[![Build status](https://ci.appveyor.com/api/projects/status/hc04m6q1des9c1sh?svg=true)](https://ci.appveyor.com/project/forcewake/commandpipeline)

Pipeline Pattern is helpful in dividing the problem into smaller reusable code components. This is a simple yet powerful structural pattern to organize a complex logic into smaller reusable components, which can be added/removed/modified independently.

## Forces for the Pattern

* The logic is complex.
* The simplicity and readability of the code is critical.
* The logic can be divided into multiple modules.
* The modules are potentially reusable for different usecases.

## Installation

### CommandPipeline

> Common solution for pipeline processing

You can install [CommandPipeline via NuGet](https://www.nuget.org/packages/CommandPipeline):

```sh
Install-Package CommandPipeline
```

### CommandPipeline.Ninject

> Install it if you want to use Ninject as dependencies resolver for commands

You can install [CommandPipeline.Ninject via NuGet](https://www.nuget.org/packages/CommandPipeline.Ninject):

```sh
Install-Package CommandPipeline.Ninject
```

## Example

The complete ready-to-use example is available in the [CommandPipeline.Example project](src/CommandPipeline.Example)

## Usage

### Create command
```cs
public class ExtractHtmlFromUrl : NonParameterizedCommand
{
    public OutArgument<HtmlDocument> HtmlPage { get; set; }

    public InArgument<Request> Request { get; set; }

    public IWebPageDownloader Downloader { get; private set; }

    public ExtractHtmlFromUrl(IWebPageDownloader downloader)
    {
        this.Downloader = downloader;
    }

    public override void Execute()
    {
        var request = Ensure.That(this.Request, "Request").Is(p => p.IsNotNull());

        var webPage = this.Downloader.DownloadWebPage(request.Url);

        var htmlPage = new HtmlDocument { Content = webPage };

        this.HtmlPage.Set(htmlPage);
    }
}
```

### Create container
```cs
var container = new NinjectContainer();

container.Bind<IWebPageDownloader>().To<WebPageDownloader>();
container.Bind<IMarkdownConveter>().To<MarkdownConveter>();
container.Bind<IDataContainerService>().To<DataContainerServiceFake>();
```

### Create and update context with your data (optional)
```cs
var context = new ParametersContext<ICommand>();

context.Set<ExtractHtmlFromUrl, Request>(x => x.Request, new Request { Url = link });
```

### Create pipeline
```cs
builder = new PipelineBuilder()

var pipeline = builder.Create(container, context)
    .Register<ExtractHtmlFromUrl>()
    .Register<ConvertHtmlToMardown>()
    .Register<UploadToDataContainer>();
```

### Handle it
```cs
pipeline.Handle();
```

using SearchService.App.Abstraction;
using SearchService.App.Common;
using SearchService.App.UseCases.CreateDirectory;
using SearchService.App.UseCases.DropDirectory;
using SearchService.App.UseCases.DropIndex;
using SearchService.App.UseCases.GetDirectory;
using SearchService.App.UseCases.Index;
using SearchService.App.UseCases.Reinforce;
using SearchService.App.UseCases.Search;
using SearchServiceAPI.Modules.Directory.Presenter;
using SearchServiceAPI.Modules.Index.Presenter;

namespace SearchServiceAPI.Extensions;

internal static class SearchServiceExtensions
{
    /// <summary>
    /// Register search related services and use cases
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static IServiceCollection AddSearchServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITokenComparer, DefaultComparer>();
        serviceCollection.AddScoped<IComparerFactory, ComparerFactory>();
        serviceCollection.AddScoped<ISymbolProducer, SymbolProducer.SymbolProducer>();
        
        // create directory handler
        serviceCollection.AddScoped<ICreateDirectoryHandler, CreateDirectoryHandler>();
        serviceCollection.AddScoped<CreateDirectoryOutput>();
        serviceCollection.AddScoped<ICreateDirectoryOutput, CreateDirectoryPresenter>();

        // drop directory handler 
        serviceCollection.AddScoped<IDropDirectoryHandler, DropDirectoryHandler>();
        serviceCollection.AddScoped<IDropDirectoryOutput, DropDirectoryPresenter>(); 
        
        // Get Directory
        serviceCollection.AddScoped<IGetDirectoryUseCase, GetDirectoryHandler>();
        serviceCollection.AddScoped<IGetDirectoryOutput, GetDirectoryPresenter>();
        serviceCollection.AddScoped<GetDirectoryPresenter>();

        // DropIndex 
        serviceCollection.AddScoped<IDropIndexHandler, DropIndexHandler>();
        serviceCollection.AddScoped<IDropIndexOutput, DropIndexPresenter>(); 
        
        // index 
        serviceCollection.AddScoped<ICreateIndexHandler, CreateIndexHandler>();
        serviceCollection.AddScoped<ICreateIndexOutput, CreateIndexPresenter>(); 
        
        // Reinforce 
        serviceCollection.AddScoped<IReinforceHandler, ReinforceHandler>();
        serviceCollection.AddScoped<IReinforceOutput, ReinforcePresenter>(); 
        
        // Search
        serviceCollection.AddScoped<ISearchHandler, SearchHandler>();
        serviceCollection.AddScoped<ISearchOutput, SearchPresenter>();
        serviceCollection.AddScoped<ISearchStrategyFactory, SearchStrategyFactory>(); 
        
        return serviceCollection;
    }
}
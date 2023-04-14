using INF36307.TP3.Models;
using INF36307.TP3.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace INF36307.TP3.Repositories;

public class EtudiantCache : IEtudiantRepository
{
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;
    
    public EtudiantCache(IMemoryCache cache)
    {
        _cache = cache;
        _cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(120))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(180))
            .SetPriority(CacheItemPriority.Normal)
            .SetSize(2048);
    }
    
    public Etudiant First(string username)
    {
        _cache.TryGetValue(username, out Etudiant etudiant);
        
        return etudiant;
    }

    public IEnumerable<Etudiant> All()
    {
        throw new NotImplementedException();
    }

    public Etudiant Add(Etudiant etudiant)
    {
        return _cache.Set(etudiant.Nom, etudiant, _cacheEntryOptions);
    }

    public void AddRange(IEnumerable<Etudiant> etudiants)
    {
        foreach (Etudiant etudiant in etudiants)
        {
            Add(etudiant);
        }
    }
}
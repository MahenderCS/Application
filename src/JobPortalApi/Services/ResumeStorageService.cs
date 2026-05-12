namespace JobPortalApi.Services;

public sealed class ResumeStorageService
{
    private readonly IWebHostEnvironment _environment;

    public ResumeStorageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var uploadsRoot = Path.Combine(_environment.ContentRootPath, "Uploads");
        Directory.CreateDirectory(uploadsRoot);

        var safeFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var destinationPath = Path.Combine(uploadsRoot, safeFileName);

        await using var stream = File.Create(destinationPath);
        await file.CopyToAsync(stream, cancellationToken);

        return safeFileName;
    }
}

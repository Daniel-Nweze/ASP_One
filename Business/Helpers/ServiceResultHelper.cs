using Business.Models;
using Data.Models;

namespace Business.Helpers;

public static class ServiceResultHelper
{
    /*HandleRepoResult metoden genererad med CHatGPT för DRY syfte*/
    internal static ServiceResult<T> HandleRepoResult<T>(RepositoryResult<T> repoResult)
    {
        if (!repoResult.Succeeded)
            return ResultFailure<T>(repoResult.StatusCode, repoResult.Error ?? "Databasfel.");

        return ResultSuccess(repoResult.StatusCode, repoResult.Data!);
    }
    public static ServiceResult<T> ResultFailure<T>(int statusCode, string error) =>
        new()
        {
            Succeeded = false,
            StatusCode = statusCode,
            Error = error
        };
    public static ServiceResult<T> ResultSuccess<T>(int statusCode, T data) =>
        new()
        {
            Succeeded = true,
            StatusCode = statusCode,
            Data = data
        };
}

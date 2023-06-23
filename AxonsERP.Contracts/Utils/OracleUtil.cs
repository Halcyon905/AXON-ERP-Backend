using Microsoft.Extensions.Configuration;

namespace AxonsERP.Contracts.Utils
{
    public static class OracleUtil
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            string? DockerSecretPass = !string.IsNullOrEmpty(configuration["orapass"]) ? configuration[configuration["orapass"]] : null;
            if (string.IsNullOrEmpty(DockerSecretPass))
            {
                return string.Format("User Id={0};Password={1};", configuration["UID"], configuration["PSW"]) + configuration["ConnectionStrings"];
            }
            else
            {
                return string.Format("User Id={0};Password={1};", configuration["UID"], DockerSecretPass) + configuration["ConnectionStrings"];
            }
        }

        public static string GetConnectionStringCenter(IConfiguration configuration)
        {
            string? DockerSecretPass = !string.IsNullOrEmpty(configuration["orapass"]) ? configuration[configuration["orapass"]] : null;
            if (string.IsNullOrEmpty(DockerSecretPass))
            {
                return string.Format("User Id={0};Password={1};", configuration["UIDCenter"], configuration["PSWCenter"]) + configuration["ConnectionStringsCenter"];
            }
            else
            {
                return string.Format("User Id={0};Password={1};", configuration["UIDCenter"], DockerSecretPass) + configuration["ConnectionStringsCenter"];
            }
        }
    }
}

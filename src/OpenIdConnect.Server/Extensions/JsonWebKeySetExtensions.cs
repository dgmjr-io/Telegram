using Microsoft.IdentityModel.Tokens;

namespace Telegram.OpenIdConnect.Extensions;

public static class JsonWebKeySetExtensions
{
    private static readonly Jso Jso = new()
{
    PropertyNamingPolicy = JNaming.CamelCase,
    WriteIndented = true
};

    public static string ToJson(this JsonWebKeySet jsonWebKeySet) =>
        Serialize(
            new
            {
                keys = jsonWebKeySet.Keys
                    .Select(
                        key =>
                            new
                            {
                                kty = key.Kty,
                                use = key.Use,
                                kid = key.Kid,
                                // x5t = key.X5t,
                                e = key.E,
                                n = key.N,
                                // x5c = key.X5c,
                                // alg = key.Alg
                            }
                    )
                    .ToArray()
            },
            Jso
        );
}

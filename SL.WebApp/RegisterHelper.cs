using SL.Controllers.Controllers;
using SL.Repositories.Impl;
using SL.Repositories.Interfaces;
using SL.Services.Impl;
using SL.Services.Interfaces;
using SL.WebApp.Data;

namespace SL.WebApp
{
    public static class RegisterHelper
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<ISongService, SongService>();
            builder.Services.AddScoped<ISpotifyOAuthService, SpotifyOAuthService>();

			return builder;
        }

        public static WebApplicationBuilder RegisterControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<SongController>();
            builder.Services.AddScoped<SpotifyOAuthController>();

			return builder;
        }

        public static WebApplicationBuilder RegisterRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISongRepository, SongRepository>();

            return builder;
        }
    }
}

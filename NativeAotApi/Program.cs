using System.Device.Gpio;
using System.Text.Json.Serialization;

namespace NativeAotApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateSlimBuilder(args);

            builder.Services.AddSingleton<GpioController>();

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
            });

            var app = builder.Build();

            var apibui = app.MapGroup("/led");


            apibui.MapGet("/on", (HttpContext hc, GpioController gp) =>
            {
                if (!gp.IsPinOpen(20))
                {
                    gp.OpenPin(20, PinMode.Output);
                }

                gp.Write(20, PinValue.High);

                Console.WriteLine("pin on");

                return Results.Ok(new PinResult("on"));
            });

            apibui.MapGet("/off", (HttpContext hc, GpioController gp) =>
            {
                if (!gp.IsPinOpen(20))
                {
                    gp.OpenPin(20, PinMode.Output);
                }

                gp.Write(20, PinValue.Low);

                Console.WriteLine("pin off");

                return Results.Ok(new PinResult("off"));
            });

            app.Run();
        }
        
    }
    public record PinResult(string status);


        [JsonSerializable(typeof(PinResult))]
        public partial class AppJsonSerializerContext : JsonSerializerContext;
}

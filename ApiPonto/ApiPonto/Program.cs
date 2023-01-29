namespace ApiPonto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Desabilita valida��o de model nativa do Net.6 api
            builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

            // em produ��o, retirar essas pol�ticas de Cors.
            builder.Services.AddCors(policyBuilder =>
                       policyBuilder.AddDefaultPolicy(policy =>
                            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
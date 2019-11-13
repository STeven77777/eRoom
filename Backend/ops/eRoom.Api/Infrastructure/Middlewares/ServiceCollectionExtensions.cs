using eRoom.CoreLib.DAL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRoom.Api.Infrastructure.Middlewares
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IRoomsDAL, RoomsDAL>();
            services.AddScoped<IPeoplesDAL, PeoplesDAL>();
            services.AddScoped<IWaterUsingsDAL, WaterUsingsDAL>();
            services.AddScoped<IElectricUsingsDAL, ElectricUsingsDAL>();
            services.AddScoped<IInvoicesDAL, InvoicesDAL>();
            services.AddScoped<IInvoiceDetailsDAL, InvoiceDetailsDAL>();
            services.AddScoped<IMotorbikesDAL, MotorbikesDAL>();
            services.AddScoped<IMotorTypesDAL, MotorTypesDAL>();
            services.AddScoped<IReceiptsDAL, ReceiptsDAL>();
            services.AddScoped<IRoomDetailsDAL, RoomDetailsDAL>();
            services.AddScoped<IRoomPeoplesDAL, RoomPeoplesDAL>();
            services.AddScoped<IRoomStatusDAL, RoomStatusDAL>();
            services.AddScoped<IRoomTypesDAL, RoomTypesDAL>();
            services.AddScoped<IServicesDAL, ServicesDAL>();
            services.AddScoped<IServiceTypesDAL, ServiceTypesDAL>();
        }
    }
}

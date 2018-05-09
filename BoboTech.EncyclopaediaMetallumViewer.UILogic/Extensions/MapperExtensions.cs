using BoboTech.EncyclopaediaMetallumViewer.UILogic.Services;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.Extensions
{
    public static class MapperExtensions
    {
        public static T To<T>(this object source) => MapperService.SingletonInstance.Mapper.Map<T>(source);

        public static TDestination To<TSource, TDestination>(this TSource source, TDestination destination) => MapperService.SingletonInstance.Mapper.Map(source, destination);
    }
}
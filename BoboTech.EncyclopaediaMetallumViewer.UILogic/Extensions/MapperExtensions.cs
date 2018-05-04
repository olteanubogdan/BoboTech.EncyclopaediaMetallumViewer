using BoboTech.EncyclopaediaMetallumViewer.UILogic.Services;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.Extensions
{
    public static class MapperExtensions
    {
        public static T To<T>(this object source) => MapperService.SingletonInstance.Mapper.Map<T>(source);
    }
}
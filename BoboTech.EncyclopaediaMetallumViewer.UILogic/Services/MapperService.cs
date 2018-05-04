using AutoMapper;
using BoboTech.EncyclopaediaMetallumViewer.Models.Api;
using BoboTech.EncyclopaediaMetallumViewer.UILogic.ViewModels;
using DevExpress.Mvvm.POCO;
using System;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.Services
{
    public class MapperService
    {
        #region Fields

        static readonly Lazy<MapperService> _lazyInstance = new Lazy<MapperService>(() => new MapperService());

        #endregion

        #region Properties

        public static MapperService SingletonInstance => _lazyInstance.Value;

        public IMapper Mapper { get; private set; }

        #endregion

        #region Constructor

        private MapperService() { }

        #endregion

        #region Public methods

        public void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Band, BandViewModel>()
                    .ConstructUsing(x => ViewModelSource.Create<BandViewModel>())
                    .ForMember(x => x.WindowTitle, opt => opt.Ignore())
                    .ReverseMap();
            });
            Mapper = config.CreateMapper();
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        #endregion
    }
}
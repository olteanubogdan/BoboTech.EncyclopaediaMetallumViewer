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

        Guid _instanceId = Guid.NewGuid();

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
                    .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id))
                    .ForAllOtherMembers(opt => opt.Ignore());
                cfg.CreateMap<SearchBandResponse, SearchBandViewModel>()
                    .ForMember(x => x.DonationMessage, opt => opt.MapFrom(y => y.Donation == null ? string.Empty : y.Donation.Message))
                    .ForMember(x => x.DonationUrl, opt => opt.MapFrom(y => y.Donation == null ? string.Empty : y.Donation.DonationUrl))
                    .ForMember(x => x.SearchStatus, opt => opt.MapFrom(y => y.Status))
                    .ForMember(x => x.SearchCode, opt => opt.MapFrom(y => y.Code))
                    .ForMember(x => x.SearchMessage, opt => opt.MapFrom(y => y.Message))
                    .ForMember(x => x.SearchHash, opt => opt.MapFrom(y => y.Hash))
                    .ForAllOtherMembers(opt => opt.Ignore());
            });
            Mapper = config.CreateMapper();
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public override string ToString() => $"{nameof(MapperService)} ({_instanceId:N})";

        #endregion
    }
}
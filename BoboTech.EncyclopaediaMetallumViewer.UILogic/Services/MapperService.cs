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
                    .ForMember(x => x.EMApiDonationMessage, opt => opt.MapFrom(y => y.Donation == null ? string.Empty : y.Donation.Message))
                    .ForMember(x => x.EMApiDonationUrl, opt => opt.MapFrom(y => y.Donation == null ? string.Empty : y.Donation.DonationUrl))
                    .ForMember(x => x.EMApiCallStatus, opt => opt.MapFrom(y => y.Status))
                    .ForMember(x => x.EMApiCallCode, opt => opt.MapFrom(y => y.Code))
                    .ForMember(x => x.EMApiCallMessage, opt => opt.MapFrom(y => y.Message))
                    .ForMember(x => x.EMApiCallHash, opt => opt.MapFrom(y => y.Hash))
                    .ForAllOtherMembers(opt => opt.Ignore());
                cfg.CreateMap<GetBandResponse, BandViewModel>()
                    .ForMember(x => x.EMApiDonationMessage, opt => opt.MapFrom(y => y.Donation == null ? string.Empty : y.Donation.Message))
                    .ForMember(x => x.EMApiDonationUrl, opt => opt.MapFrom(y => y.Donation == null ? string.Empty : y.Donation.DonationUrl))
                    .ForMember(x => x.EMApiCallStatus, opt => opt.MapFrom(y => y.Status))
                    .ForMember(x => x.EMApiCallCode, opt => opt.MapFrom(y => y.Code))
                    .ForMember(x => x.EMApiCallMessage, opt => opt.MapFrom(y => y.Message))
                    .ForMember(x => x.EMApiCallHash, opt => opt.MapFrom(y => y.Hash))
                    .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Data == null ? string.Empty : y.Data.BandName))
                    .ForMember(x => x.Logo, opt => opt.MapFrom(y => y.Data == null ? string.Empty : y.Data.Logo))
                    .ForMember(x => x.Photo, opt => opt.MapFrom(y => y.Data == null ? string.Empty : y.Data.Photo))
                    .ForMember(x => x.Bio, opt => opt.MapFrom(y => y.Data == null ? string.Empty : y.Data.Bio))
                    .ForMember(x => x.CountryOfOrigin, opt => opt.MapFrom(y => y.Data == null || y.Data.Details == null ? string.Empty : y.Data.Details.CountryOfOrigin))
                    .ForMember(x => x.Location, opt => opt.MapFrom(y => y.Data == null || y.Data.Details == null ? string.Empty : y.Data.Details.Location))
                    .ForMember(x => x.Status, opt => opt.MapFrom(y => y.Data == null || y.Data.Details == null ? string.Empty : y.Data.Details.Status))
                    .ForMember(x => x.FormedIn, opt => opt.MapFrom(y => y.Data == null || y.Data.Details == null ? string.Empty : y.Data.Details.FormedIn))
                    .ForMember(x => x.Genre, opt => opt.MapFrom(y => y.Data == null || y.Data.Details == null ? string.Empty : y.Data.Details.Genre))
                    .ForMember(x => x.LyricalThemes, opt => opt.MapFrom(y => y.Data == null || y.Data.Details == null ? string.Empty : y.Data.Details.LyricalThemes))
                    .ForMember(x => x.CurrentLabel, opt => opt.MapFrom(y => y.Data == null || y.Data.Details == null ? string.Empty : y.Data.Details.CurrentLabel))
                    .ForMember(x => x.YearsActive, opt => opt.MapFrom(y => y.Data == null || y.Data.Details == null ? string.Empty : y.Data.Details.YearsActive))
                    .ForAllOtherMembers(opt => opt.Ignore());
            });
            Mapper = config.CreateMapper();
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public override string ToString() => $"{nameof(MapperService)} ({_instanceId:N})";

        #endregion
    }
}
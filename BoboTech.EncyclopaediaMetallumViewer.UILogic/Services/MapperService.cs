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
                #region BaseViewModel

                cfg.CreateMap<BaseResponse, BaseViewModel>()
                    .ForMember(x => x.EMApiDonationMessage, opt => opt.MapFrom(y => y.Donation == null ? string.Empty : y.Donation.Message))
                    .ForMember(x => x.EMApiDonationUrl, opt => opt.MapFrom(y => y.Donation == null ? string.Empty : y.Donation.DonationUrl))
                    .ForMember(x => x.EMApiCallStatus, opt => opt.MapFrom(y => y.Status))
                    .ForMember(x => x.EMApiCallCode, opt => opt.MapFrom(y => y.Code))
                    .ForMember(x => x.EMApiCallMessage, opt => opt.MapFrom(y => y.Message))
                    .ForMember(x => x.EMApiCallHash, opt => opt.MapFrom(y => y.Hash))
                    .ForAllOtherMembers(opt => opt.Ignore());

                #endregion

                #region BandViewModel

                cfg.CreateMap<Band, BandViewModel>()
                    .ConstructUsing(x => ViewModelSource.Create<BandViewModel>())
                    .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id))
                    .ForAllOtherMembers(opt => opt.Ignore());

                cfg.CreateMap<GetBandData, BandViewModel>(MemberList.None);
                cfg.CreateMap<BandDetails, BandViewModel>(MemberList.None);

                #endregion

                #region AlbumViewModel

                cfg.CreateMap<Album, AlbumViewModel>()
                    .ConstructUsing(x => ViewModelSource.Create<AlbumViewModel>())
                    .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id))
                    .ForAllOtherMembers(opt => opt.Ignore());

                cfg.CreateMap<AlbumDetails, AlbumViewModel>(MemberList.None);

                #endregion
            });

            Mapper = config.CreateMapper();
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public override string ToString() => $"{nameof(MapperService)} ({_instanceId:N})";

        #endregion
    }
}
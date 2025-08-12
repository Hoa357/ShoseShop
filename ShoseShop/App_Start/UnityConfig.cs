using ShoesStore.Repositories;
using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.Repositories;
using System;
using System.Data.Entity;
using Unity;
using Unity.AspNet.Mvc; 
namespace ShoseShop
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        public static void RegisterTypes(IUnityContainer container)
        {
           
            container.RegisterType<DbContext, ShoesContext>(new PerRequestLifetimeManager());

           
            container.RegisterType<IPhieuMua, PhieuMuaRepo>();
            container.RegisterType<ISanPham, SanphamRepo>();
            container.RegisterType<IKhachHang, KhachhangRepo>();
            container.RegisterType<IChiTietSanPham, ChiTietSanphamRepo>();
            container.RegisterType<IKhuyenMai, KhuyenMaiRepo>();
            container.RegisterType<IMau, MauRepo>();
            container.RegisterType<ISize, SizeRepo>();
            container.RegisterType<ISanPhamSize, SanphamSizeRepo>();
            container.RegisterType<IAddressNoteBook, AddressNoteBookRepo>();
            container.RegisterType<IVoucher, VoucherRepo>();
            container.RegisterType<IPhuongthucthanhtoan, PhuongthucthanhtoanRepo>();
            container.RegisterType<IBinhLuan, BinhLuanRepository>();
        }
    }
}
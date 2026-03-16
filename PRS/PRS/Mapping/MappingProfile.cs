using AutoMapper;
using PRS.DTOs;
using PRS.Entities;
using System.Text.Json;

namespace PRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ── Product Mappings ──────────────────────────────────────────────

            // CreateProductDto → Product (entity)
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.SKU,
                    opt => opt.MapFrom(src => src.Sku.ToUpperInvariant()))
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Tags,
                    opt => opt.MapFrom(src =>
                        JsonSerializer.Serialize(src.Tags, (JsonSerializerOptions?)null)))
                .ForMember(dest => dest.Specifications,
                    opt => opt.MapFrom(src =>
                        JsonSerializer.Serialize(src.Specifications ?? new(),
                            (JsonSerializerOptions?)null)));

            // UpdateProductDto → Product (entity) — skip nulls so partial updates work
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Tags,
                    opt => opt.PreCondition(src => src.Tags != null))
                .ForMember(dest => dest.Tags,
                    opt => opt.MapFrom(src =>
                        JsonSerializer.Serialize(src.Tags, (JsonSerializerOptions?)null)))
                .ForMember(dest => dest.Specifications,
                    opt => opt.PreCondition(src => src.Specifications != null))
                .ForMember(dest => dest.Specifications,
                    opt => opt.MapFrom(src =>
                        JsonSerializer.Serialize(src.Specifications,
                            (JsonSerializerOptions?)null)))
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));

            // ── Category Mappings ─────────────────────────────────────────────

            CreateMap<CreateCategoryDto, Category>()
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(_ => true));

            CreateMap<UpdateCategoryDto, Category>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));

            // ── Order Mappings ────────────────────────────────────────────────

            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(_ => OrderStatus.Pending))
                .ForMember(dest => dest.PaymentStatus,
                    opt => opt.MapFrom(_ => PaymentStatus.Pending))
                .ForMember(dest => dest.Items,
                    opt => opt.Ignore());     // Populated manually in service
        }
    }
}

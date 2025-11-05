using PreferencesMicroservice.API.Entity;
using PreferencesMicroservice.API.GraphQLTypes;

namespace PreferencesMicroservice.API.Services
{
    public static class ProfileAddressAvailableAddressPreferenceMapper
    {
        public static ProfileAddressAvailablePreference Preference(ProfileAddressAvailablePreferenceEntity entity)
        {
            if (entity == null)
            {
                return null!;
            }

            return new ProfileAddressAvailablePreference
            {
                Id = entity.Id,
                houseNumber = entity.houseNumber,
                postalCode = entity.postalCode,
                street = entity.street,
                city = entity.city,
                country = entity.country,
                notAtHome = new NotAtHomePreference
                {
                    parcelLocker = entity.notAtHome.parcelLocker,
                    postNLPoint = entity.notAtHome.postNLPoint
                },
                deliveryMethod = entity.deliveryMethod
            };
        }
    }
}

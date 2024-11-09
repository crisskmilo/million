namespace Smartwyre.Domain.Services.Utilities
{
    using Newtonsoft.Json;
    using Smartwyre.Domain.Entities;
    using Smartwyre.Domain.Entities.Response;

    public static class HelperGeneric<T>
    {
        /// <summary>
        /// Casts to generic response.
        /// </summary>
        /// <param name="parentObject">The parent object.</param>
        /// <returns></returns>
        public static GenericResponse<T> CastToGenericResponseSerialize(object parentObject)
        {
            var serializedParent = JsonConvert.SerializeObject(parentObject);
            GenericResponse<T> serializedChild = JsonConvert.DeserializeObject<GenericResponse<T>>(serializedParent);
            return serializedChild;
        }

        public static GenericResponse<T> CastToGenericResponse(GeneralResponse parentObject)
        {
            GenericResponse<T> serializedChild = new GenericResponse<T>()
            {
                isSuccess = parentObject.isSuccess,
                result = parentObject.result,
                message = parentObject.message
            };
            return serializedChild;
        }

    }
}

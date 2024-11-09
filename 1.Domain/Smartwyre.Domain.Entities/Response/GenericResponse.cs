namespace Smartwyre.Domain.Entities.Response
{
    using Newtonsoft.Json;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericResponse<T>: GeneralResponse
    {
        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public T Entity { get; set; }
    }
}

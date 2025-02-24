﻿namespace Million.Domain.Entities.Enums
{
    public enum ErrorCodes : long
    {
        /// The invalid Code.
        /// </summary>
        TroubleCode = 900,
        ProductWithoutStock = 902,
        ErrorRequest = 903,
        ProductsNotAvailableForUser = 904,
        MaxProductsForPdf = 905
    }
}

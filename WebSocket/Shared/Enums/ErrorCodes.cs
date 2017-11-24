namespace Shared.Enums
{
    public static class ErrorCodes
    {
        public static string ERROR = "ERROR";
        public static string STATUS_OK = "OK";

        public enum ErrorEnums
        {
            EMPTY_POST_BODY = 101,
            EMPTY_REQUIRED_FIELD = 102,
            OBJECT_NOT_FOUND = 103,
            OBJECT_NOT_DELETED = 104,

            INCORRECT_SMS_CODE_OR_PHONE = 201,
            INCORRECT_LOGIN_OR_PASSWORD = 202,

            DEFAULT_MESSENGER_NOT_FOUND = 301,
            USER_NOT_AUTHORIZED = 302,
            USER_NOT_FOUND = 303,
            ORDER_CANT_CANCELED = 304,
            USER_ALREADY_SHARE = 305,
            USER_PROMO_EQUALS_ACTIVATE_PROMO = 306,
            USER_ALREADY_ACTIVATE_FRIEND_PROMO = 307,
            PROMO_NOT_FOUND = 308,
            ORDER_PUT_ERROR = 309,
            ORDER_CANT_COMMENTED = 310,
            ORDER_NOT_FOUND = 311,

            AIRPORTS_LIST_IS_NULL = 312,
            TO_ADDRESS_NOT_ASSIGNED = 313,
            OUT_OF_MAP_RANGE = 314,
            AIRPORT_AIRPORT_PRICE_NOT_FOUND = 315,
            AIRPORT_DISTRICT_PRICE_NOT_FOUND = 316,
            AIRPORT_AIRPORT_PRICE_LIST_NULL = 317,
            AIRPORT_DISTRICT_PRICE_LIST_NULL = 318,
            CURRENT_VERSION_LOW = 319,
            DISTRICT_LIST_IS_NULL = 419,
            NO_ADDRESS_SPECIFIED = 420,
            TARIFF_NOT_FOUND = 421,
            DATA_UPDATED = 422,
            DATA_CREATED = 423,
            DATA_DELETED = 424,
            LIST_IS_NULL = 425,
            ALREADY_EXIST = 426,

        }
    }
}

namespace EnergySystem.Common
{
    public static class EntityValidationConstants
    {
        public static class Property
        {
            public const int MinNameLength = 4;
            public const int MaxNameLength = 50;

            public const int MinAddressLength = 10;
            public const int MaxAddressLength = 60;

            public const int MinElectricityNeedLength = 2;
            public const int MaxElectricityNeedLength = 5;
        }

        public static class Battery
        {
            public const int MinModelLength = 3;
            public const int MaxModelLength = 100;

            public const int MinCapacity = 0;
            public const int MaxCapacity = 100000;

            public const int MinVoltage = 0;
            public const int MaxVoltage = 100000;

            public const int MinManufacturerLength = 3;
            public const int MaxManufacturerLength = 100;

            public const int MinCurrentChargeLevel = 0;
            public const int MaxCurrentChargeLevel = 100;

            public const int MinStateOfHealth = 0;
            public const int MaxStateOfHealth = 100;

            public const int MinTemperature = -50;
            public const int MaxTemperature = 80;
        }

        public static class User
        {
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;

            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 15;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 15;
        }
    }
}

namespace GameZone.Settings
{
	public static class FileSettings
	{
		public const string ImagePath = "/assets/images/games";
		public const string AllowedExtensions = ".jpg,.jpeg,.png";
		public const int MaxFileSizeinMb = 1;
		public const int MaxFileSizeInBytes = MaxFileSizeinMb * 1024 * 1024;
	}
}

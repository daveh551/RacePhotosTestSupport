﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoServer.Domain;

namespace RacePhotosTestSupport
{
    public static class ObjectMother
    {
	    public static string photoPath;
		public static void ClearDirectory()
		{
			SetPhotoPath();
			var directoryPath = Path.Combine(photoPath, "Test");
			var directoryInfo = new DirectoryInfo(photoPath);
			if (directoryInfo.Exists)
			{
				directoryInfo.Delete(true);
			}

		}

	    public static void SetPhotoPath()
	    {
		    if (string.IsNullOrWhiteSpace(photoPath))
		    {
			    photoPath = ConfigurationManager.AppSettings["PhotosPhysicalDirectory"];
		    }
	    }

	    private static PhotoServer.Domain.Photo[] testData =

	    new PhotoServer.Domain.Photo []
	    {
		    new Photo
			    {
				    Id = new Guid("E0CAF539-5C32-432B-AAC4-B01CD4EABB3A"),
				    Race = "Test",
				    Station = "FinishLine",
				    Card = "1",
				    Sequence = 1,
				    Path = @"Test/FinishLine/1/001.JPG",
				    Hres = 3008,
				    Vres = 2000,
				    TimeStamp = new DateTime(2011, 10, 22, 8, 48, 59, 60)
			    },
		    new Photo
			    {
				    Id = new Guid("24249DF5-C9FF-4B17-A259-514586F07C6B"),
				    Race = "Test",
				    Station = "FinishLine",
				    Card = "1",
				    Sequence = 3,
				    Path = @"Test/FinishLine/1/003.JPG",
				    Hres = 3008,
				    Vres = 2000,
				    TimeStamp = new DateTime(2011, 10, 22, 8, 29, 0)
			    },
		    new Photo
			    {
				    Id = new Guid("E5034D6B-3B26-4F03-8F9E-0EA1F5BE55C7"),
				    Race = "Test",
				    Station = "FinishLine",
				    Card = "1",
				    Sequence = 4,
				    Path = @"Test/FinishLine/1/004.JPG",
				    Hres = 3008,
				    Vres = 2000,
				    TimeStamp = new DateTime(2011, 10, 22, 8, 29, 0)
			    }
	    };


	    public static List<PhotoServer.Domain.Photo> ReturnPhotoDataRecord(int count)
	    {
		    var returnList = new List<Photo>();
		    if (count <= 3)
				for (int ix = 0; ix < count; ix++)
				{
					returnList.Add(testData[ix]);
				}

		    return returnList;

	    }

	    public static void CopyTestFiles()
	    {
			SetPhotoPath();
		    var sourcePhotos = new DirectoryInfo(@"..\..\..\PhotoServer_Tests\TestFiles").EnumerateFiles().ToList();
		    foreach (var photoData in testData)
		    {
			    var destFile = photoData.Path;
			    var fileName = Path.GetFileName(destFile);
			    var sourceFile = sourcePhotos.Where(p => p.Name.EndsWith(fileName)).Select(f => f.FullName).Single();
			    var destPath = Path.Combine(photoPath, destFile);
				Directory.CreateDirectory(Path.GetDirectoryName(destPath));
				File.Copy(sourceFile, destPath);
		    }
	    }
    }
}

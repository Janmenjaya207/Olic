//using OLIC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEL;

namespace BEL
{
    public static class UploadFileHelper
    {
        public static LCMS_CaseDoc SaveFileIntoLocal(HttpPostedFileBase file,string docname,int caseid)
        {
            var fileId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            var ext = file.FileName.Split('.');
            if (!(System.IO.Directory.Exists(HttpContext.Current.Server.MapPath($"~/FileUploaded"))))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath($"~/FileUploaded"));
            }

            var path = Path.Combine(HttpContext.Current.Server.MapPath($"~/FileUploaded"),
                                       Path.GetFileName(fileId));

            if (ext.Length >= 2)
                path = $"{path}.{ext[ext.Length - 1]}";

            file.SaveAs(path);

            LCMS_CaseDoc fileObject = new LCMS_CaseDoc()
            {
                CaseId=caseid,
                CreatedOn = DateTime.Now,
                DocumentName=docname,
                FileName = fileId,
                FileLocation = "FileUploaded",
                FileSize = file.ContentLength,
                CreatedBy = "IU",
                FileExtension = Path.GetExtension(path),
                IsActive=true,
                IsDelete=false
            };
            return fileObject;
        }
        public static LCMS_Case_Hearing_Doc SaveHearingFileIntoLocal(HttpPostedFileBase file, string docname, int caseid)
        {
            var fileId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            var ext = file.FileName.Split('.');
            if (!(System.IO.Directory.Exists(HttpContext.Current.Server.MapPath($"~/HearingFileUploaded"))))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath($"~/HearingFileUploaded"));
            }

            var path = Path.Combine(HttpContext.Current.Server.MapPath($"~/HearingFileUploaded"),
                                       Path.GetFileName(fileId));

            if (ext.Length >= 2)
                path = $"{path}.{ext[ext.Length - 1]}";

            file.SaveAs(path);

            LCMS_Case_Hearing_Doc fileObject = new LCMS_Case_Hearing_Doc()
            {
                Case_Hearing_Id=caseid,
                CreatedOn = DateTime.Now,
                DocumentName = docname,
                FileName = fileId,
                FileLocation = "HearingFileUploaded",
                FileSize = file.ContentLength,
                CreatedBy = "IU",
                FileExtension = Path.GetExtension(path),
                IsActive = true,
                IsDelete = false
            };
            return fileObject;
        }
        public static File Benificiary_SaveFileIntoLocal(HttpPostedFileBase file, FileTypes fileType)
        {
            var fileId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            var ext = file.FileName.Split('.');
            if (!(System.IO.Directory.Exists(HttpContext.Current.Server.MapPath($"~/FileUploadFolder"))))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath($"~/FileUploadFolder"));
            }

            var path = Path.Combine(HttpContext.Current.Server.MapPath($"~/FileUploadFolder"),
                                       Path.GetFileName(fileId));

            if (ext.Length >= 2)
                path = $"{path}.{ext[ext.Length - 1]}";

            file.SaveAs(path);

            File fileObject = new File()
            {
                FileId = fileId,
                CreatedOn = DateTime.Now,
                FileName = file.FileName,
                FileLocation = "FileUploadFolder",
                FileTypeId = (int)fileType,
                FileSize = file.ContentLength,
                CreatedBy = "Farmer",
                FileExtension = Path.GetExtension(path),
                ContentType = file.ContentType,
                SystemFileName = $"{fileId}{Path.GetExtension(path)}"
            };
            return fileObject;
        }

    }
}
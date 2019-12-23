using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Models;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ContractController : ResponseController
    {
        private IContractService contractService;

        public ContractController(IContractService contractService)
        {
            this.contractService = contractService;
        }

        // GET: api/Contract
        [HttpGet]
        public IActionResult Get()
        {
            List<Contract> contracts = contractService.GetList();

            if(contracts == null || contracts.Count == 0) return Error("Eşleşen kayıt bulunamadı.", 404);

            List<ContractModel> contractModels = new List<ContractModel>();

            contracts.ForEach(x => { contractModels.Add(ContractModel.DtoToModel(x)); });

            return Success(contractModels);
        }

        // GET: api/Contract/5
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return Error("Sözleşme bulunamadı.", 404);

            Contract contract = contractService.Get(id);

            if (contract == null) return Error("Sözleşme bulunamadı.", 404);

            return Success(ContractModel.DtoToModel(contract));
        }

        // GET: api/Contract/GetExpiringContracts
        [HttpGet("GetExpiringContracts")]
        public IActionResult GetExpiringContracts()
        {
            List<Contract> contracts = contractService.GetList();

            if (contracts == null || contracts.Count == 0) return Error("Eşleşen kayıt bulunamadı.", 404);

            List<ContractModel> oneWeekContracts = new List<ContractModel>();
            List<ContractModel> oneMonthContracts = new List<ContractModel>();
            List<ContractModel> twoMonthContracts = new List<ContractModel>();

            contracts.ForEach(x => {

                if(x.FinishDate < DateTime.Now.AddDays(7))
                {
                    oneWeekContracts.Add(ContractModel.DtoToModel(x));
                }
                else if (x.FinishDate < DateTime.Now.AddMonths(1))
                {
                    oneMonthContracts.Add(ContractModel.DtoToModel(x));
                }
                else if (x.FinishDate < DateTime.Now.AddMonths(2))
                {
                    twoMonthContracts.Add(ContractModel.DtoToModel(x));
                }
            });

            return Success(new { oneWeek = oneWeekContracts.Count, oneMonth = oneWeekContracts.Count, twoMonth = twoMonthContracts.Count, total = contracts.Count });
        }

        // GET: api/Contract/GetContractsByClient/clientId
        [HttpGet("GetContractsByClient/{clientId}")]
        public IActionResult GetContractsByClient(Guid clientId)
        {
            if (clientId == Guid.Empty) return Error("Müşteri bulunamadı.", 404);

            List<Contract> contracts = contractService.GetList(clientId);

            if (contracts == null || contracts.Count == 0) return Error("Eşleşen kayıt bulunamadı.", 404);

            List<ContractModel> contractModels = new List<ContractModel>();

            contracts.ForEach(x => { contractModels.Add(ContractModel.DtoToModel(x)); });

            return Success(contractModels, 200);
        }

        // POST: api/Contract
        [HttpPost]
        public IActionResult Post([FromBody] ContractModel contractModel)
        {
            if (contractModel.ClientId == Guid.Empty) return Error("Müşteri bulunamadı.", 404);

            if (contractModel.StartDate > contractModel.FinishDate) return Error("Sözleşme bitiş tarihi başlangıç tarihinden küçük olamaz.");

            if (!string.IsNullOrEmpty(contractModel.Code))
            {
                Contract isExists = contractService.Get(contractModel.Code);

                if (isExists != null) return Error("Bu koda ait bir sözleşme zaten var.");
            }

            Contract contract = ContractModel.ModelToDto(contractModel);
            contractService.Add(contract);

            if (contractService.Get(contract.Id) == null) return Error("Sözleşme eklenemedi.");

            return Success(ContractModel.DtoToModel(contract), 201);
        }

        // PUT: api/Contract/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ContractModel contractModel)
        {
            if (id == Guid.Empty) return Error("Sözleşme bulunamadı.", 404);

            Contract contract = contractService.Get(id);

            if(contract == null) return Error("Sözleşme bulunamadı.", 404);

            if (Guid.Empty == contractModel.ClientId) contract.ClientId = contractModel.ClientId;
            if (!string.IsNullOrEmpty(contractModel.Code))
            {
                Contract isExists = contractService.Get(contractModel.Code);

                if (isExists != null && id != isExists.Id) return Error("Güncellenmek istenen kod başka bir sözleşmeye ait.");

                contract.Code = contractModel.Code;
            }

            if (contractModel.StartDate != null) contract.StartDate = contractModel.StartDate;
            if (contractModel.FinishDate != null) contract.FinishDate = contractModel.FinishDate;
            if (contractModel.Currency > 0) contract.Currency = contractModel.Currency;
            if (contractModel.Supply > 0) contract.Supply = contractModel.Supply;
            if (contractModel.Amount > 0) contract.Amount = contractModel.Amount;

            if (contractModel.StartDate > contractModel.FinishDate) return Error("Sözleşme bitiş tarihi başlangıç tarihinden küçük olamaz.");

            contractService.Update(contract);

            return Success(ContractModel.DtoToModel(contract), 202);
        }

        // DELETE: api/Contract/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return Error("Sözleşme bulunamadı.", 404);

            Contract contract = contractService.Get(id);

            if (contract == null) return Error("Sözleşme bulunamadı.", 404);

            contractService.Delete(id);

            return Success(null, 204);
        }

        // GET: api/Contract/GetFiles/5
        [HttpGet("GetFiles/{id}")]
        public IActionResult GetFiles(Guid id)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(Path.Combine(filePath, "Contracts", id.ToString()))) return Error("Dosya bulunamadı.", 404);
            string[] fileEntries = Directory.GetFiles(Path.Combine(filePath, "Contracts", id.ToString()));

            List <object> files = new List<object>();

            for (int i = 0; i < fileEntries.Length; i++)
            {
                string url = fileEntries[i].Replace(filePath, BaseAddress).Replace('\\', '/').Replace("Contracts", "api/ContractFiles");

                FileInfo fileInfo = new FileInfo(fileEntries[i]);

                string decoded = HttpUtility.UrlDecode(fileInfo.Name);

                files.Add(new { url, id = fileInfo.Name, size = fileInfo.Length, name = decoded, type = Core.Helpers.MimeHelper.GetMimeFromType(fileInfo.Extension) });
            }

            return Success(new { ContractId = id, files });
        }

        // POST: api/Contract/Upload/5
        [HttpPost("Upload/{id}")]
        public IActionResult Upload(Guid id, [FromForm]UploadFileModel files)
        {

            if (id == Guid.Empty) return Error("Sözleşme bulunamadı.", 404);

            if (files == null || files.Files.Count == 0) return Error("Yüklenecek dosya bulunamadı.", 404);

            Contract contract = contractService.Get(id);

            if (contract == null) return Error("Sözleşme bulunamadı.", 404);

            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Contracts", id.ToString()));

            string uploadError = string.Empty;
            for (int i = 0; i < files.Files.Count; i++)
            {
                try
                {
                    if (files.Files[i].Length > 0)
                    {
                        FileInfo fileInfo = new FileInfo(files.Files[i].FileName);

                        if (string.IsNullOrEmpty(Core.Helpers.MimeHelper.GetMimeFromType(fileInfo.Extension))) throw new Exception("unavailable file type");

                        string encoded = HttpUtility.UrlEncode(files.Files[i].FileName);

                        string combined = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Contracts", id.ToString(), encoded);

                        using (var stream = System.IO.File.Create(combined)) files.Files[i].CopyTo(stream);
                    }
                }
                catch (Exception)
                {
                    if (uploadError.Length == 0) uploadError += files.Files[i].FileName;
                    else uploadError += ", " + files.Files[i].FileName;
                }
            }

            if (uploadError.Length > 0)
            {
                return Error(uploadError + " isimli dosyalar yüklenemedi.", 400);
            }

            return Success(null, 201);
        }

        // DELETE: api/Contract/DeleteFile/contractId/5
        [HttpDelete("DeleteFile/{contractId}/{id}")]
        public IActionResult DeleteFile(Guid contractId, string id)
        {
            if (contractId == Guid.Empty) return Error("Sözleşme bulunamadı.", 404);

            if (string.IsNullOrEmpty(id)) return Error("Dosya bulunamadı.", 404);

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Contracts", contractId.ToString());

            if (Directory.Exists(filePath))
            {
                string[] fileEntries = Directory.GetFiles(filePath);

                foreach (var fileEntry in fileEntries)
                {
                    if (fileEntry.Contains(id.ToString()))
                    {
                        System.IO.File.Delete(fileEntry);
                        return Success(null, 204);
                    }  
                }

                return Error("Dosya bulunamadı.", 404);
            }
            else return Error("Sözleşme bulunamadı.", 404);
        }
    }
}

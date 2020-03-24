using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ML.Xscf.Docs.Models.DatabaseModel.Dto;
using ML.Xscf.Docs.Services;
using Senparc.CO2NET.Trace;
using Senparc.Scf.Core.Models;

namespace ML.Xscf.Docs.Areas.Admin.Pages.Document
{
    public class EditModel : BaseAdminPageModel
    {
        private readonly CatalogService _catalogService;

        public EditModel(CatalogService _catalogService)
        {
            CurrentMenu = "Catalog";
            this._catalogService = _catalogService;
        }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public CatalogDto CatalogDto { get; set; }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var entity = await _catalogService.GetObjectAsync(_ => _.Id == Convert.ToInt32(Id));
                //SysButtons = await _sysButtonService.GetFullListAsync(_ => _.MenuId == Id);

                //SysMenuDto = _sysMenuService.Mapper.Map<SysMenuDto>(entity);
            }
            else
            {
                CatalogDto = new CatalogDto() { Flag = false };

                //SysMenuDto = new SysMenuDto() { Visible = true };
                //SysButtons = new List<SysButton>() { new SysButton() };
            }
        }

        public async Task<IActionResult> OnGetDetailAsync(int id)
        {
            var entity = await _catalogService.GetObjectAsync(_ => _.Id == id);
            var catalogDto = _catalogService.Mapper.Map<CatalogDto>(entity);
            return Ok(new { catalogDto });
        }

        /// <summary>
        /// 获取目录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGetCatalogAsync()
        {
            return Ok(await _catalogService.GetCatalogDtoByCacheAsync());
        }

        //public async Task<IActionResult> OnPostDeleteAsync(string[] ids)
        //{
        //    var entity = await _sysMenuService.GetFullListAsync(_ => ids.Contains(_.Id) && _.IsLocked == false);
        //    var buttons = await _sysButtonService.GetFullListAsync(_ => ids.Contains(_.MenuId));

        //    await _sysButtonService.DeleteAllAsync(buttons);
        //    await _sysMenuService.DeleteAllAsync(entity);
        //    await _sysMenuService.RemoveMenuAsync();
        //    IEnumerable<string> unDeleteIds = ids.Except(entity.Select(_ => _.Id));
        //    return Ok(unDeleteIds);
        //}

        public async Task<IActionResult> OnPostAddCatalogAsync(CatalogDto catalog)
        {
            if (string.IsNullOrEmpty(catalog.Name))
            {
                return Ok("目录名称不能为空", false, "目录名称不能为空");
            }
            var entity = await _catalogService.CreateOrUpdateAsync(catalog);
            return Ok(entity.Id);
        }

        //public async Task<IActionResult> OnPostAsync(CatalogDto catalogDto, IEnumerable<CatalogDto> subCatalog)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Ok("模型验证未通过", false, "模型验证未通过");
        //    }

        //    await _catalogService.CreateOrUpdateAsync(catalogDto, subCatalog);

        //    return Ok(new { subCatalog, catalogDto });
        //}
    }
}
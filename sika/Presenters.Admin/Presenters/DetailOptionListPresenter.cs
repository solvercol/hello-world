﻿using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Presenters.Admin.Presenters
{
    public class DetailOptionListPresenter: Presenter<IDetailOptionListView>
    {
        private readonly ISfTBL_Admin_OptionListManagementServices _optionList;
        private readonly ISfTBL_Admin_UsuariosManagementServices _usuarios;

        public DetailOptionListPresenter(ISfTBL_Admin_OptionListManagementServices optionList,
            ISfTBL_Admin_UsuariosManagementServices usuarios)
        {
            _optionList = optionList;
            _usuarios = usuarios;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            Load();
        }

        private void Load()
        {

            GetOptionList();
        }

        private void GetOptionList()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdOpcion)) return;
                var op = _optionList.FindById(Convert.ToInt32(View.IdOpcion));
                if (op == null) return;
                var createdBy = _usuarios.FindById(Convert.ToInt32(op.CreateBy));
                var modifiedBy = _usuarios.FindById(Convert.ToInt32(op.ModifiedBy));
                View.IdModulo = op.IdModule.ToString();
                View.key = op.Key;
                View.value = op.Value;
                View.descripcion = op.Descripcion;
                View.Activo = op.IsActive;
                View.CreateBy = createdBy.Nombres;
                View.CreateOn = op.CreateOn != null ? op.CreateOn.GetValueOrDefault().ToShortDateString() : string.Empty;
                View.ModifiedBy = modifiedBy.Nombres;
                View.ModifiedOn = op.ModifiedOn != null ? op.ModifiedOn.GetValueOrDefault().ToShortDateString() : string.Empty;
           
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }


        }
    }
}
